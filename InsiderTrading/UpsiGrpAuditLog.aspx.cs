using ProcsDLL.Models.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
namespace ProcsDLL.InsiderTrading
{
    public partial class UpsiGrpAuditLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Gid"] != null)
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@COMPANY_ID", Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                    parameters[1] = new SqlParameter("@MODE", "GET_UPSI_GROUP_LOG");
                    parameters[2] = new SqlParameter("@GROUP_ID", Convert.ToInt32(Request.QueryString["Gid"]));
                    parameters[3] = new SqlParameter("@AdminDb", Convert.ToString(Session["AdminDb"]));

                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UPSI_GROUP_LOG", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameters);

                    if (ds.Tables.Count > 0)
                    {
                        RepeaterUPSIGroup.DataSource = ds.Tables[0];
                        RepeaterUPSIGroup.DataBind();

                        //RepeaterCommunication.DataSource = ds.Tables[1];
                        //RepeaterCommunication.DataBind();

                        RepeaterMember.DataSource = ds.Tables[3];
                        RepeaterMember.DataBind();

                        RepeaterCp.DataSource = ds.Tables[4];
                        RepeaterCp.DataBind();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}