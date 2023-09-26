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
    [RoutePrefix("api/Relative")]
    public class RelativeController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetRelativeDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Relative APIs" })]
        public RelativeResponse GetRelativeDetail()
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
                Relative rel = new JavaScriptSerializer().Deserialize<Relative>(input);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    RelativeResponse objResponse = new RelativeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RelativeRequest relReq = new RelativeRequest(rel);
                RelativeResponse relRes = relReq.GetRelativeInformationById();
                return relRes;
            }
            catch (Exception ex)
            {
                RelativeResponse objResponse = new RelativeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
