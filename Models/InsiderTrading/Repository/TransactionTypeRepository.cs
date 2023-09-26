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
    public class TransactionTypeRepository : IRequiresSessionState
    {
        public TransactionTypeResponse GetTransactionType(TransactionType oTransType)
        {
            try
            {
                TransactionTypeResponse oResponse = new TransactionTypeResponse();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@CompanyId", oTransType.CompanyId);
                parameters[1] = new SqlParameter("@UserLogin", oTransType.LoggedInUser);
                parameters[2] = new SqlParameter("@Mode", "GET_TRANSACTION_TYPE");

                DataSet dsTransType = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRANSACTION_TYPE_OPERATION", oTransType.MODULE_DATABASE, parameters);
                DataTable dtTransType = dsTransType.Tables[0];
                if (dtTransType.Rows.Count > 0)
                {
                    List<TransactionType> lstTransType = new List<TransactionType>();
                    foreach (DataRow drTransType in dtTransType.Rows)
                    {
                        TransactionType o = new TransactionType();
                        o.Id = Convert.ToInt32(drTransType["ID"]);
                        o.Name = Convert.ToString(drTransType["NAME"]);
                        o.Nature = Convert.ToString(drTransType["TRANSACTION_NATURE"]);
                        o.AllowedWC = Convert.ToString(drTransType["ALLOWED_WC"]);
                        o.AllowedUPSI = Convert.ToString(drTransType["ALLOWED_UPSI"]);
                        o.WCCompliance = Convert.ToString(drTransType["WC_COMPLIANCE"]);
                        o.UPSICompliance = Convert.ToString(drTransType["UPSI_COMPLIANCE"]);
                        o.LimitCompliance = Convert.ToString(drTransType["LIMIT_COMPLIANCE"]);
                        lstTransType.Add(o);
                    }
                    oResponse.TransactionTypeList = lstTransType;
                    oResponse.StatusFl = true;
                    oResponse.Msg = "Success";
                }
                else
                {
                    oResponse.StatusFl = true;
                    oResponse.Msg = "No Data Found";
                }
                return oResponse;
            }
            catch (Exception ex)
            {
                TransactionTypeResponse oResponse = new TransactionTypeResponse();
                oResponse.StatusFl = false;
                oResponse.Msg = "Processing failed, because of system error !";
                return oResponse;
            }
        }
    }
}