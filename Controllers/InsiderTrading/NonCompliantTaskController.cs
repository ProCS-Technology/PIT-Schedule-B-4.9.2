using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/NonCompliantTask")]
    public class NonCompliantTaskController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetAllNonCompliant")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Non Compliant Task APIs" })]
        public NonCompliantTaskResponse GetAllNonCompliant()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                NonCompliantTask nonCompliantTask = new NonCompliantTask();
                nonCompliantTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                nonCompliantTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                nonCompliantTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!nonCompliantTask.ValidateInput())
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                NonCompliantTaskRequest getAllNonCompliantList = new NonCompliantTaskRequest(nonCompliantTask);
                NonCompliantTaskResponse gResNonCompliantList = getAllNonCompliantList.GetAllNonCompliantTask();
                return gResNonCompliantList;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("CloseNonCompliantTask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Non Compliant Task APIs" })]
        public NonCompliantTaskResponse CloseNonCompliantTask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                NonCompliantTask nonCompliantTask = new NonCompliantTask();
                String input = HttpContext.Current.Request.Form["Object"];
                nonCompliantTask = new JavaScriptSerializer().Deserialize<NonCompliantTask>(input);
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = "";
                    String userDir = "/InsiderTrading/TradingRequestDetails/";
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    String newFileName = String.Empty;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        String name = Path.GetFileNameWithoutExtension(file.FileName);
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            newFileName = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            newFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                        file.SaveAs(sSaveAs);
                        nonCompliantTask.brokerNote = newFileName;
                    }
                }

                nonCompliantTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                nonCompliantTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                nonCompliantTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!nonCompliantTask.ValidateInput())
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                NonCompliantTaskRequest closeTask = new NonCompliantTaskRequest(nonCompliantTask);
                NonCompliantTaskResponse gResCloseTask = closeTask.CloseNonCompliantTask();
                return gResCloseTask;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("RunNonCompliantFinder")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Non Compliant Task APIs" })]
        public NonCompliantTaskResponse RunNonCompliantFinder()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                NonCompliantTask nonCompliantTask = new NonCompliantTask();
                nonCompliantTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                nonCompliantTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                nonCompliantTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!nonCompliantTask.ValidateInput())
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                NonCompliantTaskRequest getRunNowCompliantFinder = new NonCompliantTaskRequest(nonCompliantTask);
                NonCompliantTaskResponse gResRunNowCompliantFinder = getRunNowCompliantFinder.RunNowCompliantFinder();
                return gResRunNowCompliantFinder;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendEmailForNC")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Non Compliant Task APIs" })]
        public NonCompliantTaskResponse SendEmailForNC()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                NonCompliantTask nonCompliantTask = new NonCompliantTask();
                nonCompliantTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                nonCompliantTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                nonCompliantTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!nonCompliantTask.ValidateInput())
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                NonCompliantTaskRequest getAllNonCompliantList = new NonCompliantTaskRequest(nonCompliantTask);
                NonCompliantTaskResponse gResNonCompliantList = getAllNonCompliantList.SendEmailForNC();
                return gResNonCompliantList;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}