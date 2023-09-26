using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UPSIMembersGreoup")]
    public class UPSIMembersGreoupController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetVendorList")]
        [HttpPost]
        public UPSIMembersGroupResponce GetVendorList()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;

            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();

            }

            UPSIMembersGroup objUMGroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            objUMGroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMGroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

            if (objUMGroup.ValidateInput())
            {
                UPSIMembersGroupRequest UgRequest = new UPSIMembersGroupRequest(objUMGroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = UgRequest.GetVendorList();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("GetUPSITypeList")]
        [HttpPost]
        public UPSIMembersGroupResponce GetUPSITypeList()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;

            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();

            }

            UPSIMembersGroup objUMGroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            objUMGroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMGroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

            if (objUMGroup.ValidateInput())
            {
                UPSIMembersGroupRequest UgRequest = new UPSIMembersGroupRequest(objUMGroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = UgRequest.GetUPSITypeList();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("GetUserList")]
        [HttpPost]
        public UPSIMembersGroupResponce GetUserList()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;

            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();

            }

            UPSIMembersGroup objUMGroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            objUMGroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMGroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            objUMGroup.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
            if (objUMGroup.ValidateInput())
            {
                UPSIMembersGroupRequest UgRequest = new UPSIMembersGroupRequest(objUMGroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = UgRequest.GetDesignatedUserList();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("SaveUPSI")]
        [HttpPost]
        public UPSIMembersGroupResponce SaveUPSI()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string sessionTokenKey = Convert.ToString(HttpContext.Current.Session["TokenKey"]);
            string headerTokenKey = HttpContext.Current.Request.Headers.GetValues("TokenKeyH").FirstOrDefault();

            if (sessionTokenKey == headerTokenKey)
            {
                string input;
                using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = rd.ReadToEnd();
                }
                UPSIMembersGroup objUMGroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
                objUMGroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                objUMGroup.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                objUMGroup.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                objUMGroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (objUMGroup.ValidateInput())
                {

                    UPSIMembersGroupRequest UgRequest = new UPSIMembersGroupRequest(objUMGroup);
                    UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                    GroupRes = UgRequest.SaveUPSI();
                    return GroupRes;
                }
                else
                {
                    UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                    GroupRes.StatusFl = false;
                    GroupRes.Msg = sXSSErrMsg;
                    return GroupRes;
                }
            }
            else
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "Unauthorized access!";
                return objResponse;
            }
        }
        [Route("GetUPSIGroupList")]
        [HttpPost]
        public UPSIMembersGroupResponce GetUPSIGroupList()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;

            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();
            }

            UPSIMembersGroup upsigroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            upsigroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsigroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsigroup.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
            upsigroup.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (upsigroup.ValidateInput())
            {
                UPSIMembersGroupRequest rques = new UPSIMembersGroupRequest(upsigroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = rques.GetUPSIGroupList();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("GetUPSIGroupListByID")]
        [HttpPost]
        public UPSIMembersGroupResponce GetUPSIGroupListByID()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;

            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();
            }

            UPSIMembersGroup upsigroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            upsigroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsigroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsigroup.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (upsigroup.ValidateInput())
            {
                UPSIMembersGroupRequest rques = new UPSIMembersGroupRequest(upsigroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = rques.GetUPSIGroupListByID();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("DeleteUPSIGroup")]
        [HttpPost]
        public UPSIMembersGroupResponce DeleteUPSIGroup()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }

            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();
            }

            UPSIMembersGroup upsigroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            upsigroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsigroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsigroup.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (upsigroup.ValidateInput())
            {
                UPSIMembersGroupRequest rques = new UPSIMembersGroupRequest(upsigroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = rques.DeleteUPSIGroupListByID();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("GetNonDesignatedUPSIMember")]
        [HttpPost]
        public UPSIMembersGroupResponce GetNonDesignatedUPSIMember()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIMembersGroupResponce objResponse = new UPSIMembersGroupResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            string input;

            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();

            }

            UPSIMembersGroup upsigroup = new JavaScriptSerializer().Deserialize<UPSIMembersGroup>(input);
            upsigroup.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            upsigroup.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            upsigroup.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

            if (upsigroup.ValidateInput())
            {
                UPSIMembersGroupRequest rques = new UPSIMembersGroupRequest(upsigroup);
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes = rques.GetNonDesignatedUPSIMember();
                return GroupRes;
            }
            else
            {
                UPSIMembersGroupResponce GroupRes = new UPSIMembersGroupResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
    }
}