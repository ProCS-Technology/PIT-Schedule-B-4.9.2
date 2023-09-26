using ProcsDLL.Models.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.Service.Response
{
    public class GroupResponse : BaseResponse
    {
        private Group _group;
        private List<Group> lstGroup;
        public Group Group
        {
            set
            {
                _group = value;
            }
            get
            {
                return _group;
            }
        }
        public List<Group> GroupList
        {
            set
            {
                lstGroup = value;
            }
            get
            {
                return lstGroup;
            }
        }
        public void AddObject(Group o)
        {
            if (lstGroup == null)
            {
                lstGroup = new List<Group>();
            }
            lstGroup.Add(o);
        }
    }
}