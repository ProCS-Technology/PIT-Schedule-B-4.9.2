using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class InsiderTradingWindowResponse : BaseResponse
    {
        private InsiderTradingWindow _InsiderTradingWindow;

        private List<InsiderTradingWindow> lstInsiderTradingWindow;

        public InsiderTradingWindow InsiderTradingWindow
        {
            set
            {
                _InsiderTradingWindow = value;
            }
            get
            {
                return _InsiderTradingWindow;
            }
        }

        public List<InsiderTradingWindow> InsiderTradingWindowList
        {
            set
            {
                lstInsiderTradingWindow = value;
            }
            get
            {
                return lstInsiderTradingWindow;
            }
        }

        public void AddObject(InsiderTradingWindow o)
        {
            if (lstInsiderTradingWindow == null)
            {
                lstInsiderTradingWindow = new List<InsiderTradingWindow>();
            }
            lstInsiderTradingWindow.Add(o);
        }
    }
}