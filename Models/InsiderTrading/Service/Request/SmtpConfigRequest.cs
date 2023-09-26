using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class SmtpConfigRequest
    {
        private SmtpConfiguration _smtpConfig;

        public SmtpConfigRequest()
        {

        }

        public SmtpConfigRequest(SmtpConfiguration smtpConfig)
        {
            _smtpConfig = new SmtpConfiguration();
            _smtpConfig = smtpConfig;

        }

        public SmtpConfigResponse SaveSmtpConfig()
        {
            SmtpConfigRepository oRepository = new SmtpConfigRepository();
            if (_smtpConfig.SMPT_CONFIG_ID == 0)
            {
                return oRepository.AddSmtpConfig(_smtpConfig);
            }
            else
            {
                return oRepository.UpdateSmtpConfig(_smtpConfig);
            }
        }

        public SmtpConfigResponse DeleteSmtpConfig()
        {

            SmtpConfigRepository oRepository = new SmtpConfigRepository();
            return oRepository.DeleteSmtpConfig(_smtpConfig);
        }

        public SmtpConfigResponse GetSmtpConfigList()
        {
            SmtpConfigRepository oRepository = new SmtpConfigRepository();
            return oRepository.GetSmtpConfigList(_smtpConfig);
        }
    }
}