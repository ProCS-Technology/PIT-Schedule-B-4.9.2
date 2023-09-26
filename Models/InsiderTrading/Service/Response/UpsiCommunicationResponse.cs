using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;


namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UpsiCommunicationResponse : BaseResponse
    {
        private UpsiCommunicationtype _UpsiCommunication;
        private List<UpsiCommunicationtype> lstUpsiCommunication;

        public UpsiCommunicationtype UpsiCommunicationtype
        {
            set
            {
                _UpsiCommunication = value;
            }
            get
            {
                return _UpsiCommunication;
            }
        }

        public List<UpsiCommunicationtype> CommunicationtypeList
        {
            set
            {
                lstUpsiCommunication = value;
            }
            get
            {
                return lstUpsiCommunication;
            }
        }


        public void AddObject(UpsiCommunicationtype o)
        {
            if (lstUpsiCommunication == null)
            {
                lstUpsiCommunication = new List<UpsiCommunicationtype>();
            }
            lstUpsiCommunication.Add(o);
        }
    }
}