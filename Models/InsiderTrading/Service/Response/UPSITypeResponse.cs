using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UPSITypeResponse : BaseResponse
    {
        private UPSIType _upsiType;
        private List<UPSIType> _lstUPSIType;
        public UPSIType UPSITyp
        {
            set
            {
                _upsiType = value;
            }
            get
            {
                return _upsiType;
            }
        }
        public List<UPSIType> UPSITypeList
        {
            set
            {
                _lstUPSIType = value;
            }
            get
            {
                return _lstUPSIType;
            }
        }
        public void AddObject(UPSIType o)
        {
            if (_lstUPSIType == null)
            {
                _lstUPSIType = new List<UPSIType>();
            }
            _lstUPSIType.Add(o);
        }
    }
}