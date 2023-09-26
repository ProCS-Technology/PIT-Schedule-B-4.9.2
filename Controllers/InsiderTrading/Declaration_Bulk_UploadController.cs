using Newtonsoft.Json;
using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Declaration_Bulk_Upload")]
    public class Declaration_Bulk_UploadController : ApiController
    {
        [Route("getdownload")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Declaration_Bulk_Upload APIs" })]
        public String getdownload()
        {
            try
            {
                // string ID = CryptorEngine.Decrypt(Convert.ToString(context.Request.Form["ID"]), true);

                // string path = context.Server.MapPath("~/SOP/Upload/myoutlet/");
                string path_filename = "/Declaration_Document_Download.xlsx";

                Declaration_Bulk_UploadResponce rep = new Declaration_Bulk_UploadResponce();
                rep.StatusFl = true;
                rep.path = path_filename;

                string json = JsonConvert.SerializeObject(rep);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                return json;

            }
            catch (Exception ex)
            {
                Declaration_Bulk_UploadResponce objResponse = new Declaration_Bulk_UploadResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = "File Not Found on the server";
                string json = JsonConvert.SerializeObject(objResponse);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                return json;
            }
        }

        [Route("SaveUploadFile")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Declaration_Bulk_Upload APIs" })]
        public String SaveUploadFile()
        {
            try
            {

                String fileNames = Convert.ToString(HttpContext.Current.Request.Form["fileNames"]);

                string db_name = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string con_to_change = SQLHelper.GetConnString();
                string modified_con = con_to_change.Replace("PROCS_ADMIN", db_name);

                SqlConnection con_main = new SqlConnection(modified_con);
                con_main.Open();
                string query = "truncate table PROCS_INSIDER_TEMP_PERSONAL_DETAIL;truncate table PROCS_INSIDER_TEMP_RELATIVES_DETAIL;truncate table PROCS_INSIDER_TEMP_DEMAT_ACCOUNT_DETAIL;truncate table PROCS_INSIDER_TEMP_HOLDING_DECLARATION_DTL;";

                SqlCommand cmd_main = new SqlCommand(query, con_main);
                cmd_main.ExecuteNonQuery();
                con_main.Close();



                string sSaveAs1 = "";


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
                        sSaveAs1 = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/Declaration_Document/"), fileNames);
                        file.SaveAs(sSaveAs1);

                    }
                }

                string extension = Path.GetExtension(sSaveAs1);
                string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sSaveAs1 + ";Extended Properties='Excel 8.0;HDR=Yes'";
                string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sSaveAs1 + ";Extended Properties='Excel 8.0;HDR=Yes'";

                string conString = "";

                switch (extension)
                {
                    case ".xls":
                        conString = string.Format(Excel03ConString, sSaveAs1, "YES");
                        break;
                    case ".xlsx":
                        conString = string.Format(Excel07ConString, sSaveAs1, "YES");
                        break;
                }
                //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, "TRUNCATE TABLE MIS_PARTICULAR_UPLOAD_TEMP");
                OleDbConnection con = new OleDbConnection(conString);
                con.Open();

                OleDbCommand oconn = new OleDbCommand("select * from  [PERSONAL_DETAILS$]", con);
                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                DataView dv = new DataView(dt);
                DataTable Seldt = new DataTable();
                Seldt = dv.ToTable("Selected", false, "USER_LOGIN", "RESIDENT_TYPE", "PAN", "IDENTIFICATION_TYPE", "IDENTIFICATION_NUMBER", "MOBILE_NO", "USER_ADDRESS", "PIN_CODE", "COUNTRY", "JOINING_DATE", "DATE_BECOMING_INSIDER", "DEPARTMENT_NAME", "LOCATION_NAME", "DESIGNATION_NAME", "CATEGORY_NAME", "SUBCATEGORY_NAME", "DIN_NUMBER", "STATUS");

                string ccccc = SQLHelper.GetConnString();
                string cons = ccccc.Replace("PROCS_ADMIN", db_name);

                SqlBulkCopy bulkcopy = new SqlBulkCopy(cons);
                bulkcopy.DestinationTableName = "PROCS_INSIDER_TEMP_PERSONAL_DETAIL";
                bulkcopy.WriteToServer(Seldt);
                con.Close();

                // next sheet RELATIVE_DETAILS
                OleDbConnection con1 = new OleDbConnection(conString);
                con1.Open();
                OleDbCommand oconn1 = new OleDbCommand("select * from [RELATIVE_DETAILS$]", con1);
                OleDbDataAdapter adp1 = new OleDbDataAdapter(oconn1);
                DataTable dt1 = new DataTable();
                adp1.Fill(dt1);

                DataView dv1 = new DataView(dt1);
                DataTable Seldt1 = new DataTable();
                Seldt1 = dv1.ToTable("Selected", false, "USER_LOGIN", "RELATIVE_NAME", "RELATION_NAME", "PAN", "IDENTIFICATION_TYPE", "IDENTIFICATION_NUMBER", "MOBILE", "ADDRESS", "PIN_CODE", "COUNTRY", "STATUS");



                SqlBulkCopy bulkcopy1 = new SqlBulkCopy(cons);
                bulkcopy1.DestinationTableName = "PROCS_INSIDER_TEMP_RELATIVES_DETAIL";
                bulkcopy1.WriteToServer(Seldt1);


                con1.Close();

                //next sheetDEMAT_DETAILS$
                OleDbConnection con2 = new OleDbConnection(conString);
                con2.Open();
                OleDbCommand oconn2 = new OleDbCommand("select * from [DEMAT_DETAILS$]", con2);
                OleDbDataAdapter adp2 = new OleDbDataAdapter(oconn2);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);

                DataView dv2 = new DataView(dt2);
                DataTable Seldt2 = new DataTable();
                Seldt2 = dv2.ToTable("Selected", false, "RELATION_NAME", "DEPOSITORY_NAME", "CLIENT_ID", "DEPOSITORY_PARTICIPANT_NAME", "DEPOSITORY_PARTICIPANT_ID", "TRADING_MEMBER_ID", "DEMAT_TYPE", "ACCOUNT_NO", "STATUS", "USER_LOGIN");



                SqlBulkCopy bulkcopy2 = new SqlBulkCopy(cons);
                bulkcopy2.DestinationTableName = "PROCS_INSIDER_TEMP_DEMAT_ACCOUNT_DETAIL";
                bulkcopy2.WriteToServer(Seldt2);


                con2.Close();

                //next sheet
                OleDbConnection con3 = new OleDbConnection(conString);
                con3.Open();
                OleDbCommand oconn3 = new OleDbCommand("select * from [INITIAL HOLDING DETAILS$]", con3);
                OleDbDataAdapter adp3 = new OleDbDataAdapter(oconn3);
                DataTable dt3 = new DataTable();
                adp3.Fill(dt3);

                DataView dv3 = new DataView(dt3);
                DataTable Seldt3 = new DataTable();
                Seldt3 = dv3.ToTable("Selected", false, "RELATION_NAME", "RESTRICTED_COMPANY_NAME", "SECURITY_TYPE", "PAN", "DEMAT_ACCOUNT_NO", "TRADING_MEMBER_ID", "NO_OF_SECURITIES", "USER_LOGIN");



                SqlBulkCopy bulkcopy3 = new SqlBulkCopy(cons);
                bulkcopy3.DestinationTableName = "PROCS_INSIDER_TEMP_HOLDING_DECLARATION_DTL";
                bulkcopy3.WriteToServer(Seldt3);


                con3.Close();

                string comp_id = HttpContext.Current.Session["CompanyId"].ToString();
                string CREATE_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                using (SqlConnection con_proce = new SqlConnection(modified_con))
                {
                    con_proce.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con_proce;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMPANY_ID", comp_id);
                    cmd.Parameters.AddWithValue("@CREATED_BY", CREATE_BY);
                    cmd.CommandText = "SP_PROCS_INSIDER_COPY_MAINTABLE";
                    SqlDataReader rd = cmd.ExecuteReader();




                }



                Declaration_Bulk_UploadResponce rep = new Declaration_Bulk_UploadResponce();
                rep.StatusFl = true;
                rep.Msg = "File Converted Successfully";

                string json = JsonConvert.SerializeObject(rep);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                return json;

            }
            catch (Exception ex)
            {
                Declaration_Bulk_UploadResponce objResponse = new Declaration_Bulk_UploadResponce();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;

                string json = JsonConvert.SerializeObject(objResponse);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                return json;

            }
        }
    }
}
