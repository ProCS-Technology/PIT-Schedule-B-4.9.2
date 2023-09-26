using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class SubCategory : BaseEntity
    {
        public Int32 ID { get; set; }
        public String subCategoryName { get; set; }
        public Category category { get; set; }
        public String createdBy { get; set; }

        public Int32 companyId { get; set; }
        public string created_on { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}