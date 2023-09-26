using ProcsDLL.Models.Login.Repository;
using ProcsDLL.Models.Login.Service.Response;
namespace ProcsDLL.Models.Login.Service.Request
{
    public class LoginRequest
    {
        private ProcsDLL.Models.Login.Modal.Login _objLogin;
        public LoginRequest(ProcsDLL.Models.Login.Modal.Login objLogin)
        {
            _objLogin = objLogin;
        }
        public LoginResponse ValidateUser()
        {
            LoginRepository rep = new LoginRepository();
            return rep.ValidateUser(_objLogin);
        }
                public bool IsPasswordChanged()
        {
            LoginRepository rep = new LoginRepository();
            return rep.IsPasswordChanged(_objLogin);
        }
                public void UpdateChangePasswordFlag()
        {
            LoginRepository rep = new LoginRepository();
            rep.UpdateChangePasswordFlag(_objLogin);
        }
        public LoginResponse GetUserInfo()
        {
            LoginRepository rep = new LoginRepository();
            return rep.GetUserInfo(_objLogin);
        }

        public LoginResponse GetUserEmailByUserId()
        {
            LoginRepository rep = new LoginRepository();
            return rep.GetUserEmailByUserId(_objLogin);
        }

        public LoginResponse ChangePassword()
        {
            LoginRepository rep = new LoginRepository();
            return rep.ChangePassword(_objLogin);
        }
        public LoginResponse ValidateADUser()
        {
            LoginRepository rep = new LoginRepository();
            return rep.ValidateADUser(_objLogin);
        }
        public LoginResponse ValidateSwitchedUser()
        {
            LoginRepository rep = new LoginRepository();
            return rep.ValidateSwitchedUser(_objLogin);
        }
    }
}