using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UserIT")]
    public class UserITController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SerchByName")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse SerchByName()
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
                string CATID2 = HttpContext.Current.Request.QueryString["UserName"];
                // string uid = "CSPIT";
                string uid = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["LDPUserId"]), true);
                // string pwd = "Petronet@1234";
                string pwd = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["LDPPassword"]), true);
                string username = CATID2;// "Karan";

                // string ldap = "LDAP://pll.co.in";
                string ldap = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["LDPPath"]), true);

                DirectoryEntry de = new DirectoryEntry(ldap, uid, pwd);
                de.RefreshCache();

                DirectorySearcher dirSearch = null;
                dirSearch = new DirectorySearcher(de);

                dirSearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(cn=*" + username + "*))";
                var searchResult = dirSearch.FindAll();

                UserResponse res = new UserResponse();

                if (searchResult != null)
                {
                    List<User> Users = new List<User>();
                    foreach (SearchResult rs in searchResult)
                    {
                        User oUser = new User();
                        int count = 0;
                        foreach (var pc in rs.GetDirectoryEntry().Properties.PropertyNames)
                        {
                            string ss = pc.ToString();
                            if (pc.ToString().ToUpper() == "CN")
                            {
                                oUser.USER_NM = rs.GetDirectoryEntry().Properties[ss].Value.ToString();
                            }
                            if (pc.ToString().ToUpper() == "SAMACCOUNTNAME")
                            {
                                oUser.LOGIN_ID = rs.GetDirectoryEntry().Properties[ss].Value.ToString();
                                oUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                                oUser.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                                oUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                                oUser.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);

                                SqlParameter[] parameters = new SqlParameter[4];
                                parameters[0] = new SqlParameter("@Mode", "GET_USER_LIST");
                                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                                parameters[1].Direction = ParameterDirection.Output;
                                parameters[2] = new SqlParameter("@COMPANY_ID", oUser.companyId);
                                parameters[3] = new SqlParameter("@MODULE_ID", oUser.moduleId);
                                var executeScalar = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", oUser.MODULE_DATABASE, parameters);
                                int iCnt = 0;
                                iCnt = Convert.ToInt32(executeScalar);
                                count = iCnt;


                            }//if
                            if (pc.ToString().ToUpper() == "MAIL")
                            {
                                oUser.USER_EMAIL = rs.GetDirectoryEntry().Properties[ss].Value.ToString();
                            }
                            if (pc.ToString().ToUpper() == "MOBILE")
                            {
                                oUser.USER_MOBILE = rs.GetDirectoryEntry().Properties[ss].Value.ToString();
                            }
                        }
                        if (oUser.USER_EMAIL != null && oUser.LOGIN_ID != null && oUser.USER_NM != null)
                        {
                            Users.Add(oUser);
                        }
                    }
                    if (Users.Count > 0)
                    {
                        res.StatusFl = true;
                        res.Msg = "Success";
                        res.UserList = Users;
                    }
                    else
                    {
                        res.Msg = "No Data Found";
                    }
                }
                else
                {
                    res.StatusFl = true;
                    res.Msg = "No Data Found";
                }
                return res;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDebtSecurityDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetDebtSecurityDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetDebtSecurityDetails();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SearchByNameInLocalSystem")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse SearchByNameInLocalSystem()
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
                string CATID2 = HttpContext.Current.Request.QueryString["UserName"];
                string path = string.Format("WinNT://{0},computer", Environment.MachineName);
                UserResponse res = new UserResponse();
                List<User> Users = new List<User>();
                using (DirectoryEntry computerEntry = new DirectoryEntry(path))
                {
                    IEnumerable<string> userNames = computerEntry.Children
                        .Cast<DirectoryEntry>()
                        .Where(childEntry => childEntry.SchemaClassName == "User")
                        .Select(userEntry => userEntry.Name);

                    foreach (string name in userNames)
                    {
                        if (name.IndexOf(CATID2) != -1)
                        {
                            User oUser = new User();
                            oUser.USER_NM = name;
                            oUser.LOGIN_ID = name;
                            oUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                            oUser.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                            oUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                            oUser.USER_EMAIL = String.Empty;
                            oUser.USER_MOBILE = String.Empty;
                            Users.Add(oUser);
                        }

                    }
                }
                if (Users.Count > 0)
                {
                    res.StatusFl = true;
                    res.Msg = "Success";
                    res.UserList = Users;
                }
                else
                {
                    res.StatusFl = true;
                    res.Msg = "No Data Found";
                    res.UserList = Users;
                }
                return res;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AddADUserToUserList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse AddADUserToUserList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                List<User> userList = new JavaScriptSerializer().Deserialize<List<User>>(input);
                foreach (User oUser in userList)
                {
                    User objUser = new User();
                    objUser = oUser;
                    objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    objUser.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                    objUser.uploadAvatar = "Unknown.png";
                    objUser.USER_PWD = hashcodegenerate.GetSHA512("P@ssw0rd");
                    objUser.status = "Active";
                    objUser.userRole = new Role
                    {
                        ROLE_ID = 2
                    };
                    objUser.designation = new Designation
                    {
                        DESIGNATION_ID = 0
                    };
                    objUser.department = new Department
                    {
                        DEPARTMENT_ID = 0
                    };
                    UserRequest UserReq = new UserRequest(objUser);
                    UserReq.SaveUser();
                }

                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = true;
                objResponse.Msg = "Success";
                return objResponse;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetUserList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
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
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetUserList();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDPUsers")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetDPUsers()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
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
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetDPUsers();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserListByBusinessUnitId")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetUserListByBusinessUnitId()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetUserListByBusinessUnitId();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        //====================================
        [Route("GetUserAuthTypeByLoginId")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetUserAuthTypeByLoginId()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetUserAuthTypeByLoginId();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        //======================================
        [Route("GetUserListForApprover")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetUserListForApprover()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }

                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
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

                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetUserListByBusinessUnitId();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUser")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse SaveUser()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                String input = HttpContext.Current.Request.Form["Object"];
                System.Web.Script.Serialization.JavaScriptSerializer serializer1 = new System.Web.Script.Serialization.JavaScriptSerializer();
                UserResponse UserRes = new UserResponse();
                List<User> lstUser = new List<User>();
                lstUser = serializer1.Deserialize<List<User>>(input);
                if (lstUser == null)
                {
                    UserRes.StatusFl = false;
                    UserRes.Msg = "Something went wrong";
                    return UserRes;
                }
                else if (lstUser.Count == 0)
                {
                    UserRes.StatusFl = false;
                    UserRes.Msg = "Something went wrong";
                    return UserRes;
                }
                else
                {
                    User objUser = new User();
                    objUser = lstUser[0];
                    objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    objUser.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                    objUser.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);

                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        String sSaveAs = "";
                        String userDir = "/InsiderTrading/images/user/";

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + userDir)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + userDir));
                        }

                        System.Security.AccessControl.DirectorySecurity sec = Directory.GetAccessControl(HttpContext.Current.Server.MapPath("~" + userDir));
                        // Using this instead of the "Everyone" string means we work on non-English systems.
                        System.Security.Principal.SecurityIdentifier everyone = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
                        sec.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(everyone, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit, System.Security.AccessControl.PropagationFlags.None, System.Security.AccessControl.AccessControlType.Allow));
                        Directory.SetAccessControl(HttpContext.Current.Server.MapPath("~" + userDir), sec);

                        HttpFileCollection files = HttpContext.Current.Request.Files;
                        String newFileName = String.Empty;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFile file = files[i];
                            String ext = Path.GetExtension(file.FileName);
                            String name = Path.GetFileNameWithoutExtension(file.FileName);
                            if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                newFileName = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                            }
                            else
                            {
                                newFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                            }
                            sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                            file.SaveAs(sSaveAs);
                            objUser.uploadAvatar = newFileName;
                        }
                    }
                    else
                    {
                        objUser.uploadAvatar = "Unknown.png";
                    }

                    UserRequest UserReq = new UserRequest(objUser);
                    UserRes = new UserResponse();
                    UserRes = UserReq.SaveUser();
                    return UserRes;
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
        [Route("DeleteUser")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse DeleteUser()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                User user1 = new JavaScriptSerializer().Deserialize<User>(input1);
                user1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!user1.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest cReq = new UserRequest(user1);
                UserResponse cRes = cReq.DeleteUser();
                return cRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AssignedApprover")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse AssignedApprover()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input1);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
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

                UserRequest cReq = new UserRequest(user);
                UserResponse cRes = cReq.AssignedApprover();
                return cRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AssignedApproverForCO")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse AssignedApproverForCO()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input1);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
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

                UserRequest cReq = new UserRequest(user);
                UserResponse cRes = cReq.AssignedApproverForCO();
                return cRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveDepositoryTypeOperation")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public DepositoryResponse SaveDepositoryTypeOperation()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepositoryResponse objSessionResponse = new DepositoryResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                Depository depository = new JavaScriptSerializer().Deserialize<Depository>(input1);
                depository.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                depository.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                if (!depository.ValidateInput())
                {
                    DepositoryResponse objResponse = new DepositoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                DepositoryRequest cReq = new DepositoryRequest(depository);
                DepositoryResponse cRes = cReq.SaveDepositoryTypeOperation();
                return cRes;
            }
            catch (Exception ex)
            {
                DepositoryResponse objResponse = new DepositoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveThresholdLimitAndByTime")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public DepositoryResponse SaveThresholdLimitAndByTime()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepositoryResponse objSessionResponse = new DepositoryResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                List<Depository> depository = new JavaScriptSerializer().Deserialize<List<Depository>>(input1);
                foreach (Depository obj in depository)
                {
                    obj.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    obj.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    if (!obj.ValidateInput())
                    {
                        DepositoryResponse objResponse = new DepositoryResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = sXSSErrMsg;
                        return objResponse;
                    }
                }




                DepositoryRequest cReq = new DepositoryRequest(depository);
                DepositoryResponse cRes = cReq.SaveThresholdLimitAndByTime();
                return cRes;
            }
            catch (Exception ex)
            {
                DepositoryResponse objResponse = new DepositoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDepositoryType")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public DepositoryResponse GetDepositoryType()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepositoryResponse objSessionResponse = new DepositoryResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                Depository depository = new Depository();
                depository.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                depository.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!depository.ValidateInput())
                {
                    DepositoryResponse objResponse = new DepositoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                DepositoryRequest getDeposRequest = new DepositoryRequest(depository);
                DepositoryResponse getDeposResposnse = getDeposRequest.GetDepositoryType();
                return getDeposResposnse;
            }
            catch (Exception ex)
            {
                DepositoryResponse objResponse = new DepositoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetThresholdByTimeSettings")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public DepositoryResponse GetThresholdByTimeSettings()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    DepositoryResponse objSessionResponse = new DepositoryResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                Depository depository = new Depository();
                depository.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                depository.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                depository.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!depository.ValidateInput())
                {
                    DepositoryResponse objResponse = new DepositoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }

                DepositoryRequest getDeposRequest = new DepositoryRequest(depository);
                DepositoryResponse getDeposResposnse = getDeposRequest.GetThresholdByTimeSettings();
                return getDeposResposnse;
            }
            catch (Exception ex)
            {
                DepositoryResponse objResponse = new DepositoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTransactionalInfo")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetTransactionalInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetTransactionalInfo();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDeclarationForms")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetDeclarationForms()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);

                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                //user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetDeclarationForms();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMyDetailReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetMyDetailReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetTransactionalInfo();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTransactionalInfoByVersion")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetTransactionalInfoByVersion()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetTransactionalInfoByVersion();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetTransactionalInfoByVersionByAdmin")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetTransactionalInfoByVersionByAdmin()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = user.LOGIN_ID == String.Empty ? Convert.ToString(HttpContext.Current.Session["EmployeeId"]) : user.LOGIN_ID;
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetTransactionalInfoByVersion();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveInsiderTradingWindow")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse SaveInsiderTradingWindow()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                InsiderTradingWindow insiderTradingWindow = new JavaScriptSerializer().Deserialize<InsiderTradingWindow>(input);
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                insiderTradingWindow.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.SaveInsiderTradingWindow();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetInsiderTradingWindowClosureInfo")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfo()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                InsiderTradingWindow insiderTradingWindow = new InsiderTradingWindow();
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                insiderTradingWindow.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.GetInsiderTradingWindowClosureInfo();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetInsiderTradingWindowClosureInfoList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfoList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                InsiderTradingWindow insiderTradingWindow = new InsiderTradingWindow();
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.GetInsiderTradingWindowClosureInfoList();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SendEmailForTradingWindowClosure")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public InsiderTradingWindowResponse SendEmailForTradingWindowClosure()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    InsiderTradingWindowResponse objSessionResponse = new InsiderTradingWindowResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                InsiderTradingWindow insiderTradingWindow = new JavaScriptSerializer().Deserialize<InsiderTradingWindow>(input);
                insiderTradingWindow.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                insiderTradingWindow.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                insiderTradingWindow.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (!insiderTradingWindow.ValidateInput())
                {
                    InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                InsiderTradingWindowRequest getInsiderTradingWindowReq = new InsiderTradingWindowRequest(insiderTradingWindow);
                InsiderTradingWindowResponse getInsiderTradingWindowRes = getInsiderTradingWindowReq.SendEmailForTradingWindowClosure();
                return getInsiderTradingWindowRes;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse objResponse = new InsiderTradingWindowResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AddTaskForPeriodicDeclaration")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public PeriodicDeclarationResponse AddTaskForPeriodicDeclaration()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PeriodicDeclarationResponse objSessionResponse = new PeriodicDeclarationResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                PeriodicDeclaration periodicDeclaration = new JavaScriptSerializer().Deserialize<PeriodicDeclaration>(input);
                periodicDeclaration.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                periodicDeclaration.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                periodicDeclaration.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                periodicDeclaration.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!periodicDeclaration.ValidateInput())
                {
                    PeriodicDeclarationResponse objResponse = new PeriodicDeclarationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PeriodicDeclarationRequest getPeriodicDeclarationReq = new PeriodicDeclarationRequest(periodicDeclaration);
                PeriodicDeclarationResponse getPeriodicDeclarationRes = getPeriodicDeclarationReq.AddTaskForPeriodicDeclaration();
                return getPeriodicDeclarationRes;
            }
            catch (Exception ex)
            {
                PeriodicDeclarationResponse objResponse = new PeriodicDeclarationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetPeriodicDeclaration")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public PeriodicDeclarationResponse GetPeriodicDeclaration()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    PeriodicDeclarationResponse objSessionResponse = new PeriodicDeclarationResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                PeriodicDeclaration periodicDeclaration = new PeriodicDeclaration();
                periodicDeclaration.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                periodicDeclaration.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                periodicDeclaration.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                periodicDeclaration.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!periodicDeclaration.ValidateInput())
                {
                    PeriodicDeclarationResponse objResponse = new PeriodicDeclarationResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                PeriodicDeclarationRequest getPeriodicDeclarationReq = new PeriodicDeclarationRequest(periodicDeclaration);
                PeriodicDeclarationResponse getPeriodicDeclarationRes = getPeriodicDeclarationReq.GetPeriodicDeclaration();
                return getPeriodicDeclarationRes;
            }
            catch (Exception ex)
            {
                PeriodicDeclarationResponse objResponse = new PeriodicDeclarationResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveForm")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse SaveForm()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User objUser = new User();
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                objUser.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objUser.businessUnit = new BusinessUnit
                {
                    businessUnitId = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]),
                    businessUnitName = Convert.ToString(HttpContext.Current.Session["BusinessUnitName"])
                };
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    String sSaveAs = "";
                    String userDir = "/InsiderTrading/Forms/UploadedFormByUser/";

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + userDir)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + userDir));
                    }

                    //System.Security.AccessControl.DirectorySecurity sec = Directory.GetAccessControl(HttpContext.Current.Server.MapPath("~" + userDir));
                    // Using this instead of the "Everyone" string means we work on non-English systems.
                    //System.Security.Principal.SecurityIdentifier everyone = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
                    //sec.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(everyone, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit, System.Security.AccessControl.PropagationFlags.None, System.Security.AccessControl.AccessControlType.Allow));
                    //Directory.SetAccessControl(HttpContext.Current.Server.MapPath("~" + userDir), sec);

                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    String newFileName = String.Empty;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        String name = Path.GetFileNameWithoutExtension(file.FileName);
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            newFileName = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            newFileName = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + userDir), newFileName);
                        file.SaveAs(sSaveAs);

                        objUser.uploadAvatar = newFileName;
                    }
                }
                if (!objUser.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userReq = new UserRequest(objUser);
                UserResponse userRes = userReq.SaveUploadedForm();
                return userRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserUploadedForms")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetUserUploadedForms()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User objUser = new User();
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                objUser.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!objUser.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userReq = new UserRequest(objUser);
                UserResponse userRes = userReq.GetUserUploadedForms();
                return userRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCompanyForms")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetCompanyForms()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User objUser = new User();
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                objUser.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!objUser.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userReq = new UserRequest(objUser);
                UserResponse userRes = userReq.GetCompanyForms();
                return userRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetMISReport")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetMISReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User objUser = new User();
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                objUser.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objUser.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                if (!objUser.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userReq = new UserRequest(objUser);
                UserResponse userRes = userReq.GetMISReport();
                return userRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SetUserRole")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse SetUserRole()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User objUser = new JavaScriptSerializer().Deserialize<User>(input);
                objUser.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUser.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                objUser.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!objUser.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest userReq = new UserRequest(objUser);
                UserResponse userRes = userReq.SetUserRole();
                return userRes;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AddCompanyNameAndISIN")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public CompanyResponse AddCompanyNameAndISIN()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CompanyResponse objSessionResponse = new CompanyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                CompanySettings companySettings = new JavaScriptSerializer().Deserialize<CompanySettings>(input);
                companySettings.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                companySettings.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!companySettings.ValidateInput())
                {
                    CompanyResponse objResponse = new CompanyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                CompanyRequest getCompanyReq = new CompanyRequest(companySettings);
                CompanyResponse getCompanyRes = getCompanyReq.AddCompanyNameAndISIN();
                return getCompanyRes;
            }
            catch (Exception ex)
            {
                CompanyResponse objResponse = new CompanyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetCompanyNameAndISIN")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public CompanyResponse GetCompanyNameAndISIN()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CompanyResponse objSessionResponse = new CompanyResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                CompanySettings companySettings = new CompanySettings();
                companySettings.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                companySettings.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!companySettings.ValidateInput())
                {
                    CompanyResponse objResponse = new CompanyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                CompanyRequest getCompanyReq = new CompanyRequest(companySettings);
                CompanyResponse getCompanyRes = getCompanyReq.GetCompanyNameAndISIN();
                return getCompanyRes;
            }
            catch (Exception ex)
            {
                CompanyResponse objResponse = new CompanyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AccessUserListByBusinessUnitId")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse AccessUserListByBusinessUnitId()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.AccessUserListByBusinessUnitId();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserApprover")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetUserApprover()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
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
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest gReqUserList = new UserRequest(user);
                UserResponse gResUserList = gReqUserList.GetUserApprover();
                return gResUserList;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        public UserResponse GetRelativeList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetRelativeList();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDisclouserTaskList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetDisclouserTaskList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                //user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetDisclouserTaskList();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetPersonDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetPersonDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetPersonalDetails();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetRelativeDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetRelativeDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetRelativeDetails();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetDematDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetDematDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetDematDetails();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetHoldingDetails")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public UserResponse GetHoldingDetails()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserResponse objSessionResponse = new UserResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                User user = new User();
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
                if (!user.ValidateInput())
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UserRequest getTransactionalInfo = new UserRequest(user);
                UserResponse gRespTransactionInfo = getTransactionalInfo.GetHoldingDetails();
                return gRespTransactionInfo;
            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        string sInsiderDb = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ITDB"]), true);
        //============get Declaration file id by skm==================
        [Route("GetDeclarationFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Declaration APIs" })]
        public HttpResponseMessage GetDeclarationFile(string DeclarationId , string Ext)
        {
            string loginUser= Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            string _sql = "select A.FILE_NAME,A.DATA_ELEMENT_ID from FORM_LOGS A  where ID= " + DeclarationId;
            DataSet dsTemplate = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, sInsiderDb, null);
            DataTable dt = dsTemplate.Tables[0];

            if (HttpContext.Current.Session.Count > 0 && loginUser== Convert.ToString(dt.Rows[0]["DATA_ELEMENT_ID"]))
            {
                //string _sql1 = "select FILE_NAME from FORM_LOGS " +
                //                            "WHERE  ID=" + DeclarationId;
                //string sFileName = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, sInsiderDb, null);

                string filePath = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + Convert.ToString(dt.Rows[0]["FILE_NAME"]));


                byte[] fileBook = File.ReadAllBytes(filePath);
                MemoryStream stream = new MemoryStream();
                string excelBase64String = Convert.ToBase64String(fileBook);
                StreamWriter excelWriter = new StreamWriter(stream);
                excelWriter.Write(excelBase64String);
                excelWriter.Flush();
                stream.Position = 0;
                //HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StreamContent(stream);
                if (Ext == ".pdf")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "PdfReport.pdf");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "PdfReport.pdf";
                }
                else if (Ext == ".txt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "TextFile.txt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "TextFile.txt";
                }
                else if (Ext == ".xlsx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                }
                else if (Ext == ".xls")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xls");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xls";
                }
                else if (Ext == ".docx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.docx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.docx";
                }
                else if (Ext == ".doc")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.doc");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.doc";
                }
                else if (Ext == ".png")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.png");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.png";
                }
                else if (Ext == ".jpeg")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.jpeg");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.jpeg";
                }
                else if (Ext == ".gif")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.gif");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.gif";
                }
                else if (Ext == ".zip")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.zip");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.zip";
                }
                else if (Ext == ".pptx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.pptx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.pptx";
                }
                else if (Ext == ".ppt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.ppt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-powerpoint");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.ppt";
                }
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                
            }
            else
            {
                //httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(baseUrl + "/login.aspx");
                return response;
            }
            return httpResponseMessage;
        }
        //===================end=================

        //============get Policy file id by skm==================
        [Route("GetPolicyFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public HttpResponseMessage GetPolicyFile(string DeclarationId, string Ext)
        {
            string loginUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            string _sql = "select A.POLICY_DOC,A.DATA_ELEMENT_ID from FORM_LOGS A  where ID= " + DeclarationId;
            DataSet dsTemplate = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, sInsiderDb, null);
            DataTable dt = dsTemplate.Tables[0];

            if (HttpContext.Current.Session.Count > 0 && loginUser == Convert.ToString(dt.Rows[0]["DATA_ELEMENT_ID"]))
            {
                //string _sql1 = "select FILE_NAME from FORM_LOGS " +
                //                            "WHERE  ID=" + DeclarationId;
                //string sFileName = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, sInsiderDb, null);

                string filePath = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + Convert.ToString(dt.Rows[0]["POLICY_DOC"]));


                byte[] fileBook = File.ReadAllBytes(filePath);
                MemoryStream stream = new MemoryStream();
                string excelBase64String = Convert.ToBase64String(fileBook);
                StreamWriter excelWriter = new StreamWriter(stream);
                excelWriter.Write(excelBase64String);
                excelWriter.Flush();
                stream.Position = 0;
                //HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StreamContent(stream);
                if (Ext == ".pdf")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "PdfReport.pdf");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "PdfReport.pdf";
                }
                else if (Ext == ".txt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "TextFile.txt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "TextFile.txt";
                }
                else if (Ext == ".xlsx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                }
                else if (Ext == ".xls")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xls");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xls";
                }
                else if (Ext == ".docx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.docx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.docx";
                }
                else if (Ext == ".doc")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.doc");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.doc";
                }
                else if (Ext == ".png")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.png");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.png";
                }
                else if (Ext == ".jpeg")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.jpeg");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.jpeg";
                }
                else if (Ext == ".gif")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "img.gif");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.gif";
                }
                else if (Ext == ".zip")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.zip");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.zip";
                }
                else if (Ext == ".pptx")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.pptx");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.pptx";
                }
                else if (Ext == ".ppt")
                {
                    httpResponseMessage.Content.Headers.Add("x-filename", "file.ppt");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-powerpoint");
                    httpResponseMessage.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.ppt";
                }
                httpResponseMessage.StatusCode = HttpStatusCode.OK;

            }
            else
            {
                //httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(baseUrl + "/login.aspx");
                return response;
            }
            return httpResponseMessage;

        }
        //===================end=================
        //[HttpPost]
        //[SwaggerOperation(Tags = new[] { "User APIs" })]
        //public UserResponse GetUserRoleWise()
        //{
        //    try
        //    {
        //        if (HttpContext.Current.Session.Count == 0)
        //        {
        //            UserResponse objSessionResponse = new UserResponse();
        //            objSessionResponse.StatusFl = false;
        //            objSessionResponse.Msg = "SessionExpired";
        //            return objSessionResponse;
        //        }
        //        User user = new User();
        //        user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
        //        user.moduleId = Convert.ToInt32(HttpContext.Current.Session["ModuleId"]);
        //        user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
        //        user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
        //        user.ADMIN_DATABASE = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["AdminDB"], true);
        //        if (!user.ValidateInput())
        //        {
        //            UserResponse objResponse = new UserResponse();
        //            objResponse.StatusFl = false;
        //            objResponse.Msg = "Invalid Input Format";
        //            return objResponse;
        //        }
        //        UserRequest getTransactionalInfo = new UserRequest(user);
        //        UserResponse gRespTransactionInfo = getTransactionalInfo.GetUserRoleWise();
        //        return gRespTransactionInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        UserResponse objResponse = new UserResponse();
        //        objResponse.StatusFl = false;
        //        objResponse.Msg = ex.Message;
        //        return objResponse;
        //    }
        //}


        //Added by Jiten
        [Route("GetAttachmentFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public HttpResponseMessage GetAttachmentFile(string Id, string FileExtension)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (HttpContext.Current.Session.Count > 0)
            {
                string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                string filenameX = "";
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    string sqlQuery = "SELECT FILE_NAME,CREATED_BY,DATA_ELEMENT_ID FROM FORM_LOGS WHERE ID=@Id";
                    SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    sCon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        filenameX = dt.Rows[0]["FILE_NAME"].ToString();
                    }
                    //string extension = Path.GetExtension(filenameX);
                    //string EXT = FileExtension;
                    //string basePath = "~/InsiderTrading/UPSI/"; // Base path to the directory
                    //string filePath1 = HttpContext.Current.Server.MapPath(basePath + filenameX);

                    string filePath = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + filenameX);

                    byte[] fileBook = File.ReadAllBytes(filePath);
                    MemoryStream stream = new MemoryStream();
                    string excelBase64String = Convert.ToBase64String(fileBook);
                    StreamWriter excelWriter = new StreamWriter(stream);
                    excelWriter.Write(excelBase64String);
                    excelWriter.Flush();
                    stream.Position = 0;

                    httpResponseMessage.Content = new StreamContent(stream);
                    if (FileExtension == "pdf")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "PdfReport.pdf");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "PdfReport.pdf";
                    }
                    else if (FileExtension == "txt")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "TextFile.txt");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "TextFile.txt";
                    }
                    else if (FileExtension == "xlsx")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                    }
                    else if (FileExtension == "xls")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xls");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xls";
                    }
                    else if (FileExtension == "doc")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.doc");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.doc";
                    }
                    else if (FileExtension == "docx")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.docx");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.docx";
                    }
                    else if (FileExtension == "png")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "img.png");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.png";
                    }
                    else if (FileExtension == "jpeg")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "img.jpeg");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.jpeg";
                    }
                    else if (FileExtension == "gif")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "img.gif");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.gif";
                    }
                    else if (FileExtension == "zip")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "file.zip");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.zip";
                    }
                    else if (FileExtension == "ppt")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "File.ppt");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.ppt";
                    }
                    else if (FileExtension == "pptx")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "File.pptx");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.pptx";
                    }

                    httpResponseMessage.StatusCode = HttpStatusCode.OK;

                }

            }
            return httpResponseMessage;
        }

        //Added by jiten
        [Route("GetPolicyFileJK")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public HttpResponseMessage GetPolicyFileJK(string DeclarationId, string Ext)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (HttpContext.Current.Session.Count > 0)
            {
                string sConStr = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                string filenameX = "";
                using (SqlConnection sCon = new SqlConnection(sConStr))
                {
                    sCon.Open();
                    string sqlQuery = "select A.POLICY_DOC,A.DATA_ELEMENT_ID from FORM_LOGS A  where ID=@DeclarationId";
                    SqlCommand cmd = new SqlCommand(sqlQuery, sCon);
                    cmd.Parameters.AddWithValue("@DeclarationId", DeclarationId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    sCon.Close();
                    if (dt.Rows.Count > 0)
                    {
                        filenameX = dt.Rows[0]["POLICY_DOC"].ToString();
                    }
                    //string extension = Path.GetExtension(filenameX);
                    //string EXT = FileExtension;
                    //string basePath = "~/InsiderTrading/UPSI/"; // Base path to the directory
                    //string filePath1 = HttpContext.Current.Server.MapPath(basePath + filenameX);

                    string filePath = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + filenameX);

                    byte[] fileBook = File.ReadAllBytes(filePath);
                    MemoryStream stream = new MemoryStream();
                    string excelBase64String = Convert.ToBase64String(fileBook);
                    StreamWriter excelWriter = new StreamWriter(stream);
                    excelWriter.Write(excelBase64String);
                    excelWriter.Flush();
                    stream.Position = 0;

                    httpResponseMessage.Content = new StreamContent(stream);
                    if (Ext == ".pdf")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "PdfReport.pdf");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "PdfReport.pdf";
                    }
                    else if (Ext == ".txt")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "TextFile.txt");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "TextFile.txt";
                    }
                    else if (Ext == ".xlsx")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                    }
                    else if (Ext == ".xls")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xls");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xls";
                    }
                    else if (Ext == ".doc")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.doc");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.doc";
                    }
                    else if (Ext == ".docx")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "DocReport.docx");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "DocReport.docx";
                    }
                    else if (Ext == ".png")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "img.png");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.png";
                    }
                    else if (Ext == ".jpeg")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "img.jpeg");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.jpeg";
                    }
                    else if (Ext == ".gif")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "img.gif");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "img.gif";
                    }
                    else if (Ext == ".zip")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "file.zip");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "file.zip";
                    }
                    else if (Ext == ".ppt")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "File.ppt");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.ppt";
                    }
                    else if (Ext == ".pptx")
                    {
                        httpResponseMessage.Content.Headers.Add("x-filename", "File.pptx");
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation");
                        httpResponseMessage.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment");
                        httpResponseMessage.Content.Headers.ContentDisposition.FileName = "File.pptx";
                    }
                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                }

            }
            else
            {
                var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri(baseUrl + "/login.aspx");
                return response;
            }
            return httpResponseMessage;
        }

    }
}
