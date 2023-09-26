using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class userRoleRequest
    {
        private UsersRole _UserRole;

        public userRoleRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public userRoleRequest(UsersRole userRole)
        {
            _UserRole = new UsersRole();
            _UserRole = userRole;

        }

        public userRoleResponse GetAllUsersRole()
        {
            userRepository oRepository = new userRepository();
            return oRepository.GetAllUsersRole();
        }
    }
}