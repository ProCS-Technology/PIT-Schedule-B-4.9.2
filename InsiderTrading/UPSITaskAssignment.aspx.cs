using ProcsDLL.Models.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
namespace ProcsDLL.InsiderTrading
{
    public partial class UPSITaskAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fnGetInitialData();
            }
        }
        private void fnGetInitialData()
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

                //if (dtRole.Rows.Count > 0)
                //{
                //    if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
                //    {
                //        _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                //            "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
                //    }
                //    else
                //    {
                //        _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                //            "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                //            "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " " +
                //            "AND A.STATUS='Active' AND A.USER_LOGIN='" + sEmployeeId + "' ORDER BY B.USER_NM";
                //    }
                //}
                //else
                //{
                //    _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                //        "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                //        "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                //        "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                //        "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                //        "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " " +
                //        "AND A.STATUS='Active' AND A.USER_LOGIN='" + sEmployeeId + "' ORDER BY B.USER_NM";
                //}
                //Cmnd.CommandText = _sql;
                //Cmnd.CommandType = CommandType.Text;

                //DataSet dsUser = new DataSet();
                //SqlDataAdapter daUser = new SqlDataAdapter(Cmnd);
                //daUser.Fill(dsUser);
                //DataTable dtUser = new DataTable();
                //dtUser = dsUser.Tables[0];

                //if (dtUser.Rows.Count > 1)
                //{
                //    ddlUsers.Items.Add(new ListItem("", ""));
                //    ddlUsers.Items.Add(new ListItem("All", "All"));
                //}
                //foreach (DataRow drUser in dtUser.Rows)
                //{
                //    ddlUsers.Items.Add(new ListItem(Convert.ToString(drUser["USER_NM"]), Convert.ToString(drUser["USER_EMAIL"])));
                //}

                if (dtRole.Rows.Count > 0)
                {
                    if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
                    {
                        _sql = "SELECT A.GRP_ID,A.GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                            "/*INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
                            "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "'*/ " +
                            "UNION " +
                            "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                            "WHERE A.VALID_TO IS NOT NULL AND CONVERT(DATE,A.VALID_TO)<CONVERT(DATE,GETDATE()) " +
                            "ORDER BY GRP_NAME";
                    }
                    else
                    {
                        _sql = "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                            "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
                            "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "' ORDER BY GRP_NAME";
                    }
                }
                else
                {
                    _sql = "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
                    "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "' ORDER BY GRP_NAME";
                }

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
                else
                {
                    if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
                    {
                        ddlUPSIGrp.Items.Add(new ListItem("All", "0"));
                    }
                }
                foreach (DataRow drGrp in dtGrp.Rows)
                {
                    ddlUPSIGrp.Items.Add(new ListItem(Convert.ToString(drGrp["GRP_NAME"]), Convert.ToString(drGrp["GRP_ID"])));
                }
            }
        }
        protected void btnRun_Click(object sender, EventArgs e)
        {
            string sDtFormat = ConfigurationManager.AppSettings["UPSIDateFormat"].ToString();
            string sTmFormat = ConfigurationManager.AppSettings["UPSITimeFormat"].ToString();
            string sUPSIGrpId = ddlUPSIGrp.SelectedValue;
            //string sUserId = ddlUsers.SelectedValue;
            string sFrmDt = ConvertDt(txtFromDate.Text);
            string sToDt = ConvertDt(txtToDate.Text);
            string sCompanyId = Convert.ToString(Session["CompanyId"]);
            string sEmployeeId = Convert.ToString(Session["EmployeeId"]);
            string sAdminDb = Convert.ToString(Session["AdminDb"]);
            string sModuleDatabase = Convert.ToString(Session["ModuleDatabase"]);


            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@MODE", "GET_UPSI_ASSIGNMENT_REPORT");
            parameters[1] = new SqlParameter("@GrpId", sUPSIGrpId);
            parameters[2] = new SqlParameter("@UPSI_OF", "All");
            parameters[3] = new SqlParameter("@FrmDt", sFrmDt);
            parameters[4] = new SqlParameter("@ToDt", sToDt);
            parameters[5] = new SqlParameter("@COMPANY_ID", sCompanyId);
            parameters[6] = new SqlParameter("@RUN_BY", sEmployeeId);
            parameters[7] = new SqlParameter("@ADMIN_DB", sAdminDb);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_REPORT", sModuleDatabase, parameters);
            DataTable dt = ds.Tables[0];
            DataTable dtAttachment = ds.Tables[1];

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    Int32 iCntr = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + iCntr.ToString() + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["SHARED_BY"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["SHARED_BY_IDENTIFICATION"]) + "</td>");

                        sb.Append("<td>" + Convert.ToString(dr["EMAIL_TO"]) + "</td>");
                        //sb.Append("<td>" + Convert.ToString(dr["ORGANIZATION_NM"]) + "</td>");
                        //sb.Append("<td>" + Convert.ToString(dr["SHARED_WITH_IDENTIFICATION"]) + "</td>");

                        sb.Append("<td>" + Convert.ToDateTime(dr["EMAIL_DATE"]).ToString(sDtFormat) + "</td>");
                        sb.Append("<td>" + Convert.ToDateTime(dr["EMAIL_DATE"]).ToString(sTmFormat) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["TYP_NM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["MODE_OF_SHARING"]) + "</td>");
                        //if (DBNull.Value == dr["CREATED_ON"])
                        //{
                        //    sb.Append("<td>&nbsp;</td>");
                        //    sb.Append("<td>&nbsp;</td>");
                        //}
                        //else
                        //{
                        //    sb.Append("<td>" + Convert.ToDateTime(dr["CREATED_ON"]).ToString(sDtFormat) + "</td>");
                        //    sb.Append("<td>" + Convert.ToDateTime(dr["CREATED_ON"]).ToString(sTmFormat) + "</td>");
                        //}
                        //sb.Append("<td>" + Convert.ToString(dr["REMARKS"]) + "</td>");

                        if (Convert.ToString(dr["MODE"]) == "Auto")
                        {
                            sb.Append("<td>");
                            sb.Append("<div style='display:none;' id='txtMsg_" + Convert.ToString(dr["COMMUNICATION_ID"]) + "_" + iCntr.ToString() + "'>" + Convert.ToString(dr["EMAIL_MSG"]) + "</div>");
                            sb.Append("<a href='#attachmentBody' class='btn btn-primary' onclick='fnGetAttachmentBody(\"" + Convert.ToString(dr["COMMUNICATION_ID"]) + "\",\"" + iCntr.ToString() + "\");' data-target='#messageBody' data-toggle='modal'><i style='color:white' class='fa fa-paperclip' aria-hidden='true'></i></a>");


                            DataRow[] drAttachments = dtAttachment.Select("COMMUNICATION_ID=" + Convert.ToString(dr["COMMUNICATION_ID"]));
                            if (drAttachments.Length > 0)
                            {
                                string str = "";
                                str += "<ul>";
                                foreach (DataRow drAttachment in drAttachments)
                                {
                                    str += "<li><a href='UPSI/" + Convert.ToString(drAttachment["ATTECHMENT"]) + "' target='_blank'>" + Convert.ToString(drAttachment["ATTECHMENT"]) + "</a></li>";
                                }
                                str += "</ul>";
                                sb.Append("<input type='text' id='txtAttachmentLnk_" + Convert.ToString(dr["COMMUNICATION_ID"]) + "' style='display:none;' value=\"" + str + "\" />");
                            }
                            else
                            {
                                sb.Append("<input type='text' id='txtAttachmentLnk_" + Convert.ToString(dr["COMMUNICATION_ID"]) + "' style='display:none;' value='' />");
                            }

                            sb.Append("<a href='#attachmentBody' class='btn btn-primary' onclick='fnGetAttachmentBody(\"" + Convert.ToString(dr["COMMUNICATION_ID"]) + "\",\"" + iCntr.ToString() + "\");' data-target='#messageBody' data-toggle='modal'><i style='color:white' class='fa fa-edit' aria-hidden='true'></i></a>");
                            sb.Append("</td>");
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                        //result += '<td>' + msg.lstRpt[i].NoticeSent + '</td>';
                        iCntr++;
                        sb.Append("</tr>");
                    }
                    //txtReport.Text = sb.ToString();
                    tbdUPSIReportList.InnerHtml = sb.ToString();
                }
            }
        }
        private string ConvertDt(string sDt)
        {
            return sDt.Split(new Char[] { '/' })[2] + "-" + sDt.Split(new Char[] { '/' })[1] + "-" + sDt.Split(new Char[] { '/' })[0];
        }
    }
}