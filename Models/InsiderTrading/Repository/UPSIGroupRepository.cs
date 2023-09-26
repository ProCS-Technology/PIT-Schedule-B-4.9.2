using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UPSIGroupRepository
    {
        string sDtFormat = ConfigurationManager.AppSettings["UPSIDateFormat"].ToString();
        string sTmFormat = ConfigurationManager.AppSettings["UPSITimeFormat"].ToString();
        public UPSIGroupResponse GetUPSIType(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@TYPE_ID", objUPSIGrp.TypId);
                parameters[2] = new SqlParameter("@Mode", "GET_UPSI_TYPE");

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<UPSIGrp> lstGrp = new List<UPSIGrp>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            UPSIGrp o = new UPSIGrp();
                            o.TypId = Convert.ToInt32(dr["TYP_ID"]);
                            o.TypNm = Convert.ToString(dr["TYP_NM"]);
                            lstGrp.Add(o);
                        }
                        oGrp.UPSIGroups = lstGrp;
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse GetUPSIGroups(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_UPSI_GROUP");
                parameters[2] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                DataTable dtKeywords = ds.Tables[1];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<UPSIGrp> lstGrp = new List<UPSIGrp>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            UPSIGrp o = new UPSIGrp();
                            o.GrpId = Convert.ToInt32(dr["GRP_ID"]);
                            o.GrpNm = Convert.ToString(dr["GRP_NAME"]);
                            o.GrpStatus = Convert.ToString(dr["GRP_STATUS"]);
                            o.TypId = Convert.ToInt32(dr["TYP_ID"]);
                            o.TypNm = Convert.ToString(dr["TYP_NM"]);
                            //o.ValidFrom = FormatHelper.FormatDate(Convert.ToString(dr["VALID_FROM"]));
                            //o.ValidTo = FormatHelper.FormatDate(Convert.ToString(dr["VALID_TO"]));
                            o.ValidFrom = Convert.ToString(dr["VALID_FROM"]);
                            o.ValidTo = Convert.ToString(dr["VALID_TO"]);
                            o.GrpDesc = Convert.ToString(dr["GRP_DESC"]);
                            o.Remarks = Convert.ToString(dr["REMARKS"]);
                            o.CanEdit = Convert.ToString(dr["CAN_EDIT"]);
                            o.DPCnt = Convert.ToInt32(dr["DP_CNT"]);
                            o.CPCnt = Convert.ToInt32(dr["CP_CNT"]);
                            o.COMMCnt = Convert.ToInt32(dr["COMM_CNT"]);
                            if (Convert.ToInt32(dr["CAN_USER_EDIT"]) > 0)
                            {
                                o.IsGroupOwner = "Yes";
                            }
                            else
                            {
                                o.IsGroupOwner = "No";
                            }

                            DataRow[] drKeywords = dtKeywords.Select("GRP_ID=" + Convert.ToInt32(dr["GRP_ID"]));
                            List<UPSIKeywords> lstKeywords = new List<UPSIKeywords>();
                            foreach (DataRow drKeyword in drKeywords)
                            {
                                UPSIKeywords obj = new UPSIKeywords();
                                obj.keyword = Convert.ToString(drKeyword["KEYWORD"]);
                                obj.sequence = Convert.ToInt32(drKeyword["MATCH_ORDER"]);
                                lstKeywords.Add(obj);
                            }
                            o.Keywords = lstKeywords;
                            lstGrp.Add(o);
                        }
                        oGrp.UPSIGroups = lstGrp;
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse SaveUPSIGroup(UPSIGrp objUPSIGrp)
        {
            try
            {
                DataTable dtUpsiKeywords = new DataTable();
                dtUpsiKeywords.Columns.Add("Keyword", typeof(string));
                dtUpsiKeywords.Columns.Add("Seq", typeof(int));
                foreach (UPSIKeywords kword in objUPSIGrp.Keywords)
                {
                    DataRow dr = dtUpsiKeywords.NewRow();
                    dr["Keyword"] = kword.keyword;
                    dr["Seq"] = kword.sequence;
                    dtUpsiKeywords.Rows.Add(dr);
                }

                SqlParameter[] parameters = new SqlParameter[13];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                parameters[2] = new SqlParameter("@GROUP_NM", objUPSIGrp.GrpNm);
                parameters[3] = new SqlParameter("@TYPE_ID", objUPSIGrp.TypId);
                parameters[4] = new SqlParameter("@Mode", "CHECK");
                parameters[5] = new SqlParameter("@VALID_FROM", FormatHelper.FormatDate(objUPSIGrp.ValidFrom));
                if (String.IsNullOrEmpty(objUPSIGrp.ValidTo))
                {
                    parameters[6] = new SqlParameter("@VALID_TO", DBNull.Value);
                }
                else
                {
                    parameters[6] = new SqlParameter("@VALID_TO", FormatHelper.FormatDate(objUPSIGrp.ValidTo));
                }
                parameters[7] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                parameters[8] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                parameters[9] = new SqlParameter("@STATUS", objUPSIGrp.GrpStatus);
                parameters[10] = new SqlParameter("@GRP_DESC", objUPSIGrp.GrpDesc);
                parameters[11] = new SqlParameter("@REMARKS", objUPSIGrp.Remarks);
                parameters[12] = new SqlParameter("@UpsiKeywords", dtUpsiKeywords);

                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                UPSIGroupResponse oGrp = new UPSIGroupResponse();
                if ((Int32)obj == 0)
                {
                    if (objUPSIGrp.GrpId == 0)
                    {
                        parameters[4] = new SqlParameter("@Mode", "INSERT_GROUP");
                        var objX = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                        objUPSIGrp.GrpId = Convert.ToInt32(objX);


                        string _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                        "WHERE TEMPLATE_EVENT='Pre-Addition of Designated user in UPSI Group' AND COMPANY_ID=" + Convert.ToString(objUPSIGrp.CompanyId);
                        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                        DataTable dt = ds.Tables[0];

                        string sAdminDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                        string sInsiderDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ITDB"]), true);

                        _sql = "SELECT B.USER_EMAIL FROM PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) A " +
                            "INNER JOIN " + objUPSIGrp.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) B " +
                            "ON A.USER_LOGIN=B.LOGIN_ID WHERE A.USER_LOGIN='" + objUPSIGrp.CreatedBy + "' " +
                            "AND A.GRP_ID=" + Convert.ToString(objUPSIGrp.GrpId);
                        string sEmailId = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);

                        SqlParameter[] parametersX = new SqlParameter[9];
                        parametersX[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                        parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_DESIGNATED");
                        parametersX[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                        parametersX[4] = new SqlParameter("@GRPMEMBER", objUPSIGrp.CreatedBy);
                        parametersX[5] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                        parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dt.Rows[0]["TEMPLATE_ID"]));
                        parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dt.Rows[0]["TEMPLATE_SUBJECT"]));
                        parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dt.Rows[0]["TEMPLATE_BODY"]));

                        DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", objUPSIGrp.MODULE_DATABASE, parametersX);
                        string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                        EmailSender.SendMail(
                            sEmailId, Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                            Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                            Convert.ToString(HttpContext.Current.Session["CompanyId"]), sCC, Convert.ToString(objUPSIGrp.GrpId),
                            Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                        );
                    }
                    else
                    {
                        parameters[4] = new SqlParameter("@Mode", "UPDATE_GROUP");
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data has been saved successfully !";
                    oGrp.UPSIGroups = new List<UPSIGrp>();
                    oGrp.UPSIGroups.Add(objUPSIGrp);
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "Name of the UPSI Project already exist !";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse GetUPSIConnectedPersons(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_UPSI_GROUP_MEMBERS");
                parameters[2] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<ConnectedPerson> lstCP = new List<ConnectedPerson>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            ConnectedPerson o = new ConnectedPerson();
                            o.CPFirmNm = Convert.ToString(dr["FIRM"]);
                            o.CPEmail = Convert.ToString(dr["CP_EMAIL"]);
                            o.CPNm = Convert.ToString(dr["CP_NAME"]);
                            o.CPStatus = Convert.ToString(dr["CP_STATUS"]);
                            o.IdentificationId = Convert.ToString(dr["CP_IDENTIFICATION_NO"]);
                            o.IdentificationTyp = Convert.ToString(dr["CP_IDENTIFICATION_TYPE"]);
                            lstCP.Add(o);
                        }
                        objUPSIGrp.ConnectedPersons = lstCP;
                        oGrp.UPSIGroups = new List<UPSIGrp>();
                        oGrp.UPSIGroups.Add(objUPSIGrp);
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                    else
                    {
                        oGrp.StatusFl = true;
                        oGrp.Msg = "No data found";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse GetUPSIDesignatedPersons(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_UPSI_GROUP_DESIGNATED_MEMBERS");
                parameters[2] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                parameters[3] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<ConnectedPerson> lstCP = new List<ConnectedPerson>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            ConnectedPerson o = new ConnectedPerson();
                            o.CPEmail = Convert.ToString(dr["USER_EMAIL"]);
                            o.CPNm = Convert.ToString(dr["USER_NM"]);
                            lstCP.Add(o);
                        }
                        objUPSIGrp.ConnectedPersons = lstCP;
                        oGrp.UPSIGroups = new List<UPSIGrp>();
                        oGrp.UPSIGroups.Add(objUPSIGrp);
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                    else
                    {
                        oGrp.StatusFl = true;
                        oGrp.Msg = "No data found";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse SaveUPSIConnectedPersons(UPSIGrp objUPSIGrp)
        {
            try
            {
                DataTable dtConnectedPerson = new DataTable();
                dtConnectedPerson.Columns.Add("GRP_ID", typeof(int));
                dtConnectedPerson.Columns.Add("CP_NAME", typeof(String));
                dtConnectedPerson.Columns.Add("CP_EMAIL", typeof(String));
                dtConnectedPerson.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(String));
                dtConnectedPerson.Columns.Add("CP_IDENTIFICATION_NO", typeof(String));
                dtConnectedPerson.Columns.Add("CP_STATUS", typeof(String));
                dtConnectedPerson.Columns.Add("CP_TYPE", typeof(String));
                dtConnectedPerson.Columns.Add("CP_FIRM", typeof(String));

                foreach (ConnectedPerson cp in objUPSIGrp.ConnectedPersons)
                {
                    DataRow dr = dtConnectedPerson.NewRow();
                    dr["GRP_ID"] = objUPSIGrp.GrpId;
                    dr["CP_NAME"] = cp.CPNm;
                    dr["CP_EMAIL"] = cp.CPEmail;
                    dr["CP_IDENTIFICATION_TYPE"] = cp.IdentificationTyp;
                    dr["CP_IDENTIFICATION_NO"] = cp.IdentificationId;
                    dr["CP_STATUS"] = "Active";
                    dr["CP_TYPE"] = cp.CPType;
                    dr["CP_FIRM"] = cp.CPFirmNm;
                    dtConnectedPerson.Rows.Add(dr);
                }

                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "INSERT_CP_TABLE");
                parameters[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                parameters[3] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                parameters[4] = new SqlParameter("@CP", dtConnectedPerson);
                parameters[5] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);

                DataSet dsCP = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters
                );
                DataTable dtCP = dsCP.Tables[0];

                string _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                        "WHERE TEMPLATE_EVENT='Pre-Addition of Connected user in UPSI Group' " +
                        "AND COMPANY_ID=" + Convert.ToString(objUPSIGrp.CompanyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                DataTable dt = ds.Tables[0];

                foreach (DataRow drCP in dtCP.Rows)
                {
                    _sql = "SELECT B.USER_EMAIL FROM PROCS_INSIDER_UPSI_GROUP_CP(NOLOCK) A " +
                        "INNER JOIN " + objUPSIGrp.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) B " +
                        "ON A.USER_LOGIN=B.LOGIN_ID WHERE A.USER_LOGIN='" + drCP["CP_EMAIL"] + "' " +
                        "AND A.GRP_ID=" + Convert.ToString(objUPSIGrp.GrpId);
                    string sEmailId = Convert.ToString(drCP["CP_EMAIL"]);// (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);

                    SqlParameter[] parametersX = new SqlParameter[9];
                    parametersX[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                    parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_CONNECTED");
                    parametersX[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                    parametersX[4] = new SqlParameter("@GRPMEMBER", drCP["CP_EMAIL"]);
                    parametersX[5] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                    parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dt.Rows[0]["TEMPLATE_ID"]));
                    parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dt.Rows[0]["TEMPLATE_SUBJECT"]));
                    parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dt.Rows[0]["TEMPLATE_BODY"]));

                    DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", objUPSIGrp.MODULE_DATABASE, parametersX);
                    string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                    EmailSender.SendMail(
                        sEmailId, Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                        Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                        Convert.ToString(HttpContext.Current.Session["CompanyId"]), sCC, Convert.ToString(objUPSIGrp.GrpId),
                        Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                    );
                }
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = true;
                oGrpRes.Msg = "Data has been saved successfully !";
                return oGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = false;
                oGrpRes.Msg = "Processing failed, because of system error !";
                return oGrpRes;
            }
        }
        public UPSIGroupResponse SaveUPSIGroupCommunication(UPSIGrp objUPSIGrp)
        {
            try
            {
                foreach (ConnectedPerson cp in objUPSIGrp.ConnectedPersons)
                {
                    string sSharedAt = FormatHelper.FormatDate(objUPSIGrp.ValidFrom) + " " + objUPSIGrp.ValidTo;//ConvertDateX(objUPSIGrp.ValidFrom, objUPSIGrp.ValidTo).ToString("yyyy-MM-dd HH:mm:ss");
                    string sEmailTo = cp.CPEmail;

                    if (cp.IdentificationTyp == "DP")
                    {
                        string _sql = "SELECT USER_EMAIL FROM " + objUPSIGrp.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) " +
                            "WHERE LOGIN_ID='" + cp.CPEmail + "'";
                        sEmailTo = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                    }

                    SqlParameter[] parameters = new SqlParameter[12];
                    parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                    parameters[1] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                    parameters[2] = new SqlParameter("@MODE", "INSERT_COMMUNICATION");
                    parameters[3] = new SqlParameter("@SHARED_WITH", sEmailTo);
                    parameters[4] = new SqlParameter("@SHARED_ON", sSharedAt);
                    parameters[5] = new SqlParameter("@SHARED_MODE", objUPSIGrp.GrpNm);
                    parameters[6] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                    parameters[7] = new SqlParameter("@SHARED_DOC", objUPSIGrp.TypNm);
                    parameters[8] = new SqlParameter("@SHARED_BY", objUPSIGrp.SharedBy);
                    parameters[9] = new SqlParameter("@REMARKS", objUPSIGrp.GrpDesc);
                    parameters[10] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                    parameters[11] = new SqlParameter("@SHARED_FRM", objUPSIGrp.SharedFrm);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_COMMUNICATION", objUPSIGrp.MODULE_DATABASE, parameters);
                }
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = true;
                oGrpRes.Msg = "Data has been saved successfully !";
                return oGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = false;
                oGrpRes.Msg = "Processing failed, because of system error !";
                return oGrpRes;
            }
        }
        public UPSIGroupResponse GetTaskDetails(UPSITask objUPSITask)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSITask.CompanyId);
                parameters[1] = new SqlParameter("@MODE", "GET_UPSI_MESSAGE_DETAIL");
                parameters[2] = new SqlParameter("@TASK_ID", objUPSITask.TaskId);
                parameters[3] = new SqlParameter("@ADMIN_DB", objUPSITask.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[4].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_MEMBERS_DETAL_LIST", objUPSITask.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<ConnectedPerson> lstCP = new List<ConnectedPerson>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            ConnectedPerson o = new ConnectedPerson();
                            o.CPEmail = Convert.ToString(dr["EMAIL_ID"]);
                            o.CPNm = Convert.ToString(dr["USER_NM"]);
                            o.IdentificationId = Convert.ToString(dr["IDENTIFICATION_NO"]);
                            o.IdentificationTyp = Convert.ToString(dr["IDENTIFICATION_TYPE"]);
                            o.CPStatus = Convert.ToString(dr["USER_TYP"]);
                            o.CPFirmNm = Convert.ToString(dr["FIRM"]);
                            lstCP.Add(o);
                        }
                        UPSIGrp objGrp = new UPSIGrp();
                        objGrp.GrpId = Convert.ToInt32(dt.Rows[0]["GROUP_ID"]);
                        objGrp.ConnectedPersons = lstCP;
                        oGrp.UPSIGroups = new List<UPSIGrp>();
                        oGrp.UPSIGroups.Add(objGrp);
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                    else
                    {
                        oGrp.StatusFl = true;
                        oGrp.Msg = "No Data found";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse DiscardUPSITask(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "DISCARD_CP_TASK");
                parameters[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                parameters[3] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                parameters[4] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                parameters[5] = new SqlParameter("@REMARKS", objUPSIGrp.Remarks);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);

                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = true;
                oGrpRes.Msg = "Data has been saved successfully !";
                return oGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = false;
                oGrpRes.Msg = "Processing failed, because of system error !";
                return oGrpRes;
            }
        }
        public UPSIGroupResponse UpdateUPSITask(UPSIGrp objUPSIGrp)
        {
            try
            {
                DataTable dtConnectedPerson = new DataTable();
                dtConnectedPerson.Columns.Add("GRP_ID", typeof(int));
                dtConnectedPerson.Columns.Add("CP_NAME", typeof(String));
                dtConnectedPerson.Columns.Add("CP_EMAIL", typeof(String));
                dtConnectedPerson.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(String));
                dtConnectedPerson.Columns.Add("CP_IDENTIFICATION_NO", typeof(String));
                dtConnectedPerson.Columns.Add("CP_STATUS", typeof(String));
                dtConnectedPerson.Columns.Add("CP_TYPE", typeof(String));
                dtConnectedPerson.Columns.Add("CP_FIRM", typeof(String));

                foreach (ConnectedPerson cp in objUPSIGrp.ConnectedPersons)
                {
                    DataRow dr = dtConnectedPerson.NewRow();
                    dr["GRP_ID"] = Convert.ToInt32(cp.CPStatus);
                    dr["CP_NAME"] = cp.CPNm;
                    dr["CP_EMAIL"] = cp.CPEmail;
                    dr["CP_IDENTIFICATION_TYPE"] = cp.IdentificationTyp;
                    dr["CP_IDENTIFICATION_NO"] = cp.IdentificationId;
                    dr["CP_STATUS"] = "Active";
                    dr["CP_TYPE"] = cp.CPType;
                    dr["CP_FIRM"] = cp.CPFirmNm;
                    dtConnectedPerson.Rows.Add(dr);
                }

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "INSERT_CP_TASK");
                parameters[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                parameters[3] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                parameters[4] = new SqlParameter("@CP", dtConnectedPerson);
                parameters[5] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                parameters[6] = new SqlParameter("@REMARKS", objUPSIGrp.Remarks);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);

                string _sql = "SELECT A.GRP_ID,CASE WHEN B.USER_EMAIL IS NOT NULL THEN B.USER_EMAIL ELSE C.CP_EMAIL END AS EMAIL_TO," +
                    "CASE WHEN B.USER_EMAIL IS NOT NULL THEN B.LOGIN_ID ELSE '' END AS USER_ID," +
                    "CASE WHEN B.USER_EMAIL IS NOT NULL THEN 'DP' ELSE 'CP' END AS USER_TYP FROM UPSI_GROUP_MEMBER_CO_TASK_RECIPIENTS(NOLOCK) A " +
                    "LEFT JOIN " + objUPSIGrp.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) B ON A.EMAIL=B.USER_EMAIL " +
                    "LEFT JOIN PROCS_INSIDER_CONNECTED_PERSONS(NOLOCK) C ON A.EMAIL=C.CP_EMAIL " +
                    "WHERE A.TASK_ID=" + objUPSIGrp.GrpId + " AND A.RECIPIENT_FL='Manual'";
                DataSet dsUsr = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                DataTable dtUsr = dsUsr.Tables[0];
                if (dtUsr.Rows.Count > 0)
                {
                    foreach (DataRow drUsr in dtUsr.Rows)
                    {
                        if (Convert.ToString(drUsr["USER_TYP"]) == "DP")
                        {
                            _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                                "WHERE TEMPLATE_EVENT='Pre-Addition of Designated user in UPSI Group' "+
                                "AND COMPANY_ID=" + Convert.ToString(objUPSIGrp.CompanyId);
                            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                            DataTable dt = ds.Tables[0];

                            SqlParameter[] parametersX = new SqlParameter[9];
                            parametersX[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                            parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_DESIGNATED");
                            parametersX[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                            parametersX[4] = new SqlParameter("@GRPMEMBER", Convert.ToString(drUsr["USER_ID"]));
                            parametersX[5] = new SqlParameter("@GROUP_ID", Convert.ToString(drUsr["GRP_ID"]));
                            parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dt.Rows[0]["TEMPLATE_ID"]));
                            parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dt.Rows[0]["TEMPLATE_SUBJECT"]));
                            parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dt.Rows[0]["TEMPLATE_BODY"]));

                            DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", objUPSIGrp.MODULE_DATABASE, parametersX);
                            string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                            EmailSender.SendMail(
                                Convert.ToString(drUsr["EMAIL_TO"]), Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                                Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                                Convert.ToString(HttpContext.Current.Session["CompanyId"]), sCC, Convert.ToString(objUPSIGrp.GrpId),
                                Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                            );
                        }
                        else if (Convert.ToString(drUsr["USER_TYP"]) == "CP")
                        {
                            string sEmailId = Convert.ToString(drUsr["EMAIL_TO"]);

                            _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                                "WHERE TEMPLATE_EVENT='Pre-Addition of Connected user in UPSI Group' " +
                                "AND COMPANY_ID=" + Convert.ToString(objUPSIGrp.CompanyId);
                            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                            DataTable dt = ds.Tables[0];

                            SqlParameter[] parametersX = new SqlParameter[9];
                            parametersX[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                            parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_CONNECTED");
                            parametersX[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                            parametersX[4] = new SqlParameter("@GRPMEMBER", Convert.ToString(drUsr["EMAIL_TO"]));
                            parametersX[5] = new SqlParameter("@GROUP_ID", Convert.ToString(drUsr["GRP_ID"]));
                            parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dt.Rows[0]["TEMPLATE_ID"]));
                            parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dt.Rows[0]["TEMPLATE_SUBJECT"]));
                            parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dt.Rows[0]["TEMPLATE_BODY"]));

                            DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", objUPSIGrp.MODULE_DATABASE, parametersX);
                            string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                            EmailSender.SendMail(
                                sEmailId, Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                                Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                                Convert.ToString(HttpContext.Current.Session["CompanyId"]), sCC, Convert.ToString(objUPSIGrp.GrpId),
                                Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                            );
                        }
                    }
                }


                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = true;
                oGrpRes.Msg = "Data has been saved successfully !";
                return oGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = false;
                oGrpRes.Msg = "Processing failed, because of system error !";
                return oGrpRes;
            }
        }
        public UPSIRptResponse GetUPSIReport(
            string sUPSIGrpId, string sUserId, string sFrmDt, string sToDt,
            string sModuleDatabase, string sCompanyId, string sEmployeeId, string sAdminDb
        )
        {
            try
            {
               
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
              
                      
                UPSIRptResponse oGrp = new UPSIRptResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<UPSIRpt> lstRpt = new List<UPSIRpt>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            UPSIRpt o = new UPSIRpt();
                            o.CommMode = Convert.ToString(dr["MODE_OF_SHARING"]);
                            o.CreatedOn = dr["CREATED_ON"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["CREATED_ON"]).ToString(sDtFormat);
                            o.CreatedTm = dr["CREATED_ON"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["CREATED_ON"]).ToString(sTmFormat);
                            o.FirmNm = Convert.ToString(dr["ORGANIZATION_NM"]);
                            o.EmailIDofSender= Convert.ToString(dr["COMM_FROM"]);
                            //o.CreatedTm = Convert.ToDateTime(dr["CREATED_ON"]).ToString(sTmFormat);

                            o.NoticeSent = "Yes";
                            o.Remarks = Convert.ToString(dr["REMARKS"]);
                            o.SharedBy = Convert.ToString(dr["SHARED_BY"]);
                            o.SharedByIdentification = Convert.ToString(dr["SHARED_BY_IDENTIFICATION"]);
                            o.SharedOn= Convert.ToDateTime(dr["EMAIL_DATE"]).ToString(sDtFormat);
                            o.SharedTime= Convert.ToDateTime(dr["EMAIL_DATE"]).ToString(sTmFormat);
                            o.SharedWith = Convert.ToString(dr["SHARED_WITH"]);
                            o.SharedWithIdentification = Convert.ToString(dr["SHARED_WITH_IDENTIFICATION"]);
                            o.UpsiTyp = Convert.ToString(dr["TYP_NM"]);
                            o.ValidFrm= Convert.ToDateTime(dr["VALID_FROM"]).ToString(sDtFormat);
                            o.DateofentryinSDD =dr["DateofentryinSDD"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["DateofentryinSDD"]).ToString(sDtFormat);
                            o.TimestampforB = dr["DateofentryinSDD"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["DateofentryinSDD"]).ToString(sTmFormat);
                            o.UPSIReportedthrough = Convert.ToString(dr["UPSIReportedthrough"]);
                            o.Attachment = Convert.ToString(dr["Attachment"]);
                            lstRpt.Add(o);
                        }
                        oGrp.lstRpt = lstRpt;
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                    else
                    {
                        oGrp.StatusFl = true;
                        oGrp.Msg = "No Data found";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIRptResponse oRel = new UPSIRptResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse SaveUPSIGroupMembers(UPSIGrp objUPSIGrp)
        {
            try
            {
                DataTable dtGrpMembers = new DataTable();
                dtGrpMembers.Columns.Add("GRP_ID", typeof(int));
                dtGrpMembers.Columns.Add("USER_ID", typeof(String));
                dtGrpMembers.Columns.Add("CREATED_BY", typeof(String));
                foreach (ConnectedPerson cm in objUPSIGrp.ConnectedPersons)
                {
                    DataRow dr = dtGrpMembers.NewRow();
                    dr["GRP_ID"] = objUPSIGrp.GrpId;
                    dr["USER_ID"] = cm.CPNm;
                    dr["CREATED_BY"] = objUPSIGrp.CreatedBy;
                    dtGrpMembers.Rows.Add(dr);
                }

                if (dtGrpMembers.Rows.Count > 0)
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                    parameters[1] = new SqlParameter("@Mode", "INSERT_GRP_MEMBER");
                    parameters[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                    parameters[4] = new SqlParameter("@GRPMEMBERS", dtGrpMembers);
                    parameters[5] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                    parameters[6] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);

                    string _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                        "WHERE TEMPLATE_EVENT='Pre-Addition of Designated user in UPSI Group' AND COMPANY_ID=" + Convert.ToString(objUPSIGrp.CompanyId);
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow drGrpMembers in dtGrpMembers.Rows)
                    {
                        _sql = "SELECT B.USER_EMAIL FROM PROCS_INSIDER_UPSI_GROUP_DP(NOLOCK) A " +
                            "INNER JOIN " + objUPSIGrp.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) B " +
                            "ON A.USER_LOGIN=B.LOGIN_ID WHERE A.USER_LOGIN='" + drGrpMembers["USER_ID"] + "' " +
                            "AND A.GRP_ID=" + Convert.ToString(objUPSIGrp.GrpId);
                        string sEmailId = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objUPSIGrp.MODULE_DATABASE, null);

                        SqlParameter[] parametersX = new SqlParameter[9];
                        parametersX[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                        parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_DESIGNATED");
                        parametersX[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                        parametersX[4] = new SqlParameter("@GRPMEMBER", drGrpMembers["USER_ID"]);
                        parametersX[5] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                        parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dt.Rows[0]["TEMPLATE_ID"]));
                        parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dt.Rows[0]["TEMPLATE_SUBJECT"]));
                        parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dt.Rows[0]["TEMPLATE_BODY"]));

                        DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", objUPSIGrp.MODULE_DATABASE, parametersX);
                        string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                        EmailSender.SendMail(
                            sEmailId, Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                            Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                            Convert.ToString(HttpContext.Current.Session["CompanyId"]), sCC, Convert.ToString(objUPSIGrp.GrpId),
                            Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                        );
                    }
                    UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                    oGrpRes.StatusFl = true;
                    oGrpRes.Msg = "Data has been saved successfully !";
                    return oGrpRes;
                }
                else
                {
                    UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                    oGrpRes.StatusFl = false;
                    oGrpRes.Msg = "Processing failed, Please select users !";
                    return oGrpRes;
                }

            }
            catch (Exception ex)
            {
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = false;
                oGrpRes.Msg = "Processing failed, because of system error !";
                return oGrpRes;
            }
        }
        public UPSIGroupResponse DeleteUPSIGroupMember(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                parameters[1] = new SqlParameter("@Mode", "DELETE_UPSI_GROUP_MEMBERS_ACCESS");
                parameters[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);


                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                UPSIGroupResponse oGrp = new UPSIGroupResponse();
                oGrp.StatusFl = true;
                oGrp.Msg = "User Removed successfully !";
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse GetUPSIGroupMember(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_MEMBERS_ACCESS");
                parameters[2] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                parameters[3] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        List<ConnectedPerson> lstCP = new List<ConnectedPerson>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            ConnectedPerson o = new ConnectedPerson();
                            o.CPNm = Convert.ToString(dr["USER_NM"]) + " (" + Convert.ToString(dr["USER_LOGIN"]) + ")";
                            o.MapId = Convert.ToString(dr["MAPID"]);
                            o.CPType = Convert.ToString(dr["GRP_OWNER"]);
                            lstCP.Add(o);
                        }
                        UPSIGrp objGrp = new UPSIGrp();
                        objGrp.ConnectedPersons = lstCP;
                        oGrp.UPSIGroups = new List<UPSIGrp>();
                        oGrp.UPSIGroups.Add(objGrp);
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                    else
                    {
                        oGrp.StatusFl = true;
                        oGrp.Msg = "No Data found";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse GetUPSIGrpAuditLog(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LOG");
                parameters[2] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_LOG", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                DataTable dt1 = ds.Tables[1];
                DataTable dt2 = ds.Tables[2];
                DataTable dt3 = ds.Tables[3];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<UPSIGrp> lstGrp = new List<UPSIGrp>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            UPSIGrp o = new UPSIGrp();
                            o.GrpId = Convert.ToInt32(dr["GRP_ID"]);
                            o.GrpNm = Convert.ToString(dr["GRP_NAME"]);
                            o.GrpStatus = Convert.ToString(dr["GRP_STATUS"]);
                            o.TypId = Convert.ToInt32(dr["TYP_ID"]);
                            o.TypNm = Convert.ToString(dr["TYP_NM"]);
                            o.ValidFrom = Convert.ToString(dr["VALID_FROM"]);
                            o.ValidTo = Convert.ToString(dr["VALID_TO"]);
                            o.GrpDesc = Convert.ToString(dr["GRP_DESC"]);
                            lstGrp.Add(o);
                        }
                        oGrp.UPSIGroups = lstGrp;
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                    else
                    {
                        oGrp.StatusFl = true;
                        oGrp.Msg = "No Data found";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
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
        private DateTime ConvertDateX(String date, String time)
        {
            String str = String.Empty;
            try
            {
                //if (date.Contains("/"))
                //{
                //    str = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0] + " " + time;
                //}
                return new DateTime(
                        Convert.ToInt32(date.Split('/')[2]),
                        Convert.ToInt32(date.Split('/')[1]),
                        Convert.ToInt32(date.Split('/')[0]),

                        Convert.ToInt32(time.Split(':')[0]),
                        Convert.ToInt32(time.Split(':')[1]),
                        0
                        );
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }

            return Convert.ToDateTime(str);
        }
        public UPSIGroupResponse GetAllUPSIGroups(UPSIGrp objUPSIGrp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_ALL_UPSI_GROUP");
                parameters[2] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];
                UPSIGroupResponse oGrp = new UPSIGroupResponse();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<UPSIGrp> lstGrp = new List<UPSIGrp>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            UPSIGrp o = new UPSIGrp();
                            o.GrpId = Convert.ToInt32(dr["GRP_ID"]);
                            o.GrpNm = Convert.ToString(dr["GRP_NAME"]);
                            o.GrpStatus = Convert.ToString(dr["GRP_STATUS"]);
                            o.TypId = Convert.ToInt32(dr["TYP_ID"]);
                            o.TypNm = Convert.ToString(dr["TYP_NM"]);
                            o.ValidFrom = Convert.ToString(dr["VALID_FROM"]);
                            o.ValidTo = Convert.ToString(dr["VALID_TO"]);
                            o.GrpDesc = Convert.ToString(dr["GRP_DESC"]);
                            lstGrp.Add(o);
                        }
                        oGrp.UPSIGroups = lstGrp;
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Success";
                    }
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No UPSI type defined, Please defined UPSI";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oRel = new UPSIGroupResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public UPSIGroupResponse AddUPSITaskDP(UPSIGrp objUPSIGrp)
        {
            try
            {
                DataTable dtGrpMembers = new DataTable();
                dtGrpMembers.Columns.Add("GRP_ID", typeof(int));
                dtGrpMembers.Columns.Add("USER_ID", typeof(String));
                dtGrpMembers.Columns.Add("CREATED_BY", typeof(String));
                foreach (ConnectedPerson cm in objUPSIGrp.ConnectedPersons)
                {
                    DataRow dr = dtGrpMembers.NewRow();
                    dr["GRP_ID"] = objUPSIGrp.GrpId;
                    dr["USER_ID"] = cm.CPNm;
                    dr["CREATED_BY"] = objUPSIGrp.CreatedBy;
                    dtGrpMembers.Rows.Add(dr);
                }
                if (dtGrpMembers.Rows.Count > 0)
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIGrp.CompanyId);
                    parameters[1] = new SqlParameter("@Mode", "ADD_DP_FOR_UPSI_TASK");
                    parameters[2] = new SqlParameter("@ADMIN_DB", objUPSIGrp.ADMIN_DATABASE);
                    parameters[4] = new SqlParameter("@GRPMEMBERS", dtGrpMembers);
                    parameters[5] = new SqlParameter("@CREATED_BY", objUPSIGrp.CreatedBy);
                    parameters[6] = new SqlParameter("@GROUP_ID", objUPSIGrp.GrpId);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP", objUPSIGrp.MODULE_DATABASE, parameters);

                    UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                    oGrpRes.StatusFl = true;
                    oGrpRes.Msg = "Data has been saved successfully !";
                    return oGrpRes;
                }
                else
                {
                    UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                    oGrpRes.StatusFl = false;
                    oGrpRes.Msg = "Processing failed, Please select users !";
                    return oGrpRes;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse oGrpRes = new UPSIGroupResponse();
                oGrpRes.StatusFl = false;
                oGrpRes.Msg = "Processing failed, because of system error !";
                return oGrpRes;
            }
        }
    }
}