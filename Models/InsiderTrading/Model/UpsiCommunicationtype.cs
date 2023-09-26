using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UpsiCommunicationtype : BaseEntity
    {

        public Int32 COMMTYPE_ID { get; set; }

        public String COMMTYPE_NAME { get; set; }

        public Int32 COMPANY_ID { get; set; }

        public String CREATE_BY { get; set; }

        public String CREATED_ON { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}