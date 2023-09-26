using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class TradeTransactionResponse : BaseResponse
    {
        private TradeTransaction _tradeTransaction;
        private List<TradeTransaction> lstTradeTransaction;
        public TradeTransaction tradeTransaction
        {
            set
            {
                _tradeTransaction = value;
            }
            get
            {
                return _tradeTransaction;
            }
        }
        public List<TradeTransaction> TradeTransactionList
        {
            set
            {
                lstTradeTransaction = value;
            }
            get
            {
                return lstTradeTransaction;
            }
        }
        public void AddObject(TradeTransaction o)
        {
            if (lstTradeTransaction == null)
            {
                lstTradeTransaction = new List<TradeTransaction>();
            }
            lstTradeTransaction.Add(o);
        }
    }
}