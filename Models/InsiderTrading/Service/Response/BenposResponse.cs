using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class BenposResponse : BaseResponse
    {
        private BenposHeader _benposHeader;
        private List<BenposHeader> lstBenposHdr;
        private BenposDetail _benposDetail;
        private List<BenposDetail> lstBenposDtl;

        public BenposHeader BenposHeader
        {
            set
            {
                _benposHeader = value;
            }
            get
            {
                return _benposHeader;
            }
        }
        public List<BenposHeader> BenposHeaderList
        {
            set
            {
                lstBenposHdr = value;
            }
            get
            {
                return lstBenposHdr;
            }
        }
        public void AddObject(BenposHeader o)
        {
            if (lstBenposHdr == null)
            {
                lstBenposHdr = new List<BenposHeader>();
            }
            lstBenposHdr.Add(o);
        }
    }
}