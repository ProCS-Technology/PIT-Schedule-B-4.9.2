using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class PreClearanceRequest : BaseEntity
    {
        public Int32 Id { get; set; }
        public string encryptTaskId { get; set; }
        public Int32 CompanyId { get; set; }
        public Int32 businessUnitId { get; set; }
        public String DematType { get; set; }
        public String LoginId { get; set; }
        public Int32 TradeCompany { get; set; }
        public String TradeDate { get; set; }
        public Int32 PreClearanceRequestedFor { get; set; }
        public Int32 SecurityType { get; set; }
        public Int32 TransactionType { get; set; }
        public String TradeQuantity { get; set; }
        public Int32 TradeExchange { get; set; }
        public String DematAccount { get; set; }
        public String Status { get; set; }
        public Int32 PreClearanceRequestId { get; set; }
        public String PreClearanceRequestedForName { get; set; }
        public String TradeExchangeName { get; set; }
        public String TransactionTypeName { get; set; }
        public String TradeCompanyName { get; set; }
        public String SecurityTypeName { get; set; }
        public String CreatedOn { get; set; }
        public String ActualTransactionDate { get; set; }
        public string ActualTradeQuantity { get; set; }
        public String ValuePerShare { get; set; }
        public String TotalAmount { get; set; }
        public String BrokerNote { get; set; }
        public Int32 brokerNoteId { get; set; }
        public Int32 relativeId { get; set; }
        public string remarks { get; set; }
        public string nonCompliantTaskText { get; set; }
        public string isBrokerNoteUploaded { get; set; }
        public string reviewedOn { get; set; }
        public string reviewedBy { get; set; }
        public string reviewerRemarks { get; set; }
        public string shareCurrentMarketPrice { get; set; }
        public string proposedTransactionThrough { get; set; }
        public string exchangeTradedOn { get; set; }
        public string layoutTemplate { get; set; }
        public string relationName { get; set; }
        public string completedOnOrBefore { get; set; }
        public string formType { get; set; }
        public String formUrl { get; set; }
        public List<String> lstFormUrl { get; set; }
        public string emailBody { get; set; }
        public bool isNUllTrade { get; set; }
        public String nullTradeRemarks { get; set; }
        public String userRole { get; set; }
        public string preClearanceFileName { get; set; }
        public string undertakingFileName { get; set; }
        public string preClearanceOrderFileName { get; set; }
        public List<BrokerNoteModal> lstBrokerNoteUploaded { get; set; }
        public string underTakingText { get; set; }
        public string PeriodQty { set; get; }
        public string PeriodVal { set; get; }
        public Int32 TransactionId { set; get; }
        public Int32 NonComplianceId { set; get; }
        public string NonComplianceRemarks { set; get; }
        public string tradingFrom { get; set; }
        public string tradingTo { get; set; }
        public Int32 userId { get; set; }

        public List<BrokerNoteModal> lstMultiTrade { get; set; }

        public int RemainingTradeQuantity { get; set; }
        public String TradeExecutedStatus { get; set; }

        public string ExceededTradeLimit { get; set; }
        public String BrokerDetails { get; set; }
    }
}