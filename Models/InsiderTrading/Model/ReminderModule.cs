using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class ReminderModule : BaseEntity
    {
        public string ACTIVITY_ID { get; set; }
        public string ACTIVITY_NAME { get; set; }
        public string MODULE_NAME { get; set; }
        public string REMINDED_IN { get; set; }
        public string UNIT_OF_MEASURE { get; set; }

        public string MSG_TEMPLATE { get; set; }
        public string COMPANY_ID { get; set; }
        public string CREATED_BY { get; set; }
        public List<ReminderModuleField> listfield { get; set; }







    }
}