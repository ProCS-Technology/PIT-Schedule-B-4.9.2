using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.Menu.Modal
{
    public class Menu : BaseEntity
    {
        public string Email { set; get; }
        public Int32 CompanyId { set; get; }
        public string DataBase { set; get; }
        public List<MenuItems> MItems { set; get; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}