using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TransactionSubTypeMaster : BaseEntity
    {
        public Int32 transactionSubTypeMasterId { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public Int32 tradeQuantity { get; set; }
        public string tradeValue { get; set; }
        public string transactionDate { get; set; }
        public string remarks { get; set; }
    }
}