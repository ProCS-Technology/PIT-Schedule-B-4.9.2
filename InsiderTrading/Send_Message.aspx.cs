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
using System.Web.Services;
using ProcsDLL.Models.InsiderTrading.Model;
namespace ProcsDLL.InsiderTrading
{
    public partial class Send_Message : System.Web.UI.Page
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

        private const string sessionKey = "CustomEmail";
        public Progress Progress
        {
            get
            {
                if (Session[sessionKey] == null)
                {
                    Session.Add(sessionKey, new Progress());
                }
                return Session[sessionKey] as Progress;
            }
            set
            {
                if (Session[sessionKey] == null)
                {
                    Session.Add(sessionKey, new Progress());
                }
                Session[sessionKey] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fnGetUsers();
                BindRelative();
                BindRoleData();
                BindCP();
            }

        }
        private void fnGetUsers()
        {
            ddlUsers.Multiple = true;
            string sCmpnId = Convert.ToString(Session["CompanyId"]);
            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    string sql = "SELECT A.USER_NM,A.USER_EMAIL FROM " + sAdminDb + "..PROCS_USERS(NOLOCK) A " +
                        "INNER JOIN PROCS_INSIDER_USER(NOLOCK) B ON A.LOGIN_ID=B.USER_LOGIN " +
                        "WHERE B.STATUS='Active' AND B.COMPANY_ID=" + sCmpnId + " ORDER BY A.USER_NM";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dtAccess = new DataTable();
                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                        daAccess.Fill(dtAccess);
                        if (dtAccess.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtAccess.Rows)
                            {
                                string userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                                string userEmail = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                                ddlUsers.Items.Add(new ListItem(userName, userEmail));
                            }
                        }
                    }
                }
            }
        }


        public delegate void Run();
        protected void ButtonSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                //int iUser = ddlUsers.SelectedIndex;
                //string sUser = txtSelectedUsers.Text;
                //string bccUsers = TextBoxBccEmail.Text;
                //string OtherEmailId = TextOtherEmail.Text;

                //string selected = Request.Form["TreeSelect"];
                //if (string.IsNullOrEmpty(selected))
                //{
                //    selected = OtherEmailId;
                //}
                //ClientScript.RegisterStartupScript(this.GetType(), "", selected, true);

                //if (OtherEmailId == "" && selected == null)
                //{
                //}
                //else
                //{

                //    string[] arrAllRole = selected.Split(new char[] { ',' });
                //    string[] arrOtherEmailId = OtherEmailId.Split(new char[] { ',' });
                //    if (OtherEmailId == "")
                //    {
                //        string[] result = arrAllRole;
                //        string[] arrUsers = sUser.Split(new char[] { ',' });
                //        List<string> attachments = new List<string>();
                //        if (FileUpload1.HasFiles)
                //        {
                //            string strFolder;
                //            string strFilePath;
                //            strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");
                //            if (!Directory.Exists(strFolder))
                //            {
                //                Directory.CreateDirectory(strFolder);
                //            }
                //            foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                //            {
                //                strFilePath = strFolder + uploadedFile.FileName;
                //                uploadedFile.SaveAs(strFilePath);
                //                attachments.Add(strFilePath);
                //            }
                //        }

                //        int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                //        var database = Convert.ToString(Session["ModuleDatabase"]);
                //        var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                //        foreach (string sEmails in result)
                //        {
                //            EmailSender.SendMail(
                //            sEmails, TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", sEmails).ToString(),
                //            attachments, "Custom Email", Convert.ToString(Session["CompanyId"]), bccUsers, null, Convert.ToString(Session["EmployeeId"])
                //            );
                //        }
                //    }
                //    else
                //    {
                //        string[] result = arrAllRole.Concat(arrOtherEmailId).ToArray();
                //        string[] arrUsers = sUser.Split(new char[] { ',' });
                //        List<string> attachments = new List<string>();
                //        if (FileUpload1.HasFiles)
                //        {
                //            string strFolder;
                //            string strFilePath;
                //            strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");
                //            if (!Directory.Exists(strFolder))
                //            {
                //                Directory.CreateDirectory(strFolder);
                //            }
                //            foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                //            {
                //                strFilePath = strFolder + uploadedFile.FileName;
                //                uploadedFile.SaveAs(strFilePath);
                //                attachments.Add(strFilePath);
                //            }
                //        }

                //        int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                //        var database = Convert.ToString(Session["ModuleDatabase"]);
                //        var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                //        foreach (string sEmails in result)
                //        {
                //            EmailSender.SendMail(
                //            sEmails, TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", sEmails).ToString(),
                //            attachments, "Custom Email", Convert.ToString(Session["CompanyId"]), bccUsers, null, Convert.ToString(Session["EmployeeId"])
                //            );
                //        }
                //    }

                //}
                hdnEmailTask.Text = "Start";
                Progress = null;
                Progress.Add(new ProgressStep("Process Started", ProgressStatus.Started, "Process Started"));
                Run run = new Run(SendCustomEmail);
                IAsyncResult res = run.BeginInvoke((IAsyncResult ar) =>
                {
                    Progress.Add(new ProgressStep("Process Completed", ProgressStatus.Completed, "All emails sent"));
                }, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){fnChkStatus();},0);", true);

            }

            catch (Exception ex)
            {
                LabelMsg.Text = ex.ToString();
            }

        }

      
        [WebMethod(EnableSession = true)]
        public static string CheckDownload()
        {
            return HttpContext.Current.Session[sessionKey] == null ? string.Empty : ((Progress)HttpContext.Current.Session[sessionKey]).ToString();
        }


        public void SendCustomEmail()
        {
            try
            {
                int iUser = ddlUsers.SelectedIndex;
                string sUser = txtSelectedUsers.Text;
                string bccUsers = TextBoxBccEmail.Text;
                string OtherEmailId = TextOtherEmail.Text;

                string selected = Request.Form["TreeSelect"];
                if (string.IsNullOrEmpty(selected))
                {
                    selected = OtherEmailId;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "", selected, true);

                if (OtherEmailId == "" && selected == null)
                {
                }
                else
                {
                    string[] arrAllRole = selected.Split(new char[] { ',' });
                    string[] arrOtherEmailId = OtherEmailId.Split(new char[] { ',' });
                    if (OtherEmailId == "")
                    {
                        string[] result = arrAllRole;
                        string[] arrUsers = sUser.Split(new char[] { ',' });
                        List<string> attachments = new List<string>();
                        if (FileUpload1.HasFiles)
                        {
                            string strFolder;
                            string strFilePath;
                            strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");
                            if (!Directory.Exists(strFolder))
                            {
                                Directory.CreateDirectory(strFolder);
                            }
                            foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                            {
                                strFilePath = strFolder + uploadedFile.FileName;
                                uploadedFile.SaveAs(strFilePath);
                                attachments.Add(strFilePath);
                            }
                        }
                        int CompanyId = 0;                        
                        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["CompanyId"] != null)
                        {
                             CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                        }
                        var database = Convert.ToString(Session["ModuleDatabase"]);
                        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["AdminDB"] != null)
                        {
                            var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                        }

                        foreach (string sEmails in result)
                        {
                            EmailSender.SendMail(
                            sEmails, TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", sEmails).ToString(),
                            attachments, "Custom Email", Convert.ToString(Session["CompanyId"]), bccUsers, null, Convert.ToString(Session["EmployeeId"])
                            );
                            Progress.Add(new ProgressStep(string.Format("Notified to {0}", sEmails), ProgressStatus.InProgress));
                        }
                    }
                    else
                    {
                        string[] result = arrAllRole.Concat(arrOtherEmailId).ToArray();
                        string[] arrUsers = sUser.Split(new char[] { ',' });
                        List<string> attachments = new List<string>();
                        if (FileUpload1.HasFiles)
                        {
                            string strFolder;
                            string strFilePath;
                            strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");
                            if (!Directory.Exists(strFolder))
                            {
                                Directory.CreateDirectory(strFolder);
                            }
                            foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                            {
                                strFilePath = strFolder + uploadedFile.FileName;
                                uploadedFile.SaveAs(strFilePath);
                                attachments.Add(strFilePath);
                            }
                        }

                        int CompanyId = 0;
                        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["CompanyId"] != null)
                        {
                            CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                        }
                        var database = Convert.ToString(Session["ModuleDatabase"]);
                        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["AdminDB"] != null)
                        {
                            var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                        }

                        //int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                        //var database = Convert.ToString(Session["ModuleDatabase"]);
                        //var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                        foreach (string sEmails in result)
                        {
                            EmailSender.SendMail(
                            sEmails, TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", sEmails).ToString(),
                            attachments, "Custom Email", Convert.ToString(Session["CompanyId"]), bccUsers, null, Convert.ToString(Session["EmployeeId"])
                            );
                            Progress.Add(new ProgressStep(string.Format("Notified to {0}", sEmails), ProgressStatus.InProgress));
                        }
                    }
                }              
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


                GetSmtpDetails();
                DataTable dt = new DataTable();


                if (!String.IsNullOrEmpty(TextBoxLoginId.Text) || !String.IsNullOrWhiteSpace(TextBoxLoginId.Text))
                {
                    var _query = string.Empty;
                    if (TextBoxLoginId.Text.ToLower() == "all")
                    {
                        _query = "SELECT A.USER_EMAIL,A.USER_NM,B.USER_TYPE,A.LOGIN_ID FROM  " + AdminDB + "..PROCS_USERS A INNER JOIN PROCS_INSIDER_USER B  ON A.LOGIN_ID=B.USER_LOGIN  WHERE B.STATUS='Active' AND B.COMPANY_ID=" + CompanyId;

                    }
                    else
                    {
                        String LoginId = TextBoxLoginId.Text.Replace(",", "','");

                        _query = "SELECT A.USER_EMAIL,A.USER_NM,B.USER_TYPE,A.LOGIN_ID FROM  " + AdminDB + "..PROCS_USERS A INNER JOIN PROCS_INSIDER_USER B  ON A.LOGIN_ID=B.USER_LOGIN  WHERE B.STATUS='Active' AND A.LOGIN_ID IN ('" + LoginId + "') AND B.COMPANY_ID=" + CompanyId;

                    }

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
                                    if (FileUpload1.HasFiles)
                                    {
                                        string strFolder;
                                        string strFilePath;
                                        strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");

                                        if (!Directory.Exists(strFolder))
                                        {
                                            Directory.CreateDirectory(strFolder);
                                        }

                                        foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                                        {
                                            strFilePath = strFolder + uploadedFile.FileName;
                                            uploadedFile.SaveAs(strFilePath);

                                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath(strFilePath)));

                                        }
                                    }
                                    String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                                    EmailSender.SendMail(
                                        row["USER_EMAIL"].ToString(), TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", row["LOGIN_ID"].ToString()).Replace("[USER_NM]", row["USER_NM"].ToString()), attachments, "Custom Email", Convert.ToString(Session["CompanyId"]),
                                        strCC, null, Convert.ToString(Session["EmployeeId"])
                                    );
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
                else
                {
                    LabelMsg.Text = "Please enter user to send email.";
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

                if (FileUpload1.HasFiles)
                {
                    string strFolder;
                    string strFilePath;
                    strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");

                    if (!Directory.Exists(strFolder))
                    {
                        Directory.CreateDirectory(strFolder);
                    }

                    foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                    {
                        strFilePath = strFolder + uploadedFile.FileName;
                        uploadedFile.SaveAs(strFilePath);

                        Attachment attachComplianceTrainingModule = new Attachment(strFilePath);
                        objMailMsg.Attachments.Add(attachComplianceTrainingModule);

                    }
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
                defaultEmail, disclosureEmail, txtTestEmail.Value, TextBoxSubject.Text,
                TextareaAppTemplate.Value.Replace("[UserId]", txtTestEmail.Value),
                smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port
                   );


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
        public void BindRelative()
        {
            string sCmpnId = Convert.ToString(Session["CompanyId"]);
            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT RELATIVE_NAME,RELATIVE_EMAIL FROM PROCS_INSIDER_RELATIVES_DETAIL WHERE ISNULL(RELATIVE_EMAIL,'')<>'' AND RELATIVE_EMAIL<>'Not Applicable' AND STATUS = 'ACTIVE'", conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    Datalist1.DataSource = dt;
                    Datalist1.DataBind();
                }
            }

        }
        public void BindCP()
        {
            string sCmpnId = Convert.ToString(Session["CompanyId"]);
            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT CP_NAME,CP_EMAIL FROM PROCS_INSIDER_CONNECTED_PERSONS(NOLOCK) WHERE ISNULL(CP_EMAIL,'')<>'' AND CP_STATUS = 'ACTIVE'", conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ddlConnected.DataSource = dt;
                    ddlConnected.DataBind();
                }
            }

        }
        public void BindRoleData()
        {

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@MODE", "GET_USER_ROLE_DETAILS");
            parameters[1] = new SqlParameter("@AdminDb", Convert.ToString(Session["AdminDb"]));


            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_ROLE_DETAILS", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
            if (ds.Tables.Count > 0)
            {
                ddlRole.DataSource = ds.Tables[0];
                ddlRole.DataBind();

                DataTable dt = ds.Tables[1];
                DataTable dat = ds.Tables[2];

                for (int i = 0; i < ddlRole.Items.Count; i++)
                {
                    DataTable dtRoleUsr = new DataTable();
                    dtRoleUsr.Columns.Add("USER_NM", typeof(string));
                    dtRoleUsr.Columns.Add("USER_EMAIL", typeof(string));
                    dtRoleUsr.Columns.Add("ROLE_NAME", typeof(string));

                    Label Label1 = (Label)ddlRole.Items[i].FindControl("Itmnamelbl1");
                    string labeltext = Label1.Text;

                    if (labeltext != "Connected Person")
                    {
                        for (int c = 0; c < dt.Rows.Count; c++)
                        {
                            DataRow dr = dt.Rows[c];
                            string s = Convert.ToString(dr["ROLE_NAME"]);
                            if (labeltext == s)
                            {
                                dtRoleUsr.ImportRow(dr);
                            }
                        }
                        DataList dl1 = (DataList)ddlRole.Items[i].FindControl("ddlCP");
                        dl1.DataSource = dtRoleUsr;
                        dl1.DataBind();
                    }
                    else
                    {
                        for (int d = 0; d < dat.Rows.Count; d++)
                        {
                            DataRow dr1 = dat.Rows[d];
                            string s = Convert.ToString(dr1["CP_NAME"]);
                            if (labeltext == s)
                                dat.ImportRow(dr1);
                            DataList dl1 = (DataList)ddlRole.Items[i].FindControl("ddlConnected");

                            dl1.DataSource = dat;
                            dl1.DataBind();
                        }

                    }
                }
            }

        }

    }
}
//using ProcsDLL.Models.Infrastructure;
 //using System;
 //using System.Collections.Generic;
 //using System.Configuration;
 //using System.Data;
 //using System.Data.SqlClient;
 //using System.IO;
 //using System.Linq;
 //using System.Net;
 //using System.Net.Mail;
 //using System.Web;
 //using System.Web.UI;
 //using System.Web.UI.WebControls;
 //namespace ProcsDLL.InsiderTrading
 //{
 //    public partial class Send_Message : System.Web.UI.Page
 //    {
 //        string defaultEmail = String.Empty;
 //        string disclosureEmail = String.Empty;
 //        string smtpHostName = String.Empty;
 //        Int32 port;
 //        bool ssl = false;
 //        string smtpUserName = String.Empty;
 //        string password = String.Empty;
 //        bool userDefaultCredential;
 //        String ConnectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
 //        protected void Page_Load(object sender, EventArgs e)
 //        {
 //            if (!Page.IsPostBack)
 //            {
 //                fnGetUsers();
 //                BindRelative();
 //                BindRoleData();
 //                BindCP();
 //            }

//        }
//        private void fnGetUsers()
//        {
//            ddlUsers.Multiple = true;
//            string sCmpnId = Convert.ToString(Session["CompanyId"]);
//            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
//            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
//            {
//                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
//                using (SqlConnection conn = new SqlConnection(ConnStr))
//                {
//                    conn.Open();
//                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
//                    string sql = "SELECT A.USER_NM,A.USER_EMAIL FROM " + sAdminDb + "..PROCS_USERS(NOLOCK) A " +
//                        "INNER JOIN PROCS_INSIDER_USER(NOLOCK) B ON A.LOGIN_ID=B.USER_LOGIN " +
//                        "WHERE B.STATUS='Active' AND B.COMPANY_ID=" + sCmpnId + " ORDER BY A.USER_NM";
//                    using (SqlCommand cmd = new SqlCommand(sql, conn))
//                    {
//                        DataTable dtAccess = new DataTable();
//                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
//                        daAccess.Fill(dtAccess);
//                        if (dtAccess.Rows.Count > 0)
//                        {
//                            foreach (DataRow dr in dtAccess.Rows)
//                            {
//                                string userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
//                                string userEmail = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
//                                ddlUsers.Items.Add(new ListItem(userName, userEmail));
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        protected void ButtonSend_OnClick(object sender, EventArgs e)
//        {
//            try
//            {
//                int iUser = ddlUsers.SelectedIndex;
//                string sUser = txtSelectedUsers.Text;
//                string bccUsers = TextBoxBccEmail.Text;
//                string OtherEmailId = TextOtherEmail.Text;

//                string selected = Request.Form["TreeSelect"];
//                ClientScript.RegisterStartupScript(this.GetType(), "", selected, true);

//                if (selected == " " || selected == null)
//                {
//                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Processing failed, because of system error !');", true);

//                }
//                else
//                {
//                    string[] arrAllRole = selected.Split(new char[] { ',' });
//                    string[] arrOtherEmailId = OtherEmailId.Split(new char[] { ',' });
//                    string[] result = arrAllRole.Concat(arrOtherEmailId).ToArray();
//                    string[] arrUsers = sUser.Split(new char[] { ',' });
//                    List<string> attachments = new List<string>();
//                    if (FileUpload1.HasFiles)
//                    {
//                        string strFolder;
//                        string strFilePath;
//                        strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");
//                        if (!Directory.Exists(strFolder))
//                        {
//                            Directory.CreateDirectory(strFolder);
//                        }
//                        foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
//                        {
//                            strFilePath = strFolder + uploadedFile.FileName;
//                            uploadedFile.SaveAs(strFilePath);
//                            attachments.Add(strFilePath);
//                        }
//                    }

//                    int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
//                    var database = Convert.ToString(Session["ModuleDatabase"]);
//                    var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

//                    foreach (string sEmails in result)
//                    {
//                        EmailSender.SendMail(
//                        sEmails, TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", sEmails).ToString(),
//                        attachments, "Custom Email", Convert.ToString(Session["CompanyId"]), bccUsers, null, Convert.ToString(Session["EmployeeId"])
//                        );
//                    }
//                }

//            }

//            catch (Exception ex)
//            {
//                LabelMsg.Text = ex.ToString();
//            }
//        }
//        protected void GetUserList()
//        {

//            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
//            {
//                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
//                var database = Convert.ToString(Session["ModuleDatabase"]);
//                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);


//                GetSmtpDetails();
//                DataTable dt = new DataTable();


//                if (!String.IsNullOrEmpty(TextBoxLoginId.Text) || !String.IsNullOrWhiteSpace(TextBoxLoginId.Text))
//                {
//                    var _query = string.Empty;
//                    if (TextBoxLoginId.Text.ToLower() == "all")
//                    {
//                        _query = "SELECT A.USER_EMAIL,A.USER_NM,B.USER_TYPE,A.LOGIN_ID FROM  " + AdminDB + "..PROCS_USERS A INNER JOIN PROCS_INSIDER_USER B  ON A.LOGIN_ID=B.USER_LOGIN  WHERE B.STATUS='Active' AND B.COMPANY_ID=" + CompanyId;

//                    }
//                    else
//                    {
//                        String LoginId = TextBoxLoginId.Text.Replace(",", "','");

//                        _query = "SELECT A.USER_EMAIL,A.USER_NM,B.USER_TYPE,A.LOGIN_ID FROM  " + AdminDB + "..PROCS_USERS A INNER JOIN PROCS_INSIDER_USER B  ON A.LOGIN_ID=B.USER_LOGIN  WHERE B.STATUS='Active' AND A.LOGIN_ID IN ('" + LoginId + "') AND B.COMPANY_ID=" + CompanyId;

//                    }

//                    using (SqlConnection con = new SqlConnection(ConnectionString))
//                    using (SqlCommand cmd = new SqlCommand(
//                        _query, con
//                        ))
//                    {
//                        using (SqlDataAdapter sda = new SqlDataAdapter())
//                        {
//                            cmd.CommandType = System.Data.CommandType.Text;
//                            try
//                            {
//                                con.Open();
//                                con.ChangeDatabase(database);
//                                sda.SelectCommand = cmd;
//                                sda.Fill(dt);


//                                foreach (DataRow row in dt.Rows)
//                                {

//                                    List<string> attachments = new List<string>();
//                                    if (FileUpload1.HasFiles)
//                                    {
//                                        string strFolder;
//                                        string strFilePath;
//                                        strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");

//                                        if (!Directory.Exists(strFolder))
//                                        {
//                                            Directory.CreateDirectory(strFolder);
//                                        }

//                                        foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
//                                        {
//                                            strFilePath = strFolder + uploadedFile.FileName;
//                                            uploadedFile.SaveAs(strFilePath);

//                                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath(strFilePath)));

//                                        }
//                                    }
//                                    String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
//                                    EmailSender.SendMail(
//                                        row["USER_EMAIL"].ToString(), TextBoxSubject.Text, TextareaAppTemplate.Value.Replace("[UserId]", row["LOGIN_ID"].ToString()).Replace("[USER_NM]", row["USER_NM"].ToString()), attachments, "Custom Email", Convert.ToString(Session["CompanyId"]),
//                                        strCC, null, Convert.ToString(Session["EmployeeId"])
//                                    );
//                                }
//                            }
//                            catch (SqlException ex)
//                            {
//                                LabelMsg.Text = ex.ToString();

//                            }
//                            con.Close();
//                        }
//                    }

//                }
//                else
//                {
//                    LabelMsg.Text = "Please enter user to send email.";
//                }

//            }
//        }
//        protected void GetSmtpDetails()
//        {
//            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
//            {
//                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
//                var database = Convert.ToString(Session["ModuleDatabase"]);
//                var AdminDB = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

//                DataTable dtsmtp = new DataTable();

//                using (SqlConnection con = new SqlConnection(ConnectionString))
//                using (SqlCommand cmd = new SqlCommand("SP_PROCS_INSIDER_CONFIG_SMTP", con))
//                {
//                    using (SqlDataAdapter sda = new SqlDataAdapter())
//                    {
//                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
//                        cmd.Parameters.Add("@MODE", SqlDbType.NVarChar).Value = "GET_Smtp_Config_List";
//                        cmd.Parameters.Add("@SET_COUNT", SqlDbType.Int);
//                        cmd.Parameters["@SET_COUNT"].Direction = ParameterDirection.Output;
//                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = CompanyId;
//                        try
//                        {
//                            con.Open();
//                            con.ChangeDatabase(database);
//                            sda.SelectCommand = cmd;
//                            sda.Fill(dtsmtp);
//                            if (dtsmtp.Rows.Count > 0)
//                            {
//                                defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(dtsmtp.Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
//                                disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(dtsmtp.Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
//                                smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(dtsmtp.Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
//                                port = Convert.ToInt32(dtsmtp.Rows[0]["PORT"]);
//                                ssl = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["SSL"]))) ? (Convert.ToString(dtsmtp.Rows[0]["SSL"]) == "Yes" ? true : false) : false;
//                                smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(dtsmtp.Rows[0]["SMTP_USER_NAME"]) : String.Empty;
//                                password = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(dtsmtp.Rows[0]["PASSWORD"]), true) : String.Empty;
//                                userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(dtsmtp.Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(dtsmtp.Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

//                            }
//                        }
//                        catch (SqlException ex)
//                        {
//                            LabelMsg.Text = ex.ToString();

//                        }
//                        con.Close();
//                    }
//                }
//            }
//        }
//        protected void SendMail(string defaultEmail, string disclosureEmail, string To, string Subject, string Message, string smtpHostName, bool ssl, string smtpUserName, string password, bool userDefaultCredential, Int32 port)
//        {

//            using (MailMessage objMailMsg = new MailMessage(defaultEmail, To))
//            {
//                if (!String.IsNullOrEmpty(TextBoxBccEmail.Text) || !String.IsNullOrWhiteSpace(TextBoxBccEmail.Text))
//                {
//                    objMailMsg.Bcc.Add(new MailAddress(TextBoxBccEmail.Text));
//                }

//                objMailMsg.From = new MailAddress(defaultEmail, disclosureEmail);
//                objMailMsg.Subject = Subject;
//                objMailMsg.Body = Message;
//                objMailMsg.IsBodyHtml = true;

//                if (FileUpload1.HasFiles)
//                {
//                    string strFolder;
//                    string strFilePath;
//                    strFolder = Server.MapPath("~/InsiderTrading/emailAttachment/SendEmail/");

//                    if (!Directory.Exists(strFolder))
//                    {
//                        Directory.CreateDirectory(strFolder);
//                    }

//                    foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
//                    {
//                        strFilePath = strFolder + uploadedFile.FileName;
//                        uploadedFile.SaveAs(strFilePath);

//                        Attachment attachComplianceTrainingModule = new Attachment(strFilePath);
//                        objMailMsg.Attachments.Add(attachComplianceTrainingModule);

//                    }
//                }

//                SmtpClient smtp = new SmtpClient();
//                smtp.Host = smtpHostName;
//                smtp.EnableSsl = ssl;
//                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
//                NetworkCredential NetworkCred = new System.Net.NetworkCredential(smtpUserName, password);
//                smtp.UseDefaultCredentials = userDefaultCredential;
//                smtp.Credentials = NetworkCred;
//                smtp.Port = port;
//                try
//                {
//                    smtp.Send(objMailMsg);
//                    LabelMsg.Text = "Success!";
//                }
//                catch (Exception ex)
//                {
//                    LabelMsg.Text = ex.ToString();
//                }
//            }
//        }
//        protected void LinkButtonSendTestMail_Click(object sender, EventArgs e)
//        {

//            GetSmtpDetails();
//            SendMail(
//                defaultEmail, disclosureEmail, txtTestEmail.Value, TextBoxSubject.Text,
//                TextareaAppTemplate.Value.Replace("[UserId]", txtTestEmail.Value),
//                smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port
//                   );


//        }
//        protected void UploadUserManual()
//        {
//            try
//            {

//                if (FileUpload1.HasFiles)
//                {
//                    string strFileName = "User-Manual";

//                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();

//                    if (Extension == ".pdf")
//                    {
//                        string strFilePath;
//                        string strFolder;
//                        strFolder = Server.MapPath("UserManual/");
//                        strFilePath = strFolder + strFileName + Extension;

//                        // Create the directory if it does not exist.
//                        if (!Directory.Exists(strFolder))
//                        {
//                            Directory.CreateDirectory(strFolder);
//                        }

//                        FileUpload1.PostedFile.SaveAs(strFilePath);
//                    }
//                    else
//                    {
//                        LabelMsg.Text = "Invalid File";
//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                LabelMsg.Text = "Error While Uploading User Manual----" + ex.ToString();
//            }
//        }
//        public void BindRelative()
//        {
//            string sCmpnId = Convert.ToString(Session["CompanyId"]);
//            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
//            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
//            {
//                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
//                using (SqlConnection conn = new SqlConnection(ConnStr))
//                {
//                    conn.Open();
//                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
//                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT RELATIVE_NAME,RELATIVE_EMAIL FROM PROCS_INSIDER_RELATIVES_DETAIL WHERE ISNULL(RELATIVE_EMAIL,'')<>'' AND RELATIVE_EMAIL<>'Not Applicable' AND STATUS = 'ACTIVE'", conn);
//                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
//                    DataTable dt = new DataTable();
//                    sda.Fill(dt);
//                    Datalist1.DataSource = dt;
//                    Datalist1.DataBind();
//                }
//            }

//        }
//        public void BindCP()
//        {
//            string sCmpnId = Convert.ToString(Session["CompanyId"]);
//            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
//            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
//            {
//                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
//                using (SqlConnection conn = new SqlConnection(ConnStr))
//                {
//                    conn.Open();
//                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
//                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT CP_NAME,CP_EMAIL FROM PROCS_INSIDER_CONNECTED_PERSONS(NOLOCK) WHERE ISNULL(CP_EMAIL,'')<>'' AND CP_STATUS = 'ACTIVE'", conn);
//                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
//                    DataTable dt = new DataTable();
//                    sda.Fill(dt);
//                    ddlConnected.DataSource = dt;
//                    ddlConnected.DataBind();
//                }
//            }

//        }
//        public void BindRoleData()
//        {

//            SqlParameter[] parameters = new SqlParameter[2];
//            parameters[0] = new SqlParameter("@MODE", "GET_USER_ROLE_DETAILS");
//            parameters[1] = new SqlParameter("@AdminDb", Convert.ToString(Session["AdminDb"]));


//            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_ROLE_DETAILS", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
//            if (ds.Tables.Count > 0)
//            {
//                ddlRole.DataSource = ds.Tables[0];
//                ddlRole.DataBind();

//                DataTable dt = ds.Tables[1];
//                DataTable dat = ds.Tables[2];

//                for (int i = 0; i < ddlRole.Items.Count; i++)
//                {
//                    DataTable dtRoleUsr = new DataTable();
//                    dtRoleUsr.Columns.Add("USER_NM", typeof(string));
//                    dtRoleUsr.Columns.Add("USER_EMAIL", typeof(string));
//                    dtRoleUsr.Columns.Add("ROLE_NAME", typeof(string));

//                    Label Label1 = (Label)ddlRole.Items[i].FindControl("Itmnamelbl1");
//                    string labeltext = Label1.Text;

//                    if (labeltext != "Connected Person")
//                    {
//                        for (int c = 0; c < dt.Rows.Count; c++)
//                        {
//                            DataRow dr = dt.Rows[c];
//                            string s = Convert.ToString(dr["ROLE_NAME"]);
//                            if (labeltext == s)
//                            {
//                                dtRoleUsr.ImportRow(dr);
//                            }
//                        }
//                        DataList dl1 = (DataList)ddlRole.Items[i].FindControl("ddlCP");
//                        dl1.DataSource = dtRoleUsr;
//                        dl1.DataBind();
//                    }
//                    else
//                    {
//                        for (int d = 0; d < dat.Rows.Count; d++)
//                        {
//                            DataRow dr1 = dat.Rows[d];
//                            string s = Convert.ToString(dr1["CP_NAME"]);
//                            if (labeltext == s)
//                                dat.ImportRow(dr1);
//                            DataList dl1 = (DataList)ddlRole.Items[i].FindControl("ddlConnected");

//                            dl1.DataSource = dat;
//                            dl1.DataBind();
//                        }

//                    }
//                }
//            }

//        }

//    }
//}