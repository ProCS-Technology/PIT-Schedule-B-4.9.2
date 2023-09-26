using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Policy : BaseEntity
    {
        public Int32 POLICY_ID { get; set; }
        public String DOCUMENT { get; set; }
        public String CREATED_BY { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String CREATED_DATE { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}