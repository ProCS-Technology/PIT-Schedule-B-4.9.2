using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class DesignationRepository : IRequiresSessionState
    {
        public DesignationResponse AddDesignation(Designation objDesignation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@DESIGNATION_ID", objDesignation.DESIGNATION_ID);
                parameters[1] = new SqlParameter("@DESIGNATION_NM", objDesignation.DESIGNATION_NM);
                parameters[2] = new SqlParameter("@COMPANY_ID", objDesignation.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objDesignation.CREATE_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                DesignationResponse oDesignation = new DesignationResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);

                    parameters[3] = new SqlParameter("@Mode", "GET_DESIGNATION_ID_BY_DESIGNATION_NAME");
                    var DesignationId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);
                    objDesignation.DESIGNATION_ID = (Int32)DesignationId;

                    oDesignation.StatusFl = true;
                    oDesignation.Msg = "Data has been saved successfully !";
                    oDesignation.Designation = objDesignation;

                }
                else
                {
                    oDesignation.StatusFl = false;
                    oDesignation.Msg = "Designation Name aleady exists !";
                }
                return oDesignation;
            }
            catch (Exception ex)
            {
                DesignationResponse oDesignation = new DesignationResponse();
                oDesignation.StatusFl = false;
                oDesignation.Msg = "Processing failed, because of system error !";
                return oDesignation;
            }
        }

        public DesignationResponse UpdateDesignation(Designation objDesignation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@DESIGNATION_ID", objDesignation.DESIGNATION_ID);
                parameters[1] = new SqlParameter("@DESIGNATION_NM", objDesignation.DESIGNATION_NM);
                parameters[2] = new SqlParameter("@COMPANY_ID", objDesignation.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objDesignation.CREATE_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                DesignationResponse oDesignation = new DesignationResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);

                    parameters[3] = new SqlParameter("@Mode", "GET_DESIGNATION_ID_BY_DESIGNATION_NAME");
                    var DesignationId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);
                    objDesignation.DESIGNATION_ID = (Int32)DesignationId;

                    oDesignation.StatusFl = true;
                    oDesignation.Msg = "Data has been updated successfully !";
                    oDesignation.Designation = objDesignation;
                }
                else
                {
                    oDesignation.StatusFl = false;
                    oDesignation.Msg = "Designation Name aleady exists !";
                }
                return oDesignation;
            }
            catch (Exception ex)
            {
                DesignationResponse oDesignation = new DesignationResponse();
                oDesignation.StatusFl = false;
                oDesignation.Msg = "Processing failed, because of system error !";
                return oDesignation;
            }
        }

        public DesignationResponse DeleteDesignation(Designation objDesignation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@DESIGNATION_ID", objDesignation.DESIGNATION_ID);
                parameters[1] = new SqlParameter("@COMPANY_ID", objDesignation.COMPANY_ID);
                parameters[2] = new SqlParameter("@Mode", "Delete_DESIGNATION");
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                DesignationResponse oDesignation = new DesignationResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);
                oDesignation.StatusFl = true;
                oDesignation.Msg = "Data has been deleted successfully !";
                oDesignation.Designation = objDesignation;
                return oDesignation;
            }
            catch (Exception ex)
            {
                DesignationResponse oGrp = new DesignationResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

        public DesignationResponse GetDesignationList(Designation objDesignation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_Designation_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objDesignation.COMPANY_ID);

                DesignationResponse oDesignation = new DesignationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DESIGNATION", objDesignation.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Designation obj = new Designation();
                        obj.DESIGNATION_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.DESIGNATION_NM = Convert.ToString(rdr.GetValue(1));
                        obj.CREATE_BY = Convert.ToString(rdr.GetValue(2));
                        obj.CREATED_ON = Convert.ToString(rdr.GetValue(3));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(4));
                        oDesignation.AddObject(obj);
                    }
                    oDesignation.StatusFl = true;
                    oDesignation.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oDesignation.StatusFl = false;
                    oDesignation.Msg = "No data found !";
                }
                rdr.Close();
                return oDesignation;
            }
            catch (Exception ex)
            {
                DesignationResponse oDesignation = new DesignationResponse();
                oDesignation.StatusFl = false;
                oDesignation.Msg = "Processing failed, because of system error !";
                return oDesignation;
            }
        }
    }
}