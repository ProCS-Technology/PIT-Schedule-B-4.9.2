using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Role : BaseEntity
    {
        public Int32 ROLE_ID { get; set; }
        public String ROLE_NM { get; set; }
        public String CREATED_BY { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String CREATED_DATE { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}