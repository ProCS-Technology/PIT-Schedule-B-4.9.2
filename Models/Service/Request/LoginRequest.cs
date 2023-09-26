using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class LoginRequest
    {
        private LoginModel _Login;
        public LoginRequest(LoginModel lgn)
        {
            _Login = new LoginModel();
            _Login = lgn;
        }
        public LoginResponse Validate_UserLogin()
        {
            LoginRepository oRepository = new LoginRepository();
            return oRepository.Validate_UserLogin(_Login);
        }
        public LoginResponse User_Details()
        {
            LoginRepository oRepository = new LoginRepository();
            return oRepository.User_Details(_Login);
        }
        public LoginRequest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}