using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class TransactionSubTypeMasterRepository
    {
        #region "Get All Transaction Sub Type"
        public TransactionSubTypeMasterResponse GetTransactionSubTypeMaster(TransactionSubTypeMaster objTransactionSubTypeMaster)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[2];
                parameter[0] = new SqlParameter("@MODE", "GET_TRANSACTION_SUB_TYPE_MSTR");
                parameter[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[1].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_HISTORY", objTransactionSubTypeMaster.MODULE_DATABASE, parameter);
                TransactionSubTypeMasterResponse objResponse = new TransactionSubTypeMasterResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TransactionSubTypeMaster obj = new TransactionSubTypeMaster();
                            obj.transactionSubTypeMasterId = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                            obj.type = !String.IsNullOrEmpty(Convert.ToString(dr["TYPE"])) ? Convert.ToString(dr["TYPE"]) : String.Empty;
                            obj.category = !String.IsNullOrEmpty(Convert.ToString(dr["CATEGORY"])) ? Convert.ToString(dr["CATEGORY"]) : String.Empty;
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
                TransactionSubTypeMasterResponse objResponse = new TransactionSubTypeMasterResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion
    }
}