using System;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BenposNonCompliant
    {
        public Int32 ID { set; get; }
        public string UserNm { set; get; }
        public string RelativeNm { set; get; }
        public string RelationNm { set; get; }
        public string PAN { set; get; }
        public string Folio { set; get; }
        public Int64 Qty { set; get; }
        public Decimal TradeVal { set; get; }
        public string NonComplianceType { set; get; }
    }
}