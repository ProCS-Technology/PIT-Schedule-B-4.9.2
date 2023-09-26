using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UserGroup : BaseEntity
    {

        public Int32 GrpId { set; get; }
        public string GrpName { set; get; }
        public string GroupMembers { set; get; }
        public int CompanyId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }

        public string GrpEmailSubject { set; get; }
        public string GrpEmailBody { set; get; }
        public string EmailGrpId { get; set; }

        public string EmailId { get; set; }
        public int LogId { get; set; }
    }
}