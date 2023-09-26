using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class InitialHolding : BaseEntity
    {
        public Int32 ID { get; set; }
        public Int32 companyId { get; set; }
        public String isPolicyAccept { get; set; }
        public String policyAcceptDate { get; set; }
        public String isHoldingExists { get; set; }
        public String isSelfDeclared { get; set; }
        public String isRelativeDeclared { get; set; }
        public String isBothDeclared { get; set; }
        public String signedCopy { get; set; }
        public List<InitialHoldingDetail> holdingDetails { get; set; }
    }
}