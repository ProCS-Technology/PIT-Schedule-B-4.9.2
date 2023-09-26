using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class InitialHoldingDetailforOther : BaseEntity
    {
        public Int32 ID { get; set; }
        public String lastModifiedOn { get; set; }
        public Int32 version { get; set; }
        public Int32 companyId { get; set; }
        public RestrictedCompanies restrictedCompany { get; set; }
        public DematAccount dematAccount { get; set; }
        public Relative relative { get; set; }
        public Int32 noOfSecurities { get; set; }
        public bool isDeleteHolding { get; set; }
    }
}