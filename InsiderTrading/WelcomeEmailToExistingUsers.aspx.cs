using ProcsDLL.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ProcsDLL.InsiderTrading
{
    public partial class WelcomeEmailToExistingUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string getConnectionString = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString(), true);// @"Data Source=WIN-P99G4H9GTJL;Initial Catalog=MIS_JF;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";// (string)settingsReader.GetValue("connectionString", typeof(String));

            using (SqlConnection con = new SqlConnection(getConnectionString))
            {
                con.Open();
                con.ChangeDatabase(Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]));
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Mode", "GET_Smtp_Config_List");
                cmd.Parameters.AddWithValue("@SET_COUNT", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@COMPANY_ID", Convert.ToInt32(Session["CompanyId"]));
                cmd.CommandText = "SP_PROCS_INSIDER_CONFIG_SMTP";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                    string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                    string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                    Int32 port = Convert.ToInt32(ds.Tables[0].Rows[0]["PORT"]);
                    bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                    string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                    string password = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                    bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                    using (SqlConnection con1 = new SqlConnection(getConnectionString))
                    {
                        con1.Open();
                        con1.ChangeDatabase(Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]));
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = con;
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Clear();
                        cmd1.Parameters.AddWithValue("@COMPANY_ID", Convert.ToInt32(Session["CompanyId"]));
                        cmd1.Parameters.AddWithValue("@Mode", "GET_ALL_INSIDERS");
                        cmd1.Parameters.AddWithValue("@ADMIN_DB", Convert.ToString(HttpContext.Current.Session["AdminDB"]));
                        cmd1.Parameters.AddWithValue("@SET_COUNT", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd1.CommandText = "SP_PROCS_INSIDER_TRADING_WINDOW";
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        da1.Fill(ds1);

                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds1.Tables[0].Rows)
                                {
                                    string userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                                    string userEmail = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                                    string userId = !String.IsNullOrEmpty(Convert.ToString(dr["LOGIN_ID"])) ? Convert.ToString(dr["LOGIN_ID"]) : String.Empty;
                                    string userPassword = !String.IsNullOrEmpty(Convert.ToString(dr["USER_PWD"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["USER_PWD"]), true) : String.Empty;
                                    string subject = "Introduction of Insider Compliance Management Software";
                                    string body = String.Empty;

                                    SqlParameter[] parameters = new SqlParameter[6];
                                    parameters[1] = new SqlParameter("@Mode", "GET_LAYOUT_TEMPLATE_AND_SUBJECT_HEADER");
                                    parameters[2] = new SqlParameter("@CompanyId", Convert.ToInt32(Session["CompanyId"]));
                                    parameters[3] = new SqlParameter("@SetCount", SqlDbType.Int);
                                    parameters[3].Direction = ParameterDirection.Output;
                                    parameters[4] = new SqlParameter("@UserId", userId);
                                    parameters[5] = new SqlParameter("@UserPassword", userPassword);

                                    DataSet ds2 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_IT_WELCOME_EMAIL_TEMPLATE", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
                                    if (ds2.Tables.Count > 0)
                                    {
                                        if (ds2.Tables[0].Rows.Count > 0)
                                        {
                                            body += !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["LAYOUT_TEMPLATE"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["LAYOUT_TEMPLATE"]) : String.Empty;
                                            subject = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["SUBJECT"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["SUBJECT"]) : String.Empty;
                                        }
                                    }
                                    List<string> attachments = new List<string>();
                                    if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf"))))
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));
                                    }
                                    if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf"))))
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf")));
                                    }
                                    if (File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf"))))
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf")));
                                    }
                                    String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                                    EmailSender.SendMail(
                                        userEmail, subject, body, attachments, "Welcome Email", Convert.ToString(Session["CompanyId"]),
                                        strCC, userId, Convert.ToString(Session["EmployeeId"])
                                    );
                                    //EmailHelper.SendWelcomeEmailForInsiderUser(
                                    //    defaultEmail, disclosureEmail, userEmail, subject, body, smtpHostName, ssl, smtpUserName, 
                                    //    password, userDefaultCredential, port
                                    //);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}