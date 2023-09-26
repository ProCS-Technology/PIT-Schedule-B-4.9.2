using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class ModelCodeConductRepository : IRequiresSessionState
    {
        public ModelCodeConductResponse AddModelCodeConduct(ModelCodeConduct objModelCodeConduct)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[13];
                parameters[0] = new SqlParameter("@MODEL_ID", objModelCodeConduct.MODEL_ID);
                parameters[1] = new SqlParameter("@FREQUENCY_OF_PERIOD", objModelCodeConduct.FREQUENCY_OF_PERIOD);
                parameters[2] = new SqlParameter("@CUT_OFF_DATES_FOR_PERIOD", objModelCodeConduct.CUT_OFF_DATES_FOR_PERIOD);
                parameters[3] = new SqlParameter("@RESTRICTED_MONTHS_FOR_CONTRATRADE", objModelCodeConduct.RESTRICTED_MONTHS_FOR_CONTRATRADE);
                parameters[4] = new SqlParameter("@AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE", objModelCodeConduct.AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE);
                parameters[5] = new SqlParameter("@AMOUNTLIMIT_FOR_PRE_CLEARANCE", objModelCodeConduct.AMOUNTLIMIT_FOR_PRE_CLEARANCE);
                parameters[6] = new SqlParameter("@SHARELIMIT_FOR_PRE_CLEARANCE", objModelCodeConduct.SHARELIMIT_FOR_PRE_CLEARANCE);
                parameters[7] = new SqlParameter("@VALIDITY_OF_PRE_CLEARANCE_APPROVAL", objModelCodeConduct.VALIDITY_OF_PRE_CLEARANCE_APPROVAL);
                parameters[8] = new SqlParameter("@COMPANY_ID", objModelCodeConduct.COMPANY_ID);
                parameters[9] = new SqlParameter("@Mode", "CHECK");
                parameters[10] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[11] = new SqlParameter("@EMPLOYEE_ID", objModelCodeConduct.CREATE_BY);
                parameters[12] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[12].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);
                var obj = parameters[12].Value;
                ModelCodeConductResponse oModelCodeConduct = new ModelCodeConductResponse();
                if ((Int32)obj == 0)
                {
                    parameters[9] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);

                    parameters[9] = new SqlParameter("@Mode", "GET_Model_Code_Of_Conduct_ID_BY_Model_Code_Of_Conduct_DETAIL");
                    var MODEL_ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);
                    objModelCodeConduct.MODEL_ID = (Int32)MODEL_ID;

                    oModelCodeConduct.StatusFl = true;
                    oModelCodeConduct.Msg = "Data has been saved successfully !";
                    oModelCodeConduct.ModelCodeConduct = objModelCodeConduct;

                }
                else
                {
                    oModelCodeConduct.StatusFl = false;
                    oModelCodeConduct.Msg = "Duplicate Entry !";
                }
                return oModelCodeConduct;
            }
            catch (Exception ex)
            {
                ModelCodeConductResponse oModelCodeConduct = new ModelCodeConductResponse();
                oModelCodeConduct.StatusFl = false;
                oModelCodeConduct.Msg = "Processing failed, because of system error !";
                return oModelCodeConduct;
            }
        }

        public ModelCodeConductResponse UpdateModelCodeConduct(ModelCodeConduct objModelCodeConduct)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[13];
                parameters[0] = new SqlParameter("@MODEL_ID", objModelCodeConduct.MODEL_ID);
                parameters[1] = new SqlParameter("@FREQUENCY_OF_PERIOD", objModelCodeConduct.FREQUENCY_OF_PERIOD);
                parameters[2] = new SqlParameter("@CUT_OFF_DATES_FOR_PERIOD", objModelCodeConduct.CUT_OFF_DATES_FOR_PERIOD);
                parameters[3] = new SqlParameter("@RESTRICTED_MONTHS_FOR_CONTRATRADE", objModelCodeConduct.RESTRICTED_MONTHS_FOR_CONTRATRADE);
                parameters[4] = new SqlParameter("@AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE", objModelCodeConduct.AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE);
                parameters[5] = new SqlParameter("@AMOUNTLIMIT_FOR_PRE_CLEARANCE", objModelCodeConduct.AMOUNTLIMIT_FOR_PRE_CLEARANCE);
                parameters[6] = new SqlParameter("@SHARELIMIT_FOR_PRE_CLEARANCE", objModelCodeConduct.SHARELIMIT_FOR_PRE_CLEARANCE);
                parameters[7] = new SqlParameter("@VALIDITY_OF_PRE_CLEARANCE_APPROVAL", objModelCodeConduct.VALIDITY_OF_PRE_CLEARANCE_APPROVAL);
                parameters[8] = new SqlParameter("@COMPANY_ID", objModelCodeConduct.COMPANY_ID);
                parameters[9] = new SqlParameter("@Mode", "CHECK");
                parameters[10] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[11] = new SqlParameter("@EMPLOYEE_ID", objModelCodeConduct.CREATE_BY);
                parameters[12] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[12].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);
                var obj = parameters[12].Value;
                ModelCodeConductResponse oModelCodeConduct = new ModelCodeConductResponse();
                if ((Int32)obj == 0)
                {
                    parameters[9] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);

                    parameters[9] = new SqlParameter("@Mode", "GET_Model_Code_Of_Conduct_ID_BY_Model_Code_Of_Conduct_DETAIL");
                    var MODEL_ID = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);
                    objModelCodeConduct.MODEL_ID = (Int32)MODEL_ID;

                    oModelCodeConduct.StatusFl = true;
                    oModelCodeConduct.Msg = "Data has been updated successfully !";
                    oModelCodeConduct.ModelCodeConduct = objModelCodeConduct;
                }
                else
                {
                    oModelCodeConduct.StatusFl = false;
                    oModelCodeConduct.Msg = "Duplicate Entry !";
                }
                return oModelCodeConduct;
            }
            catch (Exception ex)
            {
                ModelCodeConductResponse oModelCodeConduct = new ModelCodeConductResponse();
                oModelCodeConduct.StatusFl = false;
                oModelCodeConduct.Msg = "Processing failed, because of system error !";
                return oModelCodeConduct;
            }
        }

        public ModelCodeConductResponse GetModelCodeConductList(ModelCodeConduct objModelCodeConduct)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_Model_Code_Of_Conduct_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objModelCodeConduct.COMPANY_ID);

                ModelCodeConductResponse oModelCodeConduct = new ModelCodeConductResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_MODEL_CODE_OF_CONDUCT", objModelCodeConduct.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ModelCodeConduct obj = new ModelCodeConduct();
                        obj.MODEL_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.FREQUENCY_OF_PERIOD = Convert.ToString(rdr.GetValue(1));
                        obj.CUT_OFF_DATES_FOR_PERIOD = Convert.ToString(rdr.GetValue(2));
                        obj.RESTRICTED_MONTHS_FOR_CONTRATRADE = Convert.ToInt32(rdr.GetValue(3));
                        obj.AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE = Convert.ToInt32(rdr.GetValue(4));
                        obj.AMOUNTLIMIT_FOR_PRE_CLEARANCE = Convert.ToInt32(rdr.GetValue(5));
                        obj.SHARELIMIT_FOR_PRE_CLEARANCE = Convert.ToInt32(rdr.GetValue(6));
                        obj.VALIDITY_OF_PRE_CLEARANCE_APPROVAL = Convert.ToInt32(rdr.GetValue(7));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(8));
                        obj.CREATE_BY = Convert.ToInt32(rdr.GetValue(9));
                        obj.CREATED_ON = Convert.ToString(rdr.GetValue(10));
                        oModelCodeConduct.AddObject(obj);
                    }
                    oModelCodeConduct.StatusFl = true;
                    oModelCodeConduct.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oModelCodeConduct.StatusFl = false;
                    oModelCodeConduct.Msg = "No data found !";
                }
                rdr.Close();
                return oModelCodeConduct;
            }
            catch (Exception ex)
            {
                ModelCodeConductResponse oModelCodeConduct = new ModelCodeConductResponse();
                oModelCodeConduct.StatusFl = false;
                oModelCodeConduct.Msg = "Processing failed, because of system error !";
                return oModelCodeConduct;
            }
        }
    }
}