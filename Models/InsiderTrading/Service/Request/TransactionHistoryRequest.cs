using ProcsDLL.Models.InsiderTrading.Model;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class TransactionHistoryRequest
    {
        private TransactionHistory _transactionHistory;
        public TransactionHistoryRequest()
        {
            _transactionHistory = new TransactionHistory();
        }

        public TransactionHistoryRequest(TransactionHistory transactionHistory)
        {
            _transactionHistory = new TransactionHistory();
            _transactionHistory = transactionHistory;
        }
    }
}