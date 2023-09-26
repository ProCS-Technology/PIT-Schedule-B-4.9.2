using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;


namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class HolidayRepository : IRequiresSessionState
    {
        public HolidayResponse GetHolidayList(Holiday objHoliday)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_Holiday_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                HolidayResponse aHoliday = new HolidayResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Holiday obj = new Holiday();
                        obj.ID = !String.IsNullOrEmpty(rdr["ID"].ToString()) ? Convert.ToInt32(rdr["ID"]) : 0;
                        obj.HOLIDAY_DESCRIPTION = !String.IsNullOrEmpty(rdr["HOLIDAY_DESCRIPTION"].ToString()) ? Convert.ToString(rdr["HOLIDAY_DESCRIPTION"]) : String.Empty;
                        obj.HOLIDAY_DATE = !String.IsNullOrEmpty(rdr["HOLIDAY_DATE"].ToString()) ? rdr["HOLIDAY_DATE"].ToString() : String.Empty;
                        aHoliday.AddObject(obj);
                    }
                    aHoliday.StatusFl = true;
                    aHoliday.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    aHoliday.StatusFl = false;
                    aHoliday.Msg = "No data found !";
                }
                rdr.Close();
                return aHoliday;
            }
            catch (Exception ex)
            {
                HolidayResponse aHoliday = new HolidayResponse();
                aHoliday.StatusFl = false;
                aHoliday.Msg = "Processing failed, because of system error !";
                return aHoliday;
            }
        }


        public HolidayResponse AddHoliday(Holiday objHoliday)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@ID", objHoliday.ID);
                parameters[1] = new SqlParameter("@HOLIDAY_DESCRIPTION", objHoliday.HOLIDAY_DESCRIPTION);
                parameters[2] = new SqlParameter("@HOLIDAY_DATE", FormatHelper.FormatDate(objHoliday.HOLIDAY_DATE));
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                HolidayResponse oHoliday = new HolidayResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[5].Direction = ParameterDirection.Output;

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);

                    //parameters[3] = new SqlParameter("@Mode", "GET_HOLIDAY_ID_BY_HOLIDAY_NAME");
                    //var ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);
                    objHoliday.ID = (Int32)(parameters[5].Value);
                    if (objHoliday.ID == -1)
                    {
                        oHoliday.StatusFl = false;
                        oHoliday.Msg = "Holiday Description already exists !";
                        oHoliday.Holiday = objHoliday;
                    }
                    else
                    {
                        oHoliday.StatusFl = true;
                        oHoliday.Msg = "Data has been saved successfully !";
                        oHoliday.Holiday = objHoliday;
                    }

                }
                else
                {
                    oHoliday.StatusFl = false;
                    oHoliday.Msg = "Holiday  aleady exists !";
                }
                return oHoliday;
            }
            catch (Exception ex)
            {
                HolidayResponse oHoliday = new HolidayResponse();
                oHoliday.StatusFl = false;
                oHoliday.Msg = "Processing failed, because of system error !";
                return oHoliday;
            }
        }
        public HolidayResponse UpdateHoliday(Holiday objHoliday)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@ID", objHoliday.ID);
                parameters[1] = new SqlParameter("@HOLIDAY_DESCRIPTION", objHoliday.HOLIDAY_DESCRIPTION);
                parameters[2] = new SqlParameter("@HOLIDAY_DATE", FormatHelper.ConvertDateTime(objHoliday.HOLIDAY_DATE));
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                HolidayResponse oHoliday = new HolidayResponse();
                if ((Int32)obj > 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[5].Direction = ParameterDirection.Output;

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);

                    //parameters[3] = new SqlParameter("@Mode", "GET_HOLIDAY_ID_BY_HOLIDAY_NAME");
                    //var ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);
                    if ((Int32)(parameters[5].Value) == -1)
                    {
                        objHoliday.ID = objHoliday.ID;
                        oHoliday.StatusFl = false;
                        oHoliday.Msg = "Holiday Description already exists !";
                        oHoliday.Holiday = objHoliday;
                    }
                    else
                    {
                        objHoliday.ID = objHoliday.ID;

                        oHoliday.StatusFl = true;
                        oHoliday.Msg = "Data has been updated successfully !";
                        oHoliday.Holiday = objHoliday;
                    }


                }
                else
                {
                    oHoliday.StatusFl = false;
                    oHoliday.Msg = "Holiday aleady exists !";
                }
                return oHoliday;
            }
            catch (Exception ex)
            {
                HolidayResponse oHoliday = new HolidayResponse();
                oHoliday.StatusFl = false;
                oHoliday.Msg = "Processing failed, because of system error !";
                return oHoliday;
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
                throw ex;
            }

            return Convert.ToDateTime(str);
        }

        #endregion

        public HolidayResponse DeleteHoliday(Holiday objHoliday)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", objHoliday.ID);
                parameters[1] = new SqlParameter("@Mode", "Delete_Holiday");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                HolidayResponse oHoliday = new HolidayResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_HOLIDAY", objHoliday.MODULE_DATABASE, parameters);
                oHoliday.StatusFl = true;
                oHoliday.Msg = "Data has been deleted successfully !";
                oHoliday.Holiday = objHoliday;
                return oHoliday;
            }
            catch (Exception ex)
            {
                HolidayResponse oGrp = new HolidayResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

    }

}

