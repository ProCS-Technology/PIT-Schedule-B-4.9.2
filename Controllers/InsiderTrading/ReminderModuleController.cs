using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/ReminderModule")]
    public class ReminderModuleController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [HttpPost]
        [Route("GetAllActivity")]
        public ReminderModuleResponse GetAllActivity()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderModuleResponse objResponse = new ReminderModuleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }

            ReminderModule rem = new ReminderModule();
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (!rem.ValidateInput())
            {
                ReminderModuleResponse objResponse = new ReminderModuleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = sXSSErrMsg;
                return objResponse;
            }
            ReminderModuleRequest remRequest = new ReminderModuleRequest(rem);
            ReminderModuleResponse remResponce = new ReminderModuleResponse();
            remResponce = remRequest.GetAllActivity();
            return remResponce;
        }
        [HttpPost]
        [Route("UpdateActivityById")]
        public ReminderModuleResponse UpdateActivityById()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderModuleResponse objResponse = new ReminderModuleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }

            string input;
            using (System.IO.StreamReader rs = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rs.ReadToEnd();
            }

            ReminderModule rem = new JavaScriptSerializer().Deserialize<ReminderModule>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (!rem.ValidateInput())
            {
                ReminderModuleResponse objResponse = new ReminderModuleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = sXSSErrMsg;
                return objResponse;
            }
            ReminderModuleRequest remRequest = new ReminderModuleRequest(rem);
            ReminderModuleResponse remResponce = new ReminderModuleResponse();
            remResponce = remRequest.UpdateActivityById();
            return remResponce;
        }
        [HttpPost]
        [Route("GetActivityById")]
        public ReminderModuleResponse GetActivityById()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ReminderModuleResponse objResponse = new ReminderModuleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }

            string input;
            using (System.IO.StreamReader rs = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rs.ReadToEnd();
            }

            ReminderModule rem = new JavaScriptSerializer().Deserialize<ReminderModule>(input);
            rem.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            rem.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            rem.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

            if (!rem.ValidateInput())
            {
                ReminderModuleResponse objResponse = new ReminderModuleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = sXSSErrMsg;
                return objResponse;
            }
            ReminderModuleRequest remRequest = new ReminderModuleRequest(rem);
            ReminderModuleResponse remResponce = new ReminderModuleResponse();
            remResponce = remRequest.GetActivityById();
            return remResponce;
        }
    }
}