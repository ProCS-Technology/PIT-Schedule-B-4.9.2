using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/ManualAdminUser")]
    public class ManualAdminUserController : ApiController
    {
        [Route("UploadUserDoc")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public ManualAdminUserResponce UploadUserDoc()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ManualAdminUserResponce objResponse = new ManualAdminUserResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                String sSaveAs = String.Empty;
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
                        fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                    }

                    String sSaveAs_Code_of_counduct = String.Empty;
                    sSaveAs_Code_of_counduct = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/"), "User-Manual.pdf");
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/"));

                    }
                    file.SaveAs(sSaveAs_Code_of_counduct);
                }
                ManualAdminUserResponce objResponse = new ManualAdminUserResponce();
                objResponse.StatusFl = true;
                objResponse.Msg = "File Uploded successfully!";
                return objResponse;
            }
            else
            {
                ManualAdminUserResponce objResponse = new ManualAdminUserResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
        }
        [Route("UploadAdminDoc")]
        [HttpPost]
        public ManualAdminUserResponce UploadAdminDoc()
        {
            if (HttpContext.Current.Session.Count == 0)
            {
                ManualAdminUserResponce objResponse = new ManualAdminUserResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                String sSaveAs = String.Empty;
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
                        fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                    }

                    String sSaveAs_Code_of_counduct = String.Empty;
                    sSaveAs_Code_of_counduct = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/"), "Admin-Manual.pdf");
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/"));
                    }
                    file.SaveAs(sSaveAs_Code_of_counduct);
                }
                ManualAdminUserResponce objResponse = new ManualAdminUserResponce();
                objResponse.StatusFl = true;
                objResponse.Msg = "File Uploded successfully!";
                return objResponse;
            }
            else
            {
                ManualAdminUserResponce objResponse = new ManualAdminUserResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "SessionExpired";
                return objResponse;
            }
        }
    }
}