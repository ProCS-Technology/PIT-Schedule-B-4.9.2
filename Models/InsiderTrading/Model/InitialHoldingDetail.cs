using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class InitialHoldingDetail : BaseEntity
    {
        public Int32 ID { get; set; }
        public String lastModifiedOn { get; set; }
        public Int32 version { get; set; }
        public Int32 companyId { get; set; }
        public RestrictedCompanies restrictedCompany { get; set; }
        public String securityType { get; set; }
        public String securityTypeName { get; set; }
        public Relative relative { get; set; }
        public DematAccount dematAccount { get; set; }
        public Int32 noOfSecurities { get; set; }
        public bool isDeleteInitialHolding { get; set; }
        public Int32 FY_INITIAL { get; set; }
        public Int32 FY_LAST { get; set; }
        public Int32 TOTAL_BUY { get; set; }
        public Int32 TOTAL_SELL { get; set; }
        public Double TOTAL_BUY_VALUE { get; set; }
        public Double TOTAL_SELL_VALUE { get; set; }
        public string FINANCIAL_YEAR { get; set; }


    }
}