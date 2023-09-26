using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class ReminderSetUpRepository
    {

        public ReminderSetUpResponse GetReminderName(ReminderSetUp objReminderSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objReminderSetUp.Company_Id);
                parameters[1] = new SqlParameter("@Mode", "GET_REMINDER_NAME");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", objReminderSetUp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objReminderSetUp = new ReminderSetUp();
                        objReminderSetUp.REMINDER_ID = Convert.ToInt32(rdr["EVENT_ID"]);
                        objReminderSetUp.REMINDER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["EVENT_NM"]))) ? Convert.ToString(rdr["EVENT_NM"]) : String.Empty;
                        oGrp.AddObject(objReminderSetUp);
                    }
                    oGrp.StatusFl = true;
                   oGrp.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }


        public ReminderSetUpResponse GetMailEventName(ReminderSetUp objReminderSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objReminderSetUp.Company_Id);
                parameters[1] = new SqlParameter("@Mode", "GET_MAILREMINDER_NAME");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", objReminderSetUp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objReminderSetUp = new ReminderSetUp();
                        objReminderSetUp.REMINDER_ID = Convert.ToInt32(rdr["EVENT_ID"]);
                        objReminderSetUp.REMINDER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["EVENT_NM"]))) ? Convert.ToString(rdr["EVENT_NM"]) : String.Empty;
                        oGrp.AddObject(objReminderSetUp);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data Fetched !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

        public ReminderSetUpResponse GetFieldName(ReminderSetUp objReminderSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objReminderSetUp.Company_Id);
                parameters[1] = new SqlParameter("@Mode", "GET_FIELD_NAME");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3] = new SqlParameter("@REMINDER_NM", objReminderSetUp.REMINDER_NM);
                parameters[2].Direction = ParameterDirection.Output;

                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", objReminderSetUp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objReminderSetUp = new ReminderSetUp();
                        //objReminderSetUp.FIELD_ID = Convert.ToInt32(rdr["FIELD_ID"]);
                        objReminderSetUp.FIELD_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["FIELD_NM"]))) ? Convert.ToString(rdr["FIELD_NM"]) : String.Empty;
                        oGrp.AddObject(objReminderSetUp);
                    }
                    oGrp.StatusFl = true;
                    //oGrp.Msg = "Department Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

        public ReminderSetUpResponse GetMailEventFieldName(ReminderSetUp objReminderSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objReminderSetUp.Company_Id);
                parameters[1] = new SqlParameter("@Mode", "GET_MAILEVENT_FIELD_NAME");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3] = new SqlParameter("@REMINDER_NM", objReminderSetUp.REMINDER_NM);
                parameters[2].Direction = ParameterDirection.Output;

                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", objReminderSetUp.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objReminderSetUp = new ReminderSetUp();
                        //objReminderSetUp.FIELD_ID = Convert.ToInt32(rdr["FIELD_ID"]);
                        objReminderSetUp.FIELD_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["FIELD_NM"]))) ? Convert.ToString(rdr["FIELD_NM"]) : String.Empty;
                        oGrp.AddObject(objReminderSetUp);
                    }
                    oGrp.StatusFl = true;
                    //oGrp.Msg = "Department Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse oGrp = new ReminderSetUpResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }
        

public ReminderSetUpResponse getAllMailReminderType(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_MAILREMINDER_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));

                List<ReminderSetUp> oRem = new List<ReminderSetUp>();
                ReminderSetUpResponse res = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReminderSetUp obj = new ReminderSetUp();
                        obj.REMINDER_ID = !String.IsNullOrEmpty(rdr["EVENT_ID"].ToString()) ? Convert.ToInt32(rdr["EVENT_ID"]) : 0;
                        obj.REMINDER_NM = !String.IsNullOrEmpty(rdr["EVENT_NM"].ToString()) ? Convert.ToString(rdr["EVENT_NM"]) : String.Empty;
                         obj.TEMPLATE = !String.IsNullOrEmpty(rdr["TEMPLATE"].ToString()) ? Convert.ToString(rdr["TEMPLATE"]) : String.Empty;
                        obj.SUBJECT = !String.IsNullOrEmpty(rdr["SUBJECT"].ToString()) ? Convert.ToString(rdr["SUBJECT"]) : String.Empty;
                        //obj.REMINDER_NM = !String.IsNullOrEmpty(rdr["EVENT_NM"].ToString()) ? Convert.ToString(rdr["EVENT_NM"]) : String.Empty;
                        //DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        //obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        obj.Created_By = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        oRem.Add(obj);
                    }

                    res.listReminder = oRem;
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
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }



        }


        public ReminderSetUpResponse getAllReminderType(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_REMINDER_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));

                List<ReminderSetUp> oRem = new List<ReminderSetUp>();
                ReminderSetUpResponse res = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReminderSetUp obj = new ReminderSetUp();
                        obj.REMINDER_ID = !String.IsNullOrEmpty(rdr["REMINDER_ID"].ToString()) ? Convert.ToInt32(rdr["REMINDER_ID"]) : 0;
                        obj.REMINDER_NM = !String.IsNullOrEmpty(rdr["REMINDER_NM"].ToString()) ? Convert.ToString(rdr["REMINDER_NM"]) : String.Empty;
                        obj.REMINDER_TYPE = !String.IsNullOrEmpty(rdr["REMINDER_TYPE"].ToString()) ? Convert.ToString(rdr["REMINDER_TYPE"]) : String.Empty;
                        obj.REMINDER_TYPE_VALUE = !String.IsNullOrEmpty(rdr["REMINDER_TYPE_VALUE"].ToString()) ? Convert.ToString(rdr["REMINDER_TYPE_VALUE"]) : String.Empty;
                        obj.DURATION = !String.IsNullOrEmpty(rdr["DURATION"].ToString()) ? Convert.ToString(rdr["DURATION"]) : String.Empty;
                        obj.TEMPLATE = !String.IsNullOrEmpty(rdr["TEMPLATE"].ToString()) ? Convert.ToString(rdr["TEMPLATE"]) : String.Empty;
                        //obj.REMINDER_NM = !String.IsNullOrEmpty(rdr["EVENT_NM"].ToString()) ? Convert.ToString(rdr["EVENT_NM"]) : String.Empty;
                        //DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        //obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        obj.Created_By = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        oRem.Add(obj);
                    }

                    res.listReminder = oRem;
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
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
        }


        public ReminderSetUpResponse getallMailReminderTypeById(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_MAILREMINDER_LIST_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));

                List<ReminderSetUp> oRem = new List<ReminderSetUp>();
                ReminderSetUpResponse res = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReminderSetUp obj = new ReminderSetUp();
                        obj.REMINDER_ID = !String.IsNullOrEmpty(rdr["EVENT_ID"].ToString()) ? Convert.ToInt32(rdr["EVENT_ID"]) : 0;
                        obj.REMINDER_NM = !String.IsNullOrEmpty(rdr["EVENT_NM"].ToString()) ? Convert.ToString(rdr["EVENT_NM"]) : String.Empty;
                        //obj.REMINDER_TYPE = !String.IsNullOrEmpty(rdr["REMINDER_TYPE"].ToString()) ? Convert.ToString(rdr["REMINDER_TYPE"]) : String.Empty;
                        //obj.REMINDER_TYPE_VALUE = !String.IsNullOrEmpty(rdr["REMINDER_TYPE_VALUE"].ToString()) ? Convert.ToString(rdr["REMINDER_TYPE_VALUE"]) : String.Empty;
                        //obj.DURATION = !String.IsNullOrEmpty(rdr["DURATION"].ToString()) ? Convert.ToString(rdr["DURATION"]) : String.Empty;
                        obj.SUBJECT = !String.IsNullOrEmpty(rdr["SUBJECT"].ToString()) ? Convert.ToString(rdr["SUBJECT"]) : String.Empty;
                        obj.TEMPLATE = !String.IsNullOrEmpty(rdr["TEMPLATE"].ToString()) ? Convert.ToString(rdr["TEMPLATE"]) : String.Empty;
                        //DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        //obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        obj.Created_By = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        oRem.Add(obj);
                    }

                    res.listReminder = oRem;
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
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
        }




        public ReminderSetUpResponse getAllReminderTypeByID(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_REMINDER_LIST_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));

                List<ReminderSetUp> oRem = new List<ReminderSetUp>();
                ReminderSetUpResponse res = new ReminderSetUpResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ReminderSetUp obj = new ReminderSetUp();
                        obj.REMINDER_ID = !String.IsNullOrEmpty(rdr["REMINDER_ID"].ToString()) ? Convert.ToInt32(rdr["REMINDER_ID"]) : 0;
                        obj.REMINDER_NM = !String.IsNullOrEmpty(rdr["REMINDER_NM"].ToString()) ? Convert.ToString(rdr["REMINDER_NM"]) : String.Empty;
                        obj.REMINDER_TYPE = !String.IsNullOrEmpty(rdr["REMINDER_TYPE"].ToString()) ? Convert.ToString(rdr["REMINDER_TYPE"]) : String.Empty;
                        obj.REMINDER_TYPE_VALUE = !String.IsNullOrEmpty(rdr["REMINDER_TYPE_VALUE"].ToString()) ? Convert.ToString(rdr["REMINDER_TYPE_VALUE"]) : String.Empty;
                        obj.DURATION = !String.IsNullOrEmpty(rdr["DURATION"].ToString()) ? Convert.ToString(rdr["DURATION"]) : String.Empty;
                        obj.SUBJECT = !String.IsNullOrEmpty(rdr["SUBJECT"].ToString()) ? Convert.ToString(rdr["SUBJECT"]) : String.Empty;
                        obj.TEMPLATE = !String.IsNullOrEmpty(rdr["TEMPLATE"].ToString()) ? Convert.ToString(rdr["TEMPLATE"]) : String.Empty;
                        //DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        //obj.CreatedOn = dt.ToString("dd/MM/yyyy");
                        obj.Created_By = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;

                        oRem.Add(obj);
                    }

                    res.listReminder = oRem;
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
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
            }

        public ReminderSetUpResponse MailReminderSave(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@MODE", "MAILREMINDER_SAVE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));
                parameters[4] = new SqlParameter("@REMINDER_NM", Convert.ToString(remObj.REMINDER_NM));
                parameters[5] = new SqlParameter("@SUBJECT", Convert.ToString(remObj.SUBJECT));
                parameters[6] = new SqlParameter("@TEMPLATE", Convert.ToString(remObj.TEMPLATE));
                parameters[7] = new SqlParameter("@CREATED_BY", Convert.ToString(remObj.Created_By));


                ReminderSetUpResponse res = new ReminderSetUpResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);
                int result = Convert.ToInt32(parameters[1].Value);
                if (result == 0)
                {
                    res.StatusFl = true;
                    res.Msg = "Data has been saved successfully !";
                    return res;
                }
                else
                {
                    res.StatusFl = true;
                    res.Msg = "Reminder Name Already Exists";
                    return res;
                }
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
        }

        public ReminderSetUpResponse ReminderSave(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@MODE", "REMINDER_SAVE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));
                parameters[4] = new SqlParameter("@REMINDER_NM", Convert.ToString(remObj.REMINDER_NM));
                parameters[5] = new SqlParameter("@REMINDER_TYPE", Convert.ToString(remObj.REMINDER_TYPE));
                parameters[6] = new SqlParameter("@REMINDER_VALUE", Convert.ToString(remObj.REMINDER_TYPE_VALUE)); 
                 parameters[7] = new SqlParameter("@DURATION", Convert.ToInt32(remObj.DURATION));
                parameters[8] = new SqlParameter("@SUBJECT", Convert.ToString(remObj.SUBJECT));
                parameters[9] = new SqlParameter("@TEMPLATE", Convert.ToString(remObj.TEMPLATE));
                parameters[10] = new SqlParameter("@CREATED_BY", Convert.ToString(remObj.Created_By));


                ReminderSetUpResponse res = new ReminderSetUpResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);
                int result = Convert.ToInt32(parameters[1].Value);
                if (result == 0)
                {
                    res.StatusFl = true;
                    res.Msg = "Data has been saved successfully !";
                    return res;
                }
                else
                {
                    res.StatusFl = true;
                    res.Msg = "Reminder Name Already Exists";
                    return res;
                }                
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
          }

        public ReminderSetUpResponse MailReminderDelete(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "MAILREMINDER_DELETE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));


                ReminderSetUpResponse res = new ReminderSetUpResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);


                res.StatusFl = true;
                res.Msg = "Data Deleted successfully !";
                return res;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
        }
        public ReminderSetUpResponse ReminderDelete(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "REMINDER_DELETE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));


                ReminderSetUpResponse res = new ReminderSetUpResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);


                res.StatusFl = true;
                res.Msg = "Data Deleted successfully !";
                return res;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }
        }

        public ReminderSetUpResponse MailReminderUpdate(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@MODE", "MAILREMINDER_UPDATE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));
                parameters[4] = new SqlParameter("@REMINDER_NM", Convert.ToString(remObj.REMINDER_NM));
                parameters[5] = new SqlParameter("@SUBJECT", Convert.ToString(remObj.SUBJECT));
                parameters[6] = new SqlParameter("@TEMPLATE", Convert.ToString(remObj.TEMPLATE));
                parameters[7] = new SqlParameter("@CREATED_BY", Convert.ToString(remObj.Created_By));


                ReminderSetUpResponse res = new ReminderSetUpResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);


                res.StatusFl = true;
                res.Msg = "Data has been updated successfully !";
                return res;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }

        }

        public ReminderSetUpResponse ReminderUpdate(ReminderSetUp remObj)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@MODE", "REMINDER_UPDATE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(remObj.Company_Id));
                parameters[3] = new SqlParameter("@REMINDER_ID", Convert.ToInt32(remObj.REMINDER_ID));
                parameters[4] = new SqlParameter("@REMINDER_NM", Convert.ToString(remObj.REMINDER_NM));
                parameters[5] = new SqlParameter("@REMINDER_TYPE", Convert.ToString(remObj.REMINDER_TYPE));
                parameters[6] = new SqlParameter("@REMINDER_VALUE", Convert.ToString(remObj.REMINDER_TYPE_VALUE));
                parameters[7] = new SqlParameter("@DURATION", Convert.ToInt32(remObj.DURATION));
                parameters[8] = new SqlParameter("@SUBJECT", Convert.ToString(remObj.SUBJECT));
                parameters[9] = new SqlParameter("@TEMPLATE", Convert.ToString(remObj.TEMPLATE));
                parameters[10] = new SqlParameter("@CREATED_BY", Convert.ToString(remObj.Created_By));


                ReminderSetUpResponse res = new ReminderSetUpResponse();
                Int64 ct = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REMINDER_SETUP", remObj.MODULE_DATABASE, parameters);


                res.StatusFl = true;
                res.Msg = "Data has been updated successfully !";
                return res;
            }
            catch (Exception ex)
            {
                ReminderSetUpResponse rem = new ReminderSetUpResponse();
                rem.StatusFl = false;
                rem.Msg = "Processing failed, because of system error !";
                return rem;
            }

        }

    }
}