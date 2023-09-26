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
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetRoleList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Role APIs" })]
        public RoleResponse GetRoleList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Role rol = new JavaScriptSerializer().Deserialize<Role>(input);
                rol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rol.ValidateInput())
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RoleRequest RoleList = new RoleRequest(rol);
                RoleResponse gResRolList = RoleList.GetRoleList();
                return gResRolList;
            }
            catch (Exception ex)
            {
                RoleResponse objResponse = new RoleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetRoleListWithAdmin")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Role APIs" })]
        public RoleResponse GetRoleListWithAdmin()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Role rol = new JavaScriptSerializer().Deserialize<Role>(input);
                rol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rol.ValidateInput())
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RoleRequest RoleList = new RoleRequest(rol);
                RoleResponse gResRolList = RoleList.GetRoleListWithAdmin();
                return gResRolList;
            }
            catch (Exception ex)
            {
                RoleResponse objResponse = new RoleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveRole")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Role APIs" })]
        public RoleResponse SaveRole()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Role rol = new JavaScriptSerializer().Deserialize<Role>(input);
                rol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rol.ValidateInput())
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RoleRequest rReq = new RoleRequest(rol);
                RoleResponse rRes = rReq.SaveRole();
                return rRes;
            }
            catch (Exception ex)
            {
                RoleResponse objResponse = new RoleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteRole")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Role APIs" })]
        public RoleResponse DeleteRole()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                Role rol = new JavaScriptSerializer().Deserialize<Role>(input1);
                rol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rol.ValidateInput())
                {
                    RoleResponse objResponse = new RoleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RoleRequest rReq1 = new RoleRequest(rol);
                RoleResponse rRes1 = rReq1.DeleteRole();
                return rRes1;
            }
            catch (Exception ex)
            {
                RoleResponse objResponse = new RoleResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}