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
    public class NonCompliantTaskRepository
    {
        public NonCompliantTaskResponse GetAllNonCompliant(NonCompliantTask objNonCompliant)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objNonCompliant.companyId);
                parameters[1] = new SqlParameter("@Mode", "GET_ALL_NC");
                parameters[2] = new SqlParameter("@CREATED_BY", objNonCompliant.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_NC", objNonCompliant.MODULE_DATABASE, parameters);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        NonCompliantTask obj = new NonCompliantTask();
                        obj.id = Convert.ToInt32(dr["ID"]);
                        obj.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(dr["AMOUNT"])) ? Convert.ToInt32(dr["AMOUNT"]) : 0;
                        obj.user = new User
                        {
                            LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(dr["USER_ID"])) ? Convert.ToString(dr["USER_ID"]) : String.Empty,
                            USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NAME"])) ? Convert.ToString(dr["USER_NAME"]) : String.Empty,
                            USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty,
                            panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty
                        };
                        obj.relative = new Relative
                        {
                            relativeName = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_NAME"])) ? Convert.ToString(dr["RELATIVE_NAME"]) : "Self"
                        };
                        obj.nonCompliantType = !String.IsNullOrEmpty(Convert.ToString(dr["TYPE"])) ? Convert.ToString(dr["TYPE"]) : String.Empty;
                        obj.asOfDate = !String.IsNullOrEmpty(Convert.ToString(dr["TRADE_DATE"])) ? Convert.ToString(dr["TRADE_DATE"]) : String.Empty;
                        obj.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO_NO"])) ? Convert.ToString(dr["FOLIO_NO"]) : String.Empty;
                        oNonCompliantTask.AddObject(obj);
                    }
                    oNonCompliantTask.StatusFl = true;
                    oNonCompliantTask.Msg = "Document Data has been fetched successfully !";
                }
                else
                {
                    oNonCompliantTask.StatusFl = false;
                    oNonCompliantTask.Msg = "No data found !";
                }

                return oNonCompliantTask;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                oNonCompliantTask.StatusFl = false;
                oNonCompliantTask.Msg = "Processing failed, because of system error !";
                return oNonCompliantTask;
            }
        }
        public NonCompliantTaskResponse CloseNonCompliantTask(NonCompliantTask objNonCompliant)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@COMPANY_ID", objNonCompliant.companyId);
                parameters[1] = new SqlParameter("@Mode", "CLOSE_NC_TASK");
                parameters[2] = new SqlParameter("@CREATED_BY", objNonCompliant.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4] = new SqlParameter("@BROKER_NOTE", objNonCompliant.brokerNote);
                parameters[5] = new SqlParameter("@ACTUAL_TRANSACTION_DATE", ConvertDate(objNonCompliant.actualTransactionDate));
                parameters[6] = new SqlParameter("@TRADE_QTY", objNonCompliant.tradeQuantity);
                parameters[7] = new SqlParameter("@VALUE_PER_SHARE", objNonCompliant.valuePerShare);
                parameters[8] = new SqlParameter("@TOTAL_SHARE_VALUE", objNonCompliant.totalShareValue);
                parameters[9] = new SqlParameter("@ID", objNonCompliant.id);

                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_NC", objNonCompliant.MODULE_DATABASE, parameters);

                oNonCompliantTask.StatusFl = true;
                oNonCompliantTask.Msg = "Document Data has been fetched successfully !";

                return oNonCompliantTask;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                oNonCompliantTask.StatusFl = false;
                oNonCompliantTask.Msg = "Processing failed, because of system error !";
                return oNonCompliantTask;
            }
        }
        public NonCompliantTaskResponse RunNowCompliantFinder(NonCompliantTask objNonCompliant)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objNonCompliant.companyId);
                parameters[1] = new SqlParameter("@AS_OF_DATE_CURRENT", FormatHelper.FormatDate(objNonCompliant.asOfDate));
                parameters[2] = new SqlParameter("@HdrId", objNonCompliant.id);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_NON_COMPLIANCE_FINDER", objNonCompliant.MODULE_DATABASE, parameters);

                //parameters[0] = new SqlParameter("@CompanyId", objNonCompliant.companyId);
                //parameters[1] = new SqlParameter("@HdrId", objNonCompliant.id);
                //parameters[2] = new SqlParameter("@AdminDb", objNonCompliant.ADMIN_DATABASE);
                //DataSet dsNonCompliant = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_GENERATE_NON_COMPLIANCE", objNonCompliant.MODULE_DATABASE, parameters);

                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                //if (dsNonCompliant.Tables.Count > 0 && dsNonCompliant.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsNonCompliant.Tables[0].Rows)
                //    {
                //        BenposNonCompliant obj = new BenposNonCompliant();
                //        obj.ID = Convert.ToInt32(dr["ID"]);
                //        obj.UserNm = Convert.ToString(dr["USER_NM"]);
                //        obj.RelativeNm = Convert.ToString(dr["RELATIVE_NAME"]);
                //        obj.RelationNm = Convert.ToString(dr["RELATION"]);
                //        if (Convert.ToString(dr["SELF_PAN"]) == "")
                //        {
                //            obj.PAN = Convert.ToString(dr["RELATIVE_PAN"]);
                //        }
                //        else
                //        {
                //            obj.PAN = Convert.ToString(dr["SELF_PAN"]);
                //        }
                //        obj.Folio = Convert.ToString(dr["FOLIO"]);
                //        obj.Qty = Convert.ToInt64(dr["QTY"]);
                //        obj.TradeVal = Convert.ToDecimal(dr["VALUE"]);
                //        obj.NonComplianceType = Convert.ToString(dr["NON_COMPLIANCE_TYPE"]);
                //        oNonCompliantTask.AddBenposObject(obj);
                //    }
                //}

                oNonCompliantTask.StatusFl = true;
                oNonCompliantTask.Msg = "Benpos uploaded succefully !";
                //DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_NON_COMPLIANCE_FINDER", objNonCompliant.MODULE_DATABASE, parameters);
                //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        if (objNonCompliant.companyId == Convert.ToInt32(dr["COMPANY_ID"]))
                //        {
                //            NonCompliantTask obj = new NonCompliantTask();
                //            //      obj.id = Convert.ToInt32(dr["ID"]);
                //            obj.differenceInShare = !String.IsNullOrEmpty(Convert.ToString(dr["Difference_In_No_Of_Shares"])) ? Convert.ToString(dr["Difference_In_No_Of_Shares"]) : String.Empty;
                //            obj.user = new User
                //            {
                //                LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(dr["USER_LOGIN"])) ? Convert.ToString(dr["USER_LOGIN"]) : String.Empty,
                //                USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty,
                //                //    USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty,
                //                panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty
                //            };
                //            obj.relative = new Relative
                //            {
                //                relativeName = !String.IsNullOrEmpty(Convert.ToString(dr["RELATIVE_NAME"])) ? Convert.ToString(dr["RELATIVE_NAME"]) : "Self"
                //            };
                //            obj.isFolioNoDeclared = !String.IsNullOrEmpty(Convert.ToString(dr["IS_FOLIO_NO_DECLARED"])) ? Convert.ToString(dr["IS_FOLIO_NO_DECLARED"]) : String.Empty;
                //            obj.isNonCompliant = !String.IsNullOrEmpty(Convert.ToString(dr["IS_NON_COMPLIANT"])) ? Convert.ToString(dr["IS_NON_COMPLIANT"]) : String.Empty;
                //            obj.numberOfShareAsPerBenpos = !String.IsNullOrEmpty(Convert.ToString(dr["NO_OF_SHARES_AS_PER_BENPOS"])) ? Convert.ToInt32(dr["NO_OF_SHARES_AS_PER_BENPOS"]) : 0;
                //            obj.numberOfShareAsPerInitialHolding = !String.IsNullOrEmpty(Convert.ToString(dr["NO_OF_SHARES_AS_PER_INITIAL_HOLDING"])) ? Convert.ToInt32(dr["NO_OF_SHARES_AS_PER_INITIAL_HOLDING"]) : 0;
                //            obj.folioNumber = !String.IsNullOrEmpty(Convert.ToString(dr["FOLIO_NO"])) ? Convert.ToString(dr["FOLIO_NO"]) : String.Empty;
                //            oNonCompliantTask.AddObject(obj);
                //        }
                //    }
                //    oNonCompliantTask.StatusFl = true;
                //    oNonCompliantTask.Msg = "Document Data has been fetched successfully !";
                //}
                //else
                //{
                //    oNonCompliantTask.StatusFl = false;
                //    oNonCompliantTask.Msg = "Benpos Uploaded successfully !";
                //}
                //TradeBifurcationTaskClose(objNonCompliant);
                return oNonCompliantTask;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                oNonCompliantTask.StatusFl = false;
                oNonCompliantTask.Msg = "Processing failed, because of system error !";
                return oNonCompliantTask;
            }
        }
        public NonCompliantTaskResponse SendEmailForNC(NonCompliantTask objNonCompliant)
        {
            try
            {
                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                oNonCompliantTask = GetAllNonCompliant(objNonCompliant);

                if (oNonCompliantTask.NonCompliantTaskList.Count > 0)
                {


                    foreach (NonCompliantTask obj in oNonCompliantTask.NonCompliantTaskList)
                    {
                        string msg = String.Empty;
                        if (obj.tradeQuantity == 0)
                        {
                            // Content will be different
                            msg += "Dear " + obj.user.USER_NM + ",<br/><br/>";
                            msg += "This is to inform that Pan " + obj.user.panNumber + "and Relative " + obj.relative.relativeName + " has exceeded the amount " + obj.tradeQuantity + "<br/><br/>";
                            msg += "Thanks & Regards,<br/>";
                            msg += "ProCS Technology";
                        }
                        else
                        {
                            // Content will be different
                            msg += "Dear " + obj.user.USER_NM + ",<br/><br/>";
                            msg += "This is to inform that Pan " + obj.user.panNumber + " and Relative " + obj.relative.relativeName + " has exceeded the amount " + obj.tradeQuantity + "<br/><br/>";
                            msg += "Thanks & Regards,<br/>";
                            msg += "ProCS Technology";
                        }
                        SmtpConfigResponse objSmtpConfigRes = new SmtpConfigResponse();
                        SmtpConfiguration objSmtpConfig = new SmtpConfiguration();

                        objSmtpConfig.COMPANY_ID = objNonCompliant.companyId;
                        objSmtpConfig.MODULE_DATABASE = objNonCompliant.MODULE_DATABASE;
                        SmtpConfigRepository smtpConfig = new SmtpConfigRepository();
                        objSmtpConfigRes = smtpConfig.GetSmtpConfigList(objSmtpConfig);

                        if (objSmtpConfigRes.SmtpConfigList.Count > 0)
                        {
                            bool ssl = objSmtpConfigRes.SmtpConfigList[0].SSL == "Yes" ? true : false;
                            bool userDefaultCredential = objSmtpConfigRes.SmtpConfigList[0].USER_DEFAULT_CREDENTIAL == "Yes" ? true : false;
                            string password = CryptorEngine.Decrypt(objSmtpConfigRes.SmtpConfigList[0].PASSWORD, true);
                            EmailSender.SendMail(
                                obj.user.USER_EMAIL, "Non Compliant Task", msg, null, "Non Compliant Task", objNonCompliant.companyId.ToString(),
                                "", "", objNonCompliant.createdBy
                            );
                            //EmailHelper.SendEmailForNonCompliant(
                            //    objSmtpConfigRes.SmtpConfigList[0].DEFAULT_EMAIL, obj.user.USER_EMAIL, "Non Compliant Task", msg,
                            //    objSmtpConfigRes.SmtpConfigList[0].SMTP_HOST_NAME, ssl, objSmtpConfigRes.SmtpConfigList[0].SMTP_USER_NAME,
                            //    password, userDefaultCredential, Convert.ToInt32(objSmtpConfigRes.SmtpConfigList[0].PORT)
                            //);
                        }

                    }
                    oNonCompliantTask.StatusFl = true;
                    oNonCompliantTask.Msg = "Email Sent Successfully";
                }
                else
                {
                    oNonCompliantTask.StatusFl = false;
                    oNonCompliantTask.Msg = "No data found to send notification !";
                }

                return oNonCompliantTask;
            }
            catch (Exception ex)
            {
                NonCompliantTaskResponse oNonCompliantTask = new NonCompliantTaskResponse();
                oNonCompliantTask.StatusFl = false;
                oNonCompliantTask.Msg = "Processing failed, because of system error !";
                return oNonCompliantTask;
            }
        }
        private void TradeBifurcationTaskClose(NonCompliantTask objNonCompliant)
        {
            decimal volumePrice = 0;
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@COMPANY_ID", objNonCompliant.companyId);
            parameters[1] = new SqlParameter("@MODE", "GET_VWAP");
            parameters[2] = new SqlParameter("@AS_OF_DATE", ConvertDate(objNonCompliant.asOfDate));
            parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[3].Direction = ParameterDirection.Output;

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objNonCompliant.MODULE_DATABASE, parameters);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        volumePrice = Convert.ToDecimal(!String.IsNullOrEmpty(Convert.ToString(dr["VWAP"])) ? Convert.ToString(dr["VWAP"]) : "0");
                    }
                }
            }

            TransactionHistory objTransactionHistory = new TransactionHistory
            {
                companyId = objNonCompliant.companyId,
                userLogin = objNonCompliant.createdBy,
                MODULE_DATABASE = objNonCompliant.MODULE_DATABASE,
                ADMIN_DATABASE = objNonCompliant.ADMIN_DATABASE,
                transactionId = objNonCompliant.id
            };
            TransactionHistoryResponse transactionHistoryResponse = new TransactionHistoryRepository().GetTransactionHistoryByAllUser(objTransactionHistory);

            if (transactionHistoryResponse.TransactionHistoryList != null)
            {

                foreach (TransactionHistory obj in transactionHistoryResponse.TransactionHistoryList)
                {
                    if (ConvertDate(obj.transactionDate) == ConvertDate(objNonCompliant.asOfDate))
                    {
                        List<TransactionSubTypeMaster> lstTransactionBifurcation = new List<TransactionSubTypeMaster>();
                        Int32 transactionId = obj.transactionId;
                        string transactionSubType = obj.transactionSubType;
                        string pan = obj.panNumber;
                        string folioNumber = obj.folioNumber;
                        Int32 tradeQuantity = 0;
                        string transactionDate = obj.transactionDate;

                        SqlParameter[] parameters1 = new SqlParameter[4];
                        parameters1[0] = new SqlParameter("@COMPANY_ID", objNonCompliant.companyId);
                        parameters1[1] = new SqlParameter("@MODE", "GET_ESOP_DETL");
                        parameters1[2] = new SqlParameter("@AS_OF_DATE", ConvertDate(objNonCompliant.asOfDate));
                        parameters1[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters1[3].Direction = ParameterDirection.Output;

                        DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_BENPOS", objNonCompliant.MODULE_DATABASE, parameters1);

                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                                {
                                    if (pan == Convert.ToString(dr1["PAN_NUMBER"]) && folioNumber == Convert.ToString(dr1["FOLIO_NO"]))
                                    {
                                        tradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(dr1["QTY"])) ? Convert.ToInt32(dr1["QTY"]) : 0);
                                        TransactionSubTypeMaster objTransactionSubTypeMaster = new TransactionSubTypeMaster
                                        {
                                            category = "ESOP/RSU/Scheme",
                                            tradeQuantity = (!String.IsNullOrEmpty(Convert.ToString(dr1["QTY"])) ? Convert.ToInt32(dr1["QTY"]) : 0),
                                            tradeValue = (!String.IsNullOrEmpty(Convert.ToString(dr1["VALUE"])) ? Convert.ToString(dr1["VALUE"]) : "0"),
                                            remarks = String.Empty,
                                            transactionDate = transactionDate
                                        };

                                        lstTransactionBifurcation.Add(objTransactionSubTypeMaster);

                                    }
                                }
                            }
                        }

                        if (transactionSubType.ToUpper() == "BUY")
                        {
                            if ((obj.tradeQuantity - tradeQuantity) > 0)
                            {
                                TransactionSubTypeMaster objTransactionSubTypeMaster = new TransactionSubTypeMaster
                                {
                                    category = "Buy",
                                    tradeQuantity = (obj.tradeQuantity - tradeQuantity),
                                    tradeValue = Convert.ToString((obj.tradeQuantity - tradeQuantity) * volumePrice),
                                    remarks = String.Empty,
                                    transactionDate = transactionDate
                                };

                                lstTransactionBifurcation.Add(objTransactionSubTypeMaster);
                            }
                        }
                        else if (transactionSubType.ToUpper() == "SELL")
                        {
                            if ((obj.tradeQuantity - tradeQuantity) > 0)
                            {
                                TransactionSubTypeMaster objTransactionSubTypeMaster = new TransactionSubTypeMaster
                                {
                                    category = "Sell",
                                    tradeQuantity = (obj.tradeQuantity - tradeQuantity),
                                    tradeValue = Convert.ToString((obj.tradeQuantity - tradeQuantity) * volumePrice),
                                    remarks = String.Empty,
                                    transactionDate = transactionDate
                                };

                                lstTransactionBifurcation.Add(objTransactionSubTypeMaster);
                            }
                        }

                        Dashboard objDashboard = new Dashboard
                        {
                            loginId = objNonCompliant.createdBy,
                            companyId = objNonCompliant.companyId,
                            transactionHistoryBifurcation = new TransactionHistory
                            {
                                transactionId = transactionId,
                                lstTransactionBifurcation = lstTransactionBifurcation
                            },
                            MODULE_DATABASE = objNonCompliant.MODULE_DATABASE
                        };

                        new DashboardRepository().UpdateTradeBifurcation(objDashboard);
                    }

                }
            }
        }
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
    }
}