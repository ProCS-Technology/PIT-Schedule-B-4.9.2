using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class DematAccountRequest
    {
        private DematAccount _dematAccount;

        public DematAccountRequest()
        {
            _dematAccount = new DematAccount();
        }

        public DematAccountRequest(DematAccount dematAccount)
        {
            _dematAccount = new DematAccount();
            _dematAccount = dematAccount;
        }

        public DematAccountResponse GetDematAccountInfo()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDematAccountInfo(_dematAccount);
        }
    }
}