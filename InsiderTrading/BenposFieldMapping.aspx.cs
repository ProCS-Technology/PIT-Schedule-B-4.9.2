using ProcsDLL.Models.Infrastructure;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
namespace ProcsDLL.InsiderTrading
{
    public partial class BenposFieldMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetMappedField();
            }
        }
        protected void SaveBenposField(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                var CreatedBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                var database = Convert.ToString(Session["ModuleDatabase"]);
                var TemplatType = DropDownListType.SelectedValue.ToString();

                DataTable dtresult = new DataTable();
                dtresult.Columns.AddRange(new DataColumn[3] {
                new DataColumn("EXCEL_FIELD_NAME", typeof(string)),
                new DataColumn("EXCEL_FIELD_TYPE", typeof(string)),
                new DataColumn("Company_ID", typeof(string)) });

                if (!string.IsNullOrEmpty(TextBoxName.Text))
                {
                    dtresult.Rows.Add(TextBoxName.Text, "NAME", CompanyId);
                }
                if (!string.IsNullOrEmpty(TextBoxHolding.Text))
                {
                    dtresult.Rows.Add(TextBoxHolding.Text, "HOLDING", CompanyId);
                }
                if (!string.IsNullOrEmpty(TextBoxFolio.Text))
                {
                    dtresult.Rows.Add(TextBoxFolio.Text, "FOLIO", CompanyId);
                }
                if (!string.IsNullOrEmpty(TextBoxPan.Text))
                {
                    dtresult.Rows.Add(TextBoxPan.Text, "PAN", CompanyId);
                }
                if (!string.IsNullOrEmpty(TextBoxCategory.Text))
                {
                    dtresult.Rows.Add(TextBoxCategory.Text, "TYPE", CompanyId);
                }
                if (dtresult.Rows.Count > 0)
                {
                    String ConnectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);

                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand("SP_BENPOS_FIELD_MAPPING", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MODE", SqlDbType.NVarChar).Value = "Insert";
                        cmd.Parameters.Add("@CREATED_BY", SqlDbType.NVarChar).Value = CreatedBy;
                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = CompanyId;
                        cmd.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = DropDownListType.SelectedValue;
                        cmd.Parameters.AddWithValue("@MappedField", dtresult);
                        try
                        {
                            con.Open();
                            con.ChangeDatabase(database);
                            cmd.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Saved Successfully')", true);
                            GetMappedField();
                        }
                        catch (SqlException ex)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Processing failed because of system error')", true);
                        }
                        con.Close();
                    }
                }
            }

        }
        protected void GetMappedField()
        {

            if (!String.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
            {
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                var database = Convert.ToString(Session["ModuleDatabase"]);

                DataTable dt = new DataTable();
                String ConnectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionString"]), true);

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SP_BENPOS_FIELD_MAPPING", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MODE", SqlDbType.NVarChar).Value = "GET";
                        cmd.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = DropDownListType.SelectedValue;
                        cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Int).Value = CompanyId;

                        try
                        {
                            con.Open();
                            con.ChangeDatabase(database);
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["EXCEL_FIELD_TYPE"].ToString() == "FOLIO")
                                {
                                    TextBoxFolio.Text = row["EXCEL_FIELD_NAME"].ToString();
                                }
                                else if (row["EXCEL_FIELD_TYPE"].ToString() == "NAME")
                                {
                                    TextBoxName.Text = row["EXCEL_FIELD_NAME"].ToString();
                                }
                                else if (row["EXCEL_FIELD_TYPE"].ToString() == "PAN")
                                {
                                    TextBoxPan.Text = row["EXCEL_FIELD_NAME"].ToString();
                                }
                                else if (row["EXCEL_FIELD_TYPE"].ToString() == "HOLDING")
                                {
                                    TextBoxHolding.Text = row["EXCEL_FIELD_NAME"].ToString();
                                }
                                else if (row["EXCEL_FIELD_TYPE"].ToString() == "TYPE")
                                {
                                    TextBoxCategory.Text = row["EXCEL_FIELD_NAME"].ToString();
                                }
                                else
                                {

                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alert('Processing failed because of system error')", true);

                        }
                        con.Close();
                    }
                }
            }
        }
        protected void DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMappedField();
        }
    }
}