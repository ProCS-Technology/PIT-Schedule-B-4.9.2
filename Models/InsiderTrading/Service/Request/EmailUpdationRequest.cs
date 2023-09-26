using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class EmailUpdationRequest
    {
        private EmailUpdations _Email;


        public EmailUpdationRequest()
        {

        }

        public EmailUpdationRequest(EmailUpdations email)
        {
            _Email = new EmailUpdations();
            _Email = email;

        }

        public EmailUpdationResponse ListEmail()
        {

            EmailUpdationRepository oRepository = new EmailUpdationRepository();
            return oRepository.ListEmail(_Email);

        }

        public EmailUpdationResponse UpdateEmail()
        {

            EmailUpdationRepository oRepository = new EmailUpdationRepository();
            return oRepository.UpdateEmail(_Email);

        }


    }
}