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
namespace ProcsDLL.Controllers.InsiderTrading
{
    public class ConnectedPersonController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetCPUsers")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "User APIs" })]
        public CPResponse GetCPUsers()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CPResponse objSessionResponse = new CPResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }
                CP cp = new CP();
                cp.CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                cp.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                cp.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!cp.ValidateInput())
                {
                    CPResponse objResponse = new CPResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                CPRequest gReqUserList = new CPRequest(cp);
                CPResponse gResUserList = gReqUserList.GetCPUserList();
                return gResUserList;
            }
            catch (Exception ex)
            {
                CPResponse objResponse = new CPResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [HttpPost]
        public CPResponse GetUPSIGroupCP()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CPResponse objSessionResponse = new CPResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
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

                if (!upsiGrp.ValidateInput())
                {
                    CPResponse objResponse = new CPResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                CPRequest gReqUserList = new CPRequest(upsiGrp);
                CPResponse gResUserList = gReqUserList.GetUPSIGroupCP();
                return gResUserList;
            }
            catch (Exception ex)
            {
                CPResponse objResponse = new CPResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [HttpPost]
        public CPResponse SaveConnectedPersons()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CPResponse objResponse = new CPResponse();
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
                    List<CP> lstCP = new JavaScriptSerializer().Deserialize<List<CP>>(input);
                    foreach (CP cp in lstCP)
                    {
                        if (!cp.ValidateInput())
                        {
                            CPResponse objResponse = new CPResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = sXSSErrMsg;// "A potentially dangerous Request value was detected from the client, please check the data you are trying to post";
                            return objResponse;
                        }
                    }
                    CPResponse upsiGrpRes = new CPResponse();
                    CPRequest cpRequest = new CPRequest(lstCP);
                    upsiGrpRes = cpRequest.SaveConnectedPersons();
                    return upsiGrpRes;
                }
                else
                {
                    CPResponse objResponse = new CPResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                CPResponse objResponse = new CPResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [HttpPost]
        public CPResponse SaveNewConnectedPersons()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CPResponse objResponse = new CPResponse();
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
                    List<CP> lstCP = new JavaScriptSerializer().Deserialize<List<CP>>(input);
                    foreach (CP cp in lstCP)
                    {
                        if (!cp.ValidateInput())
                        {
                            CPResponse objResponse = new CPResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = sXSSErrMsg;
                            return objResponse;
                        }
                    }
                    CPResponse upsiGrpRes = new CPResponse();
                    CPRequest cpRequest = new CPRequest(lstCP);
                    upsiGrpRes = cpRequest.SaveNewConnectedPersons();
                    return upsiGrpRes;
                }
                else
                {
                    CPResponse objResponse = new CPResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                CPResponse objResponse = new CPResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [HttpPost]
        public CPResponse SaveConnectedPersonsForUPSITask()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CPResponse objResponse = new CPResponse();
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
                    List<CP> lstCP = new JavaScriptSerializer().Deserialize<List<CP>>(input);
                    foreach (CP cp in lstCP)
                    {
                        if (!cp.ValidateInput())
                        {
                            CPResponse objResponse = new CPResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = sXSSErrMsg;
                            return objResponse;
                        }
                    }
                    CPResponse upsiGrpRes = new CPResponse();
                    CPRequest cpRequest = new CPRequest(lstCP);
                    upsiGrpRes = cpRequest.SaveConnectedPersonsForUPSITask();
                    return upsiGrpRes;
                }
                else
                {
                    CPResponse objResponse = new CPResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unauthorized access!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                CPResponse objResponse = new CPResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("UploadCP")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Transaction APIs" })]
        public CPResponse UploadCP()
        {
            try
            {
                CPResponse userResponse = new CPResponse();
                if (HttpContext.Current.Session.Count == 0)
                {
                    CPResponse objResponse = new CPResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                String sSaveAs = String.Empty;
                String sUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                String sFileSize = HttpContext.Current.Request.Form["FileSize"];
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        int cLength = file.ContentLength;
                        String ext = Path.GetExtension(file.FileName);
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "ConnectedPerson_";
                        string fname;
                        string sContentTyp = file.ContentType;
                        if (sContentTyp.ToUpper() == "APPLICATION/VND.MS-EXCEL" || sContentTyp.ToUpper() == "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET")
                        {
                            if (sFileSize != cLength.ToString())
                            {
                                CPResponse objResponse = new CPResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document is corrupt, please upload correct the one.";
                                return objResponse;
                            }
                            if (sNm.Contains("%00"))
                            {
                                CPResponse objResponse = new CPResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                                return objResponse;
                            }
                            if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                            {
                                fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                                sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/Benpos/"), fname);
                                file.SaveAs(sSaveAs);
                            }
                            else
                            {
                                CPResponse objResponse = new CPResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Only xls or xlsx attachement is allowed";
                                return objResponse;
                            }
                        }
                        else
                        {
                            CPResponse objResponse = new CPResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Content type of the uploaded document does not matched with the permissible document";
                            return objResponse;
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
                                string xlsQry = "SELECT [Firm] AS FIRM,[Name] AS CP_NAME,[Email] AS CP_EMAIL,[Identification Type] AS CP_IDENTIFICATION_TYPE," +
                                    "[Identification No] AS CP_IDENTIFICATION_NO,'Active' AS CP_STATUS FROM [Sheet1$]";
                                OleDbCommand oconn = new OleDbCommand(xlsQry, con);
                                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);

                                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                                using(SqlConnection scon=new SqlConnection(str))
                                {
                                    SqlCommand scmd = new SqlCommand();
                                    scon.Open();
                                    scmd.Connection = scon;
                                    string _sql = "TRUNCATE TABLE PROCS_INSIDER_CONNECTED_PERSONS_TEMP";
                                    scmd.CommandText = _sql;
                                    scmd.CommandType = CommandType.Text;
                                    scmd.ExecuteNonQuery();
                                }
                                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                                {
                                    
                                    bulkcopy.BulkCopyTimeout = 0;
                                    bulkcopy.DestinationTableName = "PROCS_INSIDER_CONNECTED_PERSONS_TEMP";

                                    SqlBulkCopyColumnMapping mapUserId = new SqlBulkCopyColumnMapping("FIRM", "FIRM");
                                    bulkcopy.ColumnMappings.Add(mapUserId);

                                    SqlBulkCopyColumnMapping mapIsin = new SqlBulkCopyColumnMapping("CP_NAME", "CP_NAME");
                                    bulkcopy.ColumnMappings.Add(mapIsin);

                                    SqlBulkCopyColumnMapping mapDemat = new SqlBulkCopyColumnMapping("CP_EMAIL", "CP_EMAIL");
                                    bulkcopy.ColumnMappings.Add(mapDemat);

                                    SqlBulkCopyColumnMapping mapPan = new SqlBulkCopyColumnMapping("CP_IDENTIFICATION_TYPE", "CP_IDENTIFICATION_TYPE");
                                    bulkcopy.ColumnMappings.Add(mapPan);

                                    SqlBulkCopyColumnMapping mapHolding = new SqlBulkCopyColumnMapping("CP_IDENTIFICATION_NO", "CP_IDENTIFICATION_NO");
                                    bulkcopy.ColumnMappings.Add(mapHolding);

                                    SqlBulkCopyColumnMapping stHolding = new SqlBulkCopyColumnMapping("CP_STATUS", "CP_STATUS");
                                    bulkcopy.ColumnMappings.Add(stHolding);

                                    bulkcopy.WriteToServer(dt);
                                    bulkcopy.Close();

                                    con.Close();
                                }
                            }
                        }
                    }

                    string str1 = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                    using (SqlConnection sCon = new SqlConnection(str1))
                    {
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandType = CommandType.Text;
                        sCmd.CommandText = "SELECT * FROM (SELECT dbo.ChkValidEmail(CP_EMAIL) AS VALID_EMAIL," +
                            "CASE WHEN CP_IDENTIFICATION_TYPE='PAN' THEN dbo.CheckPAN(CP_IDENTIFICATION_NO) ELSE 1 END AS VALID_PAN,"+
                            "ISNULL(CP_NAME,'') AS CP_NAME,ISNULL(CP_EMAIL, '') AS CP_EMAIL,"+
                            "ISNULL(CP_IDENTIFICATION_TYPE, '') AS CP_IDENTIFICATION_TYPE,ISNULL(CP_IDENTIFICATION_NO, '') AS CP_IDENTIFICATION_NO,"+
                            "ISNULL(FIRM, '') AS FIRM," +
                            "(SELECT COUNT(*) FROM PROCS_INSIDER_CONNECTED_PERSONS(NOLOCK) WHERE CP_EMAIL=A.CP_EMAIL) AS DUPLICATE," +
                            "(SELECT COUNT(*) FROM PROCS_INSIDER_CONNECTED_PERSONS_TEMP(NOLOCK) WHERE CP_EMAIL=A.CP_EMAIL GROUP BY CP_EMAIL) AS SELF_DUPLICATE " +
                            "FROM PROCS_INSIDER_CONNECTED_PERSONS_TEMP(NOLOCK) A) X " +
                            "WHERE (X.VALID_EMAIL=0 OR X.VALID_PAN=0 OR X.DUPLICATE>0 OR X.SELF_DUPLICATE>1) OR " +
                            "(X.CP_NAME='' OR X.CP_EMAIL='' OR X.CP_IDENTIFICATION_TYPE='' OR X.CP_IDENTIFICATION_NO='' OR X.FIRM='')";

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
                            sb.Append("<th>Firm</th>");
                            sb.Append("<th>Name</th>");
                            sb.Append("<th>Email</th>");
                            sb.Append("<th>Identification</th>");
                            sb.Append("<th>Identification #</th>");
                            sb.Append("<th>Remarks</th>");
                            sb.Append("</tr>");

                            foreach (DataRow drException in dtException.Rows)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(drException["FIRM"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(drException["CP_NAME"]) + "</td>");

                                if (Convert.ToInt32(drException["VALID_EMAIL"]) == 0 || Convert.ToInt32(drException["DUPLICATE"]) > 0 || Convert.ToInt32(drException["SELF_DUPLICATE"]) >1)
                                {
                                    sb.Append("<td style='color:red;'>" + Convert.ToString(drException["CP_EMAIL"]) + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td>" + Convert.ToString(drException["CP_EMAIL"]) + "</td>");
                                }
                                sb.Append("<td>" + Convert.ToString(drException["CP_IDENTIFICATION_TYPE"]) + "</td>");

                                if (Convert.ToInt32(drException["VALID_PAN"]) == 0)
                                {
                                    sb.Append("<td style='color:red;'>" + Convert.ToString(drException["CP_IDENTIFICATION_NO"]) + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td>" + Convert.ToString(drException["CP_IDENTIFICATION_NO"]) + "</td>");
                                }

                                sb.Append("<td>");
                                string sMsg = "";
                                if (Convert.ToInt32(drException["VALID_EMAIL"]) == 0)
                                {
                                    if (sMsg == "") { sMsg += "Invalid Email"; }
                                    else { sMsg += ", Invalid Email"; }
                                }
                                if (Convert.ToInt32(drException["DUPLICATE"]) > 0)
                                {
                                    if (sMsg == "") { sMsg += "Email already exists"; }
                                    else { sMsg += ", Email already exists"; }
                                }
                                if (Convert.ToInt32(drException["SELF_DUPLICATE"]) > 1)
                                {
                                    if (sMsg == "") { sMsg += "Same email multiple times in template uploaded"; }
                                    else { sMsg += ", Same email multiple times in template uploaded"; }
                                }
                                if (Convert.ToInt32(drException["VALID_PAN"]) == 0)
                                {
                                    if (sMsg == "") { sMsg += "Invalid PAN"; }
                                    else { sMsg += ", Invalid PAN"; }
                                }
                                if(Convert.ToString(drException["CP_NAME"])=="")
                                {
                                    if (sMsg == "") { sMsg += "Connected Person name is missing"; }
                                    else { sMsg += ", Connected Person name is missing"; }
                                }
                                if (Convert.ToString(drException["CP_EMAIL"]) == "")
                                {
                                    if (sMsg == "") { sMsg += "Connected Person email is missing"; }
                                    else { sMsg += ", Connected Person email is missing"; }
                                }
                                if (Convert.ToString(drException["CP_IDENTIFICATION_TYPE"]) == "")
                                {
                                    if (sMsg == "") { sMsg += "Connected Person identification type is missing"; }
                                    else { sMsg += ", Connected Person identification type is missing"; }
                                }
                                if (Convert.ToString(drException["CP_IDENTIFICATION_NO"]) == "")
                                {
                                    if (sMsg == "") { sMsg += "Connected Person identification number is missing"; }
                                    else { sMsg += ", Connected Person identification number is missing"; }
                                }
                                if (Convert.ToString(drException["FIRM"]) == "")
                                {
                                    if (sMsg == "") { sMsg += "Connected Person firm is missing"; }
                                    else { sMsg += ", Connected Person firm is missing"; }
                                }
                                sb.Append(sMsg);
                                sb.Append("</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</table>");

                            userResponse.sException = sb.ToString();
                            userResponse.StatusFl = false;
                            userResponse.Msg = "Conflict Exists";
                        }
                        else
                        {
                            CPRequest cpRequest = new CPRequest();
                            userResponse = cpRequest.UploadCP();
                            userResponse.StatusFl = true;
                            userResponse.Msg = "Connected Persons uploaded successfully !";
                        }
                    }                    
                }
                return userResponse;
            }
            catch (Exception ex)
            {
                CPResponse objResponse = new CPResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}