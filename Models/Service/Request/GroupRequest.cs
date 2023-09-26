using ProcsDLL.Models.Model;
using ProcsDLL.Models.Repository;
using ProcsDLL.Models.Service.Response;

namespace ProcsDLL.Models.Service.Request
{
    public class GroupRequest
    {
        private Group _group;
        //public GroupRequest(CompanyDTO companyDTO)
        //{
        //    _group = new Group();
        //}

        public GroupRequest()
        {

        }

        public GroupRequest(Group grp)
        {
            _group = new Group();
            _group = grp;

        }

        public GroupResponse SaveGroup()
        {
            //_group.Validate();
            //if (_group.GetRules().Count == 0)
            //{
            GroupRepository oRepository = new GroupRepository();
            if (_group.GROUP_ID == 0)
            {
                return oRepository.AddGroup(_group);
            }
            else
            {
                return oRepository.UpdateGroup(_group);
            }
            //}
            // return null;
        }

        public GroupResponse DeleteGroup()
        {

            GroupRepository oRepository = new GroupRepository();
            return oRepository.DeleteGroup(_group);
        }

        public GroupResponse GetGroupList()
        {
            GroupRepository oRepository = new GroupRepository();
            return oRepository.GetGroupList();
        }
    }
}