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
    [RoutePrefix("api/Relation")]
    public class RelationController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetRelationList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Relation APIs" })]
        public RelationResponse GetRelationList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Relation rel = new JavaScriptSerializer().Deserialize<Relation>(input);
                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                RelationRequest RelationList = new RelationRequest(rel);
                RelationResponse gResRelList = RelationList.GetRelationList();
                return gResRelList;
            }
            catch (Exception ex)
            {
                RelationResponse objResponse = new RelationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveRelation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Relation APIs" })]
        public RelationResponse SaveRelation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Relation rel = new JavaScriptSerializer().Deserialize<Relation>(input);
                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!rel.ValidateInput())
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RelationRequest rReq = new RelationRequest(rel);
                RelationResponse rRes = rReq.SaveRelation();
                return rRes;
            }
            catch (Exception ex)
            {
                RelationResponse objResponse = new RelationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteRelation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Relation APIs" })]
        public RelationResponse DeleteRelation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                Relation rel = new JavaScriptSerializer().Deserialize<Relation>(input1);
                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                RelationRequest rReq1 = new RelationRequest(rel);
                RelationResponse rRes1 = rReq1.DeleteRelation();
                return rRes1;
            }
            catch (Exception ex)
            {
                RelationResponse objResponse = new RelationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetRelationForRelative")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Relation APIs" })]
        public RelationResponse GetRelationForRelative()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Relation rel = new JavaScriptSerializer().Deserialize<Relation>(input);
                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                RelationRequest RelationList = new RelationRequest(rel);
                RelationResponse gResRelList = RelationList.GetRelationForRelative();
                return gResRelList;
            }
            catch (Exception ex)
            {
                RelationResponse objResponse = new RelationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetRelationForDeclaration")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Relation APIs" })]
        public RelationResponse GetRelationForDeclaration()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Relation rel = new JavaScriptSerializer().Deserialize<Relation>(input);
                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    RelationResponse objResponse = new RelationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                RelationRequest RelationList = new RelationRequest(rel);
                RelationResponse gResRelList = RelationList.GetRelationForDeclaration();
                return gResRelList;
            }
            catch (Exception ex)
            {
                RelationResponse objResponse = new RelationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}