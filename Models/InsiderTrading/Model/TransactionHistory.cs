using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TransactionHistory : BaseEntity
    {
        public Int32 transactionId { get; set; }
        public string userName { get; set; }
        public string panNumber { get; set; }
        public string folioNumber { get; set; }
        public string transactionBy { get; set; }
        public string transactionDate { get; set; }
        public Int32 tradeQuantity { get; set; }
        public string transactionSubType { get; set; }
        public Int32 companyId { get; set; }
        public string userLogin { get; set; }
        public List<TransactionSubTypeMaster> lstTransactionBifurcation { get; set; }
        public string Relation { get; set; }
        public string TradeValue { get; set; }
        public string Quantity { get; set; }
    }
}