using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class PhysicalShareResponce : BaseResponse
    {
        private PhysicalShareMaster _physicalshare;
        private List<PhysicalShareMaster> lstphysicalshare;

        public PhysicalShareMaster PhysicalShare
        {
            set
            {
                _physicalshare = value;
            }
            get
            {
                return _physicalshare;
            }
        }
        public List<PhysicalShareMaster> PhysicalShareList
        {
            set
            {
                lstphysicalshare = value;
            }
            get
            {
                return lstphysicalshare;
            }
        }
        public void AddObject(PhysicalShareMaster o)
        {
            if (lstphysicalshare == null)
            {
                lstphysicalshare = new List<PhysicalShareMaster>();
            }
            lstphysicalshare.Add(o);
        }
    }
}