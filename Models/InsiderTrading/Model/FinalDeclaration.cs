using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class FinalDeclaration : BaseEntity
    {
        public Int32 Id { get; set; }
        public String fileName { get; set; }
        public String fileFormB { get; set; }
        public String fileFormEOrF { get; set; }
        public String fileFormK { get; set; }
        public String createdOn { get; set; }
        public String createdBy { get; set; }
        public Int32 version { get; set; }
        public String loginId { get; set; }

        public String PolicyVersion { get; set; }
    }
}
