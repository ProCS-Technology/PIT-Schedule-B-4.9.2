using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Login.Modal;
using ProcsDLL.Models.Login.Service.Request;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saml;
using System.Web.Services;
using System.Runtime.InteropServices;

namespace ProcsDLL
{
    public partial class Login : System.Web.UI.Page
    {
        public static string chkUserType { get; set; }
        Random random = new Random();

        string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["salt"] = "PROCS";
                Session["moreSalt"] = random.Next(59999, 199999).ToString();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                GenerateSamlURL();
                if (Convert.ToString(ConfigurationManager.AppSettings["SSOType"]) == "Cloud" &&
                    Convert.ToString(ConfigurationManager.AppSettings["AuthenticationType"])!= "Hybrid")
                {
                    Response.Redirect(GenerateSamlURL());
                }

            }
            UserName.Focus();
            string captcha = Convert.ToString(ConfigurationManager.AppSettings["EnableCaptcha"]);
            enableCaptcha.Value = captcha;
            enableADLogin.Value = Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["EnableADLogin"], true)); 
            hdnChkAuthType.Value = Convert.ToString(ConfigurationManager.AppSettings["AuthenticationType"]);
            hdnChkSSOType.Value = Convert.ToString(ConfigurationManager.AppSettings["SSOType"]);
            //if (Convert.ToString(ConfigurationManager.AppSettings["SSOType"]) == "Cloud")
            //{
            //    GenerateSamlURL();
            //}
            //Response.Redirect("https://www.example.com/destinationpage.aspx");
        }
        private string GenerateSamlURL()
        {
            var samlEndpoint = ConfigurationManager.AppSettings["SAMLUrl"];
            var entityId = ConfigurationManager.AppSettings["EntityId"];
            var redirectUri = ConfigurationManager.AppSettings["RedirectUri"];
            var request = new AuthRequest(entityId, redirectUri);
            string sUrl = request.GetRedirectUrl(samlEndpoint);
            HiddenADFSUrl.Value = sUrl;
            return sUrl;
        }
        //protected void ResetPassword(object sender, EventArgs e)
        //{
        //    ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
        //    login.Email = TextBoxLoginId.Text;
        //    LoginRequest objLogin = new LoginRequest(login);
        //    LoginResponse objResponse = objLogin.GetUserInfo();
        //    if (objResponse.StatusFl)
        //    {
        //        string from = "procstest@gmail.com";
        //        string disclosureEmail = String.Empty;
        //        string to = objResponse.User.Email;
        //        string smtpHostName = String.Empty;
        //        bool ssl = true;
        //        string smtpUserName = "procstest@gmail.com";
        //        Int32 port = 587;
        //        string password = "P@ssw0rd@123";
        //        bool userDefaultCredential = false;

        //        SqlParameter[] parameters1 = new SqlParameter[3];
        //        parameters1[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
        //        parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //        parameters1[1].Direction = ParameterDirection.Output;
        //        parameters1[2] = new SqlParameter("@COMPANY_ID", objResponse.User.CompanyId);

        //        DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ITDB"], true)), parameters1);
        //        if (ds1.Tables[0].Rows.Count > 0)
        //        {
        //            from = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
        //            disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
        //            smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
        //            port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
        //            ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
        //            smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
        //            password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
        //            userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;
        //        }

        //        string userName = objResponse.User.UserName;

        //        string moreSalts = Convert.ToString(HttpContext.Current.Session["moreSalt"]);

        //        var newPasswordMade = "H@xxL0rd" + moreSalts;
        //        var newPassword = hashcodegenerate.GetSHA512(newPasswordMade);
        //        string loginId = objResponse.User.LoginId;
        //        string sCmnpnId = objResponse.User.CompanyId;

        //        login.LoginId = loginId;
        //        login.Password = newPassword;
        //        objLogin = new LoginRequest(login);
        //        objResponse = objLogin.ChangePassword();

        //        if (objResponse.StatusFl)
        //        {
        //            String msg = " <br/>";
        //            msg += "Dear " + userName + ", <br/><br/>";
        //            msg += "Your login id is <b>" + loginId + "</b><br/><br/>";
        //            msg += "Your login password is <b>" + newPasswordMade + "</b><br/><br/>";
        //            msg += "This is a system generated mail. Please do not reply on this mail.<br/><br/>";
        //            msg += "Thanks,<br/>ProCS Technology";
        //            EmailSender.SendMail(
        //                to, "Password Recovered", msg, null, "Password Recovery", sCmnpnId, "", loginId, loginId
        //            );
        //        }
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){UserInfo('If your Email Id or Username saved in our database then you will receive Password Link');},1000);", true);
        //        TextBoxLoginId.Text = String.Empty;
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){UserInfo('If your Email Id or Username saved in our database then you will receive Password Link');},1000);", true);
        //        TextBoxLoginId.Text = String.Empty;
        //    }
        //}

        protected void ResetPassword(object sender, EventArgs e)
        {
            ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
            login.Email = TextBoxLoginId.Text;
            LoginRequest objLogin = new LoginRequest(login);
            LoginResponse objResponse = objLogin.GetUserInfo();
            //string emailTo = "prateek.raj@aksitservices.co.in";
            string emailTo = TextBoxLoginId.Text;
            string query = "SELECT ISNULL(ABS(DATEDIFF(MINUTE, GETDATE(),(SELECT MAX(EMAIL_DT) FROM PROCS_INSIDER_EMAIL_LOG WHERE EMAIL_ACTION = 'Password Recovery' AND EMAIL_TO = @EmailTo AND EMAIL_STATUS = 'Success'))), 0)";
            using (SqlConnection connection = new SqlConnection(sConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmailTo", emailTo);
                    int result = (int)command.ExecuteScalar();
                    if (result > 30)
                    {
                        if (objResponse.StatusFl)
                        {
                            string from = "procstest@gmail.com";
                            string disclosureEmail = String.Empty;
                            string to = objResponse.User.Email;
                            string smtpHostName = String.Empty;
                            bool ssl = true;
                            string smtpUserName = "procstest@gmail.com";
                            Int32 port = 587;
                            string password = "P@ssw0rd@123";
                            bool userDefaultCredential = false;
                            SqlParameter[] parameters1 = new SqlParameter[3];
                            parameters1[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                            parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters1[1].Direction = ParameterDirection.Output;
                            parameters1[2] = new SqlParameter("@COMPANY_ID", objResponse.User.CompanyId);
                            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ITDB"], true)), parameters1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                from = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                                disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                                smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                                port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                                ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                                smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                                password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                                userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;
                            }
                            string userName = objResponse.User.UserName;
                            //Added by jiten
                            //string moreSalts = Convert.ToString(HttpContext.Current.Session["moreSalt"]);
                            string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";  // Define the characters that are allowed in the random string
                            int stringLength = 15; // Set the length of the random string you want to generate, You can change this to the desired length
                            Random randomx = new Random();
                            string newPasswordMade = new string(Enumerable.Repeat(allowedChars, stringLength).Select(s => s[randomx.Next(s.Length)]).ToArray());  // Generate the random string
                            //var newPasswordMade = "H@xxL0rd" + moreSalts;
                            var newPassword = hashcodegenerate.GetSHA512(newPasswordMade);
                            string loginId = objResponse.User.LoginId;
                            string sCmnpnId = objResponse.User.CompanyId;
                            login.LoginId = loginId;
                            login.Password = newPassword;
                            objLogin = new LoginRequest(login);
                            objResponse = objLogin.ChangePassword();
                            if (objResponse.StatusFl)
                            {
                                String msg = " <br/>";
                                msg += "Dear " + userName + ", <br/><br/>";
                                msg += "Your login id is <b>" + loginId + "</b><br/><br/>";
                                msg += "Your login password is <b>" + newPasswordMade + "</b><br/><br/>";
                                msg += "This is a system generated mail. Please do not reply on this mail.<br/><br/>";
                                msg += "Thanks,<br/>ProCS Technology";
                                EmailSender.SendMail(
                                    to, "Password Recovered", msg, null, "Password Recovery", sCmnpnId, "", loginId, loginId
                                );
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){UserInfo('If your Email Id or Username saved in our database then you will receive Password Link');},1000);", true);
                            TextBoxLoginId.Text = String.Empty;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){UserInfo('If your Email Id or Username saved in our database then you will receive Password Link');},1000);", true);
                            TextBoxLoginId.Text = String.Empty;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){UserInfo('You cannot reset the password before 30 minutes, Please wait for 30 minutes');},1000);", true);
                        TextBoxLoginId.Text = String.Empty;
                    }
                }
            }
        }
        protected void LogIn(object sender, EventArgs e)
        {
            try
            {
                HttpBrowserCapabilities bc = Request.Browser;//nc
                //string enableADLogin = Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["EnableADLogin"], true));
                //if (Convert.ToBoolean(enableADLogin) == true)
                string chkAuthenticationType = Convert.ToString(ConfigurationManager.AppSettings["AuthenticationType"]);
                if(chkAuthenticationType== "AD/SSO" || chkAuthenticationType == "Hybrid")
                {
                    //if (authenticationType.Value == "Yes")
                    //if (authenticationType.Value == "Yes")
                    string SSOType = Convert.ToString(ConfigurationManager.AppSettings["SSOType"]);
                    if(SSOType== "LocalAD" && chkUserType != "Application")
                    {
                        Session["authenticationType"] = "AD";
                        bool IsUserExist = false;
                        var keys = ConfigurationManager.AppSettings.Keys;

                        List<string> lstLdaps = ConfigurationManager.AppSettings.AllKeys
                            .Where(key => key.ToString().ToLower().StartsWith("ldppath"))
                            .Select(key => ConfigurationManager.AppSettings[key]).ToList();

                        foreach (string ldap in lstLdaps)
                        {
                            DirectoryEntry de = new DirectoryEntry(ldap, UserName.Text, Password.Text);
                            de.RefreshCache();

                            DirectorySearcher dirSearch = null;
                            dirSearch = new DirectorySearcher(de);
                            dirSearch.Filter = "(&(objectClass=user)(objectCategory=person)(|(samaccountname=" + UserName.Text + ")(cn=" + UserName.Text + ")))";

                            var searchResult = dirSearch.FindAll();
                            if (searchResult != null)
                            {
                                IsUserExist = true;
                                ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
                                login.LoginId = UserName.Text;
                                LoginRequest objlogin = new LoginRequest(login);
                                LoginResponse objResponse = objlogin.ValidateADUser();

                                if (objResponse.StatusFl)
                                {
                                    if (objResponse.Msg == "Success")
                                    {
                                        Session.Clear();
                                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                        Session["EmployeeId"] = Convert.ToString(login.LoginId);
                                        Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                                        //====================nc for session====================
                                        if (HttpContext.Current.Request.UserAgent.Contains("Edg"))
                                        {
                                            Session["Browser"] = "Microsoft Edge";
                                        }
                                        else
                                        {
                                            Session["Browser"] = bc.Browser;//nc
                                        }

                                        Session["MacId"] = GetClientMAC(GetIPAddress());//nc
                                        Session["IP"] = GetIPAddress();//nc
                                        Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();

                                        SessionDTO sDTO = new SessionDTO();
                                        sDTO.EMP_ID = Convert.ToString(login.LoginId);
                                        sDTO.MAC_ID = GetClientMAC(GetIPAddress());
                                        sDTO.IP = GetIPAddress().ToString();
                                        if (HttpContext.Current.Request.UserAgent.Contains("Edg"))
                                        {
                                            sDTO.BROWSER = "Microsoft Edge";
                                        }
                                        else
                                        {
                                            sDTO.BROWSER = bc.Browser;
                                        }


                                        SessionRequest sReq = new SessionRequest(sDTO);
                                        SessionResponse sRes = sReq.SaveSession();
                                        //=========================================
                                        if (sRes.StatusFl == true)
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            HashSet<Int32> companyIds = new HashSet<Int32>();
                                            foreach (UserAccess usr in objResponse.User.UAccess)
                                            {
                                                companyIds.Add(usr.CompanyId);
                                            }
                                            foreach (Int32 companyId in companyIds)
                                            {
                                                var matchedObj = objResponse.User.UAccess.Where(p => p.CompanyId == companyId).ToList();
                                                if (companyIds.Count == 1 && matchedObj.Count == 1)
                                                {
                                                    Session["AuthToken"] = Guid.NewGuid().ToString();
                                                    Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();
                                                    Session["CompanyId"] = matchedObj[0].CompanyId;
                                                    Session["CompanyName"] = matchedObj[0].CompanyNm;
                                                    Session["ModuleId"] = matchedObj[0].ModuleId;
                                                    Session["ModuleName"] = matchedObj[0].ModuleNm;
                                                    Session["ModuleFolder"] = matchedObj[0].ModuleFolder;
                                                    Session["ModuleDatabase"] = matchedObj[0].ModuleDataBase;
                                                    string sModule = Convert.ToString(ConfigurationManager.AppSettings["Module"]);
                                                    if (sModule.ToUpper() == "UPSI")
                                                    {
                                                        Response.Redirect(Session["ModuleFolder"] + "/" + "DashboardUpsi.aspx", false);
                                                    }
                                                    else
                                                    {
                                                        Response.Redirect(Session["ModuleFolder"] + "/" + "PITDashboard.aspx", false);
                                                        //Response.Redirect(Session["ModuleFolder"] + "/" + "Dashboard.aspx");
                                                    }
                                                    //Response.Redirect(Session["ModuleFolder"] + "/" + "Dashboard.aspx");
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){openModal();},1000);", true);
                                                    sb.Append("<div class='row'>");
                                                    int count = 0;
                                                    foreach (UserAccess usr in objResponse.User.UAccess)
                                                    {
                                                        if (usr.CompanyId == companyId)
                                                        {
                                                            if (count == 0)
                                                            {
                                                                sb.Append("<img style='height:126px;padding-left:20px;padding-right:30px;' src='assets/logos/Company/" + usr.CompanyLogo + "' alt='" + usr.CompanyNm + "'/>");
                                                            }
                                                            sb.Append("<a runat='server' href=\"javascript:GoToDashBoard('" + companyId + "','" + usr.CompanyNm + "'," + usr.ModuleId + ",'" + usr.ModuleNm + "','" + usr.ModuleFolder + "','" + usr.ModuleDataBase + "', '" + Convert.ToString(login.LoginId) + "')\"><img style='height:126px;padding-right:10px;' src='assets/logos/Module/" + usr.ModuleLogo + "' alt='" + usr.ModuleNm + "' /></a>");
                                                            count++;
                                                        }
                                                    }
                                                    sb.Append("</div>");
                                                    sb.Append("</br>");
                                                }
                                            }
                                            ShowListing.InnerHtml = sb.ToString(); 
                                        }
                                        else if (sRes.StatusFl == false && sRes.Msg == "Sorry You have already logged in with another Browser or Another System")
                                        {
                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script type = 'text/javascript'>window.onload=function(){alert('Sorry You have already logged in with another Browser or Another System')};</script>");
                                            UserName.Text = "";
                                            Password.Text = "";
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential();},1000);", true);
                                    break;
                                }
                            }
                        }
                        if (IsUserExist == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential();},1000);", true);
                        }
                    }
                    else
                    {
                        Session["authenticationType"] = "System";
                        Login1();
                    }
                }
                else
                {
                    Session["authenticationType"] = "System";
                    Login1();
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "Login", "Login", Convert.ToString("superadmin"), Convert.ToInt32(1), 1);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential();},1000);", true);
            }
        }
        public void Login1()
        {
            try
            {
                HttpBrowserCapabilities bc = Request.Browser;//nc
                ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    string windowsLogin = HttpContext.Current.User.Identity.Name;
                    string lastPart = windowsLogin.Split('\\').Last();
                    login.LoginId = lastPart;
                    string salt = Convert.ToString(HttpContext.Current.Session["salt"]);
                    string moreSalts = Convert.ToString(HttpContext.Current.Session["moreSalt"]);
                    var password = "P@ssw0rd";
                    var hash = hashcodegenerate.GetSHA512(hashcodegenerate.GetSHA512(hashcodegenerate.GetSHA512(password) + salt) + salt);
                    var newPassword = hashcodegenerate.GetSHA512(hash + moreSalts);
                    login.Password = newPassword;
                }
                else
                {
                    login.LoginId = UserName.Text;
                    login.Password = Password.Text;
                }

                LoginRequest objlogin = new LoginRequest(login);
                LoginResponse objResponse = objlogin.ValidateUser();
                if (objResponse.StatusFl)
                {
                    if (objResponse.Msg == "Success")
                    {
                        Session.Clear();
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        Session["EmployeeId"] = Convert.ToString(login.LoginId);
                        Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                        Session["AuthToken"] = Guid.NewGuid().ToString();
                        Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();
                        //===session=========
                        if (HttpContext.Current.Request.UserAgent.Contains("Edg"))
                        {
                            Session["Browser"] = "Microsoft Edge";
                        }
                        else
                        {
                            Session["Browser"] = bc.Browser;//nc
                        }

                        Session["MacId"] = GetClientMAC(GetIPAddress());//nc
                        Session["IP"] = GetIPAddress();//nc

                        SessionDTO sDTO = new SessionDTO();
                        sDTO.EMP_ID = Convert.ToString(login.LoginId);
                        sDTO.MAC_ID = GetClientMAC(GetIPAddress());
                        sDTO.IP = GetIPAddress().ToString();
                        if (HttpContext.Current.Request.UserAgent.Contains("Edg"))
                        {
                            sDTO.BROWSER = "Microsoft Edge";
                        }
                        else
                        {
                            sDTO.BROWSER = bc.Browser;
                        }


                        SessionRequest sReq = new SessionRequest(sDTO);
                        SessionResponse sRes = sReq.SaveSession();
                        //================
                        if (sRes.StatusFl == true)
                        {
                            bool isPasswordChanged = objlogin.IsPasswordChanged();
                            if (isPasswordChanged)
                            {
                                Response.Redirect("ChangePassword.aspx", false);
                            }

                            StringBuilder sb = new StringBuilder();
                            HashSet<Int32> companyIds = new HashSet<Int32>();
                            foreach (UserAccess usr in objResponse.User.UAccess)
                            {
                                companyIds.Add(usr.CompanyId);
                            }

                            foreach (Int32 companyId in companyIds)
                            {
                                var matchedObj = objResponse.User.UAccess.Where(p => p.CompanyId == companyId).ToList();
                                if (companyIds.Count == 1 && matchedObj.Count == 1)
                                {
                                    Session["CompanyId"] = matchedObj[0].CompanyId;
                                    Session["CompanyName"] = matchedObj[0].CompanyNm;
                                    Session["ModuleId"] = matchedObj[0].ModuleId;
                                    Session["ModuleName"] = matchedObj[0].ModuleNm;
                                    Session["ModuleFolder"] = matchedObj[0].ModuleFolder;
                                    Session["ModuleDatabase"] = matchedObj[0].ModuleDataBase;
                                    //Response.Redirect(Session["ModuleFolder"] + "/" + "Dashboard.aspx", false);
                                    string sModule = Convert.ToString(ConfigurationManager.AppSettings["Module"]);
                                    if (sModule.ToUpper() == "UPSI")
                                    {
                                        Response.Redirect(Session["ModuleFolder"] + "/" + "DashboardUpsi.aspx", false);
                                    }
                                    else
                                    {
                                        Response.Redirect(Session["ModuleFolder"] + "/" + "PITDashboard.aspx", false);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){openModal();},1000);", true);
                                    sb.Append("<div class='row'>");
                                    int count = 0;
                                    foreach (UserAccess usr in objResponse.User.UAccess)
                                    {
                                        if (usr.CompanyId == companyId)
                                        {
                                            if (count == 0)
                                            {
                                                sb.Append("<img style='height:126px;padding-left:20px;padding-right:30px;' src='assets/logos/Company/" + usr.CompanyLogo + "' alt='" + usr.CompanyNm + "'/>");
                                            }
                                            sb.Append("<a runat='server' href=\"javascript:GoToDashBoard('" + companyId + "','" + usr.CompanyNm + "'," + usr.ModuleId + ",'" + usr.ModuleNm + "','" + usr.ModuleFolder + "','" + usr.ModuleDataBase + "', '" + Convert.ToString(login.LoginId) + "')\"><img style='height:126px;padding-right:10px;' src='assets/logos/Module/" + usr.ModuleLogo + "' alt='" + usr.ModuleNm + "' /></a>");
                                            count++;
                                        }
                                    }
                                    sb.Append("</div>");
                                    sb.Append("</br>");
                                }
                            }
                            ShowListing.InnerHtml = sb.ToString(); 
                        }
                        else if (sRes.StatusFl == false && sRes.Msg == "Sorry You have already logged in with another Browser or Another System")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script type = 'text/javascript'>window.onload=function(){alert('Sorry You have already logged in with another Browser or Another System')};</script>");
                            UserName.Text = "";
                            Password.Text = "";
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential('" + objResponse.Msg + " Salt=" + Convert.ToString(Session["salt"]) + " MoreSalt=" + Convert.ToString(Session["moreSalt"]) + "');},1000);", true);
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "Login", "Login", Convert.ToString("superadmin"), Convert.ToInt32(1), 1);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential();},1000);", true);
            }
        }

        //protected void ChkAuthType_click(object sender, EventArgs e)
        //{

        //    //if (Convert.ToString(ConfigurationManager.AppSettings["SSOType"]) == "Cloud")
        //    //{
        //    //    GenerateSamlURL();
        //    //}
        //    string abc= "just test";
        //}

        [WebMethod]
        public static void ReceiveDataFromClient(string data)
        {
             
              chkUserType = data;
            //return "Server received: " + data;
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        private static string GetClientMAC(string strClientIP)
        {
            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("L?i " + err.Message);
            }
            return mac_dest;
        }

    }
}