using System;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Web.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Configuration;
using Newtonsoft.Json.Linq;
//using System.Web.Mvc;
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class EmailConfigController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetEmailConfig")]
        [HttpPost]
        public EmailConfigResponse GetEmailConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    EmailConfigResponse objResponse = new EmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                UPSIEmailConfig upsiEmailConfig = new UPSIEmailConfig();
                upsiEmailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiEmailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiEmailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiEmailConfig.ValidateInput())
                {
                    EmailConfigRequest upsiReq = new EmailConfigRequest(upsiEmailConfig);
                    EmailConfigResponse upsiRes = upsiReq.GetEmailConfig();
                    return upsiRes;
                }
                else
                {
                    EmailConfigResponse upsiRes = new EmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                EmailConfigResponse upsiRes = new EmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddSmartEmailConfig")]
        [HttpPost]
        public EmailConfigResponse AddSmartEmailConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    EmailConfigResponse objResponse = new EmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                UPSIEmailConfig upsiEmailConfig = new UPSIEmailConfig();
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string ConfigId = HttpContext.Current.Request.Form["ConfigId"].ToString();
                string AuthenticationType = HttpContext.Current.Request.Form["AuthenticationType"].ToString();
                string SmartType = HttpContext.Current.Request.Form["SmartType"].ToString();
                string ClientId = HttpContext.Current.Request.Form["ClientId"].ToString();
                string ClientCertificate = HttpContext.Current.Request.Form["ClientCertificate"].ToString();
                string TenantId = HttpContext.Current.Request.Form["TenantId"].ToString();
                string GoogleServiceAccount = HttpContext.Current.Request.Form["GoogleServiceAccount"].ToString();
                string SmartEmail = HttpContext.Current.Request.Form["SmartEmail"].ToString();
                string ProtocolType = HttpContext.Current.Request.Form["ProtocolType"].ToString();
                string OutgoingProtocol = HttpContext.Current.Request.Form["OutgoingProtocol"].ToString();
                string OutgoingPort = HttpContext.Current.Request.Form["OutgoingPort"].ToString();
                string IncomingProtocol = HttpContext.Current.Request.Form["IncomingProtocol"].ToString();
                string IncomingPort = HttpContext.Current.Request.Form["IncomingPort"].ToString();
                string AccessToken = HttpContext.Current.Request.Form["AccessToken"].ToString();
                string RefreshToken = HttpContext.Current.Request.Form["RefreshToken"].ToString();
                string TokenIssuedAt = HttpContext.Current.Request.Form["TokenIssuedAt"].ToString();
                string TokenExpiresIn = HttpContext.Current.Request.Form["TokenExpiresIn"].ToString();
                string IsSSL = HttpContext.Current.Request.Form["IsSSL"].ToString();
                string AuthenticationEmail = HttpContext.Current.Request.Form["AuthenticationEmail"].ToString();
                string DisplayNm = HttpContext.Current.Request.Form["DisplayNm"].ToString();

                upsiEmailConfig.ConfigId = Convert.ToInt32(ConfigId);
                upsiEmailConfig.AuthenticationType = AuthenticationType;
                upsiEmailConfig.ClientCertificate = ClientCertificate;
                upsiEmailConfig.ClientId = ClientId;
                upsiEmailConfig.GoogleServiceAccount = GoogleServiceAccount;
                upsiEmailConfig.OutgoingProtocol = OutgoingProtocol;
                upsiEmailConfig.OutgoingPort = OutgoingPort;
                upsiEmailConfig.IncomingProtocol = IncomingProtocol;
                upsiEmailConfig.IncomingPort = IncomingPort;
                upsiEmailConfig.SmartType = SmartType;
                upsiEmailConfig.UPSIEmail = SmartEmail;
                upsiEmailConfig.TenantId = TenantId;
                upsiEmailConfig.ProtocolType = ProtocolType;
                upsiEmailConfig.AccessToken = AccessToken;
                upsiEmailConfig.RefreshToken = RefreshToken;
                upsiEmailConfig.IsSSL = IsSSL;
                upsiEmailConfig.AuthenticationEmail = AuthenticationEmail;
                upsiEmailConfig.DisplayNm = DisplayNm;
                if (SmartType == "Office 365")
                {
                    if (TokenIssuedAt != "" && TokenExpiresIn != "")
                    {
                        upsiEmailConfig.ExpiryAt = Convert.ToDateTime(TokenIssuedAt).AddSeconds(Convert.ToDouble(TokenExpiresIn)).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    //if (ConfigId != "0")
                    //{
                    //    upsiEmailConfig.ExpiryAt = Convert.ToDateTime(TokenIssuedAt).AddSeconds(Convert.ToDouble(TokenExpiresIn)).ToString("yyyy-MM-dd HH:mm:ss");
                    //}
                }
                //upsiEmailConfig.ExpiriesIn = TokenExpiresIn;

                var uploadPdf = "/insidertrading/EmailCertificate/";
                string ext = "";
                string name = "";
                string fileName = "";
                string sSaveAs = "";
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        ext = Path.GetExtension(file.FileName);
                        name = Path.GetFileName(file.FileName);
                        string withoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                        fileName = withoutExt + ext;
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadPdf), fileName);
                        file.SaveAs(sSaveAs);
                    }
                }
                upsiEmailConfig.GoogleCertificate = fileName;
                upsiEmailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiEmailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiEmailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiEmailConfig.ValidateInput())
                {
                    EmailConfigRequest upsiReq = new EmailConfigRequest(upsiEmailConfig);
                    EmailConfigResponse upsiRes = upsiReq.AddSmartEmailConfig();
                    return upsiRes;
                }
                else
                {
                    EmailConfigResponse upsiRes = new EmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                EmailConfigResponse upsiRes = new EmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddSmartEmailConfigBackend")]
        [HttpPost]
        public EmailConfigResponse AddSmartEmailConfigBackend()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    EmailConfigResponse objResponse = new EmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                UPSIEmailConfig upsiEmailConfig = new UPSIEmailConfig();
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string ConfigId = HttpContext.Current.Request.Form["ConfigId"].ToString();
                string AuthenticationType = HttpContext.Current.Request.Form["AuthenticationType"].ToString();
                string SmartType = HttpContext.Current.Request.Form["SmartType"].ToString();
                string ClientId = HttpContext.Current.Request.Form["ClientId"].ToString();
                string ClientCertificate = HttpContext.Current.Request.Form["ClientCertificate"].ToString();
                string TenantId = HttpContext.Current.Request.Form["TenantId"].ToString();
                string GoogleServiceAccount = HttpContext.Current.Request.Form["GoogleServiceAccount"].ToString();
                string SmartEmail = HttpContext.Current.Request.Form["SmartEmail"].ToString();
                string ProtocolType = HttpContext.Current.Request.Form["ProtocolType"].ToString();
                string OutgoingProtocol = HttpContext.Current.Request.Form["OutgoingProtocol"].ToString();
                string OutgoingPort = HttpContext.Current.Request.Form["OutgoingPort"].ToString();
                string IncomingProtocol = HttpContext.Current.Request.Form["IncomingProtocol"].ToString();
                string IncomingPort = HttpContext.Current.Request.Form["IncomingPort"].ToString();
                string AccessToken = HttpContext.Current.Request.Form["AccessToken"].ToString();
                string RefreshToken = HttpContext.Current.Request.Form["RefreshToken"].ToString();
                string TokenIssuedAt = HttpContext.Current.Request.Form["TokenIssuedAt"].ToString();
                string TokenExpiresIn = HttpContext.Current.Request.Form["TokenExpiresIn"].ToString();
                string IsSSL = HttpContext.Current.Request.Form["IsSSL"].ToString();
                string AuthenticationEmail = HttpContext.Current.Request.Form["AuthenticationEmail"].ToString();
                string DisplayNm = HttpContext.Current.Request.Form["DisplayNm"].ToString();
                string sRedirectUrl = Convert.ToString(ConfigurationManager.AppSettings["Office365ReturnUrl"]);

                string requestData = string.Format(
                    "grant_type=authorization_code&code={0}&code_verifier={1}&client_id={2}&redirect_uri={3}&client_secret={4}",
                    AccessToken, RefreshToken, ClientId, sRedirectUrl, ClientCertificate
                );
                string tokenUri = string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/token", TenantId);

                string sFile = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/EmailLog.txt");
                using (StreamWriter writer = new StreamWriter(sFile, true))
                {
                    writer.WriteLine("In EmailConfigController");
                    writer.WriteLine(tokenUri);
                    writer.WriteLine(requestData);
                    writer.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                }

                string responseText = EmailSender.GetOfficeOAuthToken(tokenUri, requestData);
                dynamic objInput = Newtonsoft.Json.JsonConvert.DeserializeObject(responseText);

                var x = (JObject)objInput;
                foreach (JToken tok in x.Children())
                {
                    if (tok is JProperty)
                    {
                        var prop = tok as JProperty;
                        var pName = prop.Name;

                        if (pName == "access_token")
                        {
                            AccessToken = prop.Value.ToString();
                        }
                        if (pName == "refresh_token")
                        {
                            RefreshToken = prop.Value.ToString();
                        }
                        if (pName == "expires_in")
                        {
                            TokenExpiresIn = Convert.ToString(prop.Value.ToString());
                        }
                    }
                }

                upsiEmailConfig.ConfigId = Convert.ToInt32(ConfigId);
                upsiEmailConfig.AuthenticationType = AuthenticationType;
                upsiEmailConfig.ClientCertificate = ClientCertificate;
                upsiEmailConfig.ClientId = ClientId;
                upsiEmailConfig.GoogleServiceAccount = GoogleServiceAccount;
                upsiEmailConfig.OutgoingProtocol = OutgoingProtocol;
                upsiEmailConfig.OutgoingPort = OutgoingPort;
                upsiEmailConfig.IncomingProtocol = IncomingProtocol;
                upsiEmailConfig.IncomingPort = IncomingPort;
                upsiEmailConfig.SmartType = SmartType;
                upsiEmailConfig.UPSIEmail = SmartEmail;
                upsiEmailConfig.TenantId = TenantId;
                upsiEmailConfig.ProtocolType = ProtocolType;
                upsiEmailConfig.AccessToken = AccessToken;
                upsiEmailConfig.RefreshToken = RefreshToken;
                upsiEmailConfig.IsSSL = IsSSL;
                upsiEmailConfig.AuthenticationEmail = AuthenticationEmail;
                upsiEmailConfig.DisplayNm = DisplayNm;
                upsiEmailConfig.ExpiryAt = DateTime.Now.AddSeconds(Convert.ToDouble(TokenExpiresIn)).ToString("yyyy-MM-dd HH:mm:ss");
                

                var uploadPdf = "/insidertrading/EmailCertificate/";
                string ext = "";
                string name = "";
                string fileName = "";
                string sSaveAs = "";
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        ext = Path.GetExtension(file.FileName);
                        name = Path.GetFileName(file.FileName);
                        string withoutExt = Path.GetFileNameWithoutExtension(file.FileName);
                        fileName = withoutExt + ext;
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadPdf), fileName);
                        file.SaveAs(sSaveAs);
                    }
                }
                upsiEmailConfig.GoogleCertificate = fileName;
                upsiEmailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiEmailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiEmailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiEmailConfig.ValidateInput())
                {
                    EmailConfigRequest upsiReq = new EmailConfigRequest(upsiEmailConfig);
                    EmailConfigResponse upsiRes = upsiReq.AddSmartEmailConfig();
                    return upsiRes;
                }
                else
                {
                    EmailConfigResponse upsiRes = new EmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                EmailConfigResponse upsiRes = new EmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddBasicEmailConfig")]
        [HttpPost]
        public EmailConfigResponse AddBasicEmailConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    EmailConfigResponse objResponse = new EmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIEmailConfig emailConfig = new JavaScriptSerializer().Deserialize<ProcsDLL.Models.InsiderTrading.Model.UPSIEmailConfig>(input);
                emailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                emailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                emailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (emailConfig.ValidateInput())
                {
                    EmailConfigRequest upsiReq = new EmailConfigRequest(emailConfig);
                    EmailConfigResponse upsiRes = upsiReq.AddBasicEmailConfig();
                    return upsiRes;
                }
                else
                {
                    EmailConfigResponse upsiRes = new EmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                EmailConfigResponse upsiRes = new EmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }        
    }
}