using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class UPSIGroupReportController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetUPSIReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public UPSIGroupReportResponse GetUPSIReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIMembersGroup uPSICommunication = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
                uPSICommunication.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                uPSICommunication.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                uPSICommunication.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (uPSICommunication.ValidateInput())
                {
                    UPSIGroupReportRequest gReqUPSICommunication = new UPSIGroupReportRequest(uPSICommunication);
                    UPSIGroupReportResponse gResUPSICommunication = gReqUPSICommunication.GetUPSIGroupList();
                    return gResUPSICommunication;
                }
                else
                {
                    UPSIGroupReportResponse gResUPSICommunication = new UPSIGroupReportResponse();
                    gResUPSICommunication.StatusFl = false;
                    gResUPSICommunication.Msg = sXSSErrMsg;
                    return gResUPSICommunication;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("HistoryUPSIGroup")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupReportResponse HistoryUPSIGroup()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSIMembersGroup upsi1 = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (upsi1.ValidateInput())
                {
                    UPSIGroupReportRequest cReq = new UPSIGroupReportRequest(upsi1);
                    UPSIGroupReportResponse cRes = cReq.HistoryUPSIGroup();
                    return cRes;
                }
                else
                {
                    UPSIGroupReportResponse cRes = new UPSIGroupReportResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIMember")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public UPSIGroupReportResponse GetUPSIMember()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIMembersGroup uPSICommunication = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
                uPSICommunication.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                uPSICommunication.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                uPSICommunication.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (uPSICommunication.ValidateInput())
                {
                    UPSIGroupReportRequest gReqUPSICommunication = new UPSIGroupReportRequest(uPSICommunication);
                    UPSIGroupReportResponse gResUPSICommunication = gReqUPSICommunication.GetUPSIMember();
                    return gResUPSICommunication;
                }
                else
                {
                    UPSIGroupReportResponse gResUPSICommunication = new UPSIGroupReportResponse();
                    gResUPSICommunication.StatusFl = false;
                    gResUPSICommunication.Msg = sXSSErrMsg;
                    return gResUPSICommunication;
                }

            }
            catch (Exception ex)
            {
                UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIReportByMember")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Report APIs" })]
        public UPSIGroupReportResponse GetUPSIReportByMember()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIMembersGroup uPSICommunication = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
                uPSICommunication.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                uPSICommunication.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                uPSICommunication.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (uPSICommunication.ValidateInput())
                {
                    UPSIGroupReportRequest gReqUPSICommunication = new UPSIGroupReportRequest(uPSICommunication);
                    UPSIGroupReportResponse gResUPSICommunication = gReqUPSICommunication.GetUPSIReportByMember();
                    return gResUPSICommunication;
                }
                else
                {
                    UPSIGroupReportResponse gResUPSICommunication = new UPSIGroupReportResponse();
                    gResUPSICommunication.StatusFl = false;
                    gResUPSICommunication.Msg = sXSSErrMsg;
                    return gResUPSICommunication;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupReportResponse objResponse = new UPSIGroupReportResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}