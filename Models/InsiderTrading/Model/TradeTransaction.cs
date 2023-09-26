using System;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TradeTransaction
    {
        public Int32 RelativeId { set; get; }
        public string RelativeNm { set; get; }
        public string RelationNm { set; get; }
        public string Pan { set; get; }
        public string Demat { set; get; }
        public string TransDt { set; get; }
        public string TransQty { set; get; }
        public string TransVal { set; get; }
        public string TransType { set; get; }
        public string AsPer { set; get; }
    }
}