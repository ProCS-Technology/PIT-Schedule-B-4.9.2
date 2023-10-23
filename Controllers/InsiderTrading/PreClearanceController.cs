using Ionic.Zip;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Linq;

namespace ProcsDLL.Controllers.InsiderTrading
{
    public class PreClearanceController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetTypeOfSecurityList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearance APIs" })]
        public SecurityTypeResponse GetTypeOfSecurityList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SecurityTypeResponse objResponse = new SecurityTypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                SecurityTypeRequest oReq = new SecurityTypeRequest();
                SecurityType obj = new SecurityType();
                obj.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                obj.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                obj.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                
                SecurityTypeRequest gReqPClR = new SecurityTypeRequest(obj);
                SecurityTypeResponse gResPClR = gReqPClR.GetTradableSecurity();
                return gResPClR;
            }
            catch (Exception ex)
            {
                SecurityTypeResponse objResponse = new SecurityTypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTypeOfTransactionList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearance APIs" })]
        public TransactionTypeResponse GetTypeOfTransactionList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    TransactionTypeResponse objResponse = new TransactionTypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                TransactionType pClR = new TransactionType();
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                TransactionTypeRequest gReqPClR = new TransactionTypeRequest(pClR);
                TransactionTypeResponse gResPClR = gReqPClR.GetTransactionType();
                return gResPClR;
            }
            catch (Exception ex)
            {
                TransactionTypeResponse objResponse = new TransactionTypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetRelativeDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "PreClearance APIs" })]
        public PreClearanceRequestResponse GetRelativeDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                PreClearanceRequest pClR = new PreClearanceRequest();
                pClR.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pClR.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pClR.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pClR.ValidateInput())
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(pClR);
                PreClearanceRequestResponse gResPClR = gReqPClR.GetRelativeDetailBN();
                return gResPClR;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveBrokerNoteList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "PreClearance APIs" })]
        public PreClearanceRequestResponse SaveBrokerNoteList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }

                List<PreClearanceRequest> pClR = new JavaScriptSerializer().Deserialize<List<PreClearanceRequest>>(input);
                PreClearanceRequestResponse gResPClRX = new PreClearanceRequestResponse();
                List<PreClearanceRequest> lstPClr = pClR.OrderBy(x => FormatHelper.FormatDate(x.ActualTransactionDate)).ToList();

                foreach (PreClearanceRequest p in lstPClr)
                {
                    p.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    p.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    p.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    p.TradeCompany = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    if (!p.ValidateInput())
                    {
                        gResPClRX.StatusFl = false;
                        gResPClRX.Msg = sXSSErrMsg;
                        return gResPClRX;
                    }
                }
                foreach (PreClearanceRequest p in lstPClr)
                {
                    p.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    p.LoginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    p.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    p.TradeCompany = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    PreClearanceRequestRequest gReqPClR = new PreClearanceRequestRequest(p);
                    PreClearanceRequestResponse gResPClR = gReqPClR.AddBrokerNoteWithNoPC();
                }
                gResPClRX.StatusFl = true;
                gResPClRX.Msg = "Data saved successfully";
                return gResPClRX;
            }
            catch (Exception ex)
            {
                PreClearanceRequestResponse objResponse = new PreClearanceRequestResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}