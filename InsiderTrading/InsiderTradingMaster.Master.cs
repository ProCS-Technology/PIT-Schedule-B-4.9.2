using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using ProcsDLL.Models.Login.Service.Request;
using ProcsDLL.Models.Login.Service.Response;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
namespace ProcsDLL.InsiderTrading
{
    public partial class InsiderTradingMaster : System.Web.UI.MasterPage
    {
        StringBuilder sb = new StringBuilder();
        protected string ltrMenu { get; set; }
        protected string ltrCompany { get; set; }
        string strGrpName = string.Empty;
        string strGrpIcon = string.Empty;
        Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("../LogOut.aspx");
                }
            }
            else
            {
                Response.Redirect("../LogOut.aspx");
            }

            string sAccess = CheckGetPageAccess();
            if (sAccess == "No")
            {
                Response.Redirect("PITDashboard.aspx", true);
            }

            string sVersion = Convert.ToString(ConfigurationManager.AppSettings["Version"]);
            spnVersion.InnerHtml = sVersion;
            string TokenKeyEncrypted = CryptorEngine.Encrypt(random.Next(59999, 199999).ToString(), true);
            Session["TokenKey"] = TokenKeyEncrypted;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            TokenKey.Value = TokenKeyEncrypted;
            Session["AuthToken"] = Guid.NewGuid().ToString();
            Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();

            string sDtFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalDateFormat"]);
            hdnDateFormat.Value = sDtFormat;
            //HnDateFormat.Value = sDtFormat;

            string sJSDtFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalJSDateFormat"]);
            hdnJSDateFormat.Value = sJSDtFormat;
            //HnJSDateFormat.Value = sJSDtFormat;

            GetUserDetails();
            String sConnectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    if (String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
                    {
                        Response.Redirect("../LogOut.aspx");
                    }
                    sCon.Open();
                    var database = Convert.ToString(Session["ModuleDatabase"]);
                    sCon.ChangeDatabase(database);
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "INSIDER_ADMIN_MENU";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@LOGIN_ID", Convert.ToString(Session["EmployeeId"]));
                    sCmd.Parameters.AddWithValue("@ADMIN_DB", Convert.ToString(Session["AdminDB"]));
                    //sCmd.Parameters.AddWithValue("@Username", Convert.ToString(Session["Username"]));
                    //sCmd.Parameters.AddWithValue("@UserRole", Convert.ToString(Session["RoleName"]));
                    //sCmd.Parameters.AddWithValue("@RoleId", Convert.ToInt32(Session["RoleId"]));
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[1];

                    sb.Append("<ul class='page-sidebar-menu'data-keep-expanded='false' data-auto-scroll='true' data-slide-speed='200'>");
                    sb.Append("<li class='heading My-InsiderTradingMaster-Side-Navbar-Top'><h3 class='uppercase My-InsiderTradingMaster-Side-Navbar-Top-Inner'>Insider Trading</h3></li>");
                    sb.Append("<li style='display:none;' id='liSwitchCompany' class='nav-item My-InsiderTradingMaster-Side-Navbar-Group'>");
                    sb.Append("<a href='#' data-toggle='modal' data-target='#ModalSwitchCompany' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon icon-home'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>Switch Company</span></a>");
                    sb.Append("</li>");
                    sb.Append("<li id='liDashboard' class='nav-item start active open My-InsiderTradingMaster-Side-Navbar-Group'>");
                    string sModule = Convert.ToString(ConfigurationManager.AppSettings["Module"]);
                    if (sModule.ToUpper() == "UPSI")
                    {
                        sb.Append("<a href='DashboardUpsi.aspx' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon icon-home'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>Dashboard</span></a></li>");
                    }
                    else
                    {
                        //sb.Append("<a href='Dashboard.aspx' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon icon-home'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>Dashboard</span></a></li>");
                        sb.Append("<a href='PITDashboard.aspx' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon icon-home'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>Dashboard</span></a></li>");
                    }

                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] dr = dt1.Select("GRP_ID=" + Convert.ToString(dt.Rows[i]["GRP_ID"]));

                        if (dt.Rows[i]["GRP_ID"].ToString() != "0")
                        {
                            strGrpIcon = Convert.ToString(dt.Rows[i]["GRP_ICON"]);
                            sb.Append("<li class='nav-item My-InsiderTradingMaster-Side-Navbar-Group'><a href='javascript:;' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon " + strGrpIcon + "'></i>");
                            strGrpName = Convert.ToString(dt.Rows[i]["GRP_NM"]);
                            sb.Append(" <span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>" + strGrpName + "</span>");
                            sb.Append("<span class='arrow'></span></a>");
                            getMenuItems(dr, strGrpName, dt1);
                        }
                    }
                    //sb.Append("<li id='liUserManualAndPolicyDocument' class='nav-item My-InsiderTradingMaster-Side-Navbar-Group'>");
                    //sb.Append("<a href='UserManualAndPolicyDocument.aspx' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon fa fa-file'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>User Manual</span></a></li>");
                    sb.Append("<li id='liPolicyDocument' class='nav-item My-InsiderTradingMaster-Side-Navbar-Group'>");
                    sb.Append("<a href='PolicyDocument.aspx' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon fa fa-file'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>Policy Document</span></a></li>");
                    //sb.Append("<li id='liSupport' class='nav-item My-InsiderTradingMaster-Side-Navbar-Group'>");
                    //sb.Append("<a href='Support.aspx' class='nav-link nav-toggle'><i class='My-InsiderTradingMaster-Side-Navbar-Group-Icon fa fa-bell'></i><span class='title My-InsiderTradingMaster-Side-Navbar-Group-Name'>Support</span></a></li>");
                    sb.Append("</ul>");
                    ltrMenu = sb.ToString();

                    liLbl.InnerHtml = Convert.ToString(Session["CompanyName"]) + " - Insider Trading Compliance Management System";

                    sb.Clear();
                    dt.Clear();
                    sCon.ChangeDatabase(Convert.ToString(Session["AdminDb"]));
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = "SELECT B.COMPANY_ID,B.COMPANY_NM FROM PROCS_USERS_BU_ACESS(NOLOCK) A INNER JOIN PROCS_BUSINESS_COMPANY(NOLOCK) B ON A.COMPANY_ID=B.COMPANY_ID WHERE A.LOGIN_ID='" + Convert.ToString(Session["EmployeeId"]) + "'";
                    da = new SqlDataAdapter(sCmd);
                    DataTable dtcomp = new DataTable();
                    da.Fill(dtcomp);
                    for (int i = 0; i < dtcomp.Rows.Count; i++)
                    {
                        sb.Append("<a href='#' onclick='javascript:fnSwitchCompany(\"" + Convert.ToString(dtcomp.Rows[i]["COMPANY_ID"]) + "\",\"" + Convert.ToString(dtcomp.Rows[i]["COMPANY_NM"]) + "\");' class='btn btn-lg btn-primary'>" + Convert.ToString(dtcomp.Rows[i]["COMPANY_NM"]) + "</a>");
                        sb.Append("<br />");
                    }
                    ltrCompany = sb.ToString();
                }
                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                }
            }
        }
        private string CheckGetPageAccess()
        {
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
            if (pageName.ToUpper() == "PITDASHBOARD")
            {
                return "Yes";
            }
            else
            {
                try
                {
                    string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                    string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                    Int32 iCmpnId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    Int32 iCnt = 0;
                    using (SqlConnection sCon = new SqlConnection(sConStr))
                    {
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.CommandText = "SELECT ISNULL(A.IS_APPROVER,'') AS IS_APPROVER,B.ROLE_NAME FROM PROCS_INSIDER_USER(NOLOCK) A " +
                            "INNER JOIN PROCS_INSIDER_ROLE_MSTR(NOLOCK) B ON A.USER_ROLE=B.ROLE_ID " +
                            "WHERE USER_LOGIN='" + sLoginId + "'";
                        sCmd.CommandType = CommandType.Text;
                        sCmd.Connection = sCon;

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["IS_APPROVER"]) == "Yes")
                            {
                                sCmd.CommandText = "SELECT COUNT(*) FROM PROCS_INSIDER_MENU_ACCESS(NOLOCK) " +
                                    "WHERE MENU_PAGE='" + pageName + "' AND CO_ACCESS='Y'";
                                sCmd.CommandType = CommandType.Text;
                                iCnt = Convert.ToInt32(sCmd.ExecuteScalar());
                            }
                            else if (Convert.ToString(dt.Rows[0]["ROLE_NAME"]) == "Admin")
                            {
                                sCmd.CommandText = "SELECT COUNT(*) FROM PROCS_INSIDER_MENU_ACCESS(NOLOCK) " +
                                    "WHERE MENU_PAGE='" + pageName + "' AND ADMIN_ACCESS='Y'";
                                sCmd.CommandType = CommandType.Text;
                                iCnt = Convert.ToInt32(sCmd.ExecuteScalar());
                            }
                            else
                            {
                                sCmd.CommandText = "SELECT COUNT(*) FROM PROCS_INSIDER_MENU_ACCESS(NOLOCK) " +
                                    "WHERE MENU_PAGE='" + pageName + "' AND DP_ACCESS='Y'";
                                sCmd.CommandType = CommandType.Text;
                                iCnt = Convert.ToInt32(sCmd.ExecuteScalar());
                            }
                        }
                    }
                    if (iCnt > 0)
                    {
                        return "Yes";
                    }
                    else
                    {
                        return "No";
                    }
                }
                catch (Exception ex)
                {

                }
                return "No";
            }
        }
        private void getMenuItems(DataRow[] dr1, string strGrpName, DataTable dt1)
        {
            ArrayList arrlist = new ArrayList();
            foreach (DataRow ddrow in dr1)
                arrlist.Add(ddrow["MENU_ID"]);
            sb.Append("<ul class='sub-menu'>");
            foreach (var modId in arrlist)
            {
                DataRow[] mRow = dt1.Select("MENU_ID=" + modId);
                foreach (DataRow modname in mRow)
                {
                    DataRow[] newRow = dt1.Select("MENU_ID=" + modId);
                    if (newRow.Length != 0)
                    {
                        sb.Append("<li id='li" + modname["MENU_URL"].ToString().Split('/')[modname["MENU_URL"].ToString().Split('/').Length - 1].Split('.')[0].ToString() + "' class='nav-item My-InsiderTradingMaster-Side-Navbar-Menu'>");
                        //  HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" +
                        sb.Append("<a href='" + ResolveUrl(modname["MENU_URL"].ToString()) + "' class='nav-link '>");
                        sb.Append("<span class='title My-InsiderTradingMaster-Side-Navbar-Menu-Name'>" + modname["MENU_DISPLAY"] + "</span></a></li>");
                    }
                }
            }
            sb.Append("</ul></li>");
        }
        private void GetUserDetails()
        {
            try
            {
                if (String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])) || String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
                {
                    Response.Redirect("../LogOut.aspx");
                }
                ProcsDLL.Models.InsiderTrading.Model.User user = new ProcsDLL.Models.InsiderTrading.Model.User();
                user.LOGIN_ID = Convert.ToString(Session["EmployeeId"]);
                user.MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(Session["AdminDb"]);
                user.companyId = Convert.ToInt32(Session["CompanyId"]);
                UserRequest objUser = new UserRequest(user);
                UserResponse objResponse = objUser.GetUserDetails();
                if (objResponse.StatusFl)
                {
                    ImgUserUploadedImageHeader.Attributes["src"] = "images/user/" + objResponse.User.uploadAvatar;
                    SpUserNameHeader.InnerHtml = objResponse.User.USER_NM;
                    Session["BusinessUnitId"] = objResponse.User.businessUnit.businessUnitId;
                    Session["BusinessUnitName"] = objResponse.User.businessUnit.businessUnitName;
                    Session["ParentBusinessUnitId"] = objResponse.User.businessUnit.parentBusinessUnitId;
                    Session["ParentBusinessUnitName"] = objResponse.User.businessUnit.parentBusinessUnitName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void RecoverPassword(Object sender, EventArgs e)
        {
            try
            {
                ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
                if (String.IsNullOrEmpty(Convert.ToString(Session["EmployeeId"])))
                {
                    Response.Redirect("../LogOut.aspx");
                }
                login.LoginId = Convert.ToString(Session["EmployeeId"]);
                LoginRequest objLogin = new LoginRequest(login);
                LoginResponse objResponse = objLogin.GetUserEmailByUserId();
                if (objResponse.StatusFl)
                {
                    login.Email = objResponse.User.Email;
                    objLogin = new LoginRequest(login);
                    objResponse = objLogin.GetUserInfo();
                    if (objResponse.StatusFl)
                    {
                        string from = "procstest@gmail.com";
                        string disclosureEmail = String.Empty;
                        string to = objResponse.User.Email;
                        string smtpHostName = "smtp.gmail.com";
                        bool ssl = true;
                        string smtpUserName = "procstest@gmail.com";
                        string password = "P@ssw0rd@123";
                        bool userDefaultCredential = false;
                        Int32 port = 587;

                        SqlParameter[] parameters1 = new SqlParameter[3];
                        parameters1[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                        parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters1[1].Direction = ParameterDirection.Output;
                        parameters1[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(Session["CompanyId"]));

                        DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters1);
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

                        string moreSalts = Convert.ToString(HttpContext.Current.Session["moreSalt"]);

                        var newPasswordMade = "H@xxL0rd" + moreSalts;
                        var newPassword = hashcodegenerate.GetSHA512(newPasswordMade);
                        string loginId = objResponse.User.LoginId;

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
                                to, "Password Recovered", msg,null,"Password Recovery", Convert.ToString(Session["CompanyId"]),
                                "", loginId, loginId
                            );
                            //EmailHelper.SendMail(
                            //    from, disclosureEmail, to, smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port, userName, 
                            //    newPasswordMade, loginId
                            //);
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){alert('Password sent to the registered mail id " + to + "');},1000);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){alert('User does not exist with this email id');},1000);", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){alert('Something went wrong');},1000);", true);
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showalert", "setTimeout(function(){alert('" + Message + "');},1000);", true);
            }

        }
    }
}