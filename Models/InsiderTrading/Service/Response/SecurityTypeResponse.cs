using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class SecurityTypeResponse : BaseResponse
    {
        private SecurityType _securityType;
        private List<SecurityType> lstSecurityType;
        public SecurityType securityType
        {
            set
            {
                _securityType = value;
            }
            get
            {
                return _securityType;
            }
        }
        public List<SecurityType> SecurityTypeList
        {
            set
            {
                lstSecurityType = value;
            }
            get
            {
                return lstSecurityType;
            }
        }
        public void AddObject(SecurityType o)
        {
            if (lstSecurityType == null)
            {
                lstSecurityType = new List<SecurityType>();
            }
            lstSecurityType.Add(o);
        }
    }
}