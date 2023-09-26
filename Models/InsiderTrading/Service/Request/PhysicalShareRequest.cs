using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class PhysicalShareRequest
    {
        private PhysicalShareMaster _physicalshare;


        public PhysicalShareRequest()
        {
            _physicalshare = new PhysicalShareMaster();
        }

        public PhysicalShareRequest(PhysicalShareMaster dep)
        {
            _physicalshare = new PhysicalShareMaster();
            _physicalshare = dep;
        }

        public PhysicalShareResponce SavePhysicalShare()
        {

            PhysicalShareRepository shareRepository = new PhysicalShareRepository();
            if (_physicalshare.physical_share_id == 0)
            {
                return shareRepository.AddPhysicalShare(_physicalshare);
            }
            else
            {
                return shareRepository.UpdatePhysicalShare(_physicalshare);
            }

            return null;
        }

        public PhysicalShareResponce viewPhysicalShare()
        {

            PhysicalShareRepository shareRepository = new PhysicalShareRepository();

            return shareRepository.viewPhysicalShare(_physicalshare);



        }
        public PhysicalShareResponce editPhysicalShare()
        {

            PhysicalShareRepository shareRepository = new PhysicalShareRepository();

            return shareRepository.editPhysicalShare(_physicalshare);



        }
        public PhysicalShareResponce DeletePhysicalShare()
        {

            PhysicalShareRepository shareRepository = new PhysicalShareRepository();

            return shareRepository.deletePhysicalShare(_physicalshare);



        }
    }
}