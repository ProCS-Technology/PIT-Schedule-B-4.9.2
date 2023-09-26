using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class AnnualDisclosureTask : BaseEntity
    {
        public Int32 id { get; set; }
        public string FINANCIALYEARS { get; set; }
        public string Title { get; set; }
        public int companyId { get; set; }
        //public Int32 windowClosureTypeId { get; set; }
        public string STARTDATE { get; set; }
        public int TILLDATE { get; set; }
        public int no_of_days { get; set; }
        //public string boardMeetingScheduledOn { get; set; }
        //public string quarterEndedOn { get; set; }
        // public string remarks { get; set; }
        public string createdBy { get; set; }
        // public string EmailTemplate { set; get; }

        public List<string> lstUser { get; set; }
    }
}