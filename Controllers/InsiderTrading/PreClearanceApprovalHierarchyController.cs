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
    [RoutePrefix("api/PreClearanceApprovalHierarchy")]
    public class PreClearanceApprovalHierarchyController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SaveOfficerHierarchyOrder")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearance Approval Hierarchy APIs" })]
        public PreClearanceApprovalHierarchyResponse SaveOfficerHierarchyOrder()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceApprovalHierarchyResponse objSessionResponse = new PreClearanceApprovalHierarchyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceApprovalHierarchy obj = new JavaScriptSerializer().Deserialize<PreClearanceApprovalHierarchy>(input);
                obj.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                obj.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                obj.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!obj.ValidateInput())
                {
                    PreClearanceApprovalHierarchyResponse objSessionResponse = new PreClearanceApprovalHierarchyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = sXSSErrMsg;
                    return objSessionResponse;
                }
                PreClearanceApprovalHierarchyRequest objRequest = new PreClearanceApprovalHierarchyRequest(obj);
                PreClearanceApprovalHierarchyResponse objResponse = objRequest.SaveOfficerHierarchyOrder();
                return objResponse;
            }
            catch (Exception ex)
            {
                PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllOfficerHierarchyOrder")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearance Approval Hierarchy APIs" })]
        public PreClearanceApprovalHierarchyResponse GetAllOfficerHierarchyOrder()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceApprovalHierarchyResponse objSessionResponse = new PreClearanceApprovalHierarchyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                PreClearanceApprovalHierarchy obj = new PreClearanceApprovalHierarchy();
                obj.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                obj.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                obj.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!obj.ValidateInput())
                {
                    PreClearanceApprovalHierarchyResponse objSessionResponse = new PreClearanceApprovalHierarchyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = sXSSErrMsg;
                    return objSessionResponse;
                }
                PreClearanceApprovalHierarchyRequest objRequest = new PreClearanceApprovalHierarchyRequest(obj);
                PreClearanceApprovalHierarchyResponse objResponse = objRequest.GetAllOfficerHierarchyOrder();
                return objResponse;
            }
            catch (Exception ex)
            {
                PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteOfficerHierarchyOrder")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearance Approval Hierarchy APIs" })]
        public PreClearanceApprovalHierarchyResponse DeleteOfficerHierarchyOrder()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceApprovalHierarchyResponse objSessionResponse = new PreClearanceApprovalHierarchyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceApprovalHierarchy obj = new JavaScriptSerializer().Deserialize<PreClearanceApprovalHierarchy>(input);
                obj.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                obj.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                obj.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!obj.ValidateInput())
                {
                    PreClearanceApprovalHierarchyResponse objSessionResponse = new PreClearanceApprovalHierarchyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = sXSSErrMsg;
                    return objSessionResponse;
                }
                PreClearanceApprovalHierarchyRequest objRequest = new PreClearanceApprovalHierarchyRequest(obj);
                PreClearanceApprovalHierarchyResponse objResponse = objRequest.DeleteOfficerHierarchyOrder();
                return objResponse;
            }
            catch (Exception ex)
            {
                PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}