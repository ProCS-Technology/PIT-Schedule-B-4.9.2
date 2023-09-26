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
    [RoutePrefix("api/Physical_share_mastr")]
    public class Physical_share_mastrController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("save_share")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Physical_share_mastr APIs" })]
        public PhysicalShareResponce save_share()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PhysicalShareMaster dept = new JavaScriptSerializer().Deserialize<PhysicalShareMaster>(input);
                dept.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!dept.ValidateInput())
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                PhysicalShareRequest physhre = new PhysicalShareRequest(dept);
                PhysicalShareResponce gResGrpList = physhre.SavePhysicalShare();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce objResponse = new PhysicalShareResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("viewallphysicalshare")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Physical_share_mastr APIs" })]
        public PhysicalShareResponce viewallphysicalshare()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PhysicalShareMaster dept = new JavaScriptSerializer().Deserialize<PhysicalShareMaster>(input);
                dept.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!dept.ValidateInput())
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                PhysicalShareRequest physhre = new PhysicalShareRequest(dept);
                PhysicalShareResponce gResGrpList = physhre.viewPhysicalShare();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce objResponse = new PhysicalShareResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("editphysicalshare")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Physical_share_mastr APIs" })]
        public PhysicalShareResponce editphysicalshare()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PhysicalShareMaster dept = new JavaScriptSerializer().Deserialize<PhysicalShareMaster>(input);
                dept.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!dept.ValidateInput())
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PhysicalShareRequest physhre = new PhysicalShareRequest(dept);
                PhysicalShareResponce gResGrpList = physhre.editPhysicalShare();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce objResponse = new PhysicalShareResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("deletephysicalshare")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Physical_share_mastr APIs" })]
        public PhysicalShareResponce deletephysicalshare()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PhysicalShareMaster dept = new JavaScriptSerializer().Deserialize<PhysicalShareMaster>(input);
                dept.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dept.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dept.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!dept.ValidateInput())
                {
                    PhysicalShareResponce objResponse = new PhysicalShareResponce();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PhysicalShareRequest physhre = new PhysicalShareRequest(dept);
                PhysicalShareResponce gResGrpList = physhre.DeletePhysicalShare();
                return gResGrpList;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce objResponse = new PhysicalShareResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}