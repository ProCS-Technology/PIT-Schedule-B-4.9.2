using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UserGroupRequest
    {
        private UserGroup _UserGroup;


        public UserGroupRequest()
        {

        }

        public UserGroupRequest(UserGroup usergroup)
        {
            _UserGroup = new UserGroup();
            _UserGroup = usergroup;

        }

        public UserGroupResponse SaveUserGroup()
        {
            UserGroupRepository oRepository = new UserGroupRepository();
            return oRepository.SaveUserGroup(_UserGroup);
        }

        public UserGroupResponse SendUserGroupEmail()
        {
            UserGroupRepository oRepository = new UserGroupRepository();
            return oRepository.SendUserGroupEmail(_UserGroup);
        }

        public UserGroupResponse UserGroupList()
        {

            UserGroupRepository URepository = new UserGroupRepository();

            return URepository.UserGroupList(_UserGroup);


        }
        public UserGroupResponse UserGroupSentMailList()
        {

            UserGroupRepository URepository = new UserGroupRepository();

            return URepository.UserGroupSentMailList(_UserGroup);


        }

    }
}