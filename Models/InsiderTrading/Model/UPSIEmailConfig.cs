using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class UPSIEmailConfig : BaseEntity
    {
        public Int32 ConfigId { set; get; }
        public Int32 UpsiTypId { set; get; }
        public string UpsiTypNm { set; get; }
        public string UPSIEmail { set; get; }
        public string AuthenticationEmail { set; get; }
        public string DisplayNm { set; get; }
        public string OutgoingProtocol { set; get; }
        public string OutgoingPort { set; get; }
        public string ProtocolType { set; get; }
        public string IncomingProtocol { set; get; }
        public string IncomingPort { set; get; }
        public string IsSSL { set; get; }
        public string Password { set; get; }

        public string AuthenticationType { set; get; }
        public string SmartType { set; get; }
        public string ClientId { set; get; }
        public string ClientCertificate { set; get; }
        public string TenantId { set; get; }
        public string AccessToken { set; get; }
        public string RefreshToken { set; get; }
        public string ExpiryAt { set; get; }

        public string GoogleServiceAccount { set; get; }
        public string GoogleCertificate { set; get; }

        public Int32 CompanyId { set; get; }
        public string UserLogin { set; get; }
    }
}