using System;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class Email : BaseEntity
    {
        public Int32 id { get; set; }
        public String mailFromUserName { get; set; }
        public String mailFrom { get; set; }
        public String mailToUserName { get; set; }
        public String mailTo { get; set; }
        public String subject { get; set; }
        public String body { get; set; }
        public List<String> listAttachment { get; set; }

        public string TaskFor { get; set; }
        
        public string EmailDate { get; set; }
        public string UserEmail { get; set; }
        public string CreatedOn { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}