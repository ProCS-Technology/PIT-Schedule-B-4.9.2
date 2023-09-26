using ProcsDLL.Models.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ProcsDLL.InsiderTrading
{
    public partial class DataUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadExcel.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUploadExcel.PostedFile.FileName);
                    string extension = Path.GetExtension(FileUploadExcel.PostedFile.FileName);

                    if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                    {

                        string excelPath = Server.MapPath("~/UserBulkUpload/" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + extension);

                        FileUploadExcel.SaveAs(excelPath);

                        string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties='Excel 8.0;HDR=Yes'";
                        string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 8.0;HDR=Yes'";

                        string conString = string.Empty;

                        switch (extension)
                        {
                            case ".xls":
                                conString = string.Format(Excel03ConString, excelPath, "YES");
                                break;
                            case ".xlsx":
                                conString = string.Format(Excel07ConString, excelPath, "YES");
                                break;

                        }
                        conString = string.Format(conString, excelPath);
                        using (OleDbConnection con = new OleDbConnection(conString))
                        {
                            con.Open();

                            DataTable dtSheet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                            if (dtSheet.Rows.Count > 0)
                            {
                                // delete previous data
                                CopyToMainTable("Delete");

                                foreach (DataRow drSheet in dtSheet.Rows)
                                {
                                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                                    {
                                        string sSheetNm = drSheet["TABLE_NAME"].ToString();
                                        if (!sSheetNm.Contains("FilterDatabase"))
                                        {
                                            OleDbCommand oconn = new OleDbCommand("select top 1 * from [" + sSheetNm + "]", con);
                                            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                                            DataTable dt = new DataTable();

                                            adp.Fill(dt);
                                            dt.Rows.Clear();
                                            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM[" + sSheetNm + "]", con))
                                            {
                                                oda.Fill(dt);
                                            }

                                            string strCon = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);

                                            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(strCon))
                                            {
                                                bulkcopy.DestinationTableName = "PROCS_INSIDER_TEMP_" + sSheetNm.TrimEnd('$');
                                                bulkcopy.WriteToServer(dt);
                                            }
                                        }
                                    }

                                }

                                CopyToMainTable("Insert");
                                LABEL_MSG.Text = "DATA UPLOADED SUCCESFULLY";
                            }
                            con.Close();
                        }

                    }

                    else
                    {
                        LABEL_MSG.Text = "Invalid file format";
                    }
                }
                catch (Exception ex)
                {
                    LABEL_MSG.Text = ex.ToString();
                }
            }
        }

        protected void CopyToMainTable(string ActionMode)
        {

            string ConnectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("PROCS_INSIDER_USER_BULK_UPLOAD", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ADMIN_DB", SqlDbType.NVarChar).Value = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["AdminDB"]), true);
                cmd.Parameters.AddWithValue("@MODE", SqlDbType.NVarChar).Value = ActionMode;
                cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                cmd.Parameters.Add("@ModuleId", SqlDbType.Int).Value = int.Parse(HttpContext.Current.Session["ModuleId"].ToString());
                cmd.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = Convert.ToInt32(HttpContext.Current.Session["BusinessUnitId"]);
                var BId = HttpContext.Current.Session["EmployeeId"];

                cmd.Parameters.Add("@CREATED_BY", SqlDbType.NVarChar).Value = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                try
                {
                    con.Open();
                    cmd.ExecuteScalar();

                }
                catch (SqlException ex)
                {
                    LABEL_MSG.Text = "Database error!" + ex.ToString();

                }
                con.Close();
            }
        }
    }
}