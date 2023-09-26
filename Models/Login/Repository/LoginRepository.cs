using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Login.Modal;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace ProcsDLL.Models.Login.Repository
{
    public class LoginRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();
        private static String dataBaseName = SQLHelper.GetDBName();
        public LoginResponse ValidateUser(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            try
            {
                LoginResponse res = new LoginResponse();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("PROCS_LOGIN_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                        cmd.Parameters.AddWithValue("@Password", objLogin.Password);
                        cmd.Parameters.AddWithValue("@Mode", "VALIDATE");
                        DataTable dtCnt = new DataTable();
                        SqlDataAdapter daCnt = new SqlDataAdapter(cmd);
                        daCnt.Fill(dtCnt);

                        if (dtCnt.Rows.Count > 0)
                        {
                            string salt = Convert.ToString(HttpContext.Current.Session["salt"]);
                            string moreSalts = Convert.ToString(HttpContext.Current.Session["moreSalt"]);

                            var dbPassword = Convert.ToString(dtCnt.Rows[0]["USER_PWD"]);

                            var hash = hashcodegenerate.GetSHA512(hashcodegenerate.GetSHA512(dbPassword + salt) + salt);
                            var fff = hashcodegenerate.GetSHA512(hash + moreSalts);

                            if (fff == objLogin.Password)
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@LoginId", Convert.ToString(dtCnt.Rows[0]["LOGIN_ID"]));
                                cmd.Parameters.AddWithValue("@Password", Convert.ToString(dtCnt.Rows[0]["USER_PWD"]));
                                cmd.Parameters.AddWithValue("@Mode", "ACCESS");
                                DataTable dtAccess = new DataTable();
                                SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                                daAccess.Fill(dtAccess);

                                if (dtAccess.Rows.Count > 0)
                                {
                                    List<UserAccess> lstAccess = new List<UserAccess>();
                                    foreach (DataRow drAccess in dtAccess.Rows)
                                    {
                                        UserAccess obj = new UserAccess();
                                        obj.CompanyId = Convert.ToInt32(drAccess["COMPANY_ID"]);
                                        obj.CompanyLogo = Convert.ToString(drAccess["COMPANY_LOGO"]);
                                        obj.CompanyNm = Convert.ToString(drAccess["COMPANY_NM"]);
                                        obj.GroupId = Convert.ToInt32(drAccess["GROUP_ID"]);
                                        obj.GroupLogo = Convert.ToString(drAccess["GROUP_LOGO"]);
                                        obj.GroupNm = Convert.ToString(drAccess["GROUP_NM"]);
                                        obj.ModuleId = Convert.ToInt32(drAccess["MODULE_ID"]);
                                        obj.ModuleLogo = Convert.ToString(drAccess["MODULE_LOGO"]);
                                        obj.ModuleNm = Convert.ToString(drAccess["MODULE_NM"]);
                                        obj.ModuleFolder = Convert.ToString(drAccess["MODULE_FOLDER"]);
                                        obj.ModuleDataBase = Convert.ToString(drAccess["DATABASE_NAME"]);
                                        lstAccess.Add(obj);
                                    }
                                    objLogin.UAccess = lstAccess;
                                    res.Msg = "Success";
                                    res.StatusFl = true;
                                    res.User = objLogin;
                                }
                            }
                            else
                            {
                                res.Msg = "No Data Found";
                                res.StatusFl = false;
                            }
                        }
                        else
                        {
                            res.Msg = "No Data Found";
                            res.StatusFl = false;
                        }
                    }
                    conn.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, "Login Page", 5, 0);
                LoginResponse res = new LoginResponse();
                res.StatusFl = false;
                res.Msg = "Processing failed, because of system error !";
                return res;
            }
        }
        /*
public LoginResponse ValidateUser_api(ProcsDLL.Models.Login.Modal.Login objLogin)
{
    try
    {
        LoginResponse res = new LoginResponse();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("PROCS_LOGIN_USER", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Clear();
                string pass = CryptorEngine.Encrypt(objLogin.Password, true);
                cmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                cmd.Parameters.AddWithValue("@Password", pass);
                cmd.Parameters.AddWithValue("@Mode", "VALIDATE");

                DataTable dtCnt = new DataTable();
                SqlDataAdapter daCnt = new SqlDataAdapter(cmd);
                daCnt.Fill(dtCnt);

                if (dtCnt.Rows.Count > 0 && Convert.ToInt32(dtCnt.Rows[0][0]) > 0)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                    cmd.Parameters.AddWithValue("@Password", pass);
                    cmd.Parameters.AddWithValue("@Mode", "USERDETAIL");
                    DataTable dtUserDetail = new DataTable();
                    SqlDataAdapter daUserDetail = new SqlDataAdapter(cmd);
                    daUserDetail.Fill(dtUserDetail);
                    if (dtUserDetail.Rows.Count > 0)
                    {
                        foreach (DataRow drUserdetail in dtUserDetail.Rows)
                        {
                            objLogin.Email = Convert.ToString(drUserdetail["USER_EMAIL"]);
                            objLogin.UserName = Convert.ToString(drUserdetail["USER_NM"]);
                            objLogin.LoginId = Convert.ToString(drUserdetail["LOGIN_ID"]);
                        }
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                    cmd.Parameters.AddWithValue("@Password", objLogin.Password);
                    cmd.Parameters.AddWithValue("@Mode", "ACCESS");

                    DataTable dtAccess = new DataTable();
                    SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                    daAccess.Fill(dtAccess);
                    if (dtAccess.Rows.Count > 0)
                    {
                        List<UserAccess> lstAccess = new List<UserAccess>();
                        foreach (DataRow drAccess in dtAccess.Rows)
                        {
                            UserAccess obj = new UserAccess();
                            obj.CompanyId = Convert.ToInt32(drAccess["COMPANY_ID"]);
                            obj.CompanyLogo = Convert.ToString(drAccess["COMPANY_LOGO"]);
                            obj.CompanyNm = Convert.ToString(drAccess["COMPANY_NM"]);
                            obj.GroupId = Convert.ToInt32(drAccess["GROUP_ID"]);
                            obj.GroupLogo = Convert.ToString(drAccess["GROUP_LOGO"]);
                            obj.GroupNm = Convert.ToString(drAccess["GROUP_NM"]);
                            obj.ModuleId = Convert.ToInt32(drAccess["MODULE_ID"]);
                            obj.ModuleLogo = Convert.ToString(drAccess["MODULE_LOGO"]);
                            obj.ModuleNm = Convert.ToString(drAccess["MODULE_NM"]);
                            obj.ModuleFolder = Convert.ToString(drAccess["MODULE_FOLDER"]);
                            obj.ModuleDataBase = Convert.ToString(drAccess["DATABASE_NAME"]);
                            lstAccess.Add(obj);
                        }
                        objLogin.UAccess = lstAccess;
                        res.Msg = "Success";
                        res.StatusFl = true;
                        res.User = objLogin;
                    }
                    else
                    {
                        res.Msg = "No Data Found";
                        res.StatusFl = false;
                    }
                }
                else
                {
                    res.Msg = "No Data Found";
                    res.StatusFl = false;
                }
            }
            conn.Close();
        }
        return res;
    }
    catch (Exception ex)
    {
        LoginResponse res = new LoginResponse();
        res.StatusFl = false;
        res.Msg = "Processing failed, because of system error !";
        return res;
    }

}
*/
        public bool IsPasswordChanged(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            bool isPasswordChanged = false;
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@MODE", "IS_PASSWORD_CHANGED");
            parameter[1] = new SqlParameter("@LoginId", objLogin.LoginId);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "PROCS_LOGIN_USER", parameter);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["CNT"]) > 0)
                    {
                        isPasswordChanged = true;
                    }
                }
            }

            return isPasswordChanged;
        }
        public void UpdateChangePasswordFlag(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@MODE", "UPDATE_PASSWORD_CHANGED_FLAG");
            parameter[1] = new SqlParameter("@LoginId", objLogin.LoginId);

            SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "PROCS_LOGIN_USER", parameter);

        }
        public LoginResponse GetUserInfo(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            try
            {
                LoginResponse res = new LoginResponse();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("PROCS_LOGIN_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Email", objLogin.Email);
                        cmd.Parameters.AddWithValue("@Mode", "GET_USER_INFO_BY_EMAIL_ID");

                        DataTable dtCnt = new DataTable();
                        SqlDataAdapter daCnt = new SqlDataAdapter(cmd);
                        daCnt.Fill(dtCnt);
                        if (dtCnt.Rows.Count > 0)
                        {
                            ProcsDLL.Models.Login.Modal.Login user = new ProcsDLL.Models.Login.Modal.Login();
                            user.UserName = dtCnt.Rows[0]["USER_NM"].ToString();
                            user.Email = dtCnt.Rows[0]["USER_EMAIL"].ToString();
                            user.Password = dtCnt.Rows[0]["USER_PWD"].ToString();
                            user.LoginId = dtCnt.Rows[0]["LOGIN_ID"].ToString();
                            user.CompanyId = dtCnt.Rows[0]["COMPANY_ID"].ToString();
                            res.User = user;
                            res.StatusFl = true;
                            res.Msg = "Success";
                        }
                        else
                        {
                            res.Msg = "No Data Found";
                            res.StatusFl = false;
                        }
                    }
                    conn.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, "Reset Password Page", 0, 0);
                LoginResponse res = new LoginResponse();
                res.StatusFl = false;
                res.Msg = "Processing failed, because of system error !";

                return res;
            }

        }
        public LoginResponse GetUserEmailByUserId(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            try
            {
                LoginResponse res = new LoginResponse();
                using (SqlConnection sCon = new SqlConnection(connectionString))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.StoredProcedure;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                    sCmd.Parameters.AddWithValue("@Mode", "GET_USER_EMAIL_BY_USER_ID");
                    sCmd.CommandText = "PROCS_LOGIN_USER";

                    DataSet dsCnt = new DataSet();
                    SqlDataAdapter daCnt = new SqlDataAdapter(sCmd);
                    daCnt.Fill(dsCnt);
                    DataTable dtCnt = new DataTable();
                    dtCnt = dsCnt.Tables[0];
                    if (dtCnt.Rows.Count > 0)
                    {
                        ProcsDLL.Models.Login.Modal.Login user = new ProcsDLL.Models.Login.Modal.Login();
                        user.Email = dtCnt.Rows[0]["USER_EMAIL"].ToString();
                        res.User = user;
                        res.StatusFl = true;
                        res.Msg = "Success";
                    }
                    else
                    {
                        res.Msg = "No Data Found";
                        res.StatusFl = false;
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, "Reset Password Page", 0, 0);
                LoginResponse res = new LoginResponse();
                res.StatusFl = false;
                res.Msg = "Processing failed, because of system error !";

                return res;
            }
        }
        public LoginResponse ChangePassword(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            try
            {
                LoginResponse res = new LoginResponse();
                using (SqlConnection sCon = new SqlConnection(connectionString))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.StoredProcedure;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                    sCmd.Parameters.AddWithValue("@Password", objLogin.Password);
                    sCmd.Parameters.AddWithValue("@Mode", "CHANGE_PASSWORD_BY_LOGIN_ID");
                    sCmd.CommandText = "PROCS_LOGIN_USER";
                    sCon.Open();
                    sCmd.ExecuteNonQuery();
                    sCon.Close();
                    res.StatusFl = true;
                    res.Msg = "Success";
                }
                return res;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, "Reset Password Page", 0, 0);
                LoginResponse res = new LoginResponse();
                res.StatusFl = false;
                res.Msg = "Processing failed, because of system error !";

                return res;
            }
        }
        public LoginResponse ValidateADUser(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            try
            {
                LoginResponse res = new LoginResponse();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("PROCS_LOGIN_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                        cmd.Parameters.AddWithValue("@Password", objLogin.Password);
                        cmd.Parameters.AddWithValue("@Mode", "VALIDATE");
                        DataTable dtCnt = new DataTable();
                        SqlDataAdapter daCnt = new SqlDataAdapter(cmd);
                        daCnt.Fill(dtCnt);

                        if (dtCnt.Rows.Count > 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@LoginId", Convert.ToString(dtCnt.Rows[0]["LOGIN_ID"]));
                            cmd.Parameters.AddWithValue("@Password", Convert.ToString(dtCnt.Rows[0]["USER_PWD"]));
                            cmd.Parameters.AddWithValue("@Mode", "ACCESS");
                            DataTable dtAccess = new DataTable();
                            SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                            daAccess.Fill(dtAccess);

                            if (dtAccess.Rows.Count > 0)
                            {
                                List<UserAccess> lstAccess = new List<UserAccess>();
                                foreach (DataRow drAccess in dtAccess.Rows)
                                {
                                    UserAccess obj = new UserAccess();
                                    obj.CompanyId = Convert.ToInt32(drAccess["COMPANY_ID"]);
                                    obj.CompanyLogo = Convert.ToString(drAccess["COMPANY_LOGO"]);
                                    obj.CompanyNm = Convert.ToString(drAccess["COMPANY_NM"]);
                                    obj.GroupId = Convert.ToInt32(drAccess["GROUP_ID"]);
                                    obj.GroupLogo = Convert.ToString(drAccess["GROUP_LOGO"]);
                                    obj.GroupNm = Convert.ToString(drAccess["GROUP_NM"]);
                                    obj.ModuleId = Convert.ToInt32(drAccess["MODULE_ID"]);
                                    obj.ModuleLogo = Convert.ToString(drAccess["MODULE_LOGO"]);
                                    obj.ModuleNm = Convert.ToString(drAccess["MODULE_NM"]);
                                    obj.ModuleFolder = Convert.ToString(drAccess["MODULE_FOLDER"]);
                                    obj.ModuleDataBase = Convert.ToString(drAccess["DATABASE_NAME"]);
                                    lstAccess.Add(obj);
                                }
                                objLogin.UAccess = lstAccess;
                                res.Msg = "Success";
                                res.StatusFl = true;
                                res.User = objLogin;
                            }

                        }
                        else
                        {
                            res.Msg = "No Data Found";
                            res.StatusFl = false;
                        }
                    }
                    conn.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, "Login Page", 5, 0);
                LoginResponse res = new LoginResponse();
                res.StatusFl = false;
                res.Msg = "Processing failed, because of system error !";
                return res;
            }
        }
        public LoginResponse ValidateSwitchedUser(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            try
            {
                LoginResponse res = new LoginResponse();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("PROCS_LOGIN_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LoginId", objLogin.LoginId);
                        cmd.Parameters.AddWithValue("@Password", objLogin.Password);
                        cmd.Parameters.AddWithValue("@Mode", "VALIDATE");
                        DataTable dtCnt = new DataTable();
                        SqlDataAdapter daCnt = new SqlDataAdapter(cmd);
                        daCnt.Fill(dtCnt);

                        if (dtCnt.Rows.Count > 0)
                        {
                            string salt = Convert.ToString(HttpContext.Current.Session["salt"]);
                            string moreSalts = Convert.ToString(HttpContext.Current.Session["moreSalt"]);
                            var dbPassword = Convert.ToString(dtCnt.Rows[0]["USER_PWD"]);

                            var hash = hashcodegenerate.GetSHA512(hashcodegenerate.GetSHA512(dbPassword + salt) + salt);
                            var fff = hashcodegenerate.GetSHA512(hash + moreSalts);


                            objLogin.Email = Convert.ToString(dtCnt.Rows[0]["USER_EMAIL"]);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@LoginId", Convert.ToString(dtCnt.Rows[0]["LOGIN_ID"]));
                            cmd.Parameters.AddWithValue("@Password", Convert.ToString(dtCnt.Rows[0]["USER_PWD"]));
                            cmd.Parameters.AddWithValue("@COMPANY_ID", Convert.ToInt32(objLogin.CompanyId));
                            cmd.Parameters.AddWithValue("@Mode", "SWITCHEDUSERACCESS");
                            DataTable dtAccess = new DataTable();
                            SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                            daAccess.Fill(dtAccess);

                            if (dtAccess.Rows.Count > 0)
                            {
                                List<UserAccess> lstAccess = new List<UserAccess>();
                                foreach (DataRow drAccess in dtAccess.Rows)
                                {
                                    UserAccess obj = new UserAccess();
                                    obj.CompanyId = Convert.ToInt32(drAccess["COMPANY_ID"]);
                                    obj.CompanyLogo = Convert.ToString(drAccess["COMPANY_LOGO"]);
                                    obj.CompanyNm = Convert.ToString(drAccess["COMPANY_NM"]);
                                    obj.GroupId = Convert.ToInt32(drAccess["GROUP_ID"]);
                                    obj.GroupLogo = Convert.ToString(drAccess["GROUP_LOGO"]);
                                    obj.GroupNm = Convert.ToString(drAccess["GROUP_NM"]);
                                    obj.ModuleId = Convert.ToInt32(drAccess["MODULE_ID"]);
                                    obj.ModuleLogo = Convert.ToString(drAccess["MODULE_LOGO"]);
                                    obj.ModuleNm = Convert.ToString(drAccess["MODULE_NM"]);
                                    obj.ModuleFolder = Convert.ToString(drAccess["MODULE_FOLDER"]);
                                    obj.ModuleDataBase = Convert.ToString(drAccess["DATABASE_NAME"]);
                                    lstAccess.Add(obj);
                                }
                                objLogin.UAccess = lstAccess;
                                res.Msg = "Success";
                                res.StatusFl = true;
                                res.User = objLogin;
                            }

                        }
                        else
                        {
                            res.Msg = "No Data Found";
                            res.StatusFl = false;
                        }
                    }
                    conn.Close();
                }
                return res;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, "Login Page", 5, 0);
                LoginResponse res = new LoginResponse();
                res.StatusFl = false;
                res.Msg = "Processing failed, because of system error !";
                return res;
            }
        }
    }
}