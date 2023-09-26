using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UPSIGroupVendor")]
    public class UPSIGroupVendorController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetVendorList")]
        [HttpPost]
        public UPSIVendorResponce GetVendorList()
        {
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();

            }

            UPSIVendor objUMvendor = new JavaScriptSerializer().Deserialize<UPSIVendor>(input);
            objUMvendor.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMvendor.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

            if (objUMvendor.ValidateInput())
            {
                UPSIVendorRequest UgRequest = new UPSIVendorRequest(objUMvendor);
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes = UgRequest.ListUPSIVendor();
                return GroupRes;
            }
            else
            {
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("SaveUPSIVendor")]
        [HttpPost]
        public UPSIVendorResponce SaveUPSIVendor()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                UPSIVendorResponce objResponse = new UPSIVendorResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            String sSaveAs = String.Empty;
            String input1 = HttpContext.Current.Request.Form["Object"];
            UPSIVendor objUMvendor = new JavaScriptSerializer().Deserialize<UPSIVendor>(input1);
            if (HttpContext.Current.Request.Files.Count > 0)
            {

                HttpFileCollection files = HttpContext.Current.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    String ext = Path.GetExtension(file.FileName);
                    String name = Path.GetFileNameWithoutExtension(file.FileName);
                    string fname;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                    }
                    else
                    {
                        fname = name + "VENDOR_DOC_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                    }
                    sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"), fname);
                    file.SaveAs(sSaveAs);
                    objUMvendor.fileName = fname;
                }

            }
            else
            {
                objUMvendor.fileName = String.Empty;
            }

            objUMvendor.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMvendor.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            objUMvendor.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (objUMvendor.ValidateInput())
            {
                UPSIVendorRequest UgRequest = new UPSIVendorRequest(objUMvendor);
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes = UgRequest.SaveUPSIVendor();
                return GroupRes;
            }
            else
            {
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("ListUPSIVendor_ById")]
        [HttpPost]
        public UPSIVendorResponce ListUPSIVendor_ById()
        {
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();
            }

            UPSIVendor objUMvendor = new JavaScriptSerializer().Deserialize<UPSIVendor>(input);
            objUMvendor.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMvendor.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            objUMvendor.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (objUMvendor.ValidateInput())
            {
                UPSIVendorRequest UgRequest = new UPSIVendorRequest(objUMvendor);
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes = UgRequest.ListUPSIVendor_ById();
                return GroupRes;
            }
            else
            {
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
        [Route("DeleteUPSIVendor_ById")]
        [HttpPost]
        public UPSIVendorResponce DeleteUPSIVendor_ById()
        {
            string input;
            using (System.IO.StreamReader rd = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                input = rd.ReadToEnd();
            }

            UPSIVendor objUMvendor = new JavaScriptSerializer().Deserialize<UPSIVendor>(input);
            objUMvendor.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            objUMvendor.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            objUMvendor.CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
            if (objUMvendor.ValidateInput())
            {
                UPSIVendorRequest UgRequest = new UPSIVendorRequest(objUMvendor);
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes = UgRequest.DeleteUPSIVendor_ById();
                return GroupRes;
            }
            else
            {
                UPSIVendorResponce GroupRes = new UPSIVendorResponce();
                GroupRes.StatusFl = false;
                GroupRes.Msg = sXSSErrMsg;
                return GroupRes;
            }
        }
    }
}