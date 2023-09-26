 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class DashboardUpsi : BaseEntity
    {
        public string UpsiFrom { get; set; }
        public string UpsiTo { get; set; }
        public Int32 TotalActiveUPSIEvent { get; set; }
        public Int32 TotalInactiveUPSIEvent { get; set; }
        public Int32 TotalAbandonedUPSIEvent { get; set; }
        public Int32 TotalPublishedUPSIEvent { get; set; }
        public Int32 TotalUPSIEvent { get; set; }
        public Int32 companyId { get; set; }
        public String loginId { get; set; }
    }
}