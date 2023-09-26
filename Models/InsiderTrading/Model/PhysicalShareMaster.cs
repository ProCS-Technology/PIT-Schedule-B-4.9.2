using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class PhysicalShareMaster : BaseEntity
    {
        public int physical_share_id { get; set; }
        public string name { get; set; }
        public string issue_date { get; set; }
        public int qty { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}