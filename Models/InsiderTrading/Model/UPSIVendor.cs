using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIVendor : BaseEntity
    {

        public string VendorId { get; set; }
        public string vendorName { get; set; }

        public string VendorStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string fileName { get; set; }
        public string PanNo { get; set; }
        public string MODULE_DATABASE { get; set; }
        public Int32 companyId { get; set; }

    }
}