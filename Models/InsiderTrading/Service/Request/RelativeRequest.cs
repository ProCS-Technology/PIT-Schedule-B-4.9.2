using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class RelativeRequest
    {
        private Relative _relative;

        public RelativeRequest()
        {
            _relative = new Relative();
        }

        public RelativeRequest(Relative rel)
        {
            _relative = new Relative();
            _relative = rel;
        }

        public RelativeResponse GetRelativeInformationById()
        {
            RelativeRepository oRepository = new RelativeRepository();
            return oRepository.GetRelativeInformationById(_relative);
        }

        public RelativeResponse GetDematAccountListByRelativeId()
        {
            UserRepository oRepository = new UserRepository();
            return oRepository.GetDematAccountListByRelativeId(_relative);
        }
    }
}