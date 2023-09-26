using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class EmailConfigResponse : BaseResponse
    {
        private UPSIEmailConfig _emailConfig;
        private List<UPSIEmailConfig> _lstEmailConfig;
        public UPSIEmailConfig emailConfig
        {
            set
            {
                _emailConfig = value;
            }
            get
            {
                return _emailConfig;
            }
        }
        public List<UPSIEmailConfig> ListEmailConfig
        {
            set
            {
                _lstEmailConfig = value;
            }
            get
            {
                return _lstEmailConfig;
            }
        }
    }
}