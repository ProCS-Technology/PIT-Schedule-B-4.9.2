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
    public class GroupRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();

        public GroupResponse AddGroup(Group objGroup)
        {
            GroupResponse oGrp = new GroupResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_GROUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objGroup.GROUP_ID));
                        cmd.Parameters.Add(new SqlParameter("@GROUP_NM", objGroup.GROUP_NM));
                        cmd.Parameters.Add(new SqlParameter("@GROUP_STATUS", objGroup.GROUP_STATUS));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objGroup.SUBSCRIPTION_ST_DT));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objGroup.SUBSCRIPTION_EN_DT));
                        cmd.Parameters.Add(new SqlParameter("@LOGO", objGroup.LOGO));
                        cmd.Parameters.Add(new SqlParameter("@Mode", "CHECK"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objGroup.CREATE_BY));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objGroup.GROUP_ID));
                            cmd.Parameters.Add(new SqlParameter("@GROUP_NM", objGroup.GROUP_NM));
                            cmd.Parameters.Add(new SqlParameter("@GROUP_STATUS", objGroup.GROUP_STATUS));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objGroup.SUBSCRIPTION_ST_DT));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objGroup.SUBSCRIPTION_EN_DT));
                            cmd.Parameters.Add(new SqlParameter("@LOGO", objGroup.LOGO));
                            cmd.Parameters.Add(new SqlParameter("@Mode", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                            cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objGroup.CREATE_BY));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@Mode", "GET_GROUP_ID_BY_GROUP_NAME"));
                            cmd.Parameters.Add(new SqlParameter("@GROUP_NM", objGroup.GROUP_NM));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            objGroup.GROUP_ID = (Int32)cmd.ExecuteScalar();

                            oGrp.StatusFl = true;
                            oGrp.Msg = "Data has been saved successfully !";
                            oGrp.Group = objGroup;
                        }
                        else
                        {
                            oGrp.StatusFl = false;
                            oGrp.Msg = "Group Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oGrp = new GroupResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oGrp;
        }

        public GroupResponse UpdateGroup(Group objGroup)
        {
            GroupResponse oGrp = new GroupResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_GROUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objGroup.GROUP_ID));
                        cmd.Parameters.Add(new SqlParameter("@GROUP_NM", objGroup.GROUP_NM));
                        cmd.Parameters.Add(new SqlParameter("@GROUP_STATUS", objGroup.GROUP_STATUS));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objGroup.SUBSCRIPTION_ST_DT));
                        cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objGroup.SUBSCRIPTION_EN_DT));
                        cmd.Parameters.Add(new SqlParameter("@LOGO", objGroup.LOGO));
                        cmd.Parameters.Add(new SqlParameter("@Mode", "CHECK"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE"));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objGroup.CREATE_BY));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objGroup.GROUP_ID));
                            cmd.Parameters.Add(new SqlParameter("@GROUP_NM", objGroup.GROUP_NM));
                            cmd.Parameters.Add(new SqlParameter("@GROUP_STATUS", objGroup.GROUP_STATUS));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_ST_DT", objGroup.SUBSCRIPTION_ST_DT));
                            cmd.Parameters.Add(new SqlParameter("@SUBSCRIPTION_EN_DT", objGroup.SUBSCRIPTION_EN_DT));
                            cmd.Parameters.Add(new SqlParameter("@LOGO", objGroup.LOGO));
                            cmd.Parameters.Add(new SqlParameter("@Mode", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", objGroup.CREATE_BY));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@Mode", "GET_GROUP_ID_BY_GROUP_NAME"));
                            cmd.Parameters.Add(new SqlParameter("@GROUP_NM", objGroup.GROUP_NM));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            objGroup.GROUP_ID = (Int32)cmd.ExecuteScalar();

                            oGrp.StatusFl = true;
                            oGrp.Msg = "Data has been updated successfully !";
                            oGrp.Group = objGroup;
                        }
                        else
                        {
                            oGrp.StatusFl = false;
                            oGrp.Msg = "Group Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oGrp = new GroupResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oGrp;
        }

        public GroupResponse DeleteGroup(Group objGroup)
        {
            GroupResponse oGrp = new GroupResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_GROUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@GROUP_ID", objGroup.GROUP_ID));
                        cmd.Parameters.Add(new SqlParameter("@Mode", "Delete_GROUP"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        oGrp.StatusFl = true;
                        oGrp.Msg = "Data has been deleted successfully !";
                        oGrp.Group = objGroup;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oGrp = new GroupResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oGrp;
        }

        public GroupResponse GetGroupList()
        {
            GroupResponse oGrp = new GroupResponse();
            oGrp.StatusFl = false;
            oGrp.Msg = "No data found !";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_BUSINESS_GROUP", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@Mode", "GET_GROUP_List"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Group obj = new Group();
                                obj.GROUP_ID = Convert.ToInt32(rdr.GetValue(0));
                                obj.GROUP_NM = Convert.ToString(rdr.GetValue(1));
                                obj.GROUP_STATUS = Convert.ToString(rdr.GetValue(2));
                                obj.SUBSCRIPTION_ST_DT = Convert.ToString(rdr.GetValue(3));
                                obj.SUBSCRIPTION_EN_DT = Convert.ToString(rdr.GetValue(4));
                                obj.CREATE_BY = Convert.ToString(rdr.GetValue(5));
                                obj.CREATED_ON = Convert.ToString(rdr.GetValue(6));
                                obj.LOGO = Convert.ToString(rdr.GetValue(7));
                                oGrp.AddObject(obj);
                            }
                            oGrp.StatusFl = true;
                            oGrp.Msg = "Data has been fetched successfully !";
                        }

                        rdr.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oGrp = new GroupResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oGrp;
        }
    }
}