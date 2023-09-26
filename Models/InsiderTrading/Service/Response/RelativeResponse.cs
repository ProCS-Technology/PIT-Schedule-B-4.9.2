using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class RelativeResponse : BaseResponse
    {
        private Relative _relative;
        private List<Relative> lstRelative;

        public Relative Relative
        {
            set
            {
                _relative = value;
            }
            get
            {
                return _relative;
            }
        }

        public List<Relative> RelativeList
        {
            set
            {
                lstRelative = value;
            }
            get
            {
                return lstRelative;
            }
        }

        public void AddObject(Relative o)
        {
            if (lstRelative == null)
            {
                lstRelative = new List<Relative>();
            }
            lstRelative.Add(o);
        }
    }
}