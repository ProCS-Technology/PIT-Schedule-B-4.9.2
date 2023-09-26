using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class CompanyResponse : BaseResponse
    {
        private CompanySettings _companySettings;
        private List<CompanySettings> lstCompanySettings;

        public CompanySettings CompanySettings
        {
            set
            {
                _companySettings = value;
            }
            get
            {
                return _companySettings;
            }
        }

        public List<CompanySettings> CompanySettingsList
        {
            set
            {
                lstCompanySettings = value;
            }
            get
            {
                return lstCompanySettings;
            }
        }

        public void AddObject(CompanySettings o)
        {
            if (lstCompanySettings == null)
            {
                lstCompanySettings = new List<CompanySettings>();
            }
            lstCompanySettings.Add(o);
        }
    }
}