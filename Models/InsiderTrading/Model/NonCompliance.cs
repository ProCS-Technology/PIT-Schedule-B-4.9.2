using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class NonCompliance : BaseEntity
    {
        public Int32 nonComplianceId { get; set; }
        public string panNumber { get; set; }
        public string nonComplianceType { get; set; }
        public string nonComplianceTradeQuantityOrValue { get; set; }
        public string subType { get; set; }
        public bool status { get; set; }
        public string transactionDate { get; set; }
        public Int32 transactionId { get; set; }
        public string ncAmount { get; set; }
        public string sName { set; get; }
        public string reportedOn { get; set; }
        public string ncQuantity { get; set; }
        public string transactionStartDate { get; set; }
        public string transactionEndDate { get; set; }
        public string Relation { get; set; }
        public string Folio { get; set; }
        public int RelativeId { get; set; }
        public string RelativeName { get; set; }
        public Int32 TransactionId { get; set; }
    }
}