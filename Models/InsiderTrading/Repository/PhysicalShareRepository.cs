using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class PhysicalShareRepository
    {
        public PhysicalShareResponce AddPhysicalShare(PhysicalShareMaster objphysicalshare)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@PHYSICAL_SHARE_ID", objphysicalshare.physical_share_id);
                parameters[1] = new SqlParameter("@NAME", objphysicalshare.name);
                parameters[2] = new SqlParameter("@ISSUE_DATE", ConvertDate(objphysicalshare.issue_date));
                parameters[3] = new SqlParameter("@MODE", "INSERT_SHARE");
                parameters[4] = new SqlParameter("@QTY", objphysicalshare.qty);
                parameters[5] = new SqlParameter("@CREATED_BY", objphysicalshare.created_by);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7] = new SqlParameter("@COMPANY_ID", objphysicalshare.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PHYSICAL_SHARE", objphysicalshare.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                if ((Int32)obj > 0)
                {


                    oSHARE.StatusFl = true;
                    oSHARE.Msg = "Data has been saved successfully !";

                }
                else
                {
                    oSHARE.StatusFl = false;
                    oSHARE.Msg = "Department Name aleady exists !";
                }
                return oSHARE;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                oSHARE.StatusFl = false;
                oSHARE.Msg = "Processing failed, because of system error !";
                return oSHARE;
            }
        }
        public PhysicalShareResponce UpdatePhysicalShare(PhysicalShareMaster objphysicalshare)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@PHYSICAL_SHARE_ID", objphysicalshare.physical_share_id);
                parameters[1] = new SqlParameter("@NAME", objphysicalshare.name);
                parameters[2] = new SqlParameter("@ISSUE_DATE", ConvertDate(objphysicalshare.issue_date));
                parameters[3] = new SqlParameter("@MODE", "UPDATE_DATA");
                parameters[4] = new SqlParameter("@QTY", objphysicalshare.qty);
                parameters[5] = new SqlParameter("@CREATED_BY", objphysicalshare.created_by);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7] = new SqlParameter("@COMPANY_ID", objphysicalshare.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PHYSICAL_SHARE", objphysicalshare.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                if ((Int32)obj > 0)
                {


                    oSHARE.StatusFl = true;
                    oSHARE.Msg = "Data has been saved successfully !";

                }
                else
                {
                    oSHARE.StatusFl = false;
                    oSHARE.Msg = "Physical Share Name aleady exists !";
                }
                return oSHARE;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                oSHARE.StatusFl = false;
                oSHARE.Msg = "Processing failed, because of system error !";
                return oSHARE;
            }
        }

        public PhysicalShareResponce viewPhysicalShare(PhysicalShareMaster objphysicalshare)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter("@MODE", "VIEW_ALL");

                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objphysicalshare.COMPANY_ID);

                //SQLHelper.ExecuteNonQuery();
                SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PHYSICAL_SHARE", objphysicalshare.MODULE_DATABASE, parameters);

                // var obj = parameters[6].Value;
                List<PhysicalShareMaster> LISHARE = new List<PhysicalShareMaster>();
                PhysicalShareResponce rSHARE = new PhysicalShareResponce();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        PhysicalShareMaster oSHARE = new PhysicalShareMaster();

                        oSHARE.physical_share_id = Convert.ToInt32(rd["PHYSICAL_SHARE_ID"]);
                        oSHARE.name = !String.IsNullOrEmpty(Convert.ToString(rd["name"])) ? Convert.ToString(rd["name"]) : String.Empty;
                        //DateTime dt = Convert.ToDateTime(rd["ISSUE_DATE"]);
                        //string DD = dt.ToString("dd/MM/yyyy");
                        oSHARE.issue_date = !String.IsNullOrEmpty(Convert.ToString(rd["ISSUE_DATE"])) ? Convert.ToString(rd["ISSUE_DATE"]) : String.Empty;
                        oSHARE.qty = Convert.ToInt32(rd["QTY"]);
                        oSHARE.created_by = !String.IsNullOrEmpty(Convert.ToString(rd["CREATED_BY"])) ? Convert.ToString(rd["CREATED_BY"]) : String.Empty;

                        LISHARE.Add(oSHARE);

                    }





                    rSHARE.StatusFl = true;
                    rSHARE.PhysicalShareList = LISHARE;
                    rSHARE.Msg = "Data has been saved successfully !";

                }
                else
                {
                    rSHARE.StatusFl = false;
                    rSHARE.Msg = "Data Not Found !";
                }
                rd.Close();
                return rSHARE;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                oSHARE.StatusFl = false;
                oSHARE.Msg = "Processing failed, because of system error !";
                return oSHARE;
            }
        }
        public PhysicalShareResponce editPhysicalShare(PhysicalShareMaster objphysicalshare)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@PHYSICAL_SHARE_ID", objphysicalshare.physical_share_id);
                parameters[1] = new SqlParameter("@MODE", "EDIT_DATA");

                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                parameters[3] = new SqlParameter("@COMPANY_ID", objphysicalshare.COMPANY_ID);


                //SQLHelper.ExecuteNonQuery();
                SqlDataReader rd = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PHYSICAL_SHARE", objphysicalshare.MODULE_DATABASE, parameters);

                // var obj = parameters[6].Value;
                List<PhysicalShareMaster> LISHARE = new List<PhysicalShareMaster>();
                PhysicalShareResponce rSHARE = new PhysicalShareResponce();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        PhysicalShareMaster oSHARE = new PhysicalShareMaster();

                        oSHARE.physical_share_id = Convert.ToInt32(rd["PHYSICAL_SHARE_ID"]);
                        oSHARE.name = !String.IsNullOrEmpty(Convert.ToString(rd["name"])) ? Convert.ToString(rd["name"]) : String.Empty;
                        //DateTime dt = Convert.ToDateTime(rd["ISSUE_DATE"]);
                        //string DD = dt.ToString("dd/MM/yyyy");
                        oSHARE.issue_date = !String.IsNullOrEmpty(Convert.ToString(rd["ISSUE_DATE"])) ? Convert.ToString(rd["ISSUE_DATE"]) : String.Empty;
                        oSHARE.qty = Convert.ToInt32(rd["QTY"]);
                        oSHARE.created_by = !String.IsNullOrEmpty(Convert.ToString(rd["CREATED_BY"])) ? Convert.ToString(rd["CREATED_BY"]) : String.Empty;

                        LISHARE.Add(oSHARE);

                    }





                    rSHARE.StatusFl = true;
                    rSHARE.PhysicalShareList = LISHARE;
                    rSHARE.Msg = "Data has been saved successfully !";

                }
                else
                {
                    rSHARE.StatusFl = false;
                    rSHARE.Msg = "Data Not Found !";
                }
                rd.Close();
                return rSHARE;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                oSHARE.StatusFl = false;
                oSHARE.Msg = "Processing failed, because of system error !";
                return oSHARE;
            }
        }

        public PhysicalShareResponce deletePhysicalShare(PhysicalShareMaster objphysicalshare)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@PHYSICAL_SHARE_ID", objphysicalshare.physical_share_id);

                parameters[1] = new SqlParameter("@MODE", "DELETE_DATA");

                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                parameters[3] = new SqlParameter("@COMPANY_ID", objphysicalshare.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PHYSICAL_SHARE", objphysicalshare.MODULE_DATABASE, parameters);
                var obj = parameters[2].Value;
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                if ((Int32)obj > 0)
                {


                    oSHARE.StatusFl = true;
                    oSHARE.Msg = "Data has been deleted successfully !";

                }
                else
                {
                    oSHARE.StatusFl = false;
                    oSHARE.Msg = "Physical Share Name aleady Lock !";
                }
                return oSHARE;
            }
            catch (Exception ex)
            {
                PhysicalShareResponce oSHARE = new PhysicalShareResponce();
                oSHARE.StatusFl = false;
                oSHARE.Msg = "Processing failed, because of system error !";
                return oSHARE;
            }
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