using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class PolicyRequest
    {
        private Policy _policy;
        public PolicyRequest()
        {
            _policy = new Policy();
        }

        public PolicyRequest(Policy pol)
        {
            _policy = new Policy();
            _policy = pol;
        }

        public PolicyResponse SavePolicy()
        {
            _policy.Validate();

            if (_policy.GetRules().Count == 0)
            {

                PolicyRepository oRepository = new PolicyRepository();
                if (_policy.POLICY_ID == 0)
                {
                    return oRepository.AddPolicy(_policy);
                }
                else
                {
                    return oRepository.UpdatePolicy(_policy);
                }
            }
            return null;
        }

        public PolicyResponse DeletePolicy()
        {
            PolicyRepository oRepository = new PolicyRepository();
            return oRepository.DeletePolicy(_policy);
        }

        public PolicyResponse GetPolicyList()
        {
            PolicyRepository oRepository = new PolicyRepository();
            return oRepository.GetPolicyList(_policy);
        }

        public PolicyResponse GetAllPolicyDocumentsList()
        {
            PolicyRepository oRepository = new PolicyRepository();
            return oRepository.GetAllPolicyDocumentsList(_policy);
        }
    }
}