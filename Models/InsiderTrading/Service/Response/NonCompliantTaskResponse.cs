using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class NonCompliantTaskResponse : BaseResponse
    {
        private NonCompliantTask _nonCompliantTask;
        private List<NonCompliantTask> lstNonCompliantTask;
        private List<BenposNonCompliant> lstBenposNonCompliant;
        public NonCompliantTask NonCompliantTask
        {
            set
            {
                _nonCompliantTask = value;
            }
            get
            {
                return _nonCompliantTask;
            }
        }
        public List<NonCompliantTask> NonCompliantTaskList
        {
            set
            {
                lstNonCompliantTask = value;
            }
            get
            {
                return lstNonCompliantTask;
            }
        }
        public void AddObject(NonCompliantTask o)
        {
            if (lstNonCompliantTask == null)
            {
                lstNonCompliantTask = new List<NonCompliantTask>();
            }
            lstNonCompliantTask.Add(o);
        }

        public List<BenposNonCompliant> BenposNonCompliantList
        {
            set
            {
                lstBenposNonCompliant = value;
            }
            get
            {
                return lstBenposNonCompliant;
            }
        }
        public void AddBenposObject(BenposNonCompliant o)
        {
            if (lstBenposNonCompliant == null)
            {
                lstBenposNonCompliant = new List<BenposNonCompliant>();
            }
            lstBenposNonCompliant.Add(o);
        }
    }
}