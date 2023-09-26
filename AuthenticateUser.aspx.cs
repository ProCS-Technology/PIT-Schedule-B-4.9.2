using ProcsDLL.Models.Infrastructure;
using Saml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ProcsDLL
{
    public partial class AuthenticateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                string samlCer = ConfigurationManager.AppSettings["GCertKey"];
                string sResponse = string.Empty;
                sResponse = Request.Form["SAMLResponse"];
                if (string.IsNullOrEmpty(sResponse) || sResponse == null)
                {
                    Server.Transfer("GSSOLogin.aspx");
                }
                else
                {
                    Saml.Response samlResponse = new Response(samlCer, Request.Form["SAMLResponse"]);
                    var samlEndpoint = ConfigurationManager.AppSettings["GLogoutUrl"];
                    string username, email, firstname, lastname;
                    username = samlResponse.GetNameID();
                    email = samlResponse.GetEmail();
                    email = username;
                    firstname = samlResponse.GetFirstName();
                    lastname = samlResponse.GetLastName();

                    lblMsg.Text += "username=" + username + "<br />";
                    lblMsg.Text += "email=" + email + "<br />";
                    lblMsg.Text += "firstname=" + firstname + "<br />";
                    lblMsg.Text += "lastname=" + lastname + "<br />";

                    string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                    using (SqlConnection conn = new SqlConnection(ConnStr))
                    {
                        conn.Open();
                        string sql = "SELECT E.GROUP_ID,E.GROUP_NM,E.LOGO AS GROUP_LOGO,D.COMPANY_ID,D.COMPANY_NM,D.LOGO AS COMPANY_LOGO,C.MODULE_ID," +
                            "C.MODULE_NM,C.MODULE_FOLDER,CASE WHEN C.MODULE_FOLDER='InsiderTrading' THEN D.IT_DB_NAME " +
                            "WHEN C.MODULE_FOLDER='BoardMeeting' THEN D.BMS_DB_NAME END AS DATABASE_NAME,C.LOGO AS MODULE_LOGO FROM PROCS_USERS(NOLOCK) A " +
                            "INNER JOIN PROCS_USERS_BU_ACESS(NOLOCK) B ON A.LOGIN_ID=B.LOGIN_ID " +
                            "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID " +
                            "INNER JOIN PROCS_BUSINESS_COMPANY(NOLOCK) D ON B.COMPANY_ID=D.COMPANY_ID " +
                            "INNER JOIN PROCS_BUSINESS_GROUP(NOLOCK) E ON D.GROUP_ID=E.GROUP_ID " +
                            "WHERE A.LOGIN_ID='" + email + "'";

                        lblMsg.Text += "sql=" + sql + "<br />";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            DataTable dtAccess = new DataTable();
                            SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                            daAccess.Fill(dtAccess);

                            lblMsg.Text += "dtAccess.Rows.Count=" + dtAccess.Rows.Count.ToString() + "<br />";

                            if (dtAccess.Rows.Count == 1)
                            {
                                Session["CompanyId"] = Convert.ToInt32(dtAccess.Rows[0]["COMPANY_ID"]);
                                Session["CompanyName"] = Convert.ToString(dtAccess.Rows[0]["COMPANY_NM"]);
                                Session["ModuleId"] = Convert.ToInt32(dtAccess.Rows[0]["MODULE_ID"]);
                                Session["ModuleName"] = Convert.ToString(dtAccess.Rows[0]["MODULE_NM"]);
                                Session["ModuleFolder"] = Convert.ToString(dtAccess.Rows[0]["MODULE_FOLDER"]);
                                Session["ModuleDatabase"] = Convert.ToString(dtAccess.Rows[0]["DATABASE_NAME"]);
                                Session["AuthenticatedFrom"] = "Google AD";
                                Session["EmployeeId"] = email;
                                Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                                Session["AuthToken"] = Guid.NewGuid().ToString();
                                Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();
                                Response.Redirect("/" + Session["ModuleFolder"] + "/" + "Dashboard.aspx");
                            }
                            else
                            {
                                string sAdminDB = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                                string sConStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                                string sUser = "";
                                using (SqlConnection connX = new SqlConnection(sConStr))
                                {
                                    connX.Open();
                                    sql = "SELECT TOP 1 A.SALUTATION+' '+B.USER_NM+' ('+B.USER_EMAIL+')' AS USR FROM PROCS_INSIDER_USER(NOLOCK) A " +
                                        "INNER JOIN " + sAdminDB + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                                        "WHERE A.IS_APPROVER='Yes'";
                                    using (SqlCommand cmdX = new SqlCommand(sql, connX))
                                    {
                                        cmdX.CommandText = sql;
                                        cmdX.CommandType = CommandType.Text;
                                        sUser = Convert.ToString(cmdX.ExecuteScalar());
                                    }
                                }
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){InValidCredential('" + sUser + "','" + samlEndpoint + "');},500);", true);
                            }
                        }
                    }
                }
            }
            else
            {
                Server.Transfer("GSSOLogin.aspx");
            }

            //Saml.Response samlResponse = new Response(samlCer, Request.Form["SAMLResponse"]);
            //string username, email, firstname, lastname;
            //username = samlResponse.GetNameID();
            //email = samlResponse.GetEmail();
            //firstname = samlResponse.GetFirstName();
            //lastname = samlResponse.GetLastName();

            //string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
            //using (SqlConnection conn = new SqlConnection(ConnStr))
            //{
            //    conn.Open();
            //    string sql = "SELECT E.GROUP_ID,E.GROUP_NM,E.LOGO AS GROUP_LOGO,D.COMPANY_ID,D.COMPANY_NM,D.LOGO AS COMPANY_LOGO,C.MODULE_ID," +
            //        "C.MODULE_NM,C.MODULE_FOLDER,CASE WHEN C.MODULE_FOLDER='InsiderTrading' THEN D.IT_DB_NAME " +
            //        "WHEN C.MODULE_FOLDER='BoardMeeting' THEN D.BMS_DB_NAME END AS DATABASE_NAME,C.LOGO AS MODULE_LOGO FROM PROCS_USERS(NOLOCK) A " +
            //        "INNER JOIN PROCS_USERS_BU_ACESS(NOLOCK) B ON A.LOGIN_ID=B.LOGIN_ID " +
            //        "INNER JOIN PROCS_MODULE(NOLOCK) C ON B.MODULE_ID=C.MODULE_ID " +
            //        "INNER JOIN PROCS_BUSINESS_COMPANY(NOLOCK) D ON B.COMPANY_ID=D.COMPANY_ID " +
            //        "INNER JOIN PROCS_BUSINESS_GROUP(NOLOCK) E ON D.GROUP_ID=E.GROUP_ID " +
            //        "WHERE A.LOGIN_ID='" + email + "'";
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        DataTable dtAccess = new DataTable();
            //        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
            //        daAccess.Fill(dtAccess);

            //        if (dtAccess.Rows.Count == 1)
            //        {
            //            Session["CompanyId"] = Convert.ToInt32(dtAccess.Rows[0]["COMPANY_ID"]);
            //            Session["CompanyName"] = Convert.ToString(dtAccess.Rows[0]["COMPANY_NM"]);
            //            Session["ModuleId"] = Convert.ToInt32(dtAccess.Rows[0]["MODULE_ID"]);
            //            Session["ModuleName"] = Convert.ToString(dtAccess.Rows[0]["MODULE_NM"]);
            //            Session["ModuleFolder"] = Convert.ToString(dtAccess.Rows[0]["MODULE_FOLDER"]);
            //            Session["ModuleDatabase"] = Convert.ToString(dtAccess.Rows[0]["DATABASE_NAME"]);
            //            Session["AuthenticatedFrom"] = "Azure AD";
            //            Session["EmployeeId"] = email;
            //            Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            //            Response.Redirect("/" + Session["ModuleFolder"] + "/" + "Dashboard.aspx");
            //        }

            //    }
            //}
        }
    }
}