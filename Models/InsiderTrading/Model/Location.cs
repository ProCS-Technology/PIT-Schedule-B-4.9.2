using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Location : BaseEntity
    {
        public Int32 locationId { get; set; }
        public String locationName { get; set; }
        public Int32 companyId { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}