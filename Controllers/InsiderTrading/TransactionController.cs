using Newtonsoft.Json.Linq;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Transaction")]
    public class TransactionController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SaveModifyRequest")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SaveModifyRequest()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                string input;
                if (sessionTokenKey == headerTokenKey)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }

                    dynamic objInput = Newtonsoft.Json.JsonConvert.DeserializeObject(input);

                    var x = (JObject)objInput;
                    string sReason = "";
                    foreach (JToken tok in x.Children())
                    {
                        if (tok is JProperty)
                        {
                            var prop = tok as JProperty;
                            var pName = prop.Name;

                            if (pName == "Reason")
                            {
                                sReason = prop.Value.ToString();
                            }
                        }
                    }

                    User user = new User();
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    user.remarks = sReason;
                    //if (!user.ValidateInput())
                    //{
                    //    UserResponse objResponse = new UserResponse();
                    //    objResponse.StatusFl = false;
                    //    objResponse.Msg = "Invalid Input Format";
                    //    return objResponse;
                    //}
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.SaveModifyRequest();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        public UserResponse UpdateModifyRequest()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                string input;
                if (sessionTokenKey == headerTokenKey)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }

                    dynamic objInput = Newtonsoft.Json.JsonConvert.DeserializeObject(input);

                    var x = (JObject)objInput;
                    string sTaskId = "";
                    string sStatus = "";
                    foreach (JToken tok in x.Children())
                    {
                        if (tok is JProperty)
                        {
                            var prop = tok as JProperty;
                            var pName = prop.Name;

                            if (pName == "TaskId")
                            {
                                sTaskId = prop.Value.ToString();
                            }
                            if (pName == "Status")
                            {
                                sStatus = prop.Value.ToString();
                            }
                        }
                    }

                    User user = new User();
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    user.ID = Convert.ToInt32(sTaskId);
                    user.status = sStatus;
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.UpdateModifyRequest();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse GetUserDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                User user = new User();
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userRequest = new UserRequest(user);
                UserResponse userResponse = userRequest.GetUserDetails();
                return userResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetFormDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public ModuleResponse GetFormDetails(string moduleNm)
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ModuleResponse objResponse = new ModuleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                ModuleRequest moduleReq = new ModuleRequest();
                ModuleResponse mRes = moduleReq.GetModule(moduleNm, Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]));
                return mRes;
            }
            catch (Exception ex)
            {
                ModuleResponse mRes = new ModuleResponse();
                mRes.StatusFl = false;
                mRes.Msg = ex.Message;
                return mRes;
            }
        }
        [Route("GetUserDetailsByFilter")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse GetUserDetailsByFilter()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userRequest = new UserRequest(user);
                UserResponse userResponse = userRequest.GetUserDetails();
                return userResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SavePersonalInformation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SavePersonalInformation()
        {
            try
            {
                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.AddUpdatePersonalDetail();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveRelativeDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SaveRelativeDetails()
        {
            try
            {

                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                string input;
                if (sessionTokenKey == headerTokenKey)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.AddUpdateRelativeDetails();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveRelativeDematDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SaveRelativeDematDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();

                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.AddUpdateDematDetails();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveInsiderHoldingDeclarationDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SaveInsiderHoldingDeclarationDetail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.AddUpdateInitialHoldingDeclarationDetail();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendEmailNoticeConfirmation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SendEmailNoticeConfirmation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();

                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    user.Task_Id = Convert.ToString(HttpContext.Current.Session["TASK_ID"]);
                    user.businessUnit = new BusinessUnit
                    {
                        businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]),
                        businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"])
                    };
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    //bool isEducAndProfQualGiven = userRequest.CheckWhetherEducationalAndProfessionalQualificationExist();
                    //if (!(isEducAndProfQualGiven))
                    //{
                    //    UserResponse userRespo = new UserResponse();
                    //    userRespo.StatusFl = false;
                    //    userRespo.Msg = "Educational And Professional Qualification does not exist.";
                    //    return userRespo;
                    //}
                    UserResponse userResponse = userRequest.SendEmailNoticeConfirmation();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveEducationalAndProfessionalDetails")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse SaveEducationalAndProfessionalDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();

                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    EducationalAndProfessionalDetail user = new JavaScriptSerializer().Deserialize<EducationalAndProfessionalDetail>(input);
                    user.loginId = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.SaveEducationalAndProfessionalQualification();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteRelativeDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse DeleteRelativeDetail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userRequest = new UserRequest(user);
                if (userRequest.ValidateRelativeDetailUsedInHigherComponent())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unable to delete relative information used in higher component !";
                    return objResponse;
                }
                UserResponse userResponse = userRequest.DeleteRelativeDetail();
                return userResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteDematDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse DeleteDematDetail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userRequest = new UserRequest(user);
                if (userRequest.ValidateDematDetailUsedInHigherComponent())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unable to delete demat information used in higher component !";
                    return objResponse;
                }
                UserResponse userResponse = userRequest.DeleteDematDetail();
                return userResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteInitialHoldingDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse DeleteInitialHoldingDetail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userRequest = new UserRequest(user);
                if (userRequest.ValidateInitialHoldingDetailUsedInHigherComponent())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unable to delete initial holding information used in higher component !";
                    return objResponse;
                }
                UserResponse userResponse = userRequest.DeleteInitialHoldingDetail();
                return userResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeletePhysicalHoldingDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse DeletePhysicalHoldingDetail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userRequest = new UserRequest(user);
                if (userRequest.ValidatePhysicalHoldingDetailUsedInHigherComponent())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unable to delete physical holding information used in higher component !";
                    return objResponse;
                }
                UserResponse userResponse = userRequest.DeletePhysicalHoldingDetail();
                return userResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SavePersonalInformation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse VerifyPan()
        {
            try
            {
                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    UserResponse userResponse = userRequest.VerifyPan();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserFormConfig")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public ModuleResponse GetUserFormConfig()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    ModuleResponse objResponse = new ModuleResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                ModuleRequest moduleReq = new ModuleRequest();
                ModuleResponse mRes = moduleReq.GetUserConfig(Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]));
                return mRes;
            }
            catch (Exception ex)
            {
                ModuleResponse mRes = new ModuleResponse();
                mRes.StatusFl = false;
                mRes.Msg = ex.Message;
                return mRes;
            }
        }
        [Route("PreviewDeclarationForm")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public UserResponse PreviewDeclarationForm()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();

                if (sessionTokenKey == headerTokenKey)
                {
                    string input;
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                    {
                        input = sr.ReadToEnd();
                    }
                    User user = new JavaScriptSerializer().Deserialize<User>(input);
                    user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    user.businessUnit = new BusinessUnit
                    {
                        businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]),
                        businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"])
                    };
                    if (!user.ValidateInput())
                    {
                        UserResponse objResponse = new UserResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = "Invalid Input Format";
                        return objResponse;
                    }
                    UserRequest userRequest = new UserRequest(user);
                    
                    UserResponse userResponse = userRequest.PreviewDeclarationForm();
                    return userResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}