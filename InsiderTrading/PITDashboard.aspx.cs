using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
namespace ProcsDLL.InsiderTrading
{
    public partial class PITDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Count == 0 || String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["CompanyId"])) || String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["EmployeeId"])))
            {
                Response.Redirect("~/login.aspx", true);
            }
            try
            {
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    txtWhetherWindowsAuthentication.Text = "true";
                }
                else
                {
                    txtWhetherWindowsAuthentication.Text = "false";
                }
                if (HttpContext.Current.Session.Count != 0 && !String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["authenticationType"])) && Convert.ToString(Session["authenticationType"]) == "AD")
                {
                    txtWhetherADAuthentication.Text = "true";
                }
                else
                {
                    txtWhetherADAuthentication.Text = "false";
                }
                if (HttpContext.Current.Session.Count != 0 && !String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["CompanyId"])))
                {
                    txtCompanyName.Text = Convert.ToString(HttpContext.Current.Session["CompanyName"]);
                }
                if (!Page.IsPostBack)
                {
                    fnGetAllCategory();
                    fnGetUserRole();
                    fnGetDashBoardNumbers();
                    fnGetDashBoardUPSIInfo();
                    fnGetTaskId();
                    fnGetCompany();
                    fnGetPendingPreclearance();
                    fnGetPendingBenposTrades();
                    fnGetOpenUPSITask();
                    /*fnGetPendingNonCompliance();
                    */
                    //fnGetUPSITasks();
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "Dashboard", "Dashboard.aspx.cs", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }

        }
        private void fnGetAllCategory()
        {
            ProcsDLL.Models.InsiderTrading.Model.Category category = new ProcsDLL.Models.InsiderTrading.Model.Category();
            category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            CategoryRequest categoryList = new CategoryRequest(category);
            CategoryResponse gResCategoryList = categoryList.GetCategoryList();

            if (gResCategoryList.StatusFl)
            {
                if (gResCategoryList.CategoryList.Count > 0)
                {
                    ddlCategory.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Category deptX in gResCategoryList.CategoryList)
                    {
                        ddlCategory.Items.Add(new ListItem(deptX.categoryName, deptX.ID.ToString()));
                    }
                }
            }
        }
        private void fnGetUserRole()
        {
            string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string sAdminDb = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

            using (SqlConnection sCon = new SqlConnection(sConStr))
            {

            }

            ProcsDLL.Models.InsiderTrading.Model.Category category = new ProcsDLL.Models.InsiderTrading.Model.Category();
            category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            CategoryRequest categoryList = new CategoryRequest(category);
            CategoryResponse gResCategoryList = categoryList.GetCategoryList();

            if (gResCategoryList.StatusFl)
            {
                if (gResCategoryList.CategoryList.Count > 0)
                {
                    ddlCategory.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Category deptX in gResCategoryList.CategoryList)
                    {
                        ddlCategory.Items.Add(new ListItem(deptX.categoryName, deptX.ID.ToString()));
                    }
                }
            }
        }
        private void fnGetDashBoardNumbers()
        {
            try
            {
                string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                Int32 iCmpnId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_GET_DASHBOARD_NUMBERS";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@UserLogin", sLoginId));
                    sCmd.Parameters.Add(new SqlParameter("@CompanyId", iCmpnId));
                    sCmd.Parameters.Add(new SqlParameter("@AdminDb", sAdminDb));

                    DataSet dsNum = new DataSet();
                    SqlDataAdapter daNum = new SqlDataAdapter(sCmd);
                    daNum.Fill(dsNum);

                    DataTable dtNum = new DataTable();
                    dtNum = dsNum.Tables[0];

                    if (dtNum.Rows.Count > 0)
                    {
                        string str = "";
                        foreach (DataRow drNum in dtNum.Rows)
                        {
                            str += "<tr>";
                            str += "<td style='border:solid 1px black;text-align:left;padding-left:5px;'>" + Convert.ToString(drNum["PERSON_NAME"]) + "</td>";
                            str += "<td style='border:solid 1px black;text-align:left;padding-left:5px;'>" + Convert.ToString(drNum["RELATION_NM"]) + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["BENPOS_HOLDING"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BENPOS_HOLDING"])) : "-") + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["CURR_HOLDING"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BROKER_NOTE_HOLDING"])) : "-") + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["BENPOS_BUY"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BENPOS_BUY"])) : "-") + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["BENPOS_SELL"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BENPOS_SELL"])) : "-") + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["BROKER_NOTE_BUY"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BROKER_NOTE_BUY"])) : "-") + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["BROKER_NOTE_SELL"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BROKER_NOTE_SELL"])) : "-") + "</td>";
                            str += "<td style='border:solid 1px black;text-align:right;padding-right:5px;'>" + (Convert.ToInt32(drNum["BROKER_NOTE_PLEDGED"]) > 0 ? FormatHelper.FormatNumber(Convert.ToString(drNum["BROKER_NOTE_PLEDGED"])) : "-") + "</td>";
                            str += "</tr>";
                        }
                        tbdNum.InnerHtml = str;
                    }

                    spnBenposDate.InnerHtml = FormatHelper.FormatDate(dsNum.Tables[1].Rows[0]["AS_OF_DATE"].ToString());
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void fnGetDashBoardUPSIInfo()
        {
            try
            {
                string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                Int32 iCmpnId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.CommandText = "SP_GET_DASHBOARD_UPSI_GROUP_INFO";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Connection = sCon;

                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add(new SqlParameter("@UserLogin", sLoginId));
                    sCmd.Parameters.Add(new SqlParameter("@CompanyId", iCmpnId));

                    DataSet dsMyUpsiGroup = new DataSet();
                    SqlDataAdapter daMyUpsiGroup = new SqlDataAdapter(sCmd);
                    daMyUpsiGroup.Fill(dsMyUpsiGroup);

                    DataTable dtMyUpsiGroup = new DataTable();
                    dtMyUpsiGroup = dsMyUpsiGroup.Tables[0];

                    if (dtMyUpsiGroup.Rows.Count > 0)
                    {
                        MyUPSIGroup_PortletBox.Visible = true;
                        RepeaterMyUPSIGroup.DataSource = dtMyUpsiGroup;
                        RepeaterMyUPSIGroup.DataBind();
                    }
                }
            }
            catch (Exception ex)
            { }

        }
        private void fnGetTaskId()
        {
            try
            {
                string loginUserId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                string sConStr = Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true));
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = "SELECT TASK_ID,TASK_FOR,CONVERT(VARCHAR,CONVERT(DATE,TASK_END_DT)) AS TASK_END_DT,ISNULL(B.Title,'') AS TASK_TITLE " +
                        "FROM PROCS_INSIDER_USER_TASK(NOLOCK) A LEFT JOIN PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK(NOLOCK) B ON A.Hdr_Id=B.ID " +
                        "WHERE USER_LOGIN='" + loginUserId + "' AND TASK_STATUS='Open' And TASK_FOR IN('Initial Disclosure Reminder','Annual Disclosure Reminder','Continual Disclosure Reminder')";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Session["TASK_ID"] = dt.Rows[0]["TASK_ID"].ToString();
                        int taskId = Convert.ToInt32(Session["TASK_ID"]);
                        string taskFor = dt.Rows[0]["TASK_FOR"].ToString();
                        string lastDate = dt.Rows[0]["TASK_END_DT"].ToString();
                        string taskTitle = dt.Rows[0]["TASK_TITLE"].ToString();
                        //taskOpenLink.HRef = "UserDeclaration.aspx?TaskId=" + taskId;// "UserDisclosure.aspx?TaskId=" + taskId;                 

                        if (taskTitle == "")
                        {
                            lblDisclosureName.InnerHtml = taskFor;
                        }
                        else
                        {
                            lblDisclosureName.InnerHtml = taskTitle;
                        }
                        lblLastDate.InnerHtml = FormatHelper.ConvertDate(lastDate) + " 23:59";
                        divTaskOpenLink.Visible = true;
                    }
                    else
                    {
                        Session["TASK_ID"] = 0;
                        divTaskOpenLink.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "fnGetTaskId", "Get Task Id", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
        }
        private void fnGetUPSITasks()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                }

                ProcsDLL.Models.InsiderTrading.Model.Dashboard dashboard = new ProcsDLL.Models.InsiderTrading.Model.Dashboard();
                dashboard.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                dashboard.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                dashboard.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                dashboard.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!dashboard.ValidateInput())
                {
                    DashboardResponse objResponse = new DashboardResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    //return objResponse;
                }
                DashboardRequest getDashboardReq = new DashboardRequest(dashboard);
                DashboardResponse getDashboardRes = getDashboardReq.GetMyUPSITask();
                //return getDashboardRes;
            }
            catch (Exception ex)
            {
                DashboardResponse objResponse = new DashboardResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                //return objResponse;
            }
        }
        private void fnGetPendingPreclearance()
        {
            string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);

            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmnd = new SqlCommand();
                sCmnd.Connection = sCon;
                sCmnd.CommandType = CommandType.Text;
                string _sql = "SELECT CASE WHEN A.PRE_CLEARANCE_REQUESTED_FOR=0 THEN B.USER_NM+' (Self)' ELSE C.RELATIVE_NAME+' ('+" +
                    "D.RELATION_NAME+')' END AS NM,CASE WHEN A.PRE_CLEARANCE_REQUESTED_FOR=0 THEN E.PAN ELSE C.PAN END AS PAN," +
                    "A.DEMAT_ACCOUNT,F.NAME AS TRANSACTION_NM,CONVERT(DATE,A.CREATED_ON) AS REQUESTED_ON," +
                    "CONVERT(DATE,A.[APPROVED/REJECTED_ON]) AS APPROVED_ON,CONVERT(DATE,A.FROM_DT) AS FROM_DT," +
                    "CONVERT(DATE,A.TO_DT) AS TO_DT,A.TRADE_QUANTITY,(A.TRADE_QUANTITY-ISNULL(A.EXECUTED_QUANTITY,0)) AS REMAINING_QTY," +
                    "A.ID,A.PRE_CLEARANCE_REQUESTED_FOR,A.TRADE_COMPANY_ID,A.TRANSACTION_TYPE,A.PROPOSED_TRANSACTION_THROUGH " +
                    "FROM PROCS_INSIDER_PRE_CLEARANCE_REQUEST(NOLOCK) A " +
                    "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.LOGIN_ID=B.LOGIN_ID " +
                    "LEFT JOIN PROCS_INSIDER_RELATIVES_DETAIL(NOLOCK) C ON A.LOGIN_ID=C.USER_LOGIN AND C.RELATIVE_ID=A.PRE_CLEARANCE_REQUESTED_FOR " +
                    "LEFT JOIN PROCS_INSIDER_RELATION_MSTR(NOLOCK) D ON C.RELATION_ID=D.RELATION_ID " +
                    "INNER JOIN PROCS_INSIDER_USER_PERSONAL_DETAIL(NOLOCK) E ON A.LOGIN_ID=E.USER_LOGIN " +
                    "INNER JOIN PROCS_INSIDER_TYPE_OF_TRANSACTION(NOLOCK) F ON A.TRANSACTION_TYPE=F.ID " +
                    "WHERE A.STATUS='Approved' AND A.EXECUTED_STATUS='Pending' AND A.LOGIN_ID='" + sLoginId + "'";
                sCmnd.CommandText = _sql;

                DataSet dsRequest = new DataSet();
                SqlDataAdapter daRequest = new SqlDataAdapter(sCmnd);
                daRequest.Fill(dsRequest);

                DataTable dtRequest = new DataTable();
                dtRequest = dsRequest.Tables[0];
                if (dtRequest.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow drRequest in dtRequest.Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["NM"]) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["PAN"]) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["DEMAT_ACCOUNT"]) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["TRANSACTION_NM"]) + "</td>");

                        sb.Append("<td style='padding-left:5px;'>" + FormatHelper.ConvertDate(Convert.ToString(drRequest["REQUESTED_ON"])) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + FormatHelper.ConvertDate(Convert.ToString(drRequest["APPROVED_ON"])) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + FormatHelper.ConvertDate(Convert.ToString(drRequest["FROM_DT"])) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + FormatHelper.ConvertDate(Convert.ToString(drRequest["TO_DT"])) + "</td>");

                        sb.Append("<td style='padding-right:5px;text-align:right;'>" + Convert.ToString(drRequest["TRADE_QUANTITY"]) + "</td>");
                        sb.Append("<td style='padding-right:5px;text-align:right;'>" + Convert.ToString(drRequest["REMAINING_QTY"]) + "</td>");

                        sb.Append("<td style='padding-left:5px;'><a class='btn btn-outline dark' href='PreClearance.aspx?PreClearanceRequestId=" + Convert.ToString(drRequest["ID"]) + "&TradeQuantity=" + Convert.ToString(drRequest["TRADE_QUANTITY"]) + "&EStatus=Approved&TradeExchange=0&DematAccount=" + Convert.ToString(drRequest["DEMAT_ACCOUNT"]) + "&PreClearanceRequestedFor=" + Convert.ToString(drRequest["PRE_CLEARANCE_REQUESTED_FOR"]) + "&TradeCompany=" + Convert.ToString(drRequest["TRADE_COMPANY_ID"]) + "&TransactionType=" + Convert.ToString(drRequest["TRANSACTION_TYPE"]) + "&TradeDate=" + Convert.ToDateTime(drRequest["REQUESTED_ON"]).ToString("yyyy-MM-dd") + "&proposedTransactionThrough=" + Convert.ToString(drRequest["PROPOSED_TRANSACTION_THROUGH"]) + "&tradingFrom=" + Convert.ToDateTime(drRequest["FROM_DT"]).ToString("yyyy-MM-dd") + "&tradingTo=" + Convert.ToDateTime(drRequest["TO_DT"]).ToString("yyyy-MM-dd") + "&RemainingTradeQuantity=" + Convert.ToString(drRequest["REMAINING_QTY"]) + "'><i class='fa fa-upload'></a></td>");
                        sb.Append("</tr>");
                    }
                    tbodyPreClearance.InnerHtml = sb.ToString();
                    Trade_PortletBox.Visible = true;
                }
            }
        }
        private void fnGetPendingBenposTrades()
        {
            string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);

            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmnd = new SqlCommand();
                sCmnd.Connection = sCon;
                sCmnd.CommandType = CommandType.Text;
                string _sql = "SELECT CASE WHEN A.PAN=E.PAN THEN B.USER_NM+' (Self)' ELSE C.RELATIVE_NAME+' ('+D.RELATION_NAME+')' END AS NM," +
                    "A.ID,A.USER_LOGIN,A.COMPANY_ID,A.PAN,A.FOLIO,A.TRANS_DATE,A.QTY,CONVERT(NUMERIC(14,2),A.VALUE) AS VALUE,A.TRANS_TYPE," +
                    "A.SUB_TYPE,A.EXEMPTED,A.REMARKS,A.BP_ID,A.STATUS,CONVERT(DATE,F.START_DT) AS START_DT,CONVERT(DATE,F.END_DT) AS END_DT, " +
                    "CASE WHEN A.PAN=E.PAN THEN 0 ELSE C.RELATIVE_ID END AS RELATIVE_ID," +
                    "CASE WHEN A.TRANS_DATE=G.MIN_TRANS_DATE THEN 0 ELSE 1 END AS ORD FROM TRANSACTION_HISTORY(NOLOCK) A " +
                    "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                    "LEFT JOIN PROCS_INSIDER_RELATIVES_DETAIL(NOLOCK) C ON A.USER_LOGIN=C.USER_LOGIN AND C.PAN=A.PAN " +
                    "LEFT JOIN PROCS_INSIDER_RELATION_MSTR(NOLOCK) D ON C.RELATION_ID=D.RELATION_ID " +
                    "INNER JOIN PROCS_INSIDER_USER_PERSONAL_DETAIL(NOLOCK) E ON A.USER_LOGIN=E.USER_LOGIN " +
                    "INNER JOIN PROCS_INSIDER_BENPOS_HDR(NOLOCK) F ON A.BP_ID=F.HDR_ID " +
                    "LEFT JOIN (SELECT X.USER_LOGIN,X.PAN,X.FOLIO,MIN(X.TRANS_DATE) AS MIN_TRANS_DATE FROM TRANSACTION_HISTORY(NOLOCK) X " +
                    "WHERE X.TRANS_TYPE='BP' AND X.USER_LOGIN='" + sLoginId + "' AND ISNULL(X.STATUS,'')='' AND X.REMARKS='Pending' " +
                    "GROUP BY X.USER_LOGIN,X.PAN,X.FOLIO) G ON A.PAN=G.PAN AND A.FOLIO=G.FOLIO AND A.USER_LOGIN=G.USER_LOGIN " +
                    "WHERE A.TRANS_TYPE='BP' AND A.USER_LOGIN='" + sLoginId + "' AND ISNULL(A.STATUS,'')= '' AND A.REMARKS='Pending'";
                sCmnd.CommandText = _sql;

                DataSet dsRequest = new DataSet();
                SqlDataAdapter daRequest = new SqlDataAdapter(sCmnd);
                daRequest.Fill(dsRequest);

                DataTable dtRequest = new DataTable();
                dtRequest = dsRequest.Tables[0];
                if (dtRequest.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow drRequest in dtRequest.Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["NM"]) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["PAN"]) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["FOLIO"]) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drRequest["SUB_TYPE"]) + "</td>");

                        sb.Append("<td style='padding-left:5px;'>" + FormatHelper.ConvertDate(Convert.ToString(drRequest["START_DT"])) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + FormatHelper.ConvertDate(Convert.ToString(drRequest["END_DT"])) + "</td>");

                        sb.Append("<td style='padding-right:5px;text-align:right;'>" + Convert.ToString(drRequest["QTY"]) + "</td>");
                        //sb.Append("<td style='padding-right:5px;text-align:right;'>" + Convert.ToString(drRequest["VALUE"]) + "</td>");

                        //sb.Append("<td style='padding-left:5px;'><a class='btn btn-outline dark' href='PreClearance.aspx?PreClearanceRequestId=" + Convert.ToString(drRequest["ID"]) + "&TradeQuantity=" + Convert.ToString(drRequest["TRADE_QUANTITY"]) + "&Status=Approved&TradeExchange=0&DematAccount=" + Convert.ToString(drRequest["DEMAT_ACCOUNT"]) + "&PreClearanceRequestedFor=" + Convert.ToString(drRequest["PRE_CLEARANCE_REQUESTED_FOR"]) + "&TradeCompany=" + Convert.ToString(drRequest["TRADE_COMPANY_ID"]) + "&TransactionType=" + Convert.ToString(drRequest["TRANSACTION_TYPE"]) + "&TradeDate=" + Convert.ToDateTime(drRequest["REQUESTED_ON"]).ToString("yyyy-MM-dd") + "&proposedTransactionThrough=" + Convert.ToString(drRequest["PROPOSED_TRANSACTION_THROUGH"]) + "&tradingFrom=" + Convert.ToDateTime(drRequest["FROM_DT"]).ToString("yyyy-MM-dd") + "&tradingTo=" + Convert.ToDateTime(drRequest["TO_DT"]).ToString("yyyy-MM-dd") + "&RemainingTradeQuantity=" + Convert.ToString(drRequest["REMAINING_QTY"]) + "'><i class='fa fa-upload'></a></td>");
                        if (Convert.ToInt32(drRequest["ORD"]) == 0)
                        {
                            sb.Append("<td style='padding-left:5px;'><a class='btn btn-outline dark' href='javascript:fnOpen(\"" + Convert.ToString(drRequest["ID"]) + "\",\"" + Convert.ToString(drRequest["RELATIVE_ID"]) + "\",\"" + Convert.ToString(drRequest["QTY"]) + "\",\"" + Convert.ToString(drRequest["FOLIO"]) + "\",\"" + Convert.ToDateTime(drRequest["TRANS_DATE"]).ToString("yyyy-MM-dd") + "\",\"" + Convert.ToString(drRequest["SUB_TYPE"]) + "\",\"" + Convert.ToDateTime(drRequest["START_DT"]).ToString("yyyy-MM-dd") + "\",\"" + Convert.ToDateTime(drRequest["END_DT"]).ToString("yyyy-MM-dd") + "\")'><i class='fa fa-upload'></a></td>");
                        }
                        else
                        {
                            sb.Append("<td style='padding-left:5px;'><a disabled='disabled' class='btn btn-outline dark'><i class='fa fa-upload'></a></td>");
                        }
                        
                        sb.Append("</tr>");
                    }
                    tbodyBenposTrade.InnerHtml = sb.ToString();
                    ActionableCompliance_PortletBox.Visible = true;
                }
            }
        }
        private void fnGetCompany()
        {
            string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string sAdminDb = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
            string sCmpnId = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            string sDtFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalDateFormat"]);

            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmnd = new SqlCommand();
                sCmnd.Connection = sCon;
                sCmnd.CommandType = CommandType.Text;
                string _sql = "SELECT ID,COMPANY_NAME FROM PROCS_INSIDER_RESTRICTED_COMPANIES(NOLOCK) WHERE IS_HOME_COMPANY=1";
                sCmnd.CommandText = _sql;

                DataSet dsCmpn = new DataSet();
                SqlDataAdapter daCmpn = new SqlDataAdapter(sCmnd);
                daCmpn.Fill(dsCmpn);

                DataTable dtCmpn = new DataTable();
                dtCmpn = dsCmpn.Tables[0];
                if (dtCmpn.Rows.Count > 0)
                {
                    string strOption = "";
                    foreach (DataRow drCmpn in dtCmpn.Rows)
                    {
                        strOption += "<option value='" + Convert.ToString(drCmpn["ID"]) + "'>" + Convert.ToString(drCmpn["COMPANY_NAME"]) + "</option>";
                    }
                    hdnCmpn.Value = strOption;
                }
            }
        }
        private void fnGetOpenUPSITask()
        {
            string sLoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
            string sAdminDb = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
            string sCmpnId = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
            string sDtFormat = Convert.ToString(ConfigurationManager.AppSettings["UniversalDateFormat"]);

            using (SqlConnection sCon = new SqlConnection(sConStr))
            {
                SqlCommand sCmnd = new SqlCommand();
                sCmnd.Connection = sCon;
                sCmnd.CommandType = CommandType.Text;
                string _sql = "DECLARE @UserEmail VARCHAR(100)=NULL,@EmailUPSI VARCHAR(100)=NULL;" +
                    "SELECT @UserEmail=USER_EMAIL FROM " + sAdminDb + "..PROCS_USERS(NOLOCK) WHERE LOGIN_ID='" + sLoginId + "'" +
                    "SELECT* FROM(SELECT (SELECT COUNT(*) FROM UPSI_GROUP_MEMBER_CO_TASK_RECIPIENTS(NOLOCK) B WHERE B.TASK_ID=A.TASK_ID " +
                    "AND B.EMAIL NOT IN(SELECT UPSI_EMAIL FROM PROCS_INSIDER_UPSI_EMAIL_CONFIG(NOLOCK) WHERE COMPANY_ID=" + sCmpnId + ")) " +
                    "AS CNT,* FROM UPSI_GROUP_MEMBER_CO_TASK(NOLOCK) A WHERE A.TASK_FOR='" + sLoginId + "' AND A.STATUS='Open') X " +
                    "WHERE X.CNT>0 ORDER BY EMAIL_DATE DESC";
                sCmnd.CommandText = _sql;

                DataSet dsTask = new DataSet();
                SqlDataAdapter daTask = new SqlDataAdapter(sCmnd);
                daTask.Fill(dsTask);

                DataTable dtTask = new DataTable();
                dtTask = dsTask.Tables[0];
                if (dtTask.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow drTask in dtTask.Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToDateTime(drTask["EMAIL_DATE"]).ToString(sDtFormat) + "</td>");
                        sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drTask["EMAIL_FROM"]) + "</td>");
                        if (Convert.ToString(drTask["EMAIL_TO"]).Length > 50)
                        {
                            sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drTask["EMAIL_TO"]).Substring(0, 50) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drTask["EMAIL_TO"]) + "</td>");
                        }
                        if (Convert.ToString(drTask["MSG_SUBJECT"]).Length > 100)
                        {
                            sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drTask["MSG_SUBJECT"]).Substring(0, 100) + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='padding-left:5px;'>" + Convert.ToString(drTask["MSG_SUBJECT"]) + "</td>");
                        }
                        
                        sb.Append("<td><a data-target='#stackUPSIMessage' data-toggle='modal' class='btn btn-outline dark' onclick=\"javascript:fnGetUPSIMessage('" + Convert.ToString(drTask["TASK_ID"]) + "');\"><i class='fa fa-search'></i></a>&nbsp;&nbsp;");
                        sb.Append("<a data-target='#stack1' data-toggle='modal' id='a_" + Convert.ToString(drTask["TASK_ID"]) + "' class='btn btn-outline dark' onclick=\"javascript:fnGetUPSITaskById('" + Convert.ToString(drTask["TASK_ID"]) + "');\"><i class='fa fa-edit'></a></td>");
                        sb.Append("</tr>");
                    }
                    UPSITaskTbody.InnerHtml = sb.ToString();
                    UPSI_PortletBox.Visible = true;
                }
            }
        }
    }
}