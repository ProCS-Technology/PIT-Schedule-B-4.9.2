using Newtonsoft.Json;
using Syncfusion.EJ.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class PdfViewerITController : ApiController
    {
        [Route("Load")]
        [HttpPost]
        public object Load(Dictionary<string, string> jsonResult)
        {
            try
            {
                PdfViewerHelper helper = new PdfViewerHelper();
                if (jsonResult.ContainsKey("newFileName"))
                {
                    var name = jsonResult["newFileName"];
                    string fileName = name.Split(new string[] { "://" }, StringSplitOptions.None)[0];
                    if (fileName == "http" || fileName == "https")
                    {
                        var WebClient = new WebClient();
                        byte[] pdfDoc = WebClient.DownloadData(name);
                        helper.Load(pdfDoc);
                    }
                    else
                    {
                        if (name.Contains("/video/"))
                        {
                            helper.Load("~" + name);
                        }
                        else
                        {
                            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(name);
                            string path = HttpContext.Current.Server.MapPath("~/assets/logos/Policy/" + fileNameWithoutExt + ".pdf");
                            helper.Load(path);
                        }
                    }
                }
                else
                {
                    if (jsonResult.ContainsKey("isInitialLoading"))
                    {
                        if (jsonResult.ContainsKey("file"))
                        {
                            var name = jsonResult["file"];
                            string fileName = name.Split(new string[] { "://" }, StringSplitOptions.None)[0];
                            if (fileName == "http" || fileName == "https")
                            {
                                var WebClient = new WebClient();
                                byte[] pdfDoc = WebClient.DownloadData(name);
                                helper.Load(pdfDoc);
                            }
                            else
                            {
                                helper.Load("~" + name);
                            }
                        }
                        else
                        {
                            helper.Load(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/HTTP Succinctly.pdf"));
                        }
                    }
                }
                return JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult));
            }
            catch (Exception ex)
            {
                return Convert.ToString(ex.Message);
            }
        }
        [Route("GetDocument")]
        [HttpGet]
        public string GetDocument()
        {
            try
            {
                string localFile = Path.Combine(HttpContext.Current.Server.MapPath("Data"), Convert.ToString(HttpContext.Current.Session["AgendaDoc"]));
                string path = Convert.ToString(HttpContext.Current.Session["AgendaFolderNm"]) + Convert.ToString(HttpContext.Current.Session["AgendaDoc"]);
                var docBytes = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Data/" + Convert.ToString(HttpContext.Current.Session["AgendaDoc"])));
                string docBase64 = "data:application/pdf;base64," + Convert.ToBase64String(docBytes);
                return (docBase64);
            }
            catch (Exception ex)
            {
                return Convert.ToString(ex.Message);
            }
        }
        public object FileUpload(Dictionary<string, string> jsonResult)
        {
            try
            {
                PdfViewerHelper helper = new PdfViewerHelper();
                if (jsonResult.ContainsKey("uploadedFile"))
                {
                    var fileUrl = jsonResult["uploadedFile"];
                    byte[] byteArray = Convert.FromBase64String(fileUrl);
                    MemoryStream stream = new MemoryStream(byteArray);
                    helper.Load(stream);
                }
                return JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult));
            }
            catch (Exception ex)
            {
                return Convert.ToString(ex.Message);
            }
        }
        public object Download(Dictionary<string, string> jsonResult)
        {
            PdfViewerHelper helper = new PdfViewerHelper();
            return helper.GetDocumentData(jsonResult);
        }
        public void Unload()
        {
            PdfViewerHelper helper = new PdfViewerHelper();
            helper.Unload();
        }
    }
}