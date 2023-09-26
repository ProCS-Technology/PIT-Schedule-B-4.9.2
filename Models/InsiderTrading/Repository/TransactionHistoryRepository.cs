using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class TransactionHistoryRepository
    {
        #region "Get All Transaction History"
        public TransactionHistoryResponse GetTransactionHistory(TransactionHistory objTransactionHistory)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@MODE", "GET_TRANSACTION_HISTORY_BY_USER");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTransactionHistory.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTransactionHistory.userLogin);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;
                parameter[4] = new SqlParameter("@ADMIN_DB", objTransactionHistory.ADMIN_DATABASE);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objTransactionHistory.MODULE_DATABASE, parameter);
                TransactionHistoryResponse objResponse = new TransactionHistoryResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TransactionHistory obj = new TransactionHistory();
                            obj.transactionId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                            obj.userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                            obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                            obj.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO"])) ? Convert.ToString(dr["FOLIO"]) : String.Empty;
                            obj.transactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRANS_DATE"])) ? FormatHelper.FormatDate(Convert.ToString(dr["TRANS_DATE"])) : String.Empty;
                            obj.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["QTY"])) ? Convert.ToInt32(dr["QTY"]) : 0;
                            obj.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_TYPE"])) ? Convert.ToString(dr["SUB_TYPE"]) : String.Empty;
                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TransactionHistoryResponse objResponse = new TransactionHistoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion

        #region "Get All Transaction History"
        public TransactionHistoryResponse GetTransactionHistoryByAllUser(TransactionHistory objTransactionHistory)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@MODE", "GET_TRANSACTION_HISTORY_BY_ALL_USER");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTransactionHistory.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTransactionHistory.userLogin);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;
                parameter[4] = new SqlParameter("@ADMIN_DB", objTransactionHistory.ADMIN_DATABASE);
                parameter[5] = new SqlParameter("@TRANSACTION_ID", objTransactionHistory.transactionId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objTransactionHistory.MODULE_DATABASE, parameter);
                TransactionHistoryResponse objResponse = new TransactionHistoryResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TransactionHistory obj = new TransactionHistory();
                            obj.transactionId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                            obj.userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                            obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                            obj.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO"])) ? Convert.ToString(dr["FOLIO"]) : String.Empty;
                            obj.transactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRANS_DATE"])) ? Convert.ToString(dr["TRANS_DATE"]) : String.Empty;
                            obj.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["QTY"])) ? Convert.ToInt32(dr["QTY"]) : 0;
                            obj.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_TYPE"])) ? Convert.ToString(dr["SUB_TYPE"]) : String.Empty;
                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TransactionHistoryResponse objResponse = new TransactionHistoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion

        #region "Get Compliant Transaction History For CO"
        public TransactionHistoryResponse GetCompliantTransactionHistory(TransactionHistory objTransactionHistory)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@MODE", "GET_TRANSACTION_HISTORY_FOR_CO");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTransactionHistory.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTransactionHistory.userLogin);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;
                parameter[4] = new SqlParameter("@ADMIN_DB", objTransactionHistory.ADMIN_DATABASE);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objTransactionHistory.MODULE_DATABASE, parameter);
                TransactionHistoryResponse objResponse = new TransactionHistoryResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TransactionHistory obj = new TransactionHistory();
                            obj.transactionId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                            obj.userName = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_NAME"])) ? Convert.ToString(dr["RELATIVE_NAME"]) : String.Empty;
                            obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                            obj.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO"])) ? Convert.ToString(dr["FOLIO"]) : String.Empty;
                            obj.transactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRANS_DATE"])) ? FormatHelper.FormatDate(Convert.ToString(dr["TRANS_DATE"])) : String.Empty;
                            obj.Quantity = !String.IsNullOrEmpty(Convert.ToString(dr["QTY"])) ? FormatHelper.FormatNumber(Convert.ToInt32(dr["QTY"]).ToString()) : string.Empty;
                            obj.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_TYPE"])) ? Convert.ToString(dr["SUB_TYPE"]) : String.Empty;
                            obj.Relation = !String.IsNullOrEmpty(Convert.ToString(dr["RELATION"])) ? Convert.ToString(dr["RELATION"]) : String.Empty;
                            obj.TradeValue = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE"])) ? FormatHelper.FormatNumber(Convert.ToString(dr["VALUE"])) : String.Empty;
                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TransactionHistoryResponse objResponse = new TransactionHistoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion
    }
}