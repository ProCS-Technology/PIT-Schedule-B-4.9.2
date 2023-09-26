using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class BenposMappingResponse : BaseResponse
    {
        private BenposMapping _benposMapping;
        private List<BenposMapping> lstBenposMapping;
        public BenposMapping benposMapping
        {
            set
            {
                _benposMapping = value;
            }
            get
            {
                return _benposMapping;
            }
        }
        public List<BenposMapping> BenposMappingList
        {
            set
            {
                lstBenposMapping = value;
            }
            get
            {
                return lstBenposMapping;
            }
        }
        public void AddObject(BenposMapping o)
        {
            if (lstBenposMapping == null)
            {
                lstBenposMapping = new List<BenposMapping>();
            }
            lstBenposMapping.Add(o);
        }
    }
}