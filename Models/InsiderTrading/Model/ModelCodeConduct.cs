using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class ModelCodeConduct : BaseEntity
    {
        public Int32 MODEL_ID { get; set; }
        public String FREQUENCY_OF_PERIOD { get; set; }
        public String CUT_OFF_DATES_FOR_PERIOD { get; set; }
        public Int32 RESTRICTED_MONTHS_FOR_CONTRATRADE { get; set; }
        public Int32 AMOUNTLIMIT_FOR_CONTINUAL_DISCLOSURE { get; set; }
        public Int32 AMOUNTLIMIT_FOR_PRE_CLEARANCE { get; set; }
        public Int32 SHARELIMIT_FOR_PRE_CLEARANCE { get; set; }
        public Int32 VALIDITY_OF_PRE_CLEARANCE_APPROVAL { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public Int32 CREATE_BY { get; set; }
        public String CREATED_ON { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}