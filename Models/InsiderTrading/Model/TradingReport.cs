using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class TradingReport : BaseEntity
    {
        public Int32 companyId { get; set; }
        public string tradingFrom { get; set; }
        public string tradingTo { get; set; }
        public Int32 userId { get; set; }
        public string name { get; set; }
        public string relativeName { get; set; }
        public string relation { get; set; }
        public string pan { get; set; }
        public string folioNumber { get; set; }

        public string holdingAsOnDate { get; set; }
        public string fileUploadedDate { get; set; }
        public string totaltradeQuantity { get; set; }
        public string totaltradeValue { get; set; }
        public string transactionSubType { get; set; }
        public string tradeType { get; set; }
        public string tradeDate { get; set; }
        public string UserType { get; set; }

        #region "Equity Calculation"
        public string equityQuantity { get; set; }
        public string equityValue { get; set; }
        public string equityTradeDate { get; set; }
        #endregion

        #region "ESOP Calculation"
        public string esopQuantity { get; set; }
        public string esopValue { get; set; }
        public string esopTradeDate { get; set; }
        #endregion

        #region "Phsical Share Calculation"
        public string physicalShareQuantity { get; set; }
        public string physicalShareValue { get; set; }
        public string physicalShareTradeDate { get; set; }
        #endregion

        public string method { get; set; }
        public string isNonCompliant { get; set; }
        public string nonCompliantType { get; set; }
        public string nonCompliantReason { get; set; }
        public string cORemarks { get; set; }
        public string nonComplianceTaskStatus { get; set; }
        public Int32 nonComplianceTaskId { get; set; }
        public string PreclearanceId { get; set; }
        public string PreclearanceDate { get; set; }

        public String TradeQuantity { get; set; }
        public String ReqTradeQuantity { get; set; }
        public String EsopFilePath { get; set; }
        public String IsEsopFormSubmitted { get; set; }
        public string UserLogin { set; get; }
    }
}