using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Dashboard : BaseEntity
    {
        public Int32 allPreClearanceRequestCount { get; set; }
        public Int32 inApprovalPreClearanceRequestCount { get; set; }
        public Int32 approvedPreClearanceRequestCount { get; set; }
        public Int32 rejectedPreClearanceRequestCount { get; set; }
        public Int32 submittedWithClearanceCount { get; set; }
        public Int32 submittedWithoutClearanceCount { get; set; }
        public String tradingLimitPerQuarterBeforePC { get; set; }
        public string tradingLimitType { set; get; }
        public string lastCurrentHoldingUpdateDate { get; set; }
        public string userCurrentHolding { get; set; }

        public Int32 stocksTradedInPeriodUsingBenpos { get; set; }
        public Int32 stocksTradedInPeriodUsingBrokerNote { get; set; }
        public Int32 notDeclaredCount { get; set; }
        public Int32 companyId { get; set; }
        public String loginId { get; set; }
        public string myDeclarationText { get; set; }
        public List<PreClearanceRequest> lstNonComplianceTask { get; set; }
        public TrainingResponse trainingModule { get; set; }
        public TransactionHistoryResponse transactionHistory { get; set; }
        public TransactionSubTypeMasterResponse transactionSubTypeMaster { get; set; }
        public TransactionHistory transactionHistoryBifurcation { get; set; }
        public List<NonCompliance> lstNonCompliance { get; set; }
        public List<DashboardUPSITask> listUPSITask { get; set; }
        public DashboardUPSITask UPSITask = new DashboardUPSITask();

        public TransactionHistoryResponse CompliantTransactionHistory { get; set; }

    }
}