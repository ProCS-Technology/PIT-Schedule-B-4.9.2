using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace ProcsDLL.InsiderTrading
{
    public partial class PendingTaskReport : System.Web.UI.Page
    {
        private const string sessionKey = "TradingWindow";
        Models.InsiderTrading.Model.User objUserX;
        public Progress Progress
        {
            get
            {
                if (Session[sessionKey] == null)
                {
                    Session.Add(sessionKey, new Progress());
                }
                return Session[sessionKey] as Progress;
            }
            set
            {
                if (Session[sessionKey] == null)
                {
                    Session.Add(sessionKey, new Progress());
                }
                Session[sessionKey] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public delegate void Run();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    Response.Redirect("~/login.aspx", true);
                }

                objUserX = new Models.InsiderTrading.Model.User();
                objUserX.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUserX.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                //objUserX.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                HttpFileCollection files = HttpContext.Current.Request.Files;
                string TradingWindowId = hdnTradingWindowId.Text;//HttpContext.Current.Request.Form["TradingWindowId"].ToString();
                string EmailTemplate = hdnEmailTemplate.Text;//HttpContext.Current.Request.Unvalidated.Form["EmailTemplate"].ToString();
                string EmailSubject = hdnEmailSubject.Text;//HttpContext.Current.Request.Unvalidated.Form["EmailSubject"].ToString();
                //string[] lstUser = Convert.ToString(hdnUsers.Text).Split(new char[] { ';' });//Convert.ToString(HttpContext.Current.Request.Form["Users"]).Split(new char[] { ';' });
                string[] emailReport = Convert.ToString(hdnMailTo.Text).Split(new char[] { ',' });//Convert.ToString(HttpContext.Current.Request.Form["mailTo"]).Split(new char[] { ';' });

                objUserX.reportTemplate = EmailTemplate;
                objUserX.subjectReport = EmailSubject;
                // objUserX.id = Convert.ToInt32(TradingWindowId);
                objUserX.emailReportt = emailReport.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                //objUserX.lstUser = lstUser.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();

                //HttpContext.Current.Session["TotalCnt"] = objUserX.emailReportt.Count + objUserX.emailReportt.Count;
                HttpContext.Current.Session["TotalCnt"] = objUserX.emailReportt.Count;
                HttpContext.Current.Session["SentCnt"] = 0;

                //var uploadPdf = "/insidertrading/TradingWindowDocumnet/";
                //string ext = "";
                ////string name = "";
                //string fileName = "";
                //string sSaveAs = "";

                //if (txtAttachment.HasFile)
                //{
                //    HttpPostedFile file = txtAttachment.PostedFile;// files[i];
                //    ext = Path.GetExtension(file.FileName);
                //    name = Path.GetFileName(file.FileName);
                //    string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                //    if (!string.IsNullOrEmpty(name))
                //    {
                //        if (sNm.Contains("%00"))
                //        {
                //            hdnEmailTask.Text = "NullbyteFileError";
                //            return;
                //        }
                //        if (ext.ToLower() == ".pdf")
                //        {
                //            string withoutExt = Path.GetFileNameWithoutExtension(file.FileName);

                //            fileName = withoutExt + ext;

                //            String nameX = "TradingWindowDocument_";
                //            string newFileName = nameX + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                //            sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadPdf), newFileName);
                //            file.SaveAs(sSaveAs);
                //            insiderTradingWindowX.TradingWindowDocument = fileName;
                //            insiderTradingWindowX.TradingWindowDocumentPath = sSaveAs;
                //        }
                //        else
                //        {
                //            hdnEmailTask.Text = "FileError";
                //            return;
                //        }
                //    }
                //}

                hdnEmailTask.Text = "Start";
                Progress = null;
                Progress.Add(new ProgressStep("Process Started", ProgressStatus.Started, "Process Started"));
                Run run = new Run(SendTradingWindowEmail);
                IAsyncResult res = run.BeginInvoke((IAsyncResult ar) =>
                {
                    Progress.Add(new ProgressStep("Process Completed", ProgressStatus.Completed, "All emails sent"));
                }, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){fnChkStatus();},0);", true);
            }
            catch (Exception ex)
            {

            }
        }

        public void SendTradingWindowEmail()
        {
            //InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindowX);
            //InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.SendEmailForTradingWindowClosure();
            try
            {
                Models.InsiderTrading.Model.User objInsiderTradingWindow = objUserX;
                //SqlParameter[] parameters = new SqlParameter[4];
                //parameters[0] = new SqlParameter("@MODE", "GET_TRADING_WINDOW_CLOSURE_INFORMATION_BY_ID");
                //parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                //parameters[1].Direction = ParameterDirection.Output;
                //parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                ////parameters[3] = new SqlParameter("@ID", objInsiderTradingWindow.id);
                //DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                //if (ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        objInsiderTradingWindow.windowClosureTypeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"]) : 0;
                //        objInsiderTradingWindow.fromDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["FROM_DATE"].ToString()) : String.Empty;
                //        objInsiderTradingWindow.toDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["TO_DATE"].ToString()) : String.Empty;
                //        objInsiderTradingWindow.boardMeetingScheduledOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"].ToString()) : String.Empty;
                //        objInsiderTradingWindow.quarterEndedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"].ToString()) : String.Empty;
                //        objInsiderTradingWindow.remarks = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]) : String.Empty;
                //    }
                //}
                List<string> mailto = objInsiderTradingWindow.emailReportt.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                objInsiderTradingWindow.emailReportt = mailto;
                //List<string> users = objInsiderTradingWindow.lstUser.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                //objInsiderTradingWindow.lstUser = users;

                //List<string> lstAttachments = new List<string>();
                //if (objInsiderTradingWindow.lstAttachment != null)
                //{
                //    lstAttachments.Add(objInsiderTradingWindow.lstAttachment);
                //}
                //foreach (string sEmail in objInsiderTradingWindow.lstUser)
                //{
                //    string subject = objInsiderTradingWindow.EmailSubject;// "TRADING WINDOW CLOSURE NOTICE";
                //    string body = objInsiderTradingWindow.EmailTemplate;

                //    if (sEmail != "")
                //    {
                //        EmailSender.SendMail(
                //            sEmail, subject, body, lstAttachments, "Trading Window Notification", objInsiderTradingWindow.companyId.ToString(),
                //            "", objInsiderTradingWindow.id.ToString()
                //        );
                //        Progress.Add(new ProgressStep(string.Format("Notified to {0}", sEmail), ProgressStatus.InProgress));
                //    }
                //}
                //for (int x = 0; x < 8; x++)
                //{
                foreach (string sMailto in objInsiderTradingWindow.emailReportt)
                {
                    string subject = objInsiderTradingWindow.subjectReport; //"TRADING WINDOW CLOSURE NOTICE";
                    string body = objInsiderTradingWindow.reportTemplate;
                    if (sMailto != "")
                    {
                        EmailSender.SendMail(
                            sMailto, subject, body, objInsiderTradingWindow.lstAttachment, "Declaration Mail", objInsiderTradingWindow.companyId.ToString(),
                            "", objInsiderTradingWindow.LOGIN_ID, objInsiderTradingWindow.LOGIN_ID
                        );
                        Progress.Add(new ProgressStep(string.Format("Notified to {0}", sMailto), ProgressStatus.InProgress));
                    }
                }
                //}
            }
            catch (Exception ex)
            {
            }
        }
        [WebMethod(EnableSession = true)]
        public static string CheckDownload()
        {
            return HttpContext.Current.Session[sessionKey] == null ? string.Empty : ((Progress)HttpContext.Current.Session[sessionKey]).ToString();
        }
    }
}