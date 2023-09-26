using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.IO;
using System.Globalization;
using System.Data.OleDb;
using System.Data;
using ProcsDLL.Models.Infrastructure;
using System.Configuration;
using System.Data.SqlClient;


namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/UserNew")]

    public class UserNewController : ApiController
    {
        #region "Upload Excel Sheet into tables"
        [Route("SaveUserNew")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "UserNew APIs" })]
        public UserNewResponse SaveUserNew()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserNewResponse objResponse = new UserNewResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }

                String sSaveAs = String.Empty;
                String sSaveAs1 = String.Empty;
                String input = HttpContext.Current.Request.Form["Object"];
                //string input1 = string.Empty;
                //using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                //{
                    //input = sr.ReadToEnd();
                //}
                UserNewHeader rel = new JavaScriptSerializer().Deserialize<UserNewHeader>(input);
                rel.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                if (!rel.ValidateInput())
                {
                    UserNewResponse objResponse = new UserNewResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                UserNewRequest pReq = new UserNewRequest(rel);

             
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
                            fname = Path.GetFileNameWithoutExtension(testfiles[testfiles.Length - 1]) + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        else
                        {
                            fname = name + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ext;
                        }
                        sSaveAs = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserBulkUpload/"), fname);

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserBulkUpload/")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserBulkUpload/"));
                        }
                        file.SaveAs(sSaveAs);

                        if (i == 0)
                        {
                            rel.fileName = fname;

                        }
                        else
                        {
                             rel.fileNameUserNew = fname;
                        }
                    }
                }
                else
                {
                    rel.fileName = String.Empty;
                    rel.fileNameUserNew = String.Empty;

                    UserNewResponse objResponse = new UserNewResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "No New User  file uploaded, please try again!";
                    return objResponse;
                }
                rel.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rel.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);

                pReq = new UserNewRequest(rel);
                UserNewResponse pRes = pReq.SaveUserNew();

                // To Import data fro excel sheet to database table

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
                        //data Inser in user bulk table 
                        
                            //create_bulktables();
                            saveUserDocument(con);
                            saveRelativesDocument(con);
                            saveDematDocument(con);
                            saveHoldingDocument(con);
                        con.Close();
                    }
                }
                rel.id = pRes.UserNewHeader.UserNewHdrId;
                rel.UserNewHdrId = pRes.UserNewHeader.UserNewHdrId;

                UserNewResponse objResponse1 = new UserNewResponse();
                objResponse1.StatusFl = true;
                objResponse1.Msg = "New User uploaded successfully !";
                return objResponse1;
               // pReq = new UserNewRequest(rel);

            }
            catch (Exception ex)
            {
                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }

         }
        #endregion "Upload Excel Sheet into tables"

        #region "create_bulktables"
        void create_bulktables()
        {
            try
            {
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                String MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                SqlParameter[] parameters1 = new SqlParameter[3];             
                parameters1[0] = new SqlParameter("@MODE", "CREATE_BULK");
                parameters1[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[2].Direction = ParameterDirection.Output;
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_CREATE_TABLE", MODULE_DATABASE, parameters1);
                var obj1 = parameters1[2].Value;
            }
            catch (Exception ex)
            {
                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
            }

        }
        #endregion "create_bulktables"

        //Insert bulk data from uploaded excel sheet to temporary user table
        #region "saveUserDocument"
        public void saveUserDocument(OleDbConnection con)
        {
            try {

                OleDbCommand oconn = new OleDbCommand("select * from [User$]", con);
                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                DataView dv = new DataView(dt);
                DataTable Seldt = new DataTable();


                Seldt = dv.ToTable("Selected", false, "Email", "Salutation", "Name", "Phone", "Pan", "Login Id", "Employee Id", "Email(Personal)", "DOJ", "DOS", "Role", "Designation", "Department", "Nationality", "Company", "Status", "Type");
                // string str = "Data Source=192.168.1.144;Initial Catalog=PROCS_INSIDER_TRADING;User ID=sa;Password=P@ssw0rd;pooling=false;";
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                String MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string key = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString(), true);
                string connectionString = key;
                string createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    SqlParameter[] parameters1 = new SqlParameter[3];
                    parameters1[0] = new SqlParameter("@MODE", "DELETE_USER");
                    parameters1[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[2].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(parameters1[0]);
                    sqlCommand.Parameters.Add(parameters1[1]);
                    sqlCommand.Parameters.Add(parameters1[2]);
                    sqlCommand.ExecuteNonQuery();
                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, , MODULE_DATABASE, parameters1);
                    var obj1 = parameters1[2].Value;

                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                    {
                        bulkcopy.DestinationTableName = "##PROCS_INSIDER_USER_UPLOAD_BULK_DATA";
                        SqlBulkCopyColumnMapping mapUSER_EMAIL =
                            new SqlBulkCopyColumnMapping("Email", "Email");
                        bulkcopy.ColumnMappings.Add(mapUSER_EMAIL);

                        SqlBulkCopyColumnMapping mapUSER_Salutation =
                            new SqlBulkCopyColumnMapping("Salutation", "Salutation");
                        bulkcopy.ColumnMappings.Add(mapUSER_Salutation);

                        SqlBulkCopyColumnMapping mapName =
                           new SqlBulkCopyColumnMapping("Name", "Name");
                        bulkcopy.ColumnMappings.Add(mapName);

                        SqlBulkCopyColumnMapping mapUSER_Phone =
                           new SqlBulkCopyColumnMapping("Phone", "Phone");
                        bulkcopy.ColumnMappings.Add(mapUSER_Phone);

                        SqlBulkCopyColumnMapping mapIS_Pan =
                           new SqlBulkCopyColumnMapping("Pan", "Pan");
                        bulkcopy.ColumnMappings.Add(mapIS_Pan);

                        SqlBulkCopyColumnMapping mapIS_Login_Id =
                           new SqlBulkCopyColumnMapping("Login Id", "Login_Id");
                        bulkcopy.ColumnMappings.Add(mapIS_Login_Id);

                        SqlBulkCopyColumnMapping mapIS_Employee_Id =
                          new SqlBulkCopyColumnMapping("Employee Id", "Employee_Id");
                        bulkcopy.ColumnMappings.Add(mapIS_Employee_Id);

                        SqlBulkCopyColumnMapping mapIS_Email_Personal =
                          new SqlBulkCopyColumnMapping("Email(Personal)", "Email_Personal");
                        bulkcopy.ColumnMappings.Add(mapIS_Email_Personal);

                        SqlBulkCopyColumnMapping mapIS_DOJ =
                          new SqlBulkCopyColumnMapping("DOJ", "DOJ");
                        bulkcopy.ColumnMappings.Add(mapIS_DOJ);

                        SqlBulkCopyColumnMapping mapIS_DOS =
                          new SqlBulkCopyColumnMapping("DOS", "DOS");
                        bulkcopy.ColumnMappings.Add(mapIS_DOS);

                        SqlBulkCopyColumnMapping mapIS_Role =
                          new SqlBulkCopyColumnMapping("Role", "Role");
                        bulkcopy.ColumnMappings.Add(mapIS_Role);

                        SqlBulkCopyColumnMapping mapIS_Designation =
                          new SqlBulkCopyColumnMapping("Designation", "Designation");
                        bulkcopy.ColumnMappings.Add(mapIS_Designation);

                        SqlBulkCopyColumnMapping mapIS_Department =
                          new SqlBulkCopyColumnMapping("Department", "Department");
                        bulkcopy.ColumnMappings.Add(mapIS_Department);

                        SqlBulkCopyColumnMapping mapIS_Nationality =
                          new SqlBulkCopyColumnMapping("Nationality", "Nationality");
                        bulkcopy.ColumnMappings.Add(mapIS_Nationality);

                        SqlBulkCopyColumnMapping mapIS_Company =
                          new SqlBulkCopyColumnMapping("Company", "Company");
                        bulkcopy.ColumnMappings.Add(mapIS_Company);

                        SqlBulkCopyColumnMapping mapIS_Status =
                          new SqlBulkCopyColumnMapping("Status", "Status");
                        bulkcopy.ColumnMappings.Add(mapIS_Status);

                        SqlBulkCopyColumnMapping mapIS_Type =
                          new SqlBulkCopyColumnMapping("Type", "Type");
                        bulkcopy.ColumnMappings.Add(mapIS_Type);


                        bulkcopy.WriteToServer(Seldt);
                        bulkcopy.Close();
                    }
                    int company_id = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    String MODULE_DATABASE1 = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    SqlParameter[] parameters = new SqlParameter[5];

                    parameters[0] = new SqlParameter("@MODE", "INSERT_USERS");
                    parameters[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters[2] = new SqlParameter("@COMPANY_ID", company_id);
                    parameters[3]=new SqlParameter("@login_id", createdBy);
                    parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[4].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand1 = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand1.CommandType = CommandType.StoredProcedure;
                    sqlCommand1.Parameters.Add(parameters[0]);
                    sqlCommand1.Parameters.Add(parameters[1]);
                    sqlCommand1.Parameters.Add(parameters[2]);
                    sqlCommand1.Parameters.Add(parameters[3]);
                    sqlCommand1.Parameters.Add(parameters[4]);

                    sqlCommand1.ExecuteNonQuery();
                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE1, parameters);
                    var obj = parameters[4].Value;
                }
            }
            catch (Exception ex)
            {
                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
               
            }
           
        }
        #endregion "saveUserDocument"
    

        //Insert bulk data from uploaded excel sheet to temporary Relatives table
        #region "saveRelativesDocument"
        public void saveRelativesDocument(OleDbConnection con)
        {
            try {


                OleDbCommand oconn = new OleDbCommand("select * from [Relatives$]", con);
                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                DataTable dt = new DataTable();

                adp.Fill(dt);



                DataView dv = new DataView(dt);
                DataTable Seldt = new DataTable();



                Seldt = dv.ToTable("Selected", false, "Login Id", "Relation", "Relative Name", "Relative Pan", "Relative Email", "Status");
                // string str = "Data Source=192.168.1.144;Initial Catalog=PROCS_INSIDER_TRADING;User ID=sa;Password=P@ssw0rd;pooling=false;";
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                String MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string key = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString(), true);// @"Data Source=WIN-P99G4H9GTJL;Initial Catalog=MIS_JF;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";// (string)settingsReader.GetValue("connectionString", typeof(String));
                string connectionString = key;
                string createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    SqlParameter[] parameters1 = new SqlParameter[3];
                    parameters1[0] = new SqlParameter("@MODE", "DELETE_RELATIVE");
                    parameters1[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[2].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(parameters1[0]);
                    sqlCommand.Parameters.Add(parameters1[1]);
                    sqlCommand.Parameters.Add(parameters1[2]);
                    sqlCommand.ExecuteNonQuery();
                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE, parameters1);
                    var obj1 = parameters1[2].Value;

                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                    {
                        bulkcopy.DestinationTableName = "##PROCS_INSIDER_RELATIVES_UPLOAD_BULK_DATA";
                        SqlBulkCopyColumnMapping mapLoginId =
                            new SqlBulkCopyColumnMapping("Login Id", "Login_Id");
                        bulkcopy.ColumnMappings.Add(mapLoginId);

                        SqlBulkCopyColumnMapping mapRelation =
                            new SqlBulkCopyColumnMapping("Relation", "Relation");
                        bulkcopy.ColumnMappings.Add(mapRelation);

                        SqlBulkCopyColumnMapping mapRelativeName =
                           new SqlBulkCopyColumnMapping("Relative Name", "Relative_Name");
                        bulkcopy.ColumnMappings.Add(mapRelativeName);

                        SqlBulkCopyColumnMapping mapRelativePan =
                           new SqlBulkCopyColumnMapping("Relative Pan", "Relative_Pan");
                        bulkcopy.ColumnMappings.Add(mapRelativePan);

                        SqlBulkCopyColumnMapping mapIS_RelativeEmail =
                           new SqlBulkCopyColumnMapping("Relative Email", "Relative_Email");
                        bulkcopy.ColumnMappings.Add(mapIS_RelativeEmail);

                        SqlBulkCopyColumnMapping mapIS_Status =
                          new SqlBulkCopyColumnMapping("Status", "Status");
                        bulkcopy.ColumnMappings.Add(mapIS_Status);



                        bulkcopy.WriteToServer(Seldt);
                        bulkcopy.Close();
                    }
                    int company_id = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    String MODULE_DATABASE1 = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    SqlParameter[] parameters = new SqlParameter[5];

                    parameters[0] = new SqlParameter("@MODE", "INSERT_RELATIVES");
                    parameters[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters[2] = new SqlParameter("@COMPANY_ID", company_id);
                    parameters[3] = new SqlParameter("@login_id", createdBy);
                    parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[4].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand1 = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand1.CommandType = CommandType.StoredProcedure;
                    sqlCommand1.Parameters.Add(parameters[0]);
                    sqlCommand1.Parameters.Add(parameters[1]);
                    sqlCommand1.Parameters.Add(parameters[2]);
                    sqlCommand1.Parameters.Add(parameters[3]);
                    sqlCommand1.Parameters.Add(parameters[4]);
                    sqlCommand1.ExecuteNonQuery();
                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE1, parameters);
                    var obj = parameters[4].Value;
                }
            }
            catch (Exception ex) {

                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
               
            }

        }
        #endregion "saveRelativesDocument"

        //Insert bulk data from uploaded excel sheet to temporary Demat table
        #region "save Demat Document"
        public void saveDematDocument(OleDbConnection con)
        {
            try
            {

                OleDbCommand oconn = new OleDbCommand("select * from [Demat$]", con);
                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                DataView dv = new DataView(dt);
                DataTable Seldt = new DataTable();

                Seldt = dv.ToTable("Selected", false, "Login Id", "Demat For", "Relative Name", "Depository", "DPId", "Client Id", "Status");
                // string str = "Data Source=192.168.1.144;Initial Catalog=PROCS_INSIDER_TRADING;User ID=sa;Password=P@ssw0rd;pooling=false;";
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                String MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string key = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString(), true);// @"Data Source=WIN-P99G4H9GTJL;Initial Catalog=MIS_JF;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";// (string)settingsReader.GetValue("connectionString", typeof(String));
                string connectionString = key;
                string createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    SqlParameter[] parameters1 = new SqlParameter[3];

                    parameters1[0] = new SqlParameter("@MODE", "DELETE_DEMAT");
                    parameters1[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[2].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(parameters1[0]);
                    sqlCommand.Parameters.Add(parameters1[1]);
                    sqlCommand.Parameters.Add(parameters1[2]);
                    sqlCommand.ExecuteNonQuery();
                    // SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE, parameters1);
                    var obj1 = parameters1[2].Value;

                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                    {
                        bulkcopy.DestinationTableName = "##PROCS_INSIDER_DEMAT_UPLOAD_BULK_DATA";
                        SqlBulkCopyColumnMapping mapLoginId =
                            new SqlBulkCopyColumnMapping("Login Id", "Login_Id");
                        bulkcopy.ColumnMappings.Add(mapLoginId);

                        SqlBulkCopyColumnMapping mapRelation =
                            new SqlBulkCopyColumnMapping("Demat For", "Demat_For");
                        bulkcopy.ColumnMappings.Add(mapRelation);

                        SqlBulkCopyColumnMapping mapRelativeName =
                           new SqlBulkCopyColumnMapping("Relative Name", "Relative_Name");
                        bulkcopy.ColumnMappings.Add(mapRelativeName);

                        SqlBulkCopyColumnMapping mapRelativePan =
                           new SqlBulkCopyColumnMapping("Depository", "Depository");
                        bulkcopy.ColumnMappings.Add(mapRelativePan);

                        SqlBulkCopyColumnMapping mapIS_RelativeEmail =
                           new SqlBulkCopyColumnMapping("DPId", "DPId");
                        bulkcopy.ColumnMappings.Add(mapIS_RelativeEmail);

                        SqlBulkCopyColumnMapping mapIS_ClientId =
                          new SqlBulkCopyColumnMapping("Client Id", "Client_Id");
                        bulkcopy.ColumnMappings.Add(mapIS_ClientId);

                        SqlBulkCopyColumnMapping mapIS_Status =
                        new SqlBulkCopyColumnMapping("Status", "Status");
                        bulkcopy.ColumnMappings.Add(mapIS_Status);


                        bulkcopy.WriteToServer(Seldt);
                        bulkcopy.Close();
                    }
                    int company_id = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    String MODULE_DATABASE1 = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    SqlParameter[] parameters = new SqlParameter[5];

                    parameters[0] = new SqlParameter("@MODE", "INSERT_DEMAT");
                    parameters[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters[2] = new SqlParameter("@COMPANY_ID", company_id);
                    parameters[3] = new SqlParameter("@login_id", createdBy);
                    parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[4].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand1 = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand1.CommandType = CommandType.StoredProcedure;
                    sqlCommand1.Parameters.Add(parameters[0]);
                    sqlCommand1.Parameters.Add(parameters[1]);
                    sqlCommand1.Parameters.Add(parameters[2]);
                    sqlCommand1.Parameters.Add(parameters[3]);
                    sqlCommand1.Parameters.Add(parameters[4]);

                    sqlCommand1.ExecuteNonQuery();

                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE1, parameters);
                    var obj = parameters[4].Value;
                }
            }
            catch (Exception ex) {
                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                

            }

           
        }
        #endregion "save Demat Document"

        //Insert bulk data from uploaded excel sheet to temporary Holding table
        #region "save Holding Document"
        public void saveHoldingDocument(OleDbConnection con)
        {

            try
            {

                OleDbCommand oconn = new OleDbCommand("select * from [Holding$]", con);
                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                DataView dv = new DataView(dt);
                DataTable Seldt = new DataTable();


                Seldt = dv.ToTable("Selected", false, "Login Id", "DPId", "Client Id", "Company ISIN", "Holding");
                // string str = "Data Source=192.168.1.144;Initial Catalog=PROCS_INSIDER_TRADING;User ID=sa;Password=P@ssw0rd;pooling=false;";
                string str = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                String MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                string key = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["ConnectionString"].ToString(), true);// @"Data Source=WIN-P99G4H9GTJL;Initial Catalog=MIS_JF;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";// (string)settingsReader.GetValue("connectionString", typeof(String));
                string connectionString = key;
                string createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    SqlParameter[] parameters1 = new SqlParameter[3];

                    parameters1[0] = new SqlParameter("@MODE", "DELETE_HOLDING");
                    parameters1[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[2].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(parameters1[0]);
                    sqlCommand.Parameters.Add(parameters1[1]);
                    sqlCommand.Parameters.Add(parameters1[2]);
                    sqlCommand.ExecuteNonQuery();
                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE, parameters1);
                    var obj1 = parameters1[2].Value;
             
                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(str))
                {
                    bulkcopy.DestinationTableName = "##PROCS_INSIDER_Holding_UPLOAD_BULK_DATA";
                    SqlBulkCopyColumnMapping mapLoginId =
                        new SqlBulkCopyColumnMapping("Login Id", "Login_Id");
                    bulkcopy.ColumnMappings.Add(mapLoginId);

                    SqlBulkCopyColumnMapping mapDPId =
                        new SqlBulkCopyColumnMapping("DPId", "DPId");
                    bulkcopy.ColumnMappings.Add(mapDPId);

                    SqlBulkCopyColumnMapping mapClientId =
                       new SqlBulkCopyColumnMapping("Client Id", "Client_Id");
                    bulkcopy.ColumnMappings.Add(mapClientId);

                    SqlBulkCopyColumnMapping mapCompanyISIN =
                       new SqlBulkCopyColumnMapping("Company ISIN", "Company_ISIN");
                    bulkcopy.ColumnMappings.Add(mapCompanyISIN);

                    SqlBulkCopyColumnMapping mapIS_Holding =
                       new SqlBulkCopyColumnMapping("Holding", "Holding");
                    bulkcopy.ColumnMappings.Add(mapIS_Holding);
                        
                    bulkcopy.WriteToServer(Seldt);
                    bulkcopy.Close();
                }
                    int company_id = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                    String MODULE_DATABASE1 = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                    SqlParameter[] parameters = new SqlParameter[5];

                    parameters[0] = new SqlParameter("@MODE", "INSERT_HOLDING");
                    parameters[1] = new SqlParameter("@ADMIN_DB", CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true));
                    parameters[2] = new SqlParameter("@COMPANY_ID", company_id);
                    parameters[3] = new SqlParameter("@login_id", createdBy);
                    parameters[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[4].Direction = ParameterDirection.Output;
                    SqlCommand sqlCommand1 = new SqlCommand("SP_USER_UPLOAD_BULK_DATA", connection);
                    sqlCommand1.CommandType = CommandType.StoredProcedure;
                    sqlCommand1.Parameters.Add(parameters[0]);
                    sqlCommand1.Parameters.Add(parameters[1]);
                    sqlCommand1.Parameters.Add(parameters[2]);
                    sqlCommand1.Parameters.Add(parameters[3]);
                    sqlCommand1.Parameters.Add(parameters[4]);

                    sqlCommand1.ExecuteNonQuery();

                    //SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_USER_UPLOAD_BULK_DATA", MODULE_DATABASE1, parameters);
                    var obj = parameters[4].Value;
                }
            }
            catch (Exception ex) {
                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                

            }
           
        }
        #endregion "save Holding Document"

        #region "Get UserNew List"

        [Route("GetUserNewList")]
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "UserNew APIs" })]
        public UserNewResponse GetUserNewList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    UserNewResponse objResponse = new UserNewResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                UserNewHeader UserNewHdr = new UserNewHeader();

                UserNewHdr.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                UserNewHdr.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                UserNewHdr.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                if (!UserNewHdr.ValidateInput())
                {
                    UserNewResponse objResponse = new UserNewResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                UserNewRequest UserNewHdrList = new UserNewRequest(UserNewHdr);
                UserNewResponse gResUserNewHdrList = UserNewHdrList.GetUserNewList();
                return gResUserNewHdrList;
            }
            catch (Exception ex)
            {
                UserNewResponse objResponse = new UserNewResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        #endregion "Get UserNew List"

    }
}

