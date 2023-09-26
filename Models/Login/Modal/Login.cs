using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.Login.Modal
{
    public class Login : BaseEntity
    {
        public string LoginId { get; set; }
        public string Email { set; get; }
        public string Password { set; get; }
        public String newPassword { get; set; }
        public Int32 Id { set; get; }
        public string UserName { set; get; }
        public string Mobile { set; get; }
        public string CommitteeId { get; set; }
        public string CompanyId { get; set; }
        public string Salt { get; set; }
        public string MoreSalt { get; set; }
        public List<UserAccess> UAccess { set; get; }
        public override void Validate()
        {
            base.Validate();
        }
    }
}