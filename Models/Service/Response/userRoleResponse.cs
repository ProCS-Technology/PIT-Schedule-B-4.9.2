using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class userRoleResponse : BaseResponse
    {
        private UsersRole _UserRole;

        private List<UsersRole> lstUserRole;


        public UsersRole UserRole
        {
            set
            {
                _UserRole = value;
            }
            get
            {
                return _UserRole;
            }
        }
        public List<UsersRole> UserRoleList
        {
            set
            {
                lstUserRole = value;
            }
            get
            {
                return lstUserRole;
            }
        }

        public void AddObject(UsersRole o)
        {
            if (lstUserRole == null)
            {
                lstUserRole = new List<UsersRole>();
            }
            lstUserRole.Add(o);
        }
    }
}