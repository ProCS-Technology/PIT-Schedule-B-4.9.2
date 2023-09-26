
using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class BusinessCompanyModuleRequest
    {
        private BusinessCompanyModule _businessCompanyModule;

        public BusinessCompanyModuleRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public BusinessCompanyModuleRequest(BusinessCompanyModule bcm)
        {
            _businessCompanyModule = new BusinessCompanyModule();
            _businessCompanyModule = bcm;
        }

        public BusinessCompanyModuleResponse GetBusinessCompanyModuleList()
        {
            BusinessCompanyModuleRepository oRepository = new BusinessCompanyModuleRepository();
            return oRepository.GetBusinessCompanyModuleList();
        }

        public BusinessCompanyModuleResponse SaveBusinessCompanyModule()
        {
            //_group.Validate();
            //if (_group.GetRules().Count == 0)
            //{
            BusinessCompanyModuleRepository oRepository = new BusinessCompanyModuleRepository();
            if (_businessCompanyModule.Operation_Type == "ADD")
            {
                return oRepository.AddBusinessCompanyModule(_businessCompanyModule);
            }
            else
            {
                return oRepository.UpdateBusinessCompanyModule(_businessCompanyModule);
            }
            //}
            // return null;
        }

        public BusinessCompanyModuleResponse DeleteBusinessCompanyModule()
        {

            BusinessCompanyModuleRepository oRepository = new BusinessCompanyModuleRepository();
            return oRepository.DeleteBusinessCompanyModule(_businessCompanyModule);
        }
    }
}