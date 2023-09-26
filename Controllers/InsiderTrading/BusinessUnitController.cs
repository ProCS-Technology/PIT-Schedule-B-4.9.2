using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/BusinessUnit")]
    public class BusinessUnitController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetBusinessUnitList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Business Unit APIs" })]
        public BusinessUnitResponse GetBusinessUnitList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BusinessUnitResponse objResponse = new BusinessUnitResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BusinessUnit businessUnit = new BusinessUnit();
                businessUnit.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                businessUnit.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                businessUnit.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                businessUnit.businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"]);
                if (!businessUnit.ValidateInput())
                {
                    BusinessUnitResponse objResponse = new BusinessUnitResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                BusinessUnitRequest businessUnitReq = new BusinessUnitRequest(businessUnit);
                BusinessUnitResponse businessUnitRes = businessUnitReq.GetBusinessUnit();
                return businessUnitRes;
            }
            catch (Exception ex)
            {
                BusinessUnitResponse objResponse = new BusinessUnitResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllBusinessUnitList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Business Unit APIs" })]
        public BusinessUnitResponse GetAllBusinessUnitList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BusinessUnitResponse objResponse = new BusinessUnitResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BusinessUnit businessUnit = new BusinessUnit();
                businessUnit.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                businessUnit.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                businessUnit.businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                businessUnit.businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"]);
                if (!businessUnit.ValidateInput())
                {
                    BusinessUnitResponse objResponse = new BusinessUnitResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                BusinessUnitRequest businessUnitReq = new BusinessUnitRequest(businessUnit);
                BusinessUnitResponse businessUnitRes = businessUnitReq.GetAllBusinessUnit();
                return businessUnitRes;
            }
            catch (Exception ex)
            {
                BusinessUnitResponse objResponse = new BusinessUnitResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
