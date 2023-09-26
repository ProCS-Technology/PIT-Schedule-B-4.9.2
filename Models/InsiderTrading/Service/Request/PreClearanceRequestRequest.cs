using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Collections.Generic;
using System.Data;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class PreClearanceRequestRequest
    {
        private PreClearanceRequest _pre_Clearance_Request;
        public PreClearanceRequestRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public PreClearanceRequestRequest(PreClearanceRequest pre_Clearance_Request)
        {
            _pre_Clearance_Request = new PreClearanceRequest();
            _pre_Clearance_Request = pre_Clearance_Request;

        }
        public PreClearanceRequestResponse GetTypeOfSecurity()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTypeOfSecurity(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetTypeOfRestrictedCompanies()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTypeOfRestrictedCompanies(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetTypeOfTransaction()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTypeOfTransaction(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetTradeExchange()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTradeExchange(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetDematAccount()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetDematAccount(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetRelativeDetail()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetRelativeDetail(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetRelativeDetailBN()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetRelativeDetailBN(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SavePreClearanceRequest()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            if (_pre_Clearance_Request.PreClearanceRequestId == 0)
            {
                return oRepository.AddPreClearanceRequest(_pre_Clearance_Request);
            }
            else
            {
                return oRepository.UpdatePreClearanceRequest(_pre_Clearance_Request);
            }
        }
        public PreClearanceRequestResponse GetUndertakingTemplate()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetUndertakingTemplate(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetPreClearanceRequest()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetPreClearanceRequest(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetPreClearanceRequestFilterByStatus()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetPreClearanceRequestFilterByStatus(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetFormsCDJ()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetFormsCDJ(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetAllPendingRequest()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetAllPendingRequest(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetTemplateForApproverForAction()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTemplateForApproverForAction(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse UpdateTaskUsers()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.UpdateTaskUsers(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetPreClearanceRequest_for_other()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetPreClearanceRequest_for_other(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SavePreClearanceRequest_for_other()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            if (_pre_Clearance_Request.PreClearanceRequestId == 0)
            {
                return oRepository.AddPreClearanceRequest_for_other(_pre_Clearance_Request);
            }
            else
            {
                return oRepository.UpdatePreClearanceRequest_for_other(_pre_Clearance_Request);
            }
        }
        public PreClearanceRequestResponse AddUpdateBrokerNote()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.AddUpdateBrokerNote(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SaveBenposTradeTransaction()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.SaveBenposTradeTransaction(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse AddBrokerNoteWithNoPC()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.AddBrokerNoteWithNoPC(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetTransactionalForms()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTransactionalForms(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SaveAndGetTransactionalForms()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.SaveAndGetTransactionalForms(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SubmitCustomForm()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.SubmitCustomForm(_pre_Clearance_Request);
        }
        public DataSet GetForms()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetForms(_pre_Clearance_Request);
        }
        public List<FileModel> GetAllTradingFilesOfCurrentRequest()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetAllTradingFilesOfCurrentRequest(_pre_Clearance_Request);
        }
        public bool ValidateTradeDateLiesInTradingWindowClosure()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.ValidateTradeDateLiesInTradingWindowClosure(_pre_Clearance_Request.TradeDate, _pre_Clearance_Request.CompanyId, _pre_Clearance_Request.MODULE_DATABASE, _pre_Clearance_Request.TransactionType);
        }
        public bool ValidateTradeDateLiesInTradingWindowClosureBrokerNote()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.ValidateTradeDateLiesInTradingWindowClosure(_pre_Clearance_Request.ActualTransactionDate, _pre_Clearance_Request.CompanyId, _pre_Clearance_Request.MODULE_DATABASE, _pre_Clearance_Request.TransactionType);
        }
        public bool ValidateTradeDateFallsInHolidayList()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.ValidateTradeDateFallsInHolidayList(_pre_Clearance_Request.TradeDate, _pre_Clearance_Request.CompanyId, _pre_Clearance_Request.MODULE_DATABASE, _pre_Clearance_Request.TransactionType);
        }
        public bool ValidateTradeDateFallsInHolidayListBrokerNote()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.ValidateTradeDateFallsInHolidayList(_pre_Clearance_Request.ActualTransactionDate, _pre_Clearance_Request.CompanyId, _pre_Clearance_Request.MODULE_DATABASE, _pre_Clearance_Request.SecurityType);
        }
        //public bool ValidateaAnotherRequest()
        //{
        //    PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
        //    return oRepository.ValidateaAnotherRequest(_pre_Clearance_Request);
        //}
        public PreClearanceRequestResponse GetFormsInfo()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetFormsInfo(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse AddNonComplianceBrokerNote()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.AddNonComplianceBrokerNote(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse UpdateNonComplianceTask()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.UpdateNonComplianceTask(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SubmitEsopFormC()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.SubmitEsopFormC(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetTransactionalEsopForms()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTransactionalEsopForms(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse SaveAndGetTransactionalEsopForms()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.SaveAndGetTransactionalEsopForms(_pre_Clearance_Request);
        }
        //public PreClearanceRequestResponse SavePreClearanceRequestOtherCompany()
        //{
        //    PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
        //    return oRepository.AddPreClearanceRequestOtherCompany(_pre_Clearance_Request);
        //}
        public PreClearanceRequestResponse GetPreClearanceRequestOtherCompanyList()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetPreClearanceRequestOtherCompanyList(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse GetPreClearanceRequestOtherCompanyReport()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetPreClearanceRequestOtherCompanyReport(_pre_Clearance_Request);
        }
        public TradeTransactionResponse GetTradeTransactions()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.GetTradeTransactions(_pre_Clearance_Request);
        }
        public PreClearanceRequestResponse UpdateTransactionHistory()
        {
            PreClearanceRequestRepository oRepository = new PreClearanceRequestRepository();
            return oRepository.UpdateTransactionHistory(_pre_Clearance_Request);
        }
    }
}