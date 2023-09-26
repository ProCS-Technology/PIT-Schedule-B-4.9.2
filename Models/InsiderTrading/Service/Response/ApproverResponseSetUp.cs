
using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ApproverResponseSetUp : BaseResponse
    {
        private ApproverSetUp _ApproverSetUp;

        private List<ApproverSetUp> lstApproverSetUp;


        public ApproverSetUp approverSetUp
        {
            set
            {
                _ApproverSetUp = value;
            }
            get
            {
                return _ApproverSetUp;
            }
        }
        public List<ApproverSetUp> ApproverSetUpList
        {
            set
            {
                lstApproverSetUp = value;
            }
            get
            {
                return lstApproverSetUp;
            }
        }
        public void AddObject(ApproverSetUp o)
        {
            if (lstApproverSetUp == null)
            {
                lstApproverSetUp = new List<ApproverSetUp>();
            }
            lstApproverSetUp.Add(o);
        }
    }
}