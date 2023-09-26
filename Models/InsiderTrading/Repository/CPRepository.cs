using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class CPRepository : IRequiresSessionState
    {
        public CPResponse GetCPUserList(CP objCP)
        {
            CPResponse cpRes = new CPResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Mode", "GET_CP_LIST");
                DataSet ds = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", objCP.MODULE_DATABASE, parameters
                );
                List<CP> lstCP = new List<CP>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CP obj = new CP();
                            obj.CPEmail = Convert.ToString(dr["CP_EMAIL"]);
                            obj.CPFirm = Convert.ToString(dr["FIRM"]);
                            obj.CPIdentificationNo = Convert.ToString(dr["CP_IDENTIFICATION_NO"]);
                            obj.CPIdentificationTyp = Convert.ToString(dr["CP_IDENTIFICATION_TYPE"]);
                            obj.CPName = Convert.ToString(dr["CP_NAME"]);
                            obj.CPStatus = Convert.ToString(dr["CP_STATUS"]);
                            lstCP.Add(obj);
                        }
                        cpRes.CPList = lstCP;
                    }
                    cpRes.StatusFl= true;
                    cpRes.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    cpRes.StatusFl = false;
                    cpRes.Msg = "No data found !";
                }
                return cpRes;
            }
            catch (Exception ex)
            {
                cpRes.StatusFl = false;
                cpRes.Msg = "Processing failed, because of system error !";
                return cpRes;
            }
        }
        public CPResponse GetUPSIGroupCP(UPSIGrp objGrp)
        {
            CPResponse cpRes = new CPResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_CP_LIST_BY_GROUP");
                parameters[1] = new SqlParameter("@GrpId", objGrp.GrpId);
                DataSet ds = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", objGrp.MODULE_DATABASE, parameters
                );
                List<CP> lstCP = new List<CP>();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CP obj = new CP();
                            obj.CPEmail = Convert.ToString(dr["CP_EMAIL"]);
                            obj.CPFirm = Convert.ToString(dr["FIRM"]);
                            obj.CPIdentificationNo = Convert.ToString(dr["CP_IDENTIFICATION_NO"]);
                            obj.CPIdentificationTyp = Convert.ToString(dr["CP_IDENTIFICATION_TYPE"]);
                            obj.CPName = Convert.ToString(dr["CP_NAME"]);
                            //obj.CPStatus = Convert.ToString(dr["CP_STATUS"]);
                            lstCP.Add(obj);
                        }
                        cpRes.CPList = lstCP;
                    }
                    cpRes.StatusFl = true;
                    cpRes.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    cpRes.StatusFl = false;
                    cpRes.Msg = "No data found !";
                }
                return cpRes;
            }
            catch (Exception ex)
            {
                cpRes.StatusFl = false;
                cpRes.Msg = "Processing failed, because of system error !";
                return cpRes;
            }
        }
        public CPResponse SaveConnectedPersons(List<CP> lstCP)
        {
            CPResponse cpRes = new CPResponse();
            try
            {
                DataTable dtPersons = new DataTable();
                dtPersons.Columns.Add("CP_NAME", typeof(string));
                dtPersons.Columns.Add("CP_EMAIL", typeof(string));
                dtPersons.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(string));
                dtPersons.Columns.Add("CP_IDENTIFICATION_NO", typeof(string));
                dtPersons.Columns.Add("CP_STATUS", typeof(string));
                dtPersons.Columns.Add("CP_FIRM", typeof(string));

                foreach(CP cp in lstCP)
                {
                    DataRow dr = dtPersons.NewRow();
                    dr["CP_NAME"] = cp.CPName;
                    dr["CP_EMAIL"] = cp.CPEmail;
                    dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                    dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                    dr["CP_STATUS"] = cp.CPStatus;
                    dr["CP_FIRM"] = cp.CPFirm;
                    dtPersons.Rows.Add(dr);
                }

                string sModuleDb = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string sCompanyId = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                string sCreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "CHECK_CP");
                //parameters[0] = new SqlParameter("@Mode", "INSERT_CP");
                parameters[1] = new SqlParameter("@dtInsider", dtPersons);
                parameters[2] = new SqlParameter("@CreatedBy", sCreatedBy);
                DataSet ds = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                );

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    parameters[0] = new SqlParameter("@Mode", "INSERT_CP");
                    SQLHelper.ExecuteNonQuery(
                        SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                    );
                    cpRes.StatusFl = true;
                    cpRes.Msg = "Data saved successfully !";
                }
                else
                {
                    cpRes.StatusFl = false;
                    List<CP> cpLst = new List<CP>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        CP o = new CP();
                        o.CPEmail = Convert.ToString(dr["CP_EMAIL"]);
                        o.CPFirm = Convert.ToString(dr["CP_FIRM"]);
                        o.CPIdentificationNo = Convert.ToString(dr["CP_IDENTIFICATION_NO"]);
                        o.CPIdentificationTyp = Convert.ToString(dr["CP_IDENTIFICATION_TYPE"]);
                        o.CPName = Convert.ToString(dr["CP_NAME"]);
                        o.CPConflict = Convert.ToString(dr["CONFLICT"]);
                        o.CPConflictFrm = Convert.ToString(dr["CONFLICT_FROM"]);
                        cpLst.Add(o);
                    }
                    cpRes.CPList = cpLst;
                    cpRes.Msg = "Conflict exists";
                }
                return cpRes;
            }
            catch (Exception ex)
            {
                cpRes.StatusFl = false;
                cpRes.Msg = "Processing failed, because of system error !";
                return cpRes;
            }
        }
        public CPResponse SaveConnectedPersonsForUPSITask(List<CP> lstCP)
        {
            CPResponse cpRes = new CPResponse();
            try
            {
                DataTable dtPersons = new DataTable();
                dtPersons.Columns.Add("CP_NAME", typeof(string));
                dtPersons.Columns.Add("CP_EMAIL", typeof(string));
                dtPersons.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(string));
                dtPersons.Columns.Add("CP_IDENTIFICATION_NO", typeof(string));
                dtPersons.Columns.Add("CP_STATUS", typeof(string));
                dtPersons.Columns.Add("CP_FIRM", typeof(string));

                foreach (CP cp in lstCP)
                {
                    if (cp.CPStatus.Split(new char[] { '~' })[0] == "New")
                    {
                        DataRow dr = dtPersons.NewRow();
                        dr["CP_NAME"] = cp.CPName;
                        dr["CP_EMAIL"] = cp.CPEmail;
                        dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                        dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                        dr["CP_STATUS"] = "Active";
                        dr["CP_FIRM"] = cp.CPFirm;
                        dtPersons.Rows.Add(dr);
                    }
                }
                string sModuleDb = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string sCompanyId = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                string sCreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (dtPersons.Rows.Count > 0)
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@Mode", "CHECK_CP");
                    parameters[1] = new SqlParameter("@dtInsider", dtPersons);
                    parameters[2] = new SqlParameter("@CreatedBy", sCreatedBy);
                    DataSet ds = SQLHelper.ExecuteDataset(
                        SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                    );

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        parameters = new SqlParameter[4];
                        parameters[0] = new SqlParameter("@Mode", "INSERT_CP_FOR_UPSI_TASK");

                        dtPersons = new DataTable();
                        dtPersons.Columns.Add("CP_NAME", typeof(string));
                        dtPersons.Columns.Add("CP_EMAIL", typeof(string));
                        dtPersons.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(string));
                        dtPersons.Columns.Add("CP_IDENTIFICATION_NO", typeof(string));
                        dtPersons.Columns.Add("CP_STATUS", typeof(string));
                        dtPersons.Columns.Add("CP_FIRM", typeof(string));

                        foreach (CP cp in lstCP)
                        {
                            if (cp.CPStatus.Split(new char[] { '~' })[0] == "New")
                            {
                                DataRow dr = dtPersons.NewRow();
                                dr["CP_NAME"] = cp.CPName;
                                dr["CP_EMAIL"] = cp.CPEmail;
                                dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                                dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                                dr["CP_STATUS"] = "Active";
                                dr["CP_FIRM"] = cp.CPFirm;
                                dtPersons.Rows.Add(dr);
                                parameters[1] = new SqlParameter("@GrpId", cp.CPStatus.Split(new char[] { '~' })[1]);
                            }
                            else
                            {
                                DataRow dr = dtPersons.NewRow();
                                dr["CP_NAME"] = cp.CPName;
                                dr["CP_EMAIL"] = cp.CPEmail;
                                dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                                dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                                dr["CP_STATUS"] = "Existing";
                                dr["CP_FIRM"] = cp.CPFirm;
                                dtPersons.Rows.Add(dr);
                                parameters[1] = new SqlParameter("@GrpId", cp.CPStatus.Split(new char[] { '~' })[1]);
                            }
                        }
                        parameters[2] = new SqlParameter("@dtInsider", dtPersons);
                        parameters[3] = new SqlParameter("@CreatedBy", sCreatedBy);

                        SQLHelper.ExecuteNonQuery(
                            SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                        );
                        cpRes.StatusFl = true;
                        cpRes.Msg = "Data saved successfully !";
                    }
                    else
                    {
                        cpRes.StatusFl = false;
                        List<CP> cpLst = new List<CP>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            CP o = new CP();
                            o.CPEmail = Convert.ToString(dr["CP_EMAIL"]);
                            o.CPFirm = Convert.ToString(dr["CP_FIRM"]);
                            o.CPIdentificationNo = Convert.ToString(dr["CP_IDENTIFICATION_NO"]);
                            o.CPIdentificationTyp = Convert.ToString(dr["CP_IDENTIFICATION_TYPE"]);
                            o.CPName = Convert.ToString(dr["CP_NAME"]);
                            o.CPConflict = Convert.ToString(dr["CONFLICT"]);
                            o.CPConflictFrm = Convert.ToString(dr["CONFLICT_FROM"]);
                            cpLst.Add(o);
                        }
                        cpRes.CPList = cpLst;
                        cpRes.Msg = "Conflict exists";
                    }
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@Mode", "INSERT_CP_FOR_UPSI_TASK");

                    dtPersons = new DataTable();
                    dtPersons.Columns.Add("CP_NAME", typeof(string));
                    dtPersons.Columns.Add("CP_EMAIL", typeof(string));
                    dtPersons.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(string));
                    dtPersons.Columns.Add("CP_IDENTIFICATION_NO", typeof(string));
                    dtPersons.Columns.Add("CP_STATUS", typeof(string));
                    dtPersons.Columns.Add("CP_FIRM", typeof(string));

                    foreach (CP cp in lstCP)
                    {
                        if (cp.CPStatus.Split(new char[] { '~' })[0] == "New")
                        {
                            DataRow dr = dtPersons.NewRow();
                            dr["CP_NAME"] = cp.CPName;
                            dr["CP_EMAIL"] = cp.CPEmail;
                            dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                            dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                            dr["CP_STATUS"] = "Active";
                            dr["CP_FIRM"] = cp.CPFirm;
                            dtPersons.Rows.Add(dr);
                            parameters[1] = new SqlParameter("@GrpId", cp.CPStatus.Split(new char[] { '~' })[1]);
                        }
                        else
                        {
                            DataRow dr = dtPersons.NewRow();
                            dr["CP_NAME"] = cp.CPName;
                            dr["CP_EMAIL"] = cp.CPEmail;
                            dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                            dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                            dr["CP_STATUS"] = "Existing";
                            dr["CP_FIRM"] = cp.CPFirm;
                            dtPersons.Rows.Add(dr);
                            parameters[1] = new SqlParameter("@GrpId", cp.CPStatus.Split(new char[] { '~' })[1]);
                        }
                    }
                    parameters[2] = new SqlParameter("@dtInsider", dtPersons);
                    parameters[3] = new SqlParameter("@CreatedBy", sCreatedBy);

                    SQLHelper.ExecuteNonQuery(
                        SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                    );
                    cpRes.StatusFl = true;
                    cpRes.Msg = "Data saved successfully !";
                }
                return cpRes;
            }
            catch (Exception ex)
            {
                cpRes.StatusFl = false;
                cpRes.Msg = "Processing failed, because of system error !";
                return cpRes;
            }
        }
        public CPResponse UploadConnectedPersons()
        {
            CPResponse cpRes = new CPResponse();
            try
            {
                string sModuleDb = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string sCompanyId = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                string sCreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "UPLOAD_CP");
                parameters[1] = new SqlParameter("@CreatedBy", sCreatedBy);
                DataSet ds1 = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                );
                cpRes.StatusFl = true;
                cpRes.Msg = "Data saved successfully !";
                return cpRes;
            }
            catch (Exception ex)
            {
                cpRes.StatusFl = false;
                cpRes.Msg = "Processing failed, because of system error !";
                return cpRes;
            }
        }
        public CPResponse SaveNewConnectedPersons(List<CP> lstCP)
        {
            CPResponse cpRes = new CPResponse();
            try
            {
                DataTable dtPersons = new DataTable();
                dtPersons.Columns.Add("CP_NAME", typeof(string));
                dtPersons.Columns.Add("CP_EMAIL", typeof(string));
                dtPersons.Columns.Add("CP_IDENTIFICATION_TYPE", typeof(string));
                dtPersons.Columns.Add("CP_IDENTIFICATION_NO", typeof(string));
                dtPersons.Columns.Add("CP_STATUS", typeof(string));
                dtPersons.Columns.Add("CP_FIRM", typeof(string));
                foreach (CP cp in lstCP)
                {
                    DataRow dr = dtPersons.NewRow();
                    dr["CP_NAME"] = cp.CPName;
                    dr["CP_EMAIL"] = cp.CPEmail;
                    dr["CP_IDENTIFICATION_TYPE"] = cp.CPIdentificationTyp;
                    dr["CP_IDENTIFICATION_NO"] = cp.CPIdentificationNo;
                    dr["CP_STATUS"] = cp.CPStatus;
                    dr["CP_FIRM"] = cp.CPFirm;
                    dtPersons.Rows.Add(dr);
                }
                string sModuleDb = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string sCompanyId = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                string sCreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "INSERT_CP");
                parameters[1] = new SqlParameter("@dtInsider", dtPersons);
                parameters[2] = new SqlParameter("@CreatedBy", sCreatedBy);
                DataSet ds = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CP_OPERATION", sModuleDb, parameters
                );
                string _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                        "WHERE TEMPLATE_EVENT='Addition of Connected user in UPSI' " +
                        "AND COMPANY_ID=" + sCompanyId;

                DataSet dsCP = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, sModuleDb, null);
                DataTable dt = dsCP.Tables[0];
                for (int x = 0; x < dtPersons.Rows.Count; x++)
                {
                    string toEmailId = dtPersons.Rows[x]["CP_EMAIL"].ToString();
                    EmailSender.SendMail(
                            toEmailId, Convert.ToString(dsCP.Tables[0].Rows[0]["TEMPLATE_SUBJECT"]),
                            Convert.ToString(dsCP.Tables[0].Rows[0]["TEMPLATE_BODY"]), null, "Inclusion IN UPSI",
                            Convert.ToString(HttpContext.Current.Session["CompanyId"]), "", "",
                            Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                        );
                }
                cpRes.StatusFl = true;
                cpRes.Msg = "Data saved successfully !";
                return cpRes;
            }
            catch (Exception ex)
            {
                cpRes.StatusFl = false;
                cpRes.Msg = "Processing failed, because of system error !";
                return cpRes;
            }
        }
    }
}