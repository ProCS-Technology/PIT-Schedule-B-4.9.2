using ProcsDLL.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ProcsDLL.InsiderTrading
{
    public partial class UpsiGrpAuditLogRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrpData();
            }
        }        
        private void BindGrpData()
        {
            try
            {
                string sConStr = SQLHelper.GetConnString();
                using (SqlConnection Conn = new SqlConnection(sConStr))
                {
                    Conn.Open();
                    SqlCommand Cmnd = new SqlCommand();
                    Cmnd.Connection = Conn;

                    string sModuleDb = Convert.ToString(Session["ModuleDatabase"]);
                    string sAdminDb = Convert.ToString(Session["AdminDb"]);
                    string sCompanyId = Convert.ToString(Session["CompanyId"]);
                    string sEmployeeId = Convert.ToString(Session["EmployeeId"]);
                    Conn.ChangeDatabase(sModuleDb);

                    string _sql = "SELECT TOP 1 B.ROLE_NAME,ISNULL(A.IS_APPROVER,'') AS IS_APPROVER " +
                        "FROM PROCS_INSIDER_USER(NOLOCK) A " +
                        "INNER JOIN PROCS_INSIDER_ROLE_MSTR(NOLOCK) B ON A.USER_ROLE=B.ROLE_ID " +
                        "WHERE A.USER_LOGIN='" + sEmployeeId + "' AND B.COMPANY_ID=" + sCompanyId;
                    Cmnd.CommandText = _sql;
                    Cmnd.CommandType = CommandType.Text;

                    DataSet dsRole = new DataSet();
                    SqlDataAdapter daRole = new SqlDataAdapter(Cmnd);
                    daRole.Fill(dsRole);
                    DataTable dtRole = new DataTable();
                    dtRole = dsRole.Tables[0];

                    _sql = "SELECT COUNT(*) FROM PROCS_INSIDER_UPSI_CONFIGURATION(NOLOCK) WHERE ISNULL(AUTHORIZED_USR,'')='" + sEmployeeId + "'";
                    Cmnd.CommandText = _sql;
                    Cmnd.CommandType = CommandType.Text;
                    Int32 iAuthorizeCnt = Convert.ToInt32(Cmnd.ExecuteScalar());

                    _sql = "SELECT ISNULL(SHOW_UPSI_TO_CO,'No') FROM PROCS_INSIDER_UPSI_CONFIGURATION(NOLOCK)";
                    Cmnd.CommandText = _sql;
                    Cmnd.CommandType = CommandType.Text;
                    string sAllowToCO = Convert.ToString(Cmnd.ExecuteScalar());

                    if (dtRole.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
                        {
                            _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                                "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                                "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                                "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                                "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                                "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
                        }
                        else
                        {
                            _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                                "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                                "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                                "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                                "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                                "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " " +
                                "AND A.STATUS='Active' AND A.USER_LOGIN='" + sEmployeeId + "' ORDER BY B.USER_NM";
                        }
                    }
                    else
                    {
                        _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                            "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                            "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                            "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                            "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                            "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " " +
                            "AND A.STATUS='Active' AND A.USER_LOGIN='" + sEmployeeId + "' ORDER BY B.USER_NM";
                    }
                    Cmnd.CommandText = _sql;
                    Cmnd.CommandType = CommandType.Text;

                    DataSet dsUser = new DataSet();
                    SqlDataAdapter daUser = new SqlDataAdapter(Cmnd);
                    daUser.Fill(dsUser);
                    DataTable dtUser = new DataTable();
                    dtUser = dsUser.Tables[0];

                    //if (dtRole.Rows.Count > 0)
                    //{
                    //    if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
                    //    {
                    //        _sql = "SELECT A.GRP_ID,A.GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A ORDER BY GRP_NAME";
                    //    }
                    //    else
                    //    {
                    //        _sql = "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                    //            "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
                    //            "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "' ORDER BY GRP_NAME";
                    //    }
                    //}
                    //else
                    //{
                    //    _sql = "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                    //    "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
                    //    "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "' ORDER BY GRP_NAME";
                    //}

                    _sql = "SELECT * FROM dbo.fnGetUPSIEvent('" + Session["EmployeeId"] + "'," + sCompanyId + ")";

                    Cmnd.CommandText = _sql;
                    Cmnd.CommandType = CommandType.Text;

                    DataSet dsGrp = new DataSet();
                    SqlDataAdapter daGrp = new SqlDataAdapter(Cmnd);
                    daGrp.Fill(dsGrp);
                    DataTable dtGrp = new DataTable();
                    dtGrp = dsGrp.Tables[0];

                    if (dtGrp.Rows.Count > 1)
                    {
                        ddlUPSIGrp.Items.Add(new ListItem("", ""));
                        ddlUPSIGrp.Items.Add(new ListItem("All", "0"));
                    }
                    foreach (DataRow drGrp in dtGrp.Rows)
                    {
                        ddlUPSIGrp.Items.Add(new ListItem(Convert.ToString(drGrp["GrpNm"]), Convert.ToString(drGrp["GrpId"])));
                    }
                }

            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "UPSI Audit Log Report", "UPSI STATUS", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlUPSIGrp.SelectedValue)
                    ||
                    !string.IsNullOrEmpty(txtFromDate.Value) || !string.IsNullOrEmpty(txtToDate.Value)
                )
                {
                    try
                    {
                        //DateTime dtFrom = DateTime.ParseExact(txtFromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var convertedDateFrom = FormatHelper.FormatDate(txtFromDate.Value);//dtFrom.ToString("yyyy-MM-dd");
                        //DateTime dtTo = DateTime.ParseExact(txtToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var convertedDateTo = FormatHelper.FormatDate(txtToDate.Value);//dtTo.ToString("yyyy-MM-dd");

                        SqlParameter[] parameters = new SqlParameter[7];
                        parameters[0] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                        parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LOG_REPORT");
                        parameters[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(ddlUPSIGrp.SelectedValue));
                        parameters[3] = new SqlParameter("@AdminDb", Convert.ToString(Session["AdminDb"]));
                        parameters[4] = new SqlParameter("@Valid_FromStr", convertedDateFrom);
                        parameters[5] = new SqlParameter("@Valid_ToStr", convertedDateTo);
                        parameters[6] = new SqlParameter("@UserLogin", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));
                        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_LOG", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
                        RepeaterUPSIGroup.DataSource = ds.Tables[0];
                        RepeaterUPSIGroup.DataBind();

                        parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_DP_LOG_REPORT");
                        DataSet dsDP = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_LOG", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
                        RepeaterMember.DataSource = dsDP.Tables[0];
                        RepeaterMember.DataBind();

                        parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_CP_LOG_REPORT");
                        DataSet dsCP = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_LOG", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
                        RepeaterCp.DataSource = dsCP.Tables[0];
                        RepeaterCp.DataBind();

                        parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_COMM_LOG_REPORT");
                        DataSet dsComm = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_LOG", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);
                        RepeaterCommunication.DataSource = dsComm.Tables[0];
                        RepeaterCommunication.DataBind();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "setTimeout(function(){fnalert();},1000);", true);
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "UPSI STATUS", "UPSI STATUS", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
        }
    }
}