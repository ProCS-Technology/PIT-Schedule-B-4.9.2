using Ionic.Zip;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/PreClearanceRequest")]
    public class PreClearanceRequestController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetTypeOfSecurityList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetTypeOfSecurityList()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetTypeOfSecurity();
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
        [Route("GetTypeOfRestrictedCompaniesList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetTypeOfRestrictedCompaniesList()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetTypeOfRestrictedCompanies();
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
        [Route("GetTypeOfTransactionList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetTypeOfTransactionList()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetTypeOfTransaction();
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
        [Route("GetTradeExchangeList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetTradeExchangeList()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetTradeExchange();
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
        [Route("GetDematAccountList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetDematAccountList()
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetDematAccount();
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
        [Route("GetRelativeDetailList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetRelativeDetailList()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetRelativeDetail();
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
        [Route("GetRelativeDetailListBN")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetRelativeDetailListBN()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetRelativeDetailBN();
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
        [Route("GetRelativeDetailList_for_other")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetRelativeDetailList_for_other()
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
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetRelativeDetail();
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
        [Route("SavePreClearanceRequest")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse SavePreClearanceRequest()
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                if (gReqPClR.ValidateTradeDateLiesInTradingWindowClosure() && pClR.Status != "Cancel")
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Requested Trade Date cannot be within the Trading Window Closure.";
                    return objResponse;
                }
                if (gReqPClR.ValidateTradeDateFallsInHolidayList() && pClR.Status != "Cancel")
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Requested Trade Date cannot fall in market closed date.";
                    return objResponse;
                }
                //if (gReqPClR.ValidateaAnotherRequest() && pClR.Status != "Cancel")
                //{
                //    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Requested Trade Date cannot fall in market closed date.";
                //    return objResponse;
                //}
                PreClearanceRequestResponse gResPClR = gReqPClR.SavePreClearanceRequest();
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
        [Route("SavePreClearanceRequest_for_other")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse SavePreClearanceRequest_for_other()
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
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.SavePreClearanceRequest_for_other();
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
        [Route("GetPreClearanceRequestList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetPreClearanceRequestList()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetPreClearanceRequest();
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
        [Route("GetPreClearanceRequestListFilterByStatus")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetPreClearanceRequestListFilterByStatus()
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetPreClearanceRequestFilterByStatus();
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
        [Route("GetFormsCDJ")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetFormsCDJ()
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
                PreClearanceRequestResponse gResPClR = gReqPClR.GetFormsCDJ();
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
        [Route("GetPreClearanceRequestList_for_other")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetPreClearanceRequestList_for_other()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetPreClearanceRequest_for_other();
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
        [Route("SaveBrokerNoteUpload")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse SaveBrokerNoteUpload()
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
                string sContentType = HttpContext.Current.Request.ContentType;
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
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "BrokerNote_" + pClR.PreClearanceRequestId;//Path.GetFileNameWithoutExtension(file.FileName);
                        string sContentTyp = file.ContentType;
                        if (sContentTyp.ToUpper() == "APPLICATION/PDF")
                        {
                            if (sNm.Contains("%00"))
                            {
                                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                                return objResponse;
                            }
                            if (ext.ToLower() == ".pdf")
                            {
                                newFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                                sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                                file.SaveAs(sSaveAs);
                                pClR.BrokerNote = newFileName;
                            }
                            else
                            {
                                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Only pdf attachement is allowed";
                                return objResponse;
                            }
                        }
                        else
                        {
                            PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Content type of the uploaded document does not matched with the permissible document";
                            return objResponse;
                        }
                    }
                }
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                if (!pClR.isNUllTrade)
                {
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
                }
                PreClearanceRequestResponse gResPClR = gReqPClR.AddUpdateBrokerNote();
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
        [Route("SaveBenposTradeTransaction")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse SaveBenposTradeTransaction()
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
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.SaveBenposTradeTransaction();
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
        [Route("GetAllPendingRequest")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Pending Request APIs" })]
        public PreClearanceRequestResponse GetAllPendingRequest()
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
                PreClearanceRequest pClR = new PreClearanceRequest();
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
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetAllPendingRequest();
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
        [Route("GetTemplateForApproverForAction")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Get Template For Approver For Action APIs" })]
        public PreClearanceRequestResponse GetTemplateForApproverForAction()
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
                // pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetTemplateForApproverForAction();
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
        [Route("UpdateTaskUsers")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Update Task Users APIs" })]
        public PreClearanceRequestResponse UpdateTaskUsers()
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.UpdateTaskUsers();
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
        [Route("GetForms")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Get Forms APIs" })]
        public PreClearanceRequestResponse GetForms()
        {
            String htmlText = String.Empty;
            String formCDJDisplayName = String.Empty;
            PreClearanceRequestResponse gResPClR = new PreClearanceRequestResponse();
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                gResPClR = gReqPClR.GetTransactionalForms();

                #region "Code Commented"
                //DataSet dtForms = gReqPClR.GetForms();
                //if (dtForms == null)
                //{
                //    gResPClR.StatusFl = false;
                //    gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                //    return gResPClR;
                //}
                //if (dtForms.Tables.Count == 0)
                //{
                //    gResPClR.StatusFl = false;
                //    gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                //    return gResPClR;
                //}
                //htmlText = ReturnFormHtml(pClR.formType, pClR);
                //formCDJDisplayName = ReturnFormName(pClR.formType, pClR);
                //if (!String.IsNullOrEmpty(htmlText))
                //{
                //    String downloadFileDir = "/InsiderTrading/emailAttachment/";
                //    String fileName = String.Empty;

                //    //Creates a new Word document
                //    WordDocument wordDocument = new WordDocument();

                //    //Creates a section to the document
                //    IWSection section = wordDocument.AddSection();

                //    //Saves the Word document to disk in DOCX format
                //    if ((pClR.formType == "FORM_CJ") || (pClR.formType == "FORM_DJ"))
                //    {
                //        fileName = formCDJDisplayName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".docx";
                //        section.PageSetup.Orientation = PageOrientation.Landscape;
                //    }
                //    else if (pClR.formType == "FORM_J")
                //    {
                //        fileName = formCDJDisplayName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".docx";
                //        section.PageSetup.Orientation = PageOrientation.Portrait;
                //    }

                //    //Validates whether the string is in proper XHTML
                //    section.Body.IsValidXHTML(htmlText, wordDocument.XHTMLValidateOption);

                //    //Inserts HTML string to the document
                //    section.Body.InsertXHTML(htmlText);
                //    wordDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName));

                //    //Close Word document
                //    wordDocument.Close();

                //    if (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName)))
                //    {
                //        gResPClR.StatusFl = true;
                //        gResPClR.PreClearanceRequest = new PreClearanceRequest();
                //        gResPClR.PreClearanceRequest.formUrl = ("" + downloadFileDir + fileName);
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                gResPClR = new PreClearanceRequestResponse();
                gResPClR.StatusFl = false;
                gResPClR.Msg = ex.Message;
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", "", 5);
            }
            return gResPClR;
        }
        [Route("SubmitCustomForm")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Submit Custom Forms APIs" })]
        public PreClearanceRequestResponse SubmitCustomForm()
        {
            String htmlText = String.Empty;
            PreClearanceRequestResponse gResPClR = new PreClearanceRequestResponse();
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);

                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                #region "Save Files In Folder"

                String docFileName = String.Empty;
                String pdfFileName = String.Empty;
                String ext = String.Empty;
                List<string> lstCustomForms = new List<string>();

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = "";
                    String mainDir = "/InsiderTrading/emailAttachment";

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + mainDir)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + mainDir));
                    }
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        ext = Path.GetExtension(file.FileName);
                        //if (file.FileName.Length < 30)
                        //{
                        //    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                        //    objResponse.StatusFl = false;
                        //    objResponse.Msg = "File name should be uploaded with same name as it is downloaded !";
                        //    return objResponse;
                        //}
                        String name = Path.GetFileNameWithoutExtension(file.FileName);//.Replace(".", "").Substring(0, 30);

                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            docFileName = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            docFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }

                        lstCustomForms.Add(docFileName);
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + mainDir), docFileName);
                        file.SaveAs(sSaveAs);
                    }
                    bool status = false;
                    if (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~" + mainDir), docFileName)))
                    {
                        //EmailHelper email = new EmailHelper();
                        // bool status = false;
                        pdfFileName = Path.GetFileNameWithoutExtension(Path.Combine(HttpContext.Current.Server.MapPath("~" + mainDir), docFileName)) + ".pdf";

                        if (ext.ToUpper() == ".DOCX")
                        {
                            bool isStatus = ConvertDocToPDF(docFileName, pdfFileName, mainDir);
                            if (isStatus)
                            {
                                pClR.lstFormUrl = lstCustomForms;
                                //status = email.SendInsiderFormToCO(pClR);
                            }
                        }
                        else
                        {
                            pClR.lstFormUrl = lstCustomForms;
                            //status = email.SendInsiderFormToCO(pClR);
                        }
                        if (status)
                        {
                            gResPClR.StatusFl = true;
                            gResPClR.Msg = "Forms have been submitted successfully! A copy has been also shared on your email id for reference!";
                        }
                        else
                        {
                            gResPClR.StatusFl = false;
                            gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                        }
                    }
                }
                else
                {
                    gResPClR.StatusFl = false;
                    gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                }

                #endregion
            }
            catch (Exception ex)
            {
                gResPClR = new PreClearanceRequestResponse();
                gResPClR.StatusFl = false;
                gResPClR.Msg = ex.Message;
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "PreClearanceRequestController", "SubmitForm", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return gResPClR;
        }
        [Route("SubmitSystemGeneratedForm")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Submit System Generated Forms APIs" })]
        public PreClearanceRequestResponse SubmitSystemGeneratedForm()
        {
            String htmlText = String.Empty;
            PreClearanceRequestResponse gResPClR = new PreClearanceRequestResponse();
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String formCDJDisplayName = String.Empty;
                String input = HttpContext.Current.Request.Form["Object"];
                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                gResPClR = gReqPClR.SaveAndGetTransactionalForms();
                #region "Commented Code"
                //PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                //DataSet dtForms = gReqPClR.GetForms();
                //if (dtForms == null)
                //{
                //    gResPClR.StatusFl = false;
                //    gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                //    return gResPClR;
                //}
                //if (dtForms.Tables.Count == 0)
                //{
                //    gResPClR.StatusFl = false;
                //    gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                //    return gResPClR;
                //}
                //htmlText = ReturnFormHtml(pClR.formType, pClR);
                //formCDJDisplayName = ReturnFormName(pClR.formType, pClR);
                //if (!String.IsNullOrEmpty(htmlText))
                //{
                //    String downloadFileDir = "/InsiderTrading/emailAttachment/";
                //    String docFileName = String.Empty;
                //    String pdfFileName = String.Empty;

                //    //Creates a new Word document
                //    WordDocument wordDocument = new WordDocument();

                //    //Creates a section to the document
                //    IWSection section = wordDocument.AddSection();

                //    //Saves the Word document to disk in DOCX format
                //    pdfFileName = formCDJDisplayName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture);
                //    if ((pClR.formType == "FORM_CJ") || (pClR.formType == "FORM_DJ"))
                //    {
                //        docFileName = pdfFileName + ".docx";
                //        section.PageSetup.Orientation = PageOrientation.Landscape;
                //    }
                //    else if (pClR.formType == "FORM_J")
                //    {
                //        docFileName = pdfFileName + ".docx";
                //        section.PageSetup.Orientation = PageOrientation.Portrait;
                //    }

                //    //Validates whether the string is in proper XHTML
                //    section.Body.IsValidXHTML(htmlText, wordDocument.XHTMLValidateOption);

                //    //Inserts HTML string to the document
                //    section.Body.InsertXHTML(htmlText);
                //    wordDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), docFileName));

                //    //Close Word document
                //    wordDocument.Close();

                //    if (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), docFileName)))
                //    {
                //        bool isStatus = ConvertDocToPDF(docFileName, pdfFileName + ".pdf", downloadFileDir);
                //        if (isStatus)
                //        {
                //            pClR.BrokerNote = pdfFileName + ".pdf";

                //            EmailHelper email = new EmailHelper();
                //            bool status = email.SendInsiderFormToCO(pClR);
                //            if (status)
                //            {
                //                gResPClR.StatusFl = true;
                //                gResPClR.Msg = "Forms have been submitted successfully! A copy has been also shared on your email id for reference!";
                //            }
                //        }
                //        else
                //        {
                //            gResPClR.StatusFl = false;
                //            gResPClR.Msg = "Something went wrong. Please try again or contact administrator!";
                //        }
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                gResPClR = new PreClearanceRequestResponse();
                gResPClR.StatusFl = false;
                gResPClR.Msg = ex.Message;
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "PreClearanceRequestController", "SubmitForm", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return gResPClR;
        }
        [HttpPost]
        [Route("DownloadTradingZipFile")]
        public HttpResponseMessage DownloadTradingZipFile()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return null;
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
                    return null;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                List<FileModel> files = gReqPClR.GetAllTradingFilesOfCurrentRequest();

                //Create HTTP Response.
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                //Create the Zip File.
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    // zip.AddDirectoryByName("Files");
                    foreach (FileModel file in files)
                    {
                        if (File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + file.FileName))))
                        {
                            zip.AddFile(Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + file.FileName)), "TradingReport");
                        }
                    }

                    //Set the Name of Zip File.
                    string zipName = String.Format("TradingFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        //Save the Zip File to MemoryStream.
                        zip.Save(memoryStream);

                        //Set the Response Content.
                        response.Content = new ByteArrayContent(memoryStream.ToArray());

                        //Set the Response Content Length.
                        response.Content.Headers.ContentLength = memoryStream.ToArray().LongLength;

                        //Set the Content Disposition Header Value and FileName.
                        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        response.Content.Headers.ContentDisposition.FileName = zipName;

                        //Set the File Content Type.
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.RequestTimeout);
                //Set the File Content Type.
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                return response;

            }

        }
        private String ReturnFormHtml(String formType, PreClearanceRequest pClR)
        {
            pClR.formType = formType;
            PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);

            DataSet Ds = new DataSet();
            Ds = gReqPClR.GetForms();
            DataTable dtValue = new DataTable();
            DataTable dtHDR = new DataTable();
            DataTable dtDTL = new DataTable();
            dtValue = Ds.Tables[0];

            dtValue.Columns.Add("From_Name", typeof(System.String));
            dtValue.Columns.Add("Today_Date", typeof(System.String));
            dtValue.Rows[0]["From_Name"] = formType;
            dtValue.Rows[0]["Today_Date"] = DateTime.Now.ToString("dd/MM/yyyy");
            dtHDR = Ds.Tables[1];
            dtDTL = Ds.Tables[2];
            string htmlText = Convert.ToString(dtHDR.Rows[0]["TEMPLATE_MSG"]);

            foreach (DataRow dr in dtDTL.Rows)
            {

                string sColNm = Convert.ToString(dr["TEMPLATE_COLUMN"]).Replace("[", "").Replace("]", "");
                string sDisNm = Convert.ToString(dr["TEMPLATE_DISPLAY"]);

                if (htmlText.IndexOf(sDisNm) > -1)
                {
                    string sMessage = htmlText.Replace(sDisNm, Convert.ToString(dtValue.Rows[0][sColNm]));
                    htmlText = sMessage;
                }

            }

            return htmlText;
        }
        private String ReturnFormName(String formType, PreClearanceRequest pClR)
        {
            pClR.formType = formType;
            PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);

            DataSet Ds = new DataSet();
            Ds = gReqPClR.GetForms();
            DataTable dtHDR = new DataTable();

            dtHDR = Ds.Tables[1];

            string formCDJDisplayName = !String.IsNullOrEmpty(Convert.ToString(dtHDR.Rows[0]["DISPLAY_NAME"])) ? Convert.ToString(dtHDR.Rows[0]["DISPLAY_NAME"]) : String.Empty;


            return formCDJDisplayName;
        }
        private bool ConvertDocToPDF(String docFileName, String pdfFileName, String filePath)
        {
            bool isStatus = false;
            try
            {
                //Loads an existing Word document
                WordDocument wordDocument = new WordDocument(Path.Combine(HttpContext.Current.Server.MapPath("~" + filePath), docFileName), Syncfusion.DocIO.FormatType.Docx);

                //create an instance of DocToPDFConverter - responsible for Word to PDF conversion
                DocToPDFConverter converter = new DocToPDFConverter();

                //Set the image quality
                converter.Settings.ImageQuality = 100;

                //Set the image resolution
                converter.Settings.ImageResolution = 640;

                //Set true to optimize the memory usage for identical images
                converter.Settings.OptimizeIdenticalImages = true;

                //Convert Word document into PDF document
                PdfDocument pdfDocument = converter.ConvertToPDF(wordDocument);
                //pdfDocument.PageSettings.Size = PdfPageSize.A4;

                //Save the PDF file to file system                
                pdfDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + filePath), pdfFileName));

                //close the instance of document objects
                pdfDocument.Close(true);

                wordDocument.Close();

                isStatus = true;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "PreClearanceRequestController", "ConvertDocToPDF", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                isStatus = false;
            }
            return isStatus;
        }
        [Route("GetUndertakingTemplate")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetUndertakingTemplate()
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetUndertakingTemplate();
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
        [Route("GetFormsInfo")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearanceRequest APIs" })]
        public PreClearanceRequestResponse GetFormsInfo()
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetFormsInfo();
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
        [Route("GetEsopForms")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Get Forms APIs" })]
        public PreClearanceRequestResponse GetEsopForms()
        {
            String htmlText = String.Empty;
            String formCDJDisplayName = String.Empty;
            PreClearanceRequestResponse gResPClR = new PreClearanceRequestResponse();
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
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                gResPClR = gReqPClR.GetTransactionalEsopForms();


            }
            catch (Exception ex)
            {
                gResPClR = new PreClearanceRequestResponse();
                gResPClR.StatusFl = false;
                gResPClR.Msg = ex.Message;
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", "", 5);
            }
            return gResPClR;
        }
        [Route("SubmitSystemGeneratedEsopForm")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Submit System Generated Forms APIs" })]
        public PreClearanceRequestResponse SubmitSystemGeneratedEsopForm()
        {
            String htmlText = String.Empty;
            PreClearanceRequestResponse gResPClR = new PreClearanceRequestResponse();
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String formCDJDisplayName = String.Empty;
                String input = HttpContext.Current.Request.Form["Object"];
                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                gResPClR = gReqPClR.SaveAndGetTransactionalEsopForms();

            }
            catch (Exception ex)
            {
                gResPClR = new PreClearanceRequestResponse();
                gResPClR.StatusFl = false;
                gResPClR.Msg = ex.Message;
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "PreClearanceRequestController", "SubmitForm", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return gResPClR;
        }
        [Route("SaveBrokerNoteList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "SaveBrokerNoteList APIs" })]
        public PreClearanceRequestResponse SaveBrokerNoteList()
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
                String input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                List<PreClearanceRequest> pClR = new JavaScriptSerializer().Deserialize<List<PreClearanceRequest>>(input);
                PreClearanceRequestResponse gResPClRX = new PreClearanceRequestResponse();
                foreach (PreClearanceRequest p in pClR)
                {
                    p.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    p.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    p.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    p.TradeCompany = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    if (!p.ValidateInput())
                    {
                        gResPClRX.StatusFl = false;
                        gResPClRX.Msg = sXSSErrMsg;
                        return gResPClRX;
                    }
                }

                foreach (PreClearanceRequest p in pClR)
                {
                    p.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    p.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    p.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    p.TradeCompany = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(p);
                    PreClearanceRequestResponse gResPClR = gReqPClR.AddBrokerNoteWithNoPC();
                }
                gResPClRX.StatusFl = true;
                gResPClRX.Msg = "Data saved successfully";
                return gResPClRX;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [SwaggerOperation(Tags = new[] { "GetTradeTransactions APIs" })]
        public TradeTransactionResponse GetTradeTransactions()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TradeTransactionResponse objResponse = new TradeTransactionResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest pClR = new PreClearanceRequest();
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                pClR.TradeCompany = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);

                if (!pClR.ValidateInput())
                {
                    TradeTransactionResponse gResPClR1 = new TradeTransactionResponse();
                    gResPClR1.StatusFl = false;
                    gResPClR1.Msg = sXSSErrMsg;
                    return gResPClR1;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                TradeTransactionResponse gResPClR = gReqPClR.GetTradeTransactions();
                return gResPClR;
            }
            catch (Exception ex)
            {
                TradeTransactionResponse objResponse = new TradeTransactionResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}