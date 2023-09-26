using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class CPResponse : BaseResponse
    {
        private CP _cp;
        private List<CP> lstCP;
        public CP cp
        {
            set
            {
                _cp = value;
            }
            get
            {
                return _cp;
            }
        }
        public List<CP> CPList
        {
            set
            {
                lstCP = value;
            }
            get
            {
                return lstCP;
            }
        }
        public void AddObject(CP o)
        {
            if (lstCP == null)
            {
                lstCP = new List<CP>();
            }
            lstCP.Add(o);
        }
        public string sException { set; get; }
    }
}