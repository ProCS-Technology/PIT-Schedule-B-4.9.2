using ProcsDLL.Models.InsiderTrading.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class AnnualDisclosureTaskResponse : BaseResponse
    {
        private AnnualDisclosureTask _AnnualDisclosureTask;

        private List<AnnualDisclosureTask> lstAnnualDisclosureTask;

        public AnnualDisclosureTask AnnualDisclosureTask
        {
            set
            {
                _AnnualDisclosureTask = value;
            }
            get
            {
                return _AnnualDisclosureTask;
            }
        }

        public List<AnnualDisclosureTask> AnnualDisclosureTaskList
        {
            set
            {
                lstAnnualDisclosureTask = value;
            }
            get
            {
                return lstAnnualDisclosureTask;
            }
        }

        public void AddObject(AnnualDisclosureTask o)
        {
            if (lstAnnualDisclosureTask == null)
            {
                lstAnnualDisclosureTask = new List<AnnualDisclosureTask>();
            }
            lstAnnualDisclosureTask.Add(o);
        }
    }
}