using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Experience : BaseEntity
    {
        public Int32 ID { get; set; }
        public String employer { get; set; }
        public String userRole { get; set; }
        public String dateFrom { get; set; }
        public String dateTo { get; set; }
        public Int32 companyId { get; set; }
        public String createdBy { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}