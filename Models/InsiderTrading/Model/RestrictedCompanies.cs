using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class RestrictedCompanies : BaseEntity
    {
        public Int32 ID { get; set; }
        public String strID { get; set; }
        public Int32 companyID { get; set; }
        public String companyName { get; set; }
        public String companyABRR { get; set; }
        public Int32 isRestricted { get; set; }
        public Int32 forPerpetuity { get; set; }
        public double stockTradeLimit { get; set; }
        public DateTime? periodOfRestrictionFrom { get; set; }
        public DateTime? periodOfRestrictionTo { get; set; }
        public String restrictionFrom { get; set; }
        public String restrictionTo { get; set; }
        public String createdBy { get; set; }
        public Int32 IsHomeCompany { get; set; }
        public List<string> lstStr = new List<string>();
        public String ISIN { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}