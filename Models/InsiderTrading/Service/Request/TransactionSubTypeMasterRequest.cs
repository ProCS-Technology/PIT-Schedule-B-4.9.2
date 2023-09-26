using ProcsDLL.Models.InsiderTrading.Model;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class TransactionSubTypeMasterRequest
    {
        private TransactionSubTypeMaster _transactionSubTypeMaster;
        public TransactionSubTypeMasterRequest()
        {
            _transactionSubTypeMaster = new TransactionSubTypeMaster();
        }

        public TransactionSubTypeMasterRequest(TransactionSubTypeMaster transactionSubTypeMaster)
        {
            _transactionSubTypeMaster = new TransactionSubTypeMaster();
            _transactionSubTypeMaster = transactionSubTypeMaster;
        }
    }
}