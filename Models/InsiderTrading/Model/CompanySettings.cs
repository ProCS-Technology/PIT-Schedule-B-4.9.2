using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class CompanySettings : BaseEntity
    {
        public string companyName { get; set; }
        public string companyISIN { get; set; }
        public string totalPhysicalShares { get; set; }
        public Int32 companyId { get; set; }
    }
}