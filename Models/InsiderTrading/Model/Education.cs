using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Education : BaseEntity
    {
        public Int32 ID { get; set; }
        public String courseName { get; set; }
        public String instituteName { get; set; }
        public String passingMonth { get; set; }
        public Int32 passingYear { get; set; }
        public Int32 companyId { get; set; }
        public String createdBy { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}