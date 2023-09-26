using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class SecurityTypeRequest
    {
        private SecurityType _secType;
        public SecurityTypeRequest() { }
        public SecurityTypeRequest(SecurityType secType) {
            _secType = secType;
        }
        public SecurityTypeResponse GetTradableSecurity()
        {
            SecurityTypeRepository oRepository = new SecurityTypeRepository();
            return oRepository.GetTradableSecurity(_secType);
        }
    }
}