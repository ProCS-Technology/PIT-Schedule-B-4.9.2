using System;

namespace ProcsDLL.Models.Model
{
    public class LoginModel : BaseEntity
    {
        public Int32 ID { get; set; }
        public string USER_NM { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_PWD { get; set; }
        public string USER_MOBILE { get; set; }
        public LoginModel()
        {
            //  new LoginModel();

            //
            // TODO: Add constructor logic here
            //
        }
        public override void Validate()
        {
            base.Validate();
        }
    }
}