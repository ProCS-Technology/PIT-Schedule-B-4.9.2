using ProcsDLL.Models.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace ProcsDLL.InsiderTrading
{
    public partial class UPSIRept : System.Web.UI.Page
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

                string _sql = "SELECT VISIBILITY FROM PROCS_INSIDER_UPSI_CONFIGURATION";
                string _sMsgAttachment = Convert.ToString(
                    SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, sModuleDb, null)
                );
                _sql = "SELECT TOP 1 B.ROLE_NAME,ISNULL(A.IS_APPROVER,'') AS IS_APPROVER " +
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
                int iAuthorizedCnt = 0;
                iAuthorizedCnt = Convert.ToInt32(Cmnd.ExecuteScalar());

                _sql = "SELECT ISNULL(SHOW_UPSI_TO_CO,'No') FROM PROCS_INSIDER_UPSI_CONFIGURATION(NOLOCK)";
                Cmnd.CommandText = _sql;
                Cmnd.CommandType = CommandType.Text;
                string sShowToCO = "Yes";
                sShowToCO = Convert.ToString(Cmnd.ExecuteScalar());

                if (iAuthorizedCnt > 0)
                {
                    _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                        "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                        "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
                }
                else if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes" || Convert.ToString(dtRole.Rows[0]["ROLE_NAME"]) == "Admin")
                {
                    _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                        "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                        "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
                }
                else
                {
                    _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
                        "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
                        "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
                        "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.COMPANY_ID=" + sCompanyId + " " +
                        "AND A.STATUS='Active' AND A.USER_LOGIN='" + sEmployeeId + "' ORDER BY B.USER_NM";
                }
                Cmnd.CommandText = _sql;
                Cmnd.CommandType = CommandType.Text;

                DataSet dsUser = new DataSet();
                SqlDataAdapter daUser = new SqlDataAdapter(Cmnd);
                daUser.Fill(dsUser);
                DataTable dtUser = new DataTable();
                dtUser = dsUser.Tables[0];

                if (dtUser.Rows.Count > 1)
                {
                    ddlUsers.Items.Add(new ListItem("", ""));
                    ddlUsers.Items.Add(new ListItem("All", "All"));
                }
                foreach (DataRow drUser in dtUser.Rows)
                {
                    ddlUsers.Items.Add(new ListItem(Convert.ToString(drUser["USER_NM"]), Convert.ToString(drUser["USER_EMAIL"])));
                }

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
        //private void fnGetInitialData()
        //{
        //    string sConStr = SQLHelper.GetConnString();
        //    using (SqlConnection Conn = new SqlConnection(sConStr))
        //    {
        //        Conn.Open();
        //        SqlCommand Cmnd = new SqlCommand();
        //        Cmnd.Connection = Conn;

        //        string sModuleDb = Convert.ToString(Session["ModuleDatabase"]);
        //        string sAdminDb = Convert.ToString(Session["AdminDb"]);
        //        string sCompanyId = Convert.ToString(Session["CompanyId"]);
        //        string sEmployeeId = Convert.ToString(Session["EmployeeId"]);
        //        Conn.ChangeDatabase(sModuleDb);

        //        string _sql = "SELECT VISIBILITY FROM PROCS_INSIDER_UPSI_CONFIGURATION";
        //        string _sMsgAttachment = Convert.ToString(
        //            SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, sModuleDb, null)
        //        );
        //        _sql = "SELECT TOP 1 B.ROLE_NAME,ISNULL(A.IS_APPROVER,'') AS IS_APPROVER " +
        //            "FROM PROCS_INSIDER_USER(NOLOCK) A " +
        //            "INNER JOIN PROCS_INSIDER_ROLE_MSTR(NOLOCK) B ON A.USER_ROLE=B.ROLE_ID " +
        //            "WHERE A.USER_LOGIN='" + sEmployeeId + "' AND B.COMPANY_ID=" + sCompanyId;
        //        Cmnd.CommandText = _sql;
        //        Cmnd.CommandType = CommandType.Text;

        //        DataSet dsRole = new DataSet();
        //        SqlDataAdapter daRole = new SqlDataAdapter(Cmnd);
        //        daRole.Fill(dsRole);
        //        DataTable dtRole = new DataTable();
        //        dtRole = dsRole.Tables[0];

        //        _sql = "SELECT COUNT(*) FROM PROCS_INSIDER_UPSI_CONFIGURATION(NOLOCK) WHERE ISNULL(AUTHORIZED_USR,'')='" + sEmployeeId + "'";
        //        Cmnd.CommandText = _sql;
        //        Cmnd.CommandType = CommandType.Text;
        //        int iAuthorizedCnt = 0;
        //        iAuthorizedCnt = Convert.ToInt32(Cmnd.ExecuteScalar());

        //        _sql = "SELECT ISNULL(SHOW_UPSI_TO_CO,'No') FROM PROCS_INSIDER_UPSI_CONFIGURATION(NOLOCK)";
        //        Cmnd.CommandText = _sql;
        //        Cmnd.CommandType = CommandType.Text;
        //        string sShowToCO = "Yes";
        //        sShowToCO = Convert.ToString(Cmnd.ExecuteScalar());

        //        if (iAuthorizedCnt > 0)
        //        {
        //            _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
        //                "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
        //        }
        //        else if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
        //        {
        //            _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
        //                "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.COMPANY_ID=" + sCompanyId + " AND A.STATUS='Active' ORDER BY B.USER_NM";
        //        }
        //        else
        //        {
        //            _sql = "SELECT B.USER_NM,B.USER_EMAIL FROM PROCS_INSIDER_USER(NOLOCK) A " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_USERS(NOLOCK) B ON A.USER_LOGIN=B.LOGIN_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_USERS_BU_ACESS(NOLOCK) C ON B.LOGIN_ID=C.LOGIN_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_BUSINESS_COMPANY_MODULE(NOLOCK) D ON C.COMPANY_ID=D.COMPANY_ID AND C.MODULE_ID=D.MODULE_ID " +
        //                "INNER JOIN " + sAdminDb + "..PROCS_MODULE(NOLOCK) E ON D.MODULE_ID=E.MODULE_ID " +
        //                "WHERE E.MODULE_NM='Insider Trading' AND C.COMPANY_ID=" + sCompanyId + " AND A.COMPANY_ID=" + sCompanyId + " " +
        //                "AND A.STATUS='Active' AND A.USER_LOGIN='" + sEmployeeId + "' ORDER BY B.USER_NM";
        //        }
        //        Cmnd.CommandText = _sql;
        //        Cmnd.CommandType = CommandType.Text;

        //        DataSet dsUser = new DataSet();
        //        SqlDataAdapter daUser = new SqlDataAdapter(Cmnd);
        //        daUser.Fill(dsUser);
        //        DataTable dtUser = new DataTable();
        //        dtUser = dsUser.Tables[0];

        //        if (dtUser.Rows.Count > 1)
        //        {
        //            ddlUsers.Items.Add(new ListItem("", ""));
        //            ddlUsers.Items.Add(new ListItem("All", "All"));
        //        }
        //        foreach (DataRow drUser in dtUser.Rows)
        //        {
        //            ddlUsers.Items.Add(new ListItem(Convert.ToString(drUser["USER_NM"]), Convert.ToString(drUser["USER_EMAIL"])));
        //        }

        //        if (iAuthorizedCnt > 0) { }
        //        else if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes")
        //        {
        //            if (sShowToCO.ToUpper() == "YES")
        //            {
        //                _sql = "SELECT A.GRP_ID,A.GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A ORDER BY A.GRP_NAME";
        //            }
        //            else
        //            {
        //                _sql = "SELECT A.GRP_ID,A.GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
        //               "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
        //               "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "' " +
        //               "UNION " +
        //               "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
        //               "WHERE ISNULL(A.VALID_TO,'9999-12-31')<CONVERT(DATE,GETDATE()) " +
        //               "ORDER BY GRP_NAME";
        //            }
        //        }
        //        else
        //        {
        //            _sql = "SELECT A.GRP_ID,GRP_NAME FROM PROCS_INSIDER_UPSI_GROUP(NOLOCK) A " +
        //                "INNER JOIN PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) B ON A.GRP_ID=B.GRP_ID " +
        //                "WHERE B.USER_LOGIN='" + Session["EmployeeId"] + "' ORDER BY GRP_NAME";
        //        }

        //        Cmnd.CommandText = _sql;
        //        Cmnd.CommandType = CommandType.Text;

        //        DataSet dsGrp = new DataSet();
        //        SqlDataAdapter daGrp = new SqlDataAdapter(Cmnd);
        //        daGrp.Fill(dsGrp);
        //        DataTable dtGrp = new DataTable();
        //        dtGrp = dsGrp.Tables[0];

        //        if (dtGrp.Rows.Count > 1)
        //        {
        //            ddlUPSIGrp.Items.Add(new ListItem("", ""));
        //            ddlUPSIGrp.Items.Add(new ListItem("All", "0"));
        //        }
        //        else
        //        {
        //            if (Convert.ToString(dtRole.Rows[0]["IS_APPROVER"]) == "Yes" || Convert.ToString(dtRole.Rows[0]["ROLE_NAME"]) == "Admin")
        //            {
        //                ddlUPSIGrp.Items.Add(new ListItem("All", "0"));
        //            }
        //        }
        //        foreach (DataRow drGrp in dtGrp.Rows)
        //        {
        //            ddlUPSIGrp.Items.Add(new ListItem(Convert.ToString(drGrp["GRP_NAME"]), Convert.ToString(drGrp["GRP_ID"])));
        //        }
        //    }
        //}
        protected void btnRun_Click(object sender, EventArgs e)
        {
            string sDtFormat = ConfigurationManager.AppSettings["UniversalDateFormat"].ToString();
            string sTmFormat = ConfigurationManager.AppSettings["UPSITimeFormat"].ToString();
            string sUPSIGrpId = ddlUPSIGrp.SelectedValue;
            string sUserId = ddlUsers.SelectedValue;
            string sFrmDt = FormatHelper.FormatDate(txtFromDate.Text);//ConvertDt(txtFromDate.Text);
            string sToDt = FormatHelper.FormatDate(txtToDate.Text);//ConvertDt(txtToDate.Text);
            string sCompanyId = Convert.ToString(Session["CompanyId"]);
            string sEmployeeId = Convert.ToString(Session["EmployeeId"]);
            string sAdminDb = Convert.ToString(Session["AdminDb"]);
            string sModuleDatabase = Convert.ToString(Session["ModuleDatabase"]);

            string _sql = "SELECT VISIBILITY FROM PROCS_INSIDER_UPSI_CONFIGURATION";
            string _sMsgAttachment = Convert.ToString(
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, sModuleDatabase, null)
            );


            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@MODE", "GET_UPSI_REPORT");
            parameters[1] = new SqlParameter("@GrpId", sUPSIGrpId);
            parameters[2] = new SqlParameter("@UPSI_OF", sUserId);
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
                        if(Convert.ToString(dr["STATUS"])== "Discarded")
                        {
                            sb.Append("<tr style='background-color:lightyellow;text-decoration:line-through;'>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                        }
                        sb.Append("<td>" + iCntr.ToString() + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["SHARED_BY"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["SHARED_BY_ORGANIZATION"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["SHARED_BY_IDENTIFICATION"]) + "</td>");

                        sb.Append("<td>" + Convert.ToString(dr["SHARED_WITH"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["ORGANIZATION_NM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["SHARED_WITH_IDENTIFICATION"]) + "</td>");

                        sb.Append("<td>" + Convert.ToDateTime(dr["EMAIL_DATE"]).ToString(sDtFormat) + "</td>");
                        sb.Append("<td>" + Convert.ToDateTime(dr["EMAIL_DATE"]).ToString(sTmFormat) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["TYP_NM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["GRP_NM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["MODE_OF_SHARING"]) + "</td>");
                        sb.Append("<td>" + Convert.ToDateTime(dr["ENTRY_DATE"]).ToString(sDtFormat+" HH:mm") + "</td>");

                        if (Convert.ToString(dr["MODE"]) == "Auto")
                        {
                            sb.Append("<td>UPSI Email</td>");
                        }
                        else
                        {
                            sb.Append("<td>Manual</td>");
                        }
                        sb.Append("<td>" + Convert.ToString(dr["REMARKS"]) + "</td>");

                        if (_sMsgAttachment.ToUpper() == "MESSAGE" || _sMsgAttachment.ToUpper() == "MESSAGEATTACHMENT")
                        {
                            tdMsgAttachment.Visible = true;
                            if (Convert.ToString(dr["MODE"]) == "Auto")
                            {
                                string sMsg = "From: " + Convert.ToString(dr["EMAIL_FROM"]) + "<br />" +
                                    "To: " + Convert.ToString(dr["EMAIL_TO"]) + "<br />" +
                                    "CC: " + Convert.ToString(dr["EMAIL_CC"]) + "<br />Subject:" + Convert.ToString(dr["REMARKS"]) + "<br />" + Convert.ToString(dr["EMAIL_MSG"]);
                                sb.Append("<td>");
                                sb.Append("<div style='display:none;' id='txtMsg_" + Convert.ToString(dr["COMMUNICATION_ID"]) + "_" + iCntr.ToString() + "'>" + sMsg + "</div>");
                                sb.Append("<a href='#attachmentBody' class='btn btn-primary' onclick='fnGetAttachmentBody(\"" + Convert.ToString(dr["COMMUNICATION_ID"]) + "\",\"" + iCntr.ToString() + "\");' data-target='#messageBody' data-toggle='modal'><i style='color:white' class='fa fa-paperclip' aria-hidden='true'></i></a>");
                                if (_sMsgAttachment.ToUpper() == "MESSAGEATTACHMENT")
                                {
                                    DataRow[] drAttachments = dtAttachment.Select("COMMUNICATION_ID=" + Convert.ToString(dr["COMMUNICATION_ID"]));
                                    if (drAttachments.Length > 0)
                                    {
                                        string str = "";
                                        str += "<ul>";
                                        foreach (DataRow drAttachment in drAttachments)
                                        {
                                            string FileName = Convert.ToString(drAttachment["ATTECHMENT"]);
                                            string fileExtension = Path.GetExtension(FileName);
                                            //str += "<li><a href='UPSI/" + Convert.ToString(drAttachment["ATTECHMENT"]) + "' target='_blank'>" + Convert.ToString(drAttachment["ATTECHMENT"]) + "</a></li>";
                                            // str += "<li><a onclick=javascript:fnDownloadBenpos('" + Convert.ToString(dr["COMMUNICATION_ID"])+ + fileExtension + "');>" + Convert.ToString(drAttachment["ATTECHMENT"]) + "</a></li>";
                                            if (fileExtension == ".pdf" ||
                                               fileExtension == ".txt" ||
                                               fileExtension == ".xlsx" ||
                                               fileExtension == ".xls" ||
                                               fileExtension == ".doc" ||
                                               fileExtension == ".docx" ||
                                               fileExtension == ".png" ||
                                               fileExtension == ".jpeg" ||
                                               fileExtension == ".gif" ||
                                               fileExtension == ".zip" ||
                                               fileExtension == ".ppt" ||
                                               fileExtension == ".pptx"
                                                 )
                                            {
                                                str += "<li><a onclick=javascript:fnDownloadAttechment('" + Convert.ToString(dr["COMMUNICATION_ID"]) + "','" + fileExtension + "');>" + Convert.ToString(drAttachment["ATTECHMENT"]) + "</a></li>";
                                            }

                                            //str += "<li><a class="fa fa-trash" style="color:red;margin-left:10px;" data-target="#modalDeleteBenposDetail" data-toggle="modal" id="d' + msg.BenposHeaderList[index].id + '" onclick=\'javascript:fnDeleteBenposList("' + msg.BenposHeaderList[index].id + '");\'"></a></td>';
                                        }
                                        str += "</ul>";
                                        sb.Append("<input type='text' id='txtAttachmentLnk_" + Convert.ToString(dr["COMMUNICATION_ID"]) + "' style='display:none;' value=\"" + str + "\" />");
                                    }
                                    else
                                    {
                                        sb.Append("<input type='text' id='txtAttachmentLnk_" + Convert.ToString(dr["COMMUNICATION_ID"]) + "' style='display:none;' value='' />");
                                    }
                                }
                                sb.Append("</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                        }
                        //else
                        //{
                        //    sb.Append("<td style='display:none;'></td>");
                        //}
                        iCntr++;
                        sb.Append("</tr>");
                    }
                    tbdUPSIReportList.InnerHtml = sb.ToString();
                }
                else
                {
                    tbdUPSIReportList.InnerHtml = "";
                }
            }
            else
            {
                tbdUPSIReportList.InnerHtml = "";
            }
        }
        private string ConvertDt(string sDt)
        {
            return sDt.Split(new Char[] { '/' })[2] + "-" + sDt.Split(new Char[] { '/' })[1] + "-" + sDt.Split(new Char[] { '/' })[0];
        }
    }
}
