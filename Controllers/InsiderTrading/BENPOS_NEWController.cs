using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.IO;
using System.Globalization;
using System.Data.OleDb;
using System.Data;
using ProcsDLL.Models.Infrastructure;
using System.Configuration;
using System.Data.SqlClient;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/BENPOS_NEW")]

    public class BENPOS_NEWController : ApiController
    {
        [Route("SaveBenposHdr")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BENPOS_NEWResponse SaveBenposHdr()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BENPOS_NEWResponse objResponse = new BENPOS_NEWResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
            
                BenposHeader rel = new JavaScriptSerializer().Deserialize<BenposHeader>(input);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);

                BENPOS_NEWRequest pReq = new BENPOS_NEWRequest(rel);
               
                bool isValidBenposAsOfDate = pReq.ValidateBenposAsOfDate();
                if (!isValidBenposAsOfDate)
                {
                    BENPOS_NEWResponse objResponse = new BENPOS_NEWResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Benpos as of date cannot be less than or equal to last benpos as of date!";
                    return objResponse;
                }
               
                pReq = new BENPOS_NEWRequest(rel);
                BENPOS_NEWResponse pRes = pReq.SaveBenposHdr();
                return pRes;
                
            }
            catch (Exception ex)
            {
                BENPOS_NEWResponse objResponse = new BENPOS_NEWResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }

        }

        [Route("GetAllBenposHdr")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BENPOS_NEWResponse GetAllBenposHdr()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BENPOS_NEWResponse objResponse = new BENPOS_NEWResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BenposHeader benposHdr = new BenposHeader();
                benposHdr.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                benposHdr.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposHdr.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!benposHdr.ValidateInput())
                {
                    BENPOS_NEWResponse objResponse = new BENPOS_NEWResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                BENPOS_NEWRequest BenposHdrList = new BENPOS_NEWRequest(benposHdr);
                BENPOS_NEWResponse gResBenposHdrList = BenposHdrList.GetAllBenposHdr();
                return gResBenposHdrList;
            }
            catch (Exception ex)
            {
                BENPOS_NEWResponse objResponse = new BENPOS_NEWResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}

    

