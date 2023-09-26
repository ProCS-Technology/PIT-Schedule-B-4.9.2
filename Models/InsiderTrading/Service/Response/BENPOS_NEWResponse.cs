using ProcsDLL.Models.InsiderTrading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class BENPOS_NEWResponse : BaseResponse
    {
        private BenposHeader _benposHeader;
        private List<BenposHeader> lstBenposHdr;
        private BENPOS_NEWDetail _benposDetail;
        private List<BENPOS_NEWDetail> lstBenposDtl;

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