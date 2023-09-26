using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class DepartmentRepository : IRequiresSessionState
    {
        public DepartmentResponse AddDepartment(Department objDepartment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@DEPARTMENT_ID", objDepartment.DEPARTMENT_ID);
                parameters[1] = new SqlParameter("@DEPARTMENT_NM", objDepartment.DEPARTMENT_NM);
                parameters[2] = new SqlParameter("@COMPANY_ID", objDepartment.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objDepartment.CREATE_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                DepartmentResponse oDep = new DepartmentResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);
                    parameters[3] = new SqlParameter("@Mode", "GET_DEPARTMENT_ID_BY_DEPARTMENT_NAME");
                    var DepartmentId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);
                    objDepartment.DEPARTMENT_ID = (Int32)DepartmentId;

                    oDep.StatusFl = true;
                    oDep.Msg = "Data has been saved successfully !";
                    oDep.Department = objDepartment;
                }
                else
                {
                    oDep.StatusFl = false;
                    oDep.Msg = "Department Name aleady exists !";
                }
                return oDep;
            }
            catch (Exception ex)
            {
                DepartmentResponse oDep = new DepartmentResponse();
                oDep.StatusFl = false;
                oDep.Msg = "Processing failed, because of system error !";
                return oDep;
            }
        }

        public DepartmentResponse UpdateDepartment(Department objDepartment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@DEPARTMENT_ID", objDepartment.DEPARTMENT_ID);
                parameters[1] = new SqlParameter("@DEPARTMENT_NM", objDepartment.DEPARTMENT_NM);
                parameters[2] = new SqlParameter("@Mode", "CHECK");
                parameters[3] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[4] = new SqlParameter("@EMPLOYEE_ID", objDepartment.CREATE_BY);
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@COMPANY_ID", objDepartment.COMPANY_ID);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                DepartmentResponse oDept = new DepartmentResponse();
                if ((Int32)obj == 0)
                {
                    parameters[2] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);

                    parameters[2] = new SqlParameter("@Mode", "GET_DEPARTMENT_ID_BY_DEPARTMENT_NAME");
                    var DepartmentId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);
                    objDepartment.DEPARTMENT_ID = (Int32)DepartmentId;

                    oDept.StatusFl = true;
                    oDept.Msg = "Data has been updated successfully !";
                    oDept.Department = objDepartment;
                }
                else
                {
                    oDept.StatusFl = false;
                    oDept.Msg = "Department Name aleady exists !";
                }
                return oDept;
            }
            catch (Exception ex)
            {
                DepartmentResponse oDept = new DepartmentResponse();
                oDept.StatusFl = false;
                oDept.Msg = "Processing failed, because of system error !";
                return oDept;
            }
        }

        public DepartmentResponse DeleteDepartment(Department objDept)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@DEPARTMENT_ID", objDept.DEPARTMENT_ID);
                parameters[1] = new SqlParameter("@Mode", "Delete_DEPARTMENT");
                parameters[2] = new SqlParameter("@COMPANY_ID", objDept.COMPANY_ID);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                DepartmentResponse oDept = new DepartmentResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDept.MODULE_DATABASE, parameters);
                oDept.StatusFl = true;
                oDept.Msg = "Data has been deleted successfully !";
                oDept.Department = objDept;
                return oDept;
            }
            catch (Exception ex)
            {
                DepartmentResponse oDept = new DepartmentResponse();
                oDept.StatusFl = false;
                oDept.Msg = "Processing failed, because of system error !";
                return oDept;
            }
        }

        public DepartmentResponse GetDepartmentList(Department objDepartment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objDepartment.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_DEPARTMENT_List");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objDepartment.CREATE_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                DepartmentResponse oGrp = new DepartmentResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPARTMENT", objDepartment.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Department obj = new Department();

                        obj.DEPARTMENT_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.DEPARTMENT_NM = Convert.ToString(rdr.GetValue(1));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(2));

                        oGrp.AddObject(obj);
                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Department Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                rdr.Close();
                return oGrp;
            }
            catch (Exception ex)
            {
                DepartmentResponse oGrp = new DepartmentResponse();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }
    }
}