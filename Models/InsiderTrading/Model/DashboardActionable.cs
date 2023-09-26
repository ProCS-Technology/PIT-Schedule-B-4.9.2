using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class DashboardActionable
    {
        public Int32 TaskId { set; get; }
        public string UserLogin { set; get; }
        public string UserNm { set; get; }
        public string TaskType { set; get; }
        public string TaskCreatedOn { set; get; }
        public string TaskStatus { set; get; }
        public string TaskStDt { set; get; }
        public string TaskEnDt { set; get; }
        public string TaskCompletedOn { set; get; }
        public string DataElementId { set; get; }
        public string Remarks { set; get; }
    }
}