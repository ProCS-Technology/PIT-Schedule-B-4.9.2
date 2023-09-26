using ProcsDLL.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProcsDLL.InsiderTrading
{
    public partial class Send_Welcome_Email_Password : System.Web.UI.Page
    {
        string defaultEmail = String.Empty;
        string disclosureEmail = String.Empty;
        string smtpHostName = String.Empty;
        Int32 port;
        bool ssl = false;
        string smtpUserName = String.Empty;
        string password = String.Empty;
        bool userDefaultCredential;
        
        String ConnectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                GetEmailTemplate();
                
            }

        }
        protected void GetEmailTemplate()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                var database = Convert.ToString(Session["ModuleDatabase"]);
                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);


                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT TEMPLATE_ID, TEMPLATE_SUBJECT,TEMPLATE_EVENT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR WHERE TEMPLATE_EVENT IN ('Welcome Email AD/SAML','Welcome Email Application') AND COMPANY_ID=" + CompanyId, con
                    ))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        try
                        {
                            con.Open();
                            con.ChangeDatabase(database);
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["TEMPLATE_EVENT"].ToString() == "Welcome Email AD/SAML")
                                {
                                    HiddenFieldAdTemplateId.Value = row["TEMPLATE_ID"].ToString();
                                    TextareaAdTemplate.InnerHtml = row["TEMPLATE_BODY"].ToString();
                                }
                                else if (row["TEMPLATE_EVENT"].ToString() == "Welcome Email Application")
                                {
                                    HiddenFieldApplicationTemplateId.Value = row["TEMPLATE_ID"].ToString();
                                    TextareaAppTemplate.InnerHtml = row["TEMPLATE_BODY"].ToString();
                                }
                                else
                                {
                                    LabelMsg.Text = "Template Not Found";
                                }
                            }

                            TextBoxSubject.Text = dt.Rows[0]["TEMPLATE_SUBJECT"].ToString();

                        }
                        catch (SqlException ex)
                        {
                            LabelMsg.Text = ex.ToString();

                        }
                        con.Close();
                    }
                }
            }
        }
        protected void ButtonSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                GetUserList();

            }
            catch (Exception ex)
            {
                LabelMsg.Text = ex.ToString();
            }

        }

        protected void GetUserList()
        {

            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                var database = Convert.ToString(Session["ModuleDatabase"]);
                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);


                //GetSmtpDetails();
                DataTable dt = new DataTable();

                var _query = "SELECT A.ID, A.USER_EMAIL,A.USER_NM,B.USER_TYPE,A.LOGIN_ID FROM  " + AdminDB + "..PROCS_USERS A INNER JOIN PROCS_INSIDER_USER B  ON A.LOGIN_ID=B.USER_LOGIN  WHERE ";
                if (!String.IsNullOrEmpty(TextBoxLoginId.Text) || !String.IsNullOrWhiteSpace(TextBoxLoginId.Text))
                {
                    _query += "A.LOGIN_ID IN (" + TextBoxLoginId.Text + ") AND ";
                }
                _query += "B.COMPANY_ID=" + CompanyId;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(
                    _query, con
                    ))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        try
                        {
                            con.Open();
                            con.ChangeDatabase(database);
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);


                            foreach (DataRow row in dt.Rows)
                            {
                                List<string> attachments = new List<string>();

                                if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf"))))
                                {
                                    attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));

                                }

                                if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Code-Of-Conduct.pdf"))))
                                {
                                    attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Code-Of-Conduct.pdf")));
                                }

                                if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf"))))
                                {
                                    attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf")));
                                }
                                String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);

                               
                                if (row["USER_TYPE"].ToString() == "AD/SAML")
                                {
                                    
                                    EmailSender.SendMail(
                                       row["USER_EMAIL"].ToString(),
                                       TextBoxSubject.Text,
                                       TextareaAdTemplate.Value.Replace("[UserId]", row["LOGIN_ID"].ToString()).Replace("[USER_NM]", row["USER_NM"].ToString()),
                                       attachments, "Welcome Email", Convert.ToString(Session["CompanyId"]),
                                       strCC, null, Convert.ToString(Session["EmployeeId"])
                                   );

                                }
                                else if (row["USER_TYPE"].ToString() == "Application")
                                {
                                    string NewPassword = "P@ssw0rd" + row["ID"].ToString();
                                    string EncryptedPassword = hashcodegenerate.GetSHA512(NewPassword);

                                    UpdatePassword(int.Parse(row["ID"].ToString()), EncryptedPassword);

                                    
                                    EmailSender.SendMail(
                                       row["USER_EMAIL"].ToString(),
                                       TextBoxSubject.Text,
                                       TextareaAppTemplate.Value.Replace("[UserId]",row["LOGIN_ID"].ToString()).Replace("[USER_NM]", row["USER_NM"].ToString()).Replace("[Password]", NewPassword),
                                       attachments, "Welcome Email", Convert.ToString(Session["CompanyId"]),
                                       strCC, null, Convert.ToString(Session["EmployeeId"])
                                   );
                                }
                                else
                                {
                                    LabelMsg.Text += "LoginId:" + row["LOGIN_ID"].ToString() + " User Type Undefined";
                                }

                            }
                        }
                        catch (SqlException ex)
                        {
                            LabelMsg.Text = ex.ToString();

                        }
                        con.Close();
                    }
                }
            
            }
        }

        protected void GetSmtpDetails()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                var database = Convert.ToString(Session["ModuleDatabase"]);
                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                DataTable dtsmtp = new DataTable();

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SP_PROCS_INSIDER_CONFIG_SMTP", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MODE", SqlDbType.NVarChar).Value = "GET_Smtp_Config_List";
                        cmd.Parameters.Add("@SET_COUNT", SqlDbType.Int);
                        cmd.Parameters["@SET_COUNT"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = CompanyId;
                        try
                        {
                            con.Open();
                            con.ChangeDatabase(database);
                            sda.SelectCommand = cmd;
                            sda.Fill(dtsmtp);
                            if (dtsmtp.Rows.Count > 0)
                            {
                                defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(dtsmtp.Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                                disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(dtsmtp.Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                                smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(dtsmtp.Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                                port = Convert.ToInt32(dtsmtp.Rows[0]["PORT"]);
                                ssl = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["SSL"]))) ? (Convert.ToString(dtsmtp.Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                                smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(dtsmtp.Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                                password = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(dtsmtp.Rows[0]["PASSWORD"]), true) : String.Empty;
                                userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(dtsmtp.Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                            }
                        }
                        catch (SqlException ex)
                        {
                            LabelMsg.Text = ex.ToString();

                        }
                        con.Close();
                    }
                }
            }
        }
        protected void SendMail(string defaultEmail, string disclosureEmail, string To, string Subject, string Message, string smtpHostName, bool ssl, string smtpUserName, string password, bool userDefaultCredential, Int32 port)
        {
            
            using (MailMessage objMailMsg = new MailMessage(defaultEmail, To))
            {
                if (!String.IsNullOrEmpty(TextBoxBccEmail.Text) || !String.IsNullOrWhiteSpace(TextBoxBccEmail.Text))
                {
                    objMailMsg.Bcc.Add(new MailAddress(TextBoxBccEmail.Text));
                }
                
                objMailMsg.From = new MailAddress(defaultEmail, disclosureEmail);
                objMailMsg.Subject = Subject;
                objMailMsg.Body = Message;
                objMailMsg.IsBodyHtml = true;



                if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf"))))
                {
                    Attachment attach = new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));
                    objMailMsg.Attachments.Add(attach);
                }

                if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Code-Of-Conduct.pdf"))))
                {
                    Attachment attachCod = new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Code-Of-Conduct.pdf")));
                    objMailMsg.Attachments.Add(attachCod);
                }

                if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf"))))
                {
                    Attachment attachComplianceTrainingModule = new Attachment(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf")));
                    objMailMsg.Attachments.Add(attachComplianceTrainingModule);
                }


                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpHostName;
                smtp.EnableSsl = ssl;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential NetworkCred = new System.Net.NetworkCredential(smtpUserName, password);
                smtp.UseDefaultCredentials = userDefaultCredential;
                smtp.Credentials = NetworkCred;
                smtp.Port = port;
                try
                {
                    smtp.Send(objMailMsg);
                    LabelMsg.Text = "Success!";
                }
                catch (Exception ex)
                {
                    LabelMsg.Text = ex.ToString();
                }
            }
        }

        protected void LinkButtonSendTestMail_Click(object sender, EventArgs e)
        {
            GetSmtpDetails();
            SendMail(
                defaultEmail, disclosureEmail, txtTestEmail.Value,TextBoxSubject.Text,
                TextareaAdTemplate.Value.Replace("[UserId]", txtTestEmail.Value),
                smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port
                   );

            SendMail(
                defaultEmail, disclosureEmail, txtTestEmail.Value,TextBoxSubject.Text,
                TextareaAppTemplate.Value.Replace("[UserId]", txtTestEmail.Value),
                smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port
                    );
            
        }

        

        protected void LinkButtonSaveTemplate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                UploadUserManual();

                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                var database = Convert.ToString(Session["ModuleDatabase"]);
                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                var ApplicationId = HiddenFieldApplicationTemplateId.Value;
                var AdSamlId = HiddenFieldAdTemplateId.Value;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("UPDATE PROCS_INSIDER_EMAILS_TEMPLATE_HDR SET TEMPLATE_BODY='" + TextareaAdTemplate.InnerHtml + "', TEMPLATE_SUBJECT='" + TextBoxSubject.Text + "' WHERE TEMPLATE_ID= " + AdSamlId + " AND COMPANY_ID=" + CompanyId, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        con.Open();
                        con.ChangeDatabase(database);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        LabelMsg.Text = "AD/SAML-----" + ex.ToString();
                    }
                }

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("UPDATE PROCS_INSIDER_EMAILS_TEMPLATE_HDR SET TEMPLATE_BODY='" + TextareaAppTemplate.InnerHtml + "' WHERE TEMPLATE_ID= " + ApplicationId + " AND COMPANY_ID=" + CompanyId, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        con.Open();
                        con.ChangeDatabase(database);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    catch (Exception ex)
                    {
                        LabelMsg.Text = "Application-----" + ex.ToString();
                    }
                }
                LabelMsg.Text = "Success";
            }
        }

        protected void UpdatePassword(int Id, string Password)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("UPDATE PROCS_USERS SET USER_PWD='" + Password + "' WHERE ID= " + Id, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    try
                    {
                        con.Open();
                        con.ChangeDatabase(AdminDB);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        LabelMsg.Text = "failed to update password----" + ex.ToString();
                    }
                }
            }
        }

        protected void UploadUserManual()
        {
            try
            {

                if (FileUpload1.HasFiles)
                {
                    string strFileName = "User-Manual";

                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();

                    if (Extension == ".pdf")
                    {
                        string strFilePath;
                        string strFolder;
                        strFolder = Server.MapPath("UserManual/");
                        strFilePath = strFolder + strFileName + Extension;

                        // Create the directory if it does not exist.
                        if (!Directory.Exists(strFolder))
                        {
                            Directory.CreateDirectory(strFolder);
                        }

                        FileUpload1.PostedFile.SaveAs(strFilePath);
                    }
                    else
                    {
                        LabelMsg.Text = "Invalid File";
                    }

                }
            }
            catch (Exception ex)
            {
                LabelMsg.Text = "Error While Uploading User Manual----" + ex.ToString();
            }
        }
    }
}