using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.Model;
using ProcsDLL.Models.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace ProcsDLL.Models.Repository
{
    public class ModuleRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();

        public ModuleResponse GetModuleList()
        {
            ModuleResponse oModule = new ModuleResponse();
            oModule.StatusFl = false;
            oModule.Msg = "No data found !";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_MODULE_SETUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@Mode", "GET_MODULE_List"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Module obj = new Module();
                                obj._MODULE_ID = Convert.ToInt32(rdr.GetValue(0));
                                obj._MODULE_NM = Convert.ToString(rdr.GetValue(1));
                                obj._MODULE_FOLDER = Convert.ToString(rdr.GetValue(2));
                                obj._DATABASE_NAME = Convert.ToString(rdr.GetValue(3));
                                oModule.AddObject(obj);
                            }
                            oModule.StatusFl = true;
                            oModule.Msg = "Data has been fetched successfully !";
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oModule = new ModuleResponse();
                oModule.StatusFl = false;
                oModule.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oModule;
        }
    }
}