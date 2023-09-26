using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class ReminderModuleRepository
    {

        public ReminderModuleResponse getAllActivity(ReminderModule remObj)
        {
            ReminderModuleResponse responce = new ReminderModuleResponse();

            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "LIST_ACTIVITY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.COMPANY_ID));

                List<ReminderModule> oRem = new List<ReminderModule>();
                ReminderModuleResponse res = new ReminderModuleResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_MODULE", remObj.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReminderModule obj = new ReminderModule();
                        obj.ACTIVITY_ID = !String.IsNullOrEmpty(rdr["ACTIVITY_ID"].ToString()) ? Convert.ToString(rdr["ACTIVITY_ID"]) : String.Empty;
                        obj.ACTIVITY_NAME = !String.IsNullOrEmpty(rdr["ACTIVITY_NAME"].ToString()) ? Convert.ToString(rdr["ACTIVITY_NAME"]) : String.Empty;
                        obj.MODULE_NAME = !String.IsNullOrEmpty(rdr["MODULE_NAME"].ToString()) ? Convert.ToString(rdr["MODULE_NAME"]) : String.Empty;
                        obj.REMINDED_IN = !String.IsNullOrEmpty(rdr["REMINDED_IN"].ToString()) ? Convert.ToString(rdr["REMINDED_IN"]) : String.Empty;
                        obj.UNIT_OF_MEASURE = !String.IsNullOrEmpty(rdr["UNIT_OF_MEASURE"].ToString()) ? Convert.ToString(rdr["UNIT_OF_MEASURE"]) : String.Empty;

                        //DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        //obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        // obj.Created_By = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        oRem.Add(obj);
                    }

                    res.listReminderModules = oRem;
                    res.StatusFl = true;
                    res.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    res.StatusFl = false;
                    res.Msg = "No data found !";
                }
                rdr.Close();
                return res;
            }
            catch (Exception ex)
            {
                ReminderModuleResponse rem = new ReminderModuleResponse();
                rem.StatusFl = false;
                rem.Msg = ex.Message;
                // rem.Msg = "Processing failed, because of system error !";
                return rem;
            }





        }
        public ReminderModuleResponse GetActivityById(ReminderModule remObj)
        {
            ReminderModuleResponse responce = new ReminderModuleResponse();

            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "LIST_FIELD");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.COMPANY_ID));
                parameters[3] = new SqlParameter("@ACTIVITY_ID", Convert.ToInt32(remObj.ACTIVITY_ID));

                List<ReminderModuleField> oRem = new List<ReminderModuleField>();
                ReminderModuleResponse res = new ReminderModuleResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_MODULE", remObj.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReminderModuleField obj = new ReminderModuleField();
                        obj.filedid = !String.IsNullOrEmpty(rdr["FIELD_ID"].ToString()) ? Convert.ToString(rdr["FIELD_ID"]) : String.Empty;
                        obj.fieldname = !String.IsNullOrEmpty(rdr["FIELD_NAME"].ToString()) ? Convert.ToString(rdr["FIELD_NAME"]) : String.Empty;
                        obj.column = !String.IsNullOrEmpty(rdr["FIELD_COLUMN"].ToString()) ? Convert.ToString(rdr["FIELD_COLUMN"]) : String.Empty;
                        obj.MsgTemplate = !String.IsNullOrEmpty(rdr["MSG_TEMPLATE"].ToString()) ? Convert.ToString(rdr["MSG_TEMPLATE"]) : String.Empty;


                        //DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        //obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        // obj.Created_By = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        oRem.Add(obj);
                    }
                    ReminderModule rm = new ReminderModule();
                    rm.listfield = oRem;
                    res.reminderModules = rm;
                    res.StatusFl = true;
                    res.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    res.StatusFl = false;
                    res.Msg = "No data found !";
                }
                rdr.Close();
                return res;
            }
            catch (Exception ex)
            {
                ReminderModuleResponse rem = new ReminderModuleResponse();
                rem.StatusFl = false;
                rem.Msg = ex.Message;
                // rem.Msg = "Processing failed, because of system error !";
                return rem;
            }





        }




        public ReminderModuleResponse UpdateActivityById(ReminderModule remObj)
        {



            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_ACTIVITY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.COMPANY_ID));
                parameters[3] = new SqlParameter("@ACTIVITY_ID", Convert.ToInt32(remObj.ACTIVITY_ID));
                parameters[4] = new SqlParameter("@REMINDED_IN", Convert.ToString(remObj.REMINDED_IN));
                parameters[5] = new SqlParameter("@UNIT_OF_MEASURE", Convert.ToString(remObj.UNIT_OF_MEASURE));
                parameters[6] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(remObj.MSG_TEMPLATE));
                parameters[7] = new SqlParameter("@CREATED_BY", Convert.ToString(remObj.CREATED_BY));


                ReminderModuleResponse res = new ReminderModuleResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_MODULE", remObj.MODULE_DATABASE, parameters);


                res.StatusFl = true;
                res.Msg = "Data has been Updated successfully !";
                return res;
            }
            catch (Exception ex)
            {
                ReminderModuleResponse rem = new ReminderModuleResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }



        }


    }
}