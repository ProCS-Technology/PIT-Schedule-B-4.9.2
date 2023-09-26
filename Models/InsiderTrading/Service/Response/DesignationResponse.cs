using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class DesignationResponse : BaseResponse
    {
        private Designation _designation;
        private List<Designation> lstDesignation;
        public Designation Designation
        {
            set
            {
                _designation = value;
            }
            get
            {
                return _designation;
            }
        }
        public List<Designation> DesignationList
        {
            set
            {
                lstDesignation = value;
            }
            get
            {
                return lstDesignation;
            }
        }
        public void AddObject(Designation o)
        {
            if (lstDesignation == null)
            {
                lstDesignation = new List<Designation>();
            }
            lstDesignation.Add(o);
        }
    }
}