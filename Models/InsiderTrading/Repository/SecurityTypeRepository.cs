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
    public class SecurityTypeRepository : IRequiresSessionState
    {
        public SecurityTypeResponse GetTradableSecurity(SecurityType oSecType)
        {
            try
            {
                SecurityTypeResponse oResponse = new SecurityTypeResponse();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@CompanyId", oSecType.CompanyId);
                parameters[1] = new SqlParameter("@UserLogin", oSecType.LoggedInUser);
                parameters[2] = new SqlParameter("@Mode", "GET_TRADABLE_SECURITY");

                DataSet dsSecType=SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SECURITY_TYPE_OPERATION", oSecType.MODULE_DATABASE, parameters);
                DataTable dtSecType = dsSecType.Tables[0];
                if (dtSecType.Rows.Count > 0)
                {
                    List<SecurityType> lstSecType = new List<SecurityType>();
                    foreach(DataRow drSecType in dtSecType.Rows)
                    {
                        SecurityType o = new SecurityType();
                        o.Id = Convert.ToInt32(drSecType["ID"]);
                        o.Name = Convert.ToString(drSecType["NAME"]);
                        lstSecType.Add(o);
                    }
                    oResponse.SecurityTypeList = lstSecType;
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
                SecurityTypeResponse oResponse = new SecurityTypeResponse();
                oResponse.StatusFl = false;
                oResponse.Msg = "Processing failed, because of system error !";
                return oResponse;
            }
        }
    }
}