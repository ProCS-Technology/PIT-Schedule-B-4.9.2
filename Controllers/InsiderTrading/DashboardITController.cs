using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/DashboardIT")]
    public class DashboardITController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetOpenDisclosureRequest")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetOpenDisclosureRequest()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.ADMIN_DATABASE = sAdminDb;
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetOpenDisclosureRequest();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfAllPreClearanceRequest")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfAllPreClearanceRequest()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetAllPreClearanceRequestCount();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfAllPreClearanceRequestForAllUser")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfAllPreClearanceRequestForAllUser()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetAllPreClearanceRequestCountForAllUser();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfAllTradeDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfAllTradeDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetCountOfAllTradeDetails();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SubmitEsopFormC")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public PreClearanceRequestResponse SubmitEsopFormC()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.SubmitEsopFormC();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCountOfMyTradeDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetCountOfMyTradeDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetCountOfMyTradeDetails();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTradeDetailsInfo")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetTradeDetailsInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.GetTradeDetailsInfo();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyActionable")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetMyActionable()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyActionable();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SubmitBrokerNoteRequestDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public PreClearanceRequestResponse SubmitBrokerNoteRequestDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                pClR.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
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
                        pClR.BrokerNote = newFileName;
                    }
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                //if (gReqPClR.ValidateTradeDateLiesInTradingWindowClosureBrokerNote())
                //{
                //    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Actual Transaction Date cannot be within the Trading Window Closure.";
                //    return objResponse;
                //}
                if (gReqPClR.ValidateTradeDateFallsInHolidayListBrokerNote())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Actual Transaction Date Date cannot fall in market closed date.";
                    return objResponse;
                }
                PreClearanceRequestResponse gResPClR = gReqPClR.AddBrokerNoteWithNoPC();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateTradeBifurcation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse UpdateTradeBifurcation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Dashboard dashboard = new JavaScriptSerializer().Deserialize<Dashboard>(input);
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest dashboardRequest = new DashboardRequest(dashboard);
                DashboardResponse dashboardResponse = dashboardRequest.UpdateTradeBifurcation();
                return dashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyUPSITask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetMyUPSITask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyUPSITask();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyUPSITaskById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse GetMyUPSITaskById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyUPSITaskById();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }


        //Added by Jiten
        [Route("GetAttachmentFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public HttpResponseMessage GetAttachmentFile(string TaskId, string FileExtension)
        {
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string filenameX = "";
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                sCon.Open();
                string sqlQuery = "SELECT TASK_ID,ATTECHMENT FROM UPSI_GROUP_MEMBER_CO_TASK_ATTACHMENT(NOLOCK) WHERE TASK_ID=@TASK_ID";
                SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
                cmd.Parameters.AddWithValue("@TASK_ID", TaskId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                sCon.Close();
                if (dt.Rows.Count > 0)
                {
                    filenameX = dt.Rows[0]["ATTECHMENT"].ToString();
                }
                //string extension = Path.GetExtension(filenameX);
                //string EXT = FileExtension;
                //string basePath = "~/InsiderTrading/UPSI/"; // Base path to the directory
                //string filePath1 = HttpContext.Current.Server.MapPath(basePath + filenameX);

                string filePath = HttpContext.Current.Server.MapPath("~/InsiderTrading/UPSI/" + filenameX);

                byte[] fileBook = File.ReadAllBytes(filePath);
                MemoryStream stream = new MemoryStream();
                string excelBase64String = Convert.ToBase64String(fileBook);
                StreamWriter excelWriter = new StreamWriter(stream);
                excelWriter.Write(excelBase64String);
                excelWriter.Flush();
                stream.Position = 0;
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StreamContent(stream);
                if (FileExtension == "pdf")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "PdfReport.pdf");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "PdfReport.pdf";
                }
                else if (FileExtension == "txt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "TextFile.txt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "TextFile.txt";
                }
                else if (FileExtension == "xlsx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                }
                else if (FileExtension == "xls")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xls");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xls";
                }
                else if (FileExtension == "doc")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.doc");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.doc";
                }
                else if (FileExtension == "docx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.docx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.docx";
                }
                else if (FileExtension == "png")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.png");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.png";
                }
                else if (FileExtension == "jpeg")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.jpeg");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.jpeg";
                }
                else if (FileExtension == "gif")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.gif");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.gif";
                }
                else if (FileExtension == "zip")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.zip");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.zip";
                }
                else if (FileExtension == "ppt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "File.ppt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.ppt";
                }
                else if (FileExtension == "pptx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "File.pptx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.pptx";
                }

                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;

            }
        }


        [Route("UpdateUPSITaskById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public DashboardResponse UpdateUPSITaskById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                Dashboard dashboard = new Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                //dashboard.loginId = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                var aa = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                dashboard.UPSITask.TaskId = Convert.ToString(HttpContext.Current.Request.Form["TaskId"]);
                dashboard.UPSITask.Group_id = Convert.ToString(HttpContext.Current.Request.Form["GROUP_ID"]);
                dashboard.UPSITask.Status = Convert.ToString(HttpContext.Current.Request.Form["Status"]);

                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.UpdateUPSITaskById();
                return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SubmitNcBrokerNoteRequestDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Dashboard APIs" })]
        public PreClearanceRequestResponse SubmitNcBrokerNoteRequestDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                PreClearanceRequest pClR = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
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
                        pClR.BrokerNote = newFileName;
                    }
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                if (gReqPClR.ValidateTradeDateLiesInTradingWindowClosureBrokerNote())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Actual Transaction Date cannot be within the Trading Window Closure.";
                    return objResponse;
                }
                if (gReqPClR.ValidateTradeDateFallsInHolidayListBrokerNote())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Actual Transaction Date Date cannot fall in market closed date.";
                    return objResponse;
                }
                PreClearanceRequestResponse gResPClR = gReqPClR.AddNonComplianceBrokerNote();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateNonComplianceTask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public PreClearanceRequestResponse UpdateNonComplianceTask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objSessionResponse = new PreClearanceRequestResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest ObjnonCompliance = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                ObjnonCompliance.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!ObjnonCompliance.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest objReq = new PreClearanceRequestRequest(ObjnonCompliance);
                PreClearanceRequestResponse objRes = objReq.UpdateNonComplianceTask();
                return objRes;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        
        [Route("UpdateTransactionHistory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public PreClearanceRequestResponse UpdateTransactionHistory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objSessionResponse = new PreClearanceRequestResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PreClearanceRequest ObjnonCompliance = new JavaScriptSerializer().Deserialize<PreClearanceRequest>(input);
                ObjnonCompliance.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!ObjnonCompliance.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest objReq = new PreClearanceRequestRequest(ObjnonCompliance);
                PreClearanceRequestResponse objRes = objReq.UpdateTransactionHistory();
                return objRes;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
