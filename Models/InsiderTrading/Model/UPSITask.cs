using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSITask : BaseEntity
    {
        public int TaskId { set; get; }
        public int CompanyId { set; get; }
        public string CreatedBy { set; get; }
        List<ConnectedPerson> CP { set; get; }
    }
}