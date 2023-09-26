using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class TransactionHistoryResponse : BaseResponse
    {
        private TransactionHistory _transactionHistory;
        private List<TransactionHistory> lstTransactionHistory;
        public TransactionHistory TransactionHistory
        {
            set
            {
                _transactionHistory = value;
            }
            get
            {
                return _transactionHistory;
            }
        }

        public List<TransactionHistory> TransactionHistoryList
        {
            set
            {
                lstTransactionHistory = value;
            }
            get
            {
                return lstTransactionHistory;
            }
        }

        public void AddObject(TransactionHistory o)
        {
            if (lstTransactionHistory == null)
            {
                lstTransactionHistory = new List<TransactionHistory>();
            }
            lstTransactionHistory.Add(o);
        }
    }
}