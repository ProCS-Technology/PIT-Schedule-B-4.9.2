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
    [RoutePrefix("api/DesignationIT")]
    public class DesignationITController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetDesignationList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Designation APIs" })]
        public DesignationResponse GetDesignationList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DesignationResponse objResponse = new DesignationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Designation designation = new JavaScriptSerializer().Deserialize<Designation>(input);
                designation.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                designation.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                designation.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);


                if (!designation.ValidateInput())
                {
                    DesignationResponse objResponse = new DesignationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DesignationRequest gReqDesignationList = new DesignationRequest(designation);
                DesignationResponse gResDesignationList = gReqDesignationList.GetDesignationList();
                return gResDesignationList;
            }
            catch (Exception ex)
            {
                DesignationResponse objResponse = new DesignationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveDesignation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Designation APIs" })]
        public DesignationResponse SaveDesignation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DesignationResponse objResponse = new DesignationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Designation designation = new JavaScriptSerializer().Deserialize<Designation>(input);
                designation.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                designation.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                designation.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!designation.ValidateInput())
                {
                    DesignationResponse objResponse = new DesignationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                DesignationRequest gReqDesignationList = new DesignationRequest(designation);
                DesignationResponse gResDesignationList = gReqDesignationList.SaveDesignation();
                return gResDesignationList;
            }
            catch (Exception ex)
            {
                DesignationResponse objResponse = new DesignationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteDesignation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Designation APIs" })]
        public DesignationResponse DeleteDesignation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DesignationResponse objResponse = new DesignationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Designation designation = new JavaScriptSerializer().Deserialize<Designation>(input);
                designation.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                designation.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                designation.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!designation.ValidateInput())
                {
                    DesignationResponse objResponse = new DesignationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                DesignationRequest gReqDesignationList = new DesignationRequest(designation);
                DesignationResponse gResDesignationList = gReqDesignationList.DeleteDesignation();
                return gResDesignationList;
            }
            catch (Exception ex)
            {
                DesignationResponse objResponse = new DesignationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}