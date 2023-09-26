using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class BusinessUnitRequest
    {
        private BusinessUnit _businessUnit;
        public BusinessUnitRequest()
        {
            _businessUnit = new BusinessUnit();
        }

        public BusinessUnitRequest(BusinessUnit businessUnit)
        {
            _businessUnit = new BusinessUnit();
            _businessUnit = businessUnit;
        }

        public BusinessUnitResponse GetBusinessUnit()
        {

            BusinessUnitRepository oRepository = new BusinessUnitRepository();
            return oRepository.GetBusinessUnit(_businessUnit);
        }

        public BusinessUnitResponse GetAllBusinessUnit()
        {

            BusinessUnitRepository oRepository = new BusinessUnitRepository();
            return oRepository.GetAllBusinessUnit(_businessUnit);
        }
    }
}