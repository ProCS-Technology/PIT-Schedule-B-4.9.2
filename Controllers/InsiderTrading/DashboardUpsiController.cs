using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/DashboardUpsi")]
    public class DashboardUpsiController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetUpsiCount")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardUpsiResponse GetUpsiCount()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardUpsiResponse objResponse = new DashboardUpsiResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                DashboardUpsi dashboardUpsi = new JavaScriptSerializer().Deserialize<DashboardUpsi>(input);
                dashboardUpsi.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboardUpsi.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboardUpsi.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboardUpsi.ValidateInput())
                {
                    DashboardUpsiResponse objResponse = new DashboardUpsiResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardUpsiRequest dashboardRequest = new DashboardUpsiRequest(dashboardUpsi);
                DashboardUpsiResponse dashboardResponse = dashboardRequest.GetUpsiCount();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardUpsiResponse objResponse = new DashboardUpsiResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}