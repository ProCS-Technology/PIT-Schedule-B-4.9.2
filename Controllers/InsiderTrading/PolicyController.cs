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
using System.Net.Http;
using ProcsDLL.Models.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using System.Net;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Policy")]
    public class PolicyController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetPolicy")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse GetPolicy()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Policy pol = new Policy();
                pol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pol.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest PolicyList = new PolicyRequest(pol);
                PolicyResponse gResPolList = PolicyList.GetPolicyList();
                return gResPolList;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllPolicyDocuments")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse GetAllPolicyDocuments()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Policy pol = new Policy();
                pol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!pol.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest PolicyList = new PolicyRequest(pol);
                PolicyResponse gResPolList = PolicyList.GetAllPolicyDocumentsList();
                return gResPolList;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeletePolicy")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse DeletePolicy()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                Policy pol = new JavaScriptSerializer().Deserialize<Policy>(input1);
                pol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pol.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest pReq1 = new PolicyRequest(pol);
                PolicyResponse pRes1 = pReq1.DeletePolicy();
                return pRes1;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SavePolicy")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse SavePolicy()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                String sFileSize = HttpContext.Current.Request.Form["FileSize"];
                Policy rel = new JavaScriptSerializer().Deserialize<Policy>(input);
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = String.Empty;
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        int cLength = file.ContentLength;

                        String ext = Path.GetExtension(file.FileName);
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "Policy_";
                        string fname;

                        string sContentTyp = file.ContentType;
                        if (sContentTyp.ToUpper() == "APPLICATION/PDF")
                        {
                            if (sFileSize != cLength.ToString())
                            {
                                PolicyResponse objResponse = new PolicyResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document is corrupt, please upload correct the one.";
                                return objResponse;
                            }
                            if (sNm.Contains("%00"))
                            {
                                PolicyResponse objResponse = new PolicyResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                                return objResponse;
                            }
                            if (ext.ToLower() == ".pdf")
                            {
                                fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                                sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/"), fname);
                                file.SaveAs(sSaveAs);
                            }
                            else
                            {
                                PolicyResponse objResponse = new PolicyResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Only pdf format is allowed in Policy Document";
                                return objResponse;
                            }
                        }
                        else
                        {
                            PolicyResponse objResponse = new PolicyResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Content type of the uploaded document does not matched with the permissible document";
                            return objResponse;
                        }
                        //if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        //{
                        //    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        //    fname = Path.GetFileNameWithoutExtension(testfiles[testfiles.Length - 1]) + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        //}
                        //else
                        //{
                        //    fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        //}
                        //sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/"), fname);
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/"));
                        }
                        String sSaveAs_Code_of_counduct = String.Empty;
                        sSaveAs_Code_of_counduct = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/"), "Code-Of-Conduct.pdf");
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/"));

                        }
                        file.SaveAs(sSaveAs_Code_of_counduct);
                        file.SaveAs(sSaveAs);
                        rel.DOCUMENT = fname;
                    }
                }
                else
                {
                    rel.DOCUMENT = String.Empty;
                }

                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest pReq = new PolicyRequest(rel);
                PolicyResponse pRes = pReq.SavePolicy();
                return pRes;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed because of system error !";
                return objResponse;
            }
        }


        [Route("GetPolicyFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public HttpResponseMessage GetPolicyFile(string PolicyId, string FileExtension)
        {
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string filenameX = "";
            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                sCon.Open();
                string sqlQuery = "SELECT DOCUMENT FROM PROCS_INSIDER_POLICY_MSTR_ARCHIVE(NOLOCK) WHERE ID=@PolicyId";
                SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
                cmd.Parameters.AddWithValue("@PolicyId", PolicyId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                sCon.Close();
                if (dt.Rows.Count > 0)
                {
                    filenameX = dt.Rows[0]["DOCUMENT"].ToString();
                }
                //string extension = Path.GetExtension(filenameX);
                //string EXT = FileExtension;
                //string basePath = "~/InsiderTrading/UPSI/"; // Base path to the directory
                //string filePath1 = HttpContext.Current.Server.MapPath(basePath + filenameX);

                string filePath = HttpContext.Current.Server.MapPath("~/assets/logos/Policy/" + filenameX);

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

    }
}