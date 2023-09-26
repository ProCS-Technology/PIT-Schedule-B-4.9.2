using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class PreClearanceRequestResponse : BaseResponse
    {
        private PreClearanceRequest _preClearanceRequest;
        private List<SecurityType> lstSecurityType;
        private List<RestrictedCompanies> lstRestrictedCompanies;
        private List<TransactionType> lstTransactionType;
        private List<TradeExchange> lstTradeExchange;
        private List<DematAccount> lstDematDetail;
        private List<Relative> lstRelativeDetail;
        private List<PreClearanceRequest> lstPreClearanceRequest;

        public List<PreClearanceRequest> PreClearanceRequestList
        {
            set
            {
                lstPreClearanceRequest = value;
            }
            get
            {
                return lstPreClearanceRequest;
            }
        }
        public List<SecurityType> SecurityTypeList
        {
            set
            {
                lstSecurityType = value;
            }
            get
            {
                return lstSecurityType;
            }
        }
        public List<RestrictedCompanies> RestrictedCompaniesList
        {
            set
            {
                lstRestrictedCompanies = value;
            }
            get
            {
                return lstRestrictedCompanies;
            }
        }
        public List<TransactionType> TransactionTypeList
        {
            set
            {
                lstTransactionType = value;
            }
            get
            {
                return lstTransactionType;
            }
        }
        public List<TradeExchange> TradeExchangeList
        {
            set
            {
                lstTradeExchange = value;
            }
            get
            {
                return lstTradeExchange;
            }
        }
        public List<DematAccount> DematDetailList
        {
            set
            {
                lstDematDetail = value;
            }
            get
            {
                return lstDematDetail;
            }
        }
        public List<Relative> RelativeDetailList
        {
            set
            {
                lstRelativeDetail = value;
            }
            get
            {
                return lstRelativeDetail;
            }
        }
        public void AddObject(SecurityType o)
        {
            if (lstSecurityType == null)
            {
                lstSecurityType = new List<SecurityType>();
            }
            lstSecurityType.Add(o);
        }
        public void AddObject(RestrictedCompanies o)
        {
            if (lstRestrictedCompanies == null)
            {
                lstRestrictedCompanies = new List<RestrictedCompanies>();
            }
            lstRestrictedCompanies.Add(o);
        }
        public void AddObject(TransactionType o)
        {
            if (lstTransactionType == null)
            {
                lstTransactionType = new List<TransactionType>();
            }
            lstTransactionType.Add(o);
        }
        public void AddObject(TradeExchange o)
        {
            if (lstTradeExchange == null)
            {
                lstTradeExchange = new List<TradeExchange>();
            }
            lstTradeExchange.Add(o);
        }
        public void AddObject(DematAccount o)
        {
            if (lstDematDetail == null)
            {
                lstDematDetail = new List<DematAccount>();
            }
            lstDematDetail.Add(o);
        }
        public void AddObject(Relative o)
        {
            if (lstRelativeDetail == null)
            {
                lstRelativeDetail = new List<Relative>();
            }
            lstRelativeDetail.Add(o);
        }
        public PreClearanceRequest PreClearanceRequest
        {
            set
            {
                _preClearanceRequest = value;
            }
            get
            {
                return _preClearanceRequest;
            }
        }
        public void AddObject(PreClearanceRequest o)
        {
            if (lstPreClearanceRequest == null)
            {
                lstPreClearanceRequest = new List<PreClearanceRequest>();
            }
            lstPreClearanceRequest.Add(o);
        }
    }
}