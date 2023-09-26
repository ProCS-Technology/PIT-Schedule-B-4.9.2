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
    [RoutePrefix("api/UpsiCommunication")]
    public class UpsiCommunicationController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetCommunicationtypeList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UpsiCommunication APIs" })]
        public UpsiCommunicationResponse GetCommunicationtypeList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UpsiCommunicationtype communicationtype = new JavaScriptSerializer().Deserialize<UpsiCommunicationtype>(input);
                communicationtype.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                communicationtype.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                communicationtype.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!communicationtype.ValidateInput())
                {
                    UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UpsiCommunicationRequest gReqCommunicationtypeList = new UpsiCommunicationRequest(communicationtype);
                UpsiCommunicationResponse gResCommunicationtypeList = gReqCommunicationtypeList.GetCommunicationtypeList();
                return gResCommunicationtypeList;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveCommunicationtype")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UpsiCommunication APIs" })]
        public UpsiCommunicationResponse SaveCommunicationtype()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                UpsiCommunicationtype communicationtype = new JavaScriptSerializer().Deserialize<UpsiCommunicationtype>(input);
                communicationtype.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                communicationtype.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                communicationtype.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!communicationtype.ValidateInput())
                {
                    UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                UpsiCommunicationRequest gReqCommunicationtypeList = new UpsiCommunicationRequest(communicationtype);
                UpsiCommunicationResponse gResCommunicationtypeList = gReqCommunicationtypeList.SaveCommunicationtype();
                return gResCommunicationtypeList;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteCommunicationtype")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UpsiCommunication APIs" })]
        public UpsiCommunicationResponse DeleteCommunicationtype()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                UpsiCommunicationtype communicationtype = new JavaScriptSerializer().Deserialize<UpsiCommunicationtype>(input);
                communicationtype.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                communicationtype.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                communicationtype.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!communicationtype.ValidateInput())
                {
                    UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                UpsiCommunicationRequest gReqCommunicationtypeList = new UpsiCommunicationRequest(communicationtype);
                UpsiCommunicationResponse gResCommunicationtypeList = gReqCommunicationtypeList.DeleteCommunicationtype();
                return gResCommunicationtypeList;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse objResponse = new UpsiCommunicationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}