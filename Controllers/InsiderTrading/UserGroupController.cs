using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class UserGroupController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetUserGroupList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UserGroup APIs" })]
        public UserGroupResponse GetUserGroupList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserGroupResponse objResponse = new UserGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UserGroupResponse usrGrpRes;
                UserGroup usrGrp = new UserGroup();
                usrGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                usrGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                usrGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                usrGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                if (!usrGrp.ValidateInput())
                {
                    usrGrpRes = new UserGroupResponse();
                    usrGrpRes.StatusFl = false;
                    usrGrpRes.Msg = "Potential dangerous data posted!";
                    return usrGrpRes;
                }
                UserGroupRequest grpRequest = new UserGroupRequest(usrGrp);
                usrGrpRes = grpRequest.UserGroupList();
                return usrGrpRes;
            }
            catch (Exception ex)
            {
                UserGroupResponse objResponse = new UserGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUserGroup")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UserGroup APIs" })]
        public UserGroupResponse SaveUserGroup()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserGroupResponse objResponse = new UserGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    UserGroup userGrp = new JavaScriptSerializer().Deserialize<UserGroup>(input);
                    userGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    userGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    userGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    userGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UserGroupResponse UsrGrpRes = new UserGroupResponse();
                    if (!userGrp.ValidateInput())
                    {
                        UsrGrpRes = new UserGroupResponse();
                        UsrGrpRes.StatusFl = false;
                        UsrGrpRes.Msg = sXSSErrMsg;
                        return UsrGrpRes;
                    }
                    UserGroupRequest grpRequest = new UserGroupRequest(userGrp);
                    UsrGrpRes = grpRequest.SaveUserGroup();
                    return UsrGrpRes;
                }
                else
                {
                    UserGroupResponse objResponse = new UserGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserGroupResponse objResponse = new UserGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendUserGroupEmail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UserGroup APIs" })]
        public UserGroupResponse SendUserGroupEmail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserGroupResponse objResponse = new UserGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    UserGroup userGrp = new JavaScriptSerializer().Deserialize<UserGroup>(input);
                    userGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    userGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    userGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UserGroupResponse UsrGrpRes = new UserGroupResponse();
                    if (!userGrp.ValidateInput())
                    {
                        UsrGrpRes = new UserGroupResponse();
                        UsrGrpRes.StatusFl = false;
                        UsrGrpRes.Msg = sXSSErrMsg;
                        return UsrGrpRes;
                    }
                    UserGroupRequest grpRequest = new UserGroupRequest(userGrp);
                    UsrGrpRes = grpRequest.SendUserGroupEmail();
                    return UsrGrpRes;
                }
                else
                {
                    UserGroupResponse objResponse = new UserGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserGroupResponse objResponse = new UserGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserGroupSentMailList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UserGroup APIs" })]
        public UserGroupResponse GetUserGroupSentMailList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserGroupResponse objResponse = new UserGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UserGroupResponse usrGrpRes;
                UserGroup usrGrp = new UserGroup();
                usrGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                usrGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                usrGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (!usrGrp.ValidateInput())
                {
                    usrGrpRes = new UserGroupResponse();
                    usrGrpRes.StatusFl = false;
                    usrGrpRes.Msg = sXSSErrMsg;
                    return usrGrpRes;
                }
                UserGroupRequest grpRequest = new UserGroupRequest(usrGrp);
                usrGrpRes = grpRequest.UserGroupSentMailList();
                return usrGrpRes;
            }
            catch (Exception ex)
            {
                UserGroupResponse objResponse = new UserGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}