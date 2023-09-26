using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Model;
using ProcsDLL.Models.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace ProcsDLL.Models.Repository
{
    public class LoginRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();

        public LoginResponse Validate_UserLogin(LoginModel objLogin)
        {
            LoginResponse oLogin = new LoginResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@USER_NM", objLogin.USER_NM));
                        cmd.Parameters.Add(new SqlParameter("@MODE", "VALIDATE_USER"));
                        cmd.Parameters.Add(new SqlParameter("@USER_PWD", objLogin.USER_PWD));
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 1)
                        {
                            oLogin.StatusFl = true;
                        }
                        else
                        {
                            oLogin.StatusFl = false;
                        }
                        //oLogin.StatusFl = true;
                        //oLogin.Msg = "Login successfully !";
                        //oLogin.Login = objLogin;  
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oLogin = new LoginResponse();
                oLogin.StatusFl = false;
                oLogin.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oLogin;
        }

        public LoginResponse User_Details(LoginModel objLogin)
        {
            LoginResponse oLogin = new LoginResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@USER_NM", objLogin.USER_NM));
                        cmd.Parameters.Add(new SqlParameter("@MODE", "USER_DETAILS"));
                        cmd.Parameters.Add(new SqlParameter("@USER_PWD", objLogin.USER_PWD));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                oLogin.Login.ID = Convert.ToInt32(rdr["ID"]);
                                oLogin.Login.USER_NM = Convert.ToString(rdr["USER_NM"]);
                                oLogin.Login.USER_EMAIL = Convert.ToString(rdr["USER_EMAIL"]);
                            }
                            oLogin.StatusFl = true;
                            oLogin.Msg = "Login successfully !";
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oLogin = new LoginResponse();
                oLogin.StatusFl = false;
                oLogin.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oLogin;
        }
    }
}