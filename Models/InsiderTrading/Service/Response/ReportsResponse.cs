using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ReportsResponse : BaseResponse
    {
        private DeclarationReport _DeclarationReport;
        private List<TradingReport> _lstTradingReport;
        private List<BenposHeader> _lstBenposHeader;
        private List<BenposReport> _lstBenposReport;
        private List<UPSICommunication> _lstUPSIReport;
        private List<LogsReport> _lstLogsReport;
        private List<User> _lstDisclouserReport;
        public DeclarationReport DeclarationReport
        {
            set
            {
                _DeclarationReport = value;
            }
            get
            {
                return _DeclarationReport;
            }
        }

        public List<TradingReport> lstTradingReport
        {
            set
            {
                _lstTradingReport = value;
            }
            get
            {
                return _lstTradingReport;
            }
        }

        public List<BenposHeader> lstBenposHeader
        {
            set
            {
                _lstBenposHeader = value;
            }
            get
            {
                return _lstBenposHeader;
            }
        }

        public List<BenposReport> lstBenposReport
        {
            set
            {
                _lstBenposReport = value;
            }
            get
            {
                return _lstBenposReport;
            }
        }

        public List<UPSICommunication> lstUPSIReport
        {
            set
            {
                _lstUPSIReport = value;
            }
            get
            {
                return _lstUPSIReport;
            }
        }
        public void AddObject(TradingReport o)
        {
            if (lstTradingReport == null)
            {
                lstTradingReport = new List<TradingReport>();
            }
            lstTradingReport.Add(o);
        }
        public List<LogsReport> lstLogsReport
        {
            set
            {
                _lstLogsReport = value;
            }
            get
            {
                return _lstLogsReport;
            }
        }

        public List<User> lstDisclouserReport
        {
            set
            {
                _lstDisclouserReport = value;
            }
            get
            {
                return _lstDisclouserReport;
            }
        }
    }
}