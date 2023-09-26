using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Login.Modal;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ProcsDLL.Controllers.UserCompanyModuleListing
{
    [RoutePrefix("api/UserCompanyModuleListing")]
    public class UserCompanyModuleSelectionController : ApiController
    {
        [Route("SetSession")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Set Session APIs" })]
        public String SetSession()
        {
            try
            {
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UserAccess usraccess = new JavaScriptSerializer().Deserialize<UserAccess>(input);
                HttpContext.Current.Session["CompanyId"] = usraccess.CompanyId;
                HttpContext.Current.Session["CompanyName"] = usraccess.CompanyNm;
                HttpContext.Current.Session["ModuleId"] = usraccess.ModuleId;
                HttpContext.Current.Session["ModuleName"] = usraccess.ModuleNm;
                HttpContext.Current.Session["ModuleFolder"] = usraccess.ModuleFolder;
                HttpContext.Current.Session["ModuleDatabase"] = usraccess.ModuleDataBase;
                HttpContext.Current.Session["EmployeeId"] = usraccess.EmployeeId;
                HttpContext.Current.Session["SESSION_ID"] = System.Guid.NewGuid().ToString();
                return Convert.ToString(HttpContext.Current.Session["ModuleFolder"]);
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return String.Empty;
        }
    }
}
