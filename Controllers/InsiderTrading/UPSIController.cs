using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UPSI")]
    public class UPSIController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetListUPSI")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse GetListUPSI()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.list_upsi();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("editUPSI")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse editUPSI()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.edit_upsi();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("delete_upsi")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse delete_upsi()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.delete_upsi();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("save_upsi")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse save_upsi()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsi1.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.SaveUPSI();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUserForUPSI")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse GetUserForUPSI()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.list_user();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("HistoryUPSIGroup")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse HistoryUPSIGroup()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.HistoryUPSIGroup();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("send_mail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse send_mail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsi1.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.send_mail();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveUPSIGroupRemarks")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse SaveUPSIGroupRemarks()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String sSaveAs = String.Empty;
                String input1 = HttpContext.Current.Request.Form["Object"];
                UPSI upsi1 = new JavaScriptSerializer().Deserialize<UPSI>(input1);
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
                            fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"), fname);
                        file.SaveAs(sSaveAs);
                        upsi1.upsiAttachment = fname;
                    }
                }
                else
                {
                    upsi1.upsiAttachment = String.Empty;
                }
                upsi1.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                upsi1.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                upsi1.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);

                if (upsi1.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(upsi1);
                    UPSIResponse cRes = cReq.SaveUPSIGroupRemarks();
                    return cRes;
                }
                else
                {
                    UPSIResponse cRes = new UPSIResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetUsersForUPSI")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UserResponse GetUsersForUPSI()
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
                string input1;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input1 = sr.ReadToEnd();
                }
                User user = new JavaScriptSerializer().Deserialize<User>(input1);
                user.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                user.LOGIN_ID = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                user.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);

                if (user.ValidateInput())
                {
                    UPSIRequest cReq = new UPSIRequest(user);
                    UserResponse cRes = cReq.GetUsersForUPSI();
                    return cRes;
                }
                else
                {
                    UserResponse cRes = new UserResponse();
                    cRes.StatusFl = false;
                    cRes.Msg = sXSSErrMsg;
                    return cRes;
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
        [Route("SaveUPSITemplate")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UPSI APIs" })]
        public UPSIResponse SaveUPSITemplate()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                String sSaveAs = String.Empty;
                String zipFileSaveAs = String.Empty;
                String zipFileNameToAppend = String.Empty;
                string fname = String.Empty;
                String newZipFileName = String.Empty;
                int hdr_id = 0;

                UPSI rel = new UPSI();
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                rel.created_by = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.COMPANY_ID = Convert.ToString(HttpContext.Current.Session["CompanyId"]);
                rel.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!rel.ValidateInput())
                {
                    UPSIResponse objResponse = new UPSIResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                UPSIRequest pReq = new UPSIRequest(rel);

                var uploadDir = "/InsiderTrading/UPSI/";
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadDir))))
                {
                    Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadDir)));
                }

                HttpFileCollection files = HttpContext.Current.Request.Files;
                if (files.Count > 1)
                {
                    HttpPostedFile zipFile = files[1];
                    zipFileNameToAppend = Path.GetFileNameWithoutExtension(zipFile.FileName) + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture);
                    newZipFileName = zipFileNameToAppend + "." + zipFile.FileName.Split('.')[1];
                    zipFileSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadDir), newZipFileName);
                    zipFile.SaveAs(zipFileSaveAs);
                    Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadDir + "/" + zipFileNameToAppend + "/")));

                    using (ZipArchive archive = ZipFile.Open(zipFileSaveAs, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            entry.ExtractToFile(Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadDir + "/" + zipFileNameToAppend + "/"), entry.Name), true);
                        }
                    }
                    File.Delete(zipFileSaveAs);
                }

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        String ext = Path.GetExtension(file.FileName);
                        String name = Path.GetFileNameWithoutExtension(file.FileName);

                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1] + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        if (i == 0)
                        {
                            sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~" + uploadDir), fname);
                            file.SaveAs(sSaveAs);
                            rel.UPSITemplate = fname;
                        }
                        else
                        {
                            // sSaveAs1 = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UPSI/"), fname);
                            //file.SaveAs(sSaveAs1);
                            //rel.fileNameESOP = fname;
                        }
                    }
                }
                else
                {
                    rel.UPSITemplate = String.Empty;
                    //  rel.fileNameESOP = String.Empty;
                }
                if (!String.IsNullOrEmpty(sSaveAs))
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        SqlParameter[] parameters = new SqlParameter[5];
                        parameters[0] = new SqlParameter("@Mode", "INSERT_HDR");
                        parameters[1] = new SqlParameter("@EXCFILE_NAME", fname);
                        parameters[2] = new SqlParameter("@ZIPFILE_NAME", newZipFileName);
                        parameters[3] = new SqlParameter("@USERNAME", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));
                        parameters[4] = new SqlParameter("@OUTPUTHDRID", SqlDbType.Int);
                        parameters[4].Direction = ParameterDirection.Output;

                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_TEMPLATE_UPLOAD", rel.MODULE_DATABASE, parameters);
                        hdr_id = Convert.ToInt32(parameters[4].Value);
                    }

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

                        OleDbCommand oconn = new OleDbCommand(
                            "SELECT [Nature of UPSI shared],[Names of persons who have shared the UPSI],[Names of persons with whom UPSI is shared]," +
                            "[PAN or any other identifier authorized by law#],[Time & Date when UPSI shared],[Mode of Sharing UPSI],[Document/Attachment]," +
                            "[Remarks],[Date when information ceases to be UPSI],'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AS CREATED_ON," +
                            "'" + rel.created_by + "' AS CREATED_BY,'" + rel.COMPANY_ID + "' AS COMPANY_ID,'"+ hdr_id+"' AS HDR_ID FROM [UPSI$]", con
                        );
                        OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        DataView dv = new DataView(dt);
                        DataTable Seldt = new DataTable();
                        Seldt = dv.ToTable();
                        
                        string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                        {
                            bulkcopy.BulkCopyTimeout = 0;
                            bulkcopy.DestinationTableName = "PROCS_INSIDER_UPSI_TEMPLATE_UPLOAD";

                            SqlBulkCopyColumnMapping mapUPSINature = new SqlBulkCopyColumnMapping("Nature of UPSI shared", "NATURE_OF_UPSI");
                            bulkcopy.ColumnMappings.Add(mapUPSINature);

                            SqlBulkCopyColumnMapping mapWhoShared = new SqlBulkCopyColumnMapping("Names of persons who have shared the UPSI", "WHO_SHARED");
                            bulkcopy.ColumnMappings.Add(mapWhoShared);

                            SqlBulkCopyColumnMapping mapWithWhomShared = new SqlBulkCopyColumnMapping("Names of persons with whom UPSI is shared", "WITH_WHOM_SHARED");
                            bulkcopy.ColumnMappings.Add(mapWithWhomShared);

                            SqlBulkCopyColumnMapping mapPAN = new SqlBulkCopyColumnMapping("PAN or any other identifier authorized by law#", "PAN_OR_OTHER_IDENTIFICATION");
                            bulkcopy.ColumnMappings.Add(mapPAN);

                            SqlBulkCopyColumnMapping mapSharedOn = new SqlBulkCopyColumnMapping("Time & Date when UPSI shared", "SHARED_ON");
                            bulkcopy.ColumnMappings.Add(mapSharedOn);

                            SqlBulkCopyColumnMapping mapModeOfSharing = new SqlBulkCopyColumnMapping("Mode of Sharing UPSI", "MODE_OF_SHARING");
                            bulkcopy.ColumnMappings.Add(mapModeOfSharing);

                            SqlBulkCopyColumnMapping mapAttachment = new SqlBulkCopyColumnMapping("Document/Attachment", "ATTACHMENT");
                            bulkcopy.ColumnMappings.Add(mapAttachment);

                            SqlBulkCopyColumnMapping mapCreatedOn = new SqlBulkCopyColumnMapping("CREATED_ON", "CREATED_ON");
                            bulkcopy.ColumnMappings.Add(mapCreatedOn);

                            SqlBulkCopyColumnMapping mapCreatedBy = new SqlBulkCopyColumnMapping("CREATED_BY", "CREATED_BY");
                            bulkcopy.ColumnMappings.Add(mapCreatedBy);

                            SqlBulkCopyColumnMapping mapRemarks = new SqlBulkCopyColumnMapping("Remarks", "REMARKS");
                            bulkcopy.ColumnMappings.Add(mapRemarks);

                            SqlBulkCopyColumnMapping mapCompanyId = new SqlBulkCopyColumnMapping("COMPANY_ID", "COMPANY_ID");
                            bulkcopy.ColumnMappings.Add(mapCompanyId);

                            SqlBulkCopyColumnMapping maphdrId = new SqlBulkCopyColumnMapping("HDR_ID", "HDR_ID");
                            bulkcopy.ColumnMappings.Add(maphdrId);

                            SqlBulkCopyColumnMapping ceaseUpsiId = new SqlBulkCopyColumnMapping("Date when information ceases to be UPSI", "UPSI_CEASE_DATE");
                            bulkcopy.ColumnMappings.Add(ceaseUpsiId);

                            bulkcopy.WriteToServer(Seldt);
                            bulkcopy.Close();
                        }
                        con.Close();
                    }
                }

                using (SqlConnection conn1 = new SqlConnection())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@HDR_ID", hdr_id);
                    parameters[1] = new SqlParameter("@Mode", "INSERT_TABLE");
                    parameters[2] = new SqlParameter("@ADMIN_DB", rel.ADMIN_DATABASE);
                    parameters[3] = new SqlParameter("@USERNAME", HttpContext.Current.Session["CompanyName"].ToString());

                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_TEMPLATE_UPLOAD", rel.MODULE_DATABASE, parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (Convert.ToString(dr["USER_TYPE"]) == "DP")
                            {
                                string _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                                "WHERE TEMPLATE_EVENT='Addition of Designated user in UPSI' " +
                                "AND COMPANY_ID=" + Convert.ToString(rel.COMPANY_ID);
                                DataSet dsTemplate = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, rel.MODULE_DATABASE, null);
                                DataTable dtTemplate = dsTemplate.Tables[0];

                                _sql = "SELECT DEFAULT_EMAIL,SMTP_HOST_NAME,PORT,SSL,SMTP_USER_NAME,PASSWORD," +
                                    "USER_DEFAULT_CREDENTIAL FROM PROCS_CONFIG_SMTP_SETUP(NOLOCK) " +
                                    "WHERE COMPANY_ID=" + Convert.ToString(rel.COMPANY_ID);
                                DataSet dsSMTP = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, rel.MODULE_DATABASE, null);
                                DataTable dtSMTP = dsSMTP.Tables[0];

                                _sql = "SELECT USER_EMAIL FROM " + rel.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) " +
                                    "WHERE LOGIN_ID='" + dr["USER_LOGIN"] + "'";
                                string sEmailId = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, rel.MODULE_DATABASE, null);
                                string sLoginId = Convert.ToString(dr["USER_LOGIN"]);

                                SqlParameter[] parametersX = new SqlParameter[9];
                                parametersX[0] = new SqlParameter("@COMPANY_ID", rel.COMPANY_ID);
                                parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_DESIGNATED");
                                parametersX[2] = new SqlParameter("@ADMIN_DB", rel.ADMIN_DATABASE);
                                parametersX[4] = new SqlParameter("@GRPMEMBER", sLoginId);
                                parametersX[5] = new SqlParameter("@GROUP_ID", Convert.ToInt32(dr["GRP_ID"]));
                                parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dtTemplate.Rows[0]["TEMPLATE_ID"]));
                                parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dtTemplate.Rows[0]["TEMPLATE_SUBJECT"]));
                                parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dtTemplate.Rows[0]["TEMPLATE_BODY"]));

                                DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", rel.MODULE_DATABASE, parametersX);
                                string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                                EmailSender.SendMail(
                                    sEmailId, Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                                    Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                                    Convert.ToString(HttpContext.Current.Session["CompanyId"]), "", sCC,
                                    Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                                );
                                bool status = false;
                            }
                            else
                            {
                                string _sql = "SELECT TEMPLATE_ID,TEMPLATE_SUBJECT,TEMPLATE_BODY FROM PROCS_INSIDER_EMAILS_TEMPLATE_HDR(NOLOCK) " +
                                    "WHERE TEMPLATE_EVENT='Addition of Connected user in UPSI' " +
                                    "AND COMPANY_ID=" + Convert.ToString(rel.COMPANY_ID);
                                DataSet dsTemplate = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, rel.MODULE_DATABASE, null);
                                DataTable dtTemplate = dsTemplate.Tables[0];

                                _sql = "SELECT DEFAULT_EMAIL,SMTP_HOST_NAME,PORT,SSL,SMTP_USER_NAME,PASSWORD," +
                                    "USER_DEFAULT_CREDENTIAL FROM PROCS_CONFIG_SMTP_SETUP(NOLOCK) " +
                                    "WHERE COMPANY_ID=" + Convert.ToString(rel.COMPANY_ID);
                                DataSet dsSMTP = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, rel.MODULE_DATABASE, null);
                                DataTable dtSMTP = dsSMTP.Tables[0];

                                string sEmailId = Convert.ToString(dr["USER_LOGIN"]);

                                SqlParameter[] parametersX = new SqlParameter[9];
                                parametersX[0] = new SqlParameter("@COMPANY_ID", rel.COMPANY_ID);
                                parametersX[1] = new SqlParameter("@Mode", "GET_UPSI_MAIL_FOR_CONNECTED");
                                parametersX[2] = new SqlParameter("@ADMIN_DB", rel.ADMIN_DATABASE);
                                parametersX[4] = new SqlParameter("@GRPMEMBER", sEmailId);
                                parametersX[5] = new SqlParameter("@GROUP_ID", Convert.ToInt32(dr["GRP_ID"]));
                                parametersX[6] = new SqlParameter("@TEMPLATE_ID", Convert.ToInt32(dtTemplate.Rows[0]["TEMPLATE_ID"]));
                                parametersX[7] = new SqlParameter("@MSG_SUBJECT", Convert.ToString(dtTemplate.Rows[0]["TEMPLATE_SUBJECT"]));
                                parametersX[8] = new SqlParameter("@MSG_TEMPLATE", Convert.ToString(dtTemplate.Rows[0]["TEMPLATE_BODY"]));

                                DataSet dsMsg = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_MAIL", rel.MODULE_DATABASE, parametersX);
                                string sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);
                                EmailSender.SendMail(
                                    sEmailId, Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_SUBJECT"]),
                                    Convert.ToString(dsMsg.Tables[0].Rows[0]["MSG_TEMPLATE"]), null, "Inclusion IN UPSI",
                                    Convert.ToString(HttpContext.Current.Session["CompanyId"]), "", sCC,
                                    Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                                );
                            }
                        }
                    }
                    UPSIResponse objResponse1 = new UPSIResponse();
                    objResponse1.StatusFl = true;
                    objResponse1.Msg = "Uploaded Successfully!";
                    return objResponse1;
                }
            }
            catch (Exception ex)
            {
                UPSIResponse objResponse = new UPSIResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}