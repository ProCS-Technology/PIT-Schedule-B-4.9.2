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
    [RoutePrefix("api/Reminder")]

    public class ReminderController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SendReminder")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "SendReminder" })]
        public ReminderResponse SendReminder()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderResponse objResponse = new ReminderResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Reminder reminder = new JavaScriptSerializer().Deserialize<Reminder>(input);
                reminder.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                reminder.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                reminder.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!reminder.ValidateInput())
                {
                    ReminderResponse objResponse = new ReminderResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReminderRequest gReqReminderList = new ReminderRequest(reminder);
                ReminderResponse gResReminderList = gReqReminderList.SendReminder();
                return gResReminderList;
            }
            catch (Exception ex)
            {
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendMailSetup")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "SendMailSetup" })]
        public ReminderResponse SendMailSetup()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderResponse objResponse = new ReminderResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Reminder reminder = new JavaScriptSerializer().Deserialize<Reminder>(input);
                reminder.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                reminder.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                reminder.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!reminder.ValidateInput())
                {
                    ReminderResponse objResponse = new ReminderResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReminderRequest gReqReminderList = new ReminderRequest(reminder);
                ReminderResponse gResReminderList = gReqReminderList.SendMailSetup();
                return gResReminderList;
            }
            catch (Exception ex)
            {
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}