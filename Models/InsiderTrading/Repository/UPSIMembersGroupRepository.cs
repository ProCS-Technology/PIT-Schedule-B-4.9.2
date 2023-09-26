using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UPSIMembersGroupRepository
    {

        public UPSIMembersGroupResponce GetDesignatedUserList(UPSIMembersGroup objgroup)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@ADMIN_DATABASE", objgroup.ADMIN_DATABASE);


                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();
                List<User> list = new List<User>();
                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<User> ulist = new List<User>();
                    while (reader.Read())
                    {

                        User item = new User();

                        item.ID = !string.IsNullOrEmpty(reader["ID"].ToString()) ? Convert.ToInt32(reader["ID"]) : 0;
                        item.USER_NM = !string.IsNullOrEmpty(reader["USER_NM"].ToString()) ? Convert.ToString(reader["USER_NM"]) : string.Empty;
                        item.USER_EMAIL = !string.IsNullOrEmpty(reader["USER_EMAIL"].ToString()) ? Convert.ToString(reader["USER_EMAIL"]) : string.Empty;


                        ulist.Add(item);

                        responce.UPSIMembersGroup.listUser = ulist;
                        responce.StatusFl = true;
                        responce.Msg = "Data has been fetched successfully !";
                    }
                }
                reader.Close();
                return responce;
            }
            catch (Exception exception1)
            {
                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();
                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";

                return responce;
            }
        }

        public UPSIMembersGroupResponce GetVendorList(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_VENDOR_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);


                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();
                List<UPSIVendor> list = new List<UPSIVendor>();
                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<UPSIVendor> listVendor = new List<UPSIVendor>();
                    while (reader.Read())
                    {

                        UPSIVendor item = new UPSIVendor();
                        item.VendorId = !string.IsNullOrEmpty(reader["VENDOR_ID"].ToString()) ? Convert.ToString(reader["VENDOR_ID"]) : string.Empty;
                        item.vendorName = !string.IsNullOrEmpty(reader["VENDOR_NAME"].ToString()) ? Convert.ToString(reader["VENDOR_NAME"]) : string.Empty;
                        item.VendorStatus = !string.IsNullOrEmpty(reader["STATUS"].ToString()) ? Convert.ToString(reader["STATUS"]) : string.Empty;
                        listVendor.Add(item);

                    }
                    responce.UPSIMembersGroup.lisVender = listVendor;
                    responce.StatusFl = true;
                    responce.Msg = "Data has been fetched successfully !";
                    return responce;
                }
                reader.Close();
                return responce;
            }
            catch (Exception)
            {

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }
        public UPSIMembersGroupResponce GetUPSITypeList(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_UPSI_TYPE_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);


                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();
                List<UPSIVendor> list = new List<UPSIVendor>();
                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<UPSIGroupType> listVendor = new List<UPSIGroupType>();
                    while (reader.Read())
                    {

                        UPSIGroupType item = new UPSIGroupType();
                        item.GROUP_TYPE_ID = !string.IsNullOrEmpty(reader["GROUP_TYPE_ID"].ToString()) ? Convert.ToString(reader["GROUP_TYPE_ID"]) : string.Empty;
                        item.GROUP_TYPE = !string.IsNullOrEmpty(reader["GROUP_TYPE"].ToString()) ? Convert.ToString(reader["GROUP_TYPE"]) : string.Empty;

                        listVendor.Add(item);

                    }
                    responce.UPSIMembersGroup.listGroupType = listVendor;
                    responce.StatusFl = true;
                    responce.Msg = "Data has been fetched successfully !";
                    return responce;
                }
                reader.Close();
                return responce;
            }
            catch (Exception)
            {

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }

        public UPSIMembersGroupResponce GetUPSIGroupList(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_OWER", objgroup.CreatedBy);

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
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

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }
        public UPSIMembersGroupResponce GetUPSIGroupListByID(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LIST_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_ID", objgroup.GROUP_ID);


                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                DataSet dS = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);


                DataTable UPSIGroup = new DataTable();

                DataTable UPSIGroupMemberDesingnated = new DataTable();
                DataTable UPSIGroupMemberNonDesingnated = new DataTable();
                DataTable UPSIGroupTYPE = new DataTable();
                UPSIGroup = dS.Tables[0];
                UPSIGroupMemberDesingnated = dS.Tables[1];
                UPSIGroupMemberNonDesingnated = dS.Tables[2];
                UPSIGroupTYPE = dS.Tables[3];
                List<UPSIMembersDesignatedAndNon> listUPSIGroupMemberDeg = new List<UPSIMembersDesignatedAndNon>();
                if (UPSIGroupMemberDesingnated.Rows.Count > 0)
                {

                    for (int i = 0; i < UPSIGroupMemberDesingnated.Rows.Count; i++)
                    {

                        UPSIMembersDesignatedAndNon item = new UPSIMembersDesignatedAndNon();
                        item.GROUP_ID = !string.IsNullOrEmpty(UPSIGroupMemberDesingnated.Rows[i]["GROUP_ID"].ToString()) ? Convert.ToString(UPSIGroupMemberDesingnated.Rows[i]["GROUP_ID"]) : string.Empty;
                        item.ID = !string.IsNullOrEmpty(UPSIGroupMemberDesingnated.Rows[i]["ID"].ToString()) ? Convert.ToString(UPSIGroupMemberDesingnated.Rows[i]["ID"]) : string.Empty;


                        listUPSIGroupMemberDeg.Add(item);
                    }

                }
                else
                {

                }

                List<UPSIMembersDesignatedAndNon> listUPSIGroupMemberNonDeg = new List<UPSIMembersDesignatedAndNon>();
                if (UPSIGroupMemberNonDesingnated.Rows.Count > 0)
                {

                    for (int i = 0; i < UPSIGroupMemberNonDesingnated.Rows.Count; i++)
                    {

                        UPSIMembersDesignatedAndNon item = new UPSIMembersDesignatedAndNon();
                        item.GROUP_ID = !string.IsNullOrEmpty(UPSIGroupMemberNonDesingnated.Rows[i]["GROUP_ID"].ToString()) ? Convert.ToString(UPSIGroupMemberNonDesingnated.Rows[i]["GROUP_ID"]) : string.Empty;
                        // item.ID = !string.IsNullOrEmpty(UPSIGroupMemberDesingnated.Rows[i]["ID"].ToString()) ? Convert.ToString(UPSIGroupMemberDesingnated.Rows[i]["ID"]) : string.Empty;
                        item.NAME = !string.IsNullOrEmpty(UPSIGroupMemberNonDesingnated.Rows[i]["NAME"].ToString()) ? Convert.ToString(UPSIGroupMemberNonDesingnated.Rows[i]["NAME"]) : string.Empty;
                        item.EMAIL = !string.IsNullOrEmpty(UPSIGroupMemberNonDesingnated.Rows[i]["EMAIL"].ToString()) ? Convert.ToString(UPSIGroupMemberNonDesingnated.Rows[i]["EMAIL"]) : string.Empty;
                        item.PAN = !string.IsNullOrEmpty(UPSIGroupMemberNonDesingnated.Rows[i]["PAN"].ToString()) ? Convert.ToString(UPSIGroupMemberNonDesingnated.Rows[i]["PAN"]) : string.Empty;
                        item.VENDOR_ID = !string.IsNullOrEmpty(UPSIGroupMemberNonDesingnated.Rows[i]["VENDOR_ID"].ToString()) ? Convert.ToString(UPSIGroupMemberNonDesingnated.Rows[i]["VENDOR_ID"]) : string.Empty;

                        listUPSIGroupMemberNonDeg.Add(item);
                    }
                }
                else
                {

                }

                List<UPSIGroupType> listUPSIGroupType = new List<UPSIGroupType>();
                if (UPSIGroupTYPE.Rows.Count > 0)
                {

                    for (int i = 0; i < UPSIGroupTYPE.Rows.Count; i++)
                    {

                        UPSIGroupType item = new UPSIGroupType();
                        //item.GROUP_ID = !string.IsNullOrEmpty(UPSIGroupMemberDesingnated.Rows[i]["GROUP_ID"].ToString()) ? Convert.ToString(UPSIGroupMemberDesingnated.Rows[i]["GROUP_ID"]) : string.Empty;
                        item.GROUP_TYPE_ID = !string.IsNullOrEmpty(UPSIGroupTYPE.Rows[i]["GROUP_TYPE_ID"].ToString()) ? Convert.ToString(UPSIGroupTYPE.Rows[i]["GROUP_TYPE_ID"]) : string.Empty;
                        //item.GROUP_TYPE = !string.IsNullOrEmpty(UPSIGroupTYPE.Rows[i]["GROUP_TYPE"].ToString()) ? Convert.ToString(UPSIGroupTYPE.Rows[i]["GROUP_TYPE"]) : string.Empty;


                        listUPSIGroupType.Add(item);
                    }

                }
                else
                {

                }

                if (UPSIGroup.Rows.Count > 0)
                {
                    List<UPSIMembersGroup> listUPSIGroup = new List<UPSIMembersGroup>();
                    for (int i = 0; i < UPSIGroup.Rows.Count; i++)
                    {

                        UPSIMembersGroup item = new UPSIMembersGroup();
                        item.GROUP_ID = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["GROUP_ID"].ToString()) ? Convert.ToInt32(UPSIGroup.Rows[i]["GROUP_ID"]) : 0;
                        item.GROUP_NM = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["GROUP_NM"].ToString()) ? Convert.ToString(UPSIGroup.Rows[i]["GROUP_NM"]) : string.Empty;
                        item.GROUP_DESC = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["GROUP_DESC"].ToString()) ? Convert.ToString(UPSIGroup.Rows[i]["GROUP_DESC"]) : string.Empty;
                        DateTime DT = Convert.ToDateTime(UPSIGroup.Rows[i]["VALID_FROM"]);
                        item.VALID_FROM = DT.ToString("dd/MM/yyyy");
                        DateTime dt1 = Convert.ToDateTime(UPSIGroup.Rows[i]["VALID_TLL"]);
                        item.VALID_TLL = dt1.ToString("dd/MM/yyyy");
                        item.CreatedBy = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["GROUP_OWER"].ToString()) ? Convert.ToString(UPSIGroup.Rows[i]["GROUP_OWER"]) : string.Empty;
                        item.STATUS = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["STATUS"].ToString()) ? Convert.ToString(UPSIGroup.Rows[i]["STATUS"]) : string.Empty;
                        item.GROUP_TYPE = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["GROUP_TYPE"].ToString()) ? Convert.ToString(UPSIGroup.Rows[i]["GROUP_TYPE"]) : string.Empty;
                        item.VERSION = !string.IsNullOrEmpty(UPSIGroup.Rows[i]["VERSION"].ToString()) ? Convert.ToString(UPSIGroup.Rows[i]["VERSION"]) : string.Empty;
                        item.listDesignatedMember = listUPSIGroupMemberDeg;
                        item.listNonDesignatedMember = listUPSIGroupMemberNonDeg;
                        item.listGroupType = listUPSIGroupType;

                        listUPSIGroup.Add(item);

                        responce.UPSIMembersGroupList = listUPSIGroup;
                        responce.StatusFl = true;
                        responce.Msg = "Data has been fetched successfully !";
                        return responce;
                    }
                }
                else
                {



                    // responce.UPSIMembersGroupList = listUPSIGroup;
                    responce.StatusFl = false;
                    responce.Msg = "Data has not been fetched successfully !";
                    return responce;
                }


                return responce;
            }
            catch (Exception ex)
            {

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;

                responce.Msg = ex.Message;
                //responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }

        public UPSIMembersGroupResponce SaveUPSI(UPSIMembersGroup objgroup)
        {

            try
            {
                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));
                parameters[3] = new SqlParameter("@GROUP_NM", Convert.ToString(objgroup.GROUP_NM));
                parameters[4] = new SqlParameter("@GROUP_DESC", Convert.ToString(objgroup.GROUP_DESC));
                parameters[5] = new SqlParameter("@VALID_FROM", Convert.ToDateTime(objgroup.VALID_FROM));
                parameters[6] = new SqlParameter("@VALID_TLL", Convert.ToDateTime(objgroup.VALID_TLL));
                parameters[7] = new SqlParameter("@GROUP_OWER", Convert.ToString(objgroup.CreatedBy));
                parameters[8] = new SqlParameter("@STATUS", Convert.ToString(objgroup.STATUS));

                parameters[9] = new SqlParameter("@GROUP_TYPE", Convert.ToString(objgroup.GROUP_TYPE));
                //parameters[10] = new SqlParameter("@GROUP_OWER", Convert.ToString(objgroup.CreatedBy));
                parameters[10] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(objgroup.CompanyId));
                int num = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                var obj2 = parameters[1].Value;


                SqlParameter[] parameter3 = new SqlParameter[4];
                parameter3[0] = new SqlParameter("@MODE", "DELETE_UPSI_GROUP_MENBER");
                parameter3[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter3[1].Direction = ParameterDirection.Output;
                parameter3[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));

                int num8 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter3);
                var obj8 = parameter3[1].Value;

                if (Convert.ToInt32(obj2) > 0)
                {

                    for (int i = 0; i < objgroup.listDesignatedMember.Count; i++)
                    {
                        SqlParameter[] parameter1 = new SqlParameter[5];
                        parameter1[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP_MENBER_DESIGNATED");
                        parameter1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters[1].Direction = ParameterDirection.Output;
                        parameter1[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(obj2));
                        parameter1[3] = new SqlParameter("@MEMBER_ID", Convert.ToInt32(objgroup.listDesignatedMember[i].ID));
                        parameter1[4] = new SqlParameter("@ADMIN_DATABASE", objgroup.ADMIN_DATABASE);
                        //parameter1[4] = new SqlParameter("@NAME", Convert.ToString(objgroup.listDesignatedMember[i].NAME));
                        //parameter1[5] = new SqlParameter("@EMAIL", Convert.ToString(objgroup.listDesignatedMember[i].EMAIL));
                        //parameter1[6] = new SqlParameter("@PAN", Convert.ToString(objgroup.listDesignatedMember[i].PAN));
                        //parameter1[7] = new SqlParameter("@VENDOR_ID", Convert.ToString(objgroup.listDesignatedMember[i].VENDOR_ID));

                        int num6 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter1);
                        var obj4 = parameter1[1].Value;
                    }
                    for (int i = 0; i < objgroup.listNonDesignatedMember.Count; i++)
                    {
                        SqlParameter[] parameter2 = new SqlParameter[7];
                        parameter2[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP_MENBER_NON_DESIGNATED");
                        parameter2[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameter2[1].Direction = ParameterDirection.Output;
                        parameter2[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(obj2));
                        //parameter1[3] = new SqlParameter("@MEMBER_ID", Convert.ToInt32(objgroup.listNonDesignatedMember[i].ID));
                        parameter2[3] = new SqlParameter("@NAME", Convert.ToString(objgroup.listNonDesignatedMember[i].NAME));
                        parameter2[4] = new SqlParameter("@EMAIL", Convert.ToString(objgroup.listNonDesignatedMember[i].EMAIL));
                        parameter2[5] = new SqlParameter("@PAN", Convert.ToString(objgroup.listNonDesignatedMember[i].PAN));
                        parameter2[6] = new SqlParameter("@VENDOR_ID", Convert.ToInt32(objgroup.listNonDesignatedMember[i].VENDOR_ID));

                        int num6 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter2);
                        var obj5 = parameter2[1].Value;
                    }
                    SqlParameter[] parameter4 = new SqlParameter[4];
                    parameter4[0] = new SqlParameter("@MODE", "DELETE_UPSI_GROUP_MENBER_TYPE");
                    parameter4[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameter4[1].Direction = ParameterDirection.Output;
                    parameter4[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));

                    int num9 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter4);
                    var obj10 = parameter3[1].Value;

                    for (int i = 0; i < objgroup.listGroupType.Count; i++)
                    {
                        SqlParameter[] parameter5 = new SqlParameter[4];
                        parameter5[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP_MENBER_TYPE");
                        parameter5[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameter5[1].Direction = ParameterDirection.Output;
                        parameter5[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(obj2));
                        //parameter1[3] = new SqlParameter("@MEMBER_ID", Convert.ToInt32(objgroup.listNonDesignatedMember[i].ID));
                        parameter5[3] = new SqlParameter("@GROUP_TYPE_ID", Convert.ToInt32(objgroup.listGroupType[i].GROUP_TYPE_ID));


                        int num6 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter5);
                        var obj5 = parameter5[1].Value;
                    }


                    responce.StatusFl = true;
                    responce.Msg = "Data has been Saved successfully !";
                    return responce;

                }
                else
                {
                    responce.StatusFl = false;
                    responce.Msg = "Data has not been Saved successfully !";
                    return responce;
                }
            }
            catch (Exception exception1)
            {

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = exception1.Message;
                //responce.Msg = "Processing failed, because of system error !";
                return responce;
            }

        }

        public UPSIMembersGroupResponce UpdateUPSI(UPSIMembersGroup objgroup)
        {
            try
            {
                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_UPSI_GROUP");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));
                parameters[3] = new SqlParameter("@GROUP_NM", Convert.ToString(objgroup.GROUP_NM));
                parameters[4] = new SqlParameter("@GROUP_DESC", Convert.ToString(objgroup.GROUP_DESC));
                parameters[5] = new SqlParameter("@VALID_FROM", Convert.ToDateTime(objgroup.VALID_FROM));
                parameters[6] = new SqlParameter("@VALID_TLL", Convert.ToDateTime(objgroup.VALID_TLL));
                parameters[7] = new SqlParameter("@GROUP_OWER", Convert.ToString(objgroup.CreatedBy));
                parameters[8] = new SqlParameter("@STATUS", Convert.ToString(objgroup.STATUS));

                parameters[9] = new SqlParameter("@GROUP_TYPE", Convert.ToString(objgroup.GROUP_TYPE));
                //parameters[10] = new SqlParameter("@GROUP_OWER", Convert.ToString(objgroup.CreatedBy));
                parameters[10] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(objgroup.CompanyId));
                int num = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                var obj2 = parameters[1].Value;


                SqlParameter[] parameter3 = new SqlParameter[3];
                parameter3[0] = new SqlParameter("@MODE", "DELETE_UPSI_GROUP_MENBER");
                parameter3[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter3[1].Direction = ParameterDirection.Output;
                parameter3[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));

                int num8 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter3);
                var obj8 = parameter3[1].Value;

                if (Convert.ToInt32(obj2) > 0)
                {

                    for (int i = 0; i < objgroup.listDesignatedMember.Count; i++)
                    {
                        SqlParameter[] parameter1 = new SqlParameter[5];
                        parameter1[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP_MENBER_DESIGNATED");
                        parameter1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters[1].Direction = ParameterDirection.Output;
                        parameter1[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(obj2));
                        parameter1[3] = new SqlParameter("@MEMBER_ID", Convert.ToInt32(objgroup.listDesignatedMember[i].ID));
                        parameter1[4] = new SqlParameter("@ADMIN_DATABASE", objgroup.ADMIN_DATABASE);
                        //parameter1[4] = new SqlParameter("@NAME", Convert.ToString(objgroup.listDesignatedMember[i].NAME));
                        //parameter1[5] = new SqlParameter("@EMAIL", Convert.ToString(objgroup.listDesignatedMember[i].EMAIL));
                        //parameter1[6] = new SqlParameter("@PAN", Convert.ToString(objgroup.listDesignatedMember[i].PAN));
                        //parameter1[7] = new SqlParameter("@VENDOR_ID", Convert.ToString(objgroup.listDesignatedMember[i].VENDOR_ID));

                        int num6 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter1);
                        var obj4 = parameter1[1].Value;
                    }
                    for (int i = 0; i < objgroup.listNonDesignatedMember.Count; i++)
                    {
                        SqlParameter[] parameter2 = new SqlParameter[7];
                        parameter2[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP_MENBER_NON_DESIGNATED");
                        parameter2[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameter2[1].Direction = ParameterDirection.Output;
                        parameter2[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(obj2));
                        //parameter1[3] = new SqlParameter("@MEMBER_ID", Convert.ToInt32(objgroup.listNonDesignatedMember[i].ID));
                        parameter2[3] = new SqlParameter("@NAME", Convert.ToString(objgroup.listNonDesignatedMember[i].NAME));
                        parameter2[4] = new SqlParameter("@EMAIL", Convert.ToString(objgroup.listNonDesignatedMember[i].EMAIL));
                        parameter2[5] = new SqlParameter("@PAN", Convert.ToString(objgroup.listNonDesignatedMember[i].PAN));
                        parameter2[6] = new SqlParameter("@VENDOR_ID", Convert.ToInt32(objgroup.listNonDesignatedMember[i].VENDOR_ID));

                        int num6 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter2);
                        var obj5 = parameter2[1].Value;
                    }
                    SqlParameter[] parameter4 = new SqlParameter[4];
                    parameter4[0] = new SqlParameter("@MODE", "DELETE_UPSI_GROUP_MENBER_TYPE");
                    parameter4[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameter4[1].Direction = ParameterDirection.Output;
                    parameter4[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));

                    int num9 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter4);
                    var obj10 = parameter3[1].Value;

                    for (int i = 0; i < objgroup.listGroupType.Count; i++)
                    {
                        SqlParameter[] parameter5 = new SqlParameter[4];
                        parameter5[0] = new SqlParameter("@MODE", "INSERT_UPSI_GROUP_MENBER_TYPE");
                        parameter5[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameter5[1].Direction = ParameterDirection.Output;
                        parameter5[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(obj2));
                        //parameter1[3] = new SqlParameter("@MEMBER_ID", Convert.ToInt32(objgroup.listNonDesignatedMember[i].ID));
                        parameter5[3] = new SqlParameter("@GROUP_TYPE_ID", Convert.ToInt32(objgroup.listGroupType[i].GROUP_TYPE_ID));


                        int num6 = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameter5);
                        var obj5 = parameter5[1].Value;
                    }


                    responce.StatusFl = true;
                    responce.Msg = "Data has been Updated successfully !";
                    return responce;

                }
                else
                {
                    responce.StatusFl = false;
                    responce.Msg = "Data has not been updated successfully !";
                    return responce;
                }
            }
            catch (Exception exception1)
            {

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = exception1.Message;
                //responce.Msg = "Processing failed, because of system error !";
                return responce;
            }
        }



        public UPSIMembersGroupResponce DeleteUPSIGroupListByID(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "DELETE_UPSI_GROUP");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_ID", Convert.ToInt32(objgroup.GROUP_ID));

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                int st = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                Int32 count = Convert.ToInt32(parameters[1].Value);
                if (count > 0)
                {
                    responce.StatusFl = true;
                    responce.Msg = "Data has been Delete successfully !";
                    return responce;
                }
                else
                {
                    responce.StatusFl = false;
                    responce.Msg = "Unable to Delete. This Category is Used in Higher Component!";
                    return responce;
                }


            }
            catch (Exception)
            {

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }


        public UPSIMembersGroupResponce GetNonDesignatedUPSIMember(UPSIMembersGroup objgroup)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_NONDESIGNATED_MEMBER_FOR_UPSI");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objgroup.CompanyId);
                parameters[3] = new SqlParameter("@GROUP_NM", objgroup.GROUP_NM);

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MEMBERS_GROUP", objgroup.MODULE_DATABASE, parameters);
                if (!reader.HasRows)
                {
                    responce.StatusFl = false;
                    responce.Msg = "No data found !";
                }
                else
                {
                    List<UPSIMembersGroup> listUPSIGroup = new List<UPSIMembersGroup>();
                    List<UPSIMembersDesignatedAndNon> listUPSIGroupMemberNonDeg = new List<UPSIMembersDesignatedAndNon>();
                    while (reader.Read())
                    {


                        UPSIMembersDesignatedAndNon item = new UPSIMembersDesignatedAndNon();
                        item.NAME = !string.IsNullOrEmpty(reader["NAME"].ToString()) ? Convert.ToString(reader["NAME"]) : string.Empty;
                        item.EMAIL = !string.IsNullOrEmpty(reader["EMAIL"].ToString()) ? Convert.ToString(reader["EMAIL"]) : string.Empty;
                        item.PAN = !string.IsNullOrEmpty(reader["PAN"].ToString()) ? Convert.ToString(reader["PAN"]) : string.Empty;
                        item.VENDOR_ID = !string.IsNullOrEmpty(reader["VENDOR_ID"].ToString()) ? Convert.ToString(reader["VENDOR_ID"]) : string.Empty;



                        listUPSIGroupMemberNonDeg.Add(item);



                    }
                    UPSIMembersGroup UPSIGroup = new UPSIMembersGroup();
                    UPSIGroup.listNonDesignatedMember = listUPSIGroupMemberNonDeg;

                    listUPSIGroup.Add(UPSIGroup);
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

                UPSIMembersGroupResponce responce = new UPSIMembersGroupResponce();

                responce.StatusFl = false;
                responce.Msg = "Processing failed, because of system error !";
                return responce;

            }
        }





    }//class 
}