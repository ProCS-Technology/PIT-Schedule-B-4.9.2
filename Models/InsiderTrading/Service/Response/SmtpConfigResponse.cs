using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class SmtpConfigResponse : BaseResponse
    {
        private SmtpConfiguration _smtpConfig;
        private List<SmtpConfiguration> lstSmtpConfig;
        public SmtpConfiguration SmtpConfig
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
        public List<SmtpConfiguration> SmtpConfigList
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
        public void AddObject(SmtpConfiguration o)
        {
            if (lstSmtpConfig == null)
            {
                lstSmtpConfig = new List<SmtpConfiguration>();
            }
            lstSmtpConfig.Add(o);
        }
    }
}