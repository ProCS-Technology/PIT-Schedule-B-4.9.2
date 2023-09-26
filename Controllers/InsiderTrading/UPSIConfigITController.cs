﻿using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UPSIConfigIT")]
    public class UPSIConfigITController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetSmtpConfigList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Smtp Config APIs" })]
        public UPSIConfigResponse GetSmtpConfigList()
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
                UPSIConfiguration smtpConfig = new JavaScriptSerializer().Deserialize<UPSIConfiguration>(input);
                smtpConfig.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                smtpConfig.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                smtpConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                UPSIConfigRequest gReqSmtpConfigList = new UPSIConfigRequest(smtpConfig);
                UPSIConfigResponse gResSmtpConfigList = gReqSmtpConfigList.GetSmtpConfigList();
                if (gResSmtpConfigList.SmtpConfigList != null && gResSmtpConfigList.SmtpConfigList.Count > 0)
                {
                    gResSmtpConfigList.SmtpConfigList[0].PASSWORD_OUTGOING = CryptorEngine.Decrypt(gResSmtpConfigList.SmtpConfigList[0].PASSWORD_OUTGOING, true);
                }
                return gResSmtpConfigList;
            }
            catch (Exception ex)
            {
                UPSIConfigResponse objResponse = new UPSIConfigResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveSmtpConfig")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Smtp Config APIs" })]
        public UPSIConfigResponse SaveSmtpConfig()
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

                UPSIConfiguration smtpConfig = new JavaScriptSerializer().Deserialize<UPSIConfiguration>(input);
                smtpConfig.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                smtpConfig.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                smtpConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                smtpConfig.PASSWORD_OUTGOING = CryptorEngine.Encrypt(smtpConfig.PASSWORD_OUTGOING, true);
                if (smtpConfig.ValidateInput())
                {
                    UPSIConfigRequest smtpConfigReq = new UPSIConfigRequest(smtpConfig);
                    UPSIConfigResponse smtpConfigRes = smtpConfigReq.SaveSmtpConfig();
                    smtpConfigRes.SmtpConfig.PASSWORD_OUTGOING = CryptorEngine.Decrypt(smtpConfigRes.SmtpConfig.PASSWORD_OUTGOING, true);
                    return smtpConfigRes;
                }
                else
                {
                    UPSIConfigResponse smtpConfigRes = new UPSIConfigResponse();
                    smtpConfigRes.StatusFl = false;
                    smtpConfigRes.Msg = sXSSErrMsg;
                    return smtpConfigRes;
                }
            }
            catch (Exception ex)
            {
                UPSIConfigResponse objResponse = new UPSIConfigResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteSmtpConfig")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Smtp Config APIs" })]
        public UPSIConfigResponse DeleteSmtpConfig()
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

                UPSIConfiguration smtpConfig = new JavaScriptSerializer().Deserialize<UPSIConfiguration>(input);
                smtpConfig.CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                smtpConfig.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                smtpConfig.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (smtpConfig.ValidateInput())
                {
                    UPSIConfigRequest smtpConfigReq = new UPSIConfigRequest(smtpConfig);
                    UPSIConfigResponse smtpConfigRes = smtpConfigReq.DeleteSmtpConfig();
                    smtpConfigRes.StatusFl = true;
                    return smtpConfigRes;
                }
                else
                {
                    UPSIConfigResponse smtpConfigRes = new UPSIConfigResponse();
                    smtpConfigRes.StatusFl = false;
                    smtpConfigRes.Msg = sXSSErrMsg;
                    return smtpConfigRes;
                }
            }
            catch (Exception ex)
            {
                UPSIConfigResponse objResponse = new UPSIConfigResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}