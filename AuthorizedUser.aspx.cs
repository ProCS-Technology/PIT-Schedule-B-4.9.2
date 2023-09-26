using ProcsDLL.Models.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web.UI;
namespace ProcsDLL
{
    public partial class AuthorizedUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var tokenString = Request.QueryString["token"];
                //var tokenString = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbXBpZCI6IjAwMDMwOTM1IiwiZW1haWwiOiJlcnBkZXZAbmlpdC5jb20ifQ.g4gaZReRYtTP4jjFTvPbbyivjL8IAD0PLIKOVHaHdmI";
                var token = new JwtSecurityToken(tokenString);
                string employeeId = token.Claims.First(c => c.Type == "empid").Value;
                string email = token.Claims.First(c => c.Type == "email").Value;

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
                        "WHERE A.LOGIN_ID='" + employeeId + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dtAccess = new DataTable();
                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                        daAccess.Fill(dtAccess);

                        if (dtAccess.Rows.Count == 0)
                        {
                            UserLoginLogs(employeeId, email, tokenString, "Failed");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){InValidCredential();},500);", true);
                        }
                        else if (dtAccess.Rows.Count == 1)
                        {

                            UserLoginLogs(employeeId, email, tokenString, "Success");

                            Session["CompanyId"] = Convert.ToInt32(dtAccess.Rows[0]["COMPANY_ID"]);
                            Session["CompanyName"] = Convert.ToString(dtAccess.Rows[0]["COMPANY_NM"]);
                            Session["ModuleId"] = Convert.ToInt32(dtAccess.Rows[0]["MODULE_ID"]);
                            Session["ModuleName"] = Convert.ToString(dtAccess.Rows[0]["MODULE_NM"]);
                            Session["ModuleFolder"] = Convert.ToString(dtAccess.Rows[0]["MODULE_FOLDER"]);
                            Session["ModuleDatabase"] = Convert.ToString(dtAccess.Rows[0]["DATABASE_NAME"]);
                            Session["AuthenticatedFrom"] = "Azure AD";
                            Session["EmployeeId"] = employeeId;
                            Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                            Session["AuthToken"] = Guid.NewGuid().ToString();

                            Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();

                            //String sPth = Session["ModuleFolder"] + "/" + "Dashboard.aspx";
                            Response.Redirect(Session["ModuleFolder"] + "/" + "Dashboard.aspx");
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
                            UserLoginLogs(employeeId, email, tokenString, "Failed");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){InValidCredential();},500);", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void UserLoginLogs(string EmployeeId,string Email,string Token, string Status )
        {
            try
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);

                using (SqlConnection connX = new SqlConnection(ConnStr))
                {
                    connX.Open();
                    string sql = "INSERT INTO PROCS_INSIDER_USER_LOGIN_LOGS (EMPLOYEE_ID,EMAIL,TOKEN,STATUS,CREATED_ON) " +
                        "VALUES ('" + EmployeeId + "','" + Email + "','" + Token + "','" + Status + "',CURRENT_TIMESTAMP)";
                    using (SqlCommand cmdX = new SqlCommand(sql, connX))
                    {
                        cmdX.CommandText = sql;
                        cmdX.CommandType = CommandType.Text;
                        cmdX.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}