using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net.Http;
using ProcsDLL.Models.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Net;

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



        [Route("CheckDownloadS")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "CheckDownloadS" })]
        public HttpResponseMessage CheckDownloadS(string LOG_ID, string FileExtension)
        {
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string filenameX = "";
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                sCon.Open();
                string sqlQuery = "SELECT LOG_ID,EMAIL_ATTACHMENT FROM PROCS_INSIDER_EMAIL_LOG_ATTACHMENT WHERE LOG_ID=@LOG_ID";
                SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
                cmd.Parameters.AddWithValue("@LOG_ID", LOG_ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                sCon.Close();
                if (dt.Rows.Count > 0)
                {
                    filenameX = dt.Rows[0]["EMAIL_ATTACHMENT"].ToString();
                }
                //string basePath = "~/InsiderTrading/emailAttachment/"; // Base path to the directory

                //string filePath1 = HttpContext.Current.Server.MapPath(basePath + filenameX);
                //string filePath = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + filenameX);
                byte[] fileBook = File.ReadAllBytes(filenameX);
                //byte[] fileBook = File.ReadAllBytes(filePath);
                MemoryStream stream = new MemoryStream();
                string excelBase64String = Convert.ToBase64String(fileBook);
                StreamWriter excelWriter = new StreamWriter(stream);
                excelWriter.Write(excelBase64String);
                excelWriter.Flush();
                stream.Position = 0;
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StreamContent(stream);
                if (FileExtension == ".pdf")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "PdfReport.pdf");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "PdfReport.pdf";
                }
                else if (FileExtension == ".txt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "TextFile.txt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "TextFile.txt";
                }
                else if (FileExtension == ".xlsx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                }
                else if (FileExtension == ".xls")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xls");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xls";
                }
                else if (FileExtension == ".doc")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.doc");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.doc";
                }
                else if (FileExtension == ".docx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.docx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.docx";
                }
                else if (FileExtension == ".png")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.png");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.png";
                }
                else if (FileExtension == ".jpeg")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.jpeg");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.jpeg";
                }
                else if (FileExtension == ".gif")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.gif");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.gif";
                }
                else if (FileExtension == ".zip")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.zip");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.zip";
                }
                else if (FileExtension == ".ppt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "File.ppt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.ppt";
                }
                else if (FileExtension == ".pptx")
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

    }
}