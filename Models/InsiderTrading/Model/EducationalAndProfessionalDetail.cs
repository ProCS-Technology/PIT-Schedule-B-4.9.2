using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class EducationalAndProfessionalDetail : BaseEntity
    {
        public Int32 ID { get; set; }
        public Int32 D_ID { get; set; }
        public Int32 companyId { get; set; }
        public string loginId { get; set; }
        public string institutionName { get; set; }
        public string stream { get; set; }
        public string employerDetails { get; set; }
    }
}