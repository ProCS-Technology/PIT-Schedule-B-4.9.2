using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ApproverRequest
    {
        private ApproverSetUp _ApproverSetUp;

        public ApproverRequest()
        {

        }

        public ApproverRequest(ApproverSetUp user)
        {
            _ApproverSetUp = new ApproverSetUp();
            _ApproverSetUp = user;
        }

        public ApproverResponseSetUp SaveApproverSetUp()
        {
            _ApproverSetUp.Validate();

            if (_ApproverSetUp.GetRules().Count == 0)
            {

                ApproverSetupRepository oRepository = new ApproverSetupRepository();
                if (_ApproverSetUp.WF_ID == 0)
                {
                    return oRepository.AddApproverSetUp(_ApproverSetUp);
                }
                else
                {
                    return oRepository.UpdateApproverSetUp(_ApproverSetUp);
                }
            }
            return null;
        }

        public ApproverResponseSetUp GetApproverSetUpLIST()
        {
            ApproverSetupRepository oRepository = new ApproverSetupRepository();
            return oRepository.GetApproverSetUpLIST(_ApproverSetUp);
        }

        public ApproverResponseSetUp GetApproverSetUpById()
        {
            ApproverSetupRepository oRepository = new ApproverSetupRepository();
            return oRepository.GetApproverSetUpById(_ApproverSetUp);
        }

        public ApproverResponseSetUp DeleteApproverSetUp()
        {
            ApproverSetupRepository oRepository = new ApproverSetupRepository();
            return oRepository.DeleteApproverSetUp(_ApproverSetUp);
        }
    }
}