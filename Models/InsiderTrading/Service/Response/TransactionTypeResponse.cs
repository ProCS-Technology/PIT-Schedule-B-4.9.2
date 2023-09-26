using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class TransactionTypeResponse : BaseResponse
    {
        private TransactionType _transType;
        private List<TransactionType> lstTransactionType;
        public TransactionType transactionType
        {
            set
            {
                _transType = value;
            }
            get
            {
                return _transType;
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
        public void AddObject(TransactionType o)
        {
            if (lstTransactionType == null)
            {
                lstTransactionType = new List<TransactionType>();
            }
            lstTransactionType.Add(o);
        }
    }
}