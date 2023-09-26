using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.HtmlConverter;
using Syncfusion.OfficeChartToImageConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class PreClearanceRequestRepository : IRequiresSessionState
    {
        private static String connectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
        public PreClearanceRequestResponse GetTypeOfSecurity(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "Get_Security_Type_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                PreClearanceRequestResponse oSecurityType = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        SecurityType obj = new SecurityType();
                        obj.Id = Convert.ToInt32(rdr.GetValue(0));
                        obj.Name = Convert.ToString(rdr.GetValue(1));
                        obj.IsTradable = Convert.ToString(rdr.GetValue(2));
                        oSecurityType.AddObject(obj);
                    }
                    oSecurityType.StatusFl = true;
                    oSecurityType.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oSecurityType.StatusFl = false;
                    oSecurityType.Msg = "No data found !";
                }
                rdr.Close();
                return oSecurityType;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oSecurityType = new PreClearanceRequestResponse();
                oSecurityType.StatusFl = false;
                oSecurityType.Msg = "Processing failed, because of system error !";
                return oSecurityType;
            }
        }
        public PreClearanceRequestResponse GetTypeOfRestrictedCompanies(PreClearanceRequest objRestrictedCompanies)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "Get_Restricted_Companies_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objRestrictedCompanies.CompanyId);

                PreClearanceRequestResponse oRestrictedCompany = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objRestrictedCompanies.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        RestrictedCompanies obj = new RestrictedCompanies();
                        obj.ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.companyName = Convert.ToString(rdr.GetValue(1));
                        obj.IsHomeCompany = Convert.ToInt32(rdr.GetValue(2));
                        oRestrictedCompany.AddObject(obj);
                    }
                    oRestrictedCompany.StatusFl = true;
                    oRestrictedCompany.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oRestrictedCompany.StatusFl = false;
                    oRestrictedCompany.Msg = "No data found !";
                }
                rdr.Close();
                return oRestrictedCompany;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oRestrictedCompany = new PreClearanceRequestResponse();
                oRestrictedCompany.StatusFl = false;
                oRestrictedCompany.Msg = "Processing failed, because of system error !";
                return oRestrictedCompany;
            }
        }
        public PreClearanceRequestResponse GetTypeOfTransaction(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "Get_Transaction_Type_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                PreClearanceRequestResponse oTransactionType = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        TransactionType obj = new TransactionType();
                        obj.Id = Convert.ToInt32(rdr.GetValue(0));
                        obj.Name = Convert.ToString(rdr.GetValue(1));
                        obj.Nature = Convert.ToString(rdr.GetValue(2));
                        oTransactionType.AddObject(obj);
                    }
                    oTransactionType.StatusFl = true;
                    oTransactionType.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oTransactionType.StatusFl = false;
                    oTransactionType.Msg = "No data found !";
                }
                rdr.Close();
                return oTransactionType;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oTransactionType = new PreClearanceRequestResponse();
                oTransactionType.StatusFl = false;
                oTransactionType.Msg = "Processing failed, because of system error !";
                return oTransactionType;
            }
        }
        public PreClearanceRequestResponse GetTradeExchange(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "Get_Trade_Exchange_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                PreClearanceRequestResponse oTradeExchange = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        TradeExchange obj = new TradeExchange();
                        obj.Id = Convert.ToInt32(rdr.GetValue(0));
                        obj.Name = Convert.ToString(rdr.GetValue(1));
                        oTradeExchange.AddObject(obj);
                    }
                    oTradeExchange.StatusFl = true;
                    oTradeExchange.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oTradeExchange.StatusFl = false;
                    oTradeExchange.Msg = "No data found !";
                }
                rdr.Close();
                return oTradeExchange;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oTradeExchange = new PreClearanceRequestResponse();
                oTradeExchange.StatusFl = false;
                oTradeExchange.Msg = "Processing failed, because of system error !";
                return oTradeExchange;
            }
        }
        public PreClearanceRequestResponse GetDematAccount(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Mode", "Get_Demat_Account_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@RELATIVE_ID", objPreClearanceRequest.relativeId);
                parameters[4] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);

                PreClearanceRequestResponse oDematDetail = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        DematAccount obj = new DematAccount();
                        obj.accountNo = Convert.ToString(rdr.GetValue(0));
                        obj.CurrentHolding = Convert.ToString(rdr.GetValue(2));
                        oDematDetail.AddObject(obj);
                    }
                    oDematDetail.StatusFl = true;
                    oDematDetail.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oDematDetail.StatusFl = false;
                    oDematDetail.Msg = "No data found !";
                }
                rdr.Close();
                return oDematDetail;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oDematDetail = new PreClearanceRequestResponse();
                oDematDetail.StatusFl = false;
                oDematDetail.Msg = "Processing failed, because of system error !";
                return oDematDetail;
            }
        }
        public PreClearanceRequestResponse GetRelativeDetail(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "Get_Relative_Detail_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);


                PreClearanceRequestResponse oRelativeDetail = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Relative obj = new Relative();
                        obj.ID = Convert.ToInt32(rdr["RELATIVE_ID"]);
                        obj.relativeName = Convert.ToString(rdr["RELATIVE_NAME"]);
                        //obj.FirstName = Convert.ToString(rdr.GetValue(1));
                        //obj.MiddleName = Convert.ToString(rdr.GetValue(2));
                        //obj.LastName = Convert.ToString(rdr.GetValue(3));
                        oRelativeDetail.AddObject(obj);
                    }
                    oRelativeDetail.StatusFl = true;
                    oRelativeDetail.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oRelativeDetail.StatusFl = false;
                    oRelativeDetail.Msg = "No data found !";
                }
                rdr.Close();
                return oRelativeDetail;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oRelativeDetail = new PreClearanceRequestResponse();
                oRelativeDetail.StatusFl = false;
                oRelativeDetail.Msg = "Processing failed, because of system error !";
                return oRelativeDetail;
            }
        }
        public PreClearanceRequestResponse GetRelativeDetailBN(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "Get_Relative_Detail_List_BN");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);


                PreClearanceRequestResponse oRelativeDetail = new PreClearanceRequestResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                DataTable dtUser = ds.Tables[0];
                DataTable dtDemat = ds.Tables[1];

                if (dtUser.Rows.Count > 0)
                {
                    foreach (DataRow drUser in dtUser.Rows)
                    {
                        Relative obj = new Relative();
                        obj.ID = Convert.ToInt32(drUser["RELATIVE_ID"]);
                        obj.relativeName = Convert.ToString(drUser["RELATIVE_NM"]);
                        obj.panNumber = Convert.ToString(drUser["RELATIVE_PAN"]);
                        obj.LastTransactionDt = Convert.ToString(drUser["LAST_TRANS_DT"]);
                        DataRow[] drDemats = dtDemat.Select("RELATIVE_ID=" + Convert.ToInt32(drUser["RELATIVE_ID"]));
                        if (drDemats.Length > 0)
                        {
                            obj.lstDematAccount = new List<DematAccount>();
                            foreach (DataRow drDemat in drDemats)
                            {
                                obj.lstDematAccount.Add(new DematAccount
                                {
                                    ID = Convert.ToInt32(drUser["RELATIVE_ID"]),
                                    accountNo = Convert.ToString(drDemat["ACCOUNT_NO"]),
                                    CurrentHolding = Convert.ToString(Convert.ToInt32(drDemat["CURRENT_HOLDING"])- Convert.ToInt32(drDemat["PLEDGED"])),
                                    Pledged = Convert.ToString(drDemat["PLEDGED"])
                                });
                            }
                        }
                        oRelativeDetail.AddObject(obj);
                    }
                    oRelativeDetail.StatusFl = true;
                    oRelativeDetail.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oRelativeDetail.StatusFl = false;
                    oRelativeDetail.Msg = "No data found !";
                }
                return oRelativeDetail;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oRelativeDetail = new PreClearanceRequestResponse();
                oRelativeDetail.StatusFl = false;
                oRelativeDetail.Msg = "Processing failed, because of system error !";
                return oRelativeDetail;
            }
        }
        public PreClearanceRequestResponse AddPreClearanceRequest(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[19];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@TRADE_COMPANY_ID", objPreClearanceRequest.TradeCompany);
                parameters[3] = new SqlParameter("@TRADE_DATE", FormatHelper.ConvertDateTime(objPreClearanceRequest.TradeDate));
                parameters[4] = new SqlParameter("@STATUS", objPreClearanceRequest.Status);
                parameters[5] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[6] = new SqlParameter("@Mode", "CHECK");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@PRE_CLEARANCE_REQUESTED_FOR", objPreClearanceRequest.PreClearanceRequestedFor);
                parameters[9] = new SqlParameter("@TRADE_QUANTITY", objPreClearanceRequest.TradeQuantity);
                parameters[10] = new SqlParameter("@SECURITY_TYPE", objPreClearanceRequest.SecurityType);
                parameters[11] = new SqlParameter("@TRADE_EXCHANGE", objPreClearanceRequest.TradeExchange);
                parameters[12] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);
                parameters[13] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[14] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);
                parameters[15] = new SqlParameter("@SHARE_CURRENT_MARKET_PRICE", objPreClearanceRequest.shareCurrentMarketPrice);
                parameters[16] = new SqlParameter("@PROPOSED_TRANSACTION_THROUGH", objPreClearanceRequest.proposedTransactionThrough);
                parameters[17] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);
                parameters[18] = new SqlParameter("@AllowMultipleRequest", Convert.ToString(ConfigurationManager.AppSettings["AllowMultiPreclearanceRequest"]));

                DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();

                //if ((Int32)obj == 0)
                if (Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG"]) == "Success")
                {
                    parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                    parameters[6] = new SqlParameter("@Mode", "GET_ID_BY_PRE_CLEARANCE_REQUEST");
                    var PreClearanceRequestId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.PreClearanceRequestId = (Int32)PreClearanceRequestId;
                    parameters[13] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                    parameters[6] = new SqlParameter("@Mode", "GET_OBJECT_BY_ID");
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.BrokerNote = ds.Tables[0].Rows[0]["BROKER_NOTE"].ToString();
                    objPreClearanceRequest.ValuePerShare = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    objPreClearanceRequest.ActualTransactionDate = ds.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE"].ToString();
                    objPreClearanceRequest.isBrokerNoteUploaded = "N";
                    objPreClearanceRequest.reviewedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REVIEWED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REVIEWED_ON"]) : String.Empty;
                    objPreClearanceRequest.reviewedBy = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REVIEWED_BY"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REVIEWED_BY"]) : String.Empty;
                    objPreClearanceRequest.remarks = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REVIEWIEWER_REMARKS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REVIEWIEWER_REMARKS"]) : String.Empty;

                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been saved successfully !";
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;

                    if (objPreClearanceRequest.Status == "InApproval")
                    {
                        SendMailToApproverForAction(objPreClearanceRequest);
                    }
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    if (Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG"]) == "Already Exists")
                    {
                        oPreClearanceRequest.Msg = "Pre-Clearance Request aleady exists !";
                    }
                    else if (Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG"]) == "Contra Trade")
                    {
                        oPreClearanceRequest.Msg = "Pre-Clearance Request could not be saved as this would allow you to enter into a contra trade transaction !";
                    }
                    else if (Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG"]) == "Exceed Qty")
                    {
                        oPreClearanceRequest.Msg = "You cannot sell more equity than the current holding !";
                    }
                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = ex.Message.ToString();
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse UpdatePreClearanceRequest(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[17];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@TRADE_COMPANY_ID", objPreClearanceRequest.TradeCompany);
                parameters[3] = new SqlParameter("@STATUS", objPreClearanceRequest.Status);
                parameters[4] = new SqlParameter("@TRADE_DATE", FormatHelper.ConvertDateTime(objPreClearanceRequest.TradeDate));
                parameters[5] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[6] = new SqlParameter("@Mode", "CHECK");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@TRADE_QUANTITY", objPreClearanceRequest.TradeQuantity);
                parameters[9] = new SqlParameter("@SECURITY_TYPE", objPreClearanceRequest.SecurityType);
                parameters[10] = new SqlParameter("@TRADE_EXCHANGE", objPreClearanceRequest.TradeExchange);
                parameters[11] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);
                parameters[12] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[13] = new SqlParameter("@PRE_CLEARANCE_REQUESTED_FOR", objPreClearanceRequest.PreClearanceRequestedFor);
                parameters[14] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);
                parameters[15] = new SqlParameter("@SHARE_CURRENT_MARKET_PRICE", objPreClearanceRequest.shareCurrentMarketPrice);
                parameters[16] = new SqlParameter("@PROPOSED_TRANSACTION_THROUGH", objPreClearanceRequest.proposedTransactionThrough);



                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                if ((Int32)obj == 0)
                {
                    parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                    parameters[6] = new SqlParameter("@Mode", "GET_ID_BY_PRE_CLEARANCE_REQUEST");
                    var PreClearanceRequestId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.PreClearanceRequestId = (Int32)PreClearanceRequestId;
                    parameters[12] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                    parameters[6] = new SqlParameter("@Mode", "GET_OBJECT_BY_ID");
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.BrokerNote = ds.Tables[0].Rows[0]["BROKER_NOTE"].ToString();
                    objPreClearanceRequest.ValuePerShare = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    objPreClearanceRequest.ActualTransactionDate = ds.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE"].ToString();
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been updated successfully !";
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                    if (objPreClearanceRequest.Status == "InApproval")
                    {
                        SendMailToApproverForAction(objPreClearanceRequest);
                    }
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Pre-Clearance Request aleady exists !";
                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse AddPreClearanceRequest_for_other(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@TRADE_COMPANY_ID", objPreClearanceRequest.TradeCompany);
                parameters[3] = new SqlParameter("@TRADE_DATE", ConvertDate(objPreClearanceRequest.TradeDate));
                parameters[4] = new SqlParameter("@STATUS", objPreClearanceRequest.Status);
                parameters[5] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[6] = new SqlParameter("@Mode", "CHECK");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@PRE_CLEARANCE_REQUESTED_FOR", objPreClearanceRequest.PreClearanceRequestedFor);
                parameters[9] = new SqlParameter("@TRADE_QUANTITY", objPreClearanceRequest.TradeQuantity);
                parameters[10] = new SqlParameter("@SECURITY_TYPE", objPreClearanceRequest.SecurityType);
                parameters[11] = new SqlParameter("@TRADE_EXCHANGE", objPreClearanceRequest.TradeExchange);
                parameters[12] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);
                parameters[13] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[14] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                if ((Int32)obj == 0)
                {
                    parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);

                    parameters[6] = new SqlParameter("@Mode", "GET_ID_BY_PRE_CLEARANCE_REQUEST");
                    var PreClearanceRequestId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.PreClearanceRequestId = (Int32)PreClearanceRequestId;
                    parameters[13] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                    parameters[6] = new SqlParameter("@Mode", "GET_OBJECT_BY_ID");
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.BrokerNote = ds.Tables[0].Rows[0]["BROKER_NOTE"].ToString();
                    objPreClearanceRequest.ValuePerShare = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    objPreClearanceRequest.ActualTransactionDate = ds.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE"].ToString();


                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been saved successfully !";
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;

                    //if (objPreClearanceRequest.Status == "InApproval")
                    //{
                    //    SendMailToApproverForAction(objPreClearanceRequest);
                    //}
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Pre-Clearance Request aleady exists !";
                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse UpdatePreClearanceRequest_for_other(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@TRADE_COMPANY_ID", objPreClearanceRequest.TradeCompany);
                parameters[3] = new SqlParameter("@STATUS", objPreClearanceRequest.Status);
                parameters[4] = new SqlParameter("@TRADE_DATE", ConvertDate(objPreClearanceRequest.TradeDate));
                parameters[5] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[6] = new SqlParameter("@Mode", "CHECK");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@TRADE_QUANTITY", objPreClearanceRequest.TradeQuantity);
                parameters[9] = new SqlParameter("@SECURITY_TYPE", objPreClearanceRequest.SecurityType);
                parameters[10] = new SqlParameter("@TRADE_EXCHANGE", objPreClearanceRequest.TradeExchange);
                parameters[11] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);
                parameters[12] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[13] = new SqlParameter("@PRE_CLEARANCE_REQUESTED_FOR", objPreClearanceRequest.PreClearanceRequestedFor);
                parameters[14] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);



                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);
                var obj = parameters[7].Value;
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                if ((Int32)obj == 0)
                {
                    parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);

                    parameters[6] = new SqlParameter("@Mode", "GET_ID_BY_PRE_CLEARANCE_REQUEST");
                    var PreClearanceRequestId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.PreClearanceRequestId = (Int32)PreClearanceRequestId;
                    parameters[12] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                    parameters[6] = new SqlParameter("@Mode", "GET_OBJECT_BY_ID");
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);
                    objPreClearanceRequest.BrokerNote = ds.Tables[0].Rows[0]["BROKER_NOTE"].ToString();
                    objPreClearanceRequest.ValuePerShare = ds.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString();
                    objPreClearanceRequest.ActualTransactionDate = ds.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE"].ToString();
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been updated successfully !";
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                    //if (objPreClearanceRequest.Status == "InApproval")
                    //{
                    //    SendMailToApproverForAction(objPreClearanceRequest);
                    //}
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Pre-Clearance Request aleady exists !";
                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetPreClearanceRequest(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Mode", "GET_ALL_PRE_CLEARANCE_REQUEST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[4] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

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
                        //obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_DATE"]))) ? FormatHelper.FormatDate((rdr["TRADE_DATE"]).ToString()) : String.Empty;
                        obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_DATE"]))) ? rdr["TRADE_DATE"].ToString() : String.Empty;
                        obj.SecurityType = !String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE"])) ? Convert.ToInt32(rdr["SECURITY_TYPE"]) : 0;
                        obj.TradeCompany = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_ID"])) ? Convert.ToInt32(rdr["TRADE_COMPANY_ID"]) : 0;
                        obj.TransactionType = !String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE"])) ? Convert.ToInt32(rdr["TRANSACTION_TYPE"]) : 0;
                        obj.TradeExchange = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_EXCHANGE"])) ? Convert.ToInt32(rdr["TRADE_EXCHANGE"]) : 0;
                        obj.Status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        obj.reviewedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_ON"])) ? rdr["REVIEWED_ON"].ToString() : String.Empty;
                        //obj.reviewedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_ON"])) ? FormatHelper.FormatDate((rdr["REVIEWED_ON"]).ToString()) : String.Empty;
                        obj.reviewedBy = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_BY"])) ? Convert.ToString(rdr["REVIEWED_BY"]) : String.Empty;
                        obj.reviewerRemarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_REMARKS"])) ? Convert.ToString(rdr["REVIEWED_REMARKS"]) : String.Empty;
                        obj.shareCurrentMarketPrice = !String.IsNullOrEmpty(Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"])) ? Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"]) : String.Empty;
                        obj.proposedTransactionThrough = !String.IsNullOrEmpty(Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"])) ? Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"]) : String.Empty;
                        obj.userRole = !String.IsNullOrEmpty(Convert.ToString(rdr["USER_ROLE"])) ? Convert.ToString(rdr["USER_ROLE"]) : String.Empty;
                        obj.lstBrokerNoteUploaded = GetAllBrokerNoteUploaded(obj.PreClearanceRequestId, objPreClearanceRequest);
                        obj.tradingFrom = (!String.IsNullOrEmpty(Convert.ToString(rdr["FROM_DT"]))) ? rdr["FROM_DT"].ToString() : String.Empty;
                        obj.tradingTo = (!String.IsNullOrEmpty(Convert.ToString(rdr["TO_DT"]))) ? rdr["TO_DT"].ToString() : String.Empty;
                        //obj.tradingFrom = (!String.IsNullOrEmpty(Convert.ToString(rdr["FROM_DT"]))) ? FormatHelper.FormatDate((rdr["FROM_DT"]).ToString()) : String.Empty;
                        //obj.tradingTo = (!String.IsNullOrEmpty(Convert.ToString(rdr["TO_DT"]))) ? FormatHelper.FormatDate((rdr["TO_DT"]).ToString()) : String.Empty;
                        obj.RemainingTradeQuantity= !String.IsNullOrEmpty(Convert.ToString(rdr["Remaining_Trade_Quantity"])) ? Convert.ToInt32(rdr["Remaining_Trade_Quantity"]) : 0;
                        obj.TradeExecutedStatus = !String.IsNullOrEmpty(Convert.ToString(rdr["EXECUTED_STATUS"])) ? Convert.ToString(rdr["EXECUTED_STATUS"]) : String.Empty;
                        oPreClearanceRequest.AddObject(obj);
                    }
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "No data found !";
                }
                rdr.Close();
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
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
                        obj.ActualTransactionDate = !String.IsNullOrEmpty(Convert.ToString(dr["ACTUAL_TRANSACTION_DATE"])) ? FormatHelper.FormatDate(dr["ACTUAL_TRANSACTION_DATE"].ToString()) : String.Empty;
                        obj.ActualTradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? FormatHelper.FormatNumber(dr["TRADE_QUANTITY"].ToString()) : String.Empty;
                        obj.ValuePerShare = !String.IsNullOrEmpty(Convert.ToString(dr["VALUE_PER_SHARE"])) ? Convert.ToString(dr["VALUE_PER_SHARE"]) : String.Empty;
                        obj.TotalAmount = !String.IsNullOrEmpty(Convert.ToString(dr["TOTAL_AMOUNT"])) ? FormatHelper.FormatNumber(dr["TOTAL_AMOUNT"].ToString()) : String.Empty;
                        obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;
                        obj.BrokerNote = !String.IsNullOrEmpty(Convert.ToString(dr["BROKER_NOTE"])) ? Convert.ToString(dr["BROKER_NOTE"]) : String.Empty;
                        obj.isFormCDJCreated = IsFormCDJCreated(preclearanceRequestId, obj.brokerNoteId, objPreClearanceRequest);
                        obj.isNullTrade = Convert.ToBoolean(dr["IS_NULL_TRADE"]);
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
        public PreClearanceRequestResponse GetFormsCDJ(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "GET_FORMS_CDJ");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[4] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[5] = new SqlParameter("@BROKER_NOTE_ID", objPreClearanceRequest.brokerNoteId);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objPreClearanceRequest.Id = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                        objPreClearanceRequest.formUrl = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FORM_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["FORM_NAME"]) : String.Empty;
                    }
                }
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetPreClearanceRequestFilterByStatus(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_ALL_PRE_CLEARANCE_REQUEST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

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
                        obj.userRole = !String.IsNullOrEmpty(Convert.ToString(rdr["USER_ROLE"])) ? Convert.ToString(rdr["USER_ROLE"]) : String.Empty;
                        obj.lstBrokerNoteUploaded = GetAllBrokerNoteUploaded(obj.PreClearanceRequestId, objPreClearanceRequest);
                        if (objPreClearanceRequest.Status != "All")
                        {
                            if (obj.Status == objPreClearanceRequest.Status)
                            {
                                oPreClearanceRequest.AddObject(obj);
                            }

                        }
                        else
                        {
                            oPreClearanceRequest.AddObject(obj);
                        }
                    }
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "No data found !";
                }
                rdr.Close();
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetPreClearanceRequest_for_other(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_ALL_PRE_CLEARANCE_REQUEST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST_FOR_OTHERS", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        PreClearanceRequest obj = new PreClearanceRequest();
                        obj.PreClearanceRequestId = Convert.ToInt32(rdr.GetValue(0));
                        obj.PreClearanceRequestedForName = Convert.ToInt32(rdr.GetValue(1)) == 0 ? "Self" : ((!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(14)))) ? Convert.ToString(rdr.GetValue(14)) : String.Empty);
                        obj.PreClearanceRequestedFor = Convert.ToInt32(rdr.GetValue(1));
                        obj.SecurityTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(2)))) ? Convert.ToString(rdr.GetValue(2)) : String.Empty;
                        obj.TradeCompanyName = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(3)))) ? Convert.ToString(rdr.GetValue(3)) : String.Empty;
                        obj.TradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(4)))) ? Convert.ToString(rdr.GetValue(4)) : String.Empty;
                        obj.TransactionTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(5)))) ? Convert.ToString(rdr.GetValue(5)) : String.Empty;
                        obj.TradeExchangeName = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(6)))) ? Convert.ToString(rdr.GetValue(6)) : String.Empty;
                        obj.DematAccount = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(7)))) ? Convert.ToString(rdr.GetValue(7)) : String.Empty;
                        obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(8)))) ? Convert.ToString(rdr.GetValue(8)) : String.Empty;
                        obj.SecurityType = Convert.ToInt32(rdr.GetValue(9));
                        obj.TradeCompany = Convert.ToInt32(rdr.GetValue(10));
                        obj.TransactionType = Convert.ToInt32(rdr.GetValue(11));
                        obj.TradeExchange = Convert.ToInt32(rdr.GetValue(12));
                        obj.Status = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(13)))) ? Convert.ToString(rdr.GetValue(13)) : String.Empty;
                        obj.BrokerNote = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(17)))) ? Convert.ToString(rdr.GetValue(17)) : String.Empty;
                        obj.ValuePerShare = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(18)))) ? Convert.ToString(rdr.GetValue(18)) : String.Empty;
                        obj.TotalAmount = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(19)))) ? Convert.ToString(rdr.GetValue(19)) : String.Empty;
                        obj.ActualTransactionDate = (!String.IsNullOrEmpty(Convert.ToString(rdr.GetValue(20)))) ? Convert.ToString(rdr.GetValue(20)) : String.Empty;

                        oPreClearanceRequest.AddObject(obj);
                    }
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "No data found !";
                }
                rdr.Close();
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse AddUpdateBrokerNote(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                DataTable dtMultiTradeBN = new DataTable();
                dtMultiTradeBN.Columns.Add("TRADE_QUANTITY", typeof(int));
                dtMultiTradeBN.Columns.Add("VALUE_PER_SHARE", typeof(String));
                dtMultiTradeBN.Columns.Add("TOTAL_AMOUNT", typeof(String));
                dtMultiTradeBN.Columns.Add("ACTUAL_TRANSACTION_DATE", typeof(String));
                dtMultiTradeBN.Columns.Add("EXCHANGE_TRADED_ON", typeof(String));

                if (objPreClearanceRequest.lstMultiTrade != null)
                {
                    foreach (BrokerNoteModal MultiTrade in objPreClearanceRequest.lstMultiTrade)
                    {
                        DataRow dr = dtMultiTradeBN.NewRow();
                        dr["TRADE_QUANTITY"] = MultiTrade.PartialTradeQuantity;
                        dr["VALUE_PER_SHARE"] = MultiTrade.PartialValuePerShare;
                        dr["TOTAL_AMOUNT"] = MultiTrade.PartialTotalAmount;
                        dr["ACTUAL_TRANSACTION_DATE"] = FormatHelper.FormatDate(MultiTrade.PartialTradeDate);
                        dr["EXCHANGE_TRADED_ON"] = MultiTrade.PartialExchangeTradedOn;
                        dtMultiTradeBN.Rows.Add(dr);
                    }
                }


                SqlParameter[] parameters = new SqlParameter[16];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@BROKER_NOTE", objPreClearanceRequest.BrokerNote);
                parameters[3] = new SqlParameter("@VALUE_PER_SHARE", objPreClearanceRequest.ValuePerShare);
                parameters[4] = new SqlParameter("@TOTAL_AMOUNT", objPreClearanceRequest.TotalAmount);
                if (objPreClearanceRequest.ActualTransactionDate != null)
                {
                    parameters[5] = new SqlParameter("@ACTUAL_TRANSACTION_DATE", FormatHelper.FormatDate(objPreClearanceRequest.ActualTransactionDate));
                }
                parameters[6] = new SqlParameter("@Mode", "ADD_UPDATE_BROKER_NOTE");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[9] = new SqlParameter("@ACTUAL_TRADE_QUANTITY", objPreClearanceRequest.ActualTradeQuantity);
                parameters[10] = new SqlParameter("@REMARKS", objPreClearanceRequest.remarks);
                if (objPreClearanceRequest.isNUllTrade)
                {
                    parameters[11] = new SqlParameter("@IS_NULL_TRADE", "true");
                    parameters[12] = new SqlParameter("@NULL_TRADE_REMARKS", objPreClearanceRequest.nullTradeRemarks);
                }
                else
                {
                    parameters[11] = new SqlParameter("@IS_NULL_TRADE", "false");
                    parameters[12] = new SqlParameter("@NULL_TRADE_REMARKS", objPreClearanceRequest.nullTradeRemarks);
                }
                parameters[13] = new SqlParameter("@EXCHANGE_TRADED_ON", objPreClearanceRequest.exchangeTradedOn);
                parameters[14] = new SqlParameter("@MultiTradeLine", dtMultiTradeBN == null ? (object)DBNull.Value : dtMultiTradeBN);
                parameters[15] = new SqlParameter("@BROKER_DETAILS", objPreClearanceRequest.BrokerDetails);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (!string.IsNullOrEmpty(Convert.ToString(parameters[7].Value)))
                {
                    objPreClearanceRequest.brokerNoteId = Convert.ToInt32(parameters[7].Value);
                }


                string subEvent = "";
                string mainEvent = string.Empty;

                if (!objPreClearanceRequest.isNUllTrade)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["EXECUTED_STATUS"]) == "Closed")
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                            {
                                mainEvent = "Pre-clearance Transaction Completed";
                                subEvent = "Both";
                            }
                            else
                            {
                                mainEvent = "Pre-clearance Transaction Completed";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                            {
                                mainEvent = "Pre-clearance Transaction With FormC";
                            }
                            else
                            {

                            }
                        }
                    }
                }
                else
                {
                    mainEvent = "Non Trade with Preclearance";
                    subEvent = "";
                }

                if (!string.IsNullOrEmpty(mainEvent))
                {
                    List<EventBasedForm> lstAllFormEvents = GetAllFormEvents(objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.LoginId, Convert.ToString(objPreClearanceRequest.brokerNoteId), mainEvent, subEvent);
                    List<string> allAttachments = new List<string>();

                    foreach (var obj in lstAllFormEvents)
                    {
                        obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";

                        if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                        {
                            string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                            if (!String.IsNullOrEmpty(docFileName))
                            {
                                String filePath = "/InsiderTrading/emailAttachment/";
                                ConvertDocToPDF(docFileName, obj.fileName, filePath);
                                CreateFormLogs(obj.formId, Convert.ToString(objPreClearanceRequest.brokerNoteId), obj.fileName, objPreClearanceRequest.LoginId, objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE);
                            }
                        }
                        else
                        {
                            obj.fileName = string.Empty;
                        }
                        if (!String.IsNullOrEmpty(obj.fileName))
                        {
                            allAttachments.Add(obj.fileName);
                            objPreClearanceRequest.lstFormUrl = allAttachments;

                            bool status = true;
                            InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                                objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.CompanyId, objPreClearanceRequest.LoginId,
                                objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.businessUnitId
                            );
                            String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                                ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                            List<string> attachments = new List<string>();
                            if (objPreClearanceRequest.lstFormUrl != null)
                            {
                                if (objPreClearanceRequest.lstFormUrl.Count > 0)
                                {
                                    foreach (string formUrl in objPreClearanceRequest.lstFormUrl)
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                                    }
                                }
                            }
                            String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                            EmailSender.SendMail(
                                objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                                "Trade disclosure", objPreClearanceRequest.CompanyId.ToString(), strCC,
                                objPreClearanceRequest.brokerNoteId.ToString(), objPreClearanceRequest.LoginId
                            );
                            //EmailHelper email = new EmailHelper();
                            //status = email.SendInsiderFormToCO(objPreClearanceRequest);
                            oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;

                            if (status)
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been shared with your Compliance Officer!";
                            }
                            else
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Sending mail failed.";
                            }
                        }
                    }
                    if (subEvent == "Both" || mainEvent == "Pre-clearance Transaction With FormC")
                    {
                        SqlParameter[] parameters1 = new SqlParameter[1];
                        parameters1[0] = new SqlParameter("@BrokerNoteId", objPreClearanceRequest.brokerNoteId);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORMC_LOGS", objPreClearanceRequest.MODULE_DATABASE, parameters1);
                    }
                }
                else
                {
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Trade details updated successfully !";

                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objPreClearanceRequest.LoginId, 5, objPreClearanceRequest.CompanyId);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse SaveBenposTradeTransaction(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                DataTable dtMultiTradeBN = new DataTable();
                dtMultiTradeBN.Columns.Add("TRADE_QUANTITY", typeof(int));
                dtMultiTradeBN.Columns.Add("VALUE_PER_SHARE", typeof(String));
                dtMultiTradeBN.Columns.Add("TOTAL_AMOUNT", typeof(String));
                dtMultiTradeBN.Columns.Add("ACTUAL_TRANSACTION_DATE", typeof(String));
                dtMultiTradeBN.Columns.Add("EXCHANGE_TRADED_ON", typeof(String));

                if (objPreClearanceRequest.lstMultiTrade != null)
                {
                    foreach (BrokerNoteModal MultiTrade in objPreClearanceRequest.lstMultiTrade)
                    {
                        DataRow dr = dtMultiTradeBN.NewRow();
                        dr["TRADE_QUANTITY"] = MultiTrade.PartialTradeQuantity;
                        dr["VALUE_PER_SHARE"] = MultiTrade.PartialValuePerShare;
                        dr["TOTAL_AMOUNT"] = MultiTrade.PartialTotalAmount;
                        dr["ACTUAL_TRANSACTION_DATE"] = FormatHelper.FormatDate(MultiTrade.PartialTradeDate);
                        dr["EXCHANGE_TRADED_ON"] = MultiTrade.PartialExchangeTradedOn;
                        dtMultiTradeBN.Rows.Add(dr);
                    }
                }

                SqlParameter[] parameters = new SqlParameter[18];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@BROKER_NOTE", objPreClearanceRequest.BrokerNote);
                parameters[3] = new SqlParameter("@VALUE_PER_SHARE", objPreClearanceRequest.ValuePerShare);
                parameters[4] = new SqlParameter("@TOTAL_AMOUNT", objPreClearanceRequest.TotalAmount);
                if (objPreClearanceRequest.ActualTransactionDate != null)
                {
                    parameters[5] = new SqlParameter("@ACTUAL_TRANSACTION_DATE", FormatHelper.FormatDate(objPreClearanceRequest.ActualTransactionDate));
                }
                parameters[6] = new SqlParameter("@Mode", "ADD_BENPOS_TRADE_TRANSACTIONS");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[9] = new SqlParameter("@ACTUAL_TRADE_QUANTITY", objPreClearanceRequest.ActualTradeQuantity);
                parameters[10] = new SqlParameter("@REMARKS", objPreClearanceRequest.remarks);
                if (objPreClearanceRequest.isNUllTrade)
                {
                    parameters[11] = new SqlParameter("@IS_NULL_TRADE", "true");
                    parameters[12] = new SqlParameter("@NULL_TRADE_REMARKS", objPreClearanceRequest.nullTradeRemarks);
                }
                else
                {
                    parameters[11] = new SqlParameter("@IS_NULL_TRADE", "false");
                    parameters[12] = new SqlParameter("@NULL_TRADE_REMARKS", objPreClearanceRequest.nullTradeRemarks);
                }
                parameters[13] = new SqlParameter("@EXCHANGE_TRADED_ON", objPreClearanceRequest.exchangeTradedOn);
                parameters[14] = new SqlParameter("@MultiTradeLine", dtMultiTradeBN == null ? (object)DBNull.Value : dtMultiTradeBN);
                parameters[15] = new SqlParameter("@BROKER_DETAILS", objPreClearanceRequest.BrokerDetails);
                parameters[16] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);
                parameters[17] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (!string.IsNullOrEmpty(Convert.ToString(parameters[7].Value)))
                {
                    objPreClearanceRequest.brokerNoteId = Convert.ToInt32(parameters[7].Value);
                }


                /*string subEvent = "";
                string mainEvent = string.Empty;

                if (!objPreClearanceRequest.isNUllTrade)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["EXECUTED_STATUS"]) == "Closed")
                        {

                            if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                            {
                                mainEvent = "Pre-clearance Transaction Completed";
                                subEvent = "Both";
                            }
                            else
                            {
                                mainEvent = "Pre-clearance Transaction Completed";
                            }
                        }
                        else
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                            {
                                mainEvent = "Pre-clearance Transaction With FormC";
                            }
                            else
                            {

                            }
                        }
                    }
                }
                else
                {
                    mainEvent = "Non Trade with Preclearance";
                    subEvent = "";
                }

                if (!string.IsNullOrEmpty(mainEvent))
                {
                    List<EventBasedForm> lstAllFormEvents = GetAllFormEvents(objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.LoginId, Convert.ToString(objPreClearanceRequest.brokerNoteId), mainEvent, subEvent);
                    List<string> allAttachments = new List<string>();

                    foreach (var obj in lstAllFormEvents)
                    {
                        obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";

                        if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                        {
                            string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                            if (!String.IsNullOrEmpty(docFileName))
                            {
                                String filePath = "/InsiderTrading/emailAttachment/";
                                ConvertDocToPDF(docFileName, obj.fileName, filePath);
                                CreateFormLogs(obj.formId, Convert.ToString(objPreClearanceRequest.brokerNoteId), obj.fileName, objPreClearanceRequest.LoginId, objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE);
                            }
                        }
                        else
                        {
                            obj.fileName = string.Empty;
                        }
                        if (!String.IsNullOrEmpty(obj.fileName))
                        {
                            allAttachments.Add(obj.fileName);
                            objPreClearanceRequest.lstFormUrl = allAttachments;

                            bool status = true;
                            InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                                objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.CompanyId, objPreClearanceRequest.LoginId,
                                objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.businessUnitId
                            );
                            String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                                ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                            List<string> attachments = new List<string>();
                            if (objPreClearanceRequest.lstFormUrl != null)
                            {
                                if (objPreClearanceRequest.lstFormUrl.Count > 0)
                                {
                                    foreach (string formUrl in objPreClearanceRequest.lstFormUrl)
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                                    }
                                }
                            }
                            String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                            EmailSender.SendMail(
                                objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                                "Trade disclosure", objPreClearanceRequest.CompanyId.ToString(), strCC,
                                objPreClearanceRequest.brokerNoteId.ToString(), objPreClearanceRequest.LoginId
                            );
                            //EmailHelper email = new EmailHelper();
                            //status = email.SendInsiderFormToCO(objPreClearanceRequest);
                            oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;

                            if (status)
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been shared with your Compliance Officer!";
                            }
                            else
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Sending mail failed.";
                            }
                        }
                    }
                    if (subEvent == "Both" || mainEvent == "Pre-clearance Transaction With FormC")
                    {
                        SqlParameter[] parameters1 = new SqlParameter[1];
                        parameters1[0] = new SqlParameter("@BrokerNoteId", objPreClearanceRequest.brokerNoteId);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORMC_LOGS", objPreClearanceRequest.MODULE_DATABASE, parameters1);
                    }
                }
                else
                {*/
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Trade details updated successfully !";

                //}
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objPreClearanceRequest.LoginId, 5, objPreClearanceRequest.CompanyId);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse AddBrokerNoteWithNoPC(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[21];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@BROKER_NOTE", objPreClearanceRequest.BrokerNote);
                parameters[3] = new SqlParameter("@VALUE_PER_SHARE", objPreClearanceRequest.ValuePerShare);
                parameters[4] = new SqlParameter("@TOTAL_AMOUNT", objPreClearanceRequest.TotalAmount);
                parameters[5] = new SqlParameter("@ACTUAL_TRANSACTION_DATE", FormatHelper.FormatDate(objPreClearanceRequest.ActualTransactionDate));
                parameters[6] = new SqlParameter("@Mode", "ADD_BROKER_NOTE_WITH_NO_PC");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[9] = new SqlParameter("@ACTUAL_TRADE_QUANTITY", objPreClearanceRequest.ActualTradeQuantity);
                parameters[10] = new SqlParameter("@REMARKS", objPreClearanceRequest.remarks);
                parameters[11] = new SqlParameter("@RELATIVE_ID", objPreClearanceRequest.relativeId);
                parameters[12] = new SqlParameter("@PRE_CLEARANCE_REQUESTED_FOR", objPreClearanceRequest.relativeId);
                parameters[13] = new SqlParameter("@SECURITY_TYPE", objPreClearanceRequest.SecurityType);
                parameters[14] = new SqlParameter("@TRADE_COMPANY_ID", objPreClearanceRequest.TradeCompany);
                parameters[15] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);
                parameters[16] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);
                parameters[17] = new SqlParameter("@SHARE_CURRENT_MARKET_PRICE", objPreClearanceRequest.shareCurrentMarketPrice);
                parameters[18] = new SqlParameter("@PROPOSED_TRANSACTION_THROUGH", objPreClearanceRequest.proposedTransactionThrough);
                parameters[19] = new SqlParameter("@EXCHANGE_TRADED_ON", objPreClearanceRequest.exchangeTradedOn);
                parameters[20] = new SqlParameter("@BROKER_DETAILS", objPreClearanceRequest.BrokerDetails);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objPreClearanceRequest.PreClearanceRequestId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PRE_CLEARANCE_REQUEST_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["PRE_CLEARANCE_REQUEST_ID"]) : 0;
                        objPreClearanceRequest.brokerNoteId = Convert.ToInt32(parameters[7].Value);


                    }
                }
                string mainEvent = "";
                string subEvent = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["EXECUTED_STATUS"]) == "Closed")
                    {

                        if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                        {
                            mainEvent = "Without Pre-clearance Transaction Completed";
                            subEvent = "Both";
                        }
                        else
                        {
                            mainEvent = "Without Pre-clearance Transaction Completed";
                        }
                    }
                    else
                    {

                    }

                }

                if (!string.IsNullOrEmpty(mainEvent))
                {


                    List<EventBasedForm> lstAllFormEvents = GetAllFormEvents(objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.LoginId, Convert.ToString(objPreClearanceRequest.brokerNoteId), mainEvent, subEvent);
                    List<string> allAttachments = new List<string>();

                    foreach (var obj in lstAllFormEvents)
                    {
                        obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";

                        if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                        {
                            string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                            if (!String.IsNullOrEmpty(docFileName))
                            {
                                String filePath = "/InsiderTrading/emailAttachment/";
                                ConvertDocToPDF(docFileName, obj.fileName, filePath);
                                CreateFormLogs(obj.formId, Convert.ToString(objPreClearanceRequest.brokerNoteId), obj.fileName, objPreClearanceRequest.LoginId, objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE);
                            }
                        }
                        else
                        {
                            obj.fileName = string.Empty;
                        }
                        if (!String.IsNullOrEmpty(obj.fileName))
                        {
                            allAttachments.Add(obj.fileName);
                            objPreClearanceRequest.lstFormUrl = allAttachments;

                            bool status = true;
                            InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                                objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.CompanyId, objPreClearanceRequest.LoginId,
                                objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.businessUnitId
                            );
                            String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                                ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                            List<string> attachments = new List<string>();
                            if (objPreClearanceRequest.lstFormUrl != null)
                            {
                                if (objPreClearanceRequest.lstFormUrl.Count > 0)
                                {
                                    foreach (string formUrl in objPreClearanceRequest.lstFormUrl)
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                                    }
                                }
                            }
                            String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                            EmailSender.SendMail(
                                objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                                "Trade disclosure", objPreClearanceRequest.CompanyId.ToString(), strCC,
                                objPreClearanceRequest.brokerNoteId.ToString(), objPreClearanceRequest.LoginId
                            );
                            //EmailHelper email = new EmailHelper();
                            //status = email.SendInsiderFormToCO(objPreClearanceRequest);
                            oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                            if (status)
                            {

                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been shared with your Compliance Officer!";
                            }
                            else
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Sending mail failed.";
                            }
                        }
                    }
                    if (subEvent == "Both")
                    {
                        SqlParameter[] parameters1 = new SqlParameter[1];
                        parameters1[0] = new SqlParameter("@BrokerNoteId", objPreClearanceRequest.brokerNoteId);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORMC_LOGS", objPreClearanceRequest.MODULE_DATABASE, parameters1);
                    }
                }
                else
                {
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Trade details updated successfully !";

                }

                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetFormsInfo(PreClearanceRequest objRequest)
        {
            try
            {
                List<string> allAttachments = new List<string>();
                List<EventBasedForm> lstAllFormEvents = null;

                String _sql = "SELECT A.IS_NULL_TRADE,ISNULL(CONVERT(VARCHAR,B.TRADE_DATE,103)," +
                    "'') AS TRADE_DATE FROM PROCS_INSIDER_BROKER_NOTE(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_PRE_CLEARANCE_REQUEST(NOLOCK) B ON A.ACTION_ID=B.ID " +
                "WHERE A.ID=" + Convert.ToString(objRequest.brokerNoteId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objRequest.MODULE_DATABASE, null);
                String sTradeDt = Convert.ToString(ds.Tables[0].Rows[0]["TRADE_DATE"]);
                bool bNullTrade = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_NULL_TRADE"]);
                objRequest.isNUllTrade = bNullTrade;

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objRequest.LoginId);
                parameters[2] = new SqlParameter("@TRANS_DATE", FormatHelper.ConvertDateTime(ds.Tables[0].Rows[0]["TRADE_DATE"].ToString()));
                parameters[3] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objRequest.PreClearanceRequestId);

                DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "CHECK_TRADE_FOR_FORMC", objRequest.MODULE_DATABASE, parameters);
                string subEvent = string.Empty;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string IsExceededLimit = Convert.ToString(ds1.Tables[0].Rows[0]["EXCEEDED_LIMIT"]);
                    if (IsExceededLimit == "Y")
                    {

                        subEvent = "D/P";
                    }
                }

                if (!String.IsNullOrEmpty(sTradeDt) && !bNullTrade)
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", subEvent);
                }
                else if (!String.IsNullOrEmpty(sTradeDt) && bNullTrade)
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Non Trade with Preclearance", "Non Trade");
                }
                else
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", "Trade");
                }

                objRequest.lstFormUrl = new List<string>();
                foreach (EventBasedForm form in lstAllFormEvents)
                {
                    objRequest.lstFormUrl.Add(Convert.ToString(form.formId) + "~" + form.formName);
                }

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.PreClearanceRequest = objRequest;
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Form has been downloaded successfully";
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetTransactionalForms(PreClearanceRequest objRequest)
        {
            try
            {
                List<string> allAttachments = new List<string>();
                List<EventBasedForm> lstAllFormEvents = null;

                String _sql = "SELECT A.IS_NULL_TRADE,ISNULL(CONVERT(VARCHAR,B.TRADE_DATE,103)," +
                    "'') AS TRADE_DATE FROM PROCS_INSIDER_BROKER_NOTE(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_PRE_CLEARANCE_REQUEST(NOLOCK) B ON A.ACTION_ID=B.ID " +
                "WHERE A.ID=" + Convert.ToString(objRequest.brokerNoteId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objRequest.MODULE_DATABASE, null);
                String sTradeDt = Convert.ToString(ds.Tables[0].Rows[0]["TRADE_DATE"]);
                bool bNullTrade = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_NULL_TRADE"]);
                
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objRequest.LoginId);
                parameters[2] = new SqlParameter("@TRANS_DATE", FormatHelper.ConvertDateTime(ds.Tables[0].Rows[0]["TRADE_DATE"].ToString()));
                parameters[3] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objRequest.PreClearanceRequestId);
                
                DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "CHECK_TRADE_FOR_FORMC", objRequest.MODULE_DATABASE, parameters);
               
                string subEvent = "";


                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string IsExceededLimit = Convert.ToString(ds1.Tables[0].Rows[0]["EXCEEDED_LIMIT"]);
                    if (IsExceededLimit == "Y")
                    {

                        subEvent = "D/P";
                    }
                }
                
                
                

                if (!String.IsNullOrEmpty(sTradeDt) && !bNullTrade)
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", subEvent);
                }
                else if (!String.IsNullOrEmpty(sTradeDt) && bNullTrade)
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Non Trade with Preclearance", "Non Trade");
                }
                else
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", subEvent);
                }

                foreach (var obj in lstAllFormEvents)
                {
                    obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";
                    if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                    {
                        string docFileName = CreateWordDocument(obj.formTemplate, obj.fileName, obj.formOrientation);
                        //string docFileName = CreateDocFileLandscape(obj.formTemplate, obj.fileName);
                        if (!String.IsNullOrEmpty(docFileName))
                        {
                            String filePath = "/InsiderTrading/emailAttachment/";
                            ConvertDocToPDF(docFileName, obj.fileName, filePath);
                        }
                    }
                    else
                    {
                        obj.fileName = String.Empty;
                    }
                    if (!String.IsNullOrEmpty(obj.fileName))
                    {
                        string sFileNm = obj.fileName.Replace(".pdf", ".docx");
                        obj.fileName = sFileNm;
                        allAttachments.Add("/InsiderTrading/emailAttachment/" + obj.fileName);
                    }
                }
                PreClearanceRequest objPreclearanceRequest = new PreClearanceRequest();
                objPreclearanceRequest.lstFormUrl = allAttachments;

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.PreClearanceRequest = objPreclearanceRequest;
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Form has been downloaded successfully";
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse SaveAndGetTransactionalForms(PreClearanceRequest objRequest)
        {
            try
            {
                List<string> allAttachments = new List<string>();
                List<EventBasedForm> lstAllFormEvents = null;
                List<EventBasedForm> lstAllEmailEvents = null;

                String _sql = "SELECT A.IS_NULL_TRADE,ISNULL(CONVERT(VARCHAR,B.TRADE_DATE,103)," +
                    "'') AS TRADE_DATE FROM PROCS_INSIDER_BROKER_NOTE(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_PRE_CLEARANCE_REQUEST(NOLOCK) B ON A.ACTION_ID=B.ID " +
                "WHERE A.ID=" + Convert.ToString(objRequest.brokerNoteId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objRequest.MODULE_DATABASE, null);
                String sTradeDt = Convert.ToString(ds.Tables[0].Rows[0]["TRADE_DATE"]);
                bool bNullTrade = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_NULL_TRADE"]);

                _sql = "SELECT [dbo].[CHECK_OVER_TRADE](A.ACTUAL_TRANSACTION_DATE,A.COMPANY_ID,A.LOGIN_ID,A.ID) AS ExceededLimit " +
                    "FROM PROCS_INSIDER_PRE_CLEARANCE_REQUEST(NOLOCK) A WHERE A.ID=" + Convert.ToString(objRequest.PreClearanceRequestId);

                ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objRequest.MODULE_DATABASE, null);
                string IsExceededLimit = Convert.ToString(ds.Tables[0].Rows[0]["ExceededLimit"]);

                string subEvent = "";

                if (IsExceededLimit == "Y")
                {

                    subEvent = "D/P";
                }

                if (!String.IsNullOrEmpty(sTradeDt) && !bNullTrade)
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", subEvent);
                }
                else if (!String.IsNullOrEmpty(sTradeDt) && bNullTrade)
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Non Trade with Preclearance", "Non Trade");
                }
                else
                {
                    lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", subEvent);
                }

                foreach (var obj in lstAllFormEvents)
                {
                    obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";

                    if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                    {
                        string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                        if (!String.IsNullOrEmpty(docFileName))
                        {
                            String filePath = "/InsiderTrading/emailAttachment/";
                            ConvertDocToPDF(docFileName, obj.fileName, filePath);
                            CreateFormLogs(obj.formId, Convert.ToString(objRequest.brokerNoteId), obj.fileName, objRequest.LoginId, objRequest.CompanyId, objRequest.MODULE_DATABASE);
                        }
                    }
                    else
                    {
                        obj.fileName = string.Empty;
                    }
                    if (!String.IsNullOrEmpty(obj.fileName))
                    {
                        allAttachments.Add(obj.fileName);
                    }
                }
                objRequest.lstFormUrl = allAttachments;

                bool status = true;
                InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                    objRequest.MODULE_DATABASE, objRequest.CompanyId, objRequest.LoginId,
                    objRequest.ADMIN_DATABASE, objRequest.businessUnitId
                );
                String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                    ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                List<string> attachments = new List<string>();
                if (objRequest.lstFormUrl != null)
                {
                    if (objRequest.lstFormUrl.Count > 0)
                    {
                        foreach (string formUrl in objRequest.lstFormUrl)
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                        }
                    }
                }
                String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                EmailSender.SendMail(
                    objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                    "Trade disclosure", objRequest.CompanyId.ToString(), strCC,
                    objRequest.brokerNoteId.ToString(), objRequest.LoginId
                );
                //EmailHelper email = new EmailHelper();
                //status = email.SendInsiderFormToCO(objRequest);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                if (status)
                {
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been shared with your Compliance Officer!";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Something went wrong. Please try again or contact administrator!";
                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }

        }
        public PreClearanceRequestResponse SubmitCustomForm(PreClearanceRequest objRequest)
        {
            try
            {
                List<EventBasedForm> lstAllFormEvents = null;
                List<EventBasedForm> lstAllEmailEvents = null;

                if (objRequest.PreClearanceRequestId != 0)
                {
                    if (objRequest.formType == "FORM_CJ")
                    {
                        lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", "FORM_C");
                        lstAllEmailEvents = GetAllEmailEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", "FORM_C");
                    }
                    else if (objRequest.formType == "FORM_DJ")
                    {
                        lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", "FORM_D");
                        lstAllEmailEvents = GetAllEmailEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", "FORM_D");
                    }
                    else
                    {
                        lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", null);
                        lstAllEmailEvents = GetAllEmailEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction With Pre-clearance", null);
                    }
                }
                else
                {
                    if (objRequest.formType == "FORM_CJ")
                    {
                        lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", "FORM_C");
                        lstAllEmailEvents = GetAllEmailEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", "FORM_C");
                    }
                    else if (objRequest.formType == "FORM_DJ")
                    {
                        lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", "FORM_D");
                        lstAllEmailEvents = GetAllEmailEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", "FORM_D");
                    }
                    else
                    {
                        lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", null);
                        lstAllEmailEvents = GetAllEmailEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Transaction Without Pre-clearance", null);
                    }
                }

                foreach (var obj in lstAllFormEvents)
                {
                    foreach (string formUrl in objRequest.lstFormUrl)
                    {
                        if (obj.formName == formUrl.Split('_')[0])
                        {
                            CreateFormLogs(obj.formId, Convert.ToString(objRequest.brokerNoteId), formUrl, objRequest.LoginId, objRequest.CompanyId, objRequest.MODULE_DATABASE);
                        }
                    }

                }

                foreach (var obj in lstAllEmailEvents)
                {
                    objRequest.emailBody = obj.formTemplate;
                }

                bool status = true;
                InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                    objRequest.MODULE_DATABASE, objRequest.CompanyId, objRequest.LoginId,
                    objRequest.ADMIN_DATABASE, objRequest.businessUnitId
                );
                String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                    ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                List<string> attachments = new List<string>();
                if (objRequest.lstFormUrl != null)
                {
                    if (objRequest.lstFormUrl.Count > 0)
                    {
                        foreach (string formUrl in objRequest.lstFormUrl)
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                        }
                    }
                }
                String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                EmailSender.SendMail(
                    objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                    "Trade disclosure", objRequest.CompanyId.ToString(), strCC,
                    objRequest.brokerNoteId.ToString(), objRequest.LoginId
                );

                //EmailHelper email = new EmailHelper();
                //status = email.SendInsiderFormToCO(objRequest);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                if (status)
                {
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been also shared on your email id for reference!";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Something went wrong. Please try again or contact administrator!";
                }

                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public TradeTransactionResponse GetTradeTransactions(PreClearanceRequest objRequest)
        {
            TradeTransactionResponse oRes = new TradeTransactionResponse();
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@UserLogin", objRequest.LoginId);
                parameters[1] = new SqlParameter("@CompanyId", objRequest.CompanyId);
                parameters[2] = new SqlParameter("@ADMIN_DB", objRequest.ADMIN_DATABASE);
                parameters[3] = new SqlParameter("@Mode", "All");
                parameters[4] = new SqlParameter("@RelativeId", DBNull.Value);

                List<TradeTransaction> lstTrades = new List<TradeTransaction>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_GET_TRADE_TRANSACTIONS", objRequest.MODULE_DATABASE, parameters);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TradeTransaction obj = new TradeTransaction();
                        obj.AsPer = Convert.ToString(dr["AS_PER"]);
                        obj.Demat = Convert.ToString(dr["RELATIVE_DEMAT"]);
                        obj.Pan = Convert.ToString(dr["RELATIVE_PAN"]);
                        obj.RelationNm = Convert.ToString(dr["RELATION_NM"]);
                        obj.RelativeId = Convert.ToInt32(dr["RELATIVE_ID"]);
                        obj.RelativeNm = Convert.ToString(dr["RELATIVE_NM"]);
                        obj.TransDt = Convert.ToString(dr["TRANS_DT"]);
                        obj.TransQty = Convert.ToString(dr["TRANS_QTY"]);
                        obj.TransType = Convert.ToString(dr["TRANS_TYPE"]);
                        obj.TransVal = Convert.ToString(dr["TRANS_VAL"]);
                        lstTrades.Add(obj);
                    }
                    oRes.TradeTransactionList = lstTrades;
                    oRes.StatusFl = true;
                    oRes.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oRes.StatusFl = false;
                    oRes.Msg = "No data found !";
                }
                return oRes;
            }
            catch (Exception ex)
            {
                oRes.StatusFl = false;
                oRes.Msg = "Processing failed, because of system error !";
                return oRes;
            }
        }
        private void SendMailToApproverForAction(PreClearanceRequest objPreClearanceRequest)
        {
            List<EventBasedForm> lstAllFormEvents = null;
            List<string> allAttachments = new List<string>();
            List<EventBasedForm> lstAllEmailEvents = null;
            string approver = String.Empty;
            string subject = "Approval Required on Pre-Clearance Request";
            string layoutTemplate = String.Empty;

            SqlParameter[] parametersXY = new SqlParameter[5];
            parametersXY[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
            parametersXY[1] = new SqlParameter("@MODE", "GET_APPROVER_FOR_REQUEST");
            parametersXY[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parametersXY[2].Direction = ParameterDirection.Output;
            parametersXY[3] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
            parametersXY[4] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parametersXY);
            if (ds.Tables[0].Rows.Count > 0)
            {
                approver = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"]) : String.Empty;
            }

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@CompanyId", objPreClearanceRequest.CompanyId);
            parameters[1] = new SqlParameter("@Mode", "GET_EMAIL_CONFIG");

            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EMAIL_CONFIG", objPreClearanceRequest.MODULE_DATABASE, parameters);

            string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["UPSI_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["UPSI_EMAIL"]) : String.Empty;
            string sSubEvent = objPreClearanceRequest.TransactionTypeName.ToUpper() == "BUY" ? "Purchase" : "Sale";
            lstAllFormEvents = GetAllFormEvents(
                objPreClearanceRequest.CompanyId,
                objPreClearanceRequest.MODULE_DATABASE,
                objPreClearanceRequest.ADMIN_DATABASE,
                objPreClearanceRequest.LoginId,
                Convert.ToString(objPreClearanceRequest.PreClearanceRequestId),
                "Pre-clearance Request",
                sSubEvent
            );

            lstAllEmailEvents = GetAllEmailEvents(
                objPreClearanceRequest.CompanyId,
                objPreClearanceRequest.MODULE_DATABASE,
                objPreClearanceRequest.ADMIN_DATABASE,
                objPreClearanceRequest.LoginId,
                Convert.ToString(objPreClearanceRequest.PreClearanceRequestId),
                "Pre-clearance Request",
                sSubEvent
            );

            foreach (var obj in lstAllFormEvents)
            {
                obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";
                if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                {
                    string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                    if (!String.IsNullOrEmpty(docFileName))
                    {
                        String filePath = "/InsiderTrading/emailAttachment/";
                        ConvertDocToPDF(docFileName, obj.fileName, filePath);
                    }
                    CreateFormLogs(obj.formId, Convert.ToString(objPreClearanceRequest.PreClearanceRequestId), obj.fileName, objPreClearanceRequest.LoginId, objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE);
                }
                if (!String.IsNullOrEmpty(obj.fileName))
                {
                    allAttachments.Add(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + obj.fileName));
                }
            }
            if (lstAllEmailEvents != null)
            {
                foreach (var obj in lstAllEmailEvents)
                {
                    objPreClearanceRequest.encryptTaskId = CryptorEngine.Encrypt(Convert.ToString(objPreClearanceRequest.PreClearanceRequestId), true);
                    layoutTemplate = obj.formTemplate.Replace("*/**** This is a System Generated Document hence no Signature is Required ****/*", "");
                    layoutTemplate += @"<b>Kindly use the links below to submit your response. </b>(Please do not modify the response of the subject)<br />" +
                        "<a href='mailto:" + defaultEmail + "?subject=RE - " + subject + " - [Task=" + objPreClearanceRequest.encryptTaskId + "][Status=Approved]'>Approve</a>&nbsp;&nbsp;" +
                        "<a href='mailto:" + defaultEmail + "?subject=RE - " + subject + " - [Task=" + objPreClearanceRequest.encryptTaskId + "][Status=Rejected]'>Reject</a><br /><br />";
                }
            }
            #region "Pre-clearance request"
            
            //layoutTemplate += @"<b>Kindly use the links below to submit your response. </b><br />" +
            //    "<a href='mailto:" + defaultEmail + "?subject=RE - " + subject + " - [Task=" + objPreClearanceRequest.encryptTaskId + "][Status=Approve]'>Approve</a>&nbsp;&nbsp;" +
            //    "<a href='mailto:" + defaultEmail + "?subject=RE - " + subject + " - [Task=" + objPreClearanceRequest.encryptTaskId + "][Status=Reject]'>Reject</a><br /><br />";
            SqlParameter[] parameters1 = new SqlParameter[4];
            parameters1[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
            parameters1[1] = new SqlParameter("@Mode", "GET_PRECLEARANCE_REQUEST_DETAIL_BY_ID");
            parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters1[2].Direction = ParameterDirection.Output;
            parameters1[3] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);

            DataSet ds2 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters1);
            User objUser = GetUserInfo(objPreClearanceRequest);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                string preClearanceRequestedForName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["PRE_CLEARANCE_FOR_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["PRE_CLEARANCE_FOR_NAME"]) : String.Empty;
                string relationName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["RELATION_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["RELATION_NAME"]) : String.Empty;
                string securityTypeName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["SECURITY_TYPE_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["SECURITY_TYPE_NAME"]) : String.Empty;
                string companyName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["COMPANY_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["COMPANY_NAME"]) : String.Empty;
                string tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_QUANTITY"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_QUANTITY"]) : String.Empty;
                string transactionTypeName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRANSACTION_TYPE_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRANSACTION_TYPE_NAME"]) : String.Empty;
                string tradeExchangeName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_EXCHANGE_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_EXCHANGE_NAME"]) : String.Empty;
                string tradeDate = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_DATE_NEW"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_DATE_NEW"]) : String.Empty;
                string totalAmount = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TOTAL_AMOUNT"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TOTAL_AMOUNT"]) : String.Empty;
                string actualTransactionDate = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE_NEW"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE_NEW"]) : String.Empty;
                Int32 preClearanceRequestedFor = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["PRE_CLEARANCE_REQUESTED_FOR"])) ? Convert.ToInt32(ds2.Tables[0].Rows[0]["PRE_CLEARANCE_REQUESTED_FOR"]) : 0;

                if (preClearanceRequestedForName == String.Empty)
                {
                    preClearanceRequestedForName = "Self";
                }

                SqlParameter[] parameters2 = new SqlParameter[4];
                parameters2[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters2[1] = new SqlParameter("@Mode", "GET_NON_COMPLIANCE_RECORD_OF_LAST_ONE_YEAR");
                parameters2[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters2[2].Direction = ParameterDirection.Output;
                parameters2[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);

                DataSet ds3 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters2);
                if (ds3.Tables.Count > 0)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        layoutTemplate += "<br/><br/>";
                        layoutTemplate += "<table border='1'>";
                        layoutTemplate += "<thead>";
                        layoutTemplate += "<tr>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>User Name</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Relation</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>PAN</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Folio No</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Holding At Declaration</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Holding As On Date</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Uploaded Date</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Trade Quantity</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Method</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Is Non-Compliant?</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Non-Compliant Type</th>";
                        layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Remarks</th>";
                        layoutTemplate += "</tr>";
                        layoutTemplate += "</thead>";
                        layoutTemplate += "<tbody>";

                        foreach (DataRow dr in ds3.Tables[0].Rows)
                        {

                            string name = !String.IsNullOrEmpty(Convert.ToString(dr["NAME"])) ? Convert.ToString(dr["NAME"]) : String.Empty;
                            string relation = !String.IsNullOrEmpty(Convert.ToString(dr["RELATION_NM"])) ? Convert.ToString(dr["RELATION_NM"]) : "Self";
                            string pan = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                            string initialHoldingAfterDeclaration = !String.IsNullOrEmpty(Convert.ToString(dr["INITIAL_HOLDING_AFTER_DECLARATION"])) ? Convert.ToString(dr["INITIAL_HOLDING_AFTER_DECLARATION"]) : "0";
                            string holdingAsOnDate = !String.IsNullOrEmpty(Convert.ToString(dr["HOLDING_AS_ON_DATE"])) ? Convert.ToString(dr["HOLDING_AS_ON_DATE"]) : "0";
                            string tradeDateNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_DATE"])) ? Convert.ToString(dr["TRADE_DATE"]) : String.Empty;
                            string tradeQuantityNew = String.Empty;
                            if (Convert.ToString(dr["VALUE"]) == "Buy")
                            {
                                tradeQuantityNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? "+" + Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                            }
                            else if (Convert.ToString(dr["VALUE"]) == "Sell")
                            {
                                tradeQuantityNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? "-" + Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                            }
                            else
                            {
                                tradeQuantityNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                            }
                            string folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO_NO"])) ? Convert.ToString(dr["FOLIO_NO"]) : String.Empty;
                            string method = !String.IsNullOrEmpty(Convert.ToString(dr["METHOD"])) ? Convert.ToString(dr["METHOD"]) : String.Empty;
                            string isNonCompliant = !String.IsNullOrEmpty(Convert.ToString(dr["IS_NON_COMPLIANT"])) ? Convert.ToString(dr["IS_NON_COMPLIANT"]) : String.Empty;
                            string nonCompliantType = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TYPE"])) ? Convert.ToString(dr["NON_COMPLIANCE_TYPE"]) : String.Empty;
                            string nonCompliantReason = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;

                            if (initialHoldingAfterDeclaration == "-1")
                            {
                                initialHoldingAfterDeclaration = "NA";
                            }

                            layoutTemplate += "<tr>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + name + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + relation + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + pan + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + folioNumber + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + initialHoldingAfterDeclaration + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + holdingAsOnDate + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + tradeDateNew + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + tradeQuantityNew + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + method + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + isNonCompliant + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + nonCompliantType + "</td>";
                            layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + nonCompliantReason + "</td>";
                            layoutTemplate += "</tr>";
                        }


                        layoutTemplate += "</tbody>";
                        layoutTemplate += "</table>";
                    }
                }

            }
            #endregion

            EmailSender.SendMail(
                approver, subject, layoutTemplate, allAttachments, "Pre-Clearance Request", objPreClearanceRequest.CompanyId.ToString(),
                objUser.USER_EMAIL, Convert.ToString(objPreClearanceRequest.PreClearanceRequestId)
            );
        }
        private static void CreatePDFFromHTMLFile(string HtmlStream, string FileName)
        {
            try
            {
                object TargetFile = FileName;
                string ModifiedFileName = string.Empty;
                string FinalFileName = string.Empty;

                /* To add a Password to PDF -http://aspnettutorialonline.blogspot.com/ */
                TestPDF.HtmlToPdfBuilder builder = new TestPDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);
                TestPDF.HtmlPdfPage first = builder.AddPage();
                first.AppendHtml(HtmlStream);

                byte[] file = builder.RenderPdf();
                File.WriteAllBytes(TargetFile.ToString(), file);

                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
                ModifiedFileName = TargetFile.ToString();
                ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

                string password = "";
                iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, password, "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
                //http://aspnettutorialonline.blogspot.com/
                reader.Close();
                if (File.Exists(TargetFile.ToString()))
                    File.Delete(TargetFile.ToString());
                FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
                File.Copy(ModifiedFileName, FinalFileName);
                if (File.Exists(ModifiedFileName))
                    File.Delete(ModifiedFileName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region "Get Template For Approver For Action"
        public PreClearanceRequestResponse GetTemplateForApproverForAction(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                string approver = String.Empty;
                string subject = String.Empty;
                string layoutTemplate = String.Empty;
                string systemName = String.Empty;

                SqlParameter[] parametersXY = new SqlParameter[5];
                parametersXY[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parametersXY[1] = new SqlParameter("@MODE", "GET_COMPANY_ASSIGNED_USER");
                parametersXY[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parametersXY[2].Direction = ParameterDirection.Output;
                parametersXY[3] = new SqlParameter("@ADMIN_DB", objPreClearanceRequest.ADMIN_DATABASE);
                parametersXY[4] = new SqlParameter("@BUSINESS_UNIT_ID", objPreClearanceRequest.businessUnitId);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objPreClearanceRequest.MODULE_DATABASE, parametersXY);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    approver = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"]) : String.Empty;
                }

                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objPreClearanceRequest.MODULE_DATABASE, parameters);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                    string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                    string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                    Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                    bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                    string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                    string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                    bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                    objPreClearanceRequest.encryptTaskId = CryptorEngine.Encrypt(Convert.ToString(objPreClearanceRequest.PreClearanceRequestId), true);

                    SqlParameter[] parameters1 = new SqlParameter[5];
                    parameters1[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                    parameters1[1] = new SqlParameter("@Mode", "GET_PRECLEARANCE_REQUEST_DETAIL_BY_ID");
                    parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[2].Direction = ParameterDirection.Output;
                    parameters1[3] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                    parameters1[4] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);

                    DataSet ds2 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters1);

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        string preClearanceRequestedForName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["PRE_CLEARANCE_FOR_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["PRE_CLEARANCE_FOR_NAME"]) : String.Empty;
                        string relationName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["RELATION_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["RELATION_NAME"]) : String.Empty;
                        string securityTypeName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["SECURITY_TYPE_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["SECURITY_TYPE_NAME"]) : String.Empty;
                        string companyName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["COMPANY_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["COMPANY_NAME"]) : String.Empty;
                        string tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_QUANTITY"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_QUANTITY"]) : String.Empty;
                        string transactionTypeName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRANSACTION_TYPE_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRANSACTION_TYPE_NAME"]) : String.Empty;
                        string tradeExchangeName = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_EXCHANGE_NAME"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_EXCHANGE_NAME"]) : String.Empty;
                        string tradeDate = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_DATE_NEW"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TRADE_DATE_NEW"]) : String.Empty;
                        string totalAmount = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["TOTAL_AMOUNT"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["TOTAL_AMOUNT"]) : String.Empty;
                        string actualTransactionDate = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE_NEW"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["ACTUAL_TRANSACTION_DATE_NEW"]) : String.Empty;
                        string loginId = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["LOGIN_ID"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["LOGIN_ID"]) : String.Empty;
                        string shareCurrentMarketPrice = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["SHARE_CURRENT_MARKET_PRICE"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["SHARE_CURRENT_MARKET_PRICE"]) : String.Empty;
                        string proposedTransactionThrough = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["PROPOSED_TRANSACTION_THROUGH"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["PROPOSED_TRANSACTION_THROUGH"]) : String.Empty;

                        if (preClearanceRequestedForName == String.Empty)
                        {
                            preClearanceRequestedForName = "Self";
                        }
                        User objUser = GetUserInfo(objPreClearanceRequest);


                        string sSubEvent = transactionTypeName.ToUpper() == "BUY" ? "Purchase" : "Sale";
                        List<EventBasedForm> lstAllFormEvents = GetAllFormEvents(objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.LoginId, Convert.ToString(objPreClearanceRequest.PreClearanceRequestId), "Pre-clearance Request", sSubEvent);

                        layoutTemplate = "";
                        foreach (var obj in lstAllFormEvents)
                        {
                            if (!String.IsNullOrEmpty(obj.formTemplate))
                            {
                                layoutTemplate += "<br /" + obj.formTemplate.Replace("*/**** This is a System Generated Document hence no Signature is Required ****/*", "");
                            }
                        }

                        SqlParameter[] parameters2 = new SqlParameter[4];
                        parameters2[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                        parameters2[1] = new SqlParameter("@Mode", "GET_NON_COMPLIANCE_RECORD_OF_LAST_ONE_YEAR");
                        parameters2[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters2[2].Direction = ParameterDirection.Output;
                        parameters2[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);

                        DataSet ds3 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters2);
                        if (ds3.Tables.Count > 0)
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {


                                layoutTemplate += "<br/><br/>";


                                layoutTemplate += "<table border='1'>";
                                layoutTemplate += "<thead>";
                                layoutTemplate += "<tr>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>User Name</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Relation</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>PAN</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Folio No</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Holding At Declaration</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Holding As On Date</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Uploaded Date</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Trade Quantity</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Method</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Is Non-Compliant?</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Non-Compliant Type</th>";
                                layoutTemplate += "<th style='border: 1px solid;white-space: nowrap;'>Remarks</th>";
                                layoutTemplate += "</tr>";
                                layoutTemplate += "</thead>";
                                layoutTemplate += "<tbody>";

                                foreach (DataRow dr in ds3.Tables[0].Rows)
                                {
                                    string name = !String.IsNullOrEmpty(Convert.ToString(dr["NAME"])) ? Convert.ToString(dr["NAME"]) : String.Empty;
                                    string relation = !String.IsNullOrEmpty(Convert.ToString(dr["RELATION_NM"])) ? Convert.ToString(dr["RELATION_NM"]) : "Self";
                                    string pan = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                                    string initialHoldingAfterDeclaration = !String.IsNullOrEmpty(Convert.ToString(dr["INITIAL_HOLDING_AFTER_DECLARATION"])) ? Convert.ToString(dr["INITIAL_HOLDING_AFTER_DECLARATION"]) : "0";
                                    string holdingAsOnDate = !String.IsNullOrEmpty(Convert.ToString(dr["HOLDING_AS_ON_DATE"])) ? Convert.ToString(dr["HOLDING_AS_ON_DATE"]) : "0";
                                    string tradeDateNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_DATE"])) ? Convert.ToString(dr["TRADE_DATE"]) : String.Empty;
                                    string tradeQuantityNew = String.Empty;
                                    if (Convert.ToString(dr["VALUE"]) == "Buy")
                                    {
                                        tradeQuantityNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? "+" + Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                                    }
                                    else if (Convert.ToString(dr["VALUE"]) == "Sell")
                                    {
                                        tradeQuantityNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? "-" + Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                                    }
                                    else
                                    {
                                        tradeQuantityNew = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_QUANTITY"])) ? Convert.ToString(dr["TRADE_QUANTITY"]) : "0";
                                    }
                                    string folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO_NO"])) ? Convert.ToString(dr["FOLIO_NO"]) : String.Empty;
                                    string method = !String.IsNullOrEmpty(Convert.ToString(dr["METHOD"])) ? Convert.ToString(dr["METHOD"]) : String.Empty;
                                    string isNonCompliant = !String.IsNullOrEmpty(Convert.ToString(dr["IS_NON_COMPLIANT"])) ? Convert.ToString(dr["IS_NON_COMPLIANT"]) : String.Empty;
                                    string nonCompliantType = !String.IsNullOrEmpty(Convert.ToString(dr["NON_COMPLIANCE_TYPE"])) ? Convert.ToString(dr["NON_COMPLIANCE_TYPE"]) : String.Empty;
                                    string nonCompliantReason = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;

                                    if (initialHoldingAfterDeclaration == "-1")
                                    {
                                        initialHoldingAfterDeclaration = "NA";
                                    }

                                    layoutTemplate += "<tr>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + name + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + relation + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + pan + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + folioNumber + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + initialHoldingAfterDeclaration + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + holdingAsOnDate + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + tradeDateNew + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + tradeQuantityNew + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + method + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + isNonCompliant + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + nonCompliantType + "</td>";
                                    layoutTemplate += "<td style='border: 1px solid;white-space: nowrap;'>" + nonCompliantReason + "</td>";
                                    layoutTemplate += "</tr>";
                                }


                                layoutTemplate += "</tbody>";
                                layoutTemplate += "</table>";
                            }
                        }

                        //  User objUser = GetUserInfo(objPreClearanceRequest);

                        SqlParameter[] parameters5 = new SqlParameter[7];
                        parameters5[0] = new SqlParameter("@UserName", objUser.USER_NM);
                        parameters5[1] = new SqlParameter("@DesignationName", objUser.designation.DESIGNATION_NM);
                        parameters5[2] = new SqlParameter("@DepartmentName", objUser.department.DEPARTMENT_NM);
                        parameters5[3] = new SqlParameter("@LocationName", objUser.location.locationName);
                        parameters5[4] = new SqlParameter("@Mode", "GET_LAYOUT_TEMPLATE_AND_SUBJECT_LOWER");
                        parameters5[5] = new SqlParameter("@CompanyId", objPreClearanceRequest.CompanyId);
                        parameters5[6] = new SqlParameter("@SetCount", SqlDbType.Int);
                        parameters5[6].Direction = ParameterDirection.Output;
                        DataSet ds5 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_IT_APPROVAL_REQUIRED_ON_PRE_CLEARANCE_REQUEST_EMAIL_TEMPLATE", objPreClearanceRequest.MODULE_DATABASE, parameters5);
                        if (ds5.Tables.Count > 0)
                        {
                            if (ds5.Tables[0].Rows.Count > 0)
                            {
                                layoutTemplate += !String.IsNullOrEmpty(Convert.ToString(ds5.Tables[0].Rows[0]["LAYOUT_TEMPLATE"])) ? Convert.ToString(ds5.Tables[0].Rows[0]["LAYOUT_TEMPLATE"]) : String.Empty;
                                subject = !String.IsNullOrEmpty(Convert.ToString(ds5.Tables[0].Rows[0]["SUBJECT"])) ? Convert.ToString(ds5.Tables[0].Rows[0]["SUBJECT"]) : String.Empty;
                            }
                        }
                    }


                }
                objPreClearanceRequest.layoutTemplate = layoutTemplate;
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                return oPreClearanceRequest;

            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        #endregion
        #region "Get All Pending Request"
        public PreClearanceRequestResponse GetAllPendingRequest(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Mode", "GET_ALL_PENDING_REQUEST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        PreClearanceRequest obj = new PreClearanceRequest();
                        obj.PreClearanceRequestId = Convert.ToInt32(rdr["ID"]);
                        obj.PreClearanceRequestedForName = Convert.ToInt32(rdr["PRE_CLEARANCE_REQUESTED_FOR"]) == 0 ? Convert.ToString(rdr["USER_NM"]) : (!String.IsNullOrEmpty(Convert.ToString(rdr["PRE_CLEARANCE_FOR_NAME"])) ? Convert.ToString(rdr["PRE_CLEARANCE_FOR_NAME"]) : String.Empty);
                        obj.relationName = Convert.ToInt32(rdr["PRE_CLEARANCE_REQUESTED_FOR"]) == 0 ? "Self" : Convert.ToString(rdr["RELATION_NAME"]);
                        obj.SecurityTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE"]))) ? Convert.ToString(rdr["SECURITY_TYPE"]) : String.Empty;
                        obj.TradeCompanyName = (!String.IsNullOrEmpty(Convert.ToString(rdr["COMPANY_NAME"]))) ? Convert.ToString(rdr["COMPANY_NAME"]) : String.Empty;
                        obj.TradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_QUANTITY"]))) ? Convert.ToString(rdr["TRADE_QUANTITY"]) : String.Empty;
                        obj.TransactionTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE"]))) ? Convert.ToString(rdr["TRANSACTION_TYPE"]) : String.Empty;
                        obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_DATE"]))) ? FormatHelper.FormatDate(Convert.ToString(rdr["TRADE_DATE"])) : String.Empty;
                        obj.LoginId = (!String.IsNullOrEmpty(Convert.ToString(rdr["LOGIN_ID"]))) ? Convert.ToString(rdr["LOGIN_ID"]) : String.Empty;
                        obj.PeriodQty = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERIOD_QTY"]))) ? Convert.ToString(rdr["PERIOD_QTY"]) : "-";
                        obj.PeriodVal = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERIOD_VAL"]))) ? Convert.ToString(rdr["PERIOD_VAL"]) : "-";
                        obj.TotalAmount = (!String.IsNullOrEmpty(Convert.ToString(rdr["TOTAL_AMOUNT"]))) ? FormatHelper.FormatNumber(Convert.ToString(rdr["TOTAL_AMOUNT"])) : String.Empty;
                        oPreClearanceRequest.AddObject(obj);
                    }
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "No data found !";
                }
                rdr.Close();
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        #endregion
        #region "Update Task Users"
        public PreClearanceRequestResponse UpdateTaskUsers(PreClearanceRequest objTaskUsers)
        {
            string newTemplate = String.Empty;
            string formIDisplayName = String.Empty;
            try
            {
                List<EventBasedForm> lstAllFormEvents = null;
                List<EventBasedForm> lstAllMailEvents = null;
                String sApprovalFrm = Convert.ToString(ConfigurationManager.AppSettings["PreClearanceFrom"]);

                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objTaskUsers.PreClearanceRequestId);
                parameters[1] = new SqlParameter("@MODE", "SET_STATUS_BY_APPROVER");
                parameters[2] = new SqlParameter("@COMPANY_ID", objTaskUsers.CompanyId);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4] = new SqlParameter("@STATUS", objTaskUsers.Status);
                parameters[5] = new SqlParameter("@REMARKS", objTaskUsers.remarks);
                parameters[6] = new SqlParameter("@LOGIN_ID", objTaskUsers.LoginId);
                parameters[7] = new SqlParameter("@ApprovalFrm", sApprovalFrm);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objTaskUsers.MODULE_DATABASE, parameters);

                string _sql = "SELECT USER_EMAIL FROM " + objTaskUsers.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) WHERE LOGIN_ID='" + objTaskUsers.LoginId + "'";
                string sApproverEmail = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objTaskUsers.MODULE_DATABASE, null);

                if ((Int32)parameters[3].Value == 1)
                {
                    User objUser = GetUserTaskInitiatorEmail(objTaskUsers);
                    PreClearanceRequest oTaskUsers = GetPreClearanceRequestDetails(objTaskUsers);

                    string emailBody = String.Empty;
                    List<string> allAttachments = new List<string>();

                    if (objTaskUsers.Status == "Approved")
                    {
                        lstAllFormEvents = GetAllFormEvents(objTaskUsers.CompanyId, objTaskUsers.MODULE_DATABASE, objTaskUsers.ADMIN_DATABASE, objTaskUsers.LoginId, Convert.ToString(objTaskUsers.PreClearanceRequestId), "CO Pre-Request Action", "Approved");
                    }
                    else
                    {
                        lstAllFormEvents = GetAllFormEvents(objTaskUsers.CompanyId, objTaskUsers.MODULE_DATABASE, objTaskUsers.ADMIN_DATABASE, objTaskUsers.LoginId, Convert.ToString(objTaskUsers.PreClearanceRequestId), "CO Pre-Request Action", "Rejected");
                    }

                    string subject = "Preclearance Order [Status=" + objTaskUsers.Status + "]";
                    foreach (var obj in lstAllFormEvents)
                    {
                        obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";
                        newTemplate = obj.formTemplate.Replace("*/**** This is a System Generated Document hence no Signature is Required ****/*", "");
                        string docFileName = CreateDocFile(newTemplate, obj.fileName, obj.formOrientation);

                        if (!String.IsNullOrEmpty(docFileName))
                        {
                            String filePath = "/InsiderTrading/emailAttachment/";
                            ConvertDocToPDF(docFileName, obj.fileName, filePath);
                            CreateFormLogs(obj.formId, Convert.ToString(objTaskUsers.PreClearanceRequestId),
                                obj.fileName, objTaskUsers.LoginId, objTaskUsers.CompanyId,
                                objTaskUsers.MODULE_DATABASE);
                        }
                        if (!String.IsNullOrEmpty(obj.fileName))
                        {
                            allAttachments.Add(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + obj.fileName));
                        }
                    }
                    if (!String.IsNullOrEmpty(newTemplate))
                    {
                        EmailSender.SendMail(objUser.USER_EMAIL, subject, newTemplate, allAttachments, "Pre-Clearance Order",
                            objTaskUsers.CompanyId.ToString(), "", Convert.ToString(objTaskUsers.PreClearanceRequestId), objTaskUsers.LoginId
                        );
                    }

                    PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Request has been updated successfully";
                    return oPreClearanceRequest;
                }
                else
                {
                    PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                    return oPreClearanceRequest;
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(PreClearanceRequestRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(objTaskUsers.LoginId), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        #endregion
        #region "Get User Email And User Name"
        private User GetUserTaskInitiatorEmail(PreClearanceRequest objTaskUsers)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objTaskUsers.PreClearanceRequestId);

            parameters[1] = new SqlParameter("@MODE", "GET_USER_TASK_INITIATOR_EMAIL_BY_TASK_ID");
            parameters[2] = new SqlParameter("@COMPANY_ID", objTaskUsers.CompanyId);
            parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4] = new SqlParameter("@ADMIN_DATABASE", objTaskUsers.ADMIN_DATABASE);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objTaskUsers.MODULE_DATABASE, parameters);
            User objUser = new User();
            if (ds.Tables[0].Rows.Count > 0)
            {
                objUser.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"]) : String.Empty;
                objUser.USER_NM = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"]) : String.Empty;
                objUser.designation = new Designation
                {
                    DESIGNATION_NM = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_NAME"]) : String.Empty
                };
                objUser.location = new Location
                {
                    locationName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LOCATION_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["LOCATION_NAME"]) : String.Empty
                };
            }
            return objUser;
        }
        #endregion
        #region "Get All Trading Files Of Current Request" 
        public List<FileModel> GetAllTradingFilesOfCurrentRequest(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@MODE", "GET_ALL_TRADING_FILES");
                parameter[1] = new SqlParameter("@DataElementId", objPreClearanceRequest.PreClearanceRequestId);
                parameter[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameter[3] = new SqlParameter("@USER_LOGIN", objPreClearanceRequest.LoginId);
                parameter[4] = new SqlParameter("@MAIN_EVENT", "Pre-clearance Request");
                parameter[5] = new SqlParameter("@SUB_EVENT", "");
                List<FileModel> lstObjFileModel = new List<FileModel>();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_GET_PROCS_INSIDER_FORMS", objPreClearanceRequest.MODULE_DATABASE, parameter);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            lstObjFileModel.Add(new FileModel
                            {
                                FileName = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_NAME"])) ? Convert.ToString(dr["FORM_NAME"]) : String.Empty
                            });
                        }
                    }
                }
                return lstObjFileModel;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objPreClearanceRequest.LoginId, 5, objPreClearanceRequest.CompanyId);
            }
            return null;
        }
        #endregion
        #region "Get Trade Date And Quantity"
        private PreClearanceRequest GetPreClearanceRequestDetails(PreClearanceRequest objTaskUsers)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objTaskUsers.PreClearanceRequestId);

            parameters[1] = new SqlParameter("@MODE", "GET_TRADE_DATE_AND_QUANTITY");
            parameters[2] = new SqlParameter("@COMPANY_ID", objTaskUsers.CompanyId);
            parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[3].Direction = ParameterDirection.Output;
            parameters[4] = new SqlParameter("@STATUS", objTaskUsers.Status);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objTaskUsers.MODULE_DATABASE, parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                objTaskUsers.TradeDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADE_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TRADE_DATE"]) : String.Empty;
                objTaskUsers.TradeQuantity = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADE_QUANTITY"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TRADE_QUANTITY"]) : String.Empty;
                objTaskUsers.completedOnOrBefore = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["COMPLETED_ON_OR_BEFORE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["COMPLETED_ON_OR_BEFORE"]) : String.Empty;
                objTaskUsers.CreatedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"]) : String.Empty;
            }

            return objTaskUsers;
        }
        #endregion
        #region "Get Forms"
        public DataSet GetForms(PreClearanceRequest objRequest)
        {
            DataSet dtForms = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_INSIDER_FORMS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@MODE", objRequest.formType));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objRequest.CompanyId));
                        cmd.Parameters.Add(new SqlParameter("@USER_LOGIN", objRequest.LoginId));
                        cmd.Parameters.Add(new SqlParameter("@PRE_REQUEST_CLEARENCE_ID", objRequest.PreClearanceRequestId));
                        cmd.Parameters.Add(new SqlParameter("@BROKER_NOTE_ID", objRequest.brokerNoteId));
                        cmd.Parameters.Add(new SqlParameter("@ADMIN_DATABASE", objRequest.ADMIN_DATABASE));
                        SqlDataAdapter Da = new SqlDataAdapter();
                        Da.SelectCommand = cmd;
                        Da.Fill(dtForms);

                        //SqlDataReader rdr = cmd.ExecuteReader();
                        if (dtForms.Tables.Count > 0)
                        {
                            //dtForms.Load(rdr);
                        }

                    }
                    conn.Close();
                }
                if (dtForms.Tables.Count > 0)
                {


                    return dtForms;
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
            }
            return null;
        }
        #endregion
        #region "Get User Info"
        private User GetUserInfo(PreClearanceRequest objPreClearanceRequest)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
            parameters[1] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
            parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3] = new SqlParameter("@MODE", "GET_USER_INFO");
            parameters[4] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);
            User obj = new User();
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    obj.USER_NM = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"]) : String.Empty;
                    obj.designation = new Designation
                    {
                        DESIGNATION_NM = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_NAME"]) : String.Empty
                    };
                    obj.department = new Department
                    {
                        DEPARTMENT_NM = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_NAME"]) : String.Empty
                    };
                    obj.location = new Location
                    {
                        locationName = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LOCATION_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["LOCATION_NAME"]) : String.Empty
                    };
                    obj.address = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"]) : String.Empty;
                    obj.USER_EMAIL = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"]) : String.Empty;
                }
            }
            return obj;
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
        #region "Validate Trade Date Lies In Trading Window Closure"
        public bool ValidateTradeDateLiesInTradingWindowClosure(string tradeDate, Int32 companyId, string moduleDatabase, Int32 TransactionType)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@TRADE_DATE", ConvertDate(tradeDate));
                parameter[1] = new SqlParameter("@COMPANY_ID", companyId);
                parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;
                parameter[3] = new SqlParameter("@MODE", "VALIDATE_TRADE_DATE_IN_TRADING_WINDOW_CLOSURE");
                parameter[4] = new SqlParameter("@TRANSACTION_TYPE", TransactionType);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", moduleDatabase, parameter);

                if (Convert.ToInt32(parameter[2].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Validate Trade Date Falls In Holiday"
        public bool ValidateTradeDateFallsInHolidayList(string tradeDate, Int32 companyId, string moduleDatabase, Int32 TransactionType)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@TRADE_DATE", ConvertDate(tradeDate));
                parameter[1] = new SqlParameter("@COMPANY_ID", companyId);
                parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;
                parameter[3] = new SqlParameter("@MODE", "VALIDATE_TRADE_DATE_IN_HOLIDAY_LIST");
                parameter[4] = new SqlParameter("@TRANSACTION_TYPE", TransactionType);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", moduleDatabase, parameter);

                if (Convert.ToInt32(parameter[2].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Create Doc File"
        private string CreateDocFile(string htmlText, string fileNameTemp, string sOrientation)
        {
            String downloadFileDir = "/InsiderTrading/emailAttachment/";
            String fileName = String.Empty;

            WordDocument wordDocument = new WordDocument();
            IWSection section = wordDocument.AddSection();

            fileName = Path.GetFileNameWithoutExtension(fileNameTemp) + ".docx";
            if (sOrientation.ToLower() == "landscape")
            {
                section.PageSetup.Orientation = PageOrientation.Landscape;
            }
            else
            {
                section.PageSetup.Orientation = PageOrientation.Portrait;
            }
            section.Body.IsValidXHTML(htmlText, wordDocument.XHTMLValidateOption);
            section.Body.InsertXHTML(htmlText);
            wordDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName));
            wordDocument.Close();
            return fileName;
        }
        #endregion
        #region "Create Doc File Landscape"
        private string CreateDocFileLandscape(string htmlText, string fileNameTemp)
        {
            String downloadFileDir = "/InsiderTrading/emailAttachment/";
            String fileName = String.Empty;

            //Creates a new Word document
            WordDocument wordDocument = new WordDocument();

            //Creates a section to the document
            IWSection section = wordDocument.AddSection();

            //Saves the Word document to disk in DOCX format
            fileName = Path.GetFileNameWithoutExtension(fileNameTemp) + ".docx";
            section.PageSetup.Orientation = PageOrientation.Landscape;

            //Validates whether the string is in proper XHTML
            section.Body.IsValidXHTML(htmlText, wordDocument.XHTMLValidateOption);

            //Inserts HTML string to the document
            section.Body.InsertXHTML(htmlText);

            //Create a custom style
            // WParagraphStyle paragraphStyle = wordDocument.Styles.FindByName("Normal") as WParagraphStyle;
            //  paragraphStyle.CharacterFormat.Font = new Font("Times New Roman", 12);
            //paragraphStyle.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Justify;

            wordDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName));

            //Close Word document
            wordDocument.Close();

            return fileName;
        }
        private string CreateWordDocument(string htmlText, string fileNameTemp, string sOrientation)
        {
            String downloadFileDir = "/InsiderTrading/emailAttachment/";
            String fileName = String.Empty;
            WordDocument wordDocument = new WordDocument();
            IWSection section = wordDocument.AddSection();

            fileName = Path.GetFileNameWithoutExtension(fileNameTemp) + ".docx";
            if (sOrientation == "Landscape")
            {
                section.PageSetup.Orientation = PageOrientation.Landscape;
            }
            else
            {
                section.PageSetup.Orientation = PageOrientation.Portrait;
            }

            section.Body.IsValidXHTML(htmlText, wordDocument.XHTMLValidateOption);
            section.Body.InsertXHTML(htmlText);
            wordDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName));
            wordDocument.Close();
            return fileName;
        }
        #endregion
        #region "Convert Doc to Pdf"
        private bool ConvertDocToPDF(String docFileName, String pdfFileName, String filePath)
        {
            bool isStatus = false;
            try
            {
                WordDocument wordDocument = new WordDocument(Path.Combine(HttpContext.Current.Server.MapPath("~" + filePath), docFileName), Syncfusion.DocIO.FormatType.Docx);
                wordDocument.ChartToImageConverter = new ChartToImageConverter();
                DocToPDFConverter converter = new DocToPDFConverter();

                converter.Settings.ImageQuality = 100;
                converter.Settings.ImageResolution = 640;
                converter.Settings.OptimizeIdenticalImages = true;

                PdfDocument pdfDocument = converter.ConvertToPDF(wordDocument);
                pdfDocument.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + filePath), pdfFileName));
                pdfDocument.Close(true);
                wordDocument.Close();
                isStatus = true;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "PreClearanceRequestController", "ConvertDocToPDF", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                isStatus = false;
            }
            return isStatus;
        }
        #endregion
        #region "Convert Html to Pdf"
        private string ConvertHtmlToPdf(string htmlText, string fileNameTemp)
        {
            String downloadFileDir = "/InsiderTrading/emailAttachment/";
            String fileName = String.Empty;

            //Saves the Html  to disk in Pdf format
            fileName = Path.GetFileNameWithoutExtension(fileNameTemp) + ".pdf";

            //Initialize HTML to PDF converter 

            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            //HTML string and base URL 

            string baseUrl = "/assets/";

            //Convert HTML to PDF document 

            PdfDocument document = htmlConverter.Convert(htmlText, HttpContext.Current.Server.MapPath("~" + baseUrl));

            //Save and close the PDF document 

            document.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName));

            document.Close(true);

            return fileName;
        }
        #endregion
        #region "Get Undertaking Template"
        public PreClearanceRequestResponse GetUndertakingTemplate(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                string formHDisplayName = String.Empty;
                string htmlText = String.Empty;
                string subject = String.Empty;

                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();

                User objUser = GetUserInfo(objPreClearanceRequest);

                SqlParameter[] parameters5 = new SqlParameter[8];
                parameters5[0] = new SqlParameter("@UserName", objUser.USER_NM);
                parameters5[1] = new SqlParameter("@DesignationName", objUser.designation.DESIGNATION_NM);
                parameters5[2] = new SqlParameter("@DepartmentName", objUser.department.DEPARTMENT_NM);
                parameters5[3] = new SqlParameter("@LocationName", objUser.address);
                parameters5[4] = new SqlParameter("@Mode", "GET_UNDERTAKING_TEMPLATE_BEFORE_PCR");
                parameters5[5] = new SqlParameter("@CompanyId", objPreClearanceRequest.CompanyId);
                parameters5[6] = new SqlParameter("@SetCount", SqlDbType.Int);
                parameters5[6].Direction = ParameterDirection.Output;
                parameters5[7] = new SqlParameter("@TradeQuantity", objPreClearanceRequest.TradeQuantity);
                DataSet ds5 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_IT_APPROVAL_REQUIRED_ON_PRE_CLEARANCE_REQUEST_EMAIL_TEMPLATE", objPreClearanceRequest.MODULE_DATABASE, parameters5);
                if (ds5.Tables.Count > 0)
                {
                    if (ds5.Tables[0].Rows.Count > 0)
                    {
                        htmlText = !String.IsNullOrEmpty(Convert.ToString(ds5.Tables[0].Rows[0]["CONTENT"])) ? Convert.ToString(ds5.Tables[0].Rows[0]["CONTENT"]) : String.Empty;
                    }
                }

                objPreClearanceRequest.underTakingText = htmlText;
                objResponse.PreClearanceRequest = objPreClearanceRequest;
                objResponse.StatusFl = true;
                objResponse.Msg = "Undertaking Template Generated !";
                return objResponse;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(PreClearanceRequestRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion
        #region "Get All Form Events"
        private List<EventBasedForm> GetAllFormEvents(Int32 companyId, string moduleDb, string adminDb, string loginId, string dataElementId, string eventType, string subEventType)
        {
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[1] = new SqlParameter("@MODE", "GET_ALL_FORM_EVENTS");
            parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[3] = new SqlParameter("@MAIN_EVENT", eventType);
            parameters[4] = new SqlParameter("@SUB_EVENT", subEventType);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);
            List<EventBasedForm> lstEventBasedForm = new List<EventBasedForm>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        EventBasedForm objEventBasedForm = new EventBasedForm();
                        objEventBasedForm.formId = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_ID"])) ? Convert.ToInt32(dr["FORM_ID"]) : 0;
                        objEventBasedForm.mainEvent = !String.IsNullOrEmpty(Convert.ToString(dr["MAIN_EVENT"])) ? Convert.ToString(dr["MAIN_EVENT"]) : String.Empty;
                        objEventBasedForm.subEvent = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_EVENT"])) ? Convert.ToString(dr["SUB_EVENT"]) : String.Empty;
                        objEventBasedForm.formName = !String.IsNullOrEmpty(Convert.ToString(dr["DISPLAY_NAME"])) ? Convert.ToString(dr["DISPLAY_NAME"]) : String.Empty;
                        objEventBasedForm.formOrientation = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_ORIENTATION"])) ? Convert.ToString(dr["FORM_ORIENTATION"]) : "Landscape";
                        objEventBasedForm.formTemplate = GetFormEventBasedTemplate(companyId, moduleDb, adminDb, loginId, objEventBasedForm.formId, dataElementId, eventType, subEventType);
                        lstEventBasedForm.Add(objEventBasedForm);
                    }
                }
            }
            return lstEventBasedForm;
        }
        private List<EventBasedForm> GetFormBySubEvent(Int32 companyId, string moduleDb, string adminDb, string loginId, string dataElementId, string eventType, string subEventType)
        {
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[1] = new SqlParameter("@MODE", "GET_FORM_BY_SUB_EVENT");
            parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[3] = new SqlParameter("@MAIN_EVENT", eventType);
            parameters[4] = new SqlParameter("@SUB_EVENT", subEventType);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);
            List<EventBasedForm> lstEventBasedForm = new List<EventBasedForm>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        EventBasedForm objEventBasedForm = new EventBasedForm();
                        objEventBasedForm.formId = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_ID"])) ? Convert.ToInt32(dr["FORM_ID"]) : 0;
                        objEventBasedForm.mainEvent = !String.IsNullOrEmpty(Convert.ToString(dr["MAIN_EVENT"])) ? Convert.ToString(dr["MAIN_EVENT"]) : String.Empty;
                        objEventBasedForm.subEvent = !String.IsNullOrEmpty(Convert.ToString(dr["SUB_EVENT"])) ? Convert.ToString(dr["SUB_EVENT"]) : String.Empty;
                        objEventBasedForm.formName = !String.IsNullOrEmpty(Convert.ToString(dr["DISPLAY_NAME"])) ? Convert.ToString(dr["DISPLAY_NAME"]) : String.Empty;
                        objEventBasedForm.formOrientation = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_ORIENTATION"])) ? Convert.ToString(dr["FORM_ORIENTATION"]) : "Landscape";
                        objEventBasedForm.formTemplate = GetFormEventBasedTemplate(companyId, moduleDb, adminDb, loginId, objEventBasedForm.formId, dataElementId, eventType, subEventType);
                        lstEventBasedForm.Add(objEventBasedForm);
                    }
                }
            }
            return lstEventBasedForm;
        }
        #endregion
        #region "Get Form Event Based Template"
        private string GetFormEventBasedTemplate(Int32 companyId, string moduleDb, string adminDb, string loginId, Int32 formId, string dataElementId, string mainEvent, string subEvent)
        {
            string layoutTemplate = String.Empty;
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[1] = new SqlParameter("@USER_LOGIN", loginId);
            parameters[2] = new SqlParameter("@ADMIN_DB", adminDb);
            parameters[3] = new SqlParameter("@FORM_ID", formId);
            parameters[4] = new SqlParameter("@DataElementId", dataElementId);
            parameters[5] = new SqlParameter("@MAIN_EVENT", mainEvent);
            parameters[6] = new SqlParameter("@SUB_EVENT", subEvent);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_INSIDER_FORM", moduleDb, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        layoutTemplate = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_TEMPLATE"])) ? Convert.ToString(dr["FORM_TEMPLATE"]) : String.Empty;
                    }
                }
            }
            return layoutTemplate;
        }
        #endregion
        #region "Get All Email Events"
        private List<EventBasedForm> GetAllEmailEvents(Int32 companyId, string moduleDb, string adminDb, string loginId, string dataElementId, string eventType, string subEventType)
        {
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[1] = new SqlParameter("@MODE", "GET_ALL_EMAIL_EVENTS");
            parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[3] = new SqlParameter("@MAIN_EVENT", eventType);
            parameters[4] = new SqlParameter("@SUB_EVENT", subEventType);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);

            List<EventBasedForm> lstEventBasedForm = new List<EventBasedForm>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        EventBasedForm objEventBasedForm = new EventBasedForm();
                        objEventBasedForm.formId = !String.IsNullOrEmpty(Convert.ToString(dr["TEMPLATE_ID"])) ? Convert.ToInt32(dr["TEMPLATE_ID"]) : 0;
                        objEventBasedForm.mainEvent = !String.IsNullOrEmpty(Convert.ToString(dr["TEMPLATE_SUBJECT"])) ? Convert.ToString(dr["TEMPLATE_SUBJECT"]) : String.Empty;
                        //objEventBasedForm.subEvent = !String.IsNullOrEmpty(Convert.ToString(dr["TEMPLATE_BODY"])) ? Convert.ToString(dr["TEMPLATE_BODY"]) : String.Empty;
                        objEventBasedForm.formTemplate = GetEmailEventBasedTemplate(companyId, moduleDb, adminDb, loginId, objEventBasedForm.formId, dataElementId, eventType, "");
                        lstEventBasedForm.Add(objEventBasedForm);
                    }

                }
            }

            return lstEventBasedForm;
        }
        #endregion
        #region "Get Email Event Based Template"
        private string GetEmailEventBasedTemplate(Int32 companyId, string moduleDb, string adminDb, string loginId, Int32 formId, string dataElementId, string mainEvent, string subEvent)
        {
            string layoutTemplate = String.Empty;
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@COMPANY_ID", companyId);
            parameters[1] = new SqlParameter("@USER_LOGIN", loginId);
            parameters[2] = new SqlParameter("@ADMIN_DB", adminDb);
            parameters[3] = new SqlParameter("@FORM_ID", formId);
            parameters[4] = new SqlParameter("@DataElementId", dataElementId);
            parameters[5] = new SqlParameter("@MAIN_EVENT", mainEvent);
            parameters[6] = new SqlParameter("@SUB_EVENT", subEvent);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_INSIDER_EMAIL", moduleDb, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        layoutTemplate = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_TEMPLATE"])) ? Convert.ToString(dr["FORM_TEMPLATE"]) : String.Empty;
                    }

                }
            }

            return layoutTemplate;
        }
        #endregion
        #region "Create Form Logs"
        private void CreateFormLogs(Int32 formId, string dataElementId, string fileName, string createdBy, Int32 companyId, string moduleDb)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@MODE", "CREATE_FORM_LOGS");
            parameters[1] = new SqlParameter("@FORM_ID", formId);
            parameters[2] = new SqlParameter("@DataElementId", dataElementId);
            parameters[3] = new SqlParameter("@FILE_NAME", fileName);
            parameters[4] = new SqlParameter("@CREATED_BY", createdBy);
            parameters[5] = new SqlParameter("@COMPANY_ID", companyId);

            SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);
        }
        #endregion
        public PreClearanceRequestResponse AddNonComplianceBrokerNote(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[23];
                parameters[0] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[1] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[2] = new SqlParameter("@BROKER_NOTE", objPreClearanceRequest.BrokerNote);
                parameters[3] = new SqlParameter("@VALUE_PER_SHARE", objPreClearanceRequest.ValuePerShare);
                parameters[4] = new SqlParameter("@TOTAL_AMOUNT", objPreClearanceRequest.TotalAmount);
                parameters[5] = new SqlParameter("@ACTUAL_TRANSACTION_DATE", ConvertDate(objPreClearanceRequest.ActualTransactionDate));
                parameters[6] = new SqlParameter("@Mode", "ADD_NON_COMPLIANCE_BROKER_NOTE");
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;
                parameters[8] = new SqlParameter("@PRE_CLEARANCE_REQUEST_ID", objPreClearanceRequest.PreClearanceRequestId);
                parameters[9] = new SqlParameter("@ACTUAL_TRADE_QUANTITY", objPreClearanceRequest.ActualTradeQuantity);
                parameters[10] = new SqlParameter("@REMARKS", objPreClearanceRequest.remarks);
                parameters[11] = new SqlParameter("@RELATIVE_ID", objPreClearanceRequest.relativeId);
                parameters[12] = new SqlParameter("@PRE_CLEARANCE_REQUESTED_FOR", objPreClearanceRequest.relativeId);
                parameters[13] = new SqlParameter("@SECURITY_TYPE", objPreClearanceRequest.SecurityType);
                parameters[14] = new SqlParameter("@TRADE_COMPANY_ID", objPreClearanceRequest.TradeCompany);
                parameters[15] = new SqlParameter("@TRANSACTION_TYPE", objPreClearanceRequest.TransactionType);
                parameters[16] = new SqlParameter("@DEMAT_ACCOUNT", objPreClearanceRequest.DematAccount);
                parameters[17] = new SqlParameter("@SHARE_CURRENT_MARKET_PRICE", objPreClearanceRequest.shareCurrentMarketPrice);
                parameters[18] = new SqlParameter("@PROPOSED_TRANSACTION_THROUGH", objPreClearanceRequest.proposedTransactionThrough);
                parameters[19] = new SqlParameter("@EXCHANGE_TRADED_ON", objPreClearanceRequest.exchangeTradedOn);
                parameters[20] = new SqlParameter("@TRANSACTION_ID", objPreClearanceRequest.TransactionId);
                parameters[21] = new SqlParameter("@NON_COMPLIANCE_ID", objPreClearanceRequest.NonComplianceId);
                parameters[22] = new SqlParameter("@BROKER_DETAILS", objPreClearanceRequest.BrokerDetails);
                
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objPreClearanceRequest.PreClearanceRequestId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PRE_CLEARANCE_REQUEST_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["PRE_CLEARANCE_REQUEST_ID"]) : 0;
                        objPreClearanceRequest.brokerNoteId = Convert.ToInt32(parameters[7].Value);
                    }
                }

                string mainEvent = string.Empty;
                string subEvent = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["EXECUTED_STATUS"]) == "Closed")
                    {

                        if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                        {
                            mainEvent = "Transaction Without Pre-clearance";
                            subEvent = "Both";
                        }
                        else
                        {
                            mainEvent = "Transaction Without Pre-clearance";
                        }
                    }
                    else
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["EXCEEDED_LIMIT"]) == "Y")
                        {
                            mainEvent = "Transaction Without Pre-clearance";
                        }
                        else
                        {

                        }
                    }

                }


                if (!string.IsNullOrEmpty(mainEvent))
                {


                    List<EventBasedForm> lstAllFormEvents = GetAllFormEvents(objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.LoginId, Convert.ToString(objPreClearanceRequest.brokerNoteId), mainEvent, subEvent);
                    List<string> allAttachments = new List<string>();

                    foreach (var obj in lstAllFormEvents)
                    {
                        obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";

                        if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                        {
                            string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                            if (!String.IsNullOrEmpty(docFileName))
                            {
                                String filePath = "/InsiderTrading/emailAttachment/";
                                ConvertDocToPDF(docFileName, obj.fileName, filePath);
                                CreateFormLogs(obj.formId, Convert.ToString(objPreClearanceRequest.brokerNoteId), obj.fileName, objPreClearanceRequest.LoginId, objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE);
                            }
                        }
                        else
                        {
                            obj.fileName = string.Empty;
                        }
                        if (!String.IsNullOrEmpty(obj.fileName))
                        {
                            allAttachments.Add(obj.fileName);
                            objPreClearanceRequest.lstFormUrl = allAttachments;

                            bool status = true;
                            InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                                objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.CompanyId, objPreClearanceRequest.LoginId,
                                objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.businessUnitId
                            );
                            String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                                ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                            List<string> attachments = new List<string>();
                            if (objPreClearanceRequest.lstFormUrl != null)
                            {
                                if (objPreClearanceRequest.lstFormUrl.Count > 0)
                                {
                                    foreach (string formUrl in objPreClearanceRequest.lstFormUrl)
                                    {
                                        attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                                    }
                                }
                            }
                            String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                            EmailSender.SendMail(
                                objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                                "Trade disclosure", objPreClearanceRequest.CompanyId.ToString(), strCC,
                                objPreClearanceRequest.brokerNoteId.ToString(), objPreClearanceRequest.LoginId
                            );
                            oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;

                            if (status)
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been shared with your Compliance Officer!";
                            }
                            else
                            {
                                oPreClearanceRequest.StatusFl = true;
                                oPreClearanceRequest.Msg = "Sending mail failed.";
                            }
                        }
                    }
                    if (subEvent == "Both")
                    {
                        SqlParameter[] parameters1 = new SqlParameter[1];
                        parameters1[0] = new SqlParameter("@BrokerNoteId", objPreClearanceRequest.brokerNoteId);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORMC_LOGS", objPreClearanceRequest.MODULE_DATABASE, parameters1);
                    }
                }
                else
                {
                    oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Trade details updated successfully !";

                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse UpdateNonComplianceTask(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@Mode", "ADD_NON_COMPLIANCE_REMARKS");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@TRANSACTION_ID", objPreClearanceRequest.TransactionId);
                parameters[3] = new SqlParameter("@NON_COMPLIANCE_ID", objPreClearanceRequest.NonComplianceId);
                parameters[4] = new SqlParameter("@REMARKS", objPreClearanceRequest.NonComplianceRemarks);


                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Data has been updated successfully !";
                oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse SubmitEsopFormC(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                objPreClearanceRequest.brokerNoteId = objPreClearanceRequest.Id;

                List<EventBasedForm> lstAllFormEvents = GetAllFormEvents(objPreClearanceRequest.CompanyId, objPreClearanceRequest.MODULE_DATABASE, objPreClearanceRequest.ADMIN_DATABASE, objPreClearanceRequest.LoginId, Convert.ToString(objPreClearanceRequest.brokerNoteId), "Esop Transaction", "");
                objPreClearanceRequest.lstFormUrl = new List<string>();
                foreach (EventBasedForm form in lstAllFormEvents)
                {
                    objPreClearanceRequest.lstFormUrl.Add(Convert.ToString(form.formId) + "~" + form.formName);
                }
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Data has been added successfully !";
                oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetTransactionalEsopForms(PreClearanceRequest objRequest)
        {
            try
            {
                List<string> allAttachments = new List<string>();
                List<EventBasedForm> lstAllFormEvents = null;

                lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Esop Transaction", "");


                foreach (var obj in lstAllFormEvents)
                {
                    obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";
                    if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                    {
                        string docFileName = CreateWordDocument(obj.formTemplate, obj.fileName, obj.formOrientation);
                        //string docFileName = CreateDocFileLandscape(obj.formTemplate, obj.fileName);
                        if (!String.IsNullOrEmpty(docFileName))
                        {
                            String filePath = "/InsiderTrading/emailAttachment/";
                            ConvertDocToPDF(docFileName, obj.fileName, filePath);
                        }
                    }
                    else
                    {
                        obj.fileName = String.Empty;
                    }
                    if (!String.IsNullOrEmpty(obj.fileName))
                    {
                        string sFileNm = obj.fileName.Replace(".pdf", ".docx");
                        obj.fileName = sFileNm;
                        allAttachments.Add("/InsiderTrading/emailAttachment/" + obj.fileName);
                    }
                }
                PreClearanceRequest objPreclearanceRequest = new PreClearanceRequest();
                objPreclearanceRequest.lstFormUrl = allAttachments;

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.PreClearanceRequest = objPreclearanceRequest;
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Form has been downloaded successfully";
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse SaveAndGetTransactionalEsopForms(PreClearanceRequest objRequest)
        {
            try
            {
                List<string> allAttachments = new List<string>();
                List<EventBasedForm> lstAllFormEvents = null;
                List<EventBasedForm> lstAllEmailEvents = null;

                lstAllFormEvents = GetAllFormEvents(objRequest.CompanyId, objRequest.MODULE_DATABASE, objRequest.ADMIN_DATABASE, objRequest.LoginId, Convert.ToString(objRequest.brokerNoteId), "Esop Transaction", "");

                foreach (var obj in lstAllFormEvents)
                {
                    obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";

                    if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                    {
                        string docFileName = CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation);
                        if (!String.IsNullOrEmpty(docFileName))
                        {
                            String filePath = "/InsiderTrading/emailAttachment/";
                            ConvertDocToPDF(docFileName, obj.fileName, filePath);
                            CreateEsopFormLogs(objRequest.brokerNoteId, obj.fileName, objRequest.MODULE_DATABASE);
                        }
                    }
                    else
                    {
                        obj.fileName = string.Empty;
                    }
                    if (!String.IsNullOrEmpty(obj.fileName))
                    {
                        allAttachments.Add(obj.fileName);
                    }
                }
                objRequest.lstFormUrl = allAttachments;

                bool status = true;
                InsiderTrading.Model.User objUserX = CommonFunctions.GetITApprover(
                    objRequest.MODULE_DATABASE, objRequest.CompanyId, objRequest.LoginId,
                    objRequest.ADMIN_DATABASE, objRequest.businessUnitId
                            );
                String msg = @"To,<br/>The Compliance Officer<br/>" + objUserX.companyName +
                    ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of the Company.<br/><br/>Regards,<br/>" + objUserX.USER_NM + "";
                List<string> attachments = new List<string>();
                if (objRequest.lstFormUrl != null)
                {
                    if (objRequest.lstFormUrl.Count > 0)
                    {
                        foreach (string formUrl in objRequest.lstFormUrl)
                        {
                            attachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + formUrl)));
                        }
                    }
                }
                String strCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                EmailSender.SendMail(
                    objUserX.approverEmail, "Trading Details Update By " + objUserX.USER_NM, msg, attachments,
                    "Trade disclosure", objRequest.CompanyId.ToString(), strCC,
                    objRequest.brokerNoteId.ToString(), objRequest.LoginId
                );
                //EmailHelper email = new EmailHelper();
                //status = email.SendInsiderEsopFormToCO(objRequest);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                if (status)
                {

                    SqlParameter[] parameters1 = new SqlParameter[2];
                    parameters1[0] = new SqlParameter("@BrokerNoteId", objRequest.brokerNoteId);
                    parameters1[0] = new SqlParameter("@MODE", "ESOP");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORMC_LOGS", objRequest.MODULE_DATABASE, parameters1);

                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Forms have been submitted successfully! A copy has been also shared on your email id for reference!";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "Something went wrong. Please try again or contact administrator!";
                }
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LoginId, 5, objRequest.CompanyId);
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        private void CreateEsopFormLogs(Int32 Id, string fileName, string moduleDb)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@MODE", "CREATE_ESOP_FORM_LOGS");
            parameters[1] = new SqlParameter("@ID", Id);
            parameters[2] = new SqlParameter("@FILENAME", fileName);
            parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[3].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", moduleDb, parameters);
        }
        public PreClearanceRequestResponse GetPreClearanceRequestOtherCompanyList(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Mode", "GET_ALL_PRE_CLEARANCE_REQUEST_OTHER_COMPANY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objPreClearanceRequest.LoginId);
                parameters[4] = new SqlParameter("@ADMIN_DATABASE", objPreClearanceRequest.ADMIN_DATABASE);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRE_CLEARANCE_REQUEST", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        PreClearanceRequest obj = new PreClearanceRequest();
                        obj.PreClearanceRequestId = !String.IsNullOrEmpty(Convert.ToString(rdr["ID"])) ? Convert.ToInt32(rdr["ID"]) : 0;
                        obj.PreClearanceRequestedForName = Convert.ToString(rdr["PRE_CLEARANCE_FOR_NAME"]);
                        obj.relationName = Convert.ToString(rdr["RELATION_NM"]);
                        obj.PreClearanceRequestedFor = !String.IsNullOrEmpty(Convert.ToString(rdr["REQUESTED_FOR"])) ? Convert.ToInt32(rdr["REQUESTED_FOR"]) : 0;
                        obj.SecurityTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE_NAME"]))) ? Convert.ToString(rdr["SECURITY_TYPE_NAME"]) : String.Empty;
                        obj.TradeCompanyName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_NAME"]))) ? Convert.ToString(rdr["TRADE_COMPANY_NAME"]) : String.Empty;
                        obj.TradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_QUANTITY"]))) ? Convert.ToString(rdr["TRADE_QUANTITY"]) : String.Empty;
                        obj.TransactionTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE_NAME"]))) ? Convert.ToString(rdr["TRANSACTION_TYPE_NAME"]) : String.Empty;
                        obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_DATE"]))) ? Convert.ToString(rdr["TRADE_DATE"]) : String.Empty;
                        obj.SecurityType = !String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE"])) ? Convert.ToInt32(rdr["SECURITY_TYPE"]) : 0;
                        obj.TradeCompany = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_ID"])) ? Convert.ToInt32(rdr["TRADE_COMPANY_ID"]) : 0;
                        obj.TransactionType = !String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE"])) ? Convert.ToInt32(rdr["TRANSACTION_TYPE"]) : 0;
                        obj.Status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        obj.reviewedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_ON"])) ? Convert.ToString(rdr["REVIEWED_ON"]) : String.Empty;
                        obj.reviewedBy = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_BY"])) ? Convert.ToString(rdr["REVIEWED_BY"]) : String.Empty;
                        obj.shareCurrentMarketPrice = !String.IsNullOrEmpty(Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"])) ? Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"]) : String.Empty;
                        obj.proposedTransactionThrough = !String.IsNullOrEmpty(Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"])) ? Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"]) : String.Empty;

                        oPreClearanceRequest.AddObject(obj);
                    }
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "No data found !";
                }
                rdr.Close();
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse GetPreClearanceRequestOtherCompanyReport(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@Mode", "REPORT_PRE_CLEARANCE_REQUEST_OTHER_COMPANY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPreClearanceRequest.CompanyId);
                parameters[3] = new SqlParameter("@TRADE_FROM", ConvertDate(objPreClearanceRequest.tradingFrom));
                parameters[4] = new SqlParameter("@TRADE_TO", ConvertDate(objPreClearanceRequest.tradingTo));
                parameters[5] = new SqlParameter("@USER_ID", objPreClearanceRequest.userId);
                parameters[6] = new SqlParameter("@ADMIN_DB", objPreClearanceRequest.ADMIN_DATABASE);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objPreClearanceRequest.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        PreClearanceRequest obj = new PreClearanceRequest();
                        obj.PreClearanceRequestId = !String.IsNullOrEmpty(Convert.ToString(rdr["ID"])) ? Convert.ToInt32(rdr["ID"]) : 0;
                        obj.PreClearanceRequestedForName = Convert.ToString(rdr["PRE_CLEARANCE_FOR_NAME"]);
                        obj.relationName = Convert.ToString(rdr["RELATION_NM"]);
                        obj.PreClearanceRequestedFor = !String.IsNullOrEmpty(Convert.ToString(rdr["REQUESTED_FOR"])) ? Convert.ToInt32(rdr["REQUESTED_FOR"]) : 0;
                        obj.SecurityTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE_NAME"]))) ? Convert.ToString(rdr["SECURITY_TYPE_NAME"]) : String.Empty;
                        obj.TradeCompanyName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_NAME"]))) ? Convert.ToString(rdr["TRADE_COMPANY_NAME"]) : String.Empty;
                        obj.TradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_QUANTITY"]))) ? Convert.ToString(rdr["TRADE_QUANTITY"]) : String.Empty;
                        obj.TransactionTypeName = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE_NAME"]))) ? Convert.ToString(rdr["TRANSACTION_TYPE_NAME"]) : String.Empty;
                        obj.TradeDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_DATE"]))) ? Convert.ToString(rdr["TRADE_DATE"]) : String.Empty;
                        obj.SecurityType = !String.IsNullOrEmpty(Convert.ToString(rdr["SECURITY_TYPE"])) ? Convert.ToInt32(rdr["SECURITY_TYPE"]) : 0;
                        obj.TradeCompany = !String.IsNullOrEmpty(Convert.ToString(rdr["TRADE_COMPANY_ID"])) ? Convert.ToInt32(rdr["TRADE_COMPANY_ID"]) : 0;
                        obj.TransactionType = !String.IsNullOrEmpty(Convert.ToString(rdr["TRANSACTION_TYPE"])) ? Convert.ToInt32(rdr["TRANSACTION_TYPE"]) : 0;
                        obj.Status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        obj.reviewedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_ON"])) ? Convert.ToString(rdr["REVIEWED_ON"]) : String.Empty;
                        obj.reviewedBy = !String.IsNullOrEmpty(Convert.ToString(rdr["REVIEWED_BY"])) ? Convert.ToString(rdr["REVIEWED_BY"]) : String.Empty;
                        obj.shareCurrentMarketPrice = !String.IsNullOrEmpty(Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"])) ? Convert.ToString(rdr["SHARE_CURRENT_MARKET_PRICE"]) : String.Empty;
                        obj.proposedTransactionThrough = !String.IsNullOrEmpty(Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"])) ? Convert.ToString(rdr["PROPOSED_TRANSACTION_THROUGH"]) : String.Empty;

                        oPreClearanceRequest.AddObject(obj);
                    }
                    oPreClearanceRequest.StatusFl = true;
                    oPreClearanceRequest.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oPreClearanceRequest.StatusFl = false;
                    oPreClearanceRequest.Msg = "No data found !";
                }
                rdr.Close();
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
        public PreClearanceRequestResponse UpdateTransactionHistory(PreClearanceRequest objPreClearanceRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@Mode", "UPDATE_TRANSACTION_HISTORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@TRANSACTION_ID", objPreClearanceRequest.TransactionId);
                parameters[3] = new SqlParameter("@STATUS", objPreClearanceRequest.Status);
                parameters[4] = new SqlParameter("@REMARKS", objPreClearanceRequest.remarks);


                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objPreClearanceRequest.MODULE_DATABASE, parameters);

                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = true;
                oPreClearanceRequest.Msg = "Data has been updated successfully !";
                oPreClearanceRequest.PreClearanceRequest = objPreClearanceRequest;
                return oPreClearanceRequest;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse oPreClearanceRequest = new PreClearanceRequestResponse();
                oPreClearanceRequest.StatusFl = false;
                oPreClearanceRequest.Msg = "Processing failed, because of system error !";
                return oPreClearanceRequest;
            }
        }
    }
}