using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class TransactionSubTypeMasterResponse : BaseResponse
    {
        private TransactionSubTypeMaster _transactionSubTypeMaster;
        private List<TransactionSubTypeMaster> lstTransactionSubTypeMaster;
        public TransactionSubTypeMaster TransactionSubTypeMaster
        {
            set
            {
                _transactionSubTypeMaster = value;
            }
            get
            {
                return _transactionSubTypeMaster;
            }
        }

        public List<TransactionSubTypeMaster> TransactionSubTypeMasterList
        {
            set
            {
                lstTransactionSubTypeMaster = value;
            }
            get
            {
                return lstTransactionSubTypeMaster;
            }
        }

        public void AddObject(TransactionSubTypeMaster o)
        {
            if (lstTransactionSubTypeMaster == null)
            {
                lstTransactionSubTypeMaster = new List<TransactionSubTypeMaster>();
            }
            lstTransactionSubTypeMaster.Add(o);
        }
    }
}