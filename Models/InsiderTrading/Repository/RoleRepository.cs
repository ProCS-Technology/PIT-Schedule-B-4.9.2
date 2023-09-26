using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class RoleRepository : IRequiresSessionState
    {
        public RoleResponse AddRole(Role objRole)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@ROLE_ID", objRole.ROLE_ID);
                parameters[1] = new SqlParameter("@ROLE_NM", objRole.ROLE_NM);
                parameters[2] = new SqlParameter("@COMPANY_ID", objRole.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objRole.CREATED_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                RoleResponse oRol = new RoleResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                    parameters[3] = new SqlParameter("@Mode", "GET_ROLE_ID_BY_ROLE_NAME");
                    var RoleId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                    objRole.ROLE_ID = (Int32)RoleId;

                    oRol.StatusFl = true;
                    oRol.Msg = "Data has been saved successfully !";
                    oRol.Role = objRole;
                }
                else
                {
                    oRol.StatusFl = false;
                    oRol.Msg = "Role Name aleady exists !";
                }
                return oRol;
            }
            catch (Exception ex)
            {
                RoleResponse oRol = new RoleResponse();
                oRol.StatusFl = false;
                oRol.Msg = "Processing failed, because of system error !";
                return oRol;
            }
        }
        public RoleResponse UpdateRole(Role objRole)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@ROLE_ID", objRole.ROLE_ID);
                parameters[1] = new SqlParameter("@ROLE_NM", objRole.ROLE_NM);
                parameters[2] = new SqlParameter("@Mode", "CHECK");
                parameters[3] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[4] = new SqlParameter("@EMPLOYEE_ID", objRole.CREATED_BY);
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@COMPANY_ID", objRole.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                RoleResponse oRel = new RoleResponse();
                if ((Int32)obj == 0)
                {
                    parameters[2] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                    parameters[2] = new SqlParameter("@Mode", "GET_ROLE_ID_BY_ROLE_NAME");

                    var RoleId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                    objRole.ROLE_ID = (Int32)RoleId;

                    oRel.StatusFl = true;
                    oRel.Msg = "Data has been updated successfully !";
                    oRel.Role = objRole;
                }
                else
                {
                    oRel.StatusFl = false;
                    oRel.Msg = "Role Name aleady exists !";
                }
                return oRel;
            }
            catch (Exception ex)
            {
                RoleResponse oRel = new RoleResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public RoleResponse DeleteRole(Role objRole)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@ROLE_ID", objRole.ROLE_ID);
                parameters[1] = new SqlParameter("@Mode", "DELETE_ROLE");
                parameters[2] = new SqlParameter("@COMPANY_ID", objRole.COMPANY_ID);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                RoleResponse oRol = new RoleResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                oRol.StatusFl = true;
                oRol.Msg = "Data has been deleted successfully !";
                oRol.Role = objRole;
                return oRol;
            }
            catch (Exception ex)
            {
                RoleResponse oRol = new RoleResponse();
                oRol.StatusFl = false;
                oRol.Msg = "Processing failed, because of system error !";
                return oRol;
            }
        }
        public RoleResponse GetRoleList(Role objRole)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRole.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_ROLE_LIST");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objRole.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RoleResponse oRol = new RoleResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Role obj = new Role();
                        obj.ROLE_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.ROLE_NM = Convert.ToString(rdr.GetValue(1));
                        oRol.AddObject(obj);
                    }
                    oRol.StatusFl = true;
                    oRol.Msg = "Role Data has been fetched successfully !";
                }
                else
                {
                    oRol.StatusFl = false;
                    oRol.Msg = "No data found !";
                }
                rdr.Close();
                return oRol;
            }
            catch (Exception ex)
            {
                RoleResponse oRol = new RoleResponse();
                oRol.StatusFl = false;
                oRol.Msg = "Processing failed, because of system error !";
                return oRol;
            }
        }
        public RoleResponse GetRoleListWithAdmin(Role objRole)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRole.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_ROLE_LIST_WITH_ADMIN");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objRole.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RoleResponse oRol = new RoleResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_ROLE", objRole.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Role obj = new Role();
                        obj.ROLE_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.ROLE_NM = Convert.ToString(rdr.GetValue(1));
                        oRol.AddObject(obj);
                    }
                    oRol.StatusFl = true;
                    oRol.Msg = "Role Data has been fetched successfully !";
                }
                else
                {
                    oRol.StatusFl = false;
                    oRol.Msg = "No data found !";
                }
                rdr.Close();
                return oRol;
            }
            catch (Exception ex)
            {
                RoleResponse oRol = new RoleResponse();
                oRol.StatusFl = false;
                oRol.Msg = "Processing failed, because of system error !";
                return oRol;
            }
        }
    }
}