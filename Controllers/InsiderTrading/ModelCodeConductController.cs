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
    [RoutePrefix("api/ModelCodeConduct")]
    public class ModelCodeConductController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetModelCodeConductList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ModelCodeConduct APIs" })]
        public ModelCodeConductResponse GetModelCodeConductList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ModelCodeConductResponse objResponse = new ModelCodeConductResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                ModelCodeConduct modelCodeConduct = new JavaScriptSerializer().Deserialize<ModelCodeConduct>(input);
                modelCodeConduct.CREATE_BY = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"]);
                modelCodeConduct.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                modelCodeConduct.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!modelCodeConduct.ValidateInput())
                {
                    ModelCodeConductResponse objResponse = new ModelCodeConductResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ModelCodeConductRequest gReqModelCodeConductList = new ModelCodeConductRequest(modelCodeConduct);
                ModelCodeConductResponse gResModelCodeConductList = gReqModelCodeConductList.GetModelCodeConductList();
                return gResModelCodeConductList;
            }
            catch (Exception ex)
            {
                ModelCodeConductResponse objResponse = new ModelCodeConductResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveModelCodeConduct")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ModelCodeConduct APIs" })]
        public ModelCodeConductResponse SaveModelCodeConduct()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ModelCodeConductResponse objResponse = new ModelCodeConductResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ModelCodeConduct modelCodeConduct = new JavaScriptSerializer().Deserialize<ModelCodeConduct>(input);
                modelCodeConduct.CREATE_BY = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"]);
                modelCodeConduct.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                modelCodeConduct.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!modelCodeConduct.ValidateInput())
                {
                    ModelCodeConductResponse objResponse = new ModelCodeConductResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ModelCodeConductRequest modelCodeConductReq = new ModelCodeConductRequest(modelCodeConduct);
                ModelCodeConductResponse modelCodeConductRes = modelCodeConductReq.SaveModelCodeConduct();
                return modelCodeConductRes;
            }
            catch (Exception ex)
            {
                ModelCodeConductResponse objResponse = new ModelCodeConductResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}