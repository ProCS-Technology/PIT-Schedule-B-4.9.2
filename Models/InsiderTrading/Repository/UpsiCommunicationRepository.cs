using System;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;


namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UpsiCommunicationRepository : IRequiresSessionState
    {

        public UpsiCommunicationResponse GetCommunicationtypeList(UpsiCommunicationtype objUpsiCommunicationtype)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_COMMUNICATION_TYPE_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUpsiCommunicationtype.COMPANY_ID);

                UpsiCommunicationResponse oUpsiCommunication = new UpsiCommunicationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UpsiCommunicationtype obj = new UpsiCommunicationtype();
                        obj.COMMTYPE_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.COMMTYPE_NAME = Convert.ToString(rdr.GetValue(1));
                        obj.CREATE_BY = Convert.ToString(rdr.GetValue(2));
                        obj.CREATED_ON = Convert.ToString(rdr.GetValue(3));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(4));
                        oUpsiCommunication.AddObject(obj);
                    }
                    oUpsiCommunication.StatusFl = true;
                    oUpsiCommunication.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oUpsiCommunication.StatusFl = false;
                    oUpsiCommunication.Msg = "No data found !";
                }
                rdr.Close();
                return oUpsiCommunication;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse oUpsiCommunication = new UpsiCommunicationResponse();
                oUpsiCommunication.StatusFl = false;
                oUpsiCommunication.Msg = "Processing failed, because of system error !";
                return oUpsiCommunication;
            }
        }

        public UpsiCommunicationResponse SaveCommunicationtype(UpsiCommunicationtype objUpsiCommunicationtype)
        {
            try
            {
                UpsiCommunicationResponse oCommunicationtype = new UpsiCommunicationResponse();
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@COMMTYPE_ID", objUpsiCommunicationtype.COMMTYPE_ID);
                parameters[1] = new SqlParameter("@COMMTYPE_NAME", objUpsiCommunicationtype.COMMTYPE_NAME);
                parameters[2] = new SqlParameter("@COMPANY_ID", objUpsiCommunicationtype.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objUpsiCommunicationtype.CREATE_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;
                if(string.IsNullOrEmpty(objUpsiCommunicationtype.COMMTYPE_NAME))
                {
                    oCommunicationtype = new UpsiCommunicationResponse();
                    oCommunicationtype.StatusFl = false;
                    oCommunicationtype.Msg = "NULL VALUE";
                    return oCommunicationtype;

                }
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
             
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);

                    parameters[3] = new SqlParameter("@Mode", "GET_COMMUNICATIONTYPE_ID_BY_DESIGNATION_NAME");
                    var commtype_id = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);
                    objUpsiCommunicationtype.COMMTYPE_ID = (Int32)commtype_id;

                    oCommunicationtype.StatusFl = true;
                    oCommunicationtype.Msg = "Data has been saved successfully !";
                    oCommunicationtype.UpsiCommunicationtype = objUpsiCommunicationtype;

                }
                else
                {
                    oCommunicationtype.StatusFl = false;
                    oCommunicationtype.Msg = "Name aleady exists !";
                }
                return oCommunicationtype;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse oCommunicationtype = new UpsiCommunicationResponse();
                oCommunicationtype.StatusFl = false;
                oCommunicationtype.Msg = "Processing failed, because of system error !";
                return oCommunicationtype;
            }
        }


        public UpsiCommunicationResponse UPDATECommunicationtype(UpsiCommunicationtype objUpsiCommunicationtype)
        {
            try
            {
                UpsiCommunicationResponse oCommunicationtype = new UpsiCommunicationResponse();
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@COMMTYPE_ID", objUpsiCommunicationtype.COMMTYPE_ID);
                parameters[1] = new SqlParameter("@COMMTYPE_NAME", objUpsiCommunicationtype.COMMTYPE_NAME);
                parameters[2] = new SqlParameter("@COMPANY_ID", objUpsiCommunicationtype.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objUpsiCommunicationtype.CREATE_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;
                if (string.IsNullOrEmpty(objUpsiCommunicationtype.COMMTYPE_NAME))
                {
                    oCommunicationtype = new UpsiCommunicationResponse();
                    oCommunicationtype.StatusFl = false;
                    oCommunicationtype.Msg = "NULL VALUE";
                    return oCommunicationtype;

                }
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;

                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);

                    parameters[3] = new SqlParameter("@Mode", "GET_COMMUNICATIONTYPE_ID_BY_DESIGNATION_NAME");
                    var commtype_id = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);
                    objUpsiCommunicationtype.COMMTYPE_ID = (Int32)commtype_id;

                    oCommunicationtype.StatusFl = true;
                    oCommunicationtype.Msg = "Data has been updated successfully !";
                    oCommunicationtype.UpsiCommunicationtype = objUpsiCommunicationtype;

                }
                else
                {
                    oCommunicationtype.StatusFl = false;
                    oCommunicationtype.Msg = "Name aleady exists !";
                }
                return oCommunicationtype;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse oCommunicationtype = new UpsiCommunicationResponse();
                oCommunicationtype.StatusFl = false;
                oCommunicationtype.Msg = "Processing failed, because of system error !";
                return oCommunicationtype;
            }
        }
        public UpsiCommunicationResponse DeleteCommunicationtype(UpsiCommunicationtype objUpsiCommunicationtype)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMMTYPE_ID", objUpsiCommunicationtype.COMMTYPE_ID);
                parameters[1] = new SqlParameter("@COMPANY_ID", objUpsiCommunicationtype.COMPANY_ID);
                parameters[2] = new SqlParameter("@Mode", "Delete_COMMUNICATION_TYPE");
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                UpsiCommunicationResponse oCommunicationtype = new UpsiCommunicationResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COMMUNICATION_TYPE", objUpsiCommunicationtype.MODULE_DATABASE, parameters);
                oCommunicationtype.StatusFl = true;
                oCommunicationtype.Msg = "Data has been set inactive successfully !";
                oCommunicationtype.UpsiCommunicationtype = objUpsiCommunicationtype;
                return oCommunicationtype;
            }
            catch (Exception ex)
            {
                UpsiCommunicationResponse oGrp = new UpsiCommunicationResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }




    }
}