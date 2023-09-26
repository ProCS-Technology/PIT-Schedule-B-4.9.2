using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class DepartmentResponse : BaseResponse
    {
        private Department _department;
        private List<Department> lstDepartment;
        public Department Department
        {
            set
            {
                _department = value;
            }
            get
            {
                return _department;
            }
        }
        public List<Department> DepartmentList
        {
            set
            {
                lstDepartment = value;
            }
            get
            {
                return lstDepartment;
            }
        }
        public void AddObject(Department o)
        {
            if (lstDepartment == null)
            {
                lstDepartment = new List<Department>();
            }
            lstDepartment.Add(o);
        }
    }
}