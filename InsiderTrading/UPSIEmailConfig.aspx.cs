using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Configuration;
namespace ProcsDLL.InsiderTrading
{
    public partial class UPSIEmailConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtRedirectUri.Text = ConfigurationManager.AppSettings["Office365ReturnUrl"].ToString();
            if (!Page.IsPostBack)
            {
                fnGetEmailConfig();
            }
        }
        private void fnGetEmailConfig()
        {
            ProcsDLL.Models.InsiderTrading.Model.UPSIEmailConfig upsiEmailConfig = new ProcsDLL.Models.InsiderTrading.Model.UPSIEmailConfig();
            upsiEmailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsiEmailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsiEmailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            UPSIConfigRequest configReq = new UPSIConfigRequest(upsiEmailConfig);
            UPSIEmailConfigResponse upsiEmailRes = configReq.GetUPSIEmailConfig();

            Boolean flAll = false;
            if (upsiEmailRes.ListEmailConfig != null)
            {
                string strHtml = "";
                foreach (var obj in upsiEmailRes.ListEmailConfig)
                {
                    if (obj.UpsiTypNm == "All")
                    {
                        flAll = true;
                        //break;
                    }
                    strHtml += "<tr>" +
                        "<td>" + obj.UpsiTypNm + "</td>" +
                        "<td>" + obj.AuthenticationType + "</td>" +
                        "<td>" + obj.SmartType + "</td>" +
                        "<td>" + obj.UPSIEmail + "</td>" +
                        "<td><a data-target='#stack1' data-toggle='modal' id='a" + obj.ConfigId + "' class='btn btn-outline dark' onclick='javascript:fnEditEmail(\"" + obj.ConfigId + "\");'>Edit</a>" +
                        "<a style='margin-left:20px' data-target='#delete' data-toggle='modal' class='btn btn-outline dark' onclick='javascript:DeleteEmail(\"" + obj.ConfigId + "\");'>Delete</a></td>" +
                        "<td style='display:none;'><input type='text' id='lblTypId' value='" + obj.UpsiTypId + "' />"+
                        "<input type='text' id='lblConfigId' value='" + obj.ConfigId + "' /></td>";
                }
                tbdUPSIEmailLst.InnerHtml = strHtml;
            }


            ProcsDLL.Models.InsiderTrading.Model.UPSIConfig upsiConfig = new ProcsDLL.Models.InsiderTrading.Model.UPSIConfig();
            upsiConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsiConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsiConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiConfig);
            UPSIConfigResponse upsiRes = upsiReq.GetUPSIConfig();
            if (upsiRes.UpsiConfig.EmailAutomation.ToUpper() == "YES")
            {
                if (upsiRes.UpsiConfig.MultipleEmail.ToUpper() == "YES")
                {
                    

                    ProcsDLL.Models.InsiderTrading.Model.UPSIType upsiConfigX = new ProcsDLL.Models.InsiderTrading.Model.UPSIType();
                    upsiConfigX.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiConfigX.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiConfigX.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    UPSITypeRequest upsiReqX = new UPSITypeRequest(upsiConfigX);
                    UPSITypeResponse upsiResX = upsiReqX.GetUPSITypeList();

                    if (flAll == false)
                    {
                        ddlUPSITyp.Items.Add(new ListItem("", ""));
                        if (upsiEmailRes.ListEmailConfig == null)
                        {
                            ddlUPSITyp.Items.Add(new ListItem("All", "0"));
                        }
                        if (upsiResX.UPSITypeList != null)
                        {
                            foreach (var obj in upsiResX.UPSITypeList)
                            {
                                ddlUPSITyp.Items.Add(new ListItem(obj.TypeNm, obj.TypeId.ToString()));
                            }
                        }
                    }
                    else
                    {
                        ddlUPSITyp.Items.Add(new ListItem("All", "0"));
                    }
                }
                else
                {
                    ddlUPSITyp.Items.Add(new ListItem("All", "0"));
                }
            }
        }
        private void fnGetUPSIType()
        {
            ProcsDLL.Models.InsiderTrading.Model.UPSIType upsiConfig = new ProcsDLL.Models.InsiderTrading.Model.UPSIType();
            upsiConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsiConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsiConfig.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            UPSITypeRequest upsiReq = new UPSITypeRequest(upsiConfig);
            UPSITypeResponse upsiRes = upsiReq.GetUPSITypeList();

            string strOptions = "<option value=''></option>";
            strOptions += "<option value='0'>All</option>";

            ddlUPSITyp.Items.Add(new ListItem("", ""));
            ddlUPSITyp.Items.Add(new ListItem("All", "0"));
            foreach (var obj in upsiRes.UPSITypeList)
            {
                ddlUPSITyp.Items.Add(new ListItem(obj.TypeNm, obj.TypeId.ToString()));
            }
        }
    }
}