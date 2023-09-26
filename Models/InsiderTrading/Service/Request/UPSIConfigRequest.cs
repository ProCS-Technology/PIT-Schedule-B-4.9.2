using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIConfigRequest
    {
        private UPSIConfig _upsiConfig;
        private UPSIEmailConfig _upsiEmailConfig;
        private UPSIConfiguration _smtpConfig;
        public UPSIConfigRequest()
        {

        }
        public UPSIConfigRequest(UPSIConfiguration smtpConfig)
        {
            _smtpConfig = new UPSIConfiguration();
            _smtpConfig = smtpConfig;

        }
        public UPSIConfigRequest(UPSIConfig upsiConfig)
        {
            _upsiConfig = upsiConfig;
        }
        public UPSIConfigRequest(UPSIEmailConfig upsiEmailConfig)
        {
            _upsiEmailConfig = upsiEmailConfig;
        }
        public UPSIConfigResponse SaveSmtpConfig()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            if (_smtpConfig.SMPT_CONFIG_ID == 0)
            {
                return oRepository.AddSmtpConfig(_smtpConfig);
            }
            else
            {
                return oRepository.UpdateSmtpConfig(_smtpConfig);
            }
        }
        public UPSIConfigResponse DeleteSmtpConfig()
        {

            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.DeleteSmtpConfig(_smtpConfig);
        }
        public UPSIConfigResponse GetSmtpConfigList()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.GetSmtpConfigList(_smtpConfig);
        }
        public UPSIConfigResponse GetUPSIConfig()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.GetUPSIConfig(_upsiConfig);
        }
        public UPSIConfigResponse AddUPSIConfig()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.AddUPSIConfig(_upsiConfig);
        }
        public UPSIEmailConfigResponse GetUPSIEmailConfig()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.GetUPSIEmailConfig(_upsiEmailConfig);
        }
        public UPSIEmailConfigResponse AddBasicUPSIEmailConfig()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.AddBasicUPSIEmailConfig(_upsiEmailConfig);
        }
        public UPSIEmailConfigResponse AddSmartUPSIEmailConfig()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.AddSmartUPSIEmailConfig(_upsiEmailConfig);
        }
        public UPSIEmailConfigResponse GetUPSIEmailConfigById()
        {
            UPSIConfigRepository oRepository = new UPSIConfigRepository();
            return oRepository.GetUPSIEmailConfigById(_upsiEmailConfig);
        }
    }
}