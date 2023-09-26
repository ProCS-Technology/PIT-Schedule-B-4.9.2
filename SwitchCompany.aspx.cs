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

namespace ProcsDLL
{
    public partial class SwitchCompany : System.Web.UI.Page
    {
        string sGblUrl = Convert.ToString(ConfigurationManager.AppSettings["GlobalUrl"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Label1.Text = "enter";
                if (Request.QueryString["compid"] != null && Request.QueryString["CompName"] != null)
                {
                    Login(Request.QueryString["compid"].ToString(), Request.QueryString["CompName"].ToString());
                }
            }

        }
        public void Login(String CompanyId,String CompanyNm)
        {
            try
            {
                ProcsDLL.Models.Login.Modal.Login login = new ProcsDLL.Models.Login.Modal.Login();
                login.LoginId = Convert.ToString(Session["EmployeeId"]);
                login.CompanyId = CompanyId;
                LoginRequest objlogin = new LoginRequest(login);

                LoginResponse objResponse = objlogin.ValidateSwitchedUser();
                if (objResponse.StatusFl)
                {
                    if (objResponse.Msg == "Success")
                    {
                        Session.Clear();
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        Session["EmployeeId"] = Convert.ToString(login.LoginId);
                        Session["AdminDb"] = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);

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
                                Session["CompanyId"] = CompanyId;
                                Session["CompanyName"] = CompanyNm;
                                Session["ModuleId"] = matchedObj[0].ModuleId;
                                Session["ModuleName"] = matchedObj[0].ModuleNm;
                                Session["ModuleFolder"] = matchedObj[0].ModuleFolder;
                                Session["ModuleDatabase"] = matchedObj[0].ModuleDataBase;
                                Session["AuthToken"] = Guid.NewGuid().ToString();
                                Response.Cookies["AuthToken"].Value = Session["AuthToken"].ToString();

                                if (sGblUrl != "" && sGblUrl != null)
                                {
                                    Response.Redirect(sGblUrl + Session["ModuleFolder"] + "/" + "Dashboard.aspx");
                                }
                                else
                                {
                                    Response.Redirect(Session["ModuleFolder"] + "/" + "Dashboard.aspx");
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
                        //ShowListing.InnerHtml = sb.ToString();

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential('" + objResponse.Msg + " Salt=" + Convert.ToString(Session["salt"]) + " MoreSalt=" + Convert.ToString(Session["moreSalt"]) + "');},1000);", true);
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "Login", "Login", Convert.ToString("superadmin"), Convert.ToInt32(1), 1);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){unValidCredential();},1000);", true);
            }

        }
    }
}