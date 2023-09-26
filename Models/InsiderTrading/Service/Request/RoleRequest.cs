using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class RoleRequest
    {
        private Role _role;
        public RoleRequest()
        {
            _role = new Role();
        }

        public RoleRequest(Role rol)
        {
            _role = new Role();
            _role = rol;
        }

        public RoleResponse SaveRole()
        {
            _role.Validate();

            if (_role.GetRules().Count == 0)
            {

                RoleRepository oRepository = new RoleRepository();
                if (_role.ROLE_ID == 0)
                {
                    return oRepository.AddRole(_role);
                }
                else
                {
                    return oRepository.UpdateRole(_role);
                }
            }
            return null;
        }


        public RoleResponse DeleteRole()
        {
            RoleRepository oRepository = new RoleRepository();
            return oRepository.DeleteRole(_role);
        }

        public RoleResponse GetRoleList()
        {
            RoleRepository oRepository = new RoleRepository();
            return oRepository.GetRoleList(_role);
        }

        public RoleResponse GetRoleListWithAdmin()
        {
            RoleRepository oRepository = new RoleRepository();
            return oRepository.GetRoleListWithAdmin(_role);
        }
    }
}