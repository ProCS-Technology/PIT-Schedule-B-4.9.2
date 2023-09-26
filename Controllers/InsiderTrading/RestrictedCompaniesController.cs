using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/RestrictedCompanies")]
    public class RestrictedCompaniesController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SaveRestrictedCompanies")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "RestrictedCompanies APIs" })]
        public RestrictedCompaniesResponse SaveRestrictedCompanies()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                System.Web.Script.Serialization.JavaScriptSerializer serializer1 = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<RestrictedCompanies> lstRestrictedCompanies = new List<RestrictedCompanies>();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                lstRestrictedCompanies = serializer1.Deserialize<List<RestrictedCompanies>>(input);
                RestrictedCompanies objRestrictedCompanies = new RestrictedCompanies();
                objRestrictedCompanies = lstRestrictedCompanies[0];
                objRestrictedCompanies.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objRestrictedCompanies.companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objRestrictedCompanies.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!objRestrictedCompanies.ValidateInput())
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                RestrictedCompaniesRequest resCompaniesRequest = new RestrictedCompaniesRequest(objRestrictedCompanies);
                RestrictedCompaniesResponse resCompaniesResponse = resCompaniesRequest.SaveRestrictedCompanies();
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("l9")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "RestrictedCompanies APIs" })]
        public RestrictedCompaniesResponse GetRestrictedCompanies()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                RestrictedCompanies objRestrictedCompanies = new JavaScriptSerializer().Deserialize<RestrictedCompanies>(input);
                objRestrictedCompanies.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objRestrictedCompanies.companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objRestrictedCompanies.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!objRestrictedCompanies.ValidateInput())
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                RestrictedCompaniesRequest resCompaniesRequest = new RestrictedCompaniesRequest(objRestrictedCompanies);
                RestrictedCompaniesResponse resCompaniesResponse = resCompaniesRequest.GetRestrictedCompaniesList();
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteRestrictedCompanies")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "RestrictedCompanies APIs" })]
        public RestrictedCompaniesResponse DeleteRestrictedCompanies()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                System.Web.Script.Serialization.JavaScriptSerializer serializer1 = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<RestrictedCompanies> lstRestrictedCompanies = new List<RestrictedCompanies>();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                lstRestrictedCompanies = serializer1.Deserialize<List<RestrictedCompanies>>(input);
                RestrictedCompanies objRestrictedCompanies = new RestrictedCompanies();
                objRestrictedCompanies = lstRestrictedCompanies[0];
                objRestrictedCompanies.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objRestrictedCompanies.companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objRestrictedCompanies.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!objRestrictedCompanies.ValidateInput())
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                RestrictedCompaniesRequest rescompaniesRequest = new RestrictedCompaniesRequest(objRestrictedCompanies);
                RestrictedCompaniesResponse resCompaniesResponse = rescompaniesRequest.DeleteRestrictedCompanies();
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateIsRestrictedCompanies")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "RestrictedCompanies APIs" })]
        public RestrictedCompaniesResponse UpdateIsRestrictedCompanies()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
         
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                
                RestrictedCompanies objRestrictedCompanies = new JavaScriptSerializer().Deserialize<RestrictedCompanies>(input);
                objRestrictedCompanies.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objRestrictedCompanies.companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objRestrictedCompanies.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!objRestrictedCompanies.ValidateInput())
                {
                    RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RestrictedCompaniesRequest resCompaniesRequest = new RestrictedCompaniesRequest(objRestrictedCompanies);
                RestrictedCompaniesResponse resCompaniesResponse = resCompaniesRequest.UpdateIsRestrictedCompanies();
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                RestrictedCompaniesResponse objResponse = new RestrictedCompaniesResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}