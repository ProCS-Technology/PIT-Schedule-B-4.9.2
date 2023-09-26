using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSIResponse : BaseResponse
    {
        private UPSI _Upsi;

        private List<UPSI> lstupsi;



        public UPSI upsi
        {
            set
            {
                _Upsi = value;
            }
            get
            {
                return _Upsi;
            }
        }
        public List<UPSI> upsilist
        {
            set
            {
                lstupsi = value;
            }
            get
            {
                return lstupsi;
            }
        }

        public void AddObject(UPSI o)
        {
            if (lstupsi == null)
            {
                lstupsi = new List<UPSI>();
            }
            lstupsi.Add(o);
        }
    }
}