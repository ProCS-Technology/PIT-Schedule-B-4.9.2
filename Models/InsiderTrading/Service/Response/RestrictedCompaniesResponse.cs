using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class RestrictedCompaniesResponse : BaseResponse
    {
        private RestrictedCompanies _restrictedCompanies;
        private List<RestrictedCompanies> lstRestrictedCompanies;
        public RestrictedCompanies restrictedCompanies
        {
            set
            {
                _restrictedCompanies = value;
            }
            get
            {
                return _restrictedCompanies;
            }
        }
        public List<RestrictedCompanies> RestrictedCompaniesList
        {
            set
            {
                lstRestrictedCompanies = value;
            }
            get
            {
                return lstRestrictedCompanies;
            }
        }
        public void AddObject(RestrictedCompanies o)
        {
            if (lstRestrictedCompanies == null)
            {
                lstRestrictedCompanies = new List<RestrictedCompanies>();
            }
            lstRestrictedCompanies.Add(o);
        }
    }
}