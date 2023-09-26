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
    public class DashboardUpsiRepository : IRequiresSessionState
    {
        #region "Get Upsi Count"
        public DashboardUpsiResponse GetUpsiCount(DashboardUpsi objDashboard)
        {
            try
            {
                DashboardUpsiResponse objDashboardResponse = new DashboardUpsiResponse();
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@LOGIN_ID", objDashboard.loginId);
                parameters[1] = new SqlParameter("@CompanyId", objDashboard.companyId);
                if (objDashboard.UpsiFrom == "" || objDashboard.UpsiFrom == null)
                {
                    parameters[2] = new SqlParameter("@UPSI_FROM", DBNull.Value);
                }
                else
                {
                    parameters[2] = new SqlParameter("@UPSI_FROM", FormatHelper.FormatDate(objDashboard.UpsiFrom));
                }

                if (objDashboard.UpsiFrom == "" || objDashboard.UpsiFrom == null)
                {
                    parameters[3] = new SqlParameter("@UPSI_TO", DBNull.Value);
                }
                else
                {
                    parameters[3] = new SqlParameter("@UPSI_TO", FormatHelper.FormatDate(objDashboard.UpsiTo));
                }

                DataSet dss = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_COUNT_FILTER", objDashboard.MODULE_DATABASE, parameters);


                if (dss.Tables[0].Rows.Count > 0)
                {

                    DashboardUpsi obj = new DashboardUpsi();
                    obj.TotalActiveUPSIEvent = Convert.ToInt32(dss.Tables[0].Rows[0]["TotalActiveUPSIEvent"]);
                    obj.TotalInactiveUPSIEvent = Convert.ToInt32(dss.Tables[0].Rows[0]["TotalInactiveUPSIEvent"]);
                    obj.TotalAbandonedUPSIEvent = Convert.ToInt32(dss.Tables[0].Rows[0]["TotalAbandonedUPSIEvent"]);
                    obj.TotalPublishedUPSIEvent = Convert.ToInt32(dss.Tables[0].Rows[0]["TotalPublishedUPSIEvent"]);
                    obj.TotalUPSIEvent = Convert.ToInt32(dss.Tables[0].Rows[0]["TotalUPSIEvent"]);
                    objDashboardResponse.DashboardUpsi = obj;
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
                DashboardUpsiResponse objDashboardResponse = new DashboardUpsiResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }
        #endregion
    }
} 