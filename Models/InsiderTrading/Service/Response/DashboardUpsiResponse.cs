using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProcsDLL.Models.InsiderTrading.Model;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class DashboardUpsiResponse : BaseResponse
    {
        private DashboardUpsi _dashboardUpsi;

            public DashboardUpsiResponse()

        {

        }

        public DashboardUpsi DashboardUpsi
        {
            set
            {
                _dashboardUpsi = value;
            }
            get
            {
                return _dashboardUpsi;
            }
        }
    }

}