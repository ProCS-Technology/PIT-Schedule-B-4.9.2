using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BenposHeader : BaseEntity
    {
        public Int32 id { get; set; }
        public Int32 esopHdrId { get; set; }
        public String Corporate_Action { get; set; }
        public String asOfDate { get; set; }
        public String fromDate { get; set; }
        public String toDate { get; set; }
        public String type { get; set; }
        public Int32 companyId { get; set; }
        public decimal vwap { get; set; }
        public RestrictedCompanies restrictedCompany { get; set; }
        public String fileName { get; set; }
        public String fileNameESOP { get; set; }
        public String createdBy { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
        public String FolioNo { get; set; }
        public String PanNo { get; set; }
        public String Qty { get; set; }
        public String Rate { get; set; }
        public String Holding { get; set; }
        public String ESOPAmount { get; set; }

        
    }
}