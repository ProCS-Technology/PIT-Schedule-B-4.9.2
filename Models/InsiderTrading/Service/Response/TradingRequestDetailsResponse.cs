using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class TradingRequestDetailsResponse : BaseResponse
    {
        private PreClearanceRequest _tradingrequestdetails;
        private List<PreClearanceRequest> lstTradingRequestDetails;
        public PreClearanceRequest TradingRequestDetails
        {
            set
            {
                _tradingrequestdetails = value;
            }
            get
            {
                return _tradingrequestdetails;
            }
        }
        public List<PreClearanceRequest> TradingRequestDetailsList
        {
            set
            {
                lstTradingRequestDetails = value;
            }
            get
            {
                return lstTradingRequestDetails;
            }
        }
        public void AddObject(PreClearanceRequest o)
        {
            if (lstTradingRequestDetails == null)
            {
                lstTradingRequestDetails = new List<PreClearanceRequest>();
            }
            lstTradingRequestDetails.Add(o);
        }
    }
}