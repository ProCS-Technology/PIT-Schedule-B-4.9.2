using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class userRequest
    {
        private User _User;

        public userRequest()
        {

        }

        public userRequest(User user)
        {
            _User = new User();
            _User = user;

        }

        public userResponse SaveUser()
        {
            //_group.Validate();
            //if (_group.GetRules().Count == 0)
            //{
            userRepository oRepository = new userRepository();
            if (_User.ID == 0)
            {
                return oRepository.AddUser(_User);
            }
            else
            {
                return oRepository.UpdateUser(_User);
            }
            //}
            // return null;
        }

        public userResponse DeleteUser()
        {

            userRepository oRepository = new userRepository();
            return oRepository.DeleteUser(_User);
        }

        public userResponse GetUserList()
        {
            userRepository oRepository = new userRepository();
            return oRepository.GetUserList();
        }

        public userRoleResponse GetAllUsersRole()
        {
            userRepository oRepository = new userRepository();
            return oRepository.GetAllUsersRole();
        }
    }
}