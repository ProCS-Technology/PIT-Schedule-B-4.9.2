using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class RestrictedCompaniesRequest
    {
        private RestrictedCompanies _restrictedCompanies;
        private RestrictedCompaniesRepository resCompaniesRepository;
        private RestrictedCompaniesResponse resCompaniesResponse;

        public RestrictedCompaniesRequest()
        {

        }

        public RestrictedCompaniesRequest(RestrictedCompanies restrictedCompanies)
        {
            _restrictedCompanies = new RestrictedCompanies();
            _restrictedCompanies = restrictedCompanies;
        }

        public RestrictedCompaniesResponse SaveRestrictedCompanies()
        {
            resCompaniesRepository = new RestrictedCompaniesRepository();
            try
            {
                if (_restrictedCompanies.ID == 0)
                {
                    return resCompaniesRepository.AddRestrictedCompanies(_restrictedCompanies);
                }
                else
                {
                    return resCompaniesRepository.UpdateRestrictedCompanies(_restrictedCompanies);
                }
            }
            catch (Exception ex)
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();
                resCompaniesResponse.StatusFl = false;
                resCompaniesResponse.Msg = "Processing failed, because of system error !";
                return resCompaniesResponse;
            }
        }

        public RestrictedCompaniesResponse DeleteRestrictedCompanies()
        {
            try
            {
                resCompaniesRepository = new RestrictedCompaniesRepository();
                return resCompaniesRepository.DeleteRestrictedCompanies(_restrictedCompanies);
            }
            catch (Exception ex)
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();
                resCompaniesResponse.StatusFl = false;
                resCompaniesResponse.Msg = "Processing failed, because of system error !";
                return resCompaniesResponse;
            }
        }

        public RestrictedCompaniesResponse GetRestrictedCompaniesList()
        {
            try
            {
                resCompaniesRepository = new RestrictedCompaniesRepository();
                return resCompaniesRepository.GetRestrictedCompaniesList(_restrictedCompanies);
            }
            catch (Exception ex)
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();
                resCompaniesResponse.StatusFl = false;
                resCompaniesResponse.Msg = "Processing failed, because of system error !";
                return resCompaniesResponse;
            }
        }

        public RestrictedCompaniesResponse UpdateIsRestrictedCompanies()
        {
            resCompaniesRepository = new RestrictedCompaniesRepository();
            try
            {
                return resCompaniesRepository.UpdateIsRestrictedCompanies(_restrictedCompanies);

            }
            catch (Exception ex)
            {
                resCompaniesResponse = new RestrictedCompaniesResponse();
                resCompaniesResponse.StatusFl = false;
                resCompaniesResponse.Msg = "Processing failed, because of system error !";
                return resCompaniesResponse;
            }
        }
    }
}