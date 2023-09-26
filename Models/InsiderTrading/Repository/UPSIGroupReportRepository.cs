using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UPSIGroupReportRepository
    {


        public UPSIGroupReportResponse GetUPSIGroupList(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_OWER", objgroup.CreatedBy);
                parameters[4] = new SqlParameter("@VALID_FROM", objgroup.VALID_FROM);
                parameters[5] = new SqlParameter("@VALID_TLL", objgroup.VALID_TLL);
                parameters[6] = new SqlParameter("@GROUP_ID", objgroup.GROUP_ID);


                UPSIGroupReportResponse responce = new UPSIGroupReportResponse();

                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<UPSIMembersGroup> listUPSIGroup = new List<UPSIMembersGroup>();
                    while (reader.Read())
                    {

                        UPSIMembersGroup item = new UPSIMembersGroup();
                        item.GROUP_ID = !string.IsNullOrEmpty(reader["GROUP_ID"].ToString()) ? Convert.ToInt32(reader["GROUP_ID"]) : 0;
                        item.GROUP_NM = !string.IsNullOrEmpty(reader["GROUP_NM"].ToString()) ? Convert.ToString(reader["GROUP_NM"]) : string.Empty;
                        item.TotalMembers = !string.IsNullOrEmpty(reader["TotalMembers"].ToString()) ? Convert.ToString(reader["TotalMembers"]) : string.Empty;
                        DateTime DT = Convert.ToDateTime(reader["VALID_FROM"]);
                        item.VALID_FROM = DT.ToString("dd/MM/yyyy");
                        DateTime dt1 = Convert.ToDateTime(reader["VALID_TLL"]);
                        item.VALID_TLL = dt1.ToString("dd/MM/yyyy");
                        item.CreatedBy = !string.IsNullOrEmpty(reader["GROUP_OWER"].ToString()) ? Convert.ToString(reader["GROUP_OWER"]) : string.Empty;
                        //item.CreatedOn= !string.IsNullOrEmpty(reader["STATUS"].ToString()) ? Convert.ToString(reader["STATUS"]) : string.Empty;


                        listUPSIGroup.Add(item);



                    }
                    responce.UPSIMembersGroupList = listUPSIGroup;
                    responce.StatusFl = true;
                    responce.Msg = "Data has been fetched successfully !";
                    return responce;
                }
                reader.Close();
                return responce;
            }
            catch (Exception)
            {

                UPSIGroupReportResponse responce = new UPSIGroupReportResponse();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }
        public UPSIGroupReportResponse GetUPSIMember(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", "GET_UPSI_MEMBRS_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_OWER", objgroup.CreatedBy);
                //parameters[4] = new SqlParameter("@VALID_FROM", objgroup.VALID_FROM);
                //parameters[5] = new SqlParameter("@VALID_TLL", objgroup.VALID_TLL);
                parameters[6] = new SqlParameter("@GROUP_TYPE", objgroup.GROUP_TYPE);


                UPSIGroupReportResponse responce = new UPSIGroupReportResponse();

                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<UPSIMembersGroup> listUPSIGroup = new List<UPSIMembersGroup>();
                    List<UPSIMembersDesignatedAndNon> listUPSIGroupMember = new List<UPSIMembersDesignatedAndNon>();
                    while (reader.Read())
                    {

                        UPSIMembersDesignatedAndNon item = new UPSIMembersDesignatedAndNon();
                        item.PAN = !string.IsNullOrEmpty(reader["PAN"].ToString()) ? Convert.ToString(reader["PAN"]) : string.Empty;
                        item.NAME = !string.IsNullOrEmpty(reader["NAME"].ToString()) ? Convert.ToString(reader["NAME"]) : string.Empty;
                        item.EMAIL = !string.IsNullOrEmpty(reader["EMAIL"].ToString()) ? Convert.ToString(reader["EMAIL"]) : string.Empty;


                        listUPSIGroupMember.Add(item);



                    }
                    UPSIMembersGroup ugm = new UPSIMembersGroup();
                    ugm.listDesignatedMember = listUPSIGroupMember;
                    listUPSIGroup.Add(ugm);
                    responce.UPSIMembersGroupList = listUPSIGroup;
                    responce.StatusFl = true;
                    responce.Msg = "Data has been fetched successfully !";
                    return responce;
                }
                reader.Close();
                return responce;
            }
            catch (Exception)
            {

                UPSIGroupReportResponse responce = new UPSIGroupReportResponse();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }

        public UPSIGroupReportResponse HistoryUPSIGroup(UPSIMembersGroup objupsi)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_FOR_HISTORY_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@GROUP_ID ", Convert.ToInt32(objupsi.GROUP_ID));


                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", objupsi.MODULE_DATABASE, parameters);
                //List<UPSI> listupsiobj = new List<UPSI>();
                List<UPSIMembersGroup> listupsiobj_for_edit = new List<UPSIMembersGroup>();
                UPSIGroupReportResponse upsiobj = new UPSIGroupReportResponse();
                if (rdr.HasRows)
                {

                    while (rdr.Read())
                    {
                        UPSIMembersGroup obj = new UPSIMembersGroup();
                        obj.GROUP_ID = Convert.ToInt32(rdr["GROUP_ID"]);
                        obj.GROUP_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["GROUP_NM"]))) ? Convert.ToString(rdr["GROUP_NM"]) : String.Empty;
                        obj.GROUP_DESC = (!String.IsNullOrEmpty(Convert.ToString(rdr["GROUP_DESC"]))) ? Convert.ToString(rdr["GROUP_DESC"]) : String.Empty;
                        obj.listNonDesignatedMember = GET_USER_HISTORY(Convert.ToInt32(rdr["GROUP_ID"]), objupsi.MODULE_DATABASE, Convert.ToInt32(rdr["VERSION"]));
                        obj.listGroupUserRemarks = GET_GROUP_USER_REMARKS(Convert.ToInt32(rdr["GROUP_ID"]), objupsi.MODULE_DATABASE, Convert.ToInt32(rdr["VERSION"]));
                        DateTime dt_from = Convert.ToDateTime(rdr["VALID_FROM"]);
                        string dd4 = dt_from.ToString("dd/MM/yyyy");
                        obj.VALID_FROM = dd4;
                        DateTime dt_till = Convert.ToDateTime(rdr["VALID_TLL"]);
                        string dd5 = dt_till.ToString("dd/MM/yyyy");
                        obj.VALID_TLL = dd5;

                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);

                        string dd6 = dt.ToString("dd/MM/yyyy");
                        obj.CreatedOn = dd6;
                        obj.VERSION = Convert.ToString(rdr["VERSION"]);
                        obj.CreatedBy = (!String.IsNullOrEmpty(Convert.ToString(rdr["GROUP_OWER"]))) ? Convert.ToString(rdr["GROUP_OWER"]) : String.Empty;

                        listupsiobj_for_edit.Add(obj);
                    }

                    //upsiobj.listuser = listobj;

                    upsiobj.StatusFl = true;
                    upsiobj.Msg = "Data has been fetched successfully !";
                    upsiobj.UPSIMembersGroupList = listupsiobj_for_edit;


                }
                else
                {
                    upsiobj.StatusFl = false;
                    upsiobj.Msg = "No data found !";
                    upsiobj.UPSIMembersGroupList = listupsiobj_for_edit;
                }
                rdr.Close();
                return upsiobj;
            }
            catch (Exception ex)
            {
                UPSIGroupReportResponse ouser1 = new UPSIGroupReportResponse();
                UPSI upsi_cat = new UPSI();
                ouser1.StatusFl = false;
                ouser1.Msg = "Processing failed, because of system error !";

                return ouser1;
            }
        }

        public List<UPSIMembersDesignatedAndNon> GET_USER_HISTORY(Int32 groupId, string MODULE_DATABASE, Int32 VERSION)
        {
            List<UPSIMembersDesignatedAndNon> listmember = new List<UPSIMembersDesignatedAndNon>();
            try
            {

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_MEMBER_HISTORY_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@VERSION", VERSION);
                parameters[3] = new SqlParameter("@GROUP_ID", groupId);


                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                DataSet dt = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", MODULE_DATABASE, parameters);

                DataTable UPSIGroupMember = new DataTable();
                UPSIGroupMember = dt.Tables[0];


                if (UPSIGroupMember.Rows.Count > 0)
                {

                    for (int i = 0; i < UPSIGroupMember.Rows.Count; i++)
                    {

                        UPSIMembersDesignatedAndNon item = new UPSIMembersDesignatedAndNon();
                        item.GROUP_ID = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["GROUP_ID"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["GROUP_ID"]) : string.Empty;
                        // item.ID = !string.IsNullOrEmpty(UPSIGroupMemberDesingnated.Rows[i]["ID"].ToString()) ? Convert.ToString(UPSIGroupMemberDesingnated.Rows[i]["ID"]) : string.Empty;
                        item.NAME = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["NAME"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["NAME"]) : string.Empty;
                        item.EMAIL = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["EMAIL"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["EMAIL"]) : string.Empty;
                        item.PAN = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["PAN"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["PAN"]) : string.Empty;
                        item.VENDOR_ID = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["VENDOR_ID"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["VENDOR_ID"]) : string.Empty;
                        item.MEMBER_TYPE = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["MEMBER_TYPE"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["MEMBER_TYPE"]) : string.Empty;
                        listmember.Add(item);
                    }
                }

                return listmember;


            }
            catch (Exception ex)
            {
                return listmember;
            }
        }



        public List<UPSIRemarks> GET_GROUP_USER_REMARKS(Int32 groupId, string MODULE_DATABASE, Int32 VERSION)
        {
            List<UPSIRemarks> listmember = new List<UPSIRemarks>();
            try
            {

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_REMARKS_HISTORY_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@VERSION", VERSION);
                parameters[3] = new SqlParameter("@GROUP_ID", groupId);


                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                DataSet dt = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", MODULE_DATABASE, parameters);

                DataTable UPSIGroupMember = new DataTable();
                UPSIGroupMember = dt.Tables[0];


                if (UPSIGroupMember.Rows.Count > 0)
                {

                    for (int i = 0; i < UPSIGroupMember.Rows.Count; i++)
                    {

                        UPSIRemarks item = new UPSIRemarks();
                        item.GroupId = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["UPSI_GROUP_ID"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["UPSI_GROUP_ID"]) : string.Empty;
                        item.HdrId = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["HDR_ID"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["HDR_ID"]) : string.Empty;
                        item.Email = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["EMAIL"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["EMAIL"]) : string.Empty;
                        DateTime dtt = Convert.ToDateTime(UPSIGroupMember.Rows[i]["MAIL_DATE"]);


                        item.mailDate = dtt.ToString("dd/MM/yyyy");
                        item.msgBody = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["MSG"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["MSG"]) : string.Empty;
                        item.listUserDetail = GetUserDetail(item.HdrId, MODULE_DATABASE);
                        item.listRemarksAttachments = GetAttachments(item.HdrId, MODULE_DATABASE);


                        listmember.Add(item);
                    }
                }

                return listmember;


            }
            catch (Exception ex)
            {
                return listmember;
            }
        }

        public List<UPSIRemarksUser> GetUserDetail(string hid, string MODULE_DATABASE)
        {

            List<UPSIRemarksUser> listUser = new List<UPSIRemarksUser>();
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_REMARKS_HDR_USER");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@HDR_ID", Convert.ToInt32(hid));





            DataSet dt = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", MODULE_DATABASE, parameters);

            DataTable UPSIGroupMember = new DataTable();
            UPSIGroupMember = dt.Tables[0];


            if (UPSIGroupMember.Rows.Count > 0)
            {

                for (int i = 0; i < UPSIGroupMember.Rows.Count; i++)
                {

                    UPSIRemarksUser item = new UPSIRemarksUser();
                    item.Hdrid = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["HDR_ID"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["HDR_ID"]) : string.Empty;
                    item.Email = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["EMAIL"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["EMAIL"]) : string.Empty;
                    item.EmailType = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["EMAIL_TYPE"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["EMAIL_TYPE"]) : string.Empty;
                    item.panAvailable = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["PAN_AVAILABLE"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["PAN_AVAILABLE"]) : string.Empty;



                    listUser.Add(item);
                }
            }

            return listUser;


        }


        public List<UPSIRemarksAttachments> GetAttachments(string hid, string MODULE_DATABASE)
        {

            List<UPSIRemarksAttachments> listUser = new List<UPSIRemarksAttachments>();
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_REMARKS_ATTACHMENT");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@HDR_ID", Convert.ToInt32(hid));



            DataSet dt = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", MODULE_DATABASE, parameters);

            DataTable UPSIGroupMember = new DataTable();
            UPSIGroupMember = dt.Tables[0];


            if (UPSIGroupMember.Rows.Count > 0)
            {

                for (int i = 0; i < UPSIGroupMember.Rows.Count; i++)
                {

                    UPSIRemarksAttachments item = new UPSIRemarksAttachments();
                    item.hdrid = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["HDR_ID"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["ATTACHMENT"]) : string.Empty;
                    item.Attachment = !string.IsNullOrEmpty(UPSIGroupMember.Rows[i]["ATTACHMENT"].ToString()) ? Convert.ToString(UPSIGroupMember.Rows[i]["ATTACHMENT"]) : string.Empty;




                    listUser.Add(item);
                }
            }




            return listUser;


        }

        public UPSIGroupReportResponse GetUPSIReportByMember(UPSIMembersGroup objgroup)
        {
            try
            {


                string PanNos = "";
                for (Int32 i = 0; i < objgroup.listDesignatedMember.Count; i++)
                {
                    if (i == (objgroup.listDesignatedMember.Count - 1))
                    {
                        PanNos += objgroup.listDesignatedMember[i].PAN;
                    }
                    else
                    {
                        PanNos += objgroup.listDesignatedMember[i].PAN + ",";
                    }


                }

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LIST_BY_MEMBER_PAN");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_OWER", objgroup.CreatedBy);
                parameters[4] = new SqlParameter("@VALID_FROM", objgroup.VALID_FROM);
                parameters[5] = new SqlParameter("@VALID_TLL", objgroup.VALID_TLL);
                parameters[6] = new SqlParameter("@PAN_MULTIPLE", PanNos);


                UPSIGroupReportResponse responce = new UPSIGroupReportResponse();

                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_REPORT", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<UPSIMembersGroup> listUPSIGroup = new List<UPSIMembersGroup>();
                    while (reader.Read())
                    {

                        UPSIMembersGroup item = new UPSIMembersGroup();
                        item.GROUP_ID = !string.IsNullOrEmpty(reader["GROUP_ID"].ToString()) ? Convert.ToInt32(reader["GROUP_ID"]) : 0;
                        item.GROUP_NM = !string.IsNullOrEmpty(reader["GROUP_NM"].ToString()) ? Convert.ToString(reader["GROUP_NM"]) : string.Empty;
                        item.TotalMembers = !string.IsNullOrEmpty(reader["TotalMembers"].ToString()) ? Convert.ToString(reader["TotalMembers"]) : string.Empty;
                        DateTime DT = Convert.ToDateTime(reader["VALID_FROM"]);
                        item.VALID_FROM = DT.ToString("dd/MM/yyyy");
                        DateTime dt1 = Convert.ToDateTime(reader["VALID_TLL"]);
                        item.VALID_TLL = dt1.ToString("dd/MM/yyyy");
                        item.CreatedBy = !string.IsNullOrEmpty(reader["GROUP_OWER"].ToString()) ? Convert.ToString(reader["GROUP_OWER"]) : string.Empty;
                        //item.CreatedOn= !string.IsNullOrEmpty(reader["STATUS"].ToString()) ? Convert.ToString(reader["STATUS"]) : string.Empty;


                        listUPSIGroup.Add(item);



                    }
                    responce.UPSIMembersGroupList = listUPSIGroup;
                    responce.StatusFl = true;
                    responce.Msg = "Data has been fetched successfully !";
                    return responce;
                }
                reader.Close();
                return responce;
            }
            catch (Exception EX)
            {

                UPSIGroupReportResponse responce = new UPSIGroupReportResponse();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }

    }//main
}