using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class PeriodicDeclaration : BaseEntity
    {
        public Int32 id { get; set; }
        public Int32 companyId { get; set; }
        public Int32 frequencyInMonths { get; set; }
        public Int32 validTillInDays { get; set; }
        public string declarationDate { get; set; }
        public string createdBy { get; set; }
    }
}