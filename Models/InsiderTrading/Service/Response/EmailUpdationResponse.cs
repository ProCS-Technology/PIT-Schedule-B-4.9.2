using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class EmailUpdationResponse : BaseResponse
    {

        private EmailUpdations _Email;

        private List<EmailUpdations> lstEmail;
        public EmailUpdations Emails
        {
            set
            {
                _Email = value;
            }
            get
            {
                return _Email;
            }
        }
        public List<EmailUpdations> ListEmails
        {
            set
            {
                lstEmail = value;
            }
            get
            {
                return lstEmail;
            }
        }

    }
}