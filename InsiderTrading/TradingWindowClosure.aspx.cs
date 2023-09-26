using ProcsDLL.Models.Infrastructure;
using System;
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
namespace ProcsDLL.InsiderTrading
{
    public partial class TradingWindowClosure : System.Web.UI.Page
    {
        private const string sessionKey = "TradingWindow";
        InsiderTradingWindow insiderTradingWindowX;
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
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])) && !String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
                {
                    fnGetAllWindowClosureType();
                    fnGetUserList();
                    BindRelative();
                    BindRoleData();
                    BindCP();

                }
            }
        }
        private void fnGetAllWindowClosureType()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    string sql = "SELECT A.WINDOW_CLOSURE_TYPE_ID,A.WINDOW_CLOSURE_TYPE FROM WINDOW_CLOSURE_TYPE_MASTER(NOLOCK) A";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dtAccess = new DataTable();
                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                        daAccess.Fill(dtAccess);

                        if (dtAccess.Rows.Count > 0)
                        {
                            ddlWindowClosureType.Items.Add(new ListItem("Please Select", "0"));
                            foreach (DataRow dr in dtAccess.Rows)
                            {
                                string windowClosureId = !String.IsNullOrEmpty(Convert.ToString(dr["WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToString(dr["WINDOW_CLOSURE_TYPE_ID"]) : String.Empty;
                                string windowClosureName = !String.IsNullOrEmpty(Convert.ToString(dr["WINDOW_CLOSURE_TYPE"])) ? Convert.ToString(dr["WINDOW_CLOSURE_TYPE"]) : String.Empty;

                                ddlWindowClosureType.Items.Add(new ListItem(windowClosureName, windowClosureId));
                            }

                        }
                    }

                }
            }
        }
        private void fnGetUserList()
        {
            string sCmpnId = Convert.ToString(Session["CompanyId"]);
            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    string sql = "SELECT A.USER_NM,A.USER_EMAIL FROM " + sAdminDb + "..PROCS_USERS(NOLOCK) A " +
                        "INNER JOIN PROCS_INSIDER_USER(NOLOCK) B ON A.LOGIN_ID=B.USER_LOGIN " +
                        "WHERE B.STATUS='Active' AND B.COMPANY_ID=" + sCmpnId + " ORDER BY A.USER_NM";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dtAccess = new DataTable();
                        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                        daAccess.Fill(dtAccess);

                        if (dtAccess.Rows.Count > 0)
                        {
                            //bindUser.Items.Add(new ListItem("Please Select", "0"));
                            foreach (DataRow dr in dtAccess.Rows)
                            {
                                string userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                                string userEmail = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                                //bindUser.Items.Add(new ListItem(userName, userEmail));
                            }
                        }
                    }
                }
            }
        }
        public void BindRelative()
        {
            string sCmpnId = Convert.ToString(Session["CompanyId"]);
            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT RELATIVE_NAME,RELATIVE_EMAIL FROM PROCS_INSIDER_RELATIVES_DETAIL WHERE ISNULL(RELATIVE_EMAIL,'')<>'' AND RELATIVE_EMAIL<>'Not Applicable' AND STATUS = 'ACTIVE'", conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    Datalist1.DataSource = dt;
                    Datalist1.DataBind();
                }
            }

        }
        public void BindCP()
        {
            string sCmpnId = Convert.ToString(Session["CompanyId"]);
            string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
            if (!String.IsNullOrEmpty(Convert.ToString(Session["ModuleDatabase"])))
            {
                string ConnStr = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    conn.Open();
                    conn.ChangeDatabase(Convert.ToString(Session["ModuleDatabase"]));
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT CP_NAME,CP_EMAIL FROM PROCS_INSIDER_CONNECTED_PERSONS(NOLOCK) WHERE ISNULL(CP_EMAIL,'')<>'' AND CP_STATUS = 'ACTIVE'", conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    ddlConnected.DataSource = dt;
                    ddlConnected.DataBind();
                }
            }

        }
        public void BindRoleData()
        {

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@MODE", "GET_USER_ROLE_DETAILS");
            parameters[1] = new SqlParameter("@AdminDb", Convert.ToString(Session["AdminDb"]));


            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_ROLE_DETAILS", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
            if (ds.Tables.Count > 0)
            {
                ddlRole.DataSource = ds.Tables[0];
                ddlRole.DataBind();

                DataTable dt = ds.Tables[1];
                DataTable dat = ds.Tables[2];

                for (int i = 0; i < ddlRole.Items.Count; i++)
                {
                    DataTable dtRoleUsr = new DataTable();
                    dtRoleUsr.Columns.Add("USER_NM", typeof(string));
                    dtRoleUsr.Columns.Add("USER_EMAIL", typeof(string));
                    dtRoleUsr.Columns.Add("ROLE_NAME", typeof(string));

                    Label Label1 = (Label)ddlRole.Items[i].FindControl("Itmnamelbl1");
                    string labeltext = Label1.Text;

                    if (labeltext != "Connected Person")
                    {
                        for (int c = 0; c < dt.Rows.Count; c++)
                        {
                            DataRow dr = dt.Rows[c];
                            string s = Convert.ToString(dr["ROLE_NAME"]);
                            if (labeltext == s)
                            {
                                dtRoleUsr.ImportRow(dr);
                            }
                        }
                        DataList dl1 = (DataList)ddlRole.Items[i].FindControl("ddlCP");
                        dl1.DataSource = dtRoleUsr;
                        dl1.DataBind();
                    }
                    else
                    {
                        for (int d = 0; d < dat.Rows.Count; d++)
                        {
                            DataRow dr1 = dat.Rows[d];
                            string s = Convert.ToString(dr1["CP_NAME"]);
                            if (labeltext == s)
                                dat.ImportRow(dr1);
                            DataList dl1 = (DataList)ddlRole.Items[i].FindControl("ddlConnected");

                            dl1.DataSource = dat;
                            dl1.DataBind();
                        }

                    }
                }
            }

        }
        public delegate void Run();
        protected void btnSendEmailTradingWindow_Click(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    Response.Redirect("~/login.aspx", true);
                }

                insiderTradingWindowX = new InsiderTradingWindow();
                insiderTradingWindowX.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindowX.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindowX.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                HttpFileCollection files = HttpContext.Current.Request.Files;
                string TradingWindowId = hdnTradingWindowId.Text;//HttpContext.Current.Request.Form["TradingWindowId"].ToString();
                string EmailTemplate = hdnEmailTemplate.Text;//HttpContext.Current.Request.Unvalidated.Form["EmailTemplate"].ToString();
                string EmailSubject = hdnEmailSubject.Text;//HttpContext.Current.Request.Unvalidated.Form["EmailSubject"].ToString();
                string[] lstUser = Convert.ToString(hdnUsers.Text).Split(new char[] { ';' });//Convert.ToString(HttpContext.Current.Request.Form["Users"]).Split(new char[] { ';' });
                string[] lstmailTo = Convert.ToString(hdnMailTo.Text).Split(new char[] { ',' });//Convert.ToString(HttpContext.Current.Request.Form["mailTo"]).Split(new char[] { ';' });

                insiderTradingWindowX.EmailTemplate = EmailTemplate;
                insiderTradingWindowX.EmailSubject = EmailSubject;
                insiderTradingWindowX.id = Convert.ToInt32(TradingWindowId);
                insiderTradingWindowX.lstmailTo = lstmailTo.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                insiderTradingWindowX.lstUser = lstUser.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();

                HttpContext.Current.Session["TotalCnt"] = insiderTradingWindowX.lstmailTo.Count + insiderTradingWindowX.lstUser.Count;
                HttpContext.Current.Session["SentCnt"] = 0;

                var uploadPdf = "/insidertrading/TradingWindowDocumnet/";
                string ext = "";
                string name = "";
                string fileName = "";
                string sSaveAs = "";

                if (txtAttachment.HasFile)
                {
                    HttpPostedFile file = txtAttachment.PostedFile;// files[i];
                    ext = Path.GetExtension(file.FileName);
                    name = Path.GetFileName(file.FileName);
                    string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (sNm.Contains("%00"))
                        {
                            hdnEmailTask.Text = "NullbyteFileError";
                            return;
                        }
                        if (ext.ToLower() == ".pdf")
                        {
                            string withoutExt = Path.GetFileNameWithoutExtension(file.FileName);

                            fileName = withoutExt + ext;

                            String nameX = "TradingWindowDocument_";
                            string newFileName = nameX + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                            sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadPdf), newFileName);
                            file.SaveAs(sSaveAs);
                            insiderTradingWindowX.TradingWindowDocument = fileName;
                            insiderTradingWindowX.TradingWindowDocumentPath = sSaveAs;
                        }
                        else
                        {
                            hdnEmailTask.Text = "FileError";
                            return;
                        }
                    }
                }

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
                InsiderTradingWindow objInsiderTradingWindow = insiderTradingWindowX;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_TRADING_WINDOW_CLOSURE_INFORMATION_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@ID", objInsiderTradingWindow.id);
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
                List<string> mailto = objInsiderTradingWindow.lstmailTo.Where(x=>!string.IsNullOrEmpty(x)).Distinct().ToList();
                objInsiderTradingWindow.lstmailTo = mailto;
                List<string> users = objInsiderTradingWindow.lstUser.Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();
                objInsiderTradingWindow.lstUser = users;

                List<string> lstAttachments = new List<string>();
                if (objInsiderTradingWindow.TradingWindowDocumentPath != null)
                {
                    lstAttachments.Add(objInsiderTradingWindow.TradingWindowDocumentPath);
                }
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
                        Progress.Add(new ProgressStep(string.Format("Notified to {0}", sEmail), ProgressStatus.InProgress));
                    }
                }
                //for (int x = 0; x < 8; x++)
                //{
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