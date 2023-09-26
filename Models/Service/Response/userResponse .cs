using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class userResponse : BaseResponse
    {
        private User _User;

        private List<User> lstUser;


        public User User
        {
            set
            {
                _User = value;
            }
            get
            {
                return _User;
            }
        }
        public List<User> UserList
        {
            set
            {
                lstUser = value;
            }
            get
            {
                return lstUser;
            }
        }

        public void AddObject(User o)
        {
            if (lstUser == null)
            {
                lstUser = new List<User>();
            }
            lstUser.Add(o);
        }
    }
}