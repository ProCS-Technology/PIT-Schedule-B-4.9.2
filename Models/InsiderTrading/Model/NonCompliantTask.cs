using System;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class NonCompliantTask : BaseEntity
    {
        public Int32 id { get; set; }
        public Int32 companyId { get; set; }
        public Relative relative { get; set; }
        public User user { get; set; }
        public string actualTransactionDate { get; set; }
        public Int32 tradeQuantity { get; set; }
        public Int32 valuePerShare { get; set; }
        public Int32 totalShareValue { get; set; }
        public string brokerNote { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public string value { get; set; }
        public string isNonCompliant { get; set; }
        public Int32 numberOfShareAsPerBenpos { get; set; }
        public Int32 numberOfShareAsPerInitialHolding { get; set; }
        public string folioNumber { get; set; }
        public string asOfDate { get; set; }
        public string differenceInShare { get; set; }
        public string isFolioNoDeclared { get; set; }
        public string nonCompliantType { get; set; }
    }
}