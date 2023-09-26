using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.Login.Modal
{
    public class UserAccess : BaseEntity
    {
        public Int32 GroupId { set; get; }
        public string GroupNm { set; get; }
        public string GroupLogo { set; get; }
        public Int32 CompanyId { set; get; }
        public string CompanyNm { set; get; }
        public string CompanyLogo { set; get; }
        public Int32 ModuleId { set; get; }
        public string ModuleNm { set; get; }
        public string ModuleLogo { set; get; }
        public string ModuleFolder { set; get; }
        public string ModuleDataBase { set; get; }
        public string EmployeeId { set; get; }
    }
}