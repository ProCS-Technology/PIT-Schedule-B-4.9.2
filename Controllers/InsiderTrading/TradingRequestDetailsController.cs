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
    [RoutePrefix("api/TradingRequestDetails")]
    public class TradingRequestDetailsController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetTradingRequestDetailsList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Trading Request Detail APIs" })]
        public TradingRequestDetailsResponse GetTradingRequestDetailsList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TradingRequestDetailsResponse objResponse = new TradingRequestDetailsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest trd = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                trd.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trd.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trd.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trd.ValidateInput())
                {
                    TradingRequestDetailsResponse objResponse = new TradingRequestDetailsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                TradingRequestDetailsRequest TradingRequestDetailsList = new TradingRequestDetailsRequest(trd);
                TradingRequestDetailsResponse gResTdList = TradingRequestDetailsList.GetTradingRequestDetailsList();
                return gResTdList;
            }
            catch (Exception ex)
            {
                TradingRequestDetailsResponse objResponse = new TradingRequestDetailsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTradingRequestDetailsListByAdmin")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Trading Request Detail APIs" })]
        public TradingRequestDetailsResponse GetTradingRequestDetailsListByAdmin()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TradingRequestDetailsResponse objResponse = new TradingRequestDetailsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest trd = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                trd.LoginId = trd.LoginId == String.Empty ? Convert.ToString(HttpContext.Current.Session["EmployeeId"]) : trd.LoginId;
                trd.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trd.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trd.ValidateInput())
                {
                    TradingRequestDetailsResponse objResponse = new TradingRequestDetailsResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                TradingRequestDetailsRequest TradingRequestDetailsList = new TradingRequestDetailsRequest(trd);
                TradingRequestDetailsResponse gResTdList = TradingRequestDetailsList.GetTradingRequestDetailsList();
                return gResTdList;
            }
            catch (Exception ex)
            {
                TradingRequestDetailsResponse objResponse = new TradingRequestDetailsResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}