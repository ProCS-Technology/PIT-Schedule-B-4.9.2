using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class LoginResponse : BaseResponse
    {
        private LoginModel _Login;
        private List<LoginModel> lstLogin;
        public LoginModel Login
        {
            set
            {
                _Login = value;
            }
            get
            {
                return _Login;
            }
        }
        public List<LoginModel> LoginList
        {
            set
            {
                lstLogin = value;
            }
            get
            {
                return lstLogin;
            }
        }
        public void AddObject(LoginModel o)
        {
            if (lstLogin == null)
            {
                lstLogin = new List<LoginModel>();
            }
            lstLogin.Add(o);
        }
        public LoginResponse()
        {
            _Login = new LoginModel();
            //
            // TODO: Add constructor logic here
            //
        }
    }
}