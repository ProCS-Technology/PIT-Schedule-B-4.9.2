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
    public class EmailUpdationController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetAllEmailByBU")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "GetAllEmailByBU" })]
        public EmailUpdationResponse GetAllEmailByBU()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    EmailUpdationResponse objResponse = new EmailUpdationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                EmailUpdations email = new JavaScriptSerializer().Deserialize<EmailUpdations>(input);
                email.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                email.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                email.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!email.ValidateInput())
                {
                    EmailUpdationResponse objResponse = new EmailUpdationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                EmailUpdationRequest departmentList = new EmailUpdationRequest(email);
                EmailUpdationResponse gResGrpList = departmentList.ListEmail();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                EmailUpdationResponse objResponse = new EmailUpdationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateEmail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UpdateEmail" })]
        public EmailUpdationResponse UpdateEmail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    EmailUpdationResponse objResponse = new EmailUpdationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                EmailUpdations email = new JavaScriptSerializer().Deserialize<EmailUpdations>(input);
                email.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                email.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                email.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!email.ValidateInput())
                {
                    EmailUpdationResponse objResponse = new EmailUpdationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg; ;
                    return objResponse;
                }
                EmailUpdationRequest departmentList = new EmailUpdationRequest(email);
                EmailUpdationResponse gResGrpList = departmentList.UpdateEmail();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                EmailUpdationResponse objResponse = new EmailUpdationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}