using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSIConfigResponse : BaseResponse
    {
        private UPSIConfig _upsiConfig; 
        private UPSIConfiguration _smtpConfig;
        private List<UPSIConfiguration> lstSmtpConfig;
        public UPSIConfig UpsiConfig
        {
            set
            {
                _upsiConfig = value;
            }
            get
            {
                return _upsiConfig;
            }
        }
        public UPSIConfiguration SmtpConfig
        {
            set
            {
                _smtpConfig = value;
            }
            get
            {
                return _smtpConfig;
            }
        }
        public List<UPSIConfiguration> SmtpConfigList
        {
            set
            {
                lstSmtpConfig = value;
            }
            get
            {
                return lstSmtpConfig;
            }
        }
        public void AddObject(UPSIConfiguration o)
        {
            if (lstSmtpConfig == null)
            {
                lstSmtpConfig = new List<UPSIConfiguration>();
            }
            lstSmtpConfig.Add(o);
        }
    }
}