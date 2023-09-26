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
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetDepartmentList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Department APIs" })]
        public DepartmentResponse GetDepartmentList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepartmentResponse objResponse = new DepartmentResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                
                Department dept = new JavaScriptSerializer().Deserialize<Department>(input);
                dept.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dept.ValidateInput())
                {
                    DepartmentResponse objResponse = new DepartmentResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                
                DepartmentRequest departmentList = new DepartmentRequest(dept);
                DepartmentResponse gResGrpList = departmentList.GetDepartmentList();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                DepartmentResponse objResponse = new DepartmentResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveDepartment")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Department APIs" })]
        public DepartmentResponse SaveDepartment()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepartmentResponse objResponse = new DepartmentResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                Department dept = new JavaScriptSerializer().Deserialize<Department>(input);
                dept.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dept.ValidateInput())
                {
                    DepartmentResponse objResponse = new DepartmentResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                DepartmentRequest dReq = new DepartmentRequest(dept);
                DepartmentResponse dRes = dReq.SaveDepartment();
                return dRes;
            }
            catch (Exception ex)
            {
                DepartmentResponse objResponse = new DepartmentResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteDepartment")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Department APIs" })]
        public DepartmentResponse DeleteDepartment()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepartmentResponse objResponse = new DepartmentResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                
                Department dept = new JavaScriptSerializer().Deserialize<Department>(input);
                dept.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dept.ValidateInput())
                {
                    DepartmentResponse objResponse = new DepartmentResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DepartmentRequest dReq = new DepartmentRequest(dept);
                DepartmentResponse dRes = dReq.DeleteDepartment();
                return dRes;
            }
            catch (Exception ex)
            {
                DepartmentResponse objResponse = new DepartmentResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}