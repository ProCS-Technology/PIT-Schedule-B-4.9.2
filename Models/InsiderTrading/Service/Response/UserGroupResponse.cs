using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class UserGroupResponse : BaseResponse
    {
        private UserGroup _UserGrp;

        private List<UserGroup> lstUserGroup;

        private List<UserGroup> lstUserGroupSentMail;
        public UserGroup UserGrp
        {
            set
            {
                _UserGrp = value;
            }
            get
            {
                return _UserGrp;
            }
        }
        public List<UserGroup> UserGroupList
        {
            set
            {
                lstUserGroup = value;
            }
            get
            {
                return lstUserGroup;
            }
        }

        public List<UserGroup> UserGroupSentMailList
        {
            set
            {
                lstUserGroupSentMail = value;
            }
            get
            {
                return lstUserGroupSentMail;
            }
        }

        public void AddObject(UserGroup o)
        {
            if (lstUserGroup == null)
            {
                lstUserGroup = new List<UserGroup>();
            }
            lstUserGroup.Add(o);
        }

        public void AddObjectUserGroupSentMail(UserGroup o)
        {
            if (lstUserGroupSentMail == null)
            {
                lstUserGroupSentMail = new List<UserGroup>();
            }
            lstUserGroupSentMail.Add(o);
        }
    }
}