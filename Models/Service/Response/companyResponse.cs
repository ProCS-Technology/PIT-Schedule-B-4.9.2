using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class companyResponse : BaseResponse
    {
        private Company _company;
        private List<Company> lstCompany;
        public Company company
        {
            set
            {
                _company = value;
            }
            get
            {
                return _company;
            }
        }
        public List<Company> companyList
        {
            set
            {
                lstCompany = value;
            }
            get
            {
                return lstCompany;
            }
        }
        public void AddObject(Company o)
        {
            if (lstCompany == null)
            {
                lstCompany = new List<Company>();
            }
            lstCompany.Add(o);
        }
    }
}