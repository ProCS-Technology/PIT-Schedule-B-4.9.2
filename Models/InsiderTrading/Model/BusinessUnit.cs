using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BusinessUnit : BaseEntity
    {
        public Int32 businessUnitId { get; set; }
        public string businessUnitName { get; set; }
        public Int32 companyId { get; set; }
        public Int32 parentBusinessUnitId { get; set; }
        public string parentBusinessUnitName { get; set; }
    }
}