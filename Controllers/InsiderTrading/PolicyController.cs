using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Policy")]
    public class PolicyController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetPolicy")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse GetPolicy()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Policy pol = new Policy();
                pol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pol.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest PolicyList = new PolicyRequest(pol);
                PolicyResponse gResPolList = PolicyList.GetPolicyList();
                return gResPolList;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllPolicyDocuments")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse GetAllPolicyDocuments()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                Policy pol = new Policy();
                pol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!pol.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest PolicyList = new PolicyRequest(pol);
                PolicyResponse gResPolList = PolicyList.GetAllPolicyDocumentsList();
                return gResPolList;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeletePolicy")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse DeletePolicy()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                Policy pol = new JavaScriptSerializer().Deserialize<Policy>(input1);
                pol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                pol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                pol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!pol.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest pReq1 = new PolicyRequest(pol);
                PolicyResponse pRes1 = pReq1.DeletePolicy();
                return pRes1;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SavePolicy")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public PolicyResponse SavePolicy()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                Policy rel = new JavaScriptSerializer().Deserialize<Policy>(input);
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = String.Empty;
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "Policy_";
                        string fname;

                        if(sNm.Contains("%00"))
                        {
                            PolicyResponse objResponse = new PolicyResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                            return objResponse;
                        }

                        if (ext.ToLower() == ".pdf")
                        {
                            fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                            sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/"), fname);
                            file.SaveAs(sSaveAs);
                        }
                        else
                        {
                            PolicyResponse objResponse = new PolicyResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Only pdf format is allowed in Policy Document";
                            return objResponse;
                        }

                        //if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        //{
                        //    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        //    fname = Path.GetFileNameWithoutExtension(testfiles[testfiles.Length - 1]) + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        //}
                        //else
                        //{
                        //    fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        //}
                        //sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/"), fname);
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/assets/logos/Policy/"));
                        }
                        String sSaveAs_Code_of_counduct = String.Empty;
                        sSaveAs_Code_of_counduct = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/"), "Code-Of-Conduct.pdf");
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/"));

                        }
                        file.SaveAs(sSaveAs_Code_of_counduct);
                        file.SaveAs(sSaveAs);
                        rel.DOCUMENT = fname;
                    }
                }
                else
                {
                    rel.DOCUMENT = String.Empty;
                }

                rel.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!rel.ValidateInput())
                {
                    PolicyResponse objResponse = new PolicyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PolicyRequest pReq = new PolicyRequest(rel);
                PolicyResponse pRes = pReq.SavePolicy();
                return pRes;
            }
            catch (Exception ex)
            {
                PolicyResponse objResponse = new PolicyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}