using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class DashboardRepository : IRequiresSessionState
    {
        #region "Get All Disclosure Request Count"
        public DashboardResponse GetOpenDisclosureRequest(Dashboard objDashboard)
        {
            try
            {
             
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "GET_ALL_DISCLOSURE_REQUEST_COUNT");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                parameters[4]= new SqlParameter("@Admin_Database", objDashboard.ADMIN_DATABASE);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<DashboardActionable> lstActionable = new List<DashboardActionable>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DashboardActionable obj = new DashboardActionable();
                            obj.DataElementId = Convert.ToString(dr["DATA_ELEMENT_ID"]);
                            obj.TaskCreatedOn = Convert.ToString(dr["REQUESTED_ON"]);
                            obj.Remarks = Convert.ToString(dr["REMARKS"]);
                            obj.TaskId = Convert.ToInt32(dr["TASK_ID"]);
                            obj.TaskType = Convert.ToString(dr["TASK_FOR"]);
                            obj.UserLogin = Convert.ToString(dr["USER_LOGIN"]);
                            obj.UserNm = Convert.ToString(dr["USER_NM"]);
                            lstActionable.Add(obj);
                        }
                        objDashboardResponse.lstActioanble = lstActionable;
                        objDashboardResponse.StatusFl = true;
                        objDashboardResponse.Msg = "Data has been fetched successfully!";
                    }
                    else
                    {
                        objDashboardResponse.StatusFl = false;
                        objDashboardResponse.Msg = "No data found!";
                    }
                }
                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get All Pre-Clearance Request Count"
        public DashboardResponse GetAllPreClearanceRequestCount(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_ALL_PRE_CLEARANCE_REQUEST_COUNT");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Dashboard obj = new Dashboard();
                    obj.allPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ALL_PRECLEARANCE_REQUEST_COUNT"]);
                    obj.inApprovalPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["INAPPROVAL_PRECLEARANCE_REQUEST_COUNT"]);
                    obj.approvedPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["APPROVED_PRECLEARANCE_REQUEST_COUNT"]);
                    obj.rejectedPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["REJECTED_PRECLEARANCE_REQUEST_COUNT"]);
                    objDashboardResponse.Dashboard = obj;
                    objDashboardResponse.StatusFl = true;
                    objDashboardResponse.Msg = "Data has been fetched successfully!";
                }
                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get All Pre-Clearance Request Count For All User"
        public DashboardResponse GetAllPreClearanceRequestCountForAllUser(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_ALL_PRE_CLEARANCE_REQUEST_COUNT_ALL_USER");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Dashboard obj = new Dashboard();
                    obj.allPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ALL_PRECLEARANCE_REQUEST_COUNT"]);
                    obj.inApprovalPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["INAPPROVAL_PRECLEARANCE_REQUEST_COUNT"]);
                    obj.approvedPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["APPROVED_PRECLEARANCE_REQUEST_COUNT"]);
                    obj.rejectedPreClearanceRequestCount = Convert.ToInt32(ds.Tables[0].Rows[0]["REJECTED_PRECLEARANCE_REQUEST_COUNT"]);
                    objDashboardResponse.Dashboard = obj;
                    objDashboardResponse.StatusFl = true;
                    objDashboardResponse.Msg = "Data has been fetched successfully!";
                }
                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get Count Of All Trade Details"
        public DashboardResponse GetCountOfAllTradeDetails(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_COUNT_OF_ALL_TRADE_DETAILS");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Dashboard obj = new Dashboard();
                    obj.submittedWithClearanceCount = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBMITTED_WITH_CLEARANCE_COUNT"]);
                    obj.submittedWithoutClearanceCount = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBMITTED_WITHOUT_CLEARANCE_COUNT"]);
                    obj.notDeclaredCount = Convert.ToInt32(ds.Tables[0].Rows[0]["NOT_DECLARED_COUNT"]);
                    objDashboardResponse.Dashboard = obj;
                    objDashboardResponse.StatusFl = true;
                    objDashboardResponse.Msg = "Data has been fetched successfully!";
                }
                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get Count Of My Trade Details"
        public DashboardResponse GetCountOfMyTradeDetails(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_COUNT_OF_MY_TRADE_DETAILS");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Dashboard obj = new Dashboard();
                    obj.submittedWithClearanceCount = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBMITTED_WITH_CLEARANCE_COUNT"]);
                    obj.submittedWithoutClearanceCount = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBMITTED_WITHOUT_CLEARANCE_COUNT"]);
                    obj.notDeclaredCount = Convert.ToInt32(ds.Tables[0].Rows[0]["NOT_DECLARED_COUNT"]);
                    objDashboardResponse.Dashboard = obj;
                    objDashboardResponse.StatusFl = true;
                    objDashboardResponse.Msg = "Data has been fetched successfully!";
                }
                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get Trade Details Info"
        public DashboardResponse GetTradeDetailsInfo(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_TRADE_DETAILS_INFO");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);

                DataTable dt = ds.Tables[0];
                DashboardResponse objDashboardResponse = new DashboardResponse();
                if (dt.Rows.Count > 0)
                {
                    Dashboard obj = new Dashboard();
                    obj.tradingLimitPerQuarterBeforePC = dt.Rows[0]["TRADING_LIMIT_PER_QUARTER_BEFORE_PC"] == System.DBNull.Value ? "0" : FormatHelper.FormatNumber(ds.Tables[0].Rows[0]["TRADING_LIMIT_PER_QUARTER_BEFORE_PC"].ToString());
                    obj.tradingLimitType = dt.Rows[0]["TRADING_LIMIT_PER_QUARTER_TYPE"] == System.DBNull.Value ? "" : Convert.ToString(ds.Tables[0].Rows[0]["TRADING_LIMIT_PER_QUARTER_TYPE"]);
                    obj.stocksTradedInPeriodUsingBenpos = dt.Rows[0]["STOCKS_TRADED_IN_PERIOD_USING_BENPOS"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["STOCKS_TRADED_IN_PERIOD_USING_BENPOS"]);
                    obj.stocksTradedInPeriodUsingBrokerNote = dt.Rows[0]["STOCKS_TRADED_IN_PERIOD_USING_BROKER_NOTE"] == System.DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["STOCKS_TRADED_IN_PERIOD_USING_BROKER_NOTE"]);
                    obj.lastCurrentHoldingUpdateDate = !String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["LAST_UPDATE_CURRENT_HOLDING_DATE"])) ? Convert.ToString(dt.Rows[0]["LAST_UPDATE_CURRENT_HOLDING_DATE"]) : String.Empty;
                    obj.userCurrentHolding = !String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["USER_CURRENT_HOLDING"])) ? Convert.ToString(dt.Rows[0]["USER_CURRENT_HOLDING"]) : String.Empty;
                    objDashboardResponse.Dashboard = obj;
                    objDashboardResponse.StatusFl = true;
                    objDashboardResponse.Msg = "Data has been fetched successfully!";
                }
                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }
                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get My Actionable"
        public DashboardResponse GetMyActionable(Dashboard objDashboard)
        
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_MY_DECLARATION_TASK");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();
                Dashboard obj = new Dashboard();
                Int32 obj1 = (Int32)(parameters[1].Value);
                if (obj1 == 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string startDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["START_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["START_DATE"].ToString()) : String.Empty;
                        string endDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["END_DATE"])) ? FormatHelper.FormatDate(ds.Tables[0].Rows[0]["END_DATE"].ToString()) : String.Empty;
                        obj.myDeclarationText = "Declaration required - " + startDate + " (to be submitted by " + endDate + ")";
                    }
                }
                else
                {
                    obj.myDeclarationText = null;
                }
                obj.lstNonComplianceTask = this.GetNonComplianceTask(objDashboard);
                TrainingModule trainingModule = new TrainingModule
                {
                    MODULE_DATABASE = objDashboard.MODULE_DATABASE,
                    companyId = objDashboard.companyId,
                    userLogin = objDashboard.loginId
                };
                obj.trainingModule = new TrainingRepository().GetTrainingModules(trainingModule);

                TransactionHistory transactionHistory = new TransactionHistory
                {
                    MODULE_DATABASE = objDashboard.MODULE_DATABASE,
                    companyId = objDashboard.companyId,
                    userLogin = objDashboard.loginId,
                    ADMIN_DATABASE = objDashboard.ADMIN_DATABASE
                };
                obj.transactionHistory = new TransactionHistoryRepository().GetTransactionHistory(transactionHistory);

                TransactionHistory CompliantTransactionHistory = new TransactionHistory
                {
                    MODULE_DATABASE = objDashboard.MODULE_DATABASE,
                    companyId = objDashboard.companyId,
                    userLogin = objDashboard.loginId,
                    ADMIN_DATABASE = objDashboard.ADMIN_DATABASE
                };
                obj.CompliantTransactionHistory = new TransactionHistoryRepository().GetCompliantTransactionHistory(transactionHistory);


                TransactionSubTypeMaster transactionSubTypeMaster = new TransactionSubTypeMaster
                {
                    MODULE_DATABASE = objDashboard.MODULE_DATABASE
                };
                obj.transactionSubTypeMaster = new TransactionSubTypeMasterRepository().GetTransactionSubTypeMaster(transactionSubTypeMaster);

                obj.lstNonCompliance = GetNonComplianceList(transactionHistory);

                objDashboardResponse.Dashboard = obj;
                objDashboardResponse.StatusFl = true;
                objDashboardResponse.Msg = "Data has been fetched successfully!";

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get Non Compliance Task"
        public List<PreClearanceRequest> GetNonComplianceTask(Dashboard objDashboard)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@MODE", "GET_MY_NON_COMPLIANCE_TASK");
            parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
            parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DASHBOARD", objDashboard.MODULE_DATABASE, parameters);
            List<PreClearanceRequest> lstPreClearanceRequest = new List<PreClearanceRequest>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PreClearanceRequest obj = new PreClearanceRequest();
                    obj.Id = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                    obj.PreClearanceRequestId = !String.IsNullOrEmpty(Convert.ToString(dr["ACTION_ID"])) ? Convert.ToInt32(dr["ACTION_ID"]) : 0;
                    obj.ActualTradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : String.Empty;
                    obj.ValuePerShare = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE_PER_SHARE"])) ? Convert.ToString(dr["VALUE_PER_SHARE"]) : String.Empty;
                    obj.TotalAmount = !String.IsNullOrEmpty(Convert.ToString(dr["TOTAL_AMOUNT"])) ? FormatHelper.FormatNumber(Convert.ToString(dr["TOTAL_AMOUNT"])) : String.Empty;
                    obj.ActualTransactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["ACTUAL_TRANSACTION_DATE"])) ? FormatHelper.FormatDate(Convert.ToString(dr["ACTUAL_TRANSACTION_DATE"])) : String.Empty;
                    obj.BrokerNote = !String.IsNullOrEmpty(Convert.ToString(dr["BROKER_NOTE"])) ? Convert.ToString(dr["BROKER_NOTE"]) : String.Empty;
                    obj.relativeId = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_ID"])) ? Convert.ToInt32(dr["RELATIVE_ID"]) : 0;
                    obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;
                    obj.CreatedOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                    obj.nonCompliantTaskText = "Broker Note Upload Request (Dated - " + obj.CreatedOn + ")";
                    lstPreClearanceRequest.Add(obj);
                }

            }
            return lstPreClearanceRequest;
        }
        #endregion
        #region "Update Trade Bifurcation"
        public DashboardResponse UpdateTradeBifurcation(Dashboard objDashboard)
        {
            try
            {
                foreach (TransactionSubTypeMaster objTransactionBifurcation in objDashboard.transactionHistoryBifurcation.lstTransactionBifurcation)
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@MODE", "UPDATE_TRADE_BIFURCATION");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@USER_LOGIN", objDashboard.loginId);
                    parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                    parameters[4] = new SqlParameter("@TRANSACTION_ID", objDashboard.transactionHistoryBifurcation.transactionId);
                    parameters[5] = new SqlParameter("@CATEGORY", objTransactionBifurcation.category);
                    parameters[6] = new SqlParameter("@TRADE_QUANTITY", objTransactionBifurcation.tradeQuantity);
                    parameters[7] = new SqlParameter("@TRADE_VALUE", objTransactionBifurcation.tradeValue);
                    parameters[8] = new SqlParameter("@REMARKS", objTransactionBifurcation.remarks);
                    parameters[9] = new SqlParameter("@TRANS_DATE", ConvertDate(objTransactionBifurcation.transactionDate));


                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objDashboard.MODULE_DATABASE, parameters);

                    if ((Int32)(parameters[1].Value) > 0)
                    {
                        Int32 transactionid = Convert.ToInt32(parameters[1].Value);
                        objDashboard.lstNonCompliance = GetNonComplianceAndItsType(transactionid, objDashboard);
                    }

                }

                SqlParameter[] parameters1 = new SqlParameter[3];
                parameters1[0] = new SqlParameter("@MODE", "DELETE_EXISTING_TRADE_HISTORY");
                parameters1[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters1[1].Direction = ParameterDirection.Output;
                parameters1[2] = new SqlParameter("@TRANSACTION_ID", objDashboard.transactionHistoryBifurcation.transactionId);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objDashboard.MODULE_DATABASE, parameters1);

                if (objDashboard.lstNonCompliance != null)
                {
                    if (objDashboard.lstNonCompliance.Count > 0)
                    {
                        foreach (NonCompliance obj in objDashboard.lstNonCompliance)
                        {
                            SqlParameter[] parameters2 = new SqlParameter[5];
                            parameters2[0] = new SqlParameter("@MODE", "INSERT_NON_COMPLIANCE");
                            parameters2[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                            parameters2[1].Direction = ParameterDirection.Output;
                            parameters2[2] = new SqlParameter("@TRANSACTION_ID", obj.transactionId);
                            parameters2[3] = new SqlParameter("@NON_COMPLIANCE_TYPE", obj.nonComplianceType);
                            parameters2[4] = new SqlParameter("@NC_AMOUNT", obj.ncAmount);


                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objDashboard.MODULE_DATABASE, parameters2);
                        }
                    }
                }


                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.Dashboard = objDashboard;
                objDashboardResponse.StatusFl = true;
                objDashboardResponse.Msg = "Data has been fetched successfully!";
                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
        #region "Get Non-Compliance Type List"
        public List<NonCompliance> GetNonComplianceAndItsType(Int32 transactionId, Dashboard objDashboard)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@MODE", "CHECK_FOR_NON_COMPLIANCE");
            parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@TRANSACTION_ID", transactionId);

            List<NonCompliance> lstNonCompliance = new List<NonCompliance>();

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objDashboard.MODULE_DATABASE, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        NonCompliance obj = new NonCompliance();
                        obj.status = true;
                        obj.nonComplianceType = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TYPE"])) ? Convert.ToString(dr["NON_COMPLIANCE_TYPE"]) : String.Empty;
                        obj.nonComplianceTradeQuantityOrValue = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY_OR_VALUE"])) ? Convert.ToString(dr["TRADE_QUANTITY_OR_VALUE"]) : String.Empty;
                        obj.subType = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_TYPE"])) ? Convert.ToString(dr["SUB_TYPE"]) : String.Empty;
                        obj.ncAmount = !String.IsNullOrEmpty(Convert.ToString(dr["NC_AMOUNT"])) ? FormatHelper.FormatNumber(Convert.ToString(dr["NC_AMOUNT"])) : "0";
                        obj.transactionId = transactionId;

                        lstNonCompliance.Add(obj);
                    }
                }
            }

            return lstNonCompliance;
        }
        #endregion
        #region "Get Non-Compliance Task List"
        private List<NonCompliance> GetNonComplianceList(TransactionHistory objTransactionHistory)
        {
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@MODE", "GET_NON_COMPLIANCE");
            parameter[1] = new SqlParameter("@COMPANY_ID", objTransactionHistory.companyId);
            parameter[2] = new SqlParameter("@USER_LOGIN", objTransactionHistory.userLogin);
            parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameter[3].Direction = ParameterDirection.Output;
            parameter[4] = new SqlParameter("@ADMIN_DB", objTransactionHistory.ADMIN_DATABASE);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objTransactionHistory.MODULE_DATABASE, parameter);

            List<NonCompliance> lstNonCompliance = new List<NonCompliance>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        NonCompliance obj = new NonCompliance();
                        obj.nonComplianceId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                        //obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                        obj.transactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRANS_DATE"])) ? FormatHelper.FormatDate(Convert.ToString(dr["TRANS_DATE"])) : String.Empty;
                        obj.nonComplianceType = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TYPE"])) ? Convert.ToString(dr["NON_COMPLIANCE_TYPE"]) : String.Empty;
                        obj.ncAmount = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE"])) ? FormatHelper.FormatNumber(Convert.ToString(dr["VALUE"])) : String.Empty;
                        obj.ncQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["QTY"])) ? FormatHelper.FormatNumber(Convert.ToString(dr["QTY"])) : String.Empty;
                        obj.sName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                        obj.subType = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_TYPE"])) ? Convert.ToString(dr["SUB_TYPE"]) : String.Empty;
                        obj.reportedOn = !String.IsNullOrEmpty(Convert.ToString(dr["REPORTED_ON"])) ? FormatHelper.FormatDate(Convert.ToString(dr["REPORTED_ON"])) : String.Empty;
                        obj.transactionStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRANS_SDATE"])) ? FormatHelper.FormatDate(Convert.ToString(dr["TRANS_SDATE"])) : String.Empty;
                        obj.transactionEndDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRANS_EDATE"])) ? FormatHelper.FormatDate(Convert.ToString(dr["TRANS_EDATE"])) : String.Empty;
                        obj.Relation = !String.IsNullOrEmpty(Convert.ToString(dr["RELATION"])) ? Convert.ToString(dr["RELATION"]) : String.Empty;
                        obj.Folio = !String.IsNullOrEmpty(Convert.ToString(dr["Folio"])) ? Convert.ToString(dr["Folio"]) : String.Empty;
                        obj.RelativeId = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_ID"])) ? Convert.ToInt32(dr["RELATIVE_ID"]) : 0;
                        obj.TransactionId = !String.IsNullOrEmpty(Convert.ToString(dr["TRANSACTION_ID"])) ? Convert.ToInt32(dr["TRANSACTION_ID"]) : 0;
                        obj.RelativeName = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_NAME"])) ? Convert.ToString(dr["RELATIVE_NAME"]) : String.Empty;


                        obj.Relation = Convert.ToString(dr["RELATION"]);
                        if (Convert.ToString(dr["SELF_PAN"]) == "")
                        {
                            obj.panNumber = Convert.ToString(dr["RELATIVE_PAN"]);
                        }
                        else
                        {
                            obj.panNumber = Convert.ToString(dr["SELF_PAN"]);
                        }

                        lstNonCompliance.Add(obj);
                    }
                }

            }
            return lstNonCompliance;

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
        #region "Get My UPSI Task"
        public DashboardResponse GetMyUPSITask(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "LIST_TASK_MESSAGE");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objDashboard.loginId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                parameters[4] = new SqlParameter("@ADMIN_DB", objDashboard.ADMIN_DATABASE);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_MEMBERS_DETAL_LIST", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();

                Int32 obj1 = (Int32)(parameters[1].Value);
                List<DashboardUPSITask> listUpsiTask = new List<DashboardUPSITask>();
                if (obj1 > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dt in ds.Tables[0].Rows)
                        {

                            DashboardUPSITask obj = new DashboardUPSITask();
                            obj.TaskId = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_ID"])) ? Convert.ToString(dt["TASK_ID"]) : String.Empty;
                            obj.TaskFor = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_FOR"])) ? Convert.ToString(dt["TASK_FOR"]) : String.Empty;
                            obj.TaskMG = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_MSG"])) ? Convert.ToString(dt["TASK_MSG"]) : String.Empty;
                            obj.EmailFrom = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_FROM"])) ? Convert.ToString(dt["EMAIL_FROM"]) : String.Empty;
                            obj.EmailTo = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_TO"])) ? Convert.ToString(dt["EMAIL_TO"]) : String.Empty;
                            obj.EmailSubject = !String.IsNullOrEmpty(Convert.ToString(dt["MSG_SUBJECT"])) ? Convert.ToString(dt["MSG_SUBJECT"]) : String.Empty;
                            obj.EmailCC = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_CC"])) ? Convert.ToString(dt["EMAIL_CC"]) : String.Empty;
                            obj.Status = !String.IsNullOrEmpty(Convert.ToString(dt["STATUS"])) ? Convert.ToString(dt["STATUS"]) : String.Empty;
                            DateTime da = Convert.ToDateTime(dt["EMAIL_DATE"]);
                            string Emaildate = da.ToString("yyyy-MM-dd");
                            obj.EmailDate = Emaildate;
                            listUpsiTask.Add(obj);
                        }
                        objDashboardResponse.Dashboard = new Dashboard
                        {
                            listUPSITask = listUpsiTask
                        };

                        objDashboardResponse.StatusFl = true;
                        objDashboardResponse.Msg = "Data has been fetched successfully!";
                    }
                    else
                    {
                        objDashboardResponse.StatusFl = false;
                        objDashboardResponse.Msg = "No data found!";
                    }
                }
                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = ex.Message;
                return objDashboardResponse;
            }
        }
        public DashboardResponse GetMyUPSITaskById(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "LIST_TASK_MESSAGE_BY_ID");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", Convert.ToInt32(objDashboard.loginId));
                parameters[3] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_MEMBERS_DETAL_LIST", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();

                Int32 obj1 = (Int32)(parameters[1].Value);
                List<DashboardUPSITask> listUpsiTask = new List<DashboardUPSITask>();
                if (obj1 > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dt in ds.Tables[0].Rows)
                        {
                            DashboardUPSITask obj = new DashboardUPSITask();
                            obj.TaskId = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_ID"])) ? Convert.ToString(dt["TASK_ID"]) : String.Empty;
                            obj.TaskFor = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_FOR"])) ? Convert.ToString(dt["TASK_FOR"]) : String.Empty;
                            obj.TaskMG = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_MSG"])) ? Convert.ToString(dt["TASK_MSG"]) : String.Empty;
                            obj.TaskMailBody = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_MSG"])) ? Convert.ToString(dt["EMAIL_MSG"]) : String.Empty;
                            obj.EmailFrom = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_FROM"])) ? Convert.ToString(dt["EMAIL_FROM"]) : String.Empty;
                            obj.EmailTo = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_TO"])) ? Convert.ToString(dt["EMAIL_TO"]) : String.Empty;
                            obj.EmailSubject = !String.IsNullOrEmpty(Convert.ToString(dt["MSG_SUBJECT"])) ? Convert.ToString(dt["MSG_SUBJECT"]) : String.Empty;
                            obj.EmailCC = !String.IsNullOrEmpty(Convert.ToString(dt["EMAIL_CC"])) ? Convert.ToString(dt["EMAIL_CC"]) : String.Empty;
                            obj.Status = !String.IsNullOrEmpty(Convert.ToString(dt["STATUS"])) ? Convert.ToString(dt["STATUS"]) : String.Empty;
                            obj.Group_id = !String.IsNullOrEmpty(Convert.ToString(dt["VISIBILITY"])) ? Convert.ToString(dt["VISIBILITY"]) : String.Empty;
                            DateTime da = Convert.ToDateTime(dt["EMAIL_DATE"]);
                            string Emaildate = da.ToString("dd/MM/yyyy");
                            obj.EmailDate = Emaildate;
                            obj.listAttachment = getAllAttechment(obj.TaskId, objDashboard.MODULE_DATABASE);
                            listUpsiTask.Add(obj);
                        }
                        objDashboardResponse.Dashboard = new Dashboard
                        {
                            listUPSITask = listUpsiTask
                        };

                        objDashboardResponse.StatusFl = true;
                        objDashboardResponse.Msg = "Data has been fetched successfully!";
                    }
                    else
                    {
                        objDashboardResponse.StatusFl = false;
                        objDashboardResponse.Msg = "No data found!";
                    }
                }






                //else
                //{
                //    objDashboardResponse.StatusFl = false;
                //    objDashboardResponse.Msg = "No data found!";
                //}

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = ex.Message;
                // objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        public DashboardResponse UpdateUPSITaskById(Dashboard objDashboard)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_TASK_MESSAGE_BY_ID");
                parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@TASK_ID", Convert.ToInt32(objDashboard.UPSITask.TaskId));
                parameters[3] = new SqlParameter("@UPSI_GROUP_ID", Convert.ToInt32(objDashboard.UPSITask.Group_id));
                parameters[4] = new SqlParameter("@STATUS", Convert.ToString(objDashboard.UPSITask.Status));

                parameters[5] = new SqlParameter("@COMPANY_ID", objDashboard.companyId);



                int st = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_MEMBERS_DETAL_LIST", objDashboard.MODULE_DATABASE, parameters);
                DashboardResponse objDashboardResponse = new DashboardResponse();

                Int32 obj1 = (Int32)(parameters[1].Value);
                List<DashboardUPSITask> listUpsiTask = new List<DashboardUPSITask>();
                if (obj1 > 0)
                {
                    objDashboardResponse.StatusFl = true;
                    objDashboardResponse.Msg = "Task Updated!";

                }


                else
                {
                    objDashboardResponse.StatusFl = false;
                    objDashboardResponse.Msg = "No data found!";
                }

                return objDashboardResponse;
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = ex.Message;
                // objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        public List<UPSIRemarksAttachments> getAllAttechment(string task_id, string MODULE_DATABASE)
        {
            List<UPSIRemarksAttachments> list = new List<UPSIRemarksAttachments>();

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@MODE", "LIST_TASK_ATTECHMENT_BY_TASKID");
            //parameters[1] = new SqlParameter("SET_COUNT", SqlDbType.Int);
            //parameters[1].Direction = ParameterDirection.Output;
            parameters[1] = new SqlParameter("@TASK_ID", Convert.ToInt32(task_id));

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_UPSI_MEMBERS_DETAL_LIST", MODULE_DATABASE, parameters);
            DashboardResponse objDashboardResponse = new DashboardResponse();

            //Int32 obj1 = (Int32)(parameters[1].Value);

            //if (obj1 > 0)
            //{
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dt in ds.Tables[0].Rows)
                {

                    UPSIRemarksAttachments obj = new UPSIRemarksAttachments();
                    obj.hdrid = !String.IsNullOrEmpty(Convert.ToString(dt["TASK_ID"])) ? Convert.ToString(dt["TASK_ID"]) : String.Empty;
                    obj.Attachment = !String.IsNullOrEmpty(Convert.ToString(dt["ATTECHMENT"])) ? Convert.ToString(dt["ATTECHMENT"]) : String.Empty;


                    list.Add(obj);
                }



            }

            //}




            return list;

        }
        #endregion
    }
}