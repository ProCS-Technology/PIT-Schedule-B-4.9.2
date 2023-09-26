using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class EmailConfigRequest
    {
        private UPSIEmailConfig _upsiEmailConfig;
        public EmailConfigRequest()
        {
        }
        public EmailConfigRequest(UPSIEmailConfig upsiEmailConfig)
        {
            _upsiEmailConfig = upsiEmailConfig;
        }
        public EmailConfigResponse GetEmailConfig()
        {
            EmailConfigRepository oRepository = new EmailConfigRepository();
            return oRepository.GetEmailConfig(_upsiEmailConfig);
        }
        public EmailConfigResponse AddBasicEmailConfig()
        {
            EmailConfigRepository oRepository = new EmailConfigRepository();
            return oRepository.AddBasicEmailConfig(_upsiEmailConfig);
        }
        public EmailConfigResponse AddSmartEmailConfig()
        {
            EmailConfigRepository oRepository = new EmailConfigRepository();
            return oRepository.AddSmartEmailConfig(_upsiEmailConfig);
        }
    }
}