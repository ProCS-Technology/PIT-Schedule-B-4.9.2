using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class InsiderTradingWindow : BaseEntity
    {
        public Int32 id { get; set; }
        public Int32 companyId { get; set; }
        public Int32 windowClosureTypeId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string boardMeetingScheduledOn { get; set; }
        public string quarterEndedOn { get; set; }
        public string remarks { get; set; }
        public string createdBy { get; set; }
        public string EmailTemplate { set; get; }
        public string EmailSubject { set; get; }
        public List<string> lstUser { get; set; }
        public List<string> lstmailTo { get; set; }

        public string mailTo { get; set; }
        public string relatives { set; get; }
        public string CPs { set; get; }
        public string TradingWindowDocument { set; get; }
        public string TradingWindowDocumentPath { set; get; }
    }
}