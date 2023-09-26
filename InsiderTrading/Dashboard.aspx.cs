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
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string sAdminDb = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);

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
            catch(Exception ex)
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
            catch(Exception ex)
            {            }
            
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
                    sCmd.CommandText = "SELECT TASK_ID,TASK_FOR,CONVERT(VARCHAR,CONVERT(DATE,TASK_END_DT)) AS TASK_END_DT,ISNULL(B.Title,'') AS TASK_TITLE "+
                        "FROM PROCS_INSIDER_USER_TASK(NOLOCK) A LEFT JOIN PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK(NOLOCK) B ON A.Hdr_Id=B.ID "+
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
                        lblLastDate.InnerHtml = FormatHelper.ConvertDate(lastDate)+" 23:59";
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
    }
}