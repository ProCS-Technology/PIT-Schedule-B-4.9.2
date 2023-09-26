using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class TransactionTypeRequest
    {
        private TransactionType _transType;
        public TransactionTypeRequest() { }
        public TransactionTypeRequest(TransactionType transType)
        {
            _transType = transType;
        }
        public TransactionTypeResponse GetTransactionType()
        {
            TransactionTypeRepository oRepository = new TransactionTypeRepository();
            return oRepository.GetTransactionType(_transType);
        }
    }
}