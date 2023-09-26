using System;

namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class SmtpConfiguration : BaseEntity
    {
        public Int32 SMPT_CONFIG_ID { get; set; }
        public String DEFAULT_EMAIL { get; set; }
        public String CONTINUAL_DISCLOSURE_EMAIL { get; set; }
        public String SMTP_HOST_NAME { get; set; }
        public String PORT { get; set; }
        public String SSL { get; set; }
        public String USER_DEFAULT_CREDENTIAL { get; set; }
        public String SMTP_USER_NAME { get; set; }
        public String PASSWORD { get; set; }
        public Int32 COMPANY_ID { get; set; }
        public String CREATE_BY { get; set; }
        public String CREATED_ON { get; set; }

        public String IS_POP { get; set; }
        public String DEFAULT_EMAIL_OUTGOING { get; set; }
        public String CONTINUAL_DISCLOSURE_EMAIL_OUTGOING { get; set; }
        public String SMTP_HOST_NAME_OUTGOING { get; set; }
        public String PORT_OUTGOING { get; set; }
        public String SSL_OUTGOING { get; set; }
        public String USER_DEFAULT_CREDENTIAL_OUTGOING { get; set; }
        public String SMTP_USER_NAME_OUTGOING { get; set; }
        public String PASSWORD_OUTGOING { get; set; }
        public bool isSMTPConfigExists { get; set; }
        public bool isSSLExists { get; set; }
        public bool isUserDefaultCredentialExists { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}