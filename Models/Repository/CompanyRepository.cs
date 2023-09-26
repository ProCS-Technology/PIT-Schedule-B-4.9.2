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
    public class CompanyRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();

        public companyResponse AddCompany(Company objcompany)
        {
            companyResponse ocmp = new companyResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_COMPANY_SETUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objcompany.GROUP_ID));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objcompany.COMPANY_ID));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_NM", objcompany.COMPANY_NM));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_STATUS", objcompany.COMPANY_STATUS));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objcompany.subscription_st_dt));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objcompany.SUBSCRIPTION_en_dt));
                        cmd.Parameters.Add(new SqlParameter("@Logo", objcompany.Logo));
                        cmd.Parameters.Add(new SqlParameter("@MODE", "CHECK"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", objcompany.createdBy));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objcompany.GROUP_ID));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objcompany.COMPANY_ID));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_NM", objcompany.COMPANY_NM));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_STATUS", objcompany.COMPANY_STATUS));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objcompany.subscription_st_dt));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objcompany.SUBSCRIPTION_en_dt));
                            cmd.Parameters.Add(new SqlParameter("@Logo", objcompany.Logo));
                            cmd.Parameters.Add(new SqlParameter("@MODE", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", objcompany.createdBy));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@MODE", "GET_COMPANY_ID_BY_Company_NAME"));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_NM", objcompany.COMPANY_NM));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            objcompany.COMPANY_ID = Convert.ToInt32(cmd.ExecuteScalar());

                            ocmp.StatusFl = true;
                            ocmp.Msg = "Data has been saved successfully !";
                            ocmp.company = objcompany;
                        }
                        else
                        {
                            ocmp.StatusFl = false;
                            ocmp.Msg = "Company Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ocmp = new companyResponse();
                ocmp.StatusFl = false;
                ocmp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return ocmp;
        }

        public companyResponse UpdateCompany(Company objcompany)
        {
            companyResponse ocmp = new companyResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_COMPANY_SETUP", conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objcompany.GROUP_ID));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objcompany.COMPANY_ID));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_NM", objcompany.COMPANY_NM));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_STATUS", objcompany.COMPANY_STATUS));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objcompany.subscription_st_dt));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objcompany.SUBSCRIPTION_en_dt));
                        cmd.Parameters.Add(new SqlParameter("@Logo", objcompany.Logo));
                        cmd.Parameters.Add(new SqlParameter("@MODE", "CHECK"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE"));
                        cmd.Parameters.Add(new SqlParameter("@CreatedBy", objcompany.createdBy));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 0)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objcompany.GROUP_ID));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objcompany.COMPANY_ID));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_NM", objcompany.COMPANY_NM));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_STATUS", objcompany.COMPANY_STATUS));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objcompany.subscription_st_dt));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objcompany.SUBSCRIPTION_en_dt));
                            cmd.Parameters.Add(new SqlParameter("@Logo", objcompany.Logo));
                            cmd.Parameters.Add(new SqlParameter("@MODE", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@CreatedBy", objcompany.createdBy));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Add(new SqlParameter("@MODE", "GET_COMPANY_ID_BY_Company_NAME"));
                            cmd.Parameters.Add(new SqlParameter("@COMPANY_NM", objcompany.COMPANY_NM));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            objcompany.COMPANY_ID = (Int32)cmd.ExecuteScalar();

                            ocmp.StatusFl = true;
                            ocmp.Msg = "Data has been updated successfully !";
                            ocmp.company = objcompany;
                        }
                        else
                        {
                            ocmp.StatusFl = false;
                            ocmp.Msg = "Group Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ocmp = new companyResponse();
                ocmp.StatusFl = false;
                ocmp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return ocmp;
        }

        public companyResponse GetCompanyList()
        {
            companyResponse oCmpy = new companyResponse();
            oCmpy.StatusFl = false;
            oCmpy.Msg = "No data found !";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_COMPANY_SETUP", conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@Mode", "GET_Company_List"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Company obj = new Company();
                                obj.GROUP_ID = Convert.ToInt32(rdr.GetValue(0));
                                obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(1));
                                obj.COMPANY_NM = Convert.ToString(rdr.GetValue(2));
                                obj.COMPANY_STATUS = Convert.ToString(rdr.GetValue(3));
                                obj.subscription_st_dt = Convert.ToString(rdr.GetValue(4));
                                obj.SUBSCRIPTION_en_dt = Convert.ToString(rdr.GetValue(5));
                                obj.createdBy = Convert.ToString(rdr.GetValue(6));
                                obj.createOn = Convert.ToString(rdr.GetValue(7));
                                obj.Logo = Convert.ToString(rdr.GetValue(8));
                                oCmpy.AddObject(obj);
                            }
                            oCmpy.StatusFl = true;
                            oCmpy.Msg = "Data has been fetched successfully !";
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oCmpy = new companyResponse();
                oCmpy.StatusFl = false;
                oCmpy.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oCmpy;
        }

        public companyResponse GetCompanyList(Company objComp)
        {
            companyResponse oCmpy = new companyResponse();
            oCmpy.StatusFl = false;
            oCmpy.Msg = "No data found !";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_COMPANY_SETUP", conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@Mode", "GET_Company_List_Group_Id"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objComp.GROUP_ID));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Company obj = new Company();
                                obj.GROUP_ID = Convert.ToInt32(rdr.GetValue(0));
                                obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(1));
                                obj.COMPANY_NM = Convert.ToString(rdr.GetValue(2));
                                obj.COMPANY_STATUS = Convert.ToString(rdr.GetValue(3));
                                obj.subscription_st_dt = Convert.ToString(rdr.GetValue(4));
                                obj.SUBSCRIPTION_en_dt = Convert.ToString(rdr.GetValue(5));
                                obj.createdBy = Convert.ToString(rdr.GetValue(6));
                                obj.createOn = Convert.ToString(rdr.GetValue(7));
                                obj.Logo = Convert.ToString(rdr.GetValue(8));
                                oCmpy.AddObject(obj);
                            }
                            oCmpy.StatusFl = true;
                            oCmpy.Msg = "Data has been fetched successfully !";
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oCmpy = new companyResponse();
                oCmpy.StatusFl = false;
                oCmpy.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oCmpy;
        }

        public companyResponse DeleteCompany(Company objCompany)
        {
            companyResponse oComp = new companyResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_COMPANY_SETUP", conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objCompany.COMPANY_ID));
                        cmd.Parameters.Add(new SqlParameter("@Mode", "Delete_Company"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        oComp.StatusFl = true;
                        oComp.Msg = "Data has been deleted successfully !";
                        oComp.company = objCompany;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oComp = new companyResponse();
                oComp.StatusFl = false;
                oComp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oComp;
        }
    }
}