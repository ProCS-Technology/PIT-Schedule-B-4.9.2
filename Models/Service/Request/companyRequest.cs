using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class companyRequest
    {
        private Company _company;
        //public GroupRequest(CompanyDTO companyDTO)
        //{
        //    _group = new Group();
        //}

        public companyRequest()
        {

        }

        public companyRequest(Company company)
        {
            _company = new Company();
            _company = company;

        }

        public companyResponse SaveGroup()
        {
            //_group.Validate();
            //if (_group.GetRules().Count == 0)
            //{
            CompanyRepository oRepository = new CompanyRepository();
            if (_company.COMPANY_ID == 0)
            {
                return oRepository.AddCompany(_company);
            }
            else
            {
                return oRepository.UpdateCompany(_company);
            }
            //}
            // return null;
        }

        public companyResponse GetCompanyList()
        {
            CompanyRepository oRepository = new CompanyRepository();
            return oRepository.GetCompanyList();
        }

        public companyResponse DeleteCompany()
        {
            CompanyRepository oRepository = new CompanyRepository();
            return oRepository.DeleteCompany(_company);
        }

        public companyResponse GetCompanyListByGroupId()
        {
            CompanyRepository oRepository = new CompanyRepository();
            return oRepository.GetCompanyList(_company);
        }
    }
}