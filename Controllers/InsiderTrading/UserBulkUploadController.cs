using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;


namespace ProcsDLL.Controllers.InsiderTrading
{

    [RoutePrefix("api/UserBulkUpload")]
    public class UserBulkUploadController : ApiController
    {


        [Route("SaveUserTemplate")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Policy APIs" })]
        public UserBulkUploadResponse SaveUserTemplate()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserBulkUploadResponse objResponse = new UserBulkUploadResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                //String input = HttpContext.Current.Request.Form["Object"];
                //Policy rel = new JavaScriptSerializer().Deserialize<Policy>(input);
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


                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserBulkUpload/"), "UserBulkUploadWithData.xls");
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserBulkUpload/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserBulkUpload/"));

                        }
                        file.SaveAs(sSaveAs);


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

                                OleDbCommand oconn = new OleDbCommand("select * from [UserData$]", con);
                                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                DataTable dt = new DataTable();

                                adp.Fill(dt);



                                DataView dv = new DataView(dt);
                                DataTable Seldt = new DataTable();


                                Seldt = dv.ToTable("Selected", false, "LOGIN_ID", "SALUTATION", "USER_NAME", "USER_EMAIL", "USER_MOBILE", "PAN", "USER_ROLE", "IS_APPROVER", "IS_FIRST_TIME");
                                // string str = "Data Source=192.168.1.144;Initial Catalog=PROCS_INSIDER_TRADING;User ID=sa;Password=P@ssw0rd;pooling=false;";
                                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                                String MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                                SqlParameter[] parameters1 = new SqlParameter[3];

                                parameters1[0] = new SqlParameter("@MODE", "DELETE");
                                parameters1[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                                parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                                parameters1[2].Direction = ParameterDirection.Output;
                                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "PROCS_INSIDER_USER_UPLOAD_BULKS_DATA", MODULE_DATABASE, parameters1);
                                var obj1 = parameters1[2].Value;

                                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                                {
                                    bulkcopy.DestinationTableName = "PROCS_INSIDER_USER_UPLOAD_BULK_DATA";
                                    SqlBulkCopyColumnMapping mapHdrId = new SqlBulkCopyColumnMapping("LOGIN_ID", "LOGIN_ID");
                                    bulkcopy.ColumnMappings.Add(mapHdrId);

                                    SqlBulkCopyColumnMapping mapSALUTATION = new SqlBulkCopyColumnMapping("SALUTATION", "SALUTATION");
                                    bulkcopy.ColumnMappings.Add(mapSALUTATION);

                                    SqlBulkCopyColumnMapping mapUSER_NAME = new SqlBulkCopyColumnMapping("USER_NAME", "USER_NAME");
                                    bulkcopy.ColumnMappings.Add(mapUSER_NAME);

                                    SqlBulkCopyColumnMapping mapUSER_EMAIL =
                                        new SqlBulkCopyColumnMapping("USER_EMAIL", "USER_EMAIL");
                                    bulkcopy.ColumnMappings.Add(mapUSER_EMAIL);

                                    SqlBulkCopyColumnMapping mapUSER_MOBILE =
                                        new SqlBulkCopyColumnMapping("USER_MOBILE", "USER_MOBILE");
                                    bulkcopy.ColumnMappings.Add(mapUSER_MOBILE);

                                    SqlBulkCopyColumnMapping mapPAN =
                                       new SqlBulkCopyColumnMapping("PAN", "PAN");
                                    bulkcopy.ColumnMappings.Add(mapPAN);

                                    SqlBulkCopyColumnMapping mapUSER_ROLE =
                                       new SqlBulkCopyColumnMapping("USER_ROLE", "USER_ROLE");
                                    bulkcopy.ColumnMappings.Add(mapUSER_ROLE);

                                    SqlBulkCopyColumnMapping mapIS_APPROVER =
                                       new SqlBulkCopyColumnMapping("IS_APPROVER", "IS_APPROVER");
                                    bulkcopy.ColumnMappings.Add(mapIS_APPROVER);
                                    SqlBulkCopyColumnMapping mapIS_FIRST_TIME =
                                       new SqlBulkCopyColumnMapping("IS_FIRST_TIME", "IS_FIRST_TIME");
                                    bulkcopy.ColumnMappings.Add(mapIS_FIRST_TIME);


                                    bulkcopy.WriteToServer(Seldt);
                                    bulkcopy.Close();
                                }

                                con.Close();
                            }
                        }
                        String MODULE_DATABASE1 = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                        SqlParameter[] parameters = new SqlParameter[3];

                        parameters[0] = new SqlParameter("@MODE", "INSERT");
                        parameters[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                        parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters[2].Direction = ParameterDirection.Output;
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "PROCS_INSIDER_USER_UPLOAD_BULKS_DATA", MODULE_DATABASE1, parameters);
                        var obj = parameters[2].Value;





                    }

                    UserBulkUploadResponse objResponse = new UserBulkUploadResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "User Updated Successfully!";
                    return objResponse;

                }
                else
                {
                    UserBulkUploadResponse objResponse = new UserBulkUploadResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Unable to  Updated User!";
                    return objResponse;
                }


            }
            catch (Exception ex)
            {
                UserBulkUploadResponse objResponse = new UserBulkUploadResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }



    }
}
