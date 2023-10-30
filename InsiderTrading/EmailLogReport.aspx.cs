using ProcsDLL.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Web.Services;

namespace ProcsDLL.InsiderTrading
{
    public partial class EmailLogReport : System.Web.UI.Page
    {
        string userEmail;
        string emailSubject;
        string emailBody; 
        string emailAction;
        string emailAattachments;
        string emailDataElementId;

        private const string sessionKey = "EmailLogReport";
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


        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetModuleType();
                GetModuleSubType();                
            }
        }
        private void GetModuleType()
        {
            try
            {
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_MODULE_NAME"));

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 1)
                        {
                            DropDownListModule.Items.Add(new ListItem("All", "0"));
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sModuleName = !String.IsNullOrEmpty(Convert.ToString(dr["EMAIL_ACTION"])) ? Convert.ToString(dr["EMAIL_ACTION"]) : String.Empty;
                            DropDownListModule.Items.Add(new ListItem(sModuleName, sModuleName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {   
                HiddenShowModal.Value = string.Empty;
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_EMAIL_LOGS"));
                    sCmd.Parameters.Add(new SqlParameter("@EMAIL_ACTION", DropDownListModule.SelectedValue));
                    sCmd.Parameters.Add(new SqlParameter("@DATA_ELEMENT", ddlModuleSubType.Value));

                    if (!String.IsNullOrEmpty(Convert.ToString(txtFromDate.Value)))
                    {
                        sCmd.Parameters.Add(new SqlParameter("@FROM_DATE", ConvertDate(txtFromDate.Value)));
                    }
                    else
                    {
                        sCmd.Parameters.Add(new SqlParameter("@FROM_DATE", DBNull.Value));
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(txtToDate.Value)))
                    {
                        sCmd.Parameters.Add(new SqlParameter("@TO_DATE", ConvertDate(txtToDate.Value)));
                    }
                    else
                    {
                        sCmd.Parameters.Add(new SqlParameter("@TO_DATE", DBNull.Value));
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(ddlStatus.SelectedValue)))
                    {
                        sCmd.Parameters.Add(new SqlParameter("@Status", ddlStatus.SelectedValue));
                    }
                   
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        RepeaterTbl.DataSource = dt;
                        RepeaterTbl.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "InitializeDataTable", "initializeDataTable();", true);

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void DropDownListModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetModuleSubType();
        }
        protected void GetModuleSubType()
        {
            try
            {
                ddlModuleSubType.Items.Clear();
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_MODULE_SUB_TYPE"));
                    sCmd.Parameters.Add(new SqlParameter("@EMAIL_ACTION", DropDownListModule.SelectedValue));

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 1)
                        {
                            ddlModuleSubType.Items.Add(new ListItem("All", "0"));
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sModuleId = !String.IsNullOrEmpty(Convert.ToString(dr["DATA_ELEMENT_ID"])) ? Convert.ToString(dr["DATA_ELEMENT_ID"]) : String.Empty;
                            string sModuleName = !String.IsNullOrEmpty(Convert.ToString(dr["DATA_ELEMENT_NAME"])) ? Convert.ToString(dr["DATA_ELEMENT_NAME"]) : String.Empty;

                            ddlModuleSubType.Items.Add(new ListItem(sModuleName, sModuleId));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private DateTime ConvertDate(String date)
        {
            String str = String.Empty;
            try
            {
                if (date.Contains("/"))
                {
                    str = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }
            return Convert.ToDateTime(str);
        }
        protected void LinkButtonLogDetail_Click(object sender, EventArgs e)
        {
            try
            {
                HiddenShowModal.Value = "YES";
                LinkButton btn = (LinkButton)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;
                HiddenField LogId = (HiddenField)item.FindControl("HiddenFieldLogId");

                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_PROCS_INSIDER_EMAIL_LOG_REPORT";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@MODE", "GET_EMAIL_LOGS_BY_ID"));
                    sCmd.Parameters.Add(new SqlParameter("@LOG_ID", Convert.ToInt64(LogId.Value)));

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dvMsg.InnerHtml = dt.Rows[0]["EMAIL_MSG"].ToString();
                        foreach (DataRow drat in dt.Rows)
                        {
                            string[] separator = new string[] { "InsiderTrading" };
                            string filePth = Convert.ToString(drat["EMAIL_ATTACHMENT"]);
                            //string[] fileurl = filePth.Split(separator, StringSplitOptions.None);
                            //string newfileurl = Server.MapPath(filePth);//"../InsiderTrading/" + fileurl[1].Replace('\'', '/');

                            string relativePath = filePth.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace(@"\", "/").Replace("insidertrading/", "");
                            drat["EMAIL_ATTACHMENT"] = relativePath;
                        }
                        dt.AcceptChanges();
                        RepeaterAttachment.DataSource = dt;
                        RepeaterAttachment.DataBind();
                    }
                    else
                    {
                        dvMsg.InnerHtml = string.Empty;
                        RepeaterAttachment.DataSource = string.Empty;
                        RepeaterAttachment.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static string MapPathReverse(string fullServerPath)
        {
            return @"~\" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }


        public delegate void Run();
        protected void btnReSendMail_Click(object sender, EventArgs e)
        {
            try
            {                
                hdnEmailTask.Text = "Start";
                Progress = null;
                Progress.Add(new ProgressStep("Process Started", ProgressStatus.Started, "Process Started"));
                Run run = new Run(SendEmailLog);
                IAsyncResult res = run.BeginInvoke((IAsyncResult ar) =>
                {
                    Progress.Add(new ProgressStep("Process Completed", ProgressStatus.Completed, "All emails sent"));
                }, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){fnChkStatus();},0);", true);

            }

            catch (Exception ex)
            {
               // LabelMsg.Text = ex.ToString();
            }

        }


        //public void SendEmailLog()
        //{
        //    try
        //    {
        //        string companyId = Convert.ToString(Session["CompanyId"]);
        //        string MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
        //        string LoggedInUser = Convert.ToString(Session["EmployeeId"]);

        //        string selectedLogIdsStr = HiddenFieldLogIds.Value;
        //        if (!string.IsNullOrEmpty(selectedLogIdsStr))
        //        {
        //            string[] selectedLogIds = selectedLogIdsStr.Split(',');
        //            // Now, you can use the selectedLogIds array as needed
        //            foreach (string logId in selectedLogIds)
        //            {
        //                // Process each logId

        //                using (SqlConnection sCon = new SqlConnection(sConStr))
        //                {
        //                    sCon.Open();
        //                    // Declare the SQL query with a parameter
        //                    // string sqlQuery = "SELECT LOG_ID, EMAIL_TO, EMAIL_SUBJECT, EMAIL_MSG, EMAIL_ACTION, EMAIL_DT, DATA_ELEMENT_ID, EMAIL_STATUS, ERR_MSG, USER_LOGIN FROM PROCS_INSIDER_EMAIL_LOG WHERE LOG_ID = @LogId";
        //                    string sqlQuery = "SELECT A.LOG_ID, A.EMAIL_TO, A.EMAIL_SUBJECT, A.EMAIL_MSG, A.EMAIL_ACTION, A.EMAIL_DT, A.DATA_ELEMENT_ID,A.EMAIL_STATUS,A.ERR_MSG, A.USER_LOGIN,B.EMAIL_ATTACHMENT FROM PROCS_INSIDER_EMAIL_LOG(NOLOCK) A LEFT JOIN PROCS_INSIDER_EMAIL_LOG_ATTACHMENT(NOLOCK) B ON A.LOG_ID = B.LOG_ID  WHERE A.LOG_ID = @LogId";

        //                    SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
        //                    // Create a parameter for the logId variable
        //                    SqlParameter logIdParam = new SqlParameter("@LogId", SqlDbType.VarChar); // Adjust SqlDbType as per your data type
        //                    logIdParam.Value = logId; // Set the parameter value
        //                    cmd.Parameters.Add(logIdParam); // Add the parameter to the command

        //                    //SqlCommand cmd = new SqlCommand(" SELECT LOG_ID,EMAIL_TO,EMAIL_SUBJECT,EMAIL_MSG,EMAIL_ACTION,EMAIL_DT,DATA_ELEMENT_ID,EMAIL_STATUS,ERR_MSG	USER_LOGIN FROM PROCS_INSIDER_EMAIL_LOG", sCon);
        //                    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                    DataTable dt = new DataTable();
        //                    da.Fill(dt);
        //                    sCon.Close();
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        List<string> attachments = new List<string>();
        //                        foreach (DataRow dr in dt.Rows)
        //                        {
        //                            emailAattachments = Convert.ToString(dr["EMAIL_ATTACHMENT"]);
        //                            if (!string.IsNullOrEmpty(emailAattachments))
        //                            {
        //                                attachments.Add(emailAattachments);
        //                            }
        //                        }

        //                        foreach (DataRow dr in dt.Rows)
        //                        {
        //                            userEmail = Convert.ToString(dr["EMAIL_TO"]);
        //                            emailSubject = Convert.ToString(dr["EMAIL_SUBJECT"]);
        //                            emailBody = Convert.ToString(dr["EMAIL_MSG"]);
        //                            emailAction = Convert.ToString(dr["EMAIL_ACTION"]);
        //                            emailDataElementId = Convert.ToString(dr["DATA_ELEMENT_ID"]);

        //                            EmailSender.SendMail(userEmail, emailSubject, emailBody, attachments, emailAction, companyId, "", emailDataElementId, LoggedInUser);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public void SendEmailLog()
        {
            try
            {
                string companyId = Convert.ToString(Session["CompanyId"]);
                string MODULE_DATABASE = Convert.ToString(Session["ModuleDatabase"]);
                string LoggedInUser = Convert.ToString(Session["EmployeeId"]);

                string selectedLogIdsStr = HiddenFieldLogIds.Value;
                if (!string.IsNullOrEmpty(selectedLogIdsStr))
                {
                    string[] selectedLogIds = selectedLogIdsStr.Split(',');
                    // Now, you can use the selectedLogIds array as needed
                    foreach (string logId in selectedLogIds)
                    {
                        // Process each logId

                        using (SqlConnection sCon = new SqlConnection(sConStr))
                        {
                            sCon.Open();
                            // Declare the SQL query with a parameter
                            string sqlQuery = "SELECT A.LOG_ID, A.EMAIL_TO, A.EMAIL_SUBJECT, A.EMAIL_MSG, A.EMAIL_ACTION, A.EMAIL_DT, A.DATA_ELEMENT_ID, A.EMAIL_STATUS, A.ERR_MSG, A.USER_LOGIN, B.EMAIL_ATTACHMENT FROM PROCS_INSIDER_EMAIL_LOG(NOLOCK) A LEFT JOIN PROCS_INSIDER_EMAIL_LOG_ATTACHMENT(NOLOCK) B ON A.LOG_ID = B.LOG_ID WHERE A.LOG_ID = @LogId";

                            SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
                            SqlParameter logIdParam = new SqlParameter("@LogId", SqlDbType.VarChar);
                            logIdParam.Value = logId;
                            cmd.Parameters.Add(logIdParam);

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            sCon.Close();
                            if (dt.Rows.Count > 0)
                            {
                                Dictionary<string, List<string>> attachmentsByLogId = new Dictionary<string, List<string>>();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    string emailAattachments = Convert.ToString(dr["EMAIL_ATTACHMENT"]);
                                    string currentLogId = Convert.ToString(dr["LOG_ID"]);

                                    if (!string.IsNullOrEmpty(emailAattachments))
                                    {
                                        if (!attachmentsByLogId.ContainsKey(currentLogId))
                                        {
                                            attachmentsByLogId[currentLogId] = new List<string>();
                                        }

                                        attachmentsByLogId[currentLogId].Add(emailAattachments);
                                    }
                                }

                                foreach (var logIdWithAttachments in attachmentsByLogId)
                                {
                                    string currentLogId = logIdWithAttachments.Key;
                                    List<string> attachments = logIdWithAttachments.Value;

                                    DataRow firstRow = dt.Rows[0]; // You can choose any row, as the data should be the same for the same LOG_ID
                                    userEmail = Convert.ToString(firstRow["EMAIL_TO"]);
                                    emailSubject = Convert.ToString(firstRow["EMAIL_SUBJECT"]);
                                    emailBody = Convert.ToString(firstRow["EMAIL_MSG"]);
                                    emailAction = Convert.ToString(firstRow["EMAIL_ACTION"]);
                                    emailDataElementId = Convert.ToString(firstRow["DATA_ELEMENT_ID"]);

                                    EmailSender.SendMail(userEmail, emailSubject, emailBody, attachments, emailAction, companyId, "", emailDataElementId, LoggedInUser);
                                    Progress.Add(new ProgressStep(string.Format("Notified to {0}", userEmail), ProgressStatus.InProgress));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
            }
        }

        protected string GetFileExtension(string relativePath)
        {
            return Path.GetExtension(relativePath);
        }

        [WebMethod(EnableSession = true)]
        public static string CheckDownload()
        {
            return HttpContext.Current.Session[sessionKey] == null ? string.Empty : ((Progress)HttpContext.Current.Session[sessionKey]).ToString();
        }

    }
}
