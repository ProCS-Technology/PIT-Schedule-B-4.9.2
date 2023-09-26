using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class DesignationRequest
    {
        private Designation _designation;

        public DesignationRequest()
        {

        }

        public DesignationRequest(Designation designation)
        {
            _designation = new Designation();
            _designation = designation;

        }

        public DesignationResponse SaveDesignation()
        {
            DesignationRepository oRepository = new DesignationRepository();
            if (_designation.DESIGNATION_ID == 0)
            {
                return oRepository.AddDesignation(_designation);
            }
            else
            {
                return oRepository.UpdateDesignation(_designation);
            }
        }

        public DesignationResponse DeleteDesignation()
        {

            DesignationRepository oRepository = new DesignationRepository();
            return oRepository.DeleteDesignation(_designation);
        }

        public DesignationResponse GetDesignationList()
        {
            DesignationRepository oRepository = new DesignationRepository();
            return oRepository.GetDesignationList(_designation);
        }
    }
}