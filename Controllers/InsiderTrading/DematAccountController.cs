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
    [RoutePrefix("api/DematAccount")]
    public class DematAccountController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetDematAccountListByRelativeId")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Demat Account APIs" })]
        public RelativeResponse GetDematAccountListByRelativeId()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RelativeResponse objResponse = new RelativeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Relative relative = new JavaScriptSerializer().Deserialize<Relative>(input);
                relative.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                relative.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                relative.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!relative.ValidateInput())
                {
                    RelativeResponse objResponse = new RelativeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RelativeRequest relativeReq = new RelativeRequest(relative);
                RelativeResponse relativeRes = relativeReq.GetDematAccountListByRelativeId();
                return relativeRes;
            }
            catch (Exception ex)
            {
                RelativeResponse objResponse = new RelativeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DematAccountInfo")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Demat Account APIs" })]
        public DematAccountResponse DematAccountInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DematAccountResponse objResponse = new DematAccountResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                DematAccount dematAccount = new JavaScriptSerializer().Deserialize<DematAccount>(input);
                dematAccount.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dematAccount.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dematAccount.ValidateInput())
                {
                    DematAccountResponse objResponse = new DematAccountResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DematAccountRequest dematAccountReq = new DematAccountRequest(dematAccount);
                DematAccountResponse dematAccountRes = dematAccountReq.GetDematAccountInfo();
                return dematAccountRes;
            }
            catch (Exception ex)
            {
                DematAccountResponse objResponse = new DematAccountResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}