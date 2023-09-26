using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class TradingRequestDetailsRequest
    {
        private PreClearanceRequest _tradingrequestdetails;
        public TradingRequestDetailsRequest()
        {
            _tradingrequestdetails = new PreClearanceRequest();
        }

        public TradingRequestDetailsRequest(PreClearanceRequest trd)
        {
            _tradingrequestdetails = new PreClearanceRequest();
            _tradingrequestdetails = trd;
        }

        public TradingRequestDetailsResponse GetTradingRequestDetailsList()
        {
            TradingRequestDetailsRepository oRepository = new TradingRequestDetailsRepository();
            return oRepository.GetTradingRequestDetailsList(_tradingrequestdetails);
        }
    }
}