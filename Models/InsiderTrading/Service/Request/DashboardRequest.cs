using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class DashboardRequest
    {
        private Dashboard _dashboard;

        public DashboardRequest()
        {

        }

        public DashboardRequest(Dashboard dashboard)
        {
            _dashboard = new Dashboard();
            _dashboard = dashboard;
        }
        #region "Get All Disclosure Request Count"
        public DashboardResponse GetOpenDisclosureRequest()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetOpenDisclosureRequest(_dashboard);
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
        public DashboardResponse GetAllPreClearanceRequestCount()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetAllPreClearanceRequestCount(_dashboard);
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
        public DashboardResponse GetAllPreClearanceRequestCountForAllUser()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetAllPreClearanceRequestCountForAllUser(_dashboard);
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
        public DashboardResponse GetCountOfAllTradeDetails()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetCountOfAllTradeDetails(_dashboard);
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
        public DashboardResponse GetCountOfMyTradeDetails()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetCountOfMyTradeDetails(_dashboard);
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
        public DashboardResponse GetTradeDetailsInfo()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetTradeDetailsInfo(_dashboard);
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
        public DashboardResponse GetMyActionable()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetMyActionable(_dashboard);
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

        #region "Update Trade Bifurcation"
        public DashboardResponse UpdateTradeBifurcation()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.UpdateTradeBifurcation(_dashboard);
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

        #region "Get My UPSITask"
        public DashboardResponse GetMyUPSITask()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetMyUPSITask(_dashboard);
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }

        public DashboardResponse GetMyUPSITaskById()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.GetMyUPSITaskById(_dashboard);
            }
            catch (Exception ex)
            {
                DashboardResponse objDashboardResponse = new DashboardResponse();
                objDashboardResponse.StatusFl = false;
                objDashboardResponse.Msg = "Processing failed due to system error!";
                return objDashboardResponse;
            }
        }

        public DashboardResponse UpdateUPSITaskById()
        {
            try
            {
                DashboardRepository oRepository = new DashboardRepository();
                return oRepository.UpdateUPSITaskById(_dashboard);
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
    }
}