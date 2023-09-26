using System;

namespace ProcsDLL.Models.Model
{
    public class BusinessCompanyModule : BaseEntity
    {
        public Int32 GROUP_ID { get; set; }
        public String GROUP_NAME { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String COMPANY_NM { get; set; }
        public Int32 _MODULE_ID { get; set; }
        public String _MODULE_NM { get; set; }
        public String MODULE_STATUS { get; set; }
        public string MODULE_st_dt { get; set; }
        public string MODULE_en_dt { get; set; }
        public String createdBy { get; set; }
        public string createOn { get; set; }
        public string Operation_Type { get; set; }
        public string ADMIN_NAME { get; set; }
        public string ADMIN_EMAIL { get; set; }
        public string ADMIN_PHONE { get; set; }
    }
}