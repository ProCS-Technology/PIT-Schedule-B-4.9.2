using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class DepartmentRequest
    {
        private Department _department;
        //public DepartmentRequest(CompanyDTO companyDTO)
        //{
        //    _department = new Group();
        //}

        public DepartmentRequest()
        {
            _department = new Department();
        }

        public DepartmentRequest(Department dep)
        {
            _department = new Department();
            _department = dep;
        }

        public DepartmentResponse SaveDepartment()
        {
            _department.Validate();

            if (_department.GetRules().Count == 0)
            {

                DepartmentRepository oRepository = new DepartmentRepository();
                if (_department.DEPARTMENT_ID == 0)
                {
                    return oRepository.AddDepartment(_department);
                }
                else
                {
                    return oRepository.UpdateDepartment(_department);
                }
            }
            return null;
        }


        public DepartmentResponse DeleteDepartment()
        {
            DepartmentRepository oRepository = new DepartmentRepository();
            return oRepository.DeleteDepartment(_department);
        }

        public DepartmentResponse GetDepartmentList()
        {
            DepartmentRepository oRepository = new DepartmentRepository();
            return oRepository.GetDepartmentList(_department);
        }
    }
}