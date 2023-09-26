using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class BusinessUnitRepository
    {
        #region "Get Business Unit"
        public BusinessUnitResponse GetBusinessUnit(BusinessUnit objBusinessUnit)
        {
            try
            {
                string sMultiBU = Convert.ToString(ConfigurationManager.AppSettings["SupportMultiBU"]);
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBusinessUnit.companyId);
                parameters[1] = new SqlParameter("@MODE", "GET_BUSINESS_UNIT");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                BusinessUnitResponse objResponse = new BusinessUnitResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BUSINESS_UNIT_SET_UP", objBusinessUnit.MODULE_DATABASE, parameters);
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BusinessUnit obj = new BusinessUnit();
                        obj.businessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_ID"])) ? Convert.ToInt32(rdr["BU_ID"]) : 0;
                        obj.businessUnitName = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_NM"])) ? Convert.ToString(rdr["BU_NM"]) : String.Empty;
                        obj.parentBusinessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["PARENT_BU"])) ? Convert.ToInt32(rdr["PARENT_BU"]) : 0;
                        //if (sMultiBU.ToUpper() == "YES")
                        //{
                        //    if (obj.parentBusinessUnitId == objBusinessUnit.businessUnitId)
                        //    {
                        //        objResponse.AddObject(obj);
                        //    }
                        //}
                        //else
                        //{
                            objResponse.AddObject(obj);
                        //}
                    }
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Document Data has been fetched successfully !";
                }
                else
                {
                    objResponse.StatusFl = false;
                    objResponse.Msg = "No data found !";
                }
                rdr.Close();
                return objResponse;
            }
            catch (Exception ex)
            {
                BusinessUnitResponse objResponse = new BusinessUnitResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion

        #region "Get All Business Unit"
        public BusinessUnitResponse GetAllBusinessUnit(BusinessUnit objBusinessUnit)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBusinessUnit.companyId);
                parameters[1] = new SqlParameter("@MODE", "GET_BUSINESS_UNIT");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                BusinessUnitResponse objResponse = new BusinessUnitResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BUSINESS_UNIT_SET_UP", objBusinessUnit.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BusinessUnit obj = new BusinessUnit();

                        obj.businessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_ID"])) ? Convert.ToInt32(rdr["BU_ID"]) : 0;
                        obj.businessUnitName = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_NM"])) ? Convert.ToString(rdr["BU_NM"]) : String.Empty;
                        obj.parentBusinessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["PARENT_BU"])) ? Convert.ToInt32(rdr["PARENT_BU"]) : 0;

                        if (obj.businessUnitId == objBusinessUnit.businessUnitId || obj.parentBusinessUnitId == objBusinessUnit.businessUnitId)
                        {
                            objResponse.AddObject(obj);
                        }
                    }
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Document Data has been fetched successfully !";
                }
                else
                {
                    objResponse.StatusFl = false;
                    objResponse.Msg = "No data found !";
                }
                rdr.Close();
                return objResponse;
            }
            catch (Exception ex)
            {
                BusinessUnitResponse objResponse = new BusinessUnitResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion
    }
}