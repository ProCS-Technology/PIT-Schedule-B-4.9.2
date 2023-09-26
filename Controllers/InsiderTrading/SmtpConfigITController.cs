using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/SmtpConfigIT")]
    public class SmtpConfigITController : ApiController
    {
        [Route("GetSmtpConfigList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Smtp Config APIs" })]
        public SmtpConfigResponse GetSmtpConfigList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SmtpConfigResponse objResponse = new SmtpConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SmtpConfiguration smtpConfig = new JavaScriptSerializer().Deserialize<SmtpConfiguration>(input);
                smtpConfig.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                smtpConfig.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                smtpConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!smtpConfig.ValidateInput())
                {
                    SmtpConfigResponse objResponse = new SmtpConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SmtpConfigRequest gReqSmtpConfigList = new SmtpConfigRequest(smtpConfig);
                SmtpConfigResponse gResSmtpConfigList = gReqSmtpConfigList.GetSmtpConfigList();
                if (gResSmtpConfigList.SmtpConfigList != null && gResSmtpConfigList.SmtpConfigList.Count > 0)
                {
                    gResSmtpConfigList.SmtpConfigList[0].PASSWORD = CryptorEngine.Decrypt(gResSmtpConfigList.SmtpConfigList[0].PASSWORD, true);
                    gResSmtpConfigList.SmtpConfigList[0].PASSWORD_OUTGOING = CryptorEngine.Decrypt(gResSmtpConfigList.SmtpConfigList[0].PASSWORD_OUTGOING, true);
                }
                return gResSmtpConfigList;
            }
            catch (Exception ex)
            {
                SmtpConfigResponse objResponse = new SmtpConfigResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("SaveSmtpConfig")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Smtp Config APIs" })]
        public SmtpConfigResponse SaveSmtpConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SmtpConfigResponse objResponse = new SmtpConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SmtpConfiguration smtpConfig = new JavaScriptSerializer().Deserialize<SmtpConfiguration>(input);
                smtpConfig.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                smtpConfig.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                smtpConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                smtpConfig.PASSWORD = CryptorEngine.Encrypt(smtpConfig.PASSWORD, true);
                smtpConfig.PASSWORD_OUTGOING = CryptorEngine.Encrypt(smtpConfig.PASSWORD_OUTGOING, true);
                if (!smtpConfig.ValidateInput())
                {
                    SmtpConfigResponse objResponse = new SmtpConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SmtpConfigRequest smtpConfigReq = new SmtpConfigRequest(smtpConfig);
                SmtpConfigResponse smtpConfigRes = smtpConfigReq.SaveSmtpConfig();
                smtpConfigRes.SmtpConfig.PASSWORD = CryptorEngine.Decrypt(smtpConfigRes.SmtpConfig.PASSWORD, true);
                smtpConfigRes.SmtpConfig.PASSWORD_OUTGOING = CryptorEngine.Decrypt(smtpConfigRes.SmtpConfig.PASSWORD_OUTGOING, true);
                return smtpConfigRes;
            }
            catch (Exception ex)
            {
                SmtpConfigResponse objResponse = new SmtpConfigResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("DeleteSmtpConfig")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Smtp Config APIs" })]
        public SmtpConfigResponse DeleteSmtpConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SmtpConfigResponse objResponse = new SmtpConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SmtpConfiguration smtpConfig = new JavaScriptSerializer().Deserialize<SmtpConfiguration>(input);
                smtpConfig.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                smtpConfig.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                smtpConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!smtpConfig.ValidateInput())
                {
                    SmtpConfigResponse objResponse = new SmtpConfigResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SmtpConfigRequest smtpConfigReq = new SmtpConfigRequest(smtpConfig);
                SmtpConfigResponse smtpConfigRes = smtpConfigReq.DeleteSmtpConfig();
                return smtpConfigRes;
            }
            catch (Exception ex)
            {
                SmtpConfigResponse objResponse = new SmtpConfigResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
