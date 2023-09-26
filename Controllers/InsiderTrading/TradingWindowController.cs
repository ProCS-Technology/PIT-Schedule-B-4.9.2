using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Linq;
using System.Configuration;
using System.Threading;
using ProcsDLL.Models.Infrastructure;
using System.Data.SqlClient;
using System.Data;

namespace ProcsDLL.Controllers.InsiderTrading
{
    public class TradingWindowController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        private const string sessionKey = "TradingWindow";
        InsiderTradingWindow insiderTradingWindowX;
        public Progress Progress
        {
            get
            {
                if (HttpContext.Current.Session[sessionKey] == null)
                {
                    HttpContext.Current.Session.Add(sessionKey, new Progress());
                }
                return HttpContext.Current.Session[sessionKey] as Progress;
            }
            set
            {
                if (HttpContext.Current.Session[sessionKey] == null)
                {
                    HttpContext.Current.Session.Add(sessionKey, new Progress());
                }
                HttpContext.Current.Session[sessionKey] = value;
            }
        }
        [Route("GetEmailTemplateForWindowClosure")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Trading Window API" })]
        public InsiderTradingWindowResponse GetEmailTemplateForWindowClosure(Int32 ClosureTypeId)
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                InsiderTradingWindow insiderTradingWindow = new InsiderTradingWindow();
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                insiderTradingWindow.windowClosureTypeId = ClosureTypeId;
                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.GetEmailTemplate();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveInsiderTradingWindow")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse SaveInsiderTradingWindow()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                InsiderTradingWindow insiderTradingWindow = new JavaScriptSerializer().Deserialize<InsiderTradingWindow>(input);
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.SaveInsiderTradingWindow();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetInsiderTradingWindowClosureInfo")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                InsiderTradingWindow insiderTradingWindow = new InsiderTradingWindow();
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.GetInsiderTradingWindowClosureInfo();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetInsiderTradingWindowClosureInfoList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfoList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }

                InsiderTradingWindow insiderTradingWindow = new InsiderTradingWindow();
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.GetInsiderTradingWindowClosureInfoList();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendEmailForTradingWindowClosure")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse SendEmailForTradingWindowClosure()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                insiderTradingWindowX = new InsiderTradingWindow();

                insiderTradingWindowX.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindowX.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindowX.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                HttpFileCollection files = HttpContext.Current.Request.Files;
                string TradingWindowId = HttpContext.Current.Request.Form["TradingWindowId"].ToString();
                string EmailTemplate = HttpContext.Current.Request.Unvalidated.Form["EmailTemplate"].ToString();
                string EmailSubject = HttpContext.Current.Request.Unvalidated.Form["EmailSubject"].ToString();
                string[] lstUser = Convert.ToString(HttpContext.Current.Request.Form["Users"]).Split(new char[] { ',' });
                string[] lstmailTo = Convert.ToString(HttpContext.Current.Request.Form["mailTo"]).Split(new char[] { ',' });
                //string relatives = HttpContext.Current.Request.Form["relatives"].ToString();
                //string CPs = HttpContext.Current.Request.Form["CPs"].ToString();

                //insiderTradingWindow.CPs = CPs;
                insiderTradingWindowX.EmailTemplate = EmailTemplate;
                insiderTradingWindowX.EmailSubject = EmailSubject;
                insiderTradingWindowX.id = Convert.ToInt32(TradingWindowId);
                insiderTradingWindowX.lstmailTo = lstmailTo.ToList();
                insiderTradingWindowX.lstUser = lstUser.ToList();

                var uploadPdf = "/insidertrading/TradingWindowDocumnet/";
                string ext = "";
                string name = "";
                string fileName = "";
                string sSaveAs = "";
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        ext = Path.GetExtension(file.FileName);
                        name = Path.GetFileName(file.FileName);
                        string withoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                        fileName = withoutExt + ext;
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadPdf), fileName);
                        file.SaveAs(sSaveAs);
                        insiderTradingWindowX.TradingWindowDocument = fileName;
                        insiderTradingWindowX.TradingWindowDocumentPath = sSaveAs;
                    }
                }
                //if (!insiderTradingWindow.ValidateInput())
                //{
                //    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "Invalid Input Format";   
                //    return objResponse;
                //}
                //InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindowX);
                //InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.SendEmailForTradingWindowClosure();

                Progress = null;
                Progress.Add(new ProgressStep("Process Started", ProgressStatus.InProgress));
                Run run = new Run(SendTradingWindowEmail);
                IAsyncResult res = run.BeginInvoke((IAsyncResult ar) =>
                {
                    Progress.Add(new ProgressStep("Download Completed", ProgressStatus.Completed, "000ABORT_CHECK000"));                    
                }, null);

                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = true;
                objResponse.Msg = "Success";
                return objResponse;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        public delegate void Run();
        public void SendTradingWindowEmail()
        {
            //InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindowX);
            //InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.SendEmailForTradingWindowClosure();
            try
            {
                InsiderTradingWindow objInsiderTradingWindow = insiderTradingWindowX;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_TRADING_WINDOW_CLOSURE_INFORMATION_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@ID", objInsiderTradingWindow.id);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objInsiderTradingWindow.windowClosureTypeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"]) : 0;
                        objInsiderTradingWindow.fromDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["FROM_DATE"].ToString()) : String.Empty;
                        objInsiderTradingWindow.toDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["TO_DATE"].ToString()) : String.Empty;
                        objInsiderTradingWindow.boardMeetingScheduledOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"].ToString()) : String.Empty;
                        objInsiderTradingWindow.quarterEndedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"].ToString()) : String.Empty;
                        objInsiderTradingWindow.remarks = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]) : String.Empty;
                    }
                }
                List<string> mailto = objInsiderTradingWindow.lstmailTo.Distinct().ToList();
                objInsiderTradingWindow.lstmailTo = mailto;
                List<string> users = objInsiderTradingWindow.lstUser.Distinct().ToList();
                objInsiderTradingWindow.lstUser = users;
                if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                {
                    oInsiderTradingWindow.StatusFl = true;
                    oInsiderTradingWindow.Msg = "Email sent successfully !";
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                }
                //return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                //return oInsiderTradingWindow;
            }
        }
        public string CheckEmailStatus()
        {
            return HttpContext.Current.Session[sessionKey] == null ? string.Empty : ((Progress)HttpContext.Current.Session[sessionKey]).ToString();
        }
        public Boolean SendEmailToAllInsiderForWindowClosure(InsiderTradingWindow objInsiderTradingWindow)
        {
            List<string> lstAttachments = new List<string>();
            if (objInsiderTradingWindow.TradingWindowDocumentPath != null)
            {
                lstAttachments.Add(objInsiderTradingWindow.TradingWindowDocumentPath);
            }

            //Progress = null;
            //var sEmployeeId = HttpContext.Current.Session["EmployeeId"];
            //var prog = HttpContext.Current.Session[sessionKey];
            //if (HttpContext.Current.Session[sessionKey] == null)
            //{
            //    HttpContext.Current.Session.Add(sessionKey, new ProgressStep("Process Started", ProgressStatus.InProgress));
            //}

            //Progress.Add(new ProgressStep("Process Started", ProgressStatus.InProgress));
            foreach (string sEmail in objInsiderTradingWindow.lstUser)
            {
                string subject = objInsiderTradingWindow.EmailSubject;// "TRADING WINDOW CLOSURE NOTICE";
                string body = objInsiderTradingWindow.EmailTemplate;

                if (sEmail != "")
                {
                    EmailSender.SendMail(
                        sEmail, subject, body, lstAttachments, "Trading Window Notification", objInsiderTradingWindow.companyId.ToString(),
                        "", objInsiderTradingWindow.id.ToString()
                    );
                    Progress.Add(new ProgressStep(string.Format("Sent mail to {0}", sEmail), ProgressStatus.InProgress));
                }
            }
            foreach (string sMailto in objInsiderTradingWindow.lstmailTo)
            {
                string subject = objInsiderTradingWindow.EmailSubject; //"TRADING WINDOW CLOSURE NOTICE";
                string body = objInsiderTradingWindow.EmailTemplate;
                if (sMailto != "")
                {
                    EmailSender.SendMail(
                        sMailto, subject, body, lstAttachments, "Trading Window Notification", objInsiderTradingWindow.companyId.ToString(),
                        "", objInsiderTradingWindow.id.ToString()
                    );
                    Progress.Add(new ProgressStep(string.Format("Sent mail to {0}", sMailto), ProgressStatus.InProgress));
                }
            }
            return true;
        }
    }
}