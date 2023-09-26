using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class PolicyResponse : BaseResponse
    {
        private Policy _policy;
        private List<Policy> lstPolicy;
        public Policy Policy
        {
            set
            {
                _policy = value;
            }
            get
            {
                return _policy;
            }
        }
        public List<Policy> PolicyList
        {
            set
            {
                lstPolicy = value;
            }
            get
            {
                return lstPolicy;
            }
        }
        public void AddObject(Policy o)
        {
            if (lstPolicy == null)
            {
                lstPolicy = new List<Policy>();
            }
            lstPolicy.Add(o);
        }
    }
}