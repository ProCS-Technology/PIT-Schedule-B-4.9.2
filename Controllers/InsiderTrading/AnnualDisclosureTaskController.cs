using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Globalization;
using Swashbuckle.Swagger.Annotations;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class AnnualDisclosureTaskController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SaveAnnualDisclosureTask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "AnnualDisclosureTask API" })]
        public AnnualDisclosureTaskResponse SaveAnnualDisclosureTask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                AnnualDisclosureTask annualDisclosureTask = new JavaScriptSerializer().Deserialize<AnnualDisclosureTask>(input);
                annualDisclosureTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                annualDisclosureTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                annualDisclosureTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!annualDisclosureTask.ValidateInput())
                {
                    AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                AnnualDisclosureTaskRequest dReq = new AnnualDisclosureTaskRequest(annualDisclosureTask);
                AnnualDisclosureTaskResponse dRes = dReq.SaveAnnualDisclosureTask();
                return dRes;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAnnualDisclosureTaskInfo")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "AnnualDisclosureTask APIs" })]
        public AnnualDisclosureTaskResponse GetAnnualDisclosureTaskInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    AnnualDisclosureTaskResponse objSessionResponse = new AnnualDisclosureTaskResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                AnnualDisclosureTask annualDisclosureTask = new AnnualDisclosureTask();
                annualDisclosureTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                annualDisclosureTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                annualDisclosureTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!annualDisclosureTask.ValidateInput())
                {
                    AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                AnnualDisclosureTaskRequest getAnnualDisclosureTaskReq = new AnnualDisclosureTaskRequest(annualDisclosureTask);
                AnnualDisclosureTaskResponse getAnnualDisclosureTaskRes = getAnnualDisclosureTaskReq.GetAnnualDisclosureTaskClosureInfo();
                return getAnnualDisclosureTaskRes;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAnnualDisclosureTaskInfoList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "AnnualDisclosureTask APIs" })]
        public AnnualDisclosureTaskResponse GetAnnualDisclosureTaskClosureInfoList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    AnnualDisclosureTaskResponse objSessionResponse = new AnnualDisclosureTaskResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                AnnualDisclosureTask annualDisclosureTask = new AnnualDisclosureTask();
                annualDisclosureTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                annualDisclosureTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                annualDisclosureTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (!annualDisclosureTask.ValidateInput())
                {
                    AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                AnnualDisclosureTaskRequest getAnnualDisclosureTaskReq = new AnnualDisclosureTaskRequest(annualDisclosureTask);
                AnnualDisclosureTaskResponse getAnnualDisclosureTaskRes = getAnnualDisclosureTaskReq.GetAnnualDisclosureTaskClosureInfoList();
                return getAnnualDisclosureTaskRes;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteDepartment")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Department APIs" })]
        public AnnualDisclosureTaskResponse DeleteAnnualDisclosureTask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                AnnualDisclosureTask dept = new JavaScriptSerializer().Deserialize<AnnualDisclosureTask>(input);
                dept.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!dept.ValidateInput())
                {
                    AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                AnnualDisclosureTaskRequest dReq = new AnnualDisclosureTaskRequest(dept);
                AnnualDisclosureTaskResponse dRes = dReq.DeleteAnnualDisclosureTask();
                return dRes;
            }
            catch (Exception ex)
            {
                AnnualDisclosureTaskResponse objResponse = new AnnualDisclosureTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
