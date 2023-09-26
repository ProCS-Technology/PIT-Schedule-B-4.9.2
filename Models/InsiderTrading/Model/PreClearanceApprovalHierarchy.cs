using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class PreClearanceApprovalHierarchy : BaseEntity
    {
        public Int32 ID { get; set; }
        public Int32 companyId { get; set; }
        public string userLogin { get; set; }
        public string officerUserLogin { get; set; }
        public string officerName { get; set; }
        public string officerEmail { get; set; }
        public Int32 orderSequence { get; set; }
    }
}