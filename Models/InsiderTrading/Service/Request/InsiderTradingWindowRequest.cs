using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class InsiderTradingWindowRequest
    {
        private InsiderTradingWindow _InsiderTradingWindow;

        public InsiderTradingWindowRequest()
        {

        }

        public InsiderTradingWindowRequest(InsiderTradingWindow InsiderTradingWindow)
        {
            _InsiderTradingWindow = new InsiderTradingWindow();
            _InsiderTradingWindow = InsiderTradingWindow;
        }

        public InsiderTradingWindowResponse SaveInsiderTradingWindow()
        {
            InsiderTradingWindowRepository oRepository = new InsiderTradingWindowRepository();
            if (_InsiderTradingWindow.id == 0)
            {
                return oRepository.SaveInsiderTradingWindow(_InsiderTradingWindow);
            }
            else
            {
                return oRepository.UpdateInsiderTradingWindow(_InsiderTradingWindow);
            }
        }

        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfo()
        {
            InsiderTradingWindowRepository oRepositry = new InsiderTradingWindowRepository();
            return oRepositry.GetInsiderTradingWindowClosureInfo(_InsiderTradingWindow);
        }

        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfoList()
        {
            InsiderTradingWindowRepository oRepositry = new InsiderTradingWindowRepository();
            return oRepositry.GetInsiderTradingWindowClosureInfoList(_InsiderTradingWindow);
        }

        public InsiderTradingWindowResponse SendEmailForTradingWindowClosure()
        {
            InsiderTradingWindowRepository oRepository = new InsiderTradingWindowRepository();
            return oRepository.SendEmailForTradingWindowClosure(_InsiderTradingWindow);
        }

        public InsiderTradingWindowResponse GetEmailTemplate()
        {
            InsiderTradingWindowRepository oRepository = new InsiderTradingWindowRepository();
            return oRepository.GetEmailTemplate(_InsiderTradingWindow);
        }
    }
}