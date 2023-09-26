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
    public class BusinessCompanyModuleRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();

        public BusinessCompanyModuleResponse AddBusinessCompanyModule(BusinessCompanyModule objBCM)
        {
            BusinessCompanyModuleResponse oBCM = new BusinessCompanyModuleResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_COMPANY_MODULE_SETUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objBCM.COMPANY_ID));
                        cmd.Parameters.Add(new SqlParameter("@MODULE_ID", objBCM._MODULE_ID));
                        cmd.Parameters.Add(new SqlParameter("@MODULE_STATUS", objBCM.MODULE_STATUS));
                        cmd.Parameters.Add(new SqlParameter("@MODULE_ST_DT", objBCM.MODULE_st_dt));
                        cmd.Parameters.Add(new SqlParameter("@MODULE_EN_DT", objBCM.MODULE_en_dt));
                        cmd.Parameters.Add(new SqlParameter("@Mode", "CHECK"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objBCM.createdBy));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;

                        if ((Int32)obj == 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objBCM.COMPANY_ID));
                            cmd.Parameters.Add(new SqlParameter("@MODULE_ID", objBCM._MODULE_ID));
                            cmd.Parameters.Add(new SqlParameter("@MODULE_STATUS", objBCM.MODULE_STATUS));
                            cmd.Parameters.Add(new SqlParameter("@MODULE_ST_DT", objBCM.MODULE_st_dt));
                            cmd.Parameters.Add(new SqlParameter("@MODULE_EN_DT", objBCM.MODULE_en_dt));
                            cmd.Parameters.Add(new SqlParameter("@Mode", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                            cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objBCM.createdBy));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            oBCM.StatusFl = true;
                            oBCM.Msg = "Data has been saved successfully !";
                            oBCM.BusinessCompanyModule = objBCM;
                        }
                        else
                        {
                            oBCM.StatusFl = false;
                            oBCM.Msg = "Business Company Module Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oBCM = new BusinessCompanyModuleResponse();
                oBCM.StatusFl = false;
                oBCM.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oBCM;
        }

        public BusinessCompanyModuleResponse UpdateBusinessCompanyModule(BusinessCompanyModule objBCM)
        {
            BusinessCompanyModuleResponse oBcm = new BusinessCompanyModuleResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_COMPANY_MODULE_SETUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objBCM.COMPANY_ID));
                        // parameters[1] = new SqlParameter("@MODULE_ID", objBCM.MODULE_ID);
                        //  parameters[2] = new SqlParameter("@MODULE_STATUS", objBCM.MODULE_STATUS);
                        //    parameters[3] = new SqlParameter("@MODULE_ST_DT", objBCM.MODULE_st_dt);
                        //   parameters[4] = new SqlParameter("@MODULE_EN_DT", objBCM.MODULE_en_dt);
                        cmd.Parameters.Add(new SqlParameter("@Mode", "CHECK"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE"));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objBCM.createdBy));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objBCM.COMPANY_ID));
                            cmd.Parameters.Add(new SqlParameter("@Mode", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objBCM.createdBy));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            oBcm.StatusFl = true;
                            oBcm.Msg = "Data has been updated successfully !";
                            oBcm.BusinessCompanyModule = objBCM;
                        }
                        else
                        {
                            oBcm.StatusFl = false;
                            oBcm.Msg = "Business Company Module Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oBcm = new BusinessCompanyModuleResponse();
                oBcm.StatusFl = false;
                oBcm.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oBcm;
        }

        public BusinessCompanyModuleResponse DeleteBusinessCompanyModule(BusinessCompanyModule objBCM)
        {
            BusinessCompanyModuleResponse oBcm = new BusinessCompanyModuleResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_COMPANY_MODULE_SETUP", conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objBCM.COMPANY_ID));
                        //  parameters[1] = new SqlParameter("@MODULE_ID", objBCM.MODULE_ID);
                        cmd.Parameters.Add(new SqlParameter("@Mode", "Delete_BUSINESS_COMPANY_MODULE"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        oBcm.StatusFl = true;
                        oBcm.Msg = "Data has been deleted successfully !";
                        oBcm.BusinessCompanyModule = objBCM;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oBcm = new BusinessCompanyModuleResponse();
                oBcm.StatusFl = false;
                oBcm.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oBcm;
        }

        public BusinessCompanyModuleResponse GetBusinessCompanyModuleList()
        {
            BusinessCompanyModuleResponse oBcm = new BusinessCompanyModuleResponse();
            oBcm.StatusFl = false;
            oBcm.Msg = "No data found !";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_COMPANY_MODULE_SETUP", conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@Mode", "GET_BUSINESS_COMPANY_MODULE_List"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                BusinessCompanyModule obj = new BusinessCompanyModule();
                                obj.GROUP_ID = Convert.ToInt32(rdr.GetValue(0));
                                obj.GROUP_NAME = Convert.ToString(rdr.GetValue(1));
                                obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(2));
                                obj.COMPANY_NM = Convert.ToString(rdr.GetValue(3));
                                obj._MODULE_ID = Convert.ToInt32(rdr.GetValue(4));
                                obj._MODULE_NM = Convert.ToString(rdr.GetValue(5));
                                obj.MODULE_STATUS = Convert.ToString(rdr.GetValue(6));
                                obj.MODULE_st_dt = Convert.ToString(rdr.GetValue(7));
                                obj.MODULE_en_dt = Convert.ToString(rdr.GetValue(8));
                                obj.createdBy = Convert.ToString(rdr.GetValue(9));
                                obj.createOn = Convert.ToString(rdr.GetValue(10));
                                oBcm.AddObject(obj);
                            }
                            oBcm.StatusFl = true;
                            oBcm.Msg = "Data has been fetched successfully !";
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oBcm = new BusinessCompanyModuleResponse();
                oBcm.StatusFl = false;
                oBcm.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oBcm;
        }
    }
}