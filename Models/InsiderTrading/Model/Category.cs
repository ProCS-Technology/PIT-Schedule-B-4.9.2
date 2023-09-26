using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Category : BaseEntity
    {
        public Int32 ID { get; set; }
        public String categoryName { get; set; }
        public Int32 companyId { get; set; }
        public string createdOn { get; set; }
        public String createdBy { get; set; }
        public string modifiedOn { get; set; }
        public String modifiedBy { get; set; }
        public SubCategory subCategory { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}