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
    public class ReportsRepository
    {
        #region "Get Declaration Report"
        public ReportsResponse GetDeclarationReports(DeclarationReport objDeclarationReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@COMPANY_ID", objDeclarationReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_DECLARATION_REPORTS");
                parameters[3] = new SqlParameter("@ADMIN_DB", objDeclarationReport.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objDeclarationReport.businessUnit.businessUnitId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objDeclarationReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    objDeclarationReport.declarationMade = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DECLARATION_MADE"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DECLARATION_MADE"]) : 0;
                    objDeclarationReport.declarationNotMade = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DECLARATION_NOT_MADE"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DECLARATION_NOT_MADE"]) : 0;
                    objDeclarationReport.totalUsers = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_USERS"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_USERS"]) : 0;
                    objDeclarationReport.lstUser = GetUsersNotMadeTheirDeclaration(objDeclarationReport);
                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.DeclarationReport = objDeclarationReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Users Made Declaration Reports"
        public ReportsResponse GetUsersMadeDeclarationReports(DeclarationReport objDeclarationReport)
        {
            try
            {
                List<User> lstUser = new List<User>();

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@COMPANY_ID", objDeclarationReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_USERS_MADE_DECLARATION_REPORTS");
                parameters[3] = new SqlParameter("@ADMIN_DB", objDeclarationReport.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objDeclarationReport.businessUnit.businessUnitId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objDeclarationReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        User objUser = new User();
                        objUser.USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                        objUser.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                        objUser.LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(dr["LOGIN_ID"])) ? Convert.ToString(dr["LOGIN_ID"]) : String.Empty;
                        objUser.DepartmentName = !String.IsNullOrEmpty(Convert.ToString(dr["DEPARTMENT_NAME"])) ? Convert.ToString(dr["DEPARTMENT_NAME"]) : String.Empty;
                        objUser.DesignationName = !String.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION_NAME"])) ? Convert.ToString(dr["DESIGNATION_NAME"]) : String.Empty;
                        objUser.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                        objUser.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(dr["USER_MOBILE"])) ? Convert.ToString(dr["USER_MOBILE"]) : String.Empty;

                        lstUser.Add(objUser);
                    }
                }
                objDeclarationReport.lstUser = lstUser;
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.DeclarationReport = objDeclarationReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Users Not Made Declaration Reports"
        public ReportsResponse GetUsersNotMadeDeclarationReports(DeclarationReport objDeclarationReport)
        {
            try
            {
                List<User> lstUser = new List<User>();

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@COMPANY_ID", objDeclarationReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_USERS_NOT_MADE_DECLARATION_REPORTS");
                parameters[3] = new SqlParameter("@ADMIN_DB", objDeclarationReport.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objDeclarationReport.businessUnit.businessUnitId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objDeclarationReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        User objUser = new User();
                        objUser.USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                        objUser.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                        objUser.LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(dr["LOGIN_ID"])) ? Convert.ToString(dr["LOGIN_ID"]) : String.Empty;
                        objUser.DepartmentName = !String.IsNullOrEmpty(Convert.ToString(dr["DEPARTMENT_NAME"])) ? Convert.ToString(dr["DEPARTMENT_NAME"]) : String.Empty;
                        objUser.DesignationName = !String.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION_NAME"])) ? Convert.ToString(dr["DESIGNATION_NAME"]) : String.Empty;
                        objUser.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                        objUser.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(dr["USER_MOBILE"])) ? Convert.ToString(dr["USER_MOBILE"]) : String.Empty;

                        lstUser.Add(objUser);
                    }
                }
                objDeclarationReport.lstUser = lstUser;
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.DeclarationReport = objDeclarationReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Users Not Made Their Declaration"
        private List<User> GetUsersNotMadeTheirDeclaration(DeclarationReport objDeclarationReport)
        {
            List<User> lstUser = new List<User>();

            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@COMPANY_ID", objDeclarationReport.companyId);
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@MODE", "GET_DECLARATION_REPORTS");
            parameters[3] = new SqlParameter("@ADMIN_DB", objDeclarationReport.ADMIN_DATABASE);
            parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objDeclarationReport.businessUnit.businessUnitId);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objDeclarationReport.MODULE_DATABASE, parameters);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    User objUser = new User();
                    objUser.USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                    objUser.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                    objUser.LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(dr["LOGIN_ID"])) ? Convert.ToString(dr["LOGIN_ID"]) : String.Empty;
                    objUser.userRole = new Role
                    {
                        ROLE_NM = !String.IsNullOrEmpty(Convert.ToString(dr["ROLE_NAME"])) ? Convert.ToString(dr["ROLE_NAME"]) : String.Empty
                    };

                    objUser.declarationStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["DECLARATION_START_DATE"])) ? Convert.ToString(dr["DECLARATION_START_DATE"]) : String.Empty;
                    objUser.declarationSubmissionDate = !String.IsNullOrEmpty(Convert.ToString(dr["DECLARATION_SUBMISSION_DATE"])) ? Convert.ToString(dr["DECLARATION_SUBMISSION_DATE"]) : String.Empty;
                    objUser.declarationDueDate = !String.IsNullOrEmpty(Convert.ToString(dr["DUE_DATE"])) ? Convert.ToString(dr["DUE_DATE"]) : String.Empty;
                    objUser.DepartmentName = !String.IsNullOrEmpty(Convert.ToString(dr["DEPARTMENT_NAME"])) ? Convert.ToString(dr["DEPARTMENT_NAME"]) : String.Empty;
                    objUser.DesignationName = !String.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION_NAME"])) ? Convert.ToString(dr["DESIGNATION_NAME"]) : String.Empty;
                    objUser.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                    objUser.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(dr["USER_MOBILE"])) ? Convert.ToString(dr["USER_MOBILE"]) : String.Empty;

                    lstUser.Add(objUser);
                }
            }

            return lstUser;
        }
        #endregion

        #region "Get Declaration Report Between Dates"
        public ReportsResponse GetDeclarationReportsBetweenDates(DeclarationReport objDeclarationReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@COMPANY_ID", objDeclarationReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_DECLARATION_REPORTS_BETWEEN_DATES");
                parameters[3] = new SqlParameter("@DECLARATION_NOT_MADE_FROM", ConvertDate(objDeclarationReport.declarationNotMadeFrom));
                parameters[4] = new SqlParameter("@DECLARATION_NOT_MADE_TO", ConvertDate(objDeclarationReport.declarationNotMadeTo));
                parameters[5] = new SqlParameter("@ADMIN_DB", objDeclarationReport.ADMIN_DATABASE);
                parameters[6] = new SqlParameter("@BUSINESS_UNIT_ID", objDeclarationReport.businessUnit.businessUnitId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objDeclarationReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    List<User> lstUser = new List<User>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        User objUser = new User();
                        objUser.USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                        objUser.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                        objUser.LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(dr["LOGIN_ID"])) ? Convert.ToString(dr["LOGIN_ID"]) : String.Empty;
                        objUser.userRole = new Role
                        {
                            ROLE_NM = !String.IsNullOrEmpty(Convert.ToString(dr["ROLE_NAME"])) ? Convert.ToString(dr["ROLE_NAME"]) : String.Empty
                        };
                        objUser.declarationStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["DECLARATION_START_DATE"])) ? Convert.ToString(dr["DECLARATION_START_DATE"]) : String.Empty;
                        objUser.declarationSubmissionDate = !String.IsNullOrEmpty(Convert.ToString(dr["DECLARATION_SUBMISSION_DATE"])) ? Convert.ToString(dr["DECLARATION_SUBMISSION_DATE"]) : String.Empty;
                        objUser.declarationDueDate = !String.IsNullOrEmpty(Convert.ToString(dr["DUE_DATE"])) ? Convert.ToString(dr["DUE_DATE"]) : String.Empty;
                        objUser.DepartmentName = !String.IsNullOrEmpty(Convert.ToString(dr["DEPARTMENT_NAME"])) ? Convert.ToString(dr["DEPARTMENT_NAME"]) : String.Empty;
                        objUser.DesignationName = !String.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION_NAME"])) ? Convert.ToString(dr["DESIGNATION_NAME"]) : String.Empty;
                        objUser.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                        objUser.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(dr["USER_MOBILE"])) ? Convert.ToString(dr["USER_MOBILE"]) : String.Empty;
                        lstUser.Add(objUser);
                    }
                    objDeclarationReport.lstUser = lstUser;
                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.DeclarationReport = objDeclarationReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Trading Report Between Dates"
        public ReportsResponse GetTradingReportBetweenDates(TradingReport objTradingReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objTradingReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_TRADING_REPORT_BETWEEN_DATES");
                parameters[3] = new SqlParameter("@TRADE_FROM", FormatHelper.FormatDate(objTradingReport.tradingFrom));
                parameters[4] = new SqlParameter("@TRADE_TO", FormatHelper.FormatDate(objTradingReport.tradingTo));
                parameters[5] = new SqlParameter("@USER_ID", objTradingReport.userId);
                List<TradingReport> lstTradingReport = new List<TradingReport>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objTradingReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow dr1 in ds.Tables[1].Rows)
                    {
                        TradingReport tradingReport = new TradingReport();
                        tradingReport.pan = !String.IsNullOrEmpty(Convert.ToString(dr1["PAN"])) ? Convert.ToString(dr1["PAN"]) : String.Empty;
                        tradingReport.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr1["FOLIO"])) ? Convert.ToString(dr1["FOLIO"]) : String.Empty;
                        tradingReport.totaltradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr1["TOTAL_TRADE_QUANTITY"])) ? Convert.ToString(dr1["TOTAL_TRADE_QUANTITY"]) : "0";
                        tradingReport.totaltradeValue = !String.IsNullOrEmpty(Convert.ToString(dr1["TOTAL_TRADE_VALUE"])) ? Convert.ToString(dr1["TOTAL_TRADE_VALUE"]) : "0";
                        Int32 bpId = !String.IsNullOrEmpty(Convert.ToString(dr1["BP_ID"])) ? Convert.ToInt32(dr1["BP_ID"]) : 0;
                        tradingReport.UserLogin = Convert.ToString(dr1["USER_LOGIN"]);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string pan = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                            string folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO_NO"])) ? Convert.ToString(dr["FOLIO_NO"]) : String.Empty;
                            string sUsrLogin = !String.IsNullOrEmpty(Convert.ToString(dr["USER_LOGIN"])) ? Convert.ToString(dr["USER_LOGIN"]) : String.Empty;
                            Int32 newBpId = !String.IsNullOrEmpty(Convert.ToString(dr["BP_ID"])) ? Convert.ToInt32(dr["BP_ID"]) : 0;
                            if (tradingReport.pan == pan && tradingReport.folioNumber == folioNumber && bpId == newBpId && tradingReport.UserLogin==sUsrLogin)
                            {
                                tradingReport.name = !String.IsNullOrEmpty(Convert.ToString(dr["NAME"])) ? Convert.ToString(dr["NAME"]) : String.Empty;
                                tradingReport.relativeName = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_NM"])) ? Convert.ToString(dr["RELATIVE_NM"]) : String.Empty;
                                tradingReport.relation = !String.IsNullOrEmpty(Convert.ToString(dr["RELATION_NM"])) ? Convert.ToString(dr["RELATION_NM"]) : "Self";

                                tradingReport.holdingAsOnDate = !String.IsNullOrEmpty(Convert.ToString(dr["HOLDING_AS_ON_DATE"])) ? Convert.ToString(dr["HOLDING_AS_ON_DATE"]) : "0";
                                tradingReport.fileUploadedDate = !String.IsNullOrEmpty(Convert.ToString(dr["FILE_UPLOADED_DATE"])) ? Convert.ToString(dr["FILE_UPLOADED_DATE"]) : String.Empty;

                                tradingReport.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE"])) ? Convert.ToString(dr["VALUE"]) : String.Empty;

                                if (tradingReport.transactionSubType == "Buy" || tradingReport.transactionSubType == "ESOP/RSU/Scheme" || tradingReport.transactionSubType == "Physical Share")
                                {
                                    tradingReport.tradeType = "Buy";
                                }
                                else if (tradingReport.transactionSubType == "Sell" || tradingReport.transactionSubType == "Buy Back/ESOP/Scheme")
                                {
                                    tradingReport.tradeType = "Sell";
                                }

                                if (tradingReport.transactionSubType == "Buy" || tradingReport.transactionSubType == "Sell")
                                {
                                    tradingReport.equityQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : String.Empty;
                                    tradingReport.equityValue = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_VALUE"])) ? Convert.ToString(dr["TRADE_VALUE"]) : String.Empty;
                                    tradingReport.equityTradeDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_DATE"])) ? Convert.ToString(dr["TRADE_DATE"]) : String.Empty;
                                }
                                else if (tradingReport.transactionSubType == "ESOP/RSU/Scheme" || tradingReport.transactionSubType == "Buy Back/ESOP/Scheme")
                                {
                                    tradingReport.esopQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : String.Empty;
                                    tradingReport.esopValue = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_VALUE"])) ? Convert.ToString(dr["TRADE_VALUE"]) : String.Empty;
                                    tradingReport.esopTradeDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_DATE"])) ? Convert.ToString(dr["TRADE_DATE"]) : String.Empty;
                                }
                                else if (tradingReport.transactionSubType == "Physical Share")
                                {

                                    tradingReport.physicalShareQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : String.Empty;
                                    tradingReport.physicalShareValue = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_VALUE"])) ? Convert.ToString(dr["TRADE_VALUE"]) : String.Empty;
                                    tradingReport.physicalShareTradeDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_DATE"])) ? Convert.ToString(dr["TRADE_DATE"]) : String.Empty;
                                }
                                //if (Convert.ToString(dr["VALUE"]) == "Buy")
                                //{
                                //    tradingReport.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? "+" + Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                                //}
                                //else if (Convert.ToString(dr["VALUE"]) == "Sell")
                                //{
                                //    tradingReport.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? "-" + Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                                //}
                                //else
                                //{
                                //    tradingReport.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                                //}
                                tradingReport.method = !String.IsNullOrEmpty(Convert.ToString(dr["METHOD"])) ? Convert.ToString(dr["METHOD"]) : String.Empty;
                                tradingReport.isNonCompliant = !String.IsNullOrEmpty(Convert.ToString(dr["IS_NON_COMPLIANT"])) ? Convert.ToString(dr["IS_NON_COMPLIANT"]) : String.Empty;
                                tradingReport.nonCompliantReason = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TYPE"])) ? Convert.ToString(dr["NON_COMPLIANCE_TYPE"]) : String.Empty;
                                tradingReport.cORemarks = !String.IsNullOrEmpty(Convert.ToString(dr["CO_REMARKS"])) ? Convert.ToString(dr["CO_REMARKS"]) : String.Empty;
                                tradingReport.nonComplianceTaskStatus = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TASK_STATUS"])) ? Convert.ToString(dr["NON_COMPLIANCE_TASK_STATUS"]) : String.Empty;
                                tradingReport.nonComplianceTaskId = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TASK_ID"])) ? Convert.ToInt32(dr["NON_COMPLIANCE_TASK_ID"]) : 0;

                            }
                        }

                        if (String.IsNullOrEmpty(tradingReport.esopQuantity))
                        {
                            tradingReport.esopQuantity = "0";
                        }
                        if (String.IsNullOrEmpty(tradingReport.esopValue))
                        {
                            tradingReport.esopValue = "0";
                        }
                        if (String.IsNullOrEmpty(tradingReport.esopTradeDate))
                        {
                            tradingReport.esopTradeDate = String.Empty;
                        }

                        if (String.IsNullOrEmpty(tradingReport.physicalShareQuantity))
                        {
                            tradingReport.physicalShareQuantity = "0";
                        }
                        if (String.IsNullOrEmpty(tradingReport.physicalShareValue))
                        {
                            tradingReport.physicalShareValue = "0";
                        }
                        if (String.IsNullOrEmpty(tradingReport.physicalShareTradeDate))
                        {
                            tradingReport.physicalShareTradeDate = String.Empty;
                        }

                        lstTradingReport.Add(tradingReport);
                    }

                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstTradingReport = lstTradingReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        public ReportsResponse GetEsopReportBetweenDates(TradingReport objTradingReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objTradingReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_ESOP_REPORT_BETWEEN_DATES");
                parameters[3] = new SqlParameter("@TRADE_FROM", ConvertDate(objTradingReport.tradingFrom));
                parameters[4] = new SqlParameter("@TRADE_TO", ConvertDate(objTradingReport.tradingTo));
                parameters[5] = new SqlParameter("@USER_ID", objTradingReport.userId);
                List<TradingReport> lstTradingReport = new List<TradingReport>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objTradingReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        TradingReport tradingReport = new TradingReport();
                        tradingReport.name = !String.IsNullOrEmpty(Convert.ToString(dr1["SHAREHOLDER_NAME"])) ? Convert.ToString(dr1["SHAREHOLDER_NAME"]) : String.Empty;

                        tradingReport.pan = !String.IsNullOrEmpty(Convert.ToString(dr1["PAN_NUMBER"])) ? Convert.ToString(dr1["PAN_NUMBER"]) : String.Empty;
                        tradingReport.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr1["FOLIO_NO"])) ? Convert.ToString(dr1["FOLIO_NO"]) : String.Empty;
                        tradingReport.esopQuantity = !String.IsNullOrEmpty(Convert.ToString(dr1["QTY"])) ? Convert.ToString(dr1["QTY"]) : "0";
                        tradingReport.esopValue = !String.IsNullOrEmpty(Convert.ToString(dr1["RATE"])) ? string.Format("{0:#.00}", dr1["RATE"]) : String.Empty;
                        tradingReport.totaltradeValue = !String.IsNullOrEmpty(Convert.ToString(dr1["AMOUNT"])) ? string.Format("{0:#.00}", dr1["AMOUNT"]) : "0";
                        Int32 bpId = !String.IsNullOrEmpty(Convert.ToString(dr1["HDR_ID"])) ? Convert.ToInt32(dr1["HDR_ID"]) : 0;
                        tradingReport.esopTradeDate = !String.IsNullOrEmpty(Convert.ToString(dr1["DATE"])) ? Convert.ToString(dr1["DATE"]) : String.Empty;
                        tradingReport.EsopFilePath = !String.IsNullOrEmpty(Convert.ToString(dr1["FILE_PATH"])) ? Convert.ToString(dr1["FILE_PATH"]) : String.Empty;
                        tradingReport.IsEsopFormSubmitted = !String.IsNullOrEmpty(Convert.ToString(dr1["IS_FORM_SUBMITED"])) ? Convert.ToString(dr1["IS_FORM_SUBMITED"]) : String.Empty;

                        lstTradingReport.Add(tradingReport);
                    }

                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstTradingReport = lstTradingReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #region "Close Non Compliance Task"
        public ReportsResponse CloseNonComplianceTask(TradingReport objTradingReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objTradingReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "CLOSE_NON_COMPLIANCE_TASK");
                parameters[3] = new SqlParameter("@NON_COMPLIANCE_TASK_ID", objTradingReport.nonComplianceTaskId);
                parameters[4] = new SqlParameter("@NON_COMPLIANCE_TASK_STATUS", objTradingReport.nonComplianceTaskStatus);
                parameters[5] = new SqlParameter("@CO_REMARKS", objTradingReport.cORemarks);

                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objTradingReport.MODULE_DATABASE, parameters);

                ReportsResponse objReportsResponse = new ReportsResponse();

                if (Convert.ToInt32(parameters[1].Value) == 1)
                {
                    objReportsResponse.StatusFl = true;
                }
                else
                {
                    objReportsResponse.StatusFl = false;
                }

                objReportsResponse.Msg = "Data has been updated successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Benpos Uploaded List"
        public ReportsResponse GetBenposUploadedList(BenposHeader objBenposHeader)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBenposHeader.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_BENPOS_HEADER_LIST");
                List<BenposHeader> lstBenposHeader = new List<BenposHeader>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objBenposHeader.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BenposHeader benposHeader = new BenposHeader();
                        benposHeader.asOfDate = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        lstBenposHeader.Add(benposHeader);
                    }
                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstBenposHeader = lstBenposHeader;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Benpos Report"
        public ReportsResponse GetBenposReport(BenposHeader objBenposHeader)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objBenposHeader.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_BENPOS_REPORT");
                parameters[3] = new SqlParameter("@BENPOS_UPLOADED_DATE", objBenposHeader.asOfDate);
                List<BenposReport> lstBenposReport = new List<BenposReport>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objBenposHeader.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BenposReport benposReport = new BenposReport();
                        benposReport.loginId = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.name = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.pan = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.relative = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.relation = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.initialHoldingAfterDeclaration = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.holdingAsOnDate = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.tradeDate = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.isNonCompliant = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.nonCompliantType = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                        benposReport.benposDetailReport = GetBenposDetailReport(benposReport.pan, benposReport.tradeDate, objBenposHeader.companyId, objBenposHeader.MODULE_DATABASE);
                        lstBenposReport.Add(benposReport);
                    }
                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstBenposReport = lstBenposReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get Benpos Detail Report"
        private List<BenposDetailReport> GetBenposDetailReport(string pan, string tradeDate, Int32 companyId, string moduleDatabase)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@MODE", "GET_BENPOS_DETL_REPORT");
            parameters[3] = new SqlParameter("@BENPOS_UPLOADED_DATE", tradeDate);
            parameters[4] = new SqlParameter("@PAN", pan);
            List<BenposDetailReport> lstBenposDetailReport = new List<BenposDetailReport>();
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", moduleDatabase, parameters);
            if (ds.Tables.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BenposDetailReport benposDetailReport = new BenposDetailReport();
                    benposDetailReport.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                    benposDetailReport.holdingAsOnDate = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                    benposDetailReport.holdingAfterDeclaration = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                    benposDetailReport.transactionType = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                    benposDetailReport.tradedQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["AS_OF_DATE"])) ? Convert.ToString(dr["AS_OF_DATE"]) : String.Empty;
                    lstBenposDetailReport.Add(benposDetailReport);
                }
            }
            return lstBenposDetailReport;
        }
        #endregion

        #region "Get UPSI Report Between Dates"
        public ReportsResponse GetUPSIReportBetweenDates(UPSICommunication objUPSIReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_UPSI_REPORT_BETWEEN_DATES");
                parameters[3] = new SqlParameter("@UPSI_FROM", ConvertDate(objUPSIReport.upsiFrom));
                parameters[4] = new SqlParameter("@UPSI_TO", ConvertDate(objUPSIReport.upsiTo));
                parameters[5] = new SqlParameter("@UPSI_COMM_FROM", objUPSIReport.upsiCommunicationFrom != "0" ? CryptorEngine.Encrypt(objUPSIReport.upsiCommunicationFrom, true) : "0");

                List<UPSICommunication> lstUPSICommunication = new List<UPSICommunication>();

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objUPSIReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UPSICommunication objUPSICommunication = new UPSICommunication();
                        objUPSICommunication.lineId = !String.IsNullOrEmpty(Convert.ToString(dr["LINE_ID"])) ? Convert.ToInt32(dr["LINE_ID"]) : 0;
                        objUPSICommunication.emailSentOn = !String.IsNullOrEmpty(Convert.ToString(dr["EMAIL_SENT_ON"])) ? Convert.ToString(dr["EMAIL_SENT_ON"]) : String.Empty;
                        objUPSICommunication.hdrId = !String.IsNullOrEmpty(Convert.ToString(dr["HDR_ID"])) ? Convert.ToInt32(dr["HDR_ID"]) : 0;
                        objUPSICommunication.subject = !String.IsNullOrEmpty(Convert.ToString(dr["SUBJECT"])) ? Convert.ToString(dr["SUBJECT"]) : String.Empty;
                        objUPSICommunication.subjectCreatedOn = !String.IsNullOrEmpty(Convert.ToString(dr["SUBJECT_CREATED_ON"])) ? Convert.ToString(dr["SUBJECT_CREATED_ON"]) : String.Empty;
                        objUPSICommunication.subjectCreatedBy = !String.IsNullOrEmpty(Convert.ToString(dr["SUBJECT_CREATED_BY"])) ? Convert.ToString(dr["SUBJECT_CREATED_BY"]) : String.Empty;
                        objUPSICommunication.from = !String.IsNullOrEmpty(Convert.ToString(dr["recFrom"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["recFrom"]), true) : String.Empty;
                        objUPSICommunication.to = !String.IsNullOrEmpty(Convert.ToString(dr["recTo"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["recTo"]), true) : String.Empty;
                        objUPSICommunication.cc = !String.IsNullOrEmpty(Convert.ToString(dr["CC"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["CC"]), true) : String.Empty;
                        objUPSICommunication.bcc = !String.IsNullOrEmpty(Convert.ToString(dr["BCC"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["BCC"]), true) : String.Empty;
                        objUPSICommunication.body = !String.IsNullOrEmpty(Convert.ToString(dr["MESSAGE"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["MESSAGE"]), true) : String.Empty;
                        objUPSICommunication.MODULE_DATABASE = objUPSIReport.MODULE_DATABASE;
                        objUPSICommunication.upsiSubLineAttachments = GetUPSISubLineAttachments(objUPSICommunication);
                        lstUPSICommunication.Add(objUPSICommunication);
                    }

                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstUPSIReport = lstUPSICommunication;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get UPSI Report Between Dates"
        public ReportsResponse GetUPSITemplateReportBetweenDates(UPSICommunication objUPSIReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUPSIReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_UPSI_TEMPLATE_REPORT_BETWEEN_DATES");
                parameters[3] = new SqlParameter("@UPSI_FROM", ConvertDate(objUPSIReport.upsiFrom));
                parameters[4] = new SqlParameter("@UPSI_TO", ConvertDate(objUPSIReport.upsiTo));

                List<UPSICommunication> lstUPSICommunication = new List<UPSICommunication>();

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objUPSIReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UPSICommunication objUPSICommunication = new UPSICommunication();
                        objUPSICommunication.UPSITemplateId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                        objUPSICommunication.natureOfUPSI = !String.IsNullOrEmpty(Convert.ToString(dr["NATURE_OF_UPSI"])) ? Convert.ToString(dr["NATURE_OF_UPSI"]) : String.Empty;
                        objUPSICommunication.whoShared = !String.IsNullOrEmpty(Convert.ToString(dr["WHO_SHARED"])) ? Convert.ToString(dr["WHO_SHARED"]) : String.Empty;
                        objUPSICommunication.withWhomShared = !String.IsNullOrEmpty(Convert.ToString(dr["WITH_WHOM_SHARED"])) ? Convert.ToString(dr["WITH_WHOM_SHARED"]) : String.Empty;
                        objUPSICommunication.panOrOtherIdentification = !String.IsNullOrEmpty(Convert.ToString(dr["PAN_OR_OTHER_IDENTIFICATION"])) ? Convert.ToString(dr["PAN_OR_OTHER_IDENTIFICATION"]) : String.Empty;
                        objUPSICommunication.sharedOn = !String.IsNullOrEmpty(Convert.ToString(dr["SHARED_ON"])) ? Convert.ToString(dr["SHARED_ON"]) : String.Empty;
                        objUPSICommunication.modeOfSharing = !String.IsNullOrEmpty(Convert.ToString(dr["MODE_OF_SHARING"])) ? Convert.ToString(dr["MODE_OF_SHARING"]) : String.Empty;
                        objUPSICommunication.attachmentShared = !String.IsNullOrEmpty(Convert.ToString(dr["ATTACHMENT"])) ? Convert.ToString(dr["ATTACHMENT"]) : String.Empty;
                        objUPSICommunication.createdOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                        objUPSICommunication.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;
                        objUPSICommunication.subjectCreatedOn = !String.IsNullOrEmpty(Convert.ToString(dr["UPSI_CEASE_DATE"])) ? Convert.ToString(dr["UPSI_CEASE_DATE"]) : String.Empty;
                        lstUPSICommunication.Add(objUPSICommunication);
                    }

                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstUPSIReport = lstUPSICommunication;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        #region "Get UPSI Sub-line Attachments"
        private List<string> GetUPSISubLineAttachments(UPSICommunication objUPSICommunicationAttach)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@MODE", "GET_UPSI_REPORT_SUB_LINE_ATTACH");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@HDR_ID", objUPSICommunicationAttach.hdrId);
            parameters[3] = new SqlParameter("@LINE_ID", objUPSICommunicationAttach.lineId);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objUPSICommunicationAttach.MODULE_DATABASE, parameters);
            List<string> lstAttachments = new List<string>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string fileName = !String.IsNullOrEmpty(Convert.ToString(dr["FILE_NAME"])) ? Convert.ToString(dr["FILE_NAME"]) : String.Empty;
                        lstAttachments.Add(fileName);
                    }
                }
            }
            return lstAttachments;
        }
        #endregion

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
        #region "Get Connected Person Trading Report Between Dates"
        public ReportsResponse GetConnectedPersonTradingReport(TradingReport objTradingReport)
        {

            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "GET_CONNECTED_PERSON_TRADING_REPORT_BETWEEN_DATES");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@TRADE_FROM", ConvertDate(objTradingReport.tradingFrom));
                parameters[3] = new SqlParameter("@TRADE_TO", ConvertDate(objTradingReport.tradingTo));
                parameters[4] = new SqlParameter("@USER_TYPE", objTradingReport.UserType);

                ReportsResponse objReportResponse = new ReportsResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objTradingReport.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        TradingReport obj = new TradingReport();
                        obj.name = !String.IsNullOrEmpty(Convert.ToString(rdr["USER_NAME"])) ? Convert.ToString(rdr["USER_NAME"]) : String.Empty;
                        obj.pan = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                        obj.tradeDate = String.Format("{0:dd/MM/yyyy}", rdr["AS_OF_DATE"]);
                        obj.holdingAsOnDate = !String.IsNullOrEmpty(Convert.ToString(rdr["HOLDING"])) ? Convert.ToString(rdr["HOLDING"]) : String.Empty;

                        objReportResponse.AddObject(obj);
                    }

                    objReportResponse.StatusFl = true;
                    objReportResponse.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    objReportResponse.StatusFl = false;
                    objReportResponse.Msg = "No data found !";
                }
                rdr.Close();
                return objReportResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get Broker Note Report Between Dates"
        public ReportsResponse GetBrokerNoteDetailsBetweenDates(TradingReport objTradingReport)
        {

            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", "GET_TRADE_REPORT_BETWEEN_DATES");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@TRADE_FROM", ConvertDate(objTradingReport.tradingFrom));
                parameters[3] = new SqlParameter("@TRADE_TO", ConvertDate(objTradingReport.tradingTo));
                parameters[4] = new SqlParameter("@USER_TYPE", objTradingReport.UserType);
                parameters[5] = new SqlParameter("@ADMIN_DB", objTradingReport.ADMIN_DATABASE);
                parameters[6] = new SqlParameter("@COMPANY_ID", objTradingReport.companyId);

                ReportsResponse objReportResponse = new ReportsResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objTradingReport.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        TradingReport obj = new TradingReport();
                        obj.PreclearanceId = !String.IsNullOrEmpty(Convert.ToString(rdr["ID"])) ? Convert.ToString(rdr["ID"]) : String.Empty;
                        obj.name = !String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"])) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
                        obj.relation = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION"])) ? Convert.ToString(rdr["RELATION"]) : String.Empty;
                        obj.pan = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                        obj.ReqTradeQuantity = !String.IsNullOrEmpty(Convert.ToString(rdr["REQ_QNT"])) ? Convert.ToString(rdr["REQ_QNT"]) : String.Empty;
                        obj.TradeQuantity = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_QUANTITY"])) ? Convert.ToString(rdr["TRADE_QUANTITY"]) : String.Empty;
                        obj.PreclearanceDate = String.Format("{0:dd/MM/yyyy}", rdr["CREATED_ON"]);
                        obj.tradeDate = String.Format("{0:dd/MM/yyyy}", rdr["ACTUAL_TRANSACTION_DATE"]);

                        objReportResponse.AddObject(obj);
                    }

                    objReportResponse.StatusFl = true;
                    objReportResponse.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    objReportResponse.StatusFl = false;
                    objReportResponse.Msg = "No data found !";
                }
                rdr.Close();
                return objReportResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion

        public ReportsResponse GetFormLogsReportBetweenDates(LogsReport objLogsReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@COMPANY_ID", objLogsReport.CompanyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_FORM_LOGS_REPORT_BETWEEN_DATES");
                parameters[3] = new SqlParameter("@TRADE_FROM", ConvertDate(objLogsReport.FromDate));
                parameters[4] = new SqlParameter("@TRADE_TO", ConvertDate(objLogsReport.ToDate));
                parameters[5] = new SqlParameter("@User_Login", objLogsReport.UserLogin);
                parameters[6] = new SqlParameter("@FormId", objLogsReport.FormId);

                List<LogsReport> lstLogsReport = new List<LogsReport>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objLogsReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        LogsReport LogsReport = new LogsReport();
                        LogsReport.FilePath = !String.IsNullOrEmpty(Convert.ToString(dr1["FILE_NAME"])) ? Convert.ToString(dr1["FILE_NAME"]) : String.Empty;
                        LogsReport.FormName = !String.IsNullOrEmpty(Convert.ToString(dr1["FORM_NM"])) ? Convert.ToString(dr1["FORM_NM"]) : String.Empty;
                        LogsReport.CreatedOn = !String.IsNullOrEmpty(Convert.ToString(dr1["CREATED_ON"])) ? Convert.ToString(dr1["CREATED_ON"]) : String.Empty;

                        lstLogsReport.Add(LogsReport);
                    }

                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstLogsReport = lstLogsReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }

        public ReportsResponse GetTaskDisclouserReport(User objLogsReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@COMPANY_ID", objLogsReport.companyId);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_TASK_DISCLOUSER_REPORT");
                parameters[3] = new SqlParameter("@USER_NM", objLogsReport.USER_NM);
                parameters[4] = new SqlParameter("@TASK_STATUS", objLogsReport.status);
                parameters[5] = new SqlParameter("@TASK_FOR", objLogsReport.TaskFor);
                List<User> lstDisclouserReport = new List<User>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objLogsReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        User LogsReport = new User();
                        LogsReport.TaskId = Convert.ToInt32(dr1["TASK_ID"]);
                        LogsReport.USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr1["USER_NM"])) ? Convert.ToString(dr1["USER_NM"]) : String.Empty;
                        LogsReport.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr1["USER_EMAIL"])) ? Convert.ToString(dr1["USER_EMAIL"]) : String.Empty;
                        LogsReport.status = !String.IsNullOrEmpty(Convert.ToString(dr1["TASK_STATUS"])) ? Convert.ToString(dr1["TASK_STATUS"]) : String.Empty;
                        LogsReport.formSubmittedOn = !String.IsNullOrEmpty(Convert.ToString(dr1["TASK_COMPLETED_ON"])) ? Convert.ToString(dr1["TASK_COMPLETED_ON"]) : String.Empty;
                        LogsReport.formName = !String.IsNullOrEmpty(Convert.ToString(dr1["FILE_NAME"])) ? Convert.ToString(dr1["FILE_NAME"]) : String.Empty;
                        LogsReport.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr1["PAN"])) ? Convert.ToString(dr1["PAN"]) : String.Empty;
                        LogsReport.DepartmentName = !String.IsNullOrEmpty(Convert.ToString(dr1["DEPARTMENT_NAME"])) ? Convert.ToString(dr1["DEPARTMENT_NAME"]) : String.Empty;
                        LogsReport.DesignationName = !String.IsNullOrEmpty(Convert.ToString(dr1["DESIGNATION_NAME"])) ? Convert.ToString(dr1["DESIGNATION_NAME"]) : String.Empty;
                        lstDisclouserReport.Add(LogsReport);
                    }
                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstDisclouserReport = lstDisclouserReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }

        //===============pending tsk rpt by skm=========
        public ReportsResponse GetPendingTaskReport(Email objLogsReport)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                //parameters[0] = new SqlParameter("@COMPANY_ID", objLogsReport.companyId);
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_PENDING_TASK_REPORT");
                parameters[2] = new SqlParameter("@TRADE_FROM", FormatHelper.ConvertDate(objLogsReport.mailFrom));
                parameters[3] = new SqlParameter("@TRADE_TO", FormatHelper.ConvertDate(objLogsReport.mailTo));
                //parameters[5] = new SqlParameter("@TASK_FOR", objLogsReport.TaskFor);
                List<Email> lstPendingTaskReport = new List<Email>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objLogsReport.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        Email LogsReport = new Email();
                        //LogsReport.TaskFor = Convert.ToInt32(dr1["TASK_ID"]);
                        LogsReport.TaskFor = !String.IsNullOrEmpty(Convert.ToString(dr1["TASK_FOR"])) ? Convert.ToString(dr1["TASK_FOR"]) : String.Empty;
                        LogsReport.UserEmail = !String.IsNullOrEmpty(Convert.ToString(dr1["USER_EMAIL"])) ? Convert.ToString(dr1["USER_EMAIL"]) : String.Empty;
                        LogsReport.mailFrom = !String.IsNullOrEmpty(Convert.ToString(dr1["EMAIL_FROM"])) ? Convert.ToString(dr1["EMAIL_FROM"]) : String.Empty;
                        LogsReport.mailTo = !String.IsNullOrEmpty(Convert.ToString(dr1["EMAIL_TO"])) ? Convert.ToString(dr1["EMAIL_TO"]) : String.Empty;
                        LogsReport.EmailDate = !String.IsNullOrEmpty(Convert.ToString(dr1["EMAIL_DATE"])) ? Convert.ToString(dr1["EMAIL_DATE"]) : String.Empty;
                        LogsReport.subject = !String.IsNullOrEmpty(Convert.ToString(dr1["MSG_SUBJECT"])) ? Convert.ToString(dr1["MSG_SUBJECT"]) : String.Empty;
                        LogsReport.CreatedOn = !String.IsNullOrEmpty(Convert.ToString(dr1["CREATED_ON"])) ? Convert.ToString(dr1["CREATED_ON"]) : String.Empty;
                        //LogsReport.DesignationName = !String.IsNullOrEmpty(Convert.ToString(dr1["DESIGNATION_NAME"])) ? Convert.ToString(dr1["DESIGNATION_NAME"]) : String.Empty;
                        lstPendingTaskReport.Add(LogsReport);
                    }
                }
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.lstPendingTaskReport = lstPendingTaskReport;
                objReportsResponse.StatusFl = true;
                objReportsResponse.Msg = "Reports has been fetched successfully !";
                return objReportsResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        //============================
        public ReportsResponse GetNonComplianceReport(TradingReport objTradingReport)
        {

            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@FromDt", FormatHelper.ConvertDate(objTradingReport.tradingFrom));
                parameters[1] = new SqlParameter("@ToDt", FormatHelper.ConvertDate(objTradingReport.tradingTo));
                parameters[2] = new SqlParameter("@LoginId", objTradingReport.userId);
                parameters[3] = new SqlParameter("@AdminDb", objTradingReport.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@CompanyId", objTradingReport.companyId);

                ReportsResponse objReportResponse = new ReportsResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_NON_COMPLIANCE_REPORT", objTradingReport.MODULE_DATABASE, parameters);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count>0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        TradingReport obj = new TradingReport();
                        obj.PreclearanceId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToString(dr["ID"]) : String.Empty;
                        obj.name = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                        obj.relativeName = !String.IsNullOrEmpty(Convert.ToString(dr["NM"])) ? Convert.ToString(dr["NM"]) : String.Empty;
                        //obj.relation = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION"])) ? Convert.ToString(rdr["RELATION"]) : String.Empty;
                        obj.pan = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                        obj.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO"])) ? Convert.ToString(dr["FOLIO"]) : String.Empty;
                        obj.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_TYPE"])) ? Convert.ToString(dr["SUB_TYPE"]) : String.Empty;
                        obj.TradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["QTY"])) ? Convert.ToString(dr["QTY"]) : String.Empty;
                        obj.equityValue = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE"])) ? Convert.ToString(dr["VALUE"]) : String.Empty;
                        obj.PreclearanceDate = Convert.ToDateTime(dr["TRANS_DATE"]).ToString("yyyy-MM-dd");
                        obj.nonComplianceTaskStatus= !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TYPE"])) ? Convert.ToString(dr["NON_COMPLIANCE_TYPE"]) : String.Empty;

                        objReportResponse.AddObject(obj);
                    }

                    objReportResponse.StatusFl = true;
                    objReportResponse.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    objReportResponse.StatusFl = false;
                    objReportResponse.Msg = "No data found !";
                }
                //rdr.Close();
                return objReportResponse;
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }

    }
}