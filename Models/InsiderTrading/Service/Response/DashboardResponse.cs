
using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class DashboardResponse : BaseResponse
    {
        private Dashboard _dashboard;
        public List<DashboardActionable> lstActioanble { set; get; }
        public DashboardResponse()
        {

        }

        public Dashboard Dashboard
        {
            set
            {
                _dashboard = value;
            }
            get
            {
                return _dashboard;
            }
        }
    }
}