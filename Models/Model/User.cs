using System;

namespace ProcsDLL.Models.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            User_Role = new UsersRole();
        }
        public Int32 ID { get; set; }
        public String USER_NM { get; set; }
        public String USER_EMAIL { get; set; }
        public String USER_PWD { get; set; }
        public String USER_MOBILE { get; set; }
        public UsersRole User_Role { get; set; }



        public override void Validate()
        {
            base.Validate();
        }
    }
}