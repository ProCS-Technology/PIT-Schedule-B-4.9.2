using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Relation : BaseEntity
    {
        public Int32 RELATION_ID { get; set; }
        public String RELATION_NM { get; set; }
        public String CREATED_BY { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String CREATED_DATE { get; set; }
        public String IS_MANDATORY { get; set; }
        public Int32 ORDER_SEQUENCE { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}