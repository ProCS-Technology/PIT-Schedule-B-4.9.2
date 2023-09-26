using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BenposReport : BaseEntity
    {
        public string loginId { get; set; }
        public string name { get; set; }
        public string pan { get; set; }
        public string relative { get; set; }
        public string relation { get; set; }
        public string initialHoldingAfterDeclaration { get; set; }
        public string holdingAsOnDate { get; set; }
        public string tradeDate { get; set; }
        public string isNonCompliant { get; set; }
        public string nonCompliantType { get; set; }
        public List<BenposDetailReport> benposDetailReport { get; set; }
    }
}