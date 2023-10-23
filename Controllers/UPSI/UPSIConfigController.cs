using System;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System.Web.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
namespace ProcsDLL.Controllers.UPSIConfig
{
    public class UPSIConfigController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetUPSIConfig")]
        [HttpGet]
        public UPSIConfigResponse GetUPSIConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIConfigResponse objResponse = new UPSIConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                ProcsDLL.Models.InsiderTrading.Model.UPSIConfig upsiConfig = new ProcsDLL.Models.InsiderTrading.Model.UPSIConfig();
                upsiConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiConfig.ValidateInput())
                {
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiConfig);
                    UPSIConfigResponse upsiRes = upsiReq.GetUPSIConfig();
                    return upsiRes;
                }
                else
                {
                    UPSIConfigResponse upsiRes = new UPSIConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }

            }
            catch (Exception ex)
            {
                UPSIConfigResponse upsiRes = new UPSIConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("GetUPSIEmailConfig")]
        [HttpGet]
        public UPSIEmailConfigResponse GetUPSIEmailConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIEmailConfigResponse objResponse = new UPSIEmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSIEmailConfig upsiConfig = new UPSIEmailConfig();
                upsiConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiConfig.ValidateInput())
                {
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiConfig);
                    UPSIEmailConfigResponse upsiRes = upsiReq.GetUPSIEmailConfig();
                    return upsiRes;
                }
                else
                {
                    UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }

            }
            catch (Exception ex)
            {
                UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddUPSIConfig")]
        [HttpPost]
        public UPSIConfigResponse AddUPSIConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIConfigResponse objResponse = new UPSIConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ProcsDLL.Models.InsiderTrading.Model.UPSIConfig upsiConfig = new JavaScriptSerializer().Deserialize<ProcsDLL.Models.InsiderTrading.Model.UPSIConfig>(input);
                upsiConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiConfig.ValidateInput())
                {
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiConfig);
                    UPSIConfigResponse upsiRes = upsiReq.AddUPSIConfig();
                    return upsiRes;
                }
                else
                {
                    UPSIConfigResponse upsiRes = new UPSIConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }

            }
            catch (Exception ex)
            {
                UPSIConfigResponse upsiRes = new UPSIConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddSmartUPSIEmailConfig")]
        [HttpPost]
        public UPSIEmailConfigResponse AddSmartUPSIEmailConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIEmailConfigResponse objResponse = new UPSIEmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                UPSIEmailConfig upsiEmailConfig = new UPSIEmailConfig();
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string ConfigId = HttpContext.Current.Request.Form["ConfigId"].ToString();
                string UpsiTypId = HttpContext.Current.Request.Form["UpsiTypId"].ToString();
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

                upsiEmailConfig.ConfigId = Convert.ToInt32(ConfigId);
                upsiEmailConfig.UpsiTypId = Convert.ToInt32(UpsiTypId);
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
                if (SmartType == "Office 365")
                {
                    if(TokenIssuedAt!="" && TokenExpiresIn != "")
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
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiEmailConfig);
                    UPSIEmailConfigResponse upsiRes = upsiReq.AddSmartUPSIEmailConfig();
                    return upsiRes;
                }
                else
                {
                    UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddBasicUPSIEmailConfig")]
        [HttpPost]
        public UPSIEmailConfigResponse AddBasicUPSIEmailConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIEmailConfigResponse objResponse = new UPSIEmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIEmailConfig upsiEmailConfig = new JavaScriptSerializer().Deserialize<ProcsDLL.Models.InsiderTrading.Model.UPSIEmailConfig>(input);
                upsiEmailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiEmailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiEmailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiEmailConfig.ValidateInput())
                {
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiEmailConfig);
                    UPSIEmailConfigResponse upsiRes = upsiReq.AddBasicUPSIEmailConfig();
                    return upsiRes;
                }
                else
                {
                    UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddUPSIType")]
        [HttpPost]
        public UPSITypeResponse AddUPSIType()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIType upsiTyp = new JavaScriptSerializer().Deserialize<UPSIType>(input);
                upsiTyp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiTyp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiTyp.CreatedBy= Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiTyp.ValidateInput())
                {
                    UPSITypeRequest upsiReq = new UPSITypeRequest(upsiTyp);
                    UPSITypeResponse upsiRes = upsiReq.AddUpdateUPSIType();
                    return upsiRes;
                }
                else
                {
                    UPSITypeResponse upsiRes = new UPSITypeResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                UPSITypeResponse upsiRes = new UPSITypeResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("GetUPSITypeList")]
        [HttpGet]
        public UPSITypeResponse GetUPSITypeList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                ProcsDLL.Models.InsiderTrading.Model.UPSIType upsiConfig = new ProcsDLL.Models.InsiderTrading.Model.UPSIType();
                upsiConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiConfig.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiConfig.ValidateInput())
                {
                    UPSITypeRequest upsiReq = new UPSITypeRequest(upsiConfig);
                    UPSITypeResponse upsiRes = upsiReq.GetUPSITypeList();
                    return upsiRes;
                }
                else
                {
                    UPSITypeResponse upsiRes = new UPSITypeResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }

            }
            catch (Exception ex)
            {
                UPSITypeResponse upsiRes = new UPSITypeResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("GetUPSIEmailConfigById")]
        [HttpPost]
        public UPSIEmailConfigResponse GetUPSIEmailConfigById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIEmailConfigResponse objResponse = new UPSIEmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIEmailConfig upsiEmailConfig = new JavaScriptSerializer().Deserialize<ProcsDLL.Models.InsiderTrading.Model.UPSIEmailConfig>(input);
                upsiEmailConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiEmailConfig.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiEmailConfig.UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiEmailConfig.ValidateInput())
                {
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiEmailConfig);
                    UPSIEmailConfigResponse upsiRes = upsiReq.GetUPSIEmailConfigById();
                    return upsiRes;
                }
                else
                {
                    UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("GetUPSITypeKeywords")]
        [HttpPost]
        public UPSITypeResponse GetUPSITypeKeywords()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIType upsiTyp = new JavaScriptSerializer().Deserialize<UPSIType>(input);
                upsiTyp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiTyp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiTyp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsiTyp.ValidateInput())
                {
                    UPSITypeRequest upsiReq = new UPSITypeRequest(upsiTyp);
                    UPSITypeResponse upsiRes = upsiReq.GetUPSITypeById();
                    return upsiRes;
                }
                else
                {
                    UPSITypeResponse upsiRes = new UPSITypeResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }

            }
            catch (Exception ex)
            {
                UPSITypeResponse upsiRes = new UPSITypeResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("AddSmartEmailConfigBackend")]
        [HttpPost]
        public UPSIEmailConfigResponse AddSmartEmailConfigBackend()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIEmailConfigResponse objResponse = new UPSIEmailConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                UPSIEmailConfig upsiEmailConfig = new UPSIEmailConfig();
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string ConfigId = HttpContext.Current.Request.Form["ConfigId"].ToString();
                string UpsiTypId = HttpContext.Current.Request.Form["UpsiTypId"].ToString();
                string AuthenticationType = HttpContext.Current.Request.Form["AuthenticationType"].ToString();
                string SmartType = HttpContext.Current.Request.Form["SmartType"].ToString();
                string ClientId = HttpContext.Current.Request.Form["ClientId"].ToString();
                string ClientCertificate = HttpContext.Current.Request.Form["ClientCertificate"].ToString();
                string TenantId = HttpContext.Current.Request.Form["TenantId"].ToString();
                string AuthenticationEmail = HttpContext.Current.Request.Form["AuthenticationEmail"].ToString();
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
                string sRedirectUrl = Convert.ToString(ConfigurationManager.AppSettings["Office365ReturnUrl"]);

                string requestData = string.Format(
                    "grant_type=authorization_code&code={0}&code_verifier={1}&client_id={2}&redirect_uri={3}&client_secret={4}",
                    AccessToken, RefreshToken, ClientId, sRedirectUrl, ClientCertificate
                );
                string tokenUri = string.Format("https://login.microsoftonline.com/{0}/oauth2/v2.0/token", TenantId);
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
                upsiEmailConfig.UpsiTypId = Convert.ToInt32(UpsiTypId);
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
                upsiEmailConfig.ExpiryAt = DateTime.Now.AddSeconds(Convert.ToDouble(TokenExpiresIn)).ToString("yyyy-MM-dd HH:mm:ss");
                upsiEmailConfig.AuthenticationEmail = AuthenticationEmail;


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
                    UPSIConfigRequest upsiReq = new UPSIConfigRequest(upsiEmailConfig);
                    UPSIEmailConfigResponse upsiRes = upsiReq.AddSmartUPSIEmailConfig();
                    return upsiRes;
                }
                else
                {
                    UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                UPSIEmailConfigResponse upsiRes = new UPSIEmailConfigResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
        [Route("GetAllUsers")]
        [HttpPost]
        public GenericResponse GetAllUsers()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    GenericResponse objResponse = new GenericResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                User objUser = new User();
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                objUser.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                objUser.businessUnit = new BusinessUnit
                {
                    businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]),
                    businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"])
                };

                if (objUser.ValidateInput())
                {
                    UserRequest gReqUserList = new UserRequest(objUser);
                    GenericResponse gResUserList = gReqUserList.GetAllUsers();
                    return gResUserList;
                }
                else
                {
                    GenericResponse upsiRes = new GenericResponse();
                    upsiRes.StatusFl = false;
                    upsiRes.Msg = sXSSErrMsg;
                    return upsiRes;
                }
            }
            catch (Exception ex)
            {
                GenericResponse upsiRes = new GenericResponse();
                upsiRes.StatusFl = false;
                upsiRes.Msg = ex.Message;
                return upsiRes;
            }
        }
    }
}