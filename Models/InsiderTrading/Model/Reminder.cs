using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Reminder : BaseEntity
    {
        public Int32 companyId { get; set; }
        public string mailType { get; set; }
        public string subject { get; set; }
        public List<string> lstUser { get; set; }
        public string mailBody { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}