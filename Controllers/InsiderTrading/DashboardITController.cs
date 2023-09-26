using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/DashboardIT")]
    public class DashboardITController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetOpenDisclosureRequest")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetOpenDisclosureRequest()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.ADMIN_DATABASE = sAdminDb;
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetOpenDisclosureRequest();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfAllPreClearanceRequest")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfAllPreClearanceRequest()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetAllPreClearanceRequestCount();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfAllPreClearanceRequestForAllUser")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfAllPreClearanceRequestForAllUser()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetAllPreClearanceRequestCountForAllUser();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfAllTradeDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfAllTradeDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetCountOfAllTradeDetails();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SubmitEsopFormC")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public PreClearanceRequestResponse SubmitEsopFormC()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.SubmitEsopFormC();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfMyTradeDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfMyTradeDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetCountOfMyTradeDetails();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTradeDetailsInfo")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetTradeDetailsInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetTradeDetailsInfo();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyActionable")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetMyActionable()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyActionable();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SubmitBrokerNoteRequestDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public PreClearanceRequestResponse SubmitBrokerNoteRequestDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = "";
                    String userDir = "/InsiderTrading/TradingRequestDetails/";
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    String newFileName = String.Empty;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        String name = Path.GetFileNameWithoutExtension(file.FileName);
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            newFileName = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            newFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                        file.SaveAs(sSaveAs);
                        pClR.BrokerNote = newFileName;
                    }
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                //if (gReqPClR.ValidateTradeDateLiesInTradingWindowClosureBrokerNote())
                //{
                //    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Actual Transaction Date cannot be within the Trading Window Closure.";
                //    return objResponse;
                //}
                if (gReqPClR.ValidateTradeDateFallsInHolidayListBrokerNote())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Actual Transaction Date Date cannot fall in market closed date.";
                    return objResponse;
                }
                PreClearanceRequestResponse gResPClR = gReqPClR.AddBrokerNoteWithNoPC();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateTradeBifurcation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse UpdateTradeBifurcation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Dashboard dashboard = new JavaScriptSerializer().Deserialize<Dashboard>(input);
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.UpdateTradeBifurcation();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyUPSITask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetMyUPSITask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyUPSITask();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyUPSITaskById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetMyUPSITaskById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyUPSITaskById();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateUPSITaskById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse UpdateUPSITaskById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                //dashboard.loginId = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                var aa = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                dashboard.UPSITask.TaskId = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                dashboard.UPSITask.Group_id = Convert.ToString(HttpContext.Current.Request.Form["GROUP_ID"]);
                dashboard.UPSITask.Status = Convert.ToString(HttpContext.Current.Request.Form["Status"]);

                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.UpdateUPSITaskById();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SubmitNcBrokerNoteRequestDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public PreClearanceRequestResponse SubmitNcBrokerNoteRequestDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = "";
                    String userDir = "/InsiderTrading/TradingRequestDetails/";
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    String newFileName = String.Empty;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        String name = Path.GetFileNameWithoutExtension(file.FileName);
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            newFileName = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            newFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                        file.SaveAs(sSaveAs);
                        pClR.BrokerNote = newFileName;
                    }
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                if (gReqPClR.ValidateTradeDateLiesInTradingWindowClosureBrokerNote())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Actual Transaction Date cannot be within the Trading Window Closure.";
                    return objResponse;
                }
                if (gReqPClR.ValidateTradeDateFallsInHolidayListBrokerNote())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Actual Transaction Date Date cannot fall in market closed date.";
                    return objResponse;
                }
                PreClearanceRequestResponse gResPClR = gReqPClR.AddNonComplianceBrokerNote();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateNonComplianceTask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public PreClearanceRequestResponse UpdateNonComplianceTask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objSessionResponse = new PreClearanceRequestResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest ObjnonCompliance = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                ObjnonCompliance.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!ObjnonCompliance.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest objReq = new PreClearanceRequestRequest(ObjnonCompliance);
                PreClearanceRequestResponse objRes = objReq.UpdateNonComplianceTask();
                return objRes;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        
        [Route("UpdateTransactionHistory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public PreClearanceRequestResponse UpdateTransactionHistory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objSessionResponse = new PreClearanceRequestResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest ObjnonCompliance = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                ObjnonCompliance.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!ObjnonCompliance.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest objReq = new PreClearanceRequestRequest(ObjnonCompliance);
                PreClearanceRequestResponse objRes = objReq.UpdateTransactionHistory();
                return objRes;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
