using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Presentation;
using Syncfusion.PresentationToPdfConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Training")]
    public class TrainingController : ApiController
    {
        [Route("GetTrainingModules")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetTrainingModules()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                TrainingModule trainingModule = new TrainingModule();
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetTrainingModules();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("GetTrainingReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetTrainingReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetTrainingReport();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("GetTrainingFileOnLoadToPdfViewer")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetTrainingFileOnLoadToPdfViewer()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetTrainingFileOnLoadToPdfViewer();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("GetTrainingFileOnNextButtonToPdfViewer")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetTrainingFileOnNextButtonToPdfViewer()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetTrainingFileOnNextButtonToPdfViewer();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("GetTrainingFileOnPreviousButtonToPdfViewer")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetTrainingFileOnPreviousButtonToPdfViewer()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetTrainingFileOnPreviousButtonToPdfViewer();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("OnSubmissionOfTrainingFile")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse OnSubmissionOfTrainingFile()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.OnSubmissionOfTrainingFile();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("GetAllTrainingModulesMaster")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetAllTrainingModulesMaster()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                TrainingModule trainingModule = new TrainingModule();
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetAllTrainingModulesMaster();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("SaveTrainingModule")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse SaveTrainingModule()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                List<TrainingAudioVideo> lstTrainingAudioVideo = new List<TrainingAudioVideo>();
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = "";
                    String userDir = "/InsiderTrading/emailAttachment/";

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + userDir)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + userDir));
                    }

                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    String newFileName = String.Empty;
                    String newFileNameWithoutExtension = String.Empty;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);

                        String name = Path.GetFileNameWithoutExtension(file.FileName);
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            newFileNameWithoutExtension = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                            newFileName = newFileNameWithoutExtension + ext;
                        }
                        else
                        {
                            newFileNameWithoutExtension = name + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                            newFileName = newFileNameWithoutExtension + ext;
                        }

                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                        file.SaveAs(sSaveAs);

                        switch (ext.Split('.')[1].ToUpper())
                        {
                            case "DOCX":
                            case "DOC":
                                ConvertWordToPdf(HttpContext.Current.Server.MapPath("~" + userDir), Path.GetFileName(newFileName));
                                break;
                            case "PPTX":
                                ConvertPPtToPdf(HttpContext.Current.Server.MapPath("~" + userDir), Path.GetFileName(newFileName));
                                break;
                            case "PDF":
                                break;
                            default:
                                TrainingAudioVideo selectedTrainingAudioVideo = trainingModule.lstTrainingAudioVideo.Where(p => Path.GetFileNameWithoutExtension(p.fileName) == name).FirstOrDefault();
                                TrainingAudioVideo trainingAudioVideo = new TrainingAudioVideo
                                {
                                    fileName = newFileName,
                                    sequence = selectedTrainingAudioVideo.sequence
                                };
                                lstTrainingAudioVideo.Add(trainingAudioVideo);
                                break;
                        }

                        trainingModule.noOfPages = ReadPageCount(HttpContext.Current.Server.MapPath("~" + userDir), (newFileNameWithoutExtension + ".pdf"));

                        trainingModule.trainingDocument = newFileNameWithoutExtension + ".pdf";
                    }
                }
                else
                {
                    trainingModule.trainingDocument = String.Empty;
                }

                if (lstTrainingAudioVideo != null)
                {
                    if (trainingModule.noOfPages == 0 && lstTrainingAudioVideo.Count > 0)
                    {
                        trainingModule.noOfPages = lstTrainingAudioVideo.Count;
                    }
                }


                trainingModule.lstTrainingAudioVideo = lstTrainingAudioVideo;
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.SaveTrainingModule();
                return trainingRes;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(TrainingController).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("GetAllTrainingModulesById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse GetAllTrainingModulesById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.GetAllTrainingModulesById();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        [Route("DeleteTrainingModule")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Training APIs" })]
        public TrainingResponse DeleteTrainingModule()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                TrainingModule trainingModule = new JavaScriptSerializer().Deserialize<TrainingModule>(input);
                trainingModule.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                trainingModule.userLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                trainingModule.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!trainingModule.ValidateInput())
                {
                    TrainingResponse objResponse = new TrainingResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                TrainingRequest trainingReq = new TrainingRequest(trainingModule);
                TrainingResponse trainingRes = trainingReq.DeleteTrainingModule();
                return trainingRes;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }

        private void ConvertWordToPdf(String filePath, String fileName)
        {
            try
            {
                //Loads an existing Word document
                WordDocument wordDocument = new WordDocument(Path.Combine(filePath, fileName), Syncfusion.DocIO.FormatType.Docx);

                //Initialize chart to image converter for converting charts during Word to pdf conversion
                wordDocument.ChartToImageConverter = new Syncfusion.OfficeChartToImageConverter.ChartToImageConverter();

                //Set the scaling mode for charts
                wordDocument.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Normal;

                //create an instance of DocToPDFConverter - responsible for Word to PDF conversion
                DocToPDFConverter converter = new DocToPDFConverter();

                //Set the image quality
                converter.Settings.ImageQuality = 100;

                //Set the image resolution
                converter.Settings.ImageResolution = 640;

                //Set true to optimize the memory usage for identical images
                converter.Settings.OptimizeIdenticalImages = true;

                //Convert Word document into PDF document
                PdfDocument pdfDocument = converter.ConvertToPDF(wordDocument);

                //Save the PDF file to file system
                String newName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
                pdfDocument.Save(Path.Combine(filePath, newName));

                //close the instance of document objects
                pdfDocument.Close(true);

                wordDocument.Close();
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "FilesToPDFConvertor", "ConvertWordToPdf", "FilesToPDFConvertor Scheduler", 1);
            }
        }

        private void ConvertPPtToPdf(String filePath, String fileName)
        {
            try
            {
                //Opens a PowerPoint Presentation
                IPresentation presentation = Presentation.Open(Path.Combine(filePath, fileName));

                //Creates an instance of ChartToImageConverter and assigns it to ChartToImageConverter property of Presentation
                presentation.ChartToImageConverter = new Syncfusion.OfficeChartToImageConverter.ChartToImageConverter();

                //Sets the scaling mode of the chart to best.
                presentation.ChartToImageConverter.ScalingMode = Syncfusion.OfficeChart.ScalingMode.Best;

                //Instantiates the Presentation to pdf converter settings instance.
                PresentationToPdfConverterSettings settings = new PresentationToPdfConverterSettings();

                //Set the image resolution
                settings.ImageResolution = 100;

                //Set the image quality
                settings.ImageQuality = 100;

                //Sets the option for adding hidden slides to converted pdf
                settings.ShowHiddenSlides = false;

                //Sets the slide per page settings; this is optional.
                //settings.SlidesPerPage = SlidesPerPage.One;

                //Sets the settings to enable notes pages while conversion.
                //settings.PublishOptions = PublishOptions.NotesPages;

                //Converts the PowerPoint Presentation into PDF document
                PdfDocument pdfDocument = PresentationToPdfConverter.Convert(presentation, settings);
                pdfDocument.PageSettings.Size = PdfPageSize.A4;
                pdfDocument.PageSettings.Margins.All = 0;
                pdfDocument.PageSettings.Orientation = PdfPageOrientation.Landscape;

                //Saves the PDF document
                String newName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
                pdfDocument.Save(Path.Combine(filePath, newName));

                //Closes the PDF document
                pdfDocument.Close(true);

                //Closes the Presentation
                presentation.Close();
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "FilesToPDFConvertor", "ConvertPPtToPdf", "FilesToPDFConvertor Scheduler", 1);
            }
        }

        private Int32 ReadPageCount(String filePath, String fileName)
        {
            Int32 pageCount = 0;
            try
            {
                //Load the PDF document.
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(Path.Combine(filePath, fileName));

                //Get the page count.
                pageCount = loadedDocument.Pages.Count;

                //Close the document.
                loadedDocument.Close(true);
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "FilesToPDFConvertor", "ReadPageCount", "FilesToPDFConvertor Scheduler", 1);
            }
            return pageCount;
        }

    }
}
