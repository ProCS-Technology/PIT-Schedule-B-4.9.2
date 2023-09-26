using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Depository : BaseEntity
    {
        public Int32 depositoryId { get; set; }
        public Int32 companyId { get; set; }
        public List<string> depository { get; set; }
        public string depositoryName { get; set; }
        public Int32 sharesCount { get; set; }
        public Int32 thresholdLimit { get; set; }
        public string byTime { get; set; }
        public string limitType { get; set; }
    }
}