using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class DashboardUPSITask : BaseEntity
    {
        public string TaskId { get; set; }
        public string Group_id { get; set; }
        public string TaskFor { get; set; }
        public string TaskMG { get; set; }
        public string TaskMailBody { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailDate { get; set; }
        public string EmailCC { get; set; }
        public string Status { get; set; }
        public List<UPSIRemarksAttachments> listAttachment { get; set; }
        public string EmailSubject { set; get; }
    }
}