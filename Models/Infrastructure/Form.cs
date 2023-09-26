using ProcsDLL.Models.InsiderTrading.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.Web.SessionState;
using System.IO;
namespace ProcsDLL.Models.Infrastructure
{
    public class Form : IRequiresSessionState
    {
        public static List<EventBasedForm> GetAllFormEvents(Int32 companyId, string moduleDb, string adminDb, string loginId, string dataElementId, string eventType, string subEventType)
        {
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[1] = new SqlParameter("@MODE", "GET_ALL_FORM_EVENTS");
            parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[3] = new SqlParameter("@MAIN_EVENT", eventType);
            parameters[4] = new SqlParameter("@SUB_EVENT", subEventType);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);

            List<EventBasedForm> lstEventBasedForm = new List<EventBasedForm>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        EventBasedForm objEventBasedForm = new EventBasedForm();
                        objEventBasedForm.formId = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_ID"])) ? Convert.ToInt32(dr["FORM_ID"]) : 0;
                        objEventBasedForm.mainEvent = !String.IsNullOrEmpty(Convert.ToString(dr["MAIN_EVENT"])) ? Convert.ToString(dr["MAIN_EVENT"]) : String.Empty;
                        objEventBasedForm.subEvent = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_EVENT"])) ? Convert.ToString(dr["SUB_EVENT"]) : String.Empty;
                        objEventBasedForm.formName = !String.IsNullOrEmpty(Convert.ToString(dr["DISPLAY_NAME"])) ? Convert.ToString(dr["DISPLAY_NAME"]) : String.Empty;
                        objEventBasedForm.formOrientation = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_ORIENTATION"])) ? Convert.ToString(dr["FORM_ORIENTATION"]) : "Landscape";
                        objEventBasedForm.formTemplate = GetFormEventBasedTemplate(companyId, moduleDb, adminDb, loginId, objEventBasedForm.formId, dataElementId, eventType, subEventType);
                        lstEventBasedForm.Add(objEventBasedForm);
                    }
                }
            }
            return lstEventBasedForm;
        }
        public static List<EventBasedForm> GetAllEmailEvents(Int32 companyId, string moduleDb, string adminDb, string loginId, string dataElementId, string eventType, string subEventType)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[1] = new SqlParameter("@MODE", "GET_ALL_EMAIL_EVENTS");
            parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[3] = new SqlParameter("@MAIN_EVENT", eventType);
            parameters[4] = new SqlParameter("@SUB_EVENT", subEventType);
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);
            List<EventBasedForm> lstEventBasedForm = new List<EventBasedForm>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        EventBasedForm objEventBasedForm = new EventBasedForm();
                        objEventBasedForm.formId = !String.IsNullOrEmpty(Convert.ToString(dr["TEMPLATE_ID"])) ? Convert.ToInt32(dr["TEMPLATE_ID"]) : 0;
                        //objEventBasedForm.mainEvent = !String.IsNullOrEmpty(Convert.ToString(dr["MAIN_EVENT"])) ? Convert.ToString(dr["MAIN_EVENT"]) : String.Empty;
                        objEventBasedForm.subEvent = !String.IsNullOrEmpty(Convert.ToString(dr["CC_EMAILS"])) ? Convert.ToString(dr["CC_EMAILS"]) : String.Empty;
                        objEventBasedForm.formName = GetEmailEventBasedTemplate(companyId, moduleDb, adminDb, loginId, objEventBasedForm.formId, dataElementId, eventType, "Subject");
                        objEventBasedForm.formTemplate = GetEmailEventBasedTemplate(companyId, moduleDb, adminDb, loginId, objEventBasedForm.formId, dataElementId, eventType, "Body");
                        lstEventBasedForm.Add(objEventBasedForm);
                    }
                }
            }
            return lstEventBasedForm;
        }
        public static string GetFormEventBasedTemplate(Int32 companyId, string moduleDb, string adminDb, string loginId, Int32 formId, string dataElementId, string mainEvent, string subEvent)
        {
            string layoutTemplate = String.Empty;
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[1] = new SqlParameter("@USER_LOGIN", loginId);
            parameters[2] = new SqlParameter("@ADMIN_DB", adminDb);
            parameters[3] = new SqlParameter("@FORM_ID", formId);
            parameters[4] = new SqlParameter("@DataElementId", dataElementId);
            parameters[5] = new SqlParameter("@MAIN_EVENT", mainEvent);
            parameters[6] = new SqlParameter("@SUB_EVENT", subEvent);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_INSIDER_FORM", moduleDb, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        layoutTemplate = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_TEMPLATE"])) ? Convert.ToString(dr["FORM_TEMPLATE"]) : String.Empty;
                    }

                }
            }
            return layoutTemplate;
        }
        public static string GetEmailEventBasedTemplate(Int32 companyId, string moduleDb, string adminDb, string loginId, Int32 formId, string dataElementId, string mainEvent, string subEvent)
        {
            string layoutTemplate = String.Empty;
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[1] = new SqlParameter("@USER_LOGIN", loginId);
            parameters[2] = new SqlParameter("@ADMIN_DB", adminDb);
            parameters[3] = new SqlParameter("@FORM_ID", formId);
            parameters[4] = new SqlParameter("@DataElementId", dataElementId);
            parameters[5] = new SqlParameter("@MAIN_EVENT", mainEvent);
            parameters[6] = new SqlParameter("@SUB_EVENT", subEvent);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_INSIDER_EMAIL", moduleDb, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        layoutTemplate = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_TEMPLATE"])) ? Convert.ToString(dr["FORM_TEMPLATE"]) : String.Empty;
                    }

                }
            }
            return layoutTemplate;
        }
        public static string CreateDocFile(string htmlText, string fileNameTemp, string sOrientation, string downloadFileDir)
        {
            //String downloadFileDir = "/InsiderTrading/emailAttachment/";
            String fileName = String.Empty;

            //Creates a new Word document
            WordDocument wordDocument = new WordDocument();

            //Creates a section to the document
            IWSection section = wordDocument.AddSection();

            //Saves the Word document to disk in DOCX format
            fileName = Path.GetFileNameWithoutExtension(fileNameTemp) + ".docx";

            if (sOrientation.ToLower() == "landscape")
            {
                section.PageSetup.Orientation = PageOrientation.Landscape;
            }
            else
            {
                section.PageSetup.Orientation = PageOrientation.Portrait;
            }

            //Validates whether the string is in proper XHTML
            section.Body.IsValidXHTML(htmlText, wordDocument.XHTMLValidateOption);

            //Inserts HTML string to the document
            section.Body.InsertXHTML(htmlText);
            wordDocument.Save(Path.Combine(downloadFileDir, fileName));

            //Close Word document
            wordDocument.Close();
            return fileName;
        }
        public static bool ConvertDocToPDF(String docFileName, String pdfFileName, String filePath)
        {
            bool isStatus = false;
            try
            {
                //Loads an existing Word document
                WordDocument wordDocument = new WordDocument(Path.Combine(filePath, docFileName), Syncfusion.DocIO.FormatType.Docx);

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
                //pdfDocument.PageSettings.Size = PdfPageSize.A4;

                //Save the PDF file to file system                
                pdfDocument.Save(Path.Combine(filePath, pdfFileName));

                //close the instance of document objects
                pdfDocument.Close(true);

                wordDocument.Close();

                isStatus = true;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "PreClearanceRequestController", "ConvertDocToPDF", Convert.ToString(value: HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                isStatus = false;
            }
            return isStatus;
        }
    }
}