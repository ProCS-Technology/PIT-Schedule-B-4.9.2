using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class RestrictedCompaniesRepository : IRequiresSessionState
    {
        private RestrictedCompaniesResponse resCompaniesResponse;

        public RestrictedCompaniesResponse AddRestrictedCompanies(RestrictedCompanies objRestrictedCompanies)
        {
            resCompaniesResponse = new RestrictedCompaniesResponse();

            try
            {
                SqlParameter[] parameters = new SqlParameter[14];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRestrictedCompanies.companyID);
                parameters[1] = new SqlParameter("@COMPANY_NAME", objRestrictedCompanies.companyName);
                parameters[2] = new SqlParameter("@COMPANY_ABRR", objRestrictedCompanies.companyABRR);
                parameters[3] = new SqlParameter("@IS_RESTRICTED", objRestrictedCompanies.isRestricted);
                parameters[4] = new SqlParameter("@FOR_PERPETUITY", objRestrictedCompanies.forPerpetuity);
                parameters[5] = new SqlParameter("@STOCK_TRADE_LIMIT", objRestrictedCompanies.stockTradeLimit);
                parameters[6] = new SqlParameter("@PERIOD_OF_RESTRICTION_FROM", objRestrictedCompanies.periodOfRestrictionFrom);
                parameters[7] = new SqlParameter("@PERIOD_OF_RESTRICTED_TO", objRestrictedCompanies.periodOfRestrictionTo);
                parameters[8] = new SqlParameter("@EMPLOYEE_ID", objRestrictedCompanies.createdBy);
                parameters[9] = new SqlParameter("@Mode", "CHECK");
                parameters[10] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[11] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[12] = new SqlParameter("@ID", SqlDbType.Int);
                parameters[11].Direction = ParameterDirection.Output;
                parameters[13] = new SqlParameter("@IS_HOME_COMPANY", objRestrictedCompanies.IsHomeCompany);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                var obj = parameters[11].Value;


                if ((Int32)obj == 0)
                {
                    parameters[9] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                    parameters[9] = new SqlParameter("@Mode", "GET_RESTRICTEDCOMPANIES_ID_BY_NAME");
                    var resCompanyId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                    objRestrictedCompanies.ID = (Int32)resCompanyId;

                    resCompaniesResponse.StatusFl = true;
                    resCompaniesResponse.Msg = "Data has been saved successfully !";
                    resCompaniesResponse.restrictedCompanies = objRestrictedCompanies;
                }
                else
                {
                    resCompaniesResponse.StatusFl = false;
                    resCompaniesResponse.Msg = "Department Name aleady exists !";
                }
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();
                resCompaniesResponse.StatusFl = false;
                resCompaniesResponse.Msg = "Processing failed, because of system error !";
                return resCompaniesResponse;
            }
            return resCompaniesResponse;
        }

        public RestrictedCompaniesResponse UpdateRestrictedCompanies(RestrictedCompanies objRestrictedCompanies)
        {
            try
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();

                try
                {
                    SqlParameter[] parameters = new SqlParameter[14];
                    parameters[0] = new SqlParameter("@COMPANY_ID", objRestrictedCompanies.companyID);
                    parameters[1] = new SqlParameter("@COMPANY_NAME", objRestrictedCompanies.companyName);
                    parameters[2] = new SqlParameter("@COMPANY_ABRR", objRestrictedCompanies.companyABRR);
                    parameters[3] = new SqlParameter("@IS_RESTRICTED", objRestrictedCompanies.isRestricted);
                    parameters[4] = new SqlParameter("@FOR_PERPETUITY", objRestrictedCompanies.forPerpetuity);
                    parameters[5] = new SqlParameter("@STOCK_TRADE_LIMIT", objRestrictedCompanies.stockTradeLimit);
                    parameters[6] = new SqlParameter("@PERIOD_OF_RESTRICTION_FROM", objRestrictedCompanies.periodOfRestrictionFrom);
                    parameters[7] = new SqlParameter("@PERIOD_OF_RESTRICTED_TO", objRestrictedCompanies.periodOfRestrictionTo);
                    parameters[8] = new SqlParameter("@EMPLOYEE_ID", objRestrictedCompanies.createdBy);
                    //parameters[9] = new SqlParameter("@Mode", "CHECK");
                    parameters[10] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                    parameters[11] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[12] = new SqlParameter("@ID", objRestrictedCompanies.ID);
                    parameters[11].Direction = ParameterDirection.Output;
                    parameters[13] = new SqlParameter("@IS_HOME_COMPANY", objRestrictedCompanies.IsHomeCompany);


                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", parameters);
                    //var obj = parameters[11].Value;

                    //if ((Int32)obj == 0)
                    //{
                    parameters[9] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                    parameters[9] = new SqlParameter("@Mode", "GET_RESTRICTEDCOMPANIES_ID_BY_NAME");
                    var resCompanyId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                    objRestrictedCompanies.ID = (Int32)resCompanyId;

                    resCompaniesResponse.StatusFl = true;
                    resCompaniesResponse.Msg = "Data has been saved successfully !";
                    resCompaniesResponse.restrictedCompanies = objRestrictedCompanies;
                    //}
                    //else
                    //{
                    //    resCompaniesResponse.StatusFl = false;
                    //    resCompaniesResponse.Msg = "Department Name aleady exists !";
                    //}
                    return resCompaniesResponse;
                }
                catch (Exception ex)
                {
                    resCompaniesResponse = new RestrictedCompaniesResponse();
                    resCompaniesResponse.StatusFl = false;
                    resCompaniesResponse.Msg = "Processing failed, because of system error !";
                    return resCompaniesResponse;
                }
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                RestrictedCompaniesResponse oDept = new RestrictedCompaniesResponse();
                oDept.StatusFl = false;
                oDept.Msg = "Processing failed, because of system error !";
                return oDept;
            }
            return null;
        }

        public RestrictedCompaniesResponse DeleteRestrictedCompanies(RestrictedCompanies objRestrictedCompanies)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@ID", objRestrictedCompanies.ID);
                parameters[1] = new SqlParameter("@Mode", "DELETE_RESTRICTEDCOMPANIES");
                parameters[2] = new SqlParameter("@COMPANY_ID", objRestrictedCompanies.companyID);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RestrictedCompaniesResponse resCompaniesResponse = new RestrictedCompaniesResponse();
                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                resCompaniesResponse.StatusFl = true;
                resCompaniesResponse.Msg = "Data has been deleted successfully !";
                resCompaniesResponse.restrictedCompanies = objRestrictedCompanies;
                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                RestrictedCompaniesResponse oDept = new RestrictedCompaniesResponse();
                oDept.StatusFl = false;
                oDept.Msg = "Processing failed, because of system error !";
                return oDept;
            }
            return null;
        }

        public RestrictedCompaniesResponse GetRestrictedCompaniesList(RestrictedCompanies objRestrictedCompanies)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRestrictedCompanies.companyID);
                parameters[1] = new SqlParameter("@Mode", "GET_RESTRICTEDCOMPANIES_List");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objRestrictedCompanies.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RestrictedCompaniesResponse oGrp = new RestrictedCompaniesResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objRestrictedCompanies = new RestrictedCompanies();
                        objRestrictedCompanies.ID = Convert.ToInt32(rdr.GetValue(0));
                        objRestrictedCompanies.ID = Convert.ToInt32(rdr["ID"]);
                        objRestrictedCompanies.companyID = Convert.ToInt32(rdr["COMPANY_ID"]);
                        objRestrictedCompanies.companyName = (!String.IsNullOrEmpty(Convert.ToString(rdr["COMPANY_NAME"]))) ? Convert.ToString(rdr["COMPANY_NAME"]) : String.Empty;
                        objRestrictedCompanies.companyABRR = (!String.IsNullOrEmpty(Convert.ToString(rdr["COMPANY_ABRR"]))) ? Convert.ToString(rdr["COMPANY_ABRR"]) : String.Empty;
                        objRestrictedCompanies.isRestricted = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_RESTRICTED"]))) ? Convert.ToInt32(rdr["IS_RESTRICTED"]) : 0;
                        objRestrictedCompanies.forPerpetuity = (!String.IsNullOrEmpty(Convert.ToString(rdr["FOR_PERPETUITY"]))) ? Convert.ToInt32(rdr["FOR_PERPETUITY"]) : 0;
                        objRestrictedCompanies.stockTradeLimit = (!String.IsNullOrEmpty(Convert.ToString(rdr["STOCK_TRADE_LIMIT"]))) ? Convert.ToInt32(rdr["STOCK_TRADE_LIMIT"]) : 0;
                        objRestrictedCompanies.periodOfRestrictionFrom = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERIOD_OF_RESTRICTION_FROM"]))) ? Convert.ToDateTime(rdr["PERIOD_OF_RESTRICTION_FROM"]) : DateTime.Now;
                        objRestrictedCompanies.periodOfRestrictionTo = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERIOD_OF_RESTRICTED_TO"]))) ? Convert.ToDateTime(rdr["PERIOD_OF_RESTRICTED_TO"]) : DateTime.Now;
                        objRestrictedCompanies.restrictionFrom = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERIOD_OF_RESTRICTION_FROM"]))) ? FormatHelper.FormatDate(rdr["PERIOD_OF_RESTRICTION_FROM"].ToString()) : String.Empty;
                        objRestrictedCompanies.restrictionTo = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERIOD_OF_RESTRICTED_TO"]))) ? FormatHelper.FormatDate(rdr["PERIOD_OF_RESTRICTED_TO"].ToString()) : String.Empty;
                        objRestrictedCompanies.createdBy = (!String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"]))) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        objRestrictedCompanies.IsHomeCompany = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_HOME_COMPANY"]))) ? Convert.ToInt32(rdr["IS_HOME_COMPANY"]) : 0;

                        oGrp.AddObject(objRestrictedCompanies);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Department Data has been fetched successfully !";
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
                RestrictedCompaniesResponse oGrp = new RestrictedCompaniesResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

        public RestrictedCompaniesResponse UpdateIsRestrictedCompanies(RestrictedCompanies objRestrictedCompanies)
        {

            resCompaniesResponse = new RestrictedCompaniesResponse();

            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRestrictedCompanies.companyID);
                parameters[1] = new SqlParameter("@IS_RESTRICTED", objRestrictedCompanies.isRestricted);
                parameters[2] = new SqlParameter("@strID", objRestrictedCompanies.strID);
                parameters[3] = new SqlParameter("@Mode", "UPDATE_IS_RESTRICTED");
                parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[4].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RESTRICTED_COMPANIES", objRestrictedCompanies.MODULE_DATABASE, parameters);
                resCompaniesResponse.StatusFl = true;
                resCompaniesResponse.Msg = "Data has been saved successfully !";
                resCompaniesResponse.restrictedCompanies = objRestrictedCompanies;

                return resCompaniesResponse;
            }
            catch (Exception ex)
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();
                resCompaniesResponse.StatusFl = false;
                resCompaniesResponse.Msg = "Processing failed, because of system error !";
                return resCompaniesResponse;
            }

        }

    }
}