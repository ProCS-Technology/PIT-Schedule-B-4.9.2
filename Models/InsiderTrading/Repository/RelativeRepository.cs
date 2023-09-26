using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class RelativeRepository : IRequiresSessionState
    {
        public RelativeResponse GetRelativeInformationById(Relative objRelative)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@RELATIVE_ID", objRelative.ID);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_RELATIVE_INFO_BY_ID");

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objRelative.MODULE_DATABASE, parameters);
                RelativeResponse objRelativeResponse = new RelativeResponse();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    Relative relative = new Relative();

                    relative.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;

                    objRelativeResponse.StatusFl = true;
                    objRelativeResponse.Msg = "Relation Data has been fetched successfully !";
                    objRelativeResponse.Relative = relative;
                }
                else
                {
                    objRelativeResponse.StatusFl = false;
                    objRelativeResponse.Msg = "No data found !";
                }
                return objRelativeResponse;
            }
            catch (Exception ex)
            {
                RelativeResponse objRelativeResponse = new RelativeResponse();
                objRelativeResponse.StatusFl = false;
                objRelativeResponse.Msg = "Processing failed, because of system error !";
                return objRelativeResponse;
            }
        }
        
    }
}