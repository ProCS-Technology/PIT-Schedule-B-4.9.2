using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using ProcsDLL.Models.InsiderTrading.Repository;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Benpos")]
    public class BenposController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("SaveBenposHdr")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public NonCompliantTaskResponse SaveBenposHdr()
        {

            Int32 HDRID = 0;
            string Module_db = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            try
            {

                if (HttpContext.Current.Session.Count == 0)
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                String sSaveAs = String.Empty;
                String sSaveAs1 = String.Empty;
                String input = HttpContext.Current.Request.Form["Object"];
                String sFileSize = HttpContext.Current.Request.Form["FileSize"];
                BenposHeader rel = new JavaScriptSerializer().Deserialize<BenposHeader>(input);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                if (!rel.ValidateInput())
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                BenposRequest pReq = new BenposRequest(rel);

                bool isValidBenposAsOfDate = pReq.ValidateBenposAsOfDate();
                if (!isValidBenposAsOfDate)
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Benpos as of date cannot be less than or equal to last benpos as of date!";
                    return objResponse;
                }

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        int cLength = file.ContentLength;
                        String ext = Path.GetExtension(file.FileName);
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "Benpos_";
                        string fname;
                        string sContentTyp = file.ContentType;
                        if (sContentTyp.ToUpper() == "APPLICATION/VND.MS-EXCEL" || sContentTyp.ToUpper() == "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET")
                        {
                            if (sFileSize != cLength.ToString())
                            {
                                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document is corrupt, please upload correct the one.";
                                return objResponse;
                            }
                            if (sNm.Contains("%00"))
                            {
                                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                                return objResponse;
                            }
                            if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                            {
                                fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                                sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/Benpos/"), fname);
                                file.SaveAs(sSaveAs);
                                rel.fileName = fname;
                            }
                            else
                            {
                                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Only xls or xlsx attachement is allowed";
                                return objResponse;
                            }
                        }
                        else
                        {
                            NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Content type of the uploaded document does not matched with the permissible document";
                            return objResponse;
                        }
                    }
                }
                else
                {
                    rel.fileName = String.Empty;
                    rel.fileNameESOP = String.Empty;

                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "No Benpos file uploaded, please try again!";
                    return objResponse;
                }
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);


                pReq = new BenposRequest(rel);
                BenposResponse pRes = pReq.SaveBenposHdr();
                HDRID = pRes.BenposHeader.id;
                BenposMappingResponse bmRes = pReq.GetBenposFieldMapping();

                var BenposFields = bmRes.BenposMappingList.Where(x => x.TemplateType == "Benpos").ToList();
                var EsopFields = bmRes.BenposMappingList.Where(x => x.TemplateType == "ESOP").ToList();

                if (BenposFields.Count > 0)
                {
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
                                            //string xlsQry = "SELECT '" + pRes.BenposHeader.id.ToString() + "' AS HDR_ID,";
                                            //foreach (var bField in BenposFields)
                                            //{
                                            //    xlsQry += "[" + bField.ExcelField + "],";
                                            //}
                                            //xlsQry += "'Benpos' AS TYPE_FL FROM [" + sSheetNm + "]";

                                            string xlsQry = "SELECT '" + pRes.BenposHeader.id.ToString() + "' AS HDR_ID,*,'Benpos' AS TYPE_FL FROM [" + sSheetNm + "]";
                                            OleDbCommand oconn = new OleDbCommand(xlsQry, con);
                                            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                            DataTable dt = new DataTable();

                                            adp.Fill(dt);

                                            List<string> lstFields = new List<string>();
                                            foreach (var bField in BenposFields)
                                            {
                                                bool colExists = false;
                                                foreach (DataColumn dc in dt.Columns)
                                                {
                                                    if (bField.ExcelField.ToLower() == dc.ColumnName.ToLower())
                                                    {
                                                        colExists = true;
                                                        break;
                                                    }
                                                }
                                                if (!colExists)
                                                {
                                                    lstFields.Add("Column " + bField.ExcelField + " does not exists in the excel template");
                                                }
                                            }

                                            if (lstFields.Count > 0)
                                            {
                                                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                                                objResponse.StatusFl = false;
                                                objResponse.Msg = "";
                                                foreach (string s in lstFields)
                                                {
                                                    objResponse.Msg += s + "\n";
                                                }
                                                BenposRepository bmprep = new BenposRepository();
                                                bmprep.deleteBenposHdr(HDRID, Module_db);
                                                return objResponse;
                                            }

                                            string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                                            {
                                                bulkcopy.BulkCopyTimeout = 0;
                                                bulkcopy.DestinationTableName = "PROCS_INSIDER_BENPOS_DETL";

                                                SqlBulkCopyColumnMapping mapHdrId = new SqlBulkCopyColumnMapping("HDR_ID", "HDR_ID");
                                                bulkcopy.ColumnMappings.Add(mapHdrId);

                                                SqlBulkCopyColumnMapping mapTypeFl = new SqlBulkCopyColumnMapping("TYPE_FL", "TYPE_FL");
                                                bulkcopy.ColumnMappings.Add(mapTypeFl);

                                                foreach (var bField in BenposFields)
                                                {
                                                    if (bField.FieldType == "FOLIO")
                                                    {
                                                        SqlBulkCopyColumnMapping mapFolio = new SqlBulkCopyColumnMapping(bField.ExcelField, "FOLIO_NO");
                                                        bulkcopy.ColumnMappings.Add(mapFolio);
                                                    }
                                                    else if (bField.FieldType == "PAN")
                                                    {
                                                        SqlBulkCopyColumnMapping mapPAN = new SqlBulkCopyColumnMapping(bField.ExcelField, "PAN_NUMBER");
                                                        bulkcopy.ColumnMappings.Add(mapPAN);
                                                    }
                                                    else if (bField.FieldType == "NAME")
                                                    {
                                                        SqlBulkCopyColumnMapping mapShareHolderName = new SqlBulkCopyColumnMapping(bField.ExcelField, "SHAREHOLDER_NAME");
                                                        bulkcopy.ColumnMappings.Add(mapShareHolderName);
                                                    }
                                                    else if (bField.FieldType == "HOLDING")
                                                    {
                                                        SqlBulkCopyColumnMapping mapHolding = new SqlBulkCopyColumnMapping(bField.ExcelField, "HOLDING");
                                                        bulkcopy.ColumnMappings.Add(mapHolding);

                                                    }
                                                    else if (bField.FieldType == "TYPE")
                                                    {
                                                        SqlBulkCopyColumnMapping mapType = new SqlBulkCopyColumnMapping(bField.ExcelField, "INVESTOR_TYPE");
                                                        bulkcopy.ColumnMappings.Add(mapType);
                                                    }
                                                }
                                                bulkcopy.WriteToServer(dt);
                                                bulkcopy.Close();

                                                //jag -- S
                                                //BenposRepository ISTHRES = new BenposRepository();
                                                //bool isThreshollimitmatch = ISTHRES.isThreshollimitmatch(HDRID,Module_db);
                                                //if (!isThreshollimitmatch)
                                                //{
                                                //    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                                                //    objResponse.StatusFl = false;
                                                //    objResponse.Msg = "Benpos Share holding id not equal to threshold limit";
                                                //    BenposRepository bmprep = new BenposRepository();
                                                //    bmprep.deleteBenposHdr(HDRID, Module_db);
                                                //    return objResponse;                                                   
                                                //}
                                                //jag -- E
                                            }
                                        }
                                    }
                                }
                            }
                            con.Close();
                        }
                    }
                }
                rel.id = pRes.BenposHeader.id;

                if (rel.ValidateInput())
                {
                    pReq = new BenposRequest(rel);
                    pRes = pReq.UpdateBenposDetail();
                    if (pRes.StatusFl)
                    {
                        NonCompliantTask nonCompliantTask = new NonCompliantTask();
                        nonCompliantTask.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                        nonCompliantTask.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                        nonCompliantTask.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                        nonCompliantTask.asOfDate = !String.IsNullOrEmpty(Convert.ToString(rel.asOfDate)) ? Convert.ToString(rel.asOfDate) : String.Empty;
                        nonCompliantTask.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDB"]);
                        nonCompliantTask.id = rel.id;
                        NonCompliantTaskRequest getRunNowCompliantFinder = new NonCompliantTaskRequest(nonCompliantTask);
                        NonCompliantTaskResponse gResRunNowCompliantFinder = getRunNowCompliantFinder.RunNowCompliantFinder();
                        return gResRunNowCompliantFinder;
                    }
                    else
                    {
                        NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                        objResponse.StatusFl = false;
                        objResponse.Msg = pRes.Msg;//"Processing failed due to system error!";
                        return objResponse;
                    }
                }
                else
                {
                    NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                BenposRepository bmprep = new BenposRepository();
                bmprep.deleteBenposHdr(HDRID, Module_db);
                NonCompliantTaskResponse objResponse = new NonCompliantTaskResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllBenposHdr")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BenposResponse GetAllBenposHdr()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BenposHeader benposHdr = new BenposHeader();
                benposHdr.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                benposHdr.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposHdr.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!benposHdr.ValidateInput())
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                BenposRequest BenposHdrList = new BenposRequest(benposHdr);
                BenposResponse gResBenposHdrList = BenposHdrList.GetAllBenposHdr();
                return gResBenposHdrList;
            }
            catch (Exception ex)
            {
                BenposResponse objResponse = new BenposResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveEsopHdr")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BenposResponse SaveEsopHdr()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                String sSaveAs = String.Empty;
                String input = HttpContext.Current.Request.Form["Object"];
                String sFileSize = HttpContext.Current.Request.Form["FileSize"];
                BenposHeader rel = new JavaScriptSerializer().Deserialize<BenposHeader>(input);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                if (!rel.ValidateInput())
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }

                BenposRequest pReq = new BenposRequest(rel);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        int cLength = file.ContentLength;
                        String ext = Path.GetExtension(file.FileName);
                        string sNm = Path.GetFileNameWithoutExtension(file.FileName);
                        String name = "ESOP_";
                        string fname;
                        string sContentTyp = file.ContentType;
                        if (sContentTyp.ToUpper() == "APPLICATION/VND.MS-EXCEL" || sContentTyp.ToUpper() == "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET")
                        {
                            if (sFileSize != cLength.ToString())
                            {
                                BenposResponse objResponse = new BenposResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document is corrupt, please upload correct the one.";
                                return objResponse;
                            }
                            if (sNm.Contains("%00"))
                            {
                                BenposResponse objResponse = new BenposResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Uploaded document contains nullbyte, please correct the name and try again.";
                                return objResponse;
                            }
                            if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                            {
                                fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                                sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/Benpos/"), fname);
                                file.SaveAs(sSaveAs);
                                rel.fileNameESOP = fname;
                            }
                            else
                            {
                                BenposResponse objResponse = new BenposResponse();
                                objResponse.StatusFl = false;
                                objResponse.Msg = "Only xls or xlsx attachement is allowed";
                                return objResponse;
                            }
                        }
                        else
                        {
                            BenposResponse objResponse = new BenposResponse();
                            objResponse.StatusFl = false;
                            objResponse.Msg = "Content type of the uploaded document does not matched with the permissible document";
                            return objResponse;
                        }
                    }
                }

                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);

                pReq = new BenposRequest(rel);
                BenposResponse pRes = pReq.SaveEsop();
                BenposMappingResponse bmRes = pReq.GetBenposFieldMapping();

                var EsopFields = bmRes.BenposMappingList.Where(x => x.TemplateType == "ESOP").ToList();

                if (EsopFields.Count > 0)
                {
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

                        if (pRes.BenposHeader.esopHdrId != 0)
                        {
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

                                                string xlsQry = "SELECT '" + pRes.BenposHeader.esopHdrId + "' AS HDR_ID,";
                                                foreach (var bField in EsopFields)
                                                {
                                                    xlsQry += "[" + bField.ExcelField + "],";
                                                }
                                                xlsQry = xlsQry.Substring(0, xlsQry.Length - 1);
                                                xlsQry += " FROM [" + sSheetNm + "]";

                                                OleDbCommand oconn = new OleDbCommand(xlsQry, con);
                                                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                                DataTable dt = new DataTable();

                                                adp.Fill(dt);

                                                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                                                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                                                {
                                                    bulkcopy.BulkCopyTimeout = 0;
                                                    bulkcopy.DestinationTableName = "PROCS_INSIDER_ESOP_DETL";
                                                    SqlBulkCopyColumnMapping mapHdrId = new SqlBulkCopyColumnMapping("HDR_ID", "HDR_ID");
                                                    bulkcopy.ColumnMappings.Add(mapHdrId);

                                                    foreach (var bField in EsopFields)
                                                    {
                                                        if (bField.FieldType == "FOLIO")
                                                        {
                                                            SqlBulkCopyColumnMapping mapFolio = new SqlBulkCopyColumnMapping(bField.ExcelField, "FOLIO_NO");
                                                            bulkcopy.ColumnMappings.Add(mapFolio);
                                                        }
                                                        else if (bField.FieldType == "PAN")
                                                        {
                                                            SqlBulkCopyColumnMapping mapPAN = new SqlBulkCopyColumnMapping(bField.ExcelField, "PAN_NUMBER");
                                                            bulkcopy.ColumnMappings.Add(mapPAN);
                                                        }
                                                        else if (bField.FieldType == "NAME")
                                                        {
                                                            SqlBulkCopyColumnMapping mapShareHolderName = new SqlBulkCopyColumnMapping(bField.ExcelField, "SHAREHOLDER_NAME");
                                                            bulkcopy.ColumnMappings.Add(mapShareHolderName);
                                                        }
                                                        else if (bField.FieldType == "QTY")
                                                        {
                                                            SqlBulkCopyColumnMapping mapQty = new SqlBulkCopyColumnMapping(bField.ExcelField, "QTY");
                                                            bulkcopy.ColumnMappings.Add(mapQty);
                                                        }
                                                        else if (bField.FieldType == "RATE")
                                                        {
                                                            SqlBulkCopyColumnMapping mapRate = new SqlBulkCopyColumnMapping(bField.ExcelField, "RATE");
                                                            bulkcopy.ColumnMappings.Add(mapRate);
                                                        }
                                                        else if (bField.FieldType == "DATE")
                                                        {
                                                            SqlBulkCopyColumnMapping mapDate = new SqlBulkCopyColumnMapping(bField.ExcelField, "DATE");
                                                            bulkcopy.ColumnMappings.Add(mapDate);
                                                        }
                                                    }
                                                    bulkcopy.WriteToServer(dt);
                                                    bulkcopy.Close();
                                                }
                                            }
                                        }
                                    }
                                }
                                con.Close();
                            }
                        }
                    }
                }
                rel.esopHdrId = pRes.BenposHeader.esopHdrId;
                pReq = new BenposRequest(rel);
                pRes = pReq.UpdateEsopAmount();
                pRes.StatusFl = true;
                pRes.Msg = "Success";
                return pRes;
            }
            catch (Exception ex)
            {
                BenposResponse objResponse = new BenposResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetAllEsopHdr")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BenposResponse GetAllEsopHdr()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BenposHeader benposHdr = new BenposHeader();
                benposHdr.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                benposHdr.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposHdr.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!benposHdr.ValidateInput())
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                BenposRequest BenposHdrList = new BenposRequest(benposHdr);
                BenposResponse gResBenposHdrList = BenposHdrList.GetAllEsopHdr();
                return gResBenposHdrList;
            }
            catch (Exception ex)
            {
                BenposResponse objResponse = new BenposResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("GetEsopListByUser")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BenposResponse GetEsopListByUser()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                BenposHeader benposHdr = new BenposHeader();
                benposHdr.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                benposHdr.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposHdr.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!benposHdr.ValidateInput())
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;
                    return objResponse;
                }
                BenposRequest BenposHdrList = new BenposRequest(benposHdr);
                BenposResponse gResBenposHdrList = BenposHdrList.GetEsopListByUser();
                return gResBenposHdrList;
            }
            catch (Exception ex)
            {
                BenposResponse objResponse = new BenposResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("CorporateActionListById")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BenposResponse CorporateActionListById()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BenposResponse objSessionResponse = new BenposResponse();
                    objSessionResponse.StatusFl = false;
                    objSessionResponse.Msg = "SessionExpired";
                    return objSessionResponse;
                }

                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                BenposHeader corporateaction = new JavaScriptSerializer().Deserialize<BenposHeader>(input);
                corporateaction.companyId = Convert.ToInt32(HttpContext.Current.Session["COMPANY_ID"]);
                corporateaction.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                corporateaction.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!corporateaction.ValidateInput())
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                BenposRequest gReqCorporateList = new BenposRequest(corporateaction);
                BenposResponse gResCorporateList = gReqCorporateList.GetCorporateListById();
                return gResCorporateList;
            }
            catch (Exception ex)
            {
                BenposResponse objResponse = new BenposResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteBenposDetail")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public BenposResponse DeleteBenposDetail()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                BenposHeader benposhdr = new JavaScriptSerializer().Deserialize<BenposHeader>(input);
                benposhdr.LoggedInUser = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                benposhdr.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                benposhdr.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                benposhdr.ADMIN_DATABASE = Convert.ToString(HttpContext.Current.Session["AdminDb"]);
                if (!benposhdr.ValidateInput())
                {
                    BenposResponse objResponse = new BenposResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                BenposRequest BenposRequest = new BenposRequest(benposhdr);
                BenposResponse BenposResponse = BenposRequest.DeleteBenposDetail();
                return BenposResponse;
            }
            catch (Exception ex)
            {
                BenposResponse objResponse = new BenposResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }



        [Route("GetBenposFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public HttpResponseMessage GetBenposFile()
        {
            try
            {
                //if (HttpContext.Current.Session.Count == 0)
                //{
                //    BenposResponse objResponse = new BenposResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "SessionExpired";
                //    return objResponse;
                //}
                
                string sBenposId = Convert.ToString(HttpContext.Current.Request.QueryString["BenposId"]);
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                string sFileNm = "";

                using (SqlConnection sCon = new SqlConnection(str))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.Text;
                    sCon.Open();
                    sCmd.CommandText = "SELECT FILENAME FROM PROCS_INSIDER_BENPOS_HDR(NOLOCK) WHERE HDR_ID=" + sBenposId;
                    sFileNm = Convert.ToString(sCmd.ExecuteScalar());
                }
                string sFile = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/Benpos/"), sFileNm);

                byte[] fileBook = File.ReadAllBytes(sFile);// tempPathExcelFile);
                MemoryStream stream = new MemoryStream();
                string excelBase64String = Convert.ToBase64String(fileBook);
                StreamWriter excelWriter = new StreamWriter(stream);
                excelWriter.Write(excelBase64String);
                excelWriter.Flush();
                stream.Position = 0;
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StreamContent(stream);
                httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                httpResponseMessage.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                throw ex;
                //return ReturnError(ErrorType.Error, errorMessage);
            }
        }
        [Route("GetESOPFile")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Benpos APIs" })]
        public HttpResponseMessage GetESOPFile()
        {
            try
            {
                //if (HttpContext.Current.Session.Count == 0)
                //{
                //    BenposResponse objResponse = new BenposResponse();
                //    objResponse.StatusFl = false;
                //    objResponse.Msg = "SessionExpired";
                //    return objResponse;
                //}

                string sEsopId = Convert.ToString(HttpContext.Current.Request.QueryString["EsopId"]);
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                string sFileNm = "";

                using (SqlConnection sCon = new SqlConnection(str))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandType = CommandType.Text;
                    sCon.Open();
                    sCmd.CommandText = "SELECT FILENAME FROM PROCS_INSIDER_BENPOS_HDR(NOLOCK) WHERE HDR_ID=" + sEsopId;
                    sFileNm = Convert.ToString(sCmd.ExecuteScalar());
                }
                string sFile = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/Benpos/"), sFileNm);

                byte[] fileBook = File.ReadAllBytes(sFile);// tempPathExcelFile);
                MemoryStream stream = new MemoryStream();
                string excelBase64String = Convert.ToBase64String(fileBook);
                StreamWriter excelWriter = new StreamWriter(stream);
                excelWriter.Write(excelBase64String);
                excelWriter.Flush();
                stream.Position = 0;
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StreamContent(stream);
                httpResponseMessage.Content.Headers.Add("x-filename", "ExcelReport.xlsx");
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                httpResponseMessage.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = "ExcelReport.xlsx";
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                throw ex;
                //return ReturnError(ErrorType.Error, errorMessage);
            }
        }
    }
}