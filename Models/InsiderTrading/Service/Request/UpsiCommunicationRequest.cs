using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UpsiCommunicationRequest
    {
        private UpsiCommunicationtype _UpsiCommunication;

        public UpsiCommunicationRequest()
        {

        }
        public UpsiCommunicationRequest(UpsiCommunicationtype UpsiCommunicationtype)
        {
            _UpsiCommunication = new UpsiCommunicationtype();
            _UpsiCommunication = UpsiCommunicationtype;

        }

        public UpsiCommunicationResponse GetCommunicationtypeList()
        {
            UpsiCommunicationRepository oRepository = new UpsiCommunicationRepository();
            return oRepository.GetCommunicationtypeList(_UpsiCommunication);
        }

        public UpsiCommunicationResponse SaveCommunicationtype()
        {
            UpsiCommunicationRepository oRepository = new UpsiCommunicationRepository();
            if (_UpsiCommunication.COMMTYPE_ID == 0)
            {
                return oRepository.SaveCommunicationtype(_UpsiCommunication);
            }
            else
            {
                return oRepository.UPDATECommunicationtype(_UpsiCommunication);
            }
        }

        public UpsiCommunicationResponse DeleteCommunicationtype()
        {

            UpsiCommunicationRepository oRepository = new UpsiCommunicationRepository();
            return oRepository.DeleteCommunicationtype(_UpsiCommunication);
        }




    }
}