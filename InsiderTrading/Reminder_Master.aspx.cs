using System;
//Added by Jitendra
using ProcsDLL.Models.Infrastructure;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using ProcsDLL.Models.InsiderTrading.Model;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Web.Services;
using System.Globalization;                                                                                                                                                                                             
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Web.Script.Serialization;

namespace ProcsDLL.InsiderTrading
{
    public partial class Reminder_Master : System.Web.UI.Page
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        private const string sessionKey = "ReminderEmail";
        Reminder reminderX;
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
        protected void btnsend_reminders_Click(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    Response.Redirect("~/login.aspx", true);
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                reminderX = new Reminder();
                //Reminder reminder = new JavaScriptSerializer().Deserialize<Reminder>(input);
                reminderX.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                reminderX.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                reminderX.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                string EmailSubject = hdnEmailSubject.Text;
                reminderX.subject = EmailSubject;

                string[] lstUser = Convert.ToString(hdnUsers.Text).Split(new char[] { ',' });
                reminderX.lstUser = lstUser.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();

                string mailBody = hdnMailBody.Text;
                reminderX.mailBody = mailBody;
                
                string mailType = hdnMailType.Text;
                reminderX.mailType = mailType;

                hdnEmailTask.Text = "Start";
                Progress = null;
                Progress.Add(new ProgressStep("Process Started", ProgressStatus.Started, "Process Started"));
                Run run = new Run(SendReminder);
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

        [WebMethod(EnableSession = true)]
        public static string CheckDownload()
        {
            return HttpContext.Current.Session[sessionKey] == null ? string.Empty : ((Progress)HttpContext.Current.Session[sessionKey]).ToString();
        }

        public void SendReminder()
        {
            try
            {
                Reminder objReminder = reminderX;
                foreach (var userEmail in objReminder.lstUser)
                {
                    if (objReminder.mailType == "1" || objReminder.mailType == "2")
                    {
                        List<string> attachments = new List<string>();
                        
                        if (File.Exists(System.IO.Path.Combine(Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));
                        }
                        if (File.Exists(System.IO.Path.Combine(Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(Server.MapPath("~/InsiderTrading/CodeOfConduct/Insider Trading Code.pdf")));
                        }
                        if (File.Exists(System.IO.Path.Combine(Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf"))))
                        {
                            attachments.Add(System.IO.Path.Combine(Server.MapPath("~/InsiderTrading/UserManual/ComplianceTrainingModule.pdf")));
                        }

                        EmailSender.SendMail(
                            userEmail, objReminder.subject, objReminder.mailBody, attachments,
                            "Reminder module manual", objReminder.companyId.ToString(), "", userEmail, objReminder.LoggedInUser
                        );
                        Progress.Add(new ProgressStep(string.Format("Notified to {0}", userEmail), ProgressStatus.InProgress));
                    }
                    else
                    {
                        EmailSender.SendMail(
                            userEmail, objReminder.subject, objReminder.mailBody, null,
                            "Reminder module manual", objReminder.companyId.ToString(), "", userEmail, objReminder.LoggedInUser
                        );
                        Progress.Add(new ProgressStep(string.Format("Notified to {0}", userEmail), ProgressStatus.InProgress));
                    }
                }
            }
            catch (Exception ex)
            {
                //ReminderResponse objResponse = new ReminderResponse();
                //objResponse.StatusFl = false;
                //objResponse.Msg = ex.Message.ToString();
                //return objResponse;
            }
        }

        public ReminderResponse SendMailSetup()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ReminderResponse objResponse = new ReminderResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Reminder reminder = new JavaScriptSerializer().Deserialize<Reminder>(input);
                reminder.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                reminder.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                reminder.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!reminder.ValidateInput())
                {
                    ReminderResponse objResponse = new ReminderResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                ReminderRequest gReqReminderList = new ReminderRequest(reminder);
                ReminderResponse gResReminderList = gReqReminderList.SendMailSetup();
                return gResReminderList;
            }
            catch (Exception ex)
            {
                ReminderResponse objResponse = new ReminderResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

    }
}