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
    [RoutePrefix("api/Location")]
    public class LocationController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetLocationList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Location APIs" })]
        public LocationResponse GetLocationList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                //Location location = new JavaScriptSerializer().Deserialize<Location>(input);
                Location location = new Location();
                location.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                location.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!location.ValidateInput())
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                LocationRequest locationList = new LocationRequest(location);
                LocationResponse gResLocationList = locationList.GetLocationList();
                return gResLocationList;
            }
            catch (Exception ex)
            {
                LocationResponse objResponse = new LocationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("EditLocation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Location APIs" })]
        public LocationResponse EditLocation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Location location = new JavaScriptSerializer().Deserialize<Location>(input);
                location.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                location.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!location.ValidateInput())
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                LocationRequest locationList = new LocationRequest(location);
                LocationResponse gResLocationList = locationList.EditLocation();
                return gResLocationList;
            }
            catch (Exception ex)
            {
                LocationResponse objResponse = new LocationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveLocation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Location APIs" })]
        public LocationResponse SaveLocation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Location location = new JavaScriptSerializer().Deserialize<Location>(input);
                location.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]); ;
                location.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                location.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!location.ValidateInput())
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                LocationRequest locationList = new LocationRequest(location);
                LocationResponse gResLocationList = locationList.SaveLocation();
                return gResLocationList;
            }
            catch (Exception ex)
            {
                LocationResponse objResponse = new LocationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteLocation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Location APIs" })]
        public LocationResponse DeleteLocation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Location location = new JavaScriptSerializer().Deserialize<Location>(input);
                location.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                location.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!location.ValidateInput())
                {
                    LocationResponse objResponse = new LocationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                LocationRequest locationList = new LocationRequest(location);
                LocationResponse gResLocationList = locationList.DeleteLocation();
                return gResLocationList;
            }
            catch (Exception ex)
            {
                LocationResponse objResponse = new LocationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}