using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class PolicyRepository : IRequiresSessionState
    {
        public PolicyResponse AddPolicy(Policy objPolicy)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@POLICY_ID", objPolicy.POLICY_ID);
                parameters[1] = new SqlParameter("@DOCUMENT", objPolicy.DOCUMENT);
                parameters[2] = new SqlParameter("@COMPANY_ID", objPolicy.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objPolicy.CREATED_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                PolicyResponse opol = new PolicyResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);
                    parameters[3] = new SqlParameter("@Mode", "GET_POLICY_ID_BY_DOCUMENT");
                    var PolicyId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);
                    objPolicy.POLICY_ID = (Int32)PolicyId;

                    opol.StatusFl = true;
                    opol.Msg = "Data has been saved successfully !";
                    opol.Policy = objPolicy;
                }
                else
                {
                    opol.StatusFl = false;
                    opol.Msg = "Document Name aleady exists !";
                }
                return opol;
            }
            catch (Exception ex)
            {
                PolicyResponse opol = new PolicyResponse();
                opol.StatusFl = false;
                opol.Msg = "Processing failed, because of system error !";
                return opol;
            }
        }
        public PolicyResponse UpdatePolicy(Policy objPolicy)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@POLICY_ID", objPolicy.POLICY_ID);
                parameters[1] = new SqlParameter("@DOCUMENT", objPolicy.DOCUMENT);
                parameters[2] = new SqlParameter("@Mode", "CHECK");
                parameters[3] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[4] = new SqlParameter("@EMPLOYEE_ID", objPolicy.CREATED_BY);
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@COMPANY_ID", objPolicy.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                PolicyResponse oPol = new PolicyResponse();
                if ((Int32)obj == 0)
                {
                    parameters[2] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);

                    parameters[2] = new SqlParameter("@Mode", "GET_POLICY_ID_BY_DOCUMENT");
                    var PolicyId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);
                    objPolicy.POLICY_ID = (Int32)PolicyId;

                    oPol.StatusFl = true;
                    oPol.Msg = "Data has been updated successfully !";
                    oPol.Policy = objPolicy;
                }
                else
                {
                    oPol.StatusFl = false;
                    oPol.Msg = "Document Name already exists !";
                }
                return oPol;
            }
            catch (Exception ex)
            {
                PolicyResponse oPol = new PolicyResponse();
                oPol.StatusFl = false;
                oPol.Msg = "Processing failed, because of system error !";
                return oPol;
            }
        }
        public PolicyResponse DeletePolicy(Policy objPolicy)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@POLICY_ID", objPolicy.POLICY_ID);
                parameters[1] = new SqlParameter("@Mode", "DELETE_POLICY");
                parameters[2] = new SqlParameter("@COMPANY_ID", objPolicy.COMPANY_ID);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                PolicyResponse oPol = new PolicyResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);
                oPol.StatusFl = true;
                oPol.Msg = "Data has been deleted successfully !";
                oPol.Policy = objPolicy;
                return oPol;
            }
            catch (Exception ex)
            {
                PolicyResponse oPol = new PolicyResponse();
                oPol.StatusFl = false;
                oPol.Msg = "Processing failed, because of system error !";
                return oPol;
            }
        }
        public PolicyResponse GetPolicyList(Policy objPolicy)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPolicy.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_POLICY_LIST");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objPolicy.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                PolicyResponse oPol = new PolicyResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Policy obj = new Policy();
                        obj.POLICY_ID = Convert.ToInt32(rdr["POLICY_ID"]);
                        obj.DOCUMENT = !String.IsNullOrEmpty(Convert.ToString(rdr["DOCUMENT"])) ? Convert.ToString(rdr["DOCUMENT"]) : String.Empty;
                        obj.CREATED_BY = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"])) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        obj.CREATED_DATE = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                        oPol.AddObject(obj);
                    }
                    oPol.StatusFl = true;
                    oPol.Msg = "Policy Document Data has been fetched successfully !";
                }
                else
                {
                    oPol.StatusFl = false;
                    oPol.Msg = "No data found !";
                }
                rdr.Close();
                return oPol;
            }
            catch (Exception ex)
            {
                PolicyResponse oPol = new PolicyResponse();
                oPol.StatusFl = false;
                oPol.Msg = "Processing failed, because of system error !";
                return oPol;
            }
        }
        public PolicyResponse GetAllPolicyDocumentsList(Policy objPolicy)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPolicy.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_POLICY_ALL_DOCUMENT_LIST");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objPolicy.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                PolicyResponse oPol = new PolicyResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_POLICY", objPolicy.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Policy obj = new Policy();

                        obj.POLICY_ID = Convert.ToInt32(rdr["ID"]);
                        obj.DOCUMENT = !String.IsNullOrEmpty(Convert.ToString(rdr["DOCUMENT"])) ? Convert.ToString(rdr["DOCUMENT"]) : String.Empty;
                        obj.CREATED_BY = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"])) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        obj.CREATED_DATE = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? rdr["CREATED_ON"].ToString() : string.Empty;
                        oPol.AddObject(obj);
                    }
                    oPol.StatusFl = true;
                    oPol.Msg = "Policy Document Data has been fetched successfully !";
                }
                else
                {
                    oPol.StatusFl = false;
                    oPol.Msg = "No data found !";
                }
                rdr.Close();
                return oPol;
            }
            catch (Exception ex)
            {
                PolicyResponse oPol = new PolicyResponse();
                oPol.StatusFl = false;
                oPol.Msg = "Processing failed, because of system error !";
                return oPol;
            }
        }
    }
}