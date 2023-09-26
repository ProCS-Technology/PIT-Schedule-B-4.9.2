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
    public class userRepository : IRequiresSessionState
    {
        private static String connectionString = SQLHelper.GetConnString();

        public userResponse AddUser(User objuser)
        {
            userResponse oUser = new userResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@ID", objuser.ID));
                        cmd.Parameters.Add(new SqlParameter("@USER_NM", objuser.USER_NM));
                        cmd.Parameters.Add(new SqlParameter("@USER_EMAIL", objuser.USER_EMAIL));
                        cmd.Parameters.Add(new SqlParameter("@USER_PWD", objuser.USER_PWD));
                        cmd.Parameters.Add(new SqlParameter("@USER_MOBILE", objuser.USER_MOBILE));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                        cmd.Parameters.Add(new SqlParameter("@Mode", "CHECK"));
                        // parameters[8] = new SqlParameter("@EMPLOYEE_ID", objuser.ID);
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var obj = cmd.Parameters["@SET_COUNT"].Value;
                        if ((Int32)obj == 0)
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@ID", objuser.ID));
                            cmd.Parameters.Add(new SqlParameter("@USER_NM", objuser.USER_NM));
                            cmd.Parameters.Add(new SqlParameter("@USER_EMAIL", objuser.USER_EMAIL));
                            cmd.Parameters.Add(new SqlParameter("@USER_PWD", objuser.USER_PWD));
                            cmd.Parameters.Add(new SqlParameter("@USER_MOBILE", objuser.USER_MOBILE));
                            cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT"));
                            cmd.Parameters.Add(new SqlParameter("@Mode", "INSERT_UPDATE"));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SqlParameter("@Mode", "GET_ID_BY_User_NAME"));
                            cmd.Parameters.Add(new SqlParameter("@USER_NM", objuser.USER_NM));
                            cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                            objuser.ID = (Int32)cmd.ExecuteScalar();
                            SetUsersRole(objuser.User_Role.ROLE_ID, objuser.USER_EMAIL);

                            oUser.StatusFl = true;
                            oUser.Msg = "Data has been saved successfully !";
                            oUser.User = objuser;
                        }
                        else
                        {
                            oUser.StatusFl = false;
                            oUser.Msg = "User Name aleady exists !";
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oUser = new userResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oUser;
        }

        private userRoleResponse SetUsersRole(Int32 Role_Id, String UserEmail)
        {
            userRoleResponse ouser = new userRoleResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@USER_EMAIL", UserEmail));
                        cmd.Parameters.Add(new SqlParameter("@ROLE_ID", Role_Id));
                        cmd.Parameters.Add(new SqlParameter("@MODE", "INSERT_UPDATE_Users_Role"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "INSERT_Role"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ouser = new userRoleResponse();
                ouser.StatusFl = false;
                ouser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return ouser;
        }
        private userRoleResponse UpdateUsersRole(Int32 Role_Id, String UserEmail)
        {
            userRoleResponse oUser = new userRoleResponse();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_USER", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@USER_EMAIL", UserEmail));
                        cmd.Parameters.Add(new SqlParameter("@ROLE_ID", Role_Id));
                        cmd.Parameters.Add(new SqlParameter("@MODE", "INSERT_UPDATE_Users_Role"));
                        cmd.Parameters.Add(new SqlParameter("@ACTION_TYPE", "UPDATE_Role"));
                        cmd.Parameters.Add(new SqlParameter("@SET_COUNT", SqlDbType.Int)).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        oUser.StatusFl = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                oUser = new userRoleResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
            }
            return oUser;
        }
        public userResponse UpdateUser(User objuser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@ID", objuser.ID);
                parameters[1] = new SqlParameter("@USER_NM", objuser.USER_NM);
                parameters[2] = new SqlParameter("@USER_EMAIL", objuser.USER_EMAIL);
                parameters[3] = new SqlParameter("@USER_PWD", objuser.USER_PWD);
                parameters[4] = new SqlParameter("@USER_MOBILE", objuser.USER_MOBILE);
                parameters[5] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[6] = new SqlParameter("@Mode", "CHECK");

                // parameters[8] = new SqlParameter("@EMPLOYEE_ID", objuser.ID);
                parameters[7] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[7].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_USER", parameters);
                var obj = parameters[7].Value;
                userResponse ouser = new userResponse();
                if ((Int32)obj == 0)

                {
                    parameters[6] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_USER", parameters);

                    parameters[6] = new SqlParameter("@Mode", "GET_ID_BY_User_NAME");
                    var UserId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_USER", parameters);
                    objuser.ID = (Int32)UserId;
                    UpdateUsersRole(objuser.User_Role.ROLE_ID, objuser.USER_EMAIL);

                    ouser.StatusFl = true;
                    ouser.Msg = "Data has been updated successfully !";
                    ouser.User = objuser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "User Name aleady exists !";
                }
                return ouser;
            }
            catch (Exception ex)
            {
                userResponse ouser = new userResponse();
                ouser.StatusFl = false;
                ouser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                return ouser;
            }
        }

        public userResponse DeleteUser(User objuser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", objuser.ID);
                parameters[1] = new SqlParameter("@Mode", "Delete_User");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                userResponse oUser = new userResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_USER", parameters);
                oUser.StatusFl = true;
                oUser.Msg = "Data has been deleted successfully !";
                oUser.User = objuser;
                return oUser;
            }
            catch (Exception ex)
            {
                userResponse oUser = new userResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                return oUser;
            }
        }

        public userResponse GetUserList()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_User_List");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                userResponse oUser = new userResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_USER", parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();
                        //  UsersRole objrole = new UsersRole();
                        obj.ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.USER_NM = Convert.ToString(rdr.GetValue(1));
                        obj.USER_EMAIL = Convert.ToString(rdr.GetValue(2));
                        obj.USER_PWD = Convert.ToString(rdr.GetValue(3));
                        obj.USER_MOBILE = Convert.ToString(rdr.GetValue(4));
                        obj.User_Role.ROLE_ID = Convert.ToInt32(rdr.GetValue(5) == DBNull.Value ? 0 : rdr.GetValue(5));
                        obj.User_Role.ROLE_NM = Convert.ToString(rdr.GetValue(6) == DBNull.Value ? 0 : rdr.GetValue(6));
                        //objrole.ROLE_ID = Convert.ToInt32(rdr.GetValue(5));
                        // objrole.ROLE_NM = Convert.ToString(rdr.GetValue(6));
                        //obj.CREATE_BY = Convert.ToString(rdr.GetValue(5));
                        //obj.CREATED_ON = Convert.ToDateTime(rdr.GetValue(6));
                        //obj.LOGO = Convert.ToString(rdr.GetValue(7));
                        oUser.AddObject(obj);
                    }
                    oUser.StatusFl = true;
                    oUser.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oUser.StatusFl = false;
                    oUser.Msg = "No data found !";
                }

                return oUser;
            }
            catch (Exception ex)
            {
                userResponse oUser = new userResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                return oUser;
            }
        }

        public userRoleResponse GetAllUsersRole()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "Get_All_Users_Role");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                userRoleResponse oUser = new userRoleResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_USER", parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UsersRole obj = new UsersRole();
                        obj.ROLE_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.ROLE_NM = Convert.ToString(rdr.GetValue(1));
                        //obj.CREATE_BY = Convert.ToString(rdr.GetValue(5));
                        //obj.CREATED_ON = Convert.ToDateTime(rdr.GetValue(6));
                        //obj.LOGO = Convert.ToString(rdr.GetValue(7));
                        oUser.AddObject(obj);
                    }
                    oUser.StatusFl = true;
                    oUser.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oUser.StatusFl = false;
                    oUser.Msg = "No data found !";
                }

                return oUser;
            }
            catch (Exception ex)
            {
                userRoleResponse oUser = new userRoleResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Something went wrong. Please try again or Contact Support!";
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, this.GetType().Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]), Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                return oUser;
            }
        }
    }
}