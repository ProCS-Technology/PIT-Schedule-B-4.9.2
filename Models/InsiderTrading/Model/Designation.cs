using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Designation : BaseEntity
    {
        public Int32 DESIGNATION_ID { get; set; }
        public String DESIGNATION_NM { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String CREATE_BY { get; set; }
        public String CREATED_ON { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}