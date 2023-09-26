using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UPSIRepository : IRequiresSessionState
    {
        private UserResponse _userResponse;
        private string connectionString = SQLHelper.GetConnString();

        public UPSIResponse Addupsi(UPSI objuser)

        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@MODE", "INSERT_UPSI");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@UPSI_GROUP", objuser.upsi_group);
                parameters[3] = new SqlParameter("@UPSI_DESC", objuser.upsi_desc);
                parameters[4] = new SqlParameter("@UPSI_FROM_DATE", Convert.ToDateTime(objuser.from_date));
                parameters[5] = new SqlParameter("@UPSI_TILL_DATE", Convert.ToDateTime(objuser.till_date));
                parameters[6] = new SqlParameter("@CREATED_BY", objuser.created_by);
                parameters[7] = new SqlParameter("@COMPANY_ID", objuser.COMPANY_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objuser.MODULE_DATABASE, parameters);
                var obj = parameters[1].Value;

                UPSI uobj = new UPSI();
                if ((Int32)obj == 0)
                {
                    uobj.StatusFl = true;
                    uobj.Msg = " UPSI already Exists!";
                    ouser.upsi = uobj;
                }
                else
                {
                    if (objuser.listuser.Count > 0)
                    {
                        for (int i = 0; i < objuser.listuser.Count; i++)
                        {
                            SqlParameter[] parameters1 = new SqlParameter[6];
                            parameters1[0] = new SqlParameter("@MODE", "INSERT_UPSI_MEMBER");
                            parameters1[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                            parameters1[1].Direction = ParameterDirection.Output;
                            parameters1[2] = new SqlParameter("@UPSI_ID", Convert.ToInt32(obj));
                            parameters1[3] = new SqlParameter("@USER_ID", objuser.listuser[i].ID);
                            parameters1[4] = new SqlParameter("@CREATED_BY", objuser.created_by);
                            parameters1[5] = new SqlParameter("@COMPANY_ID", objuser.COMPANY_ID);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objuser.MODULE_DATABASE, parameters1);
                            var obj_u = parameters[1].Value;


                        }
                        uobj.StatusFl = true;
                        uobj.Msg = "Data has been saved successfully !";
                        ouser.upsi = uobj;

                    }
                    else
                    {
                        uobj.StatusFl = false;
                        uobj.Msg = "Data has not been Saved!";
                        ouser.upsi = uobj;

                    }


                }
                return ouser;
            }
            catch (Exception ex)
            {

                UPSI uobj = new UPSI();
                uobj.StatusFl = false;
                uobj.Msg = "Processing failed, because of system error !";
                ouser.upsi = uobj;
                return ouser;
            }
        }
        public UPSIResponse Updateupsi(UPSI objuser)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_UPSI");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@UPSI_GROUP", objuser.upsi_group);
                parameters[3] = new SqlParameter("@UPSI_DESC", objuser.upsi_desc);
                parameters[4] = new SqlParameter("@UPSI_FROM_DATE", Convert.ToDateTime(objuser.from_date));
                parameters[5] = new SqlParameter("@UPSI_TILL_DATE", Convert.ToDateTime(objuser.till_date));
                parameters[6] = new SqlParameter("@UPSI_ID", objuser.upsi_id);
                parameters[7] = new SqlParameter("@CREATED_BY", objuser.created_by);
                parameters[8] = new SqlParameter("@COMPANY_ID", objuser.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objuser.MODULE_DATABASE, parameters);
                var obj = parameters[1].Value;
                var ver = Convert.ToInt32(obj);
                UPSI uobj = new UPSI();
                if ((Int32)obj == 0)
                {
                    uobj.StatusFl = true;
                    uobj.Msg = " UPSI already Exists!";
                    ouser.upsi = uobj;
                }
                else
                {
                    if (objuser.listuser.Count > 0)
                    {
                        for (int i = 0; i < objuser.listuser.Count; i++)
                        {
                            SqlParameter[] parameters1 = new SqlParameter[7];
                            parameters1[0] = new SqlParameter("@MODE", "UPDATE_UPSI_MEMBER");
                            parameters1[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                            parameters1[1].Direction = ParameterDirection.Output;
                            parameters1[2] = new SqlParameter("@UPSI_ID", objuser.upsi_id);
                            parameters1[3] = new SqlParameter("@USER_ID", objuser.listuser[i].ID);
                            parameters1[4] = new SqlParameter("@CREATED_BY", objuser.created_by);
                            parameters1[5] = new SqlParameter("@VERSION", ver);
                            parameters1[6] = new SqlParameter("@COMPANY_ID", objuser.COMPANY_ID);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objuser.MODULE_DATABASE, parameters1);
                            var obj_u = parameters[1].Value;


                        }
                        uobj.StatusFl = true;
                        uobj.Msg = "Data has been saved successfully !";
                        ouser.upsi = uobj;

                    }
                    else
                    {
                        uobj.StatusFl = true;
                        uobj.Msg = "Data has been saved successfully !";
                        ouser.upsi = uobj;

                    }


                }
                return ouser;
            }
            catch (Exception ex)
            {

                UPSI uobj = new UPSI();
                uobj.StatusFl = false;
                uobj.Msg = "Processing failed, because of system error !";
                ouser.upsi = uobj;
                return ouser;
            }
        }
        public UPSIResponse list_user(UPSI objupsi)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "ALL_USER");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objupsi.COMPANY_ID);

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objupsi.MODULE_DATABASE, parameters);
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {

                    List<User> listobj = new List<User>();

                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.ID = Convert.ToInt32(rdr["ID"]);
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.designation = new Designation()
                        {
                            DESIGNATION_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_ID"]))) ? Convert.ToInt32(rdr["DESIGNATION_ID"]) : 0,
                            DESIGNATION_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_NAME"]))) ? Convert.ToString(rdr["DESIGNATION_NAME"]) : String.Empty

                        };

                        listobj.Add(obj);
                    }
                    upsiobj.listuser = listobj;
                    upsiobj.StatusFl = true;
                    upsiobj.Msg = "Data has been fetched successfully !";
                    ouser.upsi = upsiobj;


                }
                else
                {
                    upsiobj.StatusFl = false;
                    upsiobj.Msg = "No data found !";
                    ouser.upsi = upsiobj;
                }
                rdr.Close();
                return ouser;
            }
            catch (Exception ex)
            {
                UPSIResponse ouser1 = new UPSIResponse();
                UPSI upsi_cat = new UPSI();
                upsi_cat.StatusFl = false;
                upsi_cat.Msg = "Processing failed, because of system error !";
                ouser.upsi = upsi_cat;
                return ouser;
            }
        }
        public UPSIResponse list_upsi(UPSI objupsi)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objupsi.COMPANY_ID);

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objupsi.MODULE_DATABASE, parameters);
                List<UPSI> listupsiobj = new List<UPSI>();
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {



                    while (rdr.Read())
                    {
                        UPSI obj = new UPSI();
                        obj.upsi_id = Convert.ToInt32(rdr["UPSI_ID"]);
                        obj.upsi_group = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_GROUP"]))) ? Convert.ToString(rdr["UPSI_GROUP"]) : String.Empty;
                        obj.upsi_desc = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_DESC"]))) ? Convert.ToString(rdr["UPSI_DESC"]) : String.Empty;
                        DateTime dt_from = Convert.ToDateTime(rdr["FROM_DATE"]);
                        string dd = dt_from.ToString("dd/MM/yyyy");
                        obj.from_date = dd;
                        DateTime dt_till = Convert.ToDateTime(rdr["TILL_DATE"]);
                        string dd1 = dt_till.ToString("dd/MM/yyyy");
                        obj.till_date = dd1;

                        obj.noofmember = Convert.ToInt32(rdr["TOTALMEMBER"]);
                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);

                        string dd2 = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dd2;
                        obj.created_by = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"]))) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        listupsiobj.Add(obj);
                    }

                    //upsiobj.listuser = listobj;
                    upsiobj.StatusFl = true;
                    upsiobj.Msg = "Data has been fetched successfully !";
                    ouser.upsilist = listupsiobj;
                    ouser.upsi = upsiobj;
                    ouser.StatusFl = true;
                    ouser.Msg = "Data has been fetched successfully !";

                }
                else
                {
                    upsiobj.StatusFl = false;
                    upsiobj.Msg = "No data found !";
                    ouser.upsi = upsiobj;
                    ouser.StatusFl = false;
                    ouser.Msg = "No data found !";
                }
                rdr.Close();
                return ouser;
            }
            catch (Exception ex)
            {
                UPSIResponse ouser1 = new UPSIResponse();
                UPSI upsi_cat = new UPSI();
                upsi_cat.StatusFl = false;
                upsi_cat.Msg = "Processing failed, because of system error !";
                ouser.upsi = upsi_cat;
                ouser.StatusFl = false;
                ouser.Msg = "Processing failed, because of system error !";
                return ouser;
            }
        }
        public UPSIResponse edit_upsi(UPSI objupsi)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_FOR_EDIT");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2] = new SqlParameter("@UPSI_ID", Convert.ToInt32(objupsi.upsi_id));
                parameters[1].Direction = ParameterDirection.Output;

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objupsi.MODULE_DATABASE, parameters);
                List<UPSI> listupsiobj = new List<UPSI>();
                List<UPSI> listupsiobj_for_edit = new List<UPSI>();
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {



                    while (rdr.Read())
                    {
                        UPSI obj = new UPSI();
                        obj.upsi_id = Convert.ToInt32(rdr["UPSI_ID"]);
                        obj.upsi_group = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_GROUP"]))) ? Convert.ToString(rdr["UPSI_GROUP"]) : String.Empty;
                        obj.upsi_desc = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_DESC"]))) ? Convert.ToString(rdr["UPSI_DESC"]) : String.Empty;
                        obj.listuser = GET_USER(Convert.ToInt32(rdr["UPSI_ID"]), objupsi.MODULE_DATABASE);
                        DateTime dt_from = Convert.ToDateTime(rdr["FROM_DATE"]);
                        string dd4 = dt_from.ToString("dd/MM/yyyy");
                        obj.from_date = dd4;
                        DateTime dt_till = Convert.ToDateTime(rdr["TILL_DATE"]);
                        string dd5 = dt_till.ToString("dd/MM/yyyy");
                        obj.till_date = dd5;
                        obj.noofmember = Convert.ToInt32(rdr["TOTALMEMBER"]);
                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);

                        string dd6 = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dd6;
                        obj.created_by = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"]))) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        listupsiobj_for_edit.Add(obj);
                    }

                    //upsiobj.listuser = listobj;

                    upsiobj.StatusFl = true;
                    upsiobj.Msg = "Data has been fetched successfully !";
                    ouser.upsilist = listupsiobj_for_edit;
                    ouser.upsi = upsiobj;
                    ouser.StatusFl = true;
                    ouser.Msg = "Data has been fetched successfully !";

                }
                else
                {
                    upsiobj.StatusFl = false;
                    upsiobj.Msg = "No data found !";
                    ouser.upsi = upsiobj;
                    ouser.StatusFl = false;
                    ouser.Msg = "No data found !";
                }
                rdr.Close();
                return ouser;
            }
            catch (Exception ex)
            {
                UPSIResponse ouser1 = new UPSIResponse();
                UPSI upsi_cat = new UPSI();
                upsi_cat.StatusFl = false;
                upsi_cat.Msg = "Processing failed, because of system error !";
                ouser.upsi = upsi_cat;
                ouser.StatusFl = false;
                ouser.Msg = "Processing failed, because of system error !";
                return ouser;
            }
        }
        public List<User> GET_USER(int id, string MODULE_DATABASE)
        {
            List<User> listobj = new List<User>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "USER_FOREDIT");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2] = new SqlParameter("@UPSI_ID", id);
                parameters[1].Direction = ParameterDirection.Output;

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", MODULE_DATABASE, parameters);
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {



                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.ID = Convert.ToInt32(rdr["USER_ID"]);
                        //obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        //obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        //obj.designation = new Designation()
                        //{
                        //    DESIGNATION_ID = Convert.ToInt32(rdr["DESIGNATION_ID"]),
                        //    DESIGNATION_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_NAME"]))) ? Convert.ToString(rdr["DESIGNATION_NAME"]) : String.Empty

                        //};

                        listobj.Add(obj);
                    }
                    rdr.Close();
                    return listobj;


                }
                else
                {
                    return listobj;
                }


            }
            catch (Exception ex)
            {

            }


            return listobj;
        }
        public UPSIResponse delete_upsi(UPSI objupsi)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "DELETE_UPSI");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2] = new SqlParameter("@UPSI_ID", Convert.ToInt32(objupsi.upsi_id));
                parameters[1].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objupsi.MODULE_DATABASE, parameters);
                var obj = parameters[1].Value;
                UPSI upsiobj = new UPSI();
                if ((int)obj == 0)
                {
                    //upsiobj.listuser = listobj;

                    upsiobj.StatusFl = true;
                    upsiobj.Msg = "Data has been Deleted successfully !";

                    ouser.upsi = upsiobj;

                }
                else
                {
                    upsiobj.StatusFl = false;
                    upsiobj.Msg = "Data has Not been deleted !";
                    ouser.upsi = upsiobj;
                }

                return ouser;
            }
            catch (Exception ex)
            {
                UPSIResponse ouser1 = new UPSIResponse();
                UPSI upsi_cat = new UPSI();
                upsi_cat.StatusFl = false;
                upsi_cat.Msg = "Processing failed, because of system error !";
                ouser.upsi = upsi_cat;
                return ouser;
            }
        }
        public UPSIResponse HistoryUPSIGroup(UPSI objupsi)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "SELECT_UPSI_FOR_HISTORY");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2] = new SqlParameter("@UPSI_ID", Convert.ToInt32(objupsi.upsi_id));
                parameters[1].Direction = ParameterDirection.Output;

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objupsi.MODULE_DATABASE, parameters);
                //List<UPSI> listupsiobj = new List<UPSI>();
                List<UPSI> listupsiobj_for_edit = new List<UPSI>();
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {



                    while (rdr.Read())
                    {
                        UPSI obj = new UPSI();
                        obj.upsi_id = Convert.ToInt32(rdr["UPSI_ID"]);
                        obj.upsi_group = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_GROUP"]))) ? Convert.ToString(rdr["UPSI_GROUP"]) : String.Empty;
                        obj.upsi_desc = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_DESC"]))) ? Convert.ToString(rdr["UPSI_DESC"]) : String.Empty;
                        obj.listuser = GET_USER_HISTORY(Convert.ToInt32(rdr["UPSI_ID"]), objupsi.MODULE_DATABASE, Convert.ToInt32(rdr["VERSION"]));
                        obj.listGroupUserRemarks = GET_GROUP_USER_REMARKS(Convert.ToInt32(rdr["UPSI_ID"]), objupsi.MODULE_DATABASE, Convert.ToInt32(rdr["VERSION"]));
                        DateTime dt_from = Convert.ToDateTime(rdr["FROM_DATE"]);
                        string dd4 = dt_from.ToString("dd/MM/yyyy");
                        obj.from_date = dd4;
                        DateTime dt_till = Convert.ToDateTime(rdr["TILL_DATE"]);
                        string dd5 = dt_till.ToString("dd/MM/yyyy");
                        obj.till_date = dd5;

                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);

                        string dd6 = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dd6;
                        obj.version = Convert.ToInt32(rdr["VERSION"]);
                        obj.created_by = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"]))) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        listupsiobj_for_edit.Add(obj);
                    }

                    //upsiobj.listuser = listobj;

                    upsiobj.StatusFl = true;
                    upsiobj.Msg = "Data has been fetched successfully !";
                    ouser.upsilist = listupsiobj_for_edit;
                    ouser.upsi = upsiobj;

                }
                else
                {
                    upsiobj.StatusFl = false;
                    upsiobj.Msg = "No data found !";
                    ouser.upsi = upsiobj;
                }
                rdr.Close();
                return ouser;
            }
            catch (Exception ex)
            {
                UPSIResponse ouser1 = new UPSIResponse();
                UPSI upsi_cat = new UPSI();
                upsi_cat.StatusFl = false;
                upsi_cat.Msg = "Processing failed, because of system error !";
                ouser.upsi = upsi_cat;
                return ouser;
            }
        }
        public List<User> GET_USER_HISTORY(int id, string MODULE_DATABASE, int ver)
        {
            List<User> listobj = new List<User>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "USER_FORHISTORY");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2] = new SqlParameter("@UPSI_ID", id);
                parameters[3] = new SqlParameter("@VERSION", ver);
                parameters[1].Direction = ParameterDirection.Output;

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", MODULE_DATABASE, parameters);
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {



                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.ID = Convert.ToInt32(rdr["USER_ID"]);
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.designation = new Designation()
                        {
                            DESIGNATION_ID = Convert.ToInt32(rdr["DESIGNATION_ID"]),
                            DESIGNATION_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_NAME"]))) ? Convert.ToString(rdr["DESIGNATION_NAME"]) : String.Empty

                        };

                        listobj.Add(obj);
                    }
                    rdr.Close();
                    return listobj;


                }
                else
                {
                    return listobj;
                }


            }
            catch (Exception ex)
            {

            }


            return listobj;
        }
        public List<User> GET_GROUP_USER_REMARKS(int id, string MODULE_DATABASE, int ver)
        {
            List<User> listobj = new List<User>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_GROUP_USER_REMARKS");
                parameters[1] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2] = new SqlParameter("@UPSI_ID", id);
                parameters[3] = new SqlParameter("@VERSION", ver);
                parameters[1].Direction = ParameterDirection.Output;

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", MODULE_DATABASE, parameters);
                UPSI upsiobj = new UPSI();
                if (rdr.HasRows)
                {



                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.upsiType = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_TYPE"]))) ? Convert.ToString(rdr["UPSI_TYPE"]) : String.Empty;
                        obj.upsiSharedWith = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_SHARED_WITH"]))) ? Convert.ToString(rdr["UPSI_SHARED_WITH"]) : String.Empty;
                        obj.upsiSharedOn = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_SHARED_ON"]))) ? Convert.ToString(rdr["UPSI_SHARED_ON"]) : String.Empty;
                        obj.upsiSharedCC = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_SHARED_CC"]))) ? Convert.ToString(rdr["UPSI_SHARED_CC"]) : String.Empty;
                        obj.upsiPan = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_MEMBER_PAN"]))) ? Convert.ToString(rdr["UPSI_MEMBER_PAN"]) : String.Empty;
                        obj.remarks = (!String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"]))) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
                        obj.upsiAttachment = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPSI_ATTACHMENT"]))) ? Convert.ToString(rdr["UPSI_ATTACHMENT"]) : String.Empty;

                        listobj.Add(obj);
                    }
                    rdr.Close();
                    return listobj;


                }
                else
                {
                    return listobj;
                }


            }
            catch (Exception ex)
            {

            }


            return listobj;
        }
        public UPSIResponse send_email(UPSI objuser)
        {
            UPSIResponse ouser = new UPSIResponse();


            try
            {
                string strFrom = String.Empty;
                string SMTP_HOST_NAME = "";
                string SMTP_USER_NAME = "";
                string SMTP_USER_PASSWORD = "";
                int PORT = 0;

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objuser.COMPANY_ID);

                SmtpConfigResponse oSmtpConfig = new SmtpConfigResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objuser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        strFrom = Convert.ToString(rdr["DEFAULT_EMAIL"]);
                        SMTP_HOST_NAME = Convert.ToString(rdr["SMTP_HOST_NAME"]);
                        SMTP_USER_NAME = Convert.ToString(rdr["SMTP_USER_NAME"]);
                        string st = Convert.ToString(rdr["PASSWORD_OUTGOING"]);
                        SMTP_USER_PASSWORD = CryptorEngine.Decrypt(st, true);
                        PORT = Convert.ToInt32(rdr["PORT"]);
                    }
                }

                string strMsg = "";
                string strSubject = "UPSI Group - " + objuser.upsi_group + ".";

                string strTo = "";

                strMsg += "<p>Dear Sir/Madam,</p>";
                if (Convert.ToString(HttpContext.Current.Session["CompanyName"]) == "Spark Minda")
                {
                    strMsg += "<p>Please find the UPSI group details (" + objuser.upsi_group + ") that has been created by - Company Secretary / Minda Corporation Limited.";
                }
                else
                {
                    strMsg += "<p>Please find the UPSI group details (" + objuser.upsi_group + ") that has been created/modified by -" + objuser.created_by + ".";
                }

                strMsg += "<p>1. UPSI Group Name : " + objuser.upsi_group + "</p>";
                strMsg += "<p>2. Details: " + objuser.upsi_desc + "</p>";
                strMsg += "<p>3. Validity From: " + objuser.from_date + "</p>";
                strMsg += "<p>4. Validity Till: : " + objuser.till_date + "</p>";
                strMsg += "<p>5. UPSI Members</p>";
                char b = (char)('A');
                for (int i = 0; i < objuser.listuser.Count; i++)
                {

                    char seq = Convert.ToChar(i + 65);

                    strMsg += "<p>" + seq + ". " + objuser.listuser[i].USER_NM + " (" + objuser.listuser[i].USER_EMAIL + ") " + objuser.listuser[i].designation.DESIGNATION_NM + ".</p>";
                    if (i == 0)
                    {
                        strTo = objuser.listuser[i].USER_EMAIL;
                    }
                    else
                    {

                        strTo += "," + objuser.listuser[i].USER_EMAIL;
                    }


                }

                if (Convert.ToString(HttpContext.Current.Session["CompanyName"]) == "Spark Minda")
                {
                    strMsg += "<br/><p>Regards,<p><p>CS Team<p>";
                }
                else
                {

                    string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

                    strMsg += "<br/><br/><p>For more details, please logon to the below link</p><p>" + baseUrl + "Login.aspx</p><br/><p>Regards,<p><p>CS Team<p>";
                }



                using (MailMessage objMailMsg = new MailMessage(strFrom, strTo))
                {
                    objMailMsg.Subject = strSubject;
                    objMailMsg.IsBodyHtml = true;
                    objMailMsg.Body = strMsg;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTP_HOST_NAME;
                    smtp.EnableSsl = true;

                    if (SMTP_HOST_NAME.ToUpper().Contains("OFFICE365"))
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    }
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    NetworkCredential NetworkCred = new System.Net.NetworkCredential(SMTP_USER_NAME, SMTP_USER_PASSWORD);

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = PORT;
                    try
                    {
                        smtp.Send(objMailMsg);
                    }
                    catch (Exception ex)
                    {
                        new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                    }
                }

                UPSI uobj = new UPSI();
                uobj.StatusFl = true;
                uobj.Msg = "Mail  has been Send successfully !";
                ouser.upsi = uobj;
                ouser.StatusFl = true;
                ouser.Msg = "Mail  has been Send successfully !";

                return ouser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UPSI uobj = new UPSI();
                uobj.StatusFl = false;
                uobj.Msg = "Processing failed, because of system error !";
                ouser.upsi = uobj;
                ouser.StatusFl = false;
                ouser.Msg = "Processing failed, because of system error !";
                return ouser;
            }
        }
        public UPSIResponse SaveUPSIGroupRemarks(UPSI objUPSI)
        {
            try
            {
                UPSIResponse ouser = new UPSIResponse();
                SqlParameter[] parameters = new SqlParameter[12];
                parameters[0] = new SqlParameter("@UPSI_ID", objUPSI.upsi_id);
                parameters[1] = new SqlParameter("@REMARKS", objUPSI.remarks);
                parameters[2] = new SqlParameter("@COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@MODE", "SAVE_UPSI_GROUP_REMARKS");
                parameters[4] = new SqlParameter("@CREATED_BY", objUPSI.created_by);
                parameters[5] = new SqlParameter("@COMPANY_ID", objUPSI.COMPANY_ID);
                parameters[6] = new SqlParameter("@UPSI_TYPE", objUPSI.upsiType);
                parameters[7] = new SqlParameter("@UPSI_SHARED_WITH", objUPSI.upsiSharedWith);
                parameters[8] = new SqlParameter("@UPSI_SHARED_ON", ConvertDate(objUPSI.upsiSharedOn));
                parameters[9] = new SqlParameter("@UPSI_SHARED_CC", objUPSI.upsiSharedCC);
                parameters[10] = new SqlParameter("@UPSI_MEMBER_PAN", objUPSI.upsiPan);
                parameters[11] = new SqlParameter("@UPSI_ATTACHMENT", objUPSI.upsiAttachment);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI", objUPSI.MODULE_DATABASE, parameters);
                Int32 objValue = (Int32)(parameters[2].Value);
                UPSI upsi_cat = new UPSI();
                if (objValue > 0)
                {

                    upsi_cat.StatusFl = true;
                    upsi_cat.Msg = "Data has been saved successfully !";
                    ouser.StatusFl = true;
                    ouser.Msg = "Data has been saved successfully !";
                }
                else
                {
                    upsi_cat.StatusFl = false;
                    upsi_cat.Msg = "Something went wrong !";
                    ouser.StatusFl = false;
                    ouser.Msg = "Something went wrong !";
                }

                ouser.upsi = upsi_cat;
                return ouser;
            }
            catch (Exception ex)
            {
                UPSIResponse ouser = new UPSIResponse();
                UPSI upsi_cat = new UPSI();
                upsi_cat.StatusFl = false;
                upsi_cat.Msg = "Processing failed, because of system error !";
                ouser.upsi = upsi_cat;
                ouser.StatusFl = false;
                ouser.Msg = "Processing failed, because of system error !";
                return ouser;
            }
        }
        public UserResponse GetUsersForUPSI(User objUser)
        {
            _userResponse = new UserResponse();
            _userResponse.StatusFl = false;
            _userResponse.Msg = "No Data Found!";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    conn.ChangeDatabase(objUser.MODULE_DATABASE);
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_INSIDER_UPSI", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@MODE", "GET_USER_FOR_UPSI"));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objUser.companyId));
                        cmd.Parameters.Add(new SqlParameter("@NAME", objUser.USER_NM));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                User obj = new User();
                                obj.ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["ID"]))) ? Convert.ToInt32(rdr["ID"]) : 0;
                                obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                                obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                                obj.panNumber = (!String.IsNullOrEmpty(Convert.ToString(rdr["PAN"]))) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                                obj.LOGIN_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["LOGIN_ID"]))) ? Convert.ToString(rdr["LOGIN_ID"]) : String.Empty;
                                _userResponse.AddObject(obj);
                            }
                            _userResponse.StatusFl = true;
                            _userResponse.Msg = "Data has been fetched successfully !";
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _userResponse = new UserResponse();
                _userResponse.StatusFl = false;
                _userResponse.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
            }
            return _userResponse;
        }

        #region "Date Conversion"

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

        #endregion
    }
}