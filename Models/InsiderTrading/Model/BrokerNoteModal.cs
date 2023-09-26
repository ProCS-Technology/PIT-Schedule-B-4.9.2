using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BrokerNoteModal : BaseEntity
    {
        public Int32 brokerNoteId { get; set; }
        public String ActualTransactionDate { get; set; }
        public string ActualTradeQuantity { get; set; }
        public String ValuePerShare { get; set; }
        public String TotalAmount { get; set; }
        public string remarks { get; set; }
        public String BrokerNote { get; set; }
        public bool isFormCDJCreated { get; set; }
        public bool isNullTrade { set; get; }
        
        //Multi Trade Broker Note
        public String PartialTradeDate { get; set; }
        public String PartialValuePerShare { get; set; }
        public String PartialTradeQuantity { get; set; }
        public String PartialTotalAmount { get; set; }
        public String PartialExchangeTradedOn { get; set; }
        
    }
}