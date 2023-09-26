using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class ApproverSetUp : BaseEntity
    {
        public Int32 WF_ID { get; set; }
        public string USER_LOGIN { get; set; }
        public Int32 MIN_LIMIT { get; set; }
        public Int32 MAX_LIMIT { get; set; }
        public string CREATED_BY { get; set; }
        public Int32 COMPANY_ID { get; set; }
    }
}