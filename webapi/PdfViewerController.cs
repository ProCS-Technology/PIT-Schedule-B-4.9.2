//using Newtonsoft.Json;
using Newtonsoft.Json;
using Syncfusion.EJ.PdfViewer;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace PdfViewerAPI
{
    public class PdfViewerController : ApiController
    {
        PdfViewerHelper helper = new PdfViewerHelper();
        public object Load(Dictionary<string, string> jsonResult)
        {
            helper = new PdfViewerHelper();
            helper.Load("E:/ProCS/BoardMeeting/Documents/AgendaFiles/Meeting_2/DownloadPdfFiles/BoardBook_2019 08 13 09 34 29 689.pdf");
            object output = helper.ProcessPdf(jsonResult);
            return JsonConvert.SerializeObject(output);

            //////load the multiple document from client side 
            ////if (jsonResult.ContainsKey("newFileName"))
            ////{
            ////    var name = jsonResult["newFileName"];
            ////    var pdfName = name.ToString();
            ////    helper.Load(HttpContext.Current.Server.MapPath("~/Data/" +pdfName));
            ////}
            ////else
            ////{
            ////    if (jsonResult.ContainsKey("isInitialLoading"))
            ////    {
            ////        if (jsonResult.ContainsKey("file"))
            ////        {
            ////            var name = jsonResult["file"];
            ////            helper.Load(name);
            ////        }
            ////        else
            ////        {
            ////            helper.Load(HttpContext.Current.Server.MapPath("~/Data/FormFillingDocument.pdf"));
            ////        }
            ////    } 
            ////}
            ////string output = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult));
            ////return output;            
        }
        public object FileUpload(Dictionary<string, string> jsonResult)
        {
            if (jsonResult.ContainsKey("uploadedFile"))
            {
                var fileurl = jsonResult["uploadedFile"];
                byte[] byteArray = Convert.FromBase64String(fileurl);
                MemoryStream stream = new MemoryStream(byteArray);
                helper.Load(stream);
            }
            string output = JsonConvert.SerializeObject(helper.ProcessPdf(jsonResult));
            return output;
        }
        public object Download(Dictionary<string, string> jsonResult)
        {
            var newdata = helper.GetDocumentData(jsonResult);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(helper.DocumentStream);
            PdfLoadedForm loadedForm = loadedDocument.Form;
            if (loadedForm != null)
            {
                DirectoryInfo dtInfo = new DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath);
                if (!Directory.Exists(dtInfo + "/output"))
                    Directory.CreateDirectory(dtInfo + "/output");
                loadedDocument.Save(dtInfo + "/output/FormDocument.pdf");
                loadedDocument.Close(true);
            }
            return null;
        }
    }
}