using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class RoleResponse : BaseResponse
    {
        private Role _role;
        private List<Role> lstRole;
        public Role Role
        {
            set
            {
                _role = value;
            }
            get
            {
                return _role;
            }
        }
        public List<Role> RoleList
        {
            set
            {
                lstRole = value;
            }
            get
            {
                return lstRole;
            }
        }
        public void AddObject(Role o)
        {
            if (lstRole == null)
            {
                lstRole = new List<Role>();
            }
            lstRole.Add(o);
        }
    }
}