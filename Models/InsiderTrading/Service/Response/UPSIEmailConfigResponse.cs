using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSIEmailConfigResponse : BaseResponse
    {
        private UPSIEmailConfig _uPSIEmailConfig;
        private List<UPSIEmailConfig> _lstEmailConfig;
        public UPSIEmailConfig upsiEmailConfig
        {
            set
            {
                _uPSIEmailConfig = value;
            }
            get
            {
                return _uPSIEmailConfig;
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