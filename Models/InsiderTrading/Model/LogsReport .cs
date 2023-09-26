using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class LogsReport : BaseEntity
    {
        public Int32 CompanyId { get; set; }
        public Int32 Id { get; set; }
        public Int32 FormId { get; set; }
        public Int32 UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FormName { get; set; }
        public string UserLogin { get; set; }
        public string CreatedOn { get; set; }
    }
}