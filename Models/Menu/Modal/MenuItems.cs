using System;

namespace ProcsDLL.Models.Menu.Modal
{
    public class MenuItems : BaseEntity
    {
        public Int32 ID { set; get; }
        public Int32 MenuId { set; get; }
        public string MenuItem { set; get; }
        public string SubMenu { set; get; }
        public Int32 ParentMenuItem { set; get; }
        public string URL { set; get; }
    }
}