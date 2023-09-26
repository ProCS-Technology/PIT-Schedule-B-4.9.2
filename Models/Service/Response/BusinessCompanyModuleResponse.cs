using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class BusinessCompanyModuleResponse : BaseResponse
    {
        private BusinessCompanyModule _businessCompanyModule;
        private List<BusinessCompanyModule> lstbusinessCompanyModule;
        public BusinessCompanyModule BusinessCompanyModule
        {
            set
            {
                _businessCompanyModule = value;
            }
            get
            {
                return _businessCompanyModule;
            }
        }
        public List<BusinessCompanyModule> BusinessCompanyModuleList
        {
            set
            {
                lstbusinessCompanyModule = value;
            }
            get
            {
                return lstbusinessCompanyModule;
            }
        }
        public void AddObject(BusinessCompanyModule o)
        {
            if (lstbusinessCompanyModule == null)
            {
                lstbusinessCompanyModule = new List<BusinessCompanyModule>();
            }
            lstbusinessCompanyModule.Add(o);
        }
    }
}