using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class CompanyRequest
    {
        private CompanySettings _companySettings;

        public CompanyRequest()
        {
            _companySettings = new CompanySettings();
        }

        public CompanyRequest(CompanySettings companySettings)
        {
            _companySettings = new CompanySettings();
            _companySettings = companySettings;
        }

        public CompanyResponse AddCompanyNameAndISIN()
        {
            UserRepository companySettingsRepos = new UserRepository();
            return companySettingsRepos.AddCompanyNameAndISIN(_companySettings);
        }

        public CompanyResponse GetCompanyNameAndISIN()
        {
            UserRepository companySettingsRepos = new UserRepository();
            return companySettingsRepos.GetCompanyNameAndISIN(_companySettings);
        }
    }
}