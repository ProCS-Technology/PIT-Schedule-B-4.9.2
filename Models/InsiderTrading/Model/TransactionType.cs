using System;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TransactionType : BaseEntity
    {
        public Int32 Id { set; get; }
        public String Name { set; get; }
        public String Nature { set; get; }
        public string AllowedWC { set; get; }
        public string AllowedUPSI { set; get; }
        public string WCCompliance { set; get; }
        public string UPSICompliance { set; get; }
        public string LimitCompliance { set; get; }
    }
}