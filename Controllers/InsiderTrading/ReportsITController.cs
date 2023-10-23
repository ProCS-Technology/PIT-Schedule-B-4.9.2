using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/ReportsIT")]
    public class ReportsITController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetNonComplianceReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetNonComplianceReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TradingReport tradingReport = new JavaScriptSerializer().Deserialize<TradingReport>(input);
                tradingReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                tradingReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                tradingReport.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!tradingReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(tradingReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetNonComplianceReport();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetEsopReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetEsopReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TradingReport tradingReport = new JavaScriptSerializer().Deserialize<TradingReport>(input);
                tradingReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                tradingReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!tradingReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(tradingReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetEsopReportBetweenDates();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDeclarationReports")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetDeclarationReports()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                DeclarationReport declarationReport = new JavaScriptSerializer().Deserialize<DeclarationReport>(input);
                declarationReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                declarationReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                declarationReport.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!declarationReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(declarationReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetDeclarationReports();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserMadeDeclarationReports")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetUserMadeDeclarationReports()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                DeclarationReport declarationReport = new JavaScriptSerializer().Deserialize<DeclarationReport>(input);
                declarationReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                declarationReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                declarationReport.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!declarationReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(declarationReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetUserMadeDeclarationReports();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserNotMadeDeclarationReports")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetUserNotMadeDeclarationReports()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                DeclarationReport declarationReport = new JavaScriptSerializer().Deserialize<DeclarationReport>(input);
                declarationReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                declarationReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                declarationReport.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!declarationReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(declarationReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetUserNotMadeDeclarationReports();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDeclarationReportsBetweenDates")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetDeclarationReportsBetweenDates()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                DeclarationReport declarationReport = new JavaScriptSerializer().Deserialize<DeclarationReport>(input1);
                declarationReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                declarationReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                declarationReport.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!declarationReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(declarationReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetDeclarationReportsBetweenDates();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTradingReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetTradingReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TradingReport tradingReport = new JavaScriptSerializer().Deserialize<TradingReport>(input);
                tradingReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                tradingReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!tradingReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(tradingReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetTradingReportBetweenDates();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("CloseNonComplianceTask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse CloseNonComplianceTask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TradingReport tradingReport = new JavaScriptSerializer().Deserialize<TradingReport>(input);
                tradingReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                tradingReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!tradingReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(tradingReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.CloseNonComplianceTask();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetBenposUploadedList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetBenposUploadedList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BenposHeader benposHeader = new BenposHeader();
                benposHeader.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposHeader.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!benposHeader.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqBenposHeader = new ReportsRequest(benposHeader);
                ReportsResponse gResBenposHeader = gReqBenposHeader.GetBenposUploadedList();
                return gResBenposHeader;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetBenposReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetBenposReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                BenposHeader benposHeader = new JavaScriptSerializer().Deserialize<BenposHeader>(input);
                benposHeader.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposHeader.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!benposHeader.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqBenposHeader = new ReportsRequest(benposHeader);
                ReportsResponse gResBenposHeader = gReqBenposHeader.GetBenposReport();
                return gResBenposHeader;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetUPSIReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSICommunication uPSICommunication = new JavaScriptSerializer().Deserialize<UPSICommunication>(input);
                uPSICommunication.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                uPSICommunication.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!uPSICommunication.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqUPSICommunication = new ReportsRequest(uPSICommunication);
                ReportsResponse gResUPSICommunication = gReqUPSICommunication.GetUPSIReportBetweenDates();
                return gResUPSICommunication;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSITemplateReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetUPSITemplateReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSICommunication uPSICommunication = new JavaScriptSerializer().Deserialize<UPSICommunication>(input);
                uPSICommunication.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                uPSICommunication.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!uPSICommunication.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqUPSICommunication = new ReportsRequest(uPSICommunication);
                ReportsResponse gResUPSICommunication = gReqUPSICommunication.GetUPSITemplateReportBetweenDates();
                return gResUPSICommunication;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetConnectedPersonTradingReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetConnectedPersonTradingReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TradingReport tradingReport = new JavaScriptSerializer().Deserialize<TradingReport>(input);
                tradingReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                tradingReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!tradingReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(tradingReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetConnectedPersonTradingReport();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetBrokerNoteDetailsBetweenDates")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetBrokerNoteDetailsBetweenDates()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TradingReport tradingReport = new JavaScriptSerializer().Deserialize<TradingReport>(input);
                tradingReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                tradingReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                tradingReport.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                if (!tradingReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqDeclarationReport = new ReportsRequest(tradingReport);
                ReportsResponse gResDeclarationReport = gReqDeclarationReport.GetBrokerNoteDetailsBetweenDates();
                return gResDeclarationReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetFormLogsReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetFormLogsReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                LogsReport ObjReport = new JavaScriptSerializer().Deserialize<LogsReport>(input);
                //ObjReport.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                ObjReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!ObjReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqReport = new ReportsRequest(ObjReport);
                ReportsResponse gResReport = gReqReport.GetFormLogsReportBetweenDates();
                return gResReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTaskDisclouserReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse GetTaskDisclouserReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User ObjReport = new JavaScriptSerializer().Deserialize<User>(input);
                ObjReport.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                ObjReport.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!ObjReport.ValidateInput())
                {
                    ReportsResponse objResponse = new ReportsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReportsRequest gReqReport = new ReportsRequest(ObjReport);
                ReportsResponse gResReport = gReqReport.GetTaskDisclouserReport();
                return gResReport;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendDisclousertaskEmail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public ReportsResponse SendDisclousertaskEmail()
        {
            try
            {
                ReportsResponse objResponse = new ReportsResponse();
                if (HttpContext.Current.Session.Count == 0)
                {
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User objUser = new JavaScriptSerializer().Deserialize<User>(input);
                objUser.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!objUser.ValidateInput())
                {
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                if (objUser.emailReport.Count > 0)
                {
                    string toemailId = string.Empty;
                    string sub = objUser.subjectReport;
                    string template = objUser.reportTemplate;
                    for (int i = 0; i < objUser.emailReport.Count; i++)
                    {
                        toemailId = objUser.emailReport[i].mailTo;
                        EmailSender.SendMail(
                            toemailId, sub, template, objUser.lstAttachment, "Declaration Mail", objUser.companyId.ToString(),
                            "", objUser.LOGIN_ID, objUser.LOGIN_ID
                        );
                    }
                }
                objResponse.StatusFl = true;
                objResponse.Msg = "Email Send Successfully";
                return objResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objResponse = new ReportsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}