using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Holiday : BaseEntity
    {
        public Int32 ID { get; set; }
        public String HOLIDAY_DATE { get; set; }
        public String HOLIDAY_DESCRIPTION { get; set; }
        public String MODULE_DATABASE { get; set; }



    }
}