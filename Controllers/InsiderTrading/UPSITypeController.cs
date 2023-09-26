using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Swashbuckle.Swagger.Annotations;



namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UPSIType")]
    public class UPSITypeController : ApiController
    {
        [Route("GetUPSITypeList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UPSIType APIs" })]
        public UPSITypeResponse GetUPSITypeList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objSessionResponse = new UPSITypeResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                UPSITypeModel objUPSIType = new UPSITypeModel();
                
                objUPSIType.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                UPSITypeRequest gReqUPSITypeList = new UPSITypeRequest(objUPSIType);
                UPSITypeResponse gResUPSITypeList = gReqUPSITypeList.GetUPSITypeList();
                return gResUPSITypeList;
            }
            catch (Exception ex)
            {
                UPSITypeResponse objResponse = new UPSITypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("SaveUPSIType")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSIType APIs" })]
        public UPSITypeResponse SaveUPSIType()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSITypeModel UPSIType = new JavaScriptSerializer().Deserialize<UPSITypeModel>(input);
                UPSIType.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                UPSIType.UserName = HttpContext.Current.Session["EmployeeId"].ToString();

                if (UPSIType.ValidateInput())
                {
                    UPSITypeRequest gReqUPSITypeList = new UPSITypeRequest(UPSIType);
                    UPSITypeResponse gResUPSITypeList = gReqUPSITypeList.SaveUPSIType();
                    return gResUPSITypeList;
                }
                else
                {
                    UPSITypeResponse gResUPSITypeList = new UPSITypeResponse();
                    gResUPSITypeList.StatusFl = false;
                    gResUPSITypeList.Msg = "Invalid input format";
                    return gResUPSITypeList;
                }

            }
            catch (Exception ex)
            {
                UPSITypeResponse objResponse = new UPSITypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}