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
    [RoutePrefix("api/ApproverSetUp")]
    public class ApproverSetUpController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SaveApproverSetUp")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Approver Set Up APIs" })]
        public ApproverResponseSetUp SaveApproverSetUp()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ApproverSetUp approverSetUp = new JavaScriptSerializer().Deserialize<ApproverSetUp>(input);
                approverSetUp.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                approverSetUp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                approverSetUp.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (!approverSetUp.ValidateInput())
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;// "Invalid Input Format";
                    return objResponse;
                }
                ApproverRequest approverSetUpList = new ApproverRequest(approverSetUp);
                ApproverResponseSetUp gResapproverSetUpList = approverSetUpList.SaveApproverSetUp();
                return gResapproverSetUpList;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetApproverSetUpLIST")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Approver Set Up APIs" })]
        public ApproverResponseSetUp GetApproverSetUpLIST()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                ApproverSetUp approverSetUp = new ApproverSetUp();
                approverSetUp.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                approverSetUp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!approverSetUp.ValidateInput())
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                ApproverRequest approverSetUpList = new ApproverRequest(approverSetUp);
                ApproverResponseSetUp gResapproverSetUpList = approverSetUpList.GetApproverSetUpLIST();
                return gResapproverSetUpList;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetApproverSetUpById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Approver Set Up APIs" })]
        public ApproverResponseSetUp GetApproverSetUpById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ApproverSetUp approverSetUp = new JavaScriptSerializer().Deserialize<ApproverSetUp>(input);
                approverSetUp.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                approverSetUp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!approverSetUp.ValidateInput())
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                ApproverRequest approverSetUpList = new ApproverRequest(approverSetUp);
                ApproverResponseSetUp gResapproverSetUpList = approverSetUpList.GetApproverSetUpById();
                return gResapproverSetUpList;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteApproverSetUp")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Approver Set Up APIs" })]
        public ApproverResponseSetUp DeleteApproverSetUp()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                ApproverSetUp approverSetUp = new JavaScriptSerializer().Deserialize<ApproverSetUp>(input);
                approverSetUp.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                approverSetUp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!approverSetUp.ValidateInput())
                {
                    ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                ApproverRequest approverSetUpList = new ApproverRequest(approverSetUp);
                ApproverResponseSetUp gResapproverSetUpList = approverSetUpList.DeleteApproverSetUp();
                return gResapproverSetUpList;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp objResponse = new ApproverResponseSetUp();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}