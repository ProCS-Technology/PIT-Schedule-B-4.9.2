using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class TradingRequestDetailsRepository : IRequiresSessionState
    {
        public TradingRequestDetailsResponse GetTradingRequestDetailsList(PreClearanceRequest objTradingRequestDetails)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objTradingRequestDetails.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_TRADINGREQUESTDETAILS_LIST");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@LOGIN_ID", objTradingRequestDetails.LoginId);

                TradingRequestDetailsResponse oGrp = new TradingRequestDetailsResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_TRADING_REQUEST_DETAILS", objTradingRequestDetails.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        PreClearanceRequest obj = new PreClearanceRequest();

                        obj.PreClearanceRequestId = !String.IsNullOrEmpty(Convert.ToString(rdr["ID"])) ? Convert.ToInt32(rdr["ID"]) : 0;
                        obj.PreClearanceRequestedForName = Convert.ToString(rdr["PRE_CLEARANCE_FOR_NAME"]);
                        obj.relationName = Convert.ToString(rdr["RELATION_NM"]);
                        obj.PreClearanceRequestedFor = !String.IsNullOrEmpty(Convert.ToString(rdr["PRE_CLEARANCE_REQUESTED_FOR"])) ? Convert.ToInt32(rdr["PRE_CLEARANCE_REQUESTED_FOR"]) : 0;
                        obj.SecurityTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE_NAME"]))) ? Convert.ToString(rdr["SECURITY_TYPE_NAME"]) : String.Empty;
                        obj.TradeCompanyName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_NAME"]))) ? Convert.ToString(rdr["TRADE_COMPANY_NAME"]) : String.Empty;
                        obj.TradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_QUANTITY"]))) ? Convert.ToString(rdr["TRADE_QUANTITY"]) : String.Empty;
                        obj.TransactionTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE_NAME"]))) ? Convert.ToString(rdr["TRANSACTION_TYPE_NAME"]) : String.Empty;
                        obj.TradeExchangeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_EXCHANGE_NAME"]))) ? Convert.ToString(rdr["TRADE_EXCHANGE_NAME"]) : String.Empty;
                        obj.DematAccount = (!String.IsNullOrEmpty(Convert.ToString(rdr["DEMAT_ACCOUNT"]))) ? Convert.ToString(rdr["DEMAT_ACCOUNT"]) : String.Empty;
                        obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_DATE"]))) ? Convert.ToString(rdr["TRADE_DATE"]) : String.Empty;
                        obj.SecurityType = !String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE"])) ? Convert.ToInt32(rdr["SECURITY_TYPE"]) : 0;
                        obj.TradeCompany = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_ID"])) ? Convert.ToInt32(rdr["TRADE_COMPANY_ID"]) : 0;
                        obj.TransactionType = !String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE"])) ? Convert.ToInt32(rdr["TRANSACTION_TYPE"]) : 0;
                        obj.TradeExchange = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_EXCHANGE"])) ? Convert.ToInt32(rdr["TRADE_EXCHANGE"]) : 0;
                        obj.Status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        obj.reviewedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_ON"])) ? Convert.ToString(rdr["REVIEWED_ON"]) : String.Empty;
                        obj.reviewedBy = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_BY"])) ? Convert.ToString(rdr["REVIEWED_BY"]) : String.Empty;
                        obj.reviewerRemarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_REMARKS"])) ? Convert.ToString(rdr["REVIEWED_REMARKS"]) : String.Empty;
                        obj.shareCurrentMarketPrice = !String.IsNullOrEmpty(Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"])) ? Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"]) : String.Empty;
                        obj.proposedTransactionThrough = !String.IsNullOrEmpty(Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"])) ? Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"]) : String.Empty;
                        obj.lstBrokerNoteUploaded = GetAllBrokerNoteUploaded(obj.PreClearanceRequestId, objTradingRequestDetails);

                        if (objTradingRequestDetails.Status != "All")
                        {
                            if (obj.Status == objTradingRequestDetails.Status)
                            {
                                oGrp.AddObject(obj);
                            }

                        }
                        else
                        {
                            oGrp.AddObject(obj);
                        }
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
                TradingRequestDetailsResponse oGrp = new TradingRequestDetailsResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

        private List<BrokerNoteModal> GetAllBrokerNoteUploaded(Int32 preclearanceRequestId, PreClearanceRequest objPreClearanceRequest)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Mode", "GET_ALL_BROKER_NOTE_UPLOADED");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", preclearanceRequestId);

            List<BrokerNoteModal> lstBrokerNoteModal = new List<BrokerNoteModal>();

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BrokerNoteModal obj = new BrokerNoteModal();
                        obj.brokerNoteId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                        obj.ActualTransactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["ACTUAL_TRANSACTION_DATE"])) ? Convert.ToString(dr["ACTUAL_TRANSACTION_DATE"]) : String.Empty;
                        obj.ActualTradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : String.Empty;
                        obj.ValuePerShare = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE_PER_SHARE"])) ? Convert.ToString(dr["VALUE_PER_SHARE"]) : String.Empty;
                        obj.TotalAmount = !String.IsNullOrEmpty(Convert.ToString(dr["TOTAL_AMOUNT"])) ? Convert.ToString(dr["TOTAL_AMOUNT"]) : String.Empty;
                        obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;
                        obj.BrokerNote = !String.IsNullOrEmpty(Convert.ToString(dr["BROKER_NOTE"])) ? Convert.ToString(dr["BROKER_NOTE"]) : String.Empty;
                        obj.isFormCDJCreated = IsFormCDJCreated(preclearanceRequestId, obj.brokerNoteId, objPreClearanceRequest);

                        lstBrokerNoteModal.Add(obj);
                    }
                }
            }

            return lstBrokerNoteModal;
        }

        private bool IsFormCDJCreated(Int32 preclearanceRequestId, Int32 brokerNoteId, PreClearanceRequest objPreClearanceRequest)
        {
            bool isFormCDJCreated = false;
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Mode", "IS_FORMS_CDJ_CREATED");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", preclearanceRequestId);
            parameters[3] = new SqlParameter("@BROKER_NOTE_ID", brokerNoteId);

            SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

            if (Convert.ToInt32(parameters[1].Value) > 0)
            {
                isFormCDJCreated = true;
            }

            return isFormCDJCreated;
        }
    }
}