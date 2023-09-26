using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class BusinessUnitResponse : BaseResponse
    {
        private BusinessUnit _businessUnit;
        private List<BusinessUnit> _lstBusinessUnit;

        public BusinessUnit BusinessUnit
        {
            set
            {
                _businessUnit = value;
            }
            get
            {
                return _businessUnit;
            }
        }

        public List<BusinessUnit> BusinessUnitList
        {
            set
            {
                _lstBusinessUnit = value;
            }
            get
            {
                return _lstBusinessUnit;
            }
        }

        public void AddObject(BusinessUnit o)
        {
            if (_lstBusinessUnit == null)
            {
                _lstBusinessUnit = new List<BusinessUnit>();
            }
            _lstBusinessUnit.Add(o);
        }
    }
}