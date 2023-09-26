using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Department : BaseEntity
    {
        public Int32 DEPARTMENT_ID { get; set; }
        public String DEPARTMENT_NM { get; set; }
        public String CREATE_BY { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String CREATED_DATE { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}