using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class UPSIGroupController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetUPSITypeList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSITypeResponse GetUPSITypeList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSITypeResponse upsiTypRes;
                UPSIType upsiTyp = new UPSIType();
                upsiTyp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiTyp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!upsiTyp.ValidateInput())
                {
                    upsiTypRes = new UPSITypeResponse();
                    upsiTypRes.StatusFl = false;
                    upsiTypRes.Msg = sXSSErrMsg;
                    return upsiTypRes;
                }
                UPSITypeRequest upsiRequest = new UPSITypeRequest(upsiTyp);
                upsiTypRes = upsiRequest.GetUPSITypeList();
                return upsiTypRes;
            }
            catch (Exception ex)
            {
                UPSITypeResponse objResponse = new UPSITypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUPSIType")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSITypeResponse SaveUPSIType()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
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
                    UPSIType upsiTyp = new JavaScriptSerializer().Deserialize<UPSIType>(input);
                    upsiTyp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiTyp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiTyp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiTyp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSITypeResponse upsiTypRes = new UPSITypeResponse();
                    if (!upsiTyp.ValidateInput())
                    {
                        upsiTypRes = new UPSITypeResponse();
                        upsiTypRes.StatusFl = false;
                        upsiTypRes.Msg = sXSSErrMsg;
                        return upsiTypRes;
                    }
                    UPSITypeRequest grpRequest = new UPSITypeRequest(upsiTyp);
                    upsiTypRes = grpRequest.AddUpdateUPSIType();
                    return upsiTypRes;
                }
                else
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSITypeResponse objResponse = new UPSITypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIType")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSIType()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSIGroupResponse upsiGrpRes;
                UPSIGrp upsiGrp = new UPSIGrp();
                upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!upsiGrp.ValidateInput())
                {
                    upsiGrpRes = new UPSIGroupResponse();
                    upsiGrpRes.StatusFl = false;
                    upsiGrpRes.Msg = sXSSErrMsg;
                    return upsiGrpRes;
                }
                UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                upsiGrpRes = grpRequest.GetUPSIType();
                return upsiGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIGroupList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSIGroupList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSIGroupResponse upsiGrpRes;
                UPSIGrp upsiGrp = new UPSIGrp();
                upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                if (!upsiGrp.ValidateInput())
                {
                    upsiGrpRes = new UPSIGroupResponse();
                    upsiGrpRes.StatusFl = false;
                    upsiGrpRes.Msg = sXSSErrMsg;
                    return upsiGrpRes;
                }
                UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                upsiGrpRes = grpRequest.GetUPSIGroups();
                return upsiGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUPSIGroup")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse SaveUPSIGroup()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.SaveUPSIGroup();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIConnectedPersons")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSIConnectedPersons()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                if (!upsiGrp.ValidateInput())
                {
                    upsiGrpRes = new UPSIGroupResponse();
                    upsiGrpRes.StatusFl = false;
                    upsiGrpRes.Msg = sXSSErrMsg;
                    return upsiGrpRes;
                }
                UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                upsiGrpRes = grpRequest.GetUPSIConnectedPersons();
                return upsiGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIDesignatedPersons")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSIDesignatedPersons()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                if (!upsiGrp.ValidateInput())
                {
                    upsiGrpRes = new UPSIGroupResponse();
                    upsiGrpRes.StatusFl = false;
                    upsiGrpRes.Msg = sXSSErrMsg;
                    return upsiGrpRes;
                }
                UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                upsiGrpRes = grpRequest.GetUPSIDesignatedPersons();
                return upsiGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUPSIConnectedPersons")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse SaveUPSIConnectedPersons()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.SaveUPSIConnectedPersons();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUPSIGroupCommunication")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse SaveUPSIGroupCommunication()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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

                    string sUPSIGrpId = HttpContext.Current.Request.Form["UPSIGrpId"];
                    string sSharedFrom = HttpContext.Current.Request.Form["SharedFrom"];
                    string sSharedBy = HttpContext.Current.Request.Form["SharedBy"];
                    string ConnectedPersons = HttpContext.Current.Request.Form["ConnectedPersons"];
                    string DesignatedPersons = HttpContext.Current.Request.Form["DesignatedPersons"];
                    string UPSISharedOn = HttpContext.Current.Request.Form["UPSISharedOn"];
                    string UPSISharedAt = HttpContext.Current.Request.Form["UPSISharedAt"];
                    string UPSISharingMode = HttpContext.Current.Request.Form["UPSISharingMode"];
                    string UPSIRemarks = HttpContext.Current.Request.Form["UPSIRemarks"];
                    string sSaveAs = HttpContext.Current.Request.Form["sSaveAs"];

                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        HttpFileCollection files = HttpContext.Current.Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFile file = files[i];
                            string fname;
                            if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            string sSaveAs1 = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UPSI/"), sSaveAs);
                            file.SaveAs(sSaveAs1);
                        }
                    }

                    UPSIGrp upsiGrp = new UPSIGrp();
                    upsiGrp.GrpId = Convert.ToInt32(sUPSIGrpId);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    upsiGrp.ValidFrom = UPSISharedOn;
                    upsiGrp.ValidTo = UPSISharedAt;
                    upsiGrp.GrpDesc = UPSIRemarks;
                    upsiGrp.GrpNm = UPSISharingMode;
                    upsiGrp.TypNm = sSaveAs;
                    upsiGrp.SharedBy = sSharedBy;
                    upsiGrp.SharedFrm = sSharedFrom;

                    List<ConnectedPerson> lstPersons = new List<ConnectedPerson>();
                    string[] CPArray = ConnectedPersons.Split(new char[] { ',' });
                    string[] DPArray = DesignatedPersons.Split(new char[] { ',' });

                    if (CPArray.Length > 0)
                    {
                        foreach (string str in CPArray)
                        {
                            if (str != null && str != "null")
                            {
                                ConnectedPerson cp = new ConnectedPerson();
                                cp.CPEmail = str;
                                cp.IdentificationTyp = "CP";
                                cp.IdentificationId = sSaveAs;
                                lstPersons.Add(cp);
                            }
                        }
                    }
                    if (DPArray.Length > 0)
                    {
                        foreach (string str in DPArray)
                        {
                            if (str != null && str != "null")
                            {
                                ConnectedPerson cp = new ConnectedPerson();
                                cp.CPEmail = str;
                                cp.IdentificationTyp = "DP";
                                cp.IdentificationId = sSaveAs;
                                lstPersons.Add(cp);
                            }
                        }
                    }
                    upsiGrp.ConnectedPersons = lstPersons;
                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.SaveUPSIGroupCommunication();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSITaskById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSITaskById(string TaskId)
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (sessionTokenKey == headerTokenKey)
                {
                    UPSITask upsiTask = new UPSITask();
                    upsiTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiTask.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiTask.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiTask.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    upsiTask.TaskId = Convert.ToInt32(TaskId);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiTask.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest();
                    upsiGrpRes = grpRequest.GetTaskDetails(upsiTask);
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UpdateUPSITask")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse UpdateUPSITask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.UpdateUPSITask();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIReport")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIRptResponse GetUPSIReport()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIRptResponse objResponse = new UPSIRptResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
                string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();
                if (sessionTokenKey == headerTokenKey)
                {
                    string sUPSIGrpId = HttpContext.Current.Request.Form["UPSIGrpId"];
                    string sUserId = HttpContext.Current.Request.Form["UserId"];
                    string sFrmDt = HttpContext.Current.Request.Form["from_date"];
                    string sToDt = HttpContext.Current.Request.Form["to_date"];

                    UPSIRptResponse upsiGrpRes = new UPSIRptResponse();
                    UPSIRptRequest grpRequest = new UPSIRptRequest();
                    upsiGrpRes = grpRequest.GetUPSIReport(
                        sUPSIGrpId, sUserId, sFrmDt, sToDt,
                        Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]),
                        Convert.ToString(HttpContext.Current.Session["CompanyId"]),
                        Convert.ToString(HttpContext.Current.Session["EmployeeId"]),
                        Convert.ToString(HttpContext.Current.Session["AdminDb"])
                    );
                    return upsiGrpRes;
                }
                else
                {
                    UPSIRptResponse objResponse = new UPSIRptResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIRptResponse objResponse = new UPSIRptResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUPSIGroupMembers")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse SaveUPSIGroupMembers()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.SaveUPSIGroupMembers();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteUPSIGroupMember")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse DeleteUPSIGroupMember()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.DeleteUPSIGroupMember();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIGroupMember")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSIGroupMember()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);

                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.GetUPSIGroupMember();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUPSIGrpAuditLog")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetUPSIGrpAuditLog()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);

                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.GetUPSIGrpAuditLog();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        public UPSIGroupResponse DiscardUPSITask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
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
                    UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                    upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                    upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                    UPSIGroupResponse upsiGrpRes = new UPSIGroupResponse();
                    if (!upsiGrp.ValidateInput())
                    {
                        upsiGrpRes = new UPSIGroupResponse();
                        upsiGrpRes.StatusFl = false;
                        upsiGrpRes.Msg = sXSSErrMsg;
                        return upsiGrpRes;
                    }
                    UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                    upsiGrpRes = grpRequest.DiscardUPSITask();
                    return upsiGrpRes;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllUPSIGroups")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse GetAllUPSIGroups()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSIGroupResponse upsiGrpRes;
                UPSIGrp upsiGrp = new UPSIGrp();
                upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                if (!upsiGrp.ValidateInput())
                {
                    upsiGrpRes = new UPSIGroupResponse();
                    upsiGrpRes.StatusFl = false;
                    upsiGrpRes.Msg = sXSSErrMsg;
                    return upsiGrpRes;
                }
                UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                upsiGrpRes = grpRequest.GetAllUPSIGroups();
                return upsiGrpRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UploadCommunication")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse UploadCommunication()
        {
            try
            {
                string sDtFormat = ConfigurationManager.AppSettings["UniversalDateFormat"].ToString();
                string sTmFormat = ConfigurationManager.AppSettings["UPSITimeFormat"].ToString();
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                String sSaveAs = HttpContext.Current.Request.Form["sSaveAs"];
                String sGrpId = HttpContext.Current.Request.Form["GrpId"];
                String sSaveAs1="";
                string UserLogin = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                string sAdminDb = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "UPSICommunication_";
                        string fname;

                        if (sNm.Contains("%00"))
                        {
                            UPSIGroupResponse objResponseX = new UPSIGroupResponse();
                            objResponseX.StatusFl = false;
                            objResponseX.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                            return objResponseX;
                        }
                        if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                        {
                            fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                            sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UPSI/"), fname);
                            file.SaveAs(sSaveAs);
                        }
                    }
                    if (!String.IsNullOrEmpty(sSaveAs))
                    {
                        string extension = Path.GetExtension(sSaveAs);
                        string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sSaveAs + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sSaveAs + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";

                        string conString = "";

                        switch (extension)
                        {
                            case ".xls":
                                conString = string.Format(Excel03ConString, sSaveAs, "YES");
                                break;
                            case ".xlsx":
                                conString = string.Format(Excel07ConString, sSaveAs, "YES");
                                break;
                        }

                        using (OleDbConnection con = new OleDbConnection(conString))
                        {
                            con.Open();
                            DataTable dtSheet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                            if (dtSheet.Rows.Count > 0)
                            {
                                foreach (DataRow drSheet in dtSheet.Rows)
                                {
                                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                                    {
                                        string sSheetNm = drSheet["TABLE_NAME"].ToString();
                                        if (!sSheetNm.Contains("FilterDatabase"))
                                        {
                                            String sqlQry = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionStringIT"], true);
                                            string _sql = "INSERT INTO PROCS_INSIDER_UPSI_COMMUNICATION_XLS(GRP_ID,XLS_TEMPLATE,UPLOADED_BY,UPLOADED_ON) VALUES(" +
                                                sGrpId + ",'" + sSaveAs1 + "','" + UserLogin + "',GETDATE());"+
                                                "SELECT IDENT_CURRENT('PROCS_INSIDER_UPSI_COMMUNICATION_XLS');";
                                            Object obj = SQLHelper.ExecuteScalar(sqlQry, CommandType.Text, _sql);
                                            Int64 xlsId = Convert.ToInt64(obj);

                                            string xlsQry = "SELECT " + xlsId.ToString() + " AS XLS_ID,";
                                            xlsQry += "[Shared By] AS SHARED_BY,[Shared With] AS SHARED_WITH,[PAN of Shared With Person] AS SHARED_WITH_PAN,[Firm] AS FIRM," +
                                                "[UPSI Shared on Date] AS SHARED_DATE,[Mode of Sharing] AS SHARED_MODE,[Remarks] AS REMARKS FROM [Sheet1$]";

                                            OleDbCommand oconn = new OleDbCommand(xlsQry, con);
                                            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                            DataTable dt = new DataTable();

                                            adp.Fill(dt);
                                            string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                                            {
                                                bulkcopy.BulkCopyTimeout = 0;
                                                bulkcopy.DestinationTableName = "PROCS_INSIDER_UPSI_COMMUNICATION_TEMP";

                                                SqlBulkCopyColumnMapping mapHdrId = new SqlBulkCopyColumnMapping("XLS_ID", "XLS_ID");
                                                bulkcopy.ColumnMappings.Add(mapHdrId);

                                                SqlBulkCopyColumnMapping mapSharedBy = new SqlBulkCopyColumnMapping("SHARED_BY", "SHARED_BY");
                                                bulkcopy.ColumnMappings.Add(mapSharedBy);

                                                SqlBulkCopyColumnMapping mapSharedWith = new SqlBulkCopyColumnMapping("SHARED_WITH", "SHARED_WITH");
                                                bulkcopy.ColumnMappings.Add(mapSharedWith);

                                                SqlBulkCopyColumnMapping mapSharedWithPAN = new SqlBulkCopyColumnMapping("SHARED_WITH_PAN", "SHARED_WITH_PAN");
                                                bulkcopy.ColumnMappings.Add(mapSharedWithPAN);

                                                SqlBulkCopyColumnMapping mapFirm = new SqlBulkCopyColumnMapping("FIRM", "FIRM");
                                                bulkcopy.ColumnMappings.Add(mapFirm);

                                                SqlBulkCopyColumnMapping mapSharedDt = new SqlBulkCopyColumnMapping("SHARED_DATE", "SHARED_DATE");
                                                bulkcopy.ColumnMappings.Add(mapSharedDt);

                                                //SqlBulkCopyColumnMapping mapSharedTm = new SqlBulkCopyColumnMapping("SHARED_TIME", "SHARED_TIME");
                                                //bulkcopy.ColumnMappings.Add(mapSharedTm);

                                                SqlBulkCopyColumnMapping mapSharedMode = new SqlBulkCopyColumnMapping("SHARED_MODE", "SHARED_MODE");
                                                bulkcopy.ColumnMappings.Add(mapSharedMode);

                                                SqlBulkCopyColumnMapping mapRemarks = new SqlBulkCopyColumnMapping("REMARKS", "REMARKS");
                                                bulkcopy.ColumnMappings.Add(mapRemarks);

                                                bulkcopy.WriteToServer(dt);
                                                bulkcopy.Close();
                                            }
                                            
                                            using (SqlConnection sCon = new SqlConnection(str))
                                            {
                                                SqlCommand sCmd = new SqlCommand();
                                                sCmd.Connection = sCon;
                                                sCmd.CommandType = CommandType.Text;
                                                sCmd.CommandText = "SELECT dbo.ChkValidEmail(SHARED_BY) AS E_SHARED_BY," +
                                                    "dbo.ChkValidEmail(SHARED_WITH) AS E_SHARED_WITH," +
                                                    "dbo.CheckPAN(SHARED_WITH_PAN) AS E_SHARED_WITH_PAN,* " +
                                                    "FROM PROCS_INSIDER_UPSI_COMMUNICATION_TEMP " +
                                                    "WHERE XLS_ID=" + xlsId + " AND(dbo.ChkValidEmail(SHARED_BY)=0 " +
                                                    "OR dbo.ChkValidEmail(SHARED_WITH)=0 " +
                                                    "OR dbo.CheckPAN(SHARED_WITH_PAN)=0)";

                                                DataSet dsException = new DataSet();
                                                SqlDataAdapter daException = new SqlDataAdapter(sCmd);
                                                daException.Fill(dsException);

                                                DataTable dtException = new DataTable();
                                                dtException = dsException.Tables[0];
                                                if (dtException.Rows.Count > 0)
                                                {
                                                    StringBuilder sb = new StringBuilder();
                                                    sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                                    sb.Append("<tr>");
                                                    sb.Append("<th>Shared By</th>");
                                                    sb.Append("<th>Shared With</th>");
                                                    sb.Append("<th>Shared with PAN</th>");
                                                    sb.Append("<th>Firm</th>");
                                                    sb.Append("<th>Shared Date/Time</th>");
                                                    sb.Append("<th>Shared Mode</th>");
                                                    sb.Append("<th>Remarks</th>");
                                                    sb.Append("</tr>");

                                                    foreach (DataRow drException in dtException.Rows)
                                                    {
                                                        sb.Append("<tr>");
                                                        if (Convert.ToInt32(drException["E_SHARED_BY"]) == 0)
                                                        {
                                                            sb.Append("<td style='color:red;'>" + Convert.ToString(drException["SHARED_BY"]) + "</td>");
                                                        }
                                                        else
                                                        {
                                                            sb.Append("<td>" + Convert.ToString(drException["SHARED_BY"]) + "</td>");
                                                        }
                                                        if (Convert.ToInt32(drException["E_SHARED_WITH"]) == 0)
                                                        {
                                                            sb.Append("<td style='color:red;'>" + Convert.ToString(drException["SHARED_WITH"]) + "</td>");
                                                        }
                                                        else
                                                        {
                                                            sb.Append("<td>" + Convert.ToString(drException["SHARED_WITH"]) + "</td>");
                                                        }
                                                        if (Convert.ToInt32(drException["E_SHARED_WITH_PAN"]) == 0)
                                                        {
                                                            sb.Append("<td style='color:red;'>" + Convert.ToString(drException["SHARED_WITH_PAN"]) + "</td>");
                                                        }
                                                        else
                                                        {
                                                            sb.Append("<td>" + Convert.ToString(drException["SHARED_WITH_PAN"]) + "</td>");
                                                        }
                                                        sb.Append("<td>" + Convert.ToString(drException["FIRM"]) + "</td>");
                                                        sb.Append("<td>" + Convert.ToDateTime(drException["SHARED_DATE"]).ToString(sDtFormat + " HH:mm") + "</td>");
                                                        sb.Append("<td>" + Convert.ToString(drException["SHARED_MODE"]) + "</td>");
                                                        sb.Append("<td>" + Convert.ToString(drException["REMARKS"]) + "</td>");
                                                        sb.Append("</tr>");
                                                    }
                                                    sb.Append("</table>");
                                                    objResponse.StatusFl = false;
                                                    objResponse.Msg = "Exception";
                                                    objResponse.sException = sb.ToString();
                                                }
                                                else
                                                {
                                                    sCmd.CommandType = CommandType.StoredProcedure;
                                                    sCmd.CommandText = "SP_INSIDER_UPSI_GROUP_COMMUNICATION";
                                                    sCmd.Parameters.AddWithValue("@GrpId", sGrpId);
                                                    sCmd.Parameters.AddWithValue("@UploadeBy", UserLogin);
                                                    sCmd.Parameters.AddWithValue("@XlsId", xlsId);
                                                    sCmd.Parameters.AddWithValue("@AdminDb", sAdminDb);

                                                    DataSet dsPersons = new DataSet();
                                                    SqlDataAdapter daPersons = new SqlDataAdapter(sCmd);
                                                    daPersons.Fill(dsPersons);

                                                    DataTable dtPersons = new DataTable();
                                                    dtPersons = dsPersons.Tables[0];

                                                    objResponse.StatusFl = true;
                                                    objResponse.Msg = "Data Saved successfully";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            con.Close();
                        }
                    }
                    return objResponse;
                }
                else
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetModeofCommunication")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSITypeResponse GetModeofCommunication()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSITypeResponse objResponse = new UPSITypeResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSITypeResponse upsiTypRes;
                UPSIType upsiTyp = new UPSIType();
                upsiTyp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiTyp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!upsiTyp.ValidateInput())
                {
                    upsiTypRes = new UPSITypeResponse();
                    upsiTypRes.StatusFl = false;
                    upsiTypRes.Msg = sXSSErrMsg;
                    return upsiTypRes;
                }
                UPSITypeRequest upsiRequest = new UPSITypeRequest(upsiTyp);
                upsiTypRes = upsiRequest.GetModeofCommunication();
                return upsiTypRes;
            }
            catch (Exception ex)
            {
                UPSITypeResponse objResponse = new UPSITypeResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("AddUPSITaskDP")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse AddUPSITaskDP()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIGroupResponse objResponse = new UPSIGroupResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UPSIGroupResponse upsiRes;
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                UPSIGrp upsiGrp = new JavaScriptSerializer().Deserialize<UPSIGrp>(input);
                upsiGrp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsiGrp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                upsiGrp.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsiGrp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);

                UPSIGroupRequest grpRequest = new UPSIGroupRequest(upsiGrp);
                upsiRes = grpRequest.AddUPSITaskDP();
                return upsiRes;
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteUPSICp")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIGroupResponse DeleteUPSICp()
        {
            try
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                if (HttpContext.Current.Session.Count == 0)
                {
                    
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                String sCPEmail = HttpContext.Current.Request.Form["CPEmail"];
                String sGrpId = HttpContext.Current.Request.Form["UPSIGrpId"];

                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                using (SqlConnection sCon = new SqlConnection(str))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = "UPDATE PROCS_INSIDER_UPSI_GROUP_CP SET CP_STATUS='Inactive' " +
                        "WHERE GRP_ID=" + sGrpId + " AND CP_EMAIL='" + sCPEmail + "'";
                    sCon.Open();
                    sCmd.ExecuteNonQuery();
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Success";
                }
                return objResponse;
                
            }
            catch (Exception ex)
            {
                UPSIGroupResponse objResponse = new UPSIGroupResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}