using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Dynamic;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UserRepository : IRequiresSessionState
    {
        private static String connectionString = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
        public UserResponse AccessUserListByBusinessUnitId(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@Mode", "ACCESS_USER_LIST_BY_BU_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);
                parameters[5] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);
                parameters[6] = new SqlParameter("@LOGIN_ID", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));

                UserResponse oUser = new UserResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.ID = Convert.ToInt32(rdr["ID"]);
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.userRole = new Role
                        {
                            ROLE_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_ROLE"]))) ? Convert.ToInt32(rdr["USER_ROLE"]) : 0,
                            ROLE_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["ROLE_NAME"]))) ? Convert.ToString(rdr["ROLE_NAME"]) : String.Empty
                        };
                        obj.USER_MOBILE = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_MOBILE"]))) ? Convert.ToString(rdr["USER_MOBILE"]) : String.Empty;
                        //obj.panNumber = (!String.IsNullOrEmpty(Convert.ToString(rdr["PAN"]))) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                        //obj.address = (!String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"]))) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                        //obj.tenureStartDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TENURE_START"]))) ? Convert.ToString(rdr["TENURE_START"]) : String.Empty;
                        //obj.tenureEndDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TENURE_END"]))) ? Convert.ToString(rdr["TENURE_END"]) : String.Empty;
                        //obj.dateOfBirth = (!String.IsNullOrEmpty(Convert.ToString(rdr["DATE_OF_BIRTH"]))) ? Convert.ToString(rdr["DATE_OF_BIRTH"]) : String.Empty;
                        //obj.nationality = (!String.IsNullOrEmpty(Convert.ToString(rdr["NATIONALITY"]))) ? Convert.ToString(rdr["NATIONALITY"]) : String.Empty;
                        obj.LOGIN_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["LOGIN_ID"]))) ? Convert.ToString(rdr["LOGIN_ID"]) : String.Empty;
                        //obj.USER_PWD = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_PWD"]))) ? Convert.ToString(rdr["USER_PWD"]) : String.Empty;
                        obj.status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        //obj.uploadAvatar = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPLOAD_AVATAR"]))) ? Convert.ToString(rdr["UPLOAD_AVATAR"]) : String.Empty;
                        //obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        //obj.SALUTATION = (!String.IsNullOrEmpty(Convert.ToString(rdr["SALUTATION"]))) ? Convert.ToString(rdr["SALUTATION"]) : String.Empty;
                        //obj.designation = new Designation
                        //{
                        //    DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_ID"])) ? Convert.ToInt32(rdr["DESIGNATION_ID"]) : 0,
                        //    DESIGNATION_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_NAME"]))) ? Convert.ToString(rdr["DESIGNATION_NAME"]) : String.Empty
                        //};
                        //obj.department = new Department
                        //{
                        //    DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["DEPARTMENT_ID"])) ? Convert.ToInt32(rdr["DEPARTMENT_ID"]) : 0,
                        //    DEPARTMENT_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DEPARTMENT_NAME"]))) ? Convert.ToString(rdr["DEPARTMENT_NAME"]) : String.Empty
                        //};
                        //obj.businessUnit = new BusinessUnit
                        //{
                        //    businessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_ID"])) ? Convert.ToInt32(rdr["BU_ID"]) : 0,
                        //    businessUnitName = (!String.IsNullOrEmpty(Convert.ToString(rdr["BU_NM"]))) ? Convert.ToString(rdr["BU_NM"]) : String.Empty
                        //};
                        //obj.isApprover = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER"]))) ? Convert.ToString(rdr["IS_APPROVER"]) : String.Empty;
                        //obj.isApproverForCO = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER_FOR_CO"]))) ? Convert.ToString(rdr["IS_APPROVER_FOR_CO"]) : String.Empty;
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
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse GetUserTask(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_TASK");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                //parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);

                UserResponse oUser = new UserResponse();
                //DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "[SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                User user = new User();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    user.TaskId = Convert.ToInt32(ds.Tables[0].Rows[0]["TASK_ID"]);
                    user.TaskFor = Convert.ToString(ds.Tables[0].Rows[0]["TASK_FOR"]);
                    user.TaskStatus = Convert.ToString(ds.Tables[0].Rows[0]["TASK_STATUS"]);
                    oUser.User = user;
                    oUser.StatusFl = true;
                    oUser.Msg = "Data has been fetched successfully !";
                }

                else
                {
                    //user.status = "Closed";
                    oUser.StatusFl = false;
                    oUser.Msg = "No task found.";
                }

                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse AddUser(User objuser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[34];
                parameters[0] = new SqlParameter("@MODE", "CHECK");
                parameters[1] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@USER_NM", objuser.USER_NM);
                parameters[4] = new SqlParameter("@USER_EMAIL", objuser.USER_EMAIL);
                parameters[5] = new SqlParameter("@ROLE", objuser.userRole.ROLE_ID);
                parameters[6] = new SqlParameter("@USER_MOBILE", objuser.USER_MOBILE);
                parameters[7] = new SqlParameter("@ADDRESS", objuser.address);
                if (String.IsNullOrEmpty(objuser.tenureStartDate))
                {
                    parameters[8] = new SqlParameter("@TENURE_START", DBNull.Value);
                }
                else
                {
                    parameters[8] = new SqlParameter("@TENURE_START", ConvertDate(objuser.tenureStartDate));
                }
                if (String.IsNullOrEmpty(objuser.tenureEndDate))
                {
                    parameters[9] = new SqlParameter("@TENURE_END", DBNull.Value);
                }
                else
                {
                    parameters[9] = new SqlParameter("@TENURE_END", FormatHelper.FormatDate(objuser.tenureEndDate));
                }
                if (String.IsNullOrEmpty(objuser.dateOfBirth))
                {
                    parameters[10] = new SqlParameter("@DATE_OF_BIRTH", DBNull.Value);
                }
                else
                {
                    parameters[10] = new SqlParameter("@DATE_OF_BIRTH", FormatHelper.FormatDate(objuser.dateOfBirth));
                }
                parameters[11] = new SqlParameter("@NATIONALITY", objuser.nationality);
                parameters[12] = new SqlParameter("@LOGIN_ID", objuser.LOGIN_ID);
                parameters[13] = new SqlParameter("@USER_PWD", objuser.USER_PWD);
                parameters[14] = new SqlParameter("@STATUS", objuser.status);
                parameters[15] = new SqlParameter("@UPLOAD_AVATAR", objuser.uploadAvatar);
                parameters[16] = new SqlParameter("@COMPANY_ID", objuser.companyId);
                parameters[17] = new SqlParameter("@ID", objuser.ID);
                parameters[18] = new SqlParameter("@MODULE_ID", objuser.moduleId);
                parameters[19] = new SqlParameter("@SALUTATION", objuser.SALUTATION);
                parameters[20] = new SqlParameter("@DESIGNATION_ID", objuser.designation.DESIGNATION_ID);
                parameters[21] = new SqlParameter("@DEPARTMENT_ID", objuser.department.DEPARTMENT_ID);
                parameters[22] = new SqlParameter("@PAN", objuser.panNumber);
                parameters[23] = new SqlParameter("@ADMIN_DB", objuser.ADMIN_DATABASE);
                parameters[24] = new SqlParameter("@BUSINESS_UNIT_ID", objuser.businessUnit.businessUnitId);
                parameters[25] = new SqlParameter("@PERSONAL_EMAIL", objuser.PersonalEmail);
                parameters[26] = new SqlParameter("@USER_TYPE", objuser.UserType);
                parameters[27] = new SqlParameter("@LoginUsingPersonalEmail", objuser.LoginUsingPersonalEmail);

                if (!String.IsNullOrEmpty(objuser.becomingInsiderDate))
                {
                    parameters[28] = new SqlParameter("@DATE_BECOMING_INSIDER", FormatHelper.FormatDate(objuser.becomingInsiderDate));
                }
                else
                {
                    parameters[28] = new SqlParameter("@DATE_BECOMING_INSIDER", DBNull.Value);
                }
                parameters[29] = new SqlParameter("@Category_ID", objuser.CategoryId);
                parameters[30] = new SqlParameter("@EMPLOYEE_ID", objuser.employeeId);

                if (!String.IsNullOrEmpty(objuser.joiningDate))
                {
                    parameters[31] = new SqlParameter("@JOINING_DATE", FormatHelper.FormatDate(objuser.joiningDate));
                }
                else
                {
                    parameters[31] = new SqlParameter("@JOINING_DATE", DBNull.Value);
                }
                parameters[32] = new SqlParameter("@IDENTIFICATION_TYPE", objuser.identificationType);
                parameters[33] = new SqlParameter("@IDENTIFICATION_NUMBER", objuser.identificationNumber);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objuser.MODULE_DATABASE, parameters);
                var obj = parameters[2].Value;
                UserResponse ouser = new UserResponse();
                if ((Int32)obj == 0)
                {
                    SqlParameter[] parametersX = new SqlParameter[5];
                    parametersX[0] = new SqlParameter("@Mode", "Verify_Pan_For_New_User");
                    parametersX[1] = new SqlParameter("@LOGIN_ID", objuser.LOGIN_ID);
                    parametersX[2] = new SqlParameter("@PAN", objuser.panNumber);
                    parametersX[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parametersX[3].Direction = ParameterDirection.Output;
                    parametersX[4] = new SqlParameter("@COMPANY_ID", objuser.companyId);

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objuser.MODULE_DATABASE, parametersX);
                    var obj1 = parametersX[3].Value;
                    if ((Int32)obj1 == 0)
                    {
                        parameters[0] = new SqlParameter("@Mode", "INSERT_UPDATE");
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objuser.MODULE_DATABASE, parameters);
                        var dataInsertOrUpdate = parameters[2].Value;
                        parameters[0] = new SqlParameter("@Mode", "GET_USER_ID_BY_NAME");
                        var UserId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objuser.MODULE_DATABASE, parameters);
                        objuser.ID = (Int32)UserId;
                        if (objuser.emailFl)
                        {
                            SendWelcomeEmailToAddedInsiderUser(objuser);
                        }
                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been saved successfully !";
                        ouser.User = objuser;
                    }
                    else
                    {
                        ouser.StatusFl = false;
                        ouser.Msg = "PAN already exists !";
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "User with same email id/login id already exist!";
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse UpdateUser(User objuser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[33];
                parameters[0] = new SqlParameter("@Mode", "CHECK");
                parameters[1] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[2] = new SqlParameter("@COMPANY_ID", objuser.companyId);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4] = new SqlParameter("@USER_NM", objuser.USER_NM);
                parameters[5] = new SqlParameter("@USER_EMAIL", objuser.USER_EMAIL);
                parameters[6] = new SqlParameter("@ROLE", objuser.userRole.ROLE_ID);
                parameters[7] = new SqlParameter("@USER_MOBILE", objuser.USER_MOBILE);
                parameters[8] = new SqlParameter("@ADDRESS", objuser.address);
                if (String.IsNullOrEmpty(objuser.tenureStartDate))
                {
                    parameters[9] = new SqlParameter("@TENURE_START", DBNull.Value);
                }
                else
                {
                    parameters[9] = new SqlParameter("@TENURE_START", FormatHelper.ConvertDate(objuser.tenureStartDate));
                }
                if (String.IsNullOrEmpty(objuser.tenureEndDate))
                {
                    parameters[10] = new SqlParameter("@TENURE_END", DBNull.Value);
                }
                else
                {
                    parameters[10] = new SqlParameter("@TENURE_END", FormatHelper.ConvertDate(objuser.tenureEndDate));
                }
                if (String.IsNullOrEmpty(objuser.dateOfBirth))
                {
                    parameters[11] = new SqlParameter("@DATE_OF_BIRTH", DBNull.Value);
                }
                else
                {
                    parameters[11] = new SqlParameter("@DATE_OF_BIRTH", FormatHelper.ConvertDate(objuser.dateOfBirth));
                }
                parameters[12] = new SqlParameter("@NATIONALITY", objuser.nationality);
                parameters[13] = new SqlParameter("@LOGIN_ID", objuser.LOGIN_ID);
                parameters[14] = new SqlParameter("@USER_PWD", objuser.USER_PWD);
                parameters[15] = new SqlParameter("@STATUS", objuser.status);
                parameters[16] = new SqlParameter("@UPLOAD_AVATAR", objuser.uploadAvatar);
                parameters[17] = new SqlParameter("@ID", objuser.ID);
                parameters[18] = new SqlParameter("@MODULE_ID", objuser.moduleId);
                parameters[19] = new SqlParameter("@SALUTATION", objuser.SALUTATION);
                parameters[20] = new SqlParameter("@DESIGNATION_ID", objuser.designation.DESIGNATION_ID);
                parameters[21] = new SqlParameter("@DEPARTMENT_ID", objuser.department.DEPARTMENT_ID);
                parameters[22] = new SqlParameter("@PAN", objuser.panNumber);
                parameters[23] = new SqlParameter("@ADMIN_DB", objuser.ADMIN_DATABASE);
                parameters[24] = new SqlParameter("@BUSINESS_UNIT_ID", objuser.businessUnit.businessUnitId);
                parameters[25] = new SqlParameter("@PERSONAL_EMAIL", objuser.PersonalEmail);
                parameters[26] = new SqlParameter("@USER_TYPE", objuser.UserType);
                parameters[27] = new SqlParameter("@EMPLOYEE_ID", objuser.employeeId);
                if (!String.IsNullOrEmpty(objuser.joiningDate))
                {
                    parameters[28] = new SqlParameter("@JOINING_DATE", FormatHelper.ConvertDate(objuser.joiningDate));
                }
                else
                {
                    parameters[28] = new SqlParameter("@JOINING_DATE", FormatHelper.ConvertDate(objuser.joiningDate));
                }
                parameters[29] = new SqlParameter("@IS_MARRIED", objuser.IsMarried);
                parameters[30] = new SqlParameter("@IDENTIFICATION_TYPE", objuser.identificationType);
                parameters[31] = new SqlParameter("@IDENTIFICATION_NUMBER", objuser.identificationNumber);

                if (!String.IsNullOrEmpty(objuser.becomingInsiderDate))
                {
                    parameters[32] = new SqlParameter("@DATE_BECOMING_INSIDER", FormatHelper.FormatDate(objuser.becomingInsiderDate));
                }
                else
                {
                    parameters[32] = new SqlParameter("@DATE_BECOMING_INSIDER", DBNull.Value);
                }

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objuser.MODULE_DATABASE, parameters);
                var obj = parameters[3].Value;
                UserResponse ouser = new UserResponse();
                if ((Int32)obj == 0)
                {
                    parameters[0] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objuser.MODULE_DATABASE, parameters);
                    parameters[0] = new SqlParameter("@Mode", "GET_USER_ID_BY_NAME");
                    var UserId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objuser.MODULE_DATABASE, parameters);
                    objuser.ID = (Int32)UserId;
                    ouser.StatusFl = true;
                    ouser.Msg = "Data has been updated successfully !";
                    ouser.User = objuser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "User with same email id already exist !";
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse ouser = new UserResponse();
                ouser.StatusFl = false;
                ouser.Msg = "Processing failed, because of system error !";
                return ouser;
            }
        }
        public UserResponse DeleteUser(User objuser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", objuser.ID);
                parameters[1] = new SqlParameter("@Mode", "DELETE_USER");
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                UserResponse oUser = new UserResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objuser.MODULE_DATABASE, parameters);
                oUser.StatusFl = true;
                oUser.Msg = "Data has been deleted successfully !";
                oUser.User = objuser;
                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse GetUserList(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);
                parameters[5] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);

                UserResponse oUser = new UserResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.ID = Convert.ToInt32(rdr["ID"]);
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.userRole = new Role
                        {
                            ROLE_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_ROLE"]))) ? Convert.ToInt32(rdr["USER_ROLE"]) : 0,
                            ROLE_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["ROLE_NAME"]))) ? Convert.ToString(rdr["ROLE_NAME"]) : String.Empty
                        };
                        obj.USER_MOBILE = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_MOBILE"]))) ? Convert.ToString(rdr["USER_MOBILE"]) : String.Empty;
                        obj.panNumber = (!String.IsNullOrEmpty(Convert.ToString(rdr["PAN"]))) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                        obj.address = (!String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"]))) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                        obj.tenureStartDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TENURE_START"]))) ? Convert.ToString(rdr["TENURE_START"]) : String.Empty;
                        obj.tenureEndDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TENURE_END"]))) ? Convert.ToString(rdr["TENURE_END"]) : String.Empty;
                        obj.dateOfBirth = (!String.IsNullOrEmpty(Convert.ToString(rdr["DATE_OF_BIRTH"]))) ? Convert.ToString(rdr["DATE_OF_BIRTH"]) : String.Empty;
                        obj.nationality = (!String.IsNullOrEmpty(Convert.ToString(rdr["NATIONALITY"]))) ? Convert.ToString(rdr["NATIONALITY"]) : String.Empty;
                        obj.LOGIN_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_LOGIN"]))) ? Convert.ToString(rdr["USER_LOGIN"]) : String.Empty;
                        obj.USER_PWD = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_PWD"]))) ? Convert.ToString(rdr["USER_PWD"]) : String.Empty;
                        obj.status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        obj.uploadAvatar = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPLOAD_AVATAR"]))) ? Convert.ToString(rdr["UPLOAD_AVATAR"]) : String.Empty;
                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        obj.SALUTATION = (!String.IsNullOrEmpty(Convert.ToString(rdr["SALUTATION"]))) ? Convert.ToString(rdr["SALUTATION"]) : String.Empty;
                        obj.PersonalEmail = (!String.IsNullOrEmpty(Convert.ToString(rdr["PERSONAL_EMAIL"]))) ? Convert.ToString(rdr["PERSONAL_EMAIL"]) : String.Empty;
                        obj.designation = new Designation
                        {
                            DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_ID"])) ? Convert.ToInt32(rdr["DESIGNATION_ID"]) : 0,
                            DESIGNATION_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_NAME"]))) ? Convert.ToString(rdr["DESIGNATION_NAME"]) : String.Empty
                        };
                        obj.department = new Department
                        {
                            DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["DEPARTMENT_ID"])) ? Convert.ToInt32(rdr["DEPARTMENT_ID"]) : 0,
                            DEPARTMENT_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DEPARTMENT_NAME"]))) ? Convert.ToString(rdr["DEPARTMENT_NAME"]) : String.Empty
                        };
                        obj.businessUnit = new BusinessUnit
                        {
                            businessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_ID"])) ? Convert.ToInt32(rdr["BU_ID"]) : 0,
                            businessUnitName = (!String.IsNullOrEmpty(Convert.ToString(rdr["BU_NM"]))) ? Convert.ToString(rdr["BU_NM"]) : String.Empty
                        };
                        obj.isApprover = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER"]))) ? Convert.ToString(rdr["IS_APPROVER"]) : String.Empty;
                        obj.isApproverForCO = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER_FOR_CO"]))) ? Convert.ToString(rdr["IS_APPROVER_FOR_CO"]) : String.Empty;
                        obj.UserType = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_TYPE"]))) ? Convert.ToString(rdr["USER_TYPE"]) : String.Empty;
                        obj.becomingInsiderDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["DATE_BECOMING_INSIDER"]))) ? Convert.ToString(rdr["DATE_BECOMING_INSIDER"]) : String.Empty;
                        obj.CategoryId = (!String.IsNullOrEmpty(Convert.ToString(rdr["CATEGORY_ID"]))) ? Convert.ToString(rdr["CATEGORY_ID"]) : String.Empty;
                        obj.employeeId = (!String.IsNullOrEmpty(Convert.ToString(rdr["EMPLOYEE_ID"]))) ? Convert.ToString(rdr["EMPLOYEE_ID"]) : String.Empty;
                        obj.joiningDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["JOINING_DATE"]))) ? Convert.ToString(rdr["JOINING_DATE"]) : String.Empty;
                        obj.identificationType = (!String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"]))) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
                        obj.identificationNumber = (!String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"]))) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
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
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse GetDPUsers(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "GET_DP_USERS");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);
                parameters[5] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);

                UserResponse oUser = new UserResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.LOGIN_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["LOGIN_ID"]))) ? Convert.ToString(rdr["LOGIN_ID"]) : String.Empty;
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
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse GetUserListByBusinessUnitId(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_LIST_BY_BU_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);
                parameters[5] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);

                UserResponse oUser = new UserResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.ID = Convert.ToInt32(rdr["ID"]);
                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_NM"]))) ? Convert.ToString(rdr["USER_NM"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_EMAIL"]))) ? Convert.ToString(rdr["USER_EMAIL"]) : String.Empty;
                        obj.userRole = new Role
                        {
                            ROLE_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_ROLE"]))) ? Convert.ToInt32(rdr["USER_ROLE"]) : 0,
                            ROLE_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["ROLE_NAME"]))) ? Convert.ToString(rdr["ROLE_NAME"]) : String.Empty
                        };
                        obj.USER_MOBILE = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_MOBILE"]))) ? Convert.ToString(rdr["USER_MOBILE"]) : String.Empty;
                        obj.panNumber = (!String.IsNullOrEmpty(Convert.ToString(rdr["PAN"]))) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                        obj.address = (!String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"]))) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                        obj.tenureStartDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TENURE_START"]))) ? Convert.ToString(rdr["TENURE_START"]) : String.Empty;
                        obj.tenureEndDate = (!String.IsNullOrEmpty(Convert.ToString(rdr["TENURE_END"]))) ? Convert.ToString(rdr["TENURE_END"]) : String.Empty;
                        obj.dateOfBirth = (!String.IsNullOrEmpty(Convert.ToString(rdr["DATE_OF_BIRTH"]))) ? Convert.ToString(rdr["DATE_OF_BIRTH"]) : String.Empty;
                        obj.nationality = (!String.IsNullOrEmpty(Convert.ToString(rdr["NATIONALITY"]))) ? Convert.ToString(rdr["NATIONALITY"]) : String.Empty;
                        obj.LOGIN_ID = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_LOGIN"]))) ? Convert.ToString(rdr["USER_LOGIN"]) : String.Empty;
                        obj.USER_PWD = (!String.IsNullOrEmpty(Convert.ToString(rdr["USER_PWD"]))) ? Convert.ToString(rdr["USER_PWD"]) : String.Empty;
                        obj.status = (!String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"]))) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                        obj.uploadAvatar = (!String.IsNullOrEmpty(Convert.ToString(rdr["UPLOAD_AVATAR"]))) ? Convert.ToString(rdr["UPLOAD_AVATAR"]) : String.Empty;
                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        obj.SALUTATION = (!String.IsNullOrEmpty(Convert.ToString(rdr["SALUTATION"]))) ? Convert.ToString(rdr["SALUTATION"]) : String.Empty;
                        obj.designation = new Designation
                        {
                            DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_ID"])) ? Convert.ToInt32(rdr["DESIGNATION_ID"]) : 0,
                            DESIGNATION_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DESIGNATION_NAME"]))) ? Convert.ToString(rdr["DESIGNATION_NAME"]) : String.Empty
                        };
                        obj.department = new Department
                        {
                            DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["DEPARTMENT_ID"])) ? Convert.ToInt32(rdr["DEPARTMENT_ID"]) : 0,
                            DEPARTMENT_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["DEPARTMENT_NAME"]))) ? Convert.ToString(rdr["DEPARTMENT_NAME"]) : String.Empty
                        };
                        obj.businessUnit = new BusinessUnit
                        {
                            businessUnitId = !String.IsNullOrEmpty(Convert.ToString(rdr["BU_ID"])) ? Convert.ToInt32(rdr["BU_ID"]) : 0,
                            businessUnitName = (!String.IsNullOrEmpty(Convert.ToString(rdr["BU_NM"]))) ? Convert.ToString(rdr["BU_NM"]) : String.Empty
                        };
                        obj.isApprover = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER"]))) ? Convert.ToString(rdr["IS_APPROVER"]) : String.Empty;
                        obj.isApproverForCO = (!String.IsNullOrEmpty(Convert.ToString(rdr["IS_APPROVER_FOR_CO"]))) ? Convert.ToString(rdr["IS_APPROVER_FOR_CO"]) : String.Empty;
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
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse AssignedApprover(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "ASSIGNED_APPROVER");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameters[5] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);

                UserResponse oUser = new UserResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                oUser.StatusFl = true;
                oUser.Msg = "Data has been fetched successfully !";

                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse AssignedApproverForCO(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "ASSIGNED_APPROVER_FOR_CO");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameters[5] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);

                UserResponse oUser = new UserResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                oUser.StatusFl = true;
                oUser.Msg = "Data has been fetched successfully !";

                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public DepositoryResponse GetDepositoryType(Depository objDepository)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_DEPOSITORY_SETTING");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objDepository.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPOSITORY_SETTING", objDepository.MODULE_DATABASE, parameters);
                DepositoryResponse oDepository = new DepositoryResponse();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Depository> lstDepository = new List<Depository>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Depository obj = new Depository();
                        obj.depositoryId = Convert.ToInt32(dr["ID"]);
                        obj.depositoryName = !String.IsNullOrEmpty(Convert.ToString(dr["DEPOSITORY_TYPE"])) ? Convert.ToString(dr["DEPOSITORY_TYPE"]) : String.Empty;
                        lstDepository.Add(obj);
                    }
                    oDepository.StatusFl = true;
                    oDepository.DepositoryList = lstDepository;
                }
                else
                {
                    oDepository.StatusFl = false;
                    oDepository.Msg = "No Depository Type!";
                }
                return oDepository;
            }
            catch (Exception ex)
            {
                DepositoryResponse oDepository = new DepositoryResponse();
                oDepository.StatusFl = false;
                oDepository.Msg = "Processing failed, because of system error !";
                return oDepository;
            }
        }
        public DepositoryResponse GetThresholdByTimeSettings(Depository objDepository)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_SHARES_THRESHOLD_BY_TIME_SETTINGS");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objDepository.companyId);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SHARES_THRESHOLD_BY_TIME_SETTINGS", objDepository.MODULE_DATABASE, parameters);
                DepositoryResponse oDepository = new DepositoryResponse();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Depository> lstDepository = new List<Depository>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Depository obj = new Depository();
                        obj.depositoryId = Convert.ToInt32(dr["ID"]);
                        obj.depositoryName = !String.IsNullOrEmpty(Convert.ToString(dr["DEPOSITORY_TYPE"])) ? Convert.ToString(dr["DEPOSITORY_TYPE"]) : String.Empty;
                        obj.sharesCount = !String.IsNullOrEmpty(Convert.ToString(dr["SHARES_COUNT"])) ? Convert.ToInt32(dr["SHARES_COUNT"]) : 0;
                        obj.thresholdLimit = !String.IsNullOrEmpty(Convert.ToString(dr["THRESHOLD_LIMIT"])) ? Convert.ToInt32(dr["THRESHOLD_LIMIT"]) : 0;
                        obj.byTime = !String.IsNullOrEmpty(Convert.ToString(dr["BY_TIME"])) ? Convert.ToString(dr["BY_TIME"]) : String.Empty;
                        obj.limitType = !String.IsNullOrEmpty(Convert.ToString(dr["LIMIT_TYPE"])) ? Convert.ToString(dr["LIMIT_TYPE"]) : String.Empty;
                        lstDepository.Add(obj);
                    }
                    oDepository.StatusFl = true;
                    oDepository.DepositoryList = lstDepository;
                }
                else
                {
                    oDepository.StatusFl = false;
                    oDepository.Msg = "No Threshold Time Settings!";
                }
                return oDepository;
            }
            catch (Exception ex)
            {
                DepositoryResponse oDepository = new DepositoryResponse();
                oDepository.StatusFl = false;
                oDepository.Msg = "Processing failed, because of system error !";
                return oDepository;
            }
        }
        public DepositoryResponse SaveDepositoryTypeOperation(Depository objDepository)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "ADD_UPDATE_DEPOSITORY_SETTING");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objDepository.companyId);
                DepositoryResponse oDepository = new DepositoryResponse();
                foreach (string obj in objDepository.depository)
                {
                    parameters[3] = new SqlParameter("@DEPOSITORY_TYPE", obj);
                    SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPOSITORY_SETTING", objDepository.MODULE_DATABASE, parameters);
                }

                DeleteUnselectedElement(objDepository.depository, objDepository.companyId, objDepository.MODULE_DATABASE);

                oDepository.StatusFl = true;
                oDepository.Msg = "Data has been fetched successfully !";

                return oDepository;
            }
            catch (Exception ex)
            {
                DepositoryResponse oDepository = new DepositoryResponse();
                oDepository.StatusFl = false;
                oDepository.Msg = "Processing failed, because of system error !";
                return oDepository;
            }
        }
        public DepositoryResponse SaveThresholdLimitAndByTime(List<Depository> objDepositoryList)
        {
            try
            {

                DepositoryResponse oDepository = new DepositoryResponse();
                foreach (Depository obj in objDepositoryList)
                {
                    SqlParameter[] parameters = new SqlParameter[8];
                    parameters[0] = new SqlParameter("@Mode", "ADD_UPDATE_SHARES_THRESHOLD_BY_TIME_SETTINGS");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@COMPANY_ID", obj.companyId);
                    parameters[3] = new SqlParameter("@DEPOSITORY_TYPE", obj.depositoryName);
                    parameters[4] = new SqlParameter("@SHARES_COUNT", obj.sharesCount);
                    parameters[5] = new SqlParameter("@THRESHOLD_LIMIT", obj.thresholdLimit);
                    parameters[6] = new SqlParameter("@BY_TIME", obj.byTime);
                    parameters[7] = new SqlParameter("@LIMIT_TYPE", obj.limitType);
                    SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SHARES_THRESHOLD_BY_TIME_SETTINGS", obj.MODULE_DATABASE, parameters);
                }
                oDepository.StatusFl = true;
                oDepository.Msg = "Data has been fetched successfully !";
                return oDepository;
            }
            catch (Exception ex)
            {
                DepositoryResponse oDepository = new DepositoryResponse();
                oDepository.StatusFl = false;
                oDepository.Msg = "Processing failed, because of system error !";
                return oDepository;
            }
        }
        private void DeleteUnselectedElement(List<string> arr, int companyId, string moduleDatabase)
        {
            List<Int32> tempElementsToDelete = new List<Int32>();
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Mode", "GET_DEPOSITORY_SETTING");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@COMPANY_ID", companyId);
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPOSITORY_SETTING", moduleDatabase, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string depository = !String.IsNullOrEmpty(Convert.ToString(dr["DEPOSITORY_TYPE"])) ? Convert.ToString(dr["DEPOSITORY_TYPE"]) : String.Empty;
                    if (!arr.Contains(depository))
                    {
                        Int32 elementId = Convert.ToInt32(dr["ID"]);
                        tempElementsToDelete.Add(elementId);
                    }
                }
            }

            foreach (Int32 objId in tempElementsToDelete)
            {
                parameters[0] = new SqlParameter("@Mode", "DELETE_UNSELECTED_ID_DEPOSITORY_SETTING");
                parameters[3] = new SqlParameter("@DEPOSITORY_SETTING_ID", objId);
                SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DEPOSITORY_SETTING", moduleDatabase, parameters);
            }

            tempElementsToDelete.Clear();

            SqlParameter[] parameters1 = new SqlParameter[4];
            parameters1[0] = new SqlParameter("@Mode", "GET_SHARES_THRESHOLD_BY_TIME_SETTINGS");
            parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters1[1].Direction = ParameterDirection.Output;
            parameters1[2] = new SqlParameter("@COMPANY_ID", companyId);

            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SHARES_THRESHOLD_BY_TIME_SETTINGS", moduleDatabase, parameters1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    string depository = !String.IsNullOrEmpty(Convert.ToString(dr["DEPOSITORY_TYPE"])) ? Convert.ToString(dr["DEPOSITORY_TYPE"]) : String.Empty;
                    if (!arr.Contains(depository))
                    {
                        Int32 elementId = Convert.ToInt32(dr["ID"]);
                        tempElementsToDelete.Add(elementId);
                    }
                }
            }

            foreach (Int32 objId in tempElementsToDelete)
            {
                parameters1[0] = new SqlParameter("@Mode", "DELETE_SHARES_THRESHOLD_BY_TIME_SETTINGS");
                parameters1[3] = new SqlParameter("@SHARES_THRESHOLD_BY_TIME_SETTINGS_ID", objId);
                SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SHARES_THRESHOLD_BY_TIME_SETTINGS", moduleDatabase, parameters1);
            }
            tempElementsToDelete.Clear();
        }
        public UserResponse GetUserDetails(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_DETAILS");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameters[3] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[4] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);

                UserResponse oUser = new UserResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    User user = new User();
                    user.uploadAvatar = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["UPLOAD_AVATAR"])) ? Convert.ToString(ds.Tables[0].Rows[0]["UPLOAD_AVATAR"]) : String.Empty;
                    user.USER_NM = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_NM"]) : String.Empty;
                    user.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"]) : String.Empty;
                    user.userRole = new Role
                    {
                        ROLE_NM = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ROLE_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["ROLE_NAME"]) : String.Empty
                    };
                    user.panNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PAN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PAN"]) : String.Empty;
                    user.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_MOBILE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_MOBILE"]) : String.Empty;
                    user.designation = new Designation
                    {
                        DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGNATION_ID"]) : 0
                    };
                    user.department = new Department
                    {
                        DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]) : 0
                    };
                    user.businessUnit = new BusinessUnit
                    {
                        businessUnitId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BU_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["BU_ID"]) : 0,
                        businessUnitName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BU_NM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["BU_NM"]) : String.Empty,
                        parentBusinessUnitId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PARENT_BU"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["PARENT_BU"]) : 0,
                        parentBusinessUnitName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PARENT_BU_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PARENT_BU_NAME"]) : String.Empty
                    };
                    user.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["CNT"]);
                    user.PersonalEmail = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PERSONAL_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PERSONAL_EMAIL"]) : String.Empty;
                    user.IsMarried = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IS_MARRIED"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IS_MARRIED"]) : String.Empty;
                    oUser.User = user;
                }

                oUser.StatusFl = true;
                oUser.Msg = "Data has been fetched successfully !";

                return oUser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #region "Add Update Personal Detail"
        public UserResponse AddUpdatePersonalDetail(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[32];
                parameters[0] = new SqlParameter("@MODE", "INSERT_USER_DECLARATION_HDR");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameters[4] = new SqlParameter("@RESIDENT_TYPE", objUser.residentType);
                parameters[5] = new SqlParameter("@PAN", objUser.panNumber);
                parameters[6] = new SqlParameter("@IDENTIFICATION_TYPE", objUser.identificationType);
                parameters[7] = new SqlParameter("@IDENTIFICATION_NUMBER", objUser.identificationNumber);
                parameters[8] = new SqlParameter("@USER_MOBILE", objUser.USER_MOBILE);
                parameters[9] = new SqlParameter("@ADDRESS", objUser.address);
                parameters[10] = new SqlParameter("@PIN_CODE", objUser.pinCode);
                parameters[11] = new SqlParameter("@COUNTRY", objUser.country);
                parameters[12] = new SqlParameter("@JOINING_DATE", FormatHelper.FormatDate(objUser.joiningDate));
                if (!String.IsNullOrEmpty(objUser.becomingInsiderDate))
                {
                    parameters[13] = new SqlParameter("@DATE_BECOMING_INSIDER", FormatHelper.FormatDate(objUser.becomingInsiderDate));
                }
                else
                {
                    parameters[13] = new SqlParameter("@DATE_BECOMING_INSIDER", DBNull.Value);
                }
                parameters[14] = new SqlParameter("@DEPARTMENT_ID", objUser.department.DEPARTMENT_ID);
                parameters[15] = new SqlParameter("@LOCATION", objUser.location.locationId);
                parameters[16] = new SqlParameter("@DESIGNATION_ID", objUser.designation.DESIGNATION_ID);
                parameters[17] = new SqlParameter("@CATEGORY_ID", objUser.category.ID);
                parameters[18] = new SqlParameter("@SUBCATEGORY_ID", objUser.category.subCategory.ID);
                parameters[19] = new SqlParameter("@DIN_NUMBER", objUser.dinNumber);
                parameters[20] = new SqlParameter("@STATUS", objUser.status);
                parameters[21] = new SqlParameter("@EMPLOYEE_ID", objUser.LOGIN_ID);
                parameters[22] = new SqlParameter("@IS_FINAL_DECLARED", objUser.isFinalDeclared);
                parameters[23] = new SqlParameter("@D_ID", objUser.D_ID);
                parameters[24] = new SqlParameter("@INSTITUTE", objUser.institutionName);
                parameters[25] = new SqlParameter("@COURSE_NAME", objUser.stream);
                parameters[26] = new SqlParameter("@EMPLOYER", objUser.employerDetails);
                parameters[27] = new SqlParameter("@EMPLOYEEID", objUser.employeeId);
                parameters[28] = new SqlParameter("@SSN", objUser.Ssn);
                parameters[29] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);
                parameters[30] = new SqlParameter("@IS_MARRIED", objUser.IsMarried);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                if (objUser.D_ID == 0)
                {
                    objUser.D_ID = (Int32)parameters[1].Value;
                }

                parameters[23] = new SqlParameter("@D_ID", objUser.D_ID);

                parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_PERSONAL_DETAILS");
                parameters[31] = new SqlParameter("@PERSONAL_EMAIL", objUser.PersonalEmail);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                UserResponse ouser = new UserResponse();
                ouser.StatusFl = true;
                ouser.Msg = "Data has been saved successfully !";
                ouser.User = objUser;
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add Update Education Details"
        public UserResponse AddUpdateEducationDetails(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();

                if (objUser.lstEducation != null)
                {
                    if (objUser.lstEducation.Count > 0)
                    {
                        foreach (Education objEducation in objUser.lstEducation)
                        {
                            SqlParameter[] parameters = new SqlParameter[10];
                            parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_EDUCATION_DETAILS");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                            parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                            parameters[4] = new SqlParameter("@COURSE_NAME", objEducation.courseName);
                            parameters[5] = new SqlParameter("@INSTITUTE", objEducation.instituteName);
                            parameters[6] = new SqlParameter("@PASSING_MONTH", objEducation.passingMonth);
                            parameters[7] = new SqlParameter("@PASSING_YEAR", objEducation.passingYear);
                            parameters[8] = new SqlParameter("@EMPLOYEE_ID", objUser.CREATED_BY);
                            parameters[9] = new SqlParameter("@EDUCATION_ID", objEducation.ID);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                            ouser.StatusFl = true;
                            ouser.Msg = "Data has been saved successfully !";
                            ouser.User = objUser;
                        }
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Education Data not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Education Details"
        public UserResponse DeleteEducationDetails(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();

                if (objUser.lstEducation != null)
                {
                    if (objUser.lstEducation.Count > 0)
                    {
                        foreach (Education objEducation in objUser.lstEducation)
                        {
                            SqlParameter[] parameters = new SqlParameter[3];
                            parameters[0] = new SqlParameter("@MODE", "DELETE_EDUCATION");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@EDUCATION_ID", objEducation.ID);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                            ouser.StatusFl = true;
                            ouser.Msg = "Data has been deleted successfully !";
                            ouser.User = objUser;
                        }
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Education Data not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Experience Details"
        public UserResponse AddUpdateExperienceDetails(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();

                if (objUser.lstExperience != null)
                {
                    if (objUser.lstExperience.Count > 0)
                    {
                        foreach (Experience objExperience in objUser.lstExperience)
                        {
                            SqlParameter[] parameters = new SqlParameter[10];
                            parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_EXPERIENCE_DETAILS");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                            parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                            parameters[4] = new SqlParameter("@EMPLOYER", objExperience.employer);
                            parameters[5] = new SqlParameter("@USER_ROLE", objExperience.userRole);
                            parameters[6] = new SqlParameter("@DATE_FROM", objExperience.dateFrom);
                            parameters[7] = new SqlParameter("@DATE_TO", objExperience.dateTo);
                            parameters[8] = new SqlParameter("@EMPLOYEE_ID", objExperience.createdBy);
                            parameters[9] = new SqlParameter("@EXPERIENCE_ID", objExperience.ID);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                            ouser.StatusFl = true;
                            ouser.Msg = "Data has been saved successfully !";
                            ouser.User = objUser;
                        }
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Education Data not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Experience Details"
        public UserResponse DeleteExperienceDetails(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();

                if (objUser.lstExperience != null)
                {
                    if (objUser.lstExperience.Count > 0)
                    {
                        foreach (Experience objExperience in objUser.lstExperience)
                        {
                            SqlParameter[] parameters = new SqlParameter[3];
                            parameters[0] = new SqlParameter("@MODE", "DELETE_EXPERIENCE");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@EXPERIENCE_ID", objExperience.ID);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                            ouser.StatusFl = true;
                            ouser.Msg = "Data has been deleted successfully !";
                            ouser.User = objUser;
                        }
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Education Data not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Relative Details"
        public UserResponse AddUpdateRelativeDetails(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();
                if (objUser.lstRelative != null)
                {
                    if (objUser.lstRelative.Count > 0)
                    {
                        foreach (Relative objRelative in objUser.lstRelative)
                        {
                            SqlParameter[] parameters = new SqlParameter[20];
                            parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_RELATIVE_DETAILS");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                            parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                            parameters[4] = new SqlParameter("@RELATIVE_NAME", objRelative.relativeName);
                            parameters[5] = new SqlParameter("@RELATION_ID", objRelative.relation.RELATION_ID);
                            parameters[6] = new SqlParameter("@PAN", objRelative.panNumber);
                            parameters[7] = new SqlParameter("@IDENTIFICATION_TYPE", objRelative.identificationType);
                            parameters[8] = new SqlParameter("@IDENTIFICATION_NUMBER", objRelative.identificationNumber);
                            parameters[9] = new SqlParameter("@USER_MOBILE", objRelative.mobile);
                            parameters[10] = new SqlParameter("@ADDRESS", objRelative.address);
                            parameters[11] = new SqlParameter("@PIN_CODE", objRelative.pinCode);
                            parameters[12] = new SqlParameter("@COUNTRY", objRelative.country);
                            parameters[13] = new SqlParameter("@STATUS", objRelative.status);
                            parameters[14] = new SqlParameter("@EMPLOYEE_ID", objUser.LOGIN_ID);
                            parameters[15] = new SqlParameter("@RELATIVE_ID", objRelative.ID);
                            parameters[16] = new SqlParameter("@D_ID", objUser.D_ID);
                            parameters[17] = new SqlParameter("@RELATIVE_REMARKS", objRelative.remarks);
                            parameters[18] = new SqlParameter("@RELATIVE_EMAIL", objRelative.relativeEmail);
                            parameters[19] = new SqlParameter("@IS_DESIGNATED_PERSON", objRelative.IsdesignatedPerson);

                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters);
                            if (objRelative.ID == 0)
                            {
                                objRelative.ID = (Int32)parameters[1].Value;
                                objRelative.isDeleteRelative = true;
                            }
                        }

                        SqlParameter[] parameters2 = new SqlParameter[3];
                        parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters2[0].Direction = ParameterDirection.Output;
                        parameters2[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID");
                        parameters2[2] = new SqlParameter("@D_ID", objUser.D_ID);
                        SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters2);
                        if (rdr2.HasRows)
                        {
                            List<Relative> lstRelativeDetail = new List<Relative>();
                            while (rdr2.Read())
                            {
                                Relative objRelative = new Relative();
                                objRelative.ID = Convert.ToInt32(rdr2["RELATIVE_ID"]);
                                objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : String.Empty;
                                objRelative.relation = new Relation
                                {
                                    RELATION_ID = Convert.ToInt32(rdr2["RELATION_ID"]),
                                    RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATION_NAME"])) ? Convert.ToString(rdr2["RELATION_NAME"]) : String.Empty
                                };
                                objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
                                objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty;
                                objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_EMAIL"])) ? Convert.ToString(rdr2["RELATIVE_EMAIL"]) : String.Empty;
                                objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr2["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr2["IDENTIFICATION_TYPE"]) : String.Empty;
                                objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr2["IDENTIFICATION_NUMBER"]) : String.Empty;
                                objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr2["MOBILE"])) ? Convert.ToString(rdr2["MOBILE"]) : String.Empty;
                                objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr2["ADDRESS"])) ? Convert.ToString(rdr2["ADDRESS"]) : String.Empty;
                                objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr2["PIN_CODE"])) ? Convert.ToString(rdr2["PIN_CODE"]) : String.Empty;
                                objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr2["COUNTRY"])) ? Convert.ToString(rdr2["COUNTRY"]) : String.Empty;
                                objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr2["STATUS"])) ? Convert.ToString(rdr2["STATUS"]) : String.Empty;
                                objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr2["REMARKS"])) ? Convert.ToString(rdr2["REMARKS"]) : String.Empty;
                                objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
                                //   objRelative.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                                objRelative.isDeleteRelative = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DELETE_RELATIVE"])) ? (Convert.ToString(rdr2["IS_DELETE_RELATIVE"]) == "Yes" ? true : false) : false;
                                objRelative.IsdesignatedPerson = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DP"])) ? Convert.ToString(rdr2["IS_DP"]) : String.Empty;

                                lstRelativeDetail.Add(objRelative);
                            }
                            objUser.lstRelative = lstRelativeDetail;
                        }
                        rdr2.Close();

                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been saved successfully !";
                        ouser.User = objUser;
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Relative Data not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Demat Details"
        public UserResponse AddUpdateDematDetails(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();
                List<DematAccount> newListDematAccount = new List<DematAccount>();
                if (objUser.lstRelative != null)
                {
                    if (objUser.lstRelative.Count > 0)
                    {
                        foreach (Relative objRelative in objUser.lstRelative)
                        {
                            objRelative.createdBy = objUser.LOGIN_ID;
                            objRelative.companyId = objUser.companyId;
                            objRelative.MODULE_DATABASE = objUser.MODULE_DATABASE;
                            List<DematAccount> lstDematAccount = AddUpdateRelativeDematAccount(objRelative, objUser.D_ID);
                            newListDematAccount.AddRange(lstDematAccount);
                        }

                        SqlParameter[] parameters1 = new SqlParameter[3];
                        parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters1[0].Direction = ParameterDirection.Output;
                        parameters1[1] = new SqlParameter("@MODE", "GET_DEMAT_ACCOUNT_DETAILS_BY_D_ID");
                        parameters1[2] = new SqlParameter("@D_ID", objUser.D_ID);
                        SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objUser.MODULE_DATABASE, parameters1);
                        if (rdr1.HasRows)
                        {
                            List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
                            while (rdr1.Read())
                            {
                                DematAccount objDematAccountDetail = new DematAccount();
                                objDematAccountDetail.ID = Convert.ToInt32(rdr1["ACCOUNT_ID"]);
                                objDematAccountDetail.relative = new Relative
                                {
                                    ID = Convert.ToInt32(rdr1["RELATIVE_ID"]),
                                    relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : "Self"
                                };
                                objDematAccountDetail.relative.relativeName = objDematAccountDetail.relative.ID == -1 ? "Not Applicable" : objDematAccountDetail.relative.relativeName;
                                objDematAccountDetail.depositoryName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_NAME"]) : String.Empty;
                                objDematAccountDetail.clientId = !String.IsNullOrEmpty(Convert.ToString(rdr1["CLIENT_ID"])) ? Convert.ToString(rdr1["CLIENT_ID"]) : String.Empty;
                                objDematAccountDetail.depositoryParticipantName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"]) : String.Empty;
                                objDematAccountDetail.depositoryParticipantId = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"]) : String.Empty;
                                objDematAccountDetail.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr1["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr1["TRADING_MEMBER_ID"]) : String.Empty;
                                objDematAccountDetail.dematType = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEMAT_TYPE"])) ? Convert.ToString(rdr1["DEMAT_TYPE"]) : String.Empty;
                                objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
                                objDematAccountDetail.status = !String.IsNullOrEmpty(Convert.ToString(rdr1["STATUS"])) ? Convert.ToString(rdr1["STATUS"]) : String.Empty;
                                objDematAccountDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr1["CREATED_ON"])) ? Convert.ToString(rdr1["CREATED_ON"]) : String.Empty;
                                //  objDematAccountDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                                objDematAccountDetail.isDeleteDemat = !String.IsNullOrEmpty(Convert.ToString(rdr1["IS_DELETE_DEMAT"])) ? (Convert.ToString(rdr1["IS_DELETE_DEMAT"]) == "Yes" ? true : false) : false;
                                lstDematAccountDetail.Add(objDematAccountDetail);
                            }
                            objUser.lstDematAccount = lstDematAccountDetail;
                        }
                        rdr1.Close();

                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been saved successfully !";
                        //    objUser.lstDematAccount = newListDematAccount;
                        ouser.User = objUser;
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Relative Data not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Add and Update Relative Demat Account Details"
        private List<DematAccount> AddUpdateRelativeDematAccount(Relative objRelative, Int32 D_ID)
        {
            //try
            //{
            List<DematAccount> lstDematAccount = new List<DematAccount>();
            UserResponse ouser = new UserResponse();

            if (objRelative.lstDematAccount != null)
            {
                if (objRelative.lstDematAccount.Count > 0)
                {
                    foreach (DematAccount objDematAccount in objRelative.lstDematAccount)
                    {
                        SqlParameter[] parameters = new SqlParameter[16];
                        parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_DEMAT_ACCOUNT_DETAILS");
                        parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters[1].Direction = ParameterDirection.Output;
                        parameters[2] = new SqlParameter("@RELATIVE_ID", objRelative.ID);
                        parameters[3] = new SqlParameter("@DEPOSITORY_NAME", objDematAccount.depositoryName);
                        parameters[4] = new SqlParameter("@CLIENT_ID", objDematAccount.clientId);
                        parameters[5] = new SqlParameter("@DEPOSITORY_PARTICIPANT_NAME", objDematAccount.depositoryParticipantName);
                        parameters[6] = new SqlParameter("@DEPOSITORY_PARTICIPANT_ID", objDematAccount.depositoryParticipantId);
                        parameters[7] = new SqlParameter("@TRADING_MEMBER_ID", objDematAccount.tradingMemberId);
                        parameters[8] = new SqlParameter("@DEMAT_TYPE", objDematAccount.dematType);
                        parameters[9] = new SqlParameter("@ACCOUNT_NO", objDematAccount.accountNo);
                        parameters[10] = new SqlParameter("@DEMAT_ACCOUNT_STATUS", objDematAccount.status);
                        parameters[11] = new SqlParameter("@EMPLOYEE_ID", objRelative.createdBy);
                        parameters[12] = new SqlParameter("@ACCOUNT_ID", objDematAccount.ID);
                        parameters[13] = new SqlParameter("@D_ID", D_ID);
                        parameters[14] = new SqlParameter("@LOGIN_ID", objRelative.createdBy);
                        parameters[15] = new SqlParameter("@COMPANY_ID", objRelative.companyId);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objRelative.MODULE_DATABASE, parameters);
                        if (objDematAccount.ID == 0)
                        {
                            objDematAccount.ID = (Int32)parameters[1].Value;
                            objDematAccount.isDeleteDemat = true;
                        }
                        objDematAccount.relative = new Relative
                        {
                            ID = Convert.ToInt32(objRelative.ID),
                            relativeName = !String.IsNullOrEmpty(Convert.ToString(objRelative.relativeName)) ? Convert.ToString(objRelative.relativeName) : "Self"
                        };
                        lstDematAccount.Add(objDematAccount);
                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been saved successfully !";
                    }
                }
            }
            else
            {
                ouser.StatusFl = false;
                ouser.Msg = "Relative Data not Found !";
            }
            return lstDematAccount;
            //}
            //catch (Exception ex)
            //{
            //    UserResponse oUser = new UserResponse();
            //    oUser.StatusFl = false;
            //    oUser.Msg = "Processing failed, because of system error !";
            //    return oUser;
            //}
        }
        #endregion
        #region "Date Conversion"
        private DateTime ConvertDate(String date)
        {
            String str = String.Empty;
            try
            {
                if (date.Contains("/"))
                {
                    str = date.Split('/')[2] + "-" + date.Split('/')[1] + "-" + date.Split('/')[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Convert.ToDateTime(str);
        }
        #endregion
        #region "Get Demat Account List By Relative Id"
        public RelativeResponse GetDematAccountListByRelativeId(Relative objRelative)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "DEMAT_ACC_LST_BY_RELATIVE_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@RELATIVE_ID", objRelative.ID);
                parameters[3] = new SqlParameter("@LOGIN_ID", objRelative.createdBy);

                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objRelative.MODULE_DATABASE, parameters);
                Relative relative = new Relative();
                List<DematAccount> lstDematAccount = new List<DematAccount>();
                RelativeResponse oRelative = new RelativeResponse();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        DematAccount dematAccount = new DematAccount();
                        dematAccount.ID = Convert.ToInt32(rdr["ACCOUNT_ID"]);
                        dematAccount.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr["ACCOUNT_NO"])) ? Convert.ToString(rdr["ACCOUNT_NO"]) : String.Empty;
                        lstDematAccount.Add(dematAccount);
                    }
                    relative.lstDematAccount = lstDematAccount;
                    oRelative.StatusFl = true;
                    oRelative.Msg = "Data has been fetched successfully !";
                    oRelative.Relative = relative;
                }
                else
                {
                    oRelative.StatusFl = false;
                    oRelative.Msg = "No data found !";
                }
                rdr.Close();

                return oRelative;
            }
            catch (Exception ex)
            {
                RelativeResponse oRelative = new RelativeResponse();
                oRelative.StatusFl = false;
                oRelative.Msg = "Processing failed, because of system error !";
                return oRelative;
            }
        }
        #endregion
        #region "Get Demat Account Info"
        public DematAccountResponse GetDematAccountInfo(DematAccount objDematAccount)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_DEMAT_ACC_INFO_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@ACCOUNT_ID", objDematAccount.ID);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objDematAccount.MODULE_DATABASE, parameters);

                DematAccountResponse oDematAccount = new DematAccountResponse();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DematAccount dematAccount = new DematAccount();
                    dematAccount.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADING_MEMBER_ID"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TRADING_MEMBER_ID"]) : String.Empty;

                    oDematAccount.StatusFl = true;
                    oDematAccount.Msg = "Data has been fetched successfully !";
                    oDematAccount.DematAccount = dematAccount;
                }
                else
                {
                    oDematAccount.StatusFl = false;
                    oDematAccount.Msg = "No data found !";
                }


                return oDematAccount;
            }
            catch (Exception ex)
            {
                DematAccountResponse oDematAccount = new DematAccountResponse();
                oDematAccount.StatusFl = false;
                oDematAccount.Msg = "Processing failed, because of system error !";
                return oDematAccount;
            }
        }
        #endregion       
        #region "Send Email Notice Confirmation"
        public UserResponse SendEmailNoticeConfirmation(User objUser)
        {
            try
            {
                string fileName = String.Empty;

                List<EventBasedForm> lstAllFormEvents = null;
                List<EventBasedForm> lstAllEmailEvents = null;
                List<string> allAttachments = new List<string>();

                UserResponse ouser = new UserResponse();

                String msg = String.Empty;
                String msg1 = String.Empty;
                bool newUser = false;
                bool isChangeInRelative = false;
                User objUserApprover = CommonFunctions.GetITApprover(
                    Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]),
                    objUser.companyId, objUser.LOGIN_ID, objUser.ADMIN_DATABASE, objUser.businessUnit.businessUnitId
                );
                String mailBody = String.Empty;
                string enableHalfYearlyDeclaration = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["EnableHalfYearlyDeclaration"]), true);
                string enableAnnualDeclaration = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["EnableAnnualDeclaration"]), true);
                string enableFormB = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["EnableFormB"]), true);
                string enableFormK = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["EnableFormK"]), true);
                string enableMailOnSubmissionOfDeclaration = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["EnableMailOnSubmissionOfDeclaration"]), true);

                #region "Get flag whether declaration already submitted or not"
                SqlParameter[] parameterNewSubmitted = new SqlParameter[4];
                parameterNewSubmitted[0] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameterNewSubmitted[1] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameterNewSubmitted[2] = new SqlParameter("@MODE", "GET_FLAG_WHETHER_SUBMITTED");
                parameterNewSubmitted[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameterNewSubmitted[3].Direction = ParameterDirection.Output;

                string sqlTsk = "SELECT TOP 1 TASK_FOR FROM PROCS_INSIDER_USER_TASK(NOLOCK) WHERE USER_LOGIN='" + objUser.LOGIN_ID + "' " +
                    "AND TASK_FOR IN('Initial Disclosure Reminder','Annual Disclosure Reminder') AND TASK_STATUS='Open' ORDER BY TASK_ID DESC";
                string sTskFor = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, sqlTsk, Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), null);


                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameterNewSubmitted);
                int alreadySubmitted = Convert.ToInt32(parameterNewSubmitted[3].Value);
                #endregion

                #region "Get flag whether new user or not"
                SqlParameter[] parameterNewUser = new SqlParameter[4];
                parameterNewUser[0] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameterNewUser[1] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameterNewUser[2] = new SqlParameter("@MODE", "GET_FLAG_WHETHER_NEW_USER");
                parameterNewUser[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameterNewUser[3].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameterNewUser);
                int userFlag = Convert.ToInt32(parameterNewUser[3].Value);

                if (userFlag == 1)
                {
                    newUser = true;
                }
                else
                {
                    newUser = false;
                }
                #endregion

                #region "Get flag whether change in relative"
                SqlParameter[] parameterChangeInRelative = new SqlParameter[4];
                parameterChangeInRelative[0] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameterChangeInRelative[1] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameterChangeInRelative[2] = new SqlParameter("@MODE", "GET_FLAG_WHETHER_CHANGE_IN_RELATIVE");
                parameterChangeInRelative[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameterChangeInRelative[3].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameterChangeInRelative);
                int changeInRelativeFlag = Convert.ToInt32(parameterChangeInRelative[3].Value);

                if (changeInRelativeFlag == 1)
                {
                    isChangeInRelative = true;
                }
                else
                {
                    isChangeInRelative = false;
                }
                #endregion

                #region "Form E and F Creation"
                string subEvent = string.Empty;
                string role_name = string.Empty;
                String connectionStringIT = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select B.ROLE_NAME FROM PROCS_INSIDER_USER A JOIN PROCS_INSIDER_ROLE_MSTR B ON A.USER_ROLE=B.ROLE_ID  where A.USER_LOGIN='" + objUser.LOGIN_ID + "'", conn))
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                role_name = Convert.ToString(rdr["ROLE_NAME"]);
                            }
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
                if (role_name == "Promoter" || role_name == "Director" || role_name == "KMP" || role_name == "Promoters Group" || role_name == "Body Corporate")
                {
                    subEvent = "Director";
                }
                else if (role_name == "Connected Person")
                {
                    subEvent = "Connected";
                }
                else if (role_name == "Designated Person" || role_name == "Admin")
                {
                    subEvent = "Designated";
                }
                else
                {
                    subEvent = "Admin";
                }
                if (sTskFor.ToUpper() == "INITIAL DISCLOSURE REMINDER")
                {
                    //string subEvent = "One Time";
                    lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "User Declaration", subEvent);
                }
                else
                {
                    if (Convert.ToBoolean(enableHalfYearlyDeclaration) && Convert.ToBoolean(enableAnnualDeclaration))
                    {
                        int[] arrDeclarationMonth = { 10, 11, 12, 1, 2, 3 };
                        if (arrDeclarationMonth.Contains(DateTime.Now.Month))
                        {
                            lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "Half Yearly Declaration", subEvent);
                        }
                        else
                        {
                            lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "Annual Declaration", subEvent);
                        }
                    }
                    else if (!Convert.ToBoolean(enableHalfYearlyDeclaration) && Convert.ToBoolean(enableAnnualDeclaration))
                    {
                        lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "Annual Declaration", subEvent);
                    }
                    else
                    {
                        lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "Annual Declaration", subEvent);
                    }
                }
                foreach (var obj in lstAllFormEvents)
                {
                    obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";
                    if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                    {
                        string docFileName3 = Form.CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation, HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"));
                        if (!String.IsNullOrEmpty(docFileName3))
                        {
                            //String filePath = "/InsiderTrading/emailAttachment/";
                            Form.ConvertDocToPDF(docFileName3, obj.fileName, HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"));
                            CreateFormLogs(obj.formId, objUser.LOGIN_ID, obj.fileName, objUser.LOGIN_ID, objUser.companyId, objUser.MODULE_DATABASE);
                        }
                    }
                    if (!String.IsNullOrEmpty(obj.fileName))
                    {
                        allAttachments.Add(
                            System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/" + obj.fileName))
                        );
                    }
                }
                objUser.lstAttachment = allAttachments;

                if (newUser)
                {
                    SqlParameter[] parameterNewUser1 = new SqlParameter[4];
                    parameterNewUser1[0] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameterNewUser1[1] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                    parameterNewUser1[2] = new SqlParameter("@MODE", "SET_FLAG_NOT_NEW_USER");
                    parameterNewUser1[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameterNewUser1[3].Direction = ParameterDirection.Output;

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), parameterNewUser1);
                }
                #endregion




                #region Commented Code
                #region "Form E , F , K and B Logs"

                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_USER_DECLARATION_HDR");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@D_ID", objUser.D_ID);
                parameters[3] = new SqlParameter("@IS_FINAL_DECLARED", objUser.isFinalDeclared);
                parameters[4] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameters[5] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[6] = new SqlParameter("@EMPLOYEE_ID", objUser.LOGIN_ID);
                //parameters[7] = new SqlParameter("@ANNUAL_BIANNUAL_ATTACHMENT", objUser.attachmentAnnualOrBiannualDeclaration);
                //parameters[8] = new SqlParameter("@ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES", objUser.attachmentAnnualDisclosureByDesignatedEmployees);
                //parameters[9] = new SqlParameter("@FORMB_DISCLOSURE_BY_DESIGNATED_EMPLOYEES", objUser.attachmentFormBDeclaration);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                #endregion
                #endregion

                lstAllEmailEvents = Form.GetAllEmailEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "User Declaration", "");
                String sCC = "";
                if (lstAllEmailEvents.Count > 0)
                {
                    mailBody = lstAllEmailEvents[0].formTemplate;
                    objUser.email.subject = lstAllEmailEvents[0].formName;
                    sCC = lstAllEmailEvents[0].subEvent;
                }
                else
                {
                    mailBody = @"To,<br/>The Compliance Officer<br/>" + objUserApprover.companyName + ",<br/><br/>Please find enclosed my updated Declaration in requisite Form as per the Insider Trading Code of Conduct.<br/><br/>Regards,<br/>" + objUserApprover.USER_NM + "";
                    objUser.email.subject = "Submission of Declaration";
                }
                sCC = Convert.ToString(ConfigurationManager.AppSettings["SecretarialTeamEmail"]);

                if (!String.IsNullOrEmpty(mailBody))
                {
                    if (Convert.ToBoolean(enableMailOnSubmissionOfDeclaration))
                    {
                        EmailSender.SendMail(
                            objUserApprover.approverEmail, objUser.email.subject, mailBody, objUser.lstAttachment,
                            "User Declaration", objUser.companyId.ToString(), objUser.USER_EMAIL
                        );
                    }
                }
                connectionStringIT = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                using (SqlConnection sCon = new SqlConnection(connectionStringIT))
                {
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCon.Open();

                    string _sql = "UPDATE PROCS_INSIDER_USER_TASK SET TASK_STATUS='Closed',TASK_COMPLETED_ON=GETDATE() WHERE TASK_ID=" + objUser.Task_Id;
                    sCmd.CommandType = CommandType.Text;
                    sCmd.CommandText = _sql;
                    sCmd.ExecuteNonQuery();

                    _sql = "SELECT A.ID,ISNULL(B.TASK_ID,0) AS TASK_ID FROM PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK(NOLOCK) A " +
                        "LEFT JOIN PROCS_INSIDER_USER_TASK(NOLOCK) B ON A.ID=B.Hdr_Id AND B.USER_LOGIN='" + objUser.LOGIN_ID + "' " +
                        "WHERE CONVERT(DATE,GETDATE()) BETWEEN A.TASK_START_DATE AND A.TASK_END_DATE";
                    DataSet dsTsk = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objUser.MODULE_DATABASE, null);
                    DataTable dtTsk = dsTsk.Tables[0];

                    if (dtTsk.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtTsk.Rows[0]["TASK_ID"]) == 0)
                        {
                            _sql = "INSERT INTO PROCS_INSIDER_USER_TASK(" +
                                "USER_LOGIN,TASK_FOR,TASK_CREATED_ON,TASK_STATUS,TASK_START_DT,TASK_END_DT,DATA_ELEMENT_ID,Hdr_Id) SELECT " +
                                "'" + objUser.LOGIN_ID + "','Annual Disclosure Reminder',GETDATE(),'Open',TASK_START_DATE,TASK_END_DATE," +
                                "'" + objUser.LOGIN_ID + "',ID FROM PROCS_INSIDER_ANNUAL_DISCLOSURE_TASK(NOLOCK) WHERE ID=" + Convert.ToInt32(dtTsk.Rows[0]["ID"]);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objUser.MODULE_DATABASE, null);
                        }
                    }
                }

                

                ouser.StatusFl = true;
                ouser.Msg = "Mail has been sent successfully !";
                ouser.User = objUser;

                return ouser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Check Whether Educational And Professional Qualification Exist Or Not"
        public bool CheckWhetherEducationalAndProfessionalQualificationExist(User objUser)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@D_ID", objUser.D_ID);
                parameter[1] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameter[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;
                parameter[4] = new SqlParameter("@MODE", "CHECK_WHETHER_EDU_AND_PROF_QUAL_EXIST");

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameter);

                if (Convert.ToInt32(parameter[3].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Save Educational And Professional Qualification"
        public UserResponse SaveEducationalAndProfessionalQualification(EducationalAndProfessionalDetail objEduAndProf)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[8];
                parameter[0] = new SqlParameter("@D_ID", objEduAndProf.D_ID);
                parameter[1] = new SqlParameter("@COMPANY_ID", objEduAndProf.companyId);
                parameter[2] = new SqlParameter("@LOGIN_ID", objEduAndProf.loginId);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;
                parameter[4] = new SqlParameter("@MODE", "ADD_UPDATE_EDUCATIONAL_AND_PROFESSIONAL_DETAILS");
                parameter[5] = new SqlParameter("@INSTITUTE", objEduAndProf.institutionName);
                parameter[6] = new SqlParameter("@COURSE_NAME", objEduAndProf.stream);
                parameter[7] = new SqlParameter("@EMPLOYER", objEduAndProf.employerDetails);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objEduAndProf.MODULE_DATABASE, parameter);

                if (Convert.ToInt32(parameter[3].Value) == 1)
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Information has been added successfully!";
                    return objResponse;
                }
                else
                {
                    UserResponse objResponse = new UserResponse();
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Information has been updated successfully!";
                    return objResponse;
                }

            }
            catch (Exception ex)
            {
                UserResponse objResponse = new UserResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = Convert.ToString("Processing failed due to system error!");
                return objResponse;
            }
        }
        #endregion
        #region "Get Forms"
        public DataTable GetForms(User objRequest, string formType)
        {
            DataTable dtForms = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_PROCS_INSIDER_FORMS", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@MODE", formType));
                        cmd.Parameters.Add(new SqlParameter("@COMPANY_ID", objRequest.companyId));
                        cmd.Parameters.Add(new SqlParameter("@USER_LOGIN", objRequest.LOGIN_ID));
                        cmd.Parameters.Add(new SqlParameter("@PRE_REQUEST_CLEARENCE_ID", 0));
                        cmd.Parameters.Add(new SqlParameter("@BROKER_NOTE_ID", 0));
                        cmd.Parameters.Add(new SqlParameter("@ADMIN_DATABASE", objRequest.ADMIN_DATABASE));
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            dtForms.Load(rdr);
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
                if (dtForms.Rows.Count > 0)
                {
                    return dtForms;
                }
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "", "", objRequest.LOGIN_ID, 5, objRequest.companyId);
            }
            return null;
        }
        #endregion
        #region "return html"
        private String ReturnFormHtml(DataTable dtForms, String formType, User objUser)
        {
            String htmlText = String.Empty;

            foreach (DataRow dr in dtForms.Rows)
            {
                #region "FORM B"

                if (formType == "FORM_B")
                {
                    SqlParameter[] parameters = new SqlParameter[19];
                    parameters[0] = new SqlParameter("@MODE", "GET_FORM_B_TEMPLATE_CREATION");
                    parameters[1] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[2] = new SqlParameter("@COMPANY_NAME", Convert.ToString(dr["COMPANY_NAME"]));
                    parameters[3] = new SqlParameter("@ISIN_NUMBER", Convert.ToString(dr["ISIN_NUMBER"]));
                    parameters[4] = new SqlParameter("@USER_NAME", Convert.ToString(dr["USER_NAME"]));
                    parameters[5] = new SqlParameter("@PAN", Convert.ToString(dr["PAN"]));
                    parameters[6] = new SqlParameter("@CIN_DIN", Convert.ToString(dr["CIN_DIN"]));
                    parameters[7] = new SqlParameter("@USER_ADDRESS", Convert.ToString(dr["USER_ADDRESS"]));
                    parameters[8] = new SqlParameter("@USER_CONTACT", Convert.ToString(dr["USER_CONTACT"]));
                    parameters[9] = new SqlParameter("@CATEGORY_NAME", Convert.ToString(dr["CATEGORY_NAME"]));
                    parameters[10] = new SqlParameter("@DATE_OF_APPOINTMENT", Convert.ToString(dr["DATE_OF_APPOINTMENT"]));
                    parameters[11] = new SqlParameter("@TYPE_OF_SECURITY_HELD", Convert.ToString(dr["TYPE_OF_SECURITY_HELD"]));
                    parameters[12] = new SqlParameter("@NO_OF_SHARES_HELD", Convert.ToString(dr["NO_OF_SHARES_HELD"]));
                    parameters[13] = new SqlParameter("@PERCENTAGE_OF_SHARES_HELD", Convert.ToString(dr["PERCENTAGE_OF_SHARES_HELD"]));
                    parameters[14] = new SqlParameter("@CURRENT_DATE", DateTime.Now.ToString("dd/MM/yyyy"));
                    parameters[15] = new SqlParameter("@LOCATION_NAME", Convert.ToString(dr["LOCATION_NAME"]));
                    parameters[16] = new SqlParameter("@DESIGNATION_NAME", Convert.ToString(dr["DESIGNATION_NAME"]));
                    parameters[17] = new SqlParameter("@DEPARTMENT_NAME", Convert.ToString(dr["DEPARTMENT_NAME"]));
                    parameters[18] = new SqlParameter("@USER_LOGIN", Convert.ToString(dr["USER_LOGIN"]));

                    DataSet ds4 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORM_B_TEMPLATE_CREATION", objUser.MODULE_DATABASE, parameters);

                    if (ds4.Tables.Count > 0)
                    {
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in ds4.Tables[0].Rows)
                            {
                                htmlText = !String.IsNullOrEmpty(Convert.ToString(dr1["LAYOUT_TEMPLATE"])) ? Convert.ToString(dr1["LAYOUT_TEMPLATE"]) : String.Empty;

                            }
                        }
                    }

                }

                #endregion
            }
            return htmlText;
        }
        #endregion
        #region "return form name"
        private String ReturnFormName(DataTable dtForms, String formType, User objUser)
        {
            String formBDisplayName = String.Empty;

            foreach (DataRow dr in dtForms.Rows)
            {
                #region "FORM B"

                if (formType == "FORM_B")
                {
                    SqlParameter[] parameters = new SqlParameter[19];
                    parameters[0] = new SqlParameter("@MODE", "GET_FORM_B_TEMPLATE_CREATION");
                    parameters[1] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[2] = new SqlParameter("@COMPANY_NAME", Convert.ToString(dr["COMPANY_NAME"]));
                    parameters[3] = new SqlParameter("@ISIN_NUMBER", Convert.ToString(dr["ISIN_NUMBER"]));
                    parameters[4] = new SqlParameter("@USER_NAME", Convert.ToString(dr["USER_NAME"]));
                    parameters[5] = new SqlParameter("@PAN", Convert.ToString(dr["PAN"]));
                    parameters[6] = new SqlParameter("@CIN_DIN", Convert.ToString(dr["CIN_DIN"]));
                    parameters[7] = new SqlParameter("@USER_ADDRESS", Convert.ToString(dr["USER_ADDRESS"]));
                    parameters[8] = new SqlParameter("@USER_CONTACT", Convert.ToString(dr["USER_CONTACT"]));
                    parameters[9] = new SqlParameter("@CATEGORY_NAME", Convert.ToString(dr["CATEGORY_NAME"]));
                    parameters[10] = new SqlParameter("@DATE_OF_APPOINTMENT", Convert.ToString(dr["DATE_OF_APPOINTMENT"]));
                    parameters[11] = new SqlParameter("@TYPE_OF_SECURITY_HELD", Convert.ToString(dr["TYPE_OF_SECURITY_HELD"]));
                    parameters[12] = new SqlParameter("@NO_OF_SHARES_HELD", Convert.ToString(dr["NO_OF_SHARES_HELD"]));
                    parameters[13] = new SqlParameter("@PERCENTAGE_OF_SHARES_HELD", Convert.ToString(dr["PERCENTAGE_OF_SHARES_HELD"]));
                    parameters[14] = new SqlParameter("@CURRENT_DATE", DateTime.Now.ToString("dd/MM/yyyy"));
                    parameters[15] = new SqlParameter("@LOCATION_NAME", Convert.ToString(dr["LOCATION_NAME"]));
                    parameters[16] = new SqlParameter("@DESIGNATION_NAME", Convert.ToString(dr["DESIGNATION_NAME"]));
                    parameters[17] = new SqlParameter("@DEPARTMENT_NAME", Convert.ToString(dr["DEPARTMENT_NAME"]));
                    parameters[18] = new SqlParameter("@USER_LOGIN", Convert.ToString(dr["USER_LOGIN"]));

                    DataSet ds4 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORM_B_TEMPLATE_CREATION", objUser.MODULE_DATABASE, parameters);

                    if (ds4.Tables.Count > 0)
                    {
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in ds4.Tables[0].Rows)
                            {
                                formBDisplayName = !String.IsNullOrEmpty(Convert.ToString(dr1["DISPLAY_NAME"])) ? Convert.ToString(dr1["DISPLAY_NAME"]) : String.Empty;

                            }
                        }
                    }

                }

                #endregion
            }
            return formBDisplayName;
        }
        #endregion
        //#region "Get Transactional Information"
        //public UserResponse GetTransactionalInfo(User objUser)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = new SqlParameter[3];
        //        parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //        parameters[0].Direction = ParameterDirection.Output;
        //        parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
        //        parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
        //        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
        //        Int32 declarationObject = (Int32)parameters[0].Value;
        //        UserResponse objUserResponse = new UserResponse();
        //        if (declarationObject > 0)
        //        {
        //            parameters[1] = new SqlParameter("@MODE", "GET_PERSONAL_DETAILS_BY_D_ID");
        //            parameters[2] = new SqlParameter("@D_ID", declarationObject);
        //            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                User obj = new User();
        //                obj.residentType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"]) : String.Empty;
        //                obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PAN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PAN"]) : String.Empty;
        //                obj.identificationType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"]) : String.Empty;
        //                obj.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"]) : String.Empty;
        //                obj.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"])) ? Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"]) : String.Empty;
        //                obj.address = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"]) : String.Empty;
        //                obj.pinCode = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"]) : String.Empty;
        //                obj.country = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"])) ? Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"]) : String.Empty;
        //                obj.IsMarried = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IS_MARRIED"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IS_MARRIED"]) : String.Empty;
        //                obj.Ssn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SSN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["SSN"]) : String.Empty;
        //                obj.employeeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"]) : String.Empty;

        //                obj.joiningDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"]) : String.Empty;
        //                obj.becomingInsiderDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"]) : String.Empty;
        //                obj.becomingInsiderDate = obj.becomingInsiderDate == "1/1/1900 12:00:00 AM" ? String.Empty : obj.becomingInsiderDate;
        //                obj.department = new Department
        //                {
        //                    DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]) : 0
        //                };
        //                obj.location = new Location
        //                {
        //                    locationId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LOCATION"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]) : 0
        //                };
        //                obj.designation = new Designation
        //                {
        //                    DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGNATION_ID"]) : 0
        //                };
        //                obj.category = new Category
        //                {
        //                    ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORY_ID"]) : 0,
        //                    subCategory = new SubCategory
        //                    {
        //                        ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"]) : 0
        //                    }
        //                };
        //                obj.dinNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"]) : String.Empty;
        //                obj.status = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STATUS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) : String.Empty;
        //                obj.D_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["D_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["D_ID"]) : 0;
        //                obj.companyId = objUser.companyId;
        //                obj.MODULE_DATABASE = objUser.MODULE_DATABASE;
        //                obj.moduleId = objUser.moduleId;
        //                obj.LOGIN_ID = objUser.LOGIN_ID;
        //                obj.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"]) : String.Empty;
        //                obj.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
        //                obj.institutionName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"]) : String.Empty;
        //                obj.stream = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STREAM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STREAM"]) : String.Empty;
        //                obj.employerDetails = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"]) : String.Empty;

        //                objUser = obj;
        //            }

        //            parameters[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID");
        //            SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters);
        //            if (rdr.HasRows)
        //            {
        //                List<Relative> lstRelativeDetail = new List<Relative>();
        //                while (rdr.Read())
        //                {
        //                    Relative objRelative = new Relative();
        //                    objRelative.ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_ID"])) ? Convert.ToInt32(rdr["RELATIVE_ID"]) : 0;
        //                    objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
        //                    objRelative.relation = new Relation
        //                    {
        //                        RELATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_ID"])) ? Convert.ToInt32(rdr["RELATION_ID"]) : 0,
        //                        RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_NAME"])) ? Convert.ToString(rdr["RELATION_NAME"]) : String.Empty
        //                    };
        //                    objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
        //                    objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
        //                    objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_EMAIL"])) ? Convert.ToString(rdr["RELATIVE_EMAIL"]) : String.Empty;

        //                    objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
        //                    objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
        //                    objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr["MOBILE"])) ? Convert.ToString(rdr["MOBILE"]) : String.Empty;
        //                    objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"])) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
        //                    objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr["PIN_CODE"])) ? Convert.ToString(rdr["PIN_CODE"]) : String.Empty;
        //                    objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr["COUNTRY"])) ? Convert.ToString(rdr["COUNTRY"]) : String.Empty;
        //                    objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"])) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
        //                    objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"])) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
        //                    objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
        //                    objRelative.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
        //                    objRelative.isDeleteRelative = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DELETE_RELATIVE"])) ? (Convert.ToString(rdr["IS_DELETE_RELATIVE"]) == "Yes" ? true : false) : false;
        //                    objRelative.IsdesignatedPerson = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DP"])) ? Convert.ToString(rdr["IS_DP"]) : String.Empty;
        //                    objRelative.IsMandatory = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_MANDATORY"])) ? Convert.ToString(rdr["IS_MANDATORY"]) : String.Empty;
        //                    lstRelativeDetail.Add(objRelative);

        //                }
        //                objUser.lstRelative = lstRelativeDetail;
        //            }
        //            rdr.Close();

        //            SqlParameter[] parameters1 = new SqlParameter[3];
        //            parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //            parameters1[0].Direction = ParameterDirection.Output;
        //            parameters1[1] = new SqlParameter("@MODE", "GET_DEMAT_ACCOUNT_DETAILS_BY_D_ID");
        //            parameters1[2] = new SqlParameter("@D_ID", declarationObject);
        //            SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objUser.MODULE_DATABASE, parameters1);
        //            if (rdr1.HasRows)
        //            {
        //                List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
        //                while (rdr1.Read())
        //                {
        //                    DematAccount objDematAccountDetail = new DematAccount();
        //                    objDematAccountDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_ID"])) ? Convert.ToInt32(rdr1["ACCOUNT_ID"]) : 0;
        //                    objDematAccountDetail.relative = new Relative
        //                    {
        //                        ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_ID"])) ? Convert.ToInt32(rdr1["RELATIVE_ID"]) : 0,
        //                        relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : "Self"
        //                    };
        //                    objDematAccountDetail.relative.relativeName = objDematAccountDetail.relative.ID == -1 ? "Not Applicable" : objDematAccountDetail.relative.relativeName;
        //                    objDematAccountDetail.depositoryName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_NAME"]) : String.Empty;
        //                    objDematAccountDetail.clientId = !String.IsNullOrEmpty(Convert.ToString(rdr1["CLIENT_ID"])) ? Convert.ToString(rdr1["CLIENT_ID"]) : String.Empty;
        //                    objDematAccountDetail.depositoryParticipantName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"]) : String.Empty;
        //                    objDematAccountDetail.depositoryParticipantId = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"]) : String.Empty;
        //                    objDematAccountDetail.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr1["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr1["TRADING_MEMBER_ID"]) : String.Empty;
        //                    objDematAccountDetail.dematType = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEMAT_TYPE"])) ? Convert.ToString(rdr1["DEMAT_TYPE"]) : String.Empty;
        //                    objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
        //                    objDematAccountDetail.status = !String.IsNullOrEmpty(Convert.ToString(rdr1["STATUS"])) ? Convert.ToString(rdr1["STATUS"]) : String.Empty;
        //                    objDematAccountDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr1["CREATED_ON"])) ? Convert.ToString(rdr1["CREATED_ON"]) : String.Empty;
        //                    objDematAccountDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
        //                    objDematAccountDetail.isDeleteDemat = !String.IsNullOrEmpty(Convert.ToString(rdr1["IS_DELETE_DEMAT"])) ? (Convert.ToString(rdr1["IS_DELETE_DEMAT"]) == "Yes" ? true : false) : false;
        //                    lstDematAccountDetail.Add(objDematAccountDetail);
        //                }
        //                objUser.lstDematAccount = lstDematAccountDetail;
        //            }
        //            rdr1.Close();

        //            SqlParameter[] parameters2 = new SqlParameter[3];
        //            parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //            parameters2[0].Direction = ParameterDirection.Output;
        //            parameters2[1] = new SqlParameter("@MODE", "GET_INITIAL_HOLDING_DETAILS_BY_D_ID");
        //            parameters2[2] = new SqlParameter("@D_ID", declarationObject);
        //            SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters2);
        //            if (rdr2.HasRows)
        //            {
        //                List<InitialHoldingDetail> lstInitialHoldingDetail = new List<InitialHoldingDetail>();
        //                while (rdr2.Read())
        //                {
        //                    InitialHoldingDetail initialHoldingDetail = new InitialHoldingDetail();
        //                    initialHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["HOLDING_ID"])) ? Convert.ToInt32(rdr2["HOLDING_ID"]) : 0;
        //                    initialHoldingDetail.restrictedCompany = new RestrictedCompanies
        //                    {
        //                        ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr2["RESTRICTED_COMPANY_ID"]) : 0,
        //                        companyName = !String.IsNullOrEmpty(Convert.ToString(rdr2["COMPANY_NAME"])) ? Convert.ToString(rdr2["COMPANY_NAME"]) : String.Empty
        //                    };
        //                    initialHoldingDetail.restrictedCompany.companyName = initialHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : initialHoldingDetail.restrictedCompany.companyName;
        //                    initialHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE"])) ? Convert.ToString(rdr2["SECURITY_TYPE"]) : String.Empty;
        //                    initialHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr2["SECURITY_TYPE_NAME"]) : String.Empty;
        //                    initialHoldingDetail.securityTypeName = initialHoldingDetail.securityType == "0" ? "Not Applicable" : initialHoldingDetail.securityTypeName;
        //                    initialHoldingDetail.relative = new Relative
        //                    {
        //                        ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_ID"])) ? Convert.ToInt32(rdr2["RELATIVE_ID"]) : 0,
        //                        relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : "Self",
        //                        panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty
        //                    };
        //                    initialHoldingDetail.relative.relativeName = initialHoldingDetail.relative.ID == -1 ? "Not Applicable" : initialHoldingDetail.relative.relativeName;
        //                    initialHoldingDetail.dematAccount = new DematAccount
        //                    {
        //                        //  ID = Convert.ToInt32(rdr2["ACCOUNT_ID"]),
        //                        accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"]) : String.Empty,
        //                        tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr2["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr2["TRADING_MEMBER_ID"]) : String.Empty
        //                    };

        //                    initialHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr2["CURRENT_HOLDING"])) ? Convert.ToInt32(rdr2["CURRENT_HOLDING"]) : 0;
        //                    initialHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
        //                    initialHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
        //                    initialHoldingDetail.isDeleteInitialHolding = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;

        //                    lstInitialHoldingDetail.Add(initialHoldingDetail);
        //                }
        //                objUser.lstInitialHoldingDetail = lstInitialHoldingDetail;
        //            }
        //            rdr2.Close();

        //            SqlParameter[] parameters3 = new SqlParameter[3];
        //            parameters3[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //            parameters3[0].Direction = ParameterDirection.Output;
        //            parameters3[1] = new SqlParameter("@MODE", "GET_FINAL_DECLARATION_DETAILS_BY_D_ID");
        //            parameters3[2] = new SqlParameter("@D_ID", declarationObject);
        //            SqlDataReader rdr3 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters3);
        //            if (rdr3.HasRows)
        //            {
        //                List<FinalDeclaration> lstFinalDeclaration = new List<FinalDeclaration>();
        //                while (rdr3.Read())
        //                {
        //                    FinalDeclaration finalDeclaration = new FinalDeclaration();
        //                    finalDeclaration.createdOn = !String.IsNullOrEmpty(Convert.ToString(rdr3["CREATED_ON"])) ? Convert.ToString(rdr3["CREATED_ON"]) : String.Empty;
        //                    finalDeclaration.createdBy = !String.IsNullOrEmpty(Convert.ToString(rdr3["CREATED_BY"])) ? Convert.ToString(rdr3["CREATED_BY"]) : String.Empty;
        //                    finalDeclaration.fileName = !String.IsNullOrEmpty(Convert.ToString(rdr3["POLICY_DOCUMENT"])) ? Convert.ToString(rdr3["POLICY_DOCUMENT"]) : String.Empty;
        //                    finalDeclaration.fileFormB = !String.IsNullOrEmpty(Convert.ToString(rdr3["FORMB_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"])) ? Convert.ToString(rdr3["FORMB_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"]) : String.Empty;
        //                    finalDeclaration.fileFormEOrF = !String.IsNullOrEmpty(Convert.ToString(rdr3["ANNUAL_BIANNUAL_ATTACHMENT"])) ? Convert.ToString(rdr3["ANNUAL_BIANNUAL_ATTACHMENT"]) : String.Empty;
        //                    finalDeclaration.fileFormK = !String.IsNullOrEmpty(Convert.ToString(rdr3["ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"])) ? Convert.ToString(rdr3["ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"]) : String.Empty;
        //                    finalDeclaration.version = !String.IsNullOrEmpty(Convert.ToString(rdr3["VERSION_ID"])) ? Convert.ToInt32(rdr3["VERSION_ID"]) : 0;
        //                    lstFinalDeclaration.Add(finalDeclaration);
        //                }
        //                objUser.lstFinalDeclaration = lstFinalDeclaration;
        //            }
        //            rdr3.Close();

        //            SqlParameter[] parameters4 = new SqlParameter[3];
        //            parameters4[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //            parameters4[0].Direction = ParameterDirection.Output;
        //            parameters4[1] = new SqlParameter("@MODE", "GET_PHYSICAL_HOLDING_DETAILS_BY_D_ID");
        //            parameters4[2] = new SqlParameter("@D_ID", declarationObject);
        //            SqlDataReader rdr4 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters4);
        //            if (rdr4.HasRows)
        //            {
        //                List<PhysicalHoldingDetail> lstPhysicalHoldingDetail = new List<PhysicalHoldingDetail>();
        //                while (rdr4.Read())
        //                {
        //                    PhysicalHoldingDetail physicalHoldingDetail = new PhysicalHoldingDetail();
        //                    physicalHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["HOLDING_ID"])) ? Convert.ToInt32(rdr4["HOLDING_ID"]) : 0;
        //                    physicalHoldingDetail.restrictedCompany = new RestrictedCompanies
        //                    {
        //                        ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr4["RESTRICTED_COMPANY_ID"]) : 0,
        //                        companyName = !String.IsNullOrEmpty(Convert.ToString(rdr4["COMPANY_NAME"])) ? Convert.ToString(rdr4["COMPANY_NAME"]) : String.Empty
        //                    };
        //                    physicalHoldingDetail.restrictedCompany.companyName = physicalHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : physicalHoldingDetail.restrictedCompany.companyName;

        //                    physicalHoldingDetail.relative = new Relative
        //                    {
        //                        ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_ID"])) ? Convert.ToInt32(rdr4["RELATIVE_ID"]) : 0,
        //                        relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_NAME"])) ? Convert.ToString(rdr4["RELATIVE_NAME"]) : "Self",
        //                        panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr4["PAN"])) ? Convert.ToString(rdr4["PAN"]) : String.Empty
        //                    };
        //                    physicalHoldingDetail.relative.relativeName = physicalHoldingDetail.relative.ID == -1 ? "Not Applicable" : physicalHoldingDetail.relative.relativeName;
        //                    physicalHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr4["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr4["NO_OF_SECURITIES"]) : 0;
        //                    physicalHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr4["CREATED_ON"])) ? Convert.ToString(rdr4["CREATED_ON"]) : String.Empty;
        //                    physicalHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
        //                    physicalHoldingDetail.isDeletePhysicalHolding = !String.IsNullOrEmpty(Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
        //                    physicalHoldingDetail.dematAccountNo = !String.IsNullOrEmpty(Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"]) : String.Empty;
        //                    physicalHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE"])) ? Convert.ToString(rdr4["SECURITY_TYPE"]) : String.Empty;
        //                    physicalHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr4["SECURITY_TYPE_NAME"]) : String.Empty;

        //                    lstPhysicalHoldingDetail.Add(physicalHoldingDetail);
        //                }
        //                objUser.lstPhysicalHoldingDetail = lstPhysicalHoldingDetail;
        //            }
        //            rdr4.Close();

        //            objUserResponse.StatusFl = true;
        //            objUserResponse.Msg = "Data has been fetched succesfully !";
        //            objUserResponse.User = objUser;
        //        }
        //        else
        //        {
        //            objUserResponse.StatusFl = false;
        //            objUserResponse.Msg = "No data found !";
        //        }
        //        return objUserResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        UserResponse objUserResponse = new UserResponse();
        //        objUserResponse.StatusFl = false;
        //        objUserResponse.Msg = "Processing failed, because of system error !";

        //        return objUserResponse;
        //    }
        //}
        //#endregion
        public UserResponse GetDeclarationForms(User objUser)
        {
            try
            {
                objUser.LOGIN_ID = !String.IsNullOrEmpty(Convert.ToString(objUser.LOGIN_ID)) ? Convert.ToString(objUser.LOGIN_ID) : Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();
                if (declarationObject > 0)
                {
                    parameters[1] = new SqlParameter("@MODE", "GET_PERSONAL_DETAILS_BY_D_ID");
                    parameters[2] = new SqlParameter("@D_ID", declarationObject);
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        User obj = new User();
                        obj.residentType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"]) : String.Empty;
                        obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PAN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PAN"]) : String.Empty;
                        obj.identificationType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"]) : String.Empty;
                        obj.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"]) : String.Empty;
                        obj.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"])) ? Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"]) : String.Empty;
                        obj.address = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"]) : String.Empty;
                        obj.pinCode = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"]) : String.Empty;
                        obj.country = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"])) ? Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"]) : String.Empty;

                        obj.Ssn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SSN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["SSN"]) : String.Empty;
                        obj.employeeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"]) : String.Empty;

                        obj.joiningDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"]) : String.Empty;
                        obj.becomingInsiderDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"]) : String.Empty;
                        obj.becomingInsiderDate = obj.becomingInsiderDate == "1/1/1900 12:00:00 AM" ? String.Empty : obj.becomingInsiderDate;
                        obj.department = new Department
                        {
                            DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]) : 0
                        };
                        obj.location = new Location
                        {
                            locationId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LOCATION"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]) : 0
                        };
                        obj.designation = new Designation
                        {
                            DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGNATION_ID"]) : 0
                        };
                        obj.category = new Category
                        {
                            ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORY_ID"]) : 0,
                            subCategory = new SubCategory
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"]) : 0
                            }
                        };
                        obj.dinNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"]) : String.Empty;
                        obj.status = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STATUS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) : String.Empty;
                        obj.D_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["D_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["D_ID"]) : 0;
                        obj.companyId = objUser.companyId;
                        obj.MODULE_DATABASE = objUser.MODULE_DATABASE;
                        obj.moduleId = objUser.moduleId;
                        obj.LOGIN_ID = objUser.LOGIN_ID;
                        obj.ADMIN_DATABASE = objUser.ADMIN_DATABASE;
                        obj.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"]) : String.Empty;
                        obj.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                        obj.institutionName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"]) : String.Empty;
                        obj.stream = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STREAM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STREAM"]) : String.Empty;
                        obj.employerDetails = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"]) : String.Empty;
                        objUser = obj;
                    }

                    parameters[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID");
                    SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (rdr.HasRows)
                    {
                        List<Relative> lstRelativeDetail = new List<Relative>();
                        while (rdr.Read())
                        {
                            Relative objRelative = new Relative();
                            objRelative.ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_ID"])) ? Convert.ToInt32(rdr["RELATIVE_ID"]) : 0;
                            objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
                            objRelative.relation = new Relation
                            {
                                RELATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_ID"])) ? Convert.ToInt32(rdr["RELATION_ID"]) : 0,
                                RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_NAME"])) ? Convert.ToString(rdr["RELATION_NAME"]) : String.Empty
                            };
                            objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
                            objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                            objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_EMAIL"])) ? Convert.ToString(rdr["RELATIVE_EMAIL"]) : String.Empty;

                            objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
                            objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
                            objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr["MOBILE"])) ? Convert.ToString(rdr["MOBILE"]) : String.Empty;
                            objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"])) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                            objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr["PIN_CODE"])) ? Convert.ToString(rdr["PIN_CODE"]) : String.Empty;
                            objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr["COUNTRY"])) ? Convert.ToString(rdr["COUNTRY"]) : String.Empty;
                            objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"])) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                            objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"])) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
                            objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                            objRelative.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objRelative.isDeleteRelative = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DELETE_RELATIVE"])) ? (Convert.ToString(rdr["IS_DELETE_RELATIVE"]) == "Yes" ? true : false) : false;
                            lstRelativeDetail.Add(objRelative);
                        }
                        objUser.lstRelative = lstRelativeDetail;
                    }
                    rdr.Close();

                    SqlParameter[] parameters1 = new SqlParameter[3];
                    parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[0].Direction = ParameterDirection.Output;
                    parameters1[1] = new SqlParameter("@MODE", "GET_DEMAT_ACCOUNT_DETAILS_BY_D_ID");
                    parameters1[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objUser.MODULE_DATABASE, parameters1);
                    if (rdr1.HasRows)
                    {
                        List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
                        while (rdr1.Read())
                        {
                            DematAccount objDematAccountDetail = new DematAccount();
                            objDematAccountDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_ID"])) ? Convert.ToInt32(rdr1["ACCOUNT_ID"]) : 0;
                            objDematAccountDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_ID"])) ? Convert.ToInt32(rdr1["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : "Self"
                            };
                            objDematAccountDetail.relative.relativeName = objDematAccountDetail.relative.ID == -1 ? "Not Applicable" : objDematAccountDetail.relative.relativeName;
                            objDematAccountDetail.depositoryName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_NAME"]) : String.Empty;
                            objDematAccountDetail.clientId = !String.IsNullOrEmpty(Convert.ToString(rdr1["CLIENT_ID"])) ? Convert.ToString(rdr1["CLIENT_ID"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantId = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"]) : String.Empty;
                            objDematAccountDetail.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr1["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr1["TRADING_MEMBER_ID"]) : String.Empty;
                            objDematAccountDetail.dematType = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEMAT_TYPE"])) ? Convert.ToString(rdr1["DEMAT_TYPE"]) : String.Empty;
                            objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
                            objDematAccountDetail.status = !String.IsNullOrEmpty(Convert.ToString(rdr1["STATUS"])) ? Convert.ToString(rdr1["STATUS"]) : String.Empty;
                            objDematAccountDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr1["CREATED_ON"])) ? Convert.ToString(rdr1["CREATED_ON"]) : String.Empty;
                            objDematAccountDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objDematAccountDetail.isDeleteDemat = !String.IsNullOrEmpty(Convert.ToString(rdr1["IS_DELETE_DEMAT"])) ? (Convert.ToString(rdr1["IS_DELETE_DEMAT"]) == "Yes" ? true : false) : false;
                            lstDematAccountDetail.Add(objDematAccountDetail);
                        }
                        objUser.lstDematAccount = lstDematAccountDetail;
                    }
                    rdr1.Close();

                    SqlParameter[] parameters2 = new SqlParameter[3];
                    parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters2[0].Direction = ParameterDirection.Output;
                    parameters2[1] = new SqlParameter("@MODE", "GET_INITIAL_HOLDING_DETAILS_BY_D_ID");
                    parameters2[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters2);
                    if (rdr2.HasRows)
                    {
                        List<InitialHoldingDetail> lstInitialHoldingDetail = new List<InitialHoldingDetail>();
                        while (rdr2.Read())
                        {
                            InitialHoldingDetail initialHoldingDetail = new InitialHoldingDetail();
                            initialHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["HOLDING_ID"])) ? Convert.ToInt32(rdr2["HOLDING_ID"]) : 0;
                            initialHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr2["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr2["COMPANY_NAME"])) ? Convert.ToString(rdr2["COMPANY_NAME"]) : String.Empty
                            };
                            initialHoldingDetail.restrictedCompany.companyName = initialHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : initialHoldingDetail.restrictedCompany.companyName;
                            initialHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE"])) ? Convert.ToString(rdr2["SECURITY_TYPE"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr2["SECURITY_TYPE_NAME"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = initialHoldingDetail.securityType == "0" ? "Not Applicable" : initialHoldingDetail.securityTypeName;
                            initialHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_ID"])) ? Convert.ToInt32(rdr2["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty
                            };
                            initialHoldingDetail.relative.relativeName = initialHoldingDetail.relative.ID == -1 ? "Not Applicable" : initialHoldingDetail.relative.relativeName;
                            initialHoldingDetail.dematAccount = new DematAccount
                            {
                                //  ID = Convert.ToInt32(rdr2["ACCOUNT_ID"]),
                                accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"]) : String.Empty,
                                tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr2["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr2["TRADING_MEMBER_ID"]) : String.Empty
                            };

                            initialHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr2["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr2["NO_OF_SECURITIES"]) : 0;
                            initialHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
                            initialHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            initialHoldingDetail.isDeleteInitialHolding = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;

                            lstInitialHoldingDetail.Add(initialHoldingDetail);
                        }
                        objUser.lstInitialHoldingDetail = lstInitialHoldingDetail;
                    }
                    rdr2.Close();

                    SqlParameter[] parameters3 = new SqlParameter[4];
                    parameters3[0] = new SqlParameter("@LoginId", objUser.LOGIN_ID);
                    parameters3[1] = new SqlParameter("@MODE", "Declarartion Forms");
                    parameters3[2] = new SqlParameter("@CompanyId", objUser.companyId);
                    parameters3[3] = new SqlParameter("@AdminDb", objUser.ADMIN_DATABASE);

                    SqlDataReader rdr3 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_GET_FORMS_LISTING", objUser.MODULE_DATABASE, parameters3);
                    if (rdr3.HasRows)
                    {
                        List<FinalDeclaration> lstFinalDeclaration = new List<FinalDeclaration>();
                        while (rdr3.Read())
                        {
                            FinalDeclaration finalDeclaration = new FinalDeclaration();
                            finalDeclaration.createdOn = !String.IsNullOrEmpty(Convert.ToString(rdr3["CO"])) ? Convert.ToString(rdr3["CO"]) : String.Empty;
                            finalDeclaration.createdBy = !String.IsNullOrEmpty(Convert.ToString(rdr3["USER_NM"])) ? Convert.ToString(rdr3["USER_NM"]) : String.Empty;
                            finalDeclaration.fileName = !String.IsNullOrEmpty(Convert.ToString(rdr3["POLICY_DOC"])) ? Convert.ToString(rdr3["POLICY_DOC"]) : String.Empty;
                            finalDeclaration.fileFormB = !String.IsNullOrEmpty(Convert.ToString(rdr3["FORM_NAME"])) ? Convert.ToString(rdr3["FORM_NAME"]) : String.Empty;
                            finalDeclaration.fileFormEOrF = !String.IsNullOrEmpty(Convert.ToString(rdr3["FILE_NAME"])) ? Convert.ToString(rdr3["FILE_NAME"]) : String.Empty;
                            finalDeclaration.PolicyVersion = !String.IsNullOrEmpty(Convert.ToString(rdr3["VERSION"])) ? Convert.ToString(rdr3["VERSION"]) : String.Empty;
                            //finalDeclaration.fileFormK = !String.IsNullOrEmpty(Convert.ToString(rdr3["ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"])) ? Convert.ToString(rdr3["ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"]) : String.Empty;
                            //finalDeclaration.version = !String.IsNullOrEmpty(Convert.ToString(rdr3["VERSION_ID"])) ? Convert.ToInt32(rdr3["VERSION_ID"]) : 0;
                            lstFinalDeclaration.Add(finalDeclaration);
                        }
                        objUser.lstFinalDeclaration = lstFinalDeclaration;
                    }
                    rdr3.Close();

                    SqlParameter[] parameters4 = new SqlParameter[3];
                    parameters4[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters4[0].Direction = ParameterDirection.Output;
                    parameters4[1] = new SqlParameter("@MODE", "GET_PHYSICAL_HOLDING_DETAILS_BY_D_ID");
                    parameters4[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr4 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters4);
                    if (rdr4.HasRows)
                    {
                        List<PhysicalHoldingDetail> lstPhysicalHoldingDetail = new List<PhysicalHoldingDetail>();
                        while (rdr4.Read())
                        {
                            PhysicalHoldingDetail physicalHoldingDetail = new PhysicalHoldingDetail();
                            physicalHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["HOLDING_ID"])) ? Convert.ToInt32(rdr4["HOLDING_ID"]) : 0;
                            physicalHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr4["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr4["COMPANY_NAME"])) ? Convert.ToString(rdr4["COMPANY_NAME"]) : String.Empty
                            };
                            physicalHoldingDetail.restrictedCompany.companyName = physicalHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : physicalHoldingDetail.restrictedCompany.companyName;

                            physicalHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_ID"])) ? Convert.ToInt32(rdr4["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_NAME"])) ? Convert.ToString(rdr4["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr4["PAN"])) ? Convert.ToString(rdr4["PAN"]) : String.Empty
                            };
                            physicalHoldingDetail.relative.relativeName = physicalHoldingDetail.relative.ID == -1 ? "Not Applicable" : physicalHoldingDetail.relative.relativeName;
                            physicalHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr4["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr4["NO_OF_SECURITIES"]) : 0;
                            physicalHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr4["CREATED_ON"])) ? Convert.ToString(rdr4["CREATED_ON"]) : String.Empty;
                            physicalHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            physicalHoldingDetail.isDeletePhysicalHolding = !String.IsNullOrEmpty(Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;

                            lstPhysicalHoldingDetail.Add(physicalHoldingDetail);
                        }
                        objUser.lstPhysicalHoldingDetail = lstPhysicalHoldingDetail;
                    }
                    rdr4.Close();

                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";

                return objUserResponse;
            }
        }
        #region "Get Transactional Information By Version"
        public UserResponse GetTransactionalInfoByVersion(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();
                if (declarationObject > 0)
                {
                    parameters[1] = new SqlParameter("@MODE", "GET_PERSONAL_DETAILS_BY_D_ID_VERSION");
                    parameters[2] = new SqlParameter("@D_ID", declarationObject);
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<User> lstObjUser = new List<User>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            User obj = new User();
                            obj.residentType = !String.IsNullOrEmpty(Convert.ToString(dr["RESIDENT_TYPE"])) ? Convert.ToString(dr["RESIDENT_TYPE"]) : String.Empty;
                            obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(dr["PAN"])) ? Convert.ToString(dr["PAN"]) : String.Empty;
                            obj.identificationType = !String.IsNullOrEmpty(Convert.ToString(dr["IDENTIFICATION_TYPE"])) ? Convert.ToString(dr["IDENTIFICATION_TYPE"]) : String.Empty;
                            obj.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(dr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(dr["IDENTIFICATION_NUMBER"]) : String.Empty;
                            obj.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(dr["MOBILE_NO"])) ? Convert.ToString(dr["MOBILE_NO"]) : String.Empty;
                            obj.address = !String.IsNullOrEmpty(Convert.ToString(dr["USER_ADDRESS"])) ? Convert.ToString(dr["USER_ADDRESS"]) : String.Empty;
                            obj.pinCode = !String.IsNullOrEmpty(Convert.ToString(dr["PIN_CODE"])) ? Convert.ToString(dr["PIN_CODE"]) : String.Empty;
                            obj.country = !String.IsNullOrEmpty(Convert.ToString(dr["COUNTRY"])) ? Convert.ToString(dr["COUNTRY"]) : String.Empty;
                            obj.Ssn = !String.IsNullOrEmpty(Convert.ToString(dr["SSN"])) ? Convert.ToString(dr["SSN"]) : String.Empty;
                            obj.employeeId = !String.IsNullOrEmpty(Convert.ToString(dr["EMPLOYEE_ID"])) ? Convert.ToString(dr["EMPLOYEE_ID"]) : String.Empty;
                            obj.joiningDate = !String.IsNullOrEmpty(Convert.ToString(dr["JOINING_DATE"])) ? Convert.ToString(dr["JOINING_DATE"]) : String.Empty;
                            obj.becomingInsiderDate = !String.IsNullOrEmpty(Convert.ToString(dr["DATE_BECOMING_INSIDER"])) ? Convert.ToString(dr["DATE_BECOMING_INSIDER"]) : String.Empty;
                            obj.becomingInsiderDate = obj.becomingInsiderDate == "1/1/1900 12:00:00 AM" ? String.Empty : obj.becomingInsiderDate;
                            obj.department = new Department
                            {
                                DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(dr["DEPARTMENT_ID"])) ? Convert.ToInt32(dr["DEPARTMENT_ID"]) : 0,
                                DEPARTMENT_NM = !String.IsNullOrEmpty(Convert.ToString(dr["DEPARTMENT_NAME"])) ? Convert.ToString(dr["DEPARTMENT_NAME"]) : String.Empty
                            };
                            obj.location = new Location
                            {
                                locationId = !String.IsNullOrEmpty(Convert.ToString(dr["LOCATION"])) ? Convert.ToInt32(dr["LOCATION"]) : 0,
                                locationName = !String.IsNullOrEmpty(Convert.ToString(dr["LOCATION_NAME"])) ? Convert.ToString(dr["LOCATION_NAME"]) : String.Empty
                            };
                            obj.designation = new Designation
                            {
                                DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION_ID"])) ? Convert.ToInt32(dr["DESIGNATION_ID"]) : 0,
                                DESIGNATION_NM = !String.IsNullOrEmpty(Convert.ToString(dr["DESIGNATION_NAME"])) ? Convert.ToString(dr["DESIGNATION_NAME"]) : String.Empty
                            };
                            obj.category = new Category
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(dr["CATEGORY_ID"])) ? Convert.ToInt32(dr["CATEGORY_ID"]) : 0,
                                categoryName = !String.IsNullOrEmpty(Convert.ToString(dr["CATEGORY_NAME"])) ? Convert.ToString(dr["CATEGORY_NAME"]) : String.Empty,
                                subCategory = new SubCategory
                                {
                                    ID = !String.IsNullOrEmpty(Convert.ToString(dr["SUBCATEGORY_ID"])) ? Convert.ToInt32(dr["SUBCATEGORY_ID"]) : 0,
                                    subCategoryName = !String.IsNullOrEmpty(Convert.ToString(dr["SUBCATEGORY_NAME"])) ? Convert.ToString(dr["SUBCATEGORY_NAME"]) : String.Empty
                                }
                            };
                            obj.dinNumber = !String.IsNullOrEmpty(Convert.ToString(dr["DIN_NUMBER"])) ? Convert.ToString(dr["DIN_NUMBER"]) : String.Empty;
                            obj.institutionName = !String.IsNullOrEmpty(Convert.ToString(dr["INSTITUTE"])) ? Convert.ToString(dr["INSTITUTE"]) : String.Empty;
                            obj.stream = !String.IsNullOrEmpty(Convert.ToString(dr["STREAM"])) ? Convert.ToString(dr["STREAM"]) : String.Empty;
                            obj.employerDetails = !String.IsNullOrEmpty(Convert.ToString(dr["EMPLOYER_DETAILS"])) ? Convert.ToString(dr["EMPLOYER_DETAILS"]) : String.Empty;
                            obj.status = !String.IsNullOrEmpty(Convert.ToString(dr["STATUS"])) ? Convert.ToString(dr["STATUS"]) : String.Empty;
                            obj.D_ID = !String.IsNullOrEmpty(Convert.ToString(dr["D_ID"])) ? Convert.ToInt32(dr["D_ID"]) : 0;
                            obj.companyId = objUser.companyId;
                            obj.MODULE_DATABASE = objUser.MODULE_DATABASE;
                            obj.moduleId = objUser.moduleId;
                            obj.LOGIN_ID = objUser.LOGIN_ID;
                            obj.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                            obj.version = !String.IsNullOrEmpty(Convert.ToString(dr["VERSION_ID"])) ? Convert.ToInt32(dr["VERSION_ID"]) : 0;
                            lstObjUser.Add(obj);
                            // objUser = obj;
                        }
                        objUserResponse.UserList = lstObjUser;

                    }

                    parameters[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID_VERSION");
                    SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (rdr.HasRows)
                    {
                        List<Relative> lstRelativeDetail = new List<Relative>();
                        while (rdr.Read())
                        {
                            Relative objRelative = new Relative();
                            objRelative.ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_ID"])) ? Convert.ToInt32(rdr["RELATIVE_ID"]) : 0;
                            objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
                            objRelative.relation = new Relation
                            {
                                RELATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_ID"])) ? Convert.ToInt32(rdr["RELATION_ID"]) : 0,
                                RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_NAME"])) ? Convert.ToString(rdr["RELATION_NAME"]) : String.Empty
                            };
                            objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
                            objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_EMAIL"])) ? Convert.ToString(rdr["RELATIVE_EMAIL"]) : String.Empty;
                            objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                            objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
                            objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
                            objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr["MOBILE"])) ? Convert.ToString(rdr["MOBILE"]) : String.Empty;
                            objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"])) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                            objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr["PIN_CODE"])) ? Convert.ToString(rdr["PIN_CODE"]) : String.Empty;
                            objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr["COUNTRY"])) ? Convert.ToString(rdr["COUNTRY"]) : String.Empty;
                            objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"])) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                            objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"])) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
                            objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                            objRelative.version = !String.IsNullOrEmpty(Convert.ToString(rdr["VERSION_ID"])) ? Convert.ToInt32(rdr["VERSION_ID"]) : 0;
                            lstRelativeDetail.Add(objRelative);
                        }
                        objUser.lstRelative = lstRelativeDetail;
                    }
                    rdr.Close();

                    SqlParameter[] parameters1 = new SqlParameter[3];
                    parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[0].Direction = ParameterDirection.Output;
                    parameters1[1] = new SqlParameter("@MODE", "GET_DEMAT_ACCOUNT_DETAILS_BY_D_ID_VERSION");
                    parameters1[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objUser.MODULE_DATABASE, parameters1);
                    if (rdr1.HasRows)
                    {
                        List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
                        while (rdr1.Read())
                        {
                            DematAccount objDematAccountDetail = new DematAccount();
                            objDematAccountDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_ID"])) ? Convert.ToInt32(rdr1["ACCOUNT_ID"]) : 0;
                            objDematAccountDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_ID"])) ? Convert.ToInt32(rdr1["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : "Self"
                            };
                            objDematAccountDetail.relative.relativeName = objDematAccountDetail.relative.ID == -1 ? "Not Applicable" : objDematAccountDetail.relative.relativeName;
                            objDematAccountDetail.depositoryName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_NAME"]) : String.Empty;
                            objDematAccountDetail.clientId = !String.IsNullOrEmpty(Convert.ToString(rdr1["CLIENT_ID"])) ? Convert.ToString(rdr1["CLIENT_ID"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantId = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"]) : String.Empty;
                            objDematAccountDetail.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr1["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr1["TRADING_MEMBER_ID"]) : String.Empty;
                            objDematAccountDetail.dematType = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEMAT_TYPE"])) ? Convert.ToString(rdr1["DEMAT_TYPE"]) : String.Empty;
                            objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
                            objDematAccountDetail.status = !String.IsNullOrEmpty(Convert.ToString(rdr1["STATUS"])) ? Convert.ToString(rdr1["STATUS"]) : String.Empty;
                            objDematAccountDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr1["CREATED_ON"])) ? Convert.ToString(rdr1["CREATED_ON"]) : String.Empty;
                            objDematAccountDetail.version = !String.IsNullOrEmpty(Convert.ToString(rdr1["VERSION_ID"])) ? Convert.ToInt32(rdr1["VERSION_ID"]) : 0;
                            lstDematAccountDetail.Add(objDematAccountDetail);
                        }
                        objUser.lstDematAccount = lstDematAccountDetail;
                    }
                    rdr1.Close();

                    SqlParameter[] parameters2 = new SqlParameter[3];
                    parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters2[0].Direction = ParameterDirection.Output;
                    parameters2[1] = new SqlParameter("@MODE", "GET_INITIAL_HOLDING_DETAILS_BY_D_ID_VERSION");
                    parameters2[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters2);
                    if (rdr2.HasRows)
                    {
                        List<InitialHoldingDetail> lstInitialHoldingDetail = new List<InitialHoldingDetail>();
                        while (rdr2.Read())
                        {
                            InitialHoldingDetail initialHoldingDetail = new InitialHoldingDetail();
                            initialHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["HOLDING_ID"])) ? Convert.ToInt32(rdr2["HOLDING_ID"]) : 0;
                            initialHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr2["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr2["COMPANY_NAME"])) ? Convert.ToString(rdr2["COMPANY_NAME"]) : String.Empty
                            };
                            initialHoldingDetail.restrictedCompany.companyName = initialHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : initialHoldingDetail.restrictedCompany.companyName;
                            initialHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE"])) ? Convert.ToString(rdr2["SECURITY_TYPE"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr2["SECURITY_TYPE_NAME"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = initialHoldingDetail.securityType == "0" ? "Not Applicable" : initialHoldingDetail.securityTypeName;
                            initialHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_ID"])) ? Convert.ToInt32(rdr2["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty
                            };
                            initialHoldingDetail.relative.relativeName = initialHoldingDetail.relative.ID == -1 ? "Not Applicable" : initialHoldingDetail.relative.relativeName;
                            initialHoldingDetail.dematAccount = new DematAccount
                            {
                                //  ID = Convert.ToInt32(rdr2["ACCOUNT_ID"]),
                                accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"]) : String.Empty,
                                tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr2["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr2["TRADING_MEMBER_ID"]) : String.Empty
                            };

                            initialHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr2["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr2["NO_OF_SECURITIES"]) : 0;
                            initialHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
                            initialHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(rdr2["VERSION_ID"])) ? Convert.ToInt32(rdr2["VERSION_ID"]) : 0;

                            lstInitialHoldingDetail.Add(initialHoldingDetail);
                        }
                        objUser.lstInitialHoldingDetail = lstInitialHoldingDetail;
                    }
                    rdr2.Close();

                    SqlParameter[] parameters3 = new SqlParameter[3];
                    parameters3[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters3[0].Direction = ParameterDirection.Output;
                    parameters3[1] = new SqlParameter("@MODE", "GET_FINAL_DECLARATION_DETAILS_BY_D_ID");
                    parameters3[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr3 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters3);
                    if (rdr3.HasRows)
                    {
                        List<FinalDeclaration> lstFinalDeclaration = new List<FinalDeclaration>();
                        while (rdr3.Read())
                        {
                            FinalDeclaration finalDeclaration = new FinalDeclaration();
                            finalDeclaration.createdOn = !String.IsNullOrEmpty(Convert.ToString(rdr3["CREATED_ON"])) ? Convert.ToString(rdr3["CREATED_ON"]) : String.Empty;
                            finalDeclaration.createdBy = !String.IsNullOrEmpty(Convert.ToString(rdr3["CREATED_BY"])) ? Convert.ToString(rdr3["CREATED_BY"]) : String.Empty;
                            finalDeclaration.fileName = !String.IsNullOrEmpty(Convert.ToString(rdr3["POLICY_DOCUMENT"])) ? Convert.ToString(rdr3["POLICY_DOCUMENT"]) : String.Empty;
                            finalDeclaration.version = !String.IsNullOrEmpty(Convert.ToString(rdr3["VERSION_ID"])) ? Convert.ToInt32(rdr3["VERSION_ID"]) : 0;
                            lstFinalDeclaration.Add(finalDeclaration);
                        }
                        objUser.lstFinalDeclaration = lstFinalDeclaration;
                    }
                    rdr3.Close();

                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }



                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";

                return objUserResponse;
            }

        }
        #endregion
        #region "Save Insider Trading Window"
        public InsiderTradingWindowResponse SaveInsiderTradingWindow(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@MODE", "ADD_INSIDER_TRADING_WINDOW");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@FROM_DATE", ConvertDate(objInsiderTradingWindow.fromDate));
                parameters[4] = new SqlParameter("@TO_DATE", !String.IsNullOrEmpty(objInsiderTradingWindow.toDate) ? ConvertDate(objInsiderTradingWindow.toDate) : ConvertDate("31/12/9999"));
                parameters[5] = new SqlParameter("@REMARKS", objInsiderTradingWindow.remarks);
                parameters[6] = new SqlParameter("@CREATED_BY", objInsiderTradingWindow.createdBy);
                parameters[7] = new SqlParameter("@BOARD_MEETING_SCHEDULED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.boardMeetingScheduledOn) ? ConvertDate(objInsiderTradingWindow.boardMeetingScheduledOn) : ConvertDate("31/12/9999"));
                parameters[8] = new SqlParameter("@QUARTER_ENDED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.quarterEndedOn) ? ConvertDate(objInsiderTradingWindow.quarterEndedOn) : ConvertDate("31/12/9999"));
                parameters[9] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objInsiderTradingWindow.windowClosureTypeId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                //if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Email sent successfully !";
                //}
                //else
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Data has been fetched successfully !";
                //}
                oInsiderTradingWindow.StatusFl = true;
                oInsiderTradingWindow.Msg = "Data has been added successfully !";
                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Update Insider Trading Window"
        public InsiderTradingWindowResponse UpdateInsiderTradingWindow(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[11];
                parameters[0] = new SqlParameter("@MODE", "UPDATE_INSIDER_TRADING_WINDOW");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@FROM_DATE", ConvertDate(objInsiderTradingWindow.fromDate));
                parameters[4] = new SqlParameter("@TO_DATE", !String.IsNullOrEmpty(objInsiderTradingWindow.toDate) ? ConvertDate(objInsiderTradingWindow.toDate) : ConvertDate("31/12/9999"));
                parameters[5] = new SqlParameter("@REMARKS", objInsiderTradingWindow.remarks);
                parameters[6] = new SqlParameter("@CREATED_BY", objInsiderTradingWindow.createdBy);
                parameters[7] = new SqlParameter("@ID", objInsiderTradingWindow.id);
                parameters[8] = new SqlParameter("@BOARD_MEETING_SCHEDULED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.boardMeetingScheduledOn) ? ConvertDate(objInsiderTradingWindow.boardMeetingScheduledOn) : ConvertDate("31/12/9999"));
                parameters[9] = new SqlParameter("@QUARTER_ENDED_ON", !String.IsNullOrEmpty(objInsiderTradingWindow.quarterEndedOn) ? ConvertDate(objInsiderTradingWindow.quarterEndedOn) : ConvertDate("31/12/9999"));
                parameters[10] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objInsiderTradingWindow.windowClosureTypeId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                //if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Email sent successfully !";
                //}
                //else
                //{
                //    oInsiderTradingWindow.StatusFl = true;
                //    oInsiderTradingWindow.Msg = "Data has been updated successfully !";
                //}
                oInsiderTradingWindow.StatusFl = true;
                oInsiderTradingWindow.Msg = "Data has been updated successfully !";
                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Get Insider Trading Window Closure Info"
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfo(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_IT_WINDOW_CLOSURE_INFO");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        InsiderTradingWindow obj = new InsiderTradingWindow();
                        obj.fromDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"]) : String.Empty;
                        obj.toDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"]) : String.Empty;
                        obj.remarks = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]) : String.Empty;
                        oInsiderTradingWindow.InsiderTradingWindow = obj;
                        oInsiderTradingWindow.StatusFl = true;
                        oInsiderTradingWindow.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oInsiderTradingWindow.StatusFl = false;
                        oInsiderTradingWindow.Msg = "No data found !";
                    }
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "No data found !";
                }

                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Get Insider Trading Window Closure Info List"
        public InsiderTradingWindowResponse GetInsiderTradingWindowClosureInfoList(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_IT_WINDOW_CLOSURE_INFO_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            InsiderTradingWindow obj = new InsiderTradingWindow();
                            obj.id = Convert.ToInt32(dr["ID"]);
                            obj.windowClosureTypeId = !String.IsNullOrEmpty(Convert.ToString(dr["TRADING_WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToInt32(dr["TRADING_WINDOW_CLOSURE_TYPE_ID"]) : 0;
                            obj.fromDate = !String.IsNullOrEmpty(Convert.ToString(dr["FROM_DATE"])) ? Convert.ToString(dr["FROM_DATE"]) : String.Empty;
                            obj.toDate = !String.IsNullOrEmpty(Convert.ToString(dr["TO_DATE"])) ? Convert.ToString(dr["TO_DATE"]) : String.Empty;
                            obj.remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty;
                            obj.boardMeetingScheduledOn = !String.IsNullOrEmpty(Convert.ToString(dr["BOARD_MEETING_SCHEDULED_ON"])) ? Convert.ToString(dr["BOARD_MEETING_SCHEDULED_ON"]) : String.Empty;
                            obj.quarterEndedOn = !String.IsNullOrEmpty(Convert.ToString(dr["QUARTER_ENDED_ON"])) ? Convert.ToString(dr["QUARTER_ENDED_ON"]) : String.Empty;
                            oInsiderTradingWindow.AddObject(obj);
                        }
                        oInsiderTradingWindow.StatusFl = true;
                        oInsiderTradingWindow.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oInsiderTradingWindow.StatusFl = false;
                        oInsiderTradingWindow.Msg = "No data found !";
                    }
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "No data found !";
                }

                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Send Email To All Insider For Window Closure"
        public Boolean SendEmailToAllInsiderForWindowClosure(InsiderTradingWindow objInsiderTradingWindow)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);

            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objInsiderTradingWindow.MODULE_DATABASE, parameters);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                SqlParameter[] parameters1 = new SqlParameter[4];
                parameters1[0] = new SqlParameter("@MODE", "GET_ALL_INSIDERS");
                parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[1].Direction = ParameterDirection.Output;
                parameters1[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters1[3] = new SqlParameter("@ADMIN_DB", Convert.ToString(HttpContext.Current.Session["AdminDB"]));
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters1);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                            string userEmail = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                            string subject = "Insider Trading Window Closure";
                            string body = String.Empty;
                            //body += "Dear " + userName + ",<br/><br/>";
                            //body += "Please Note: The Trading Window is closed from  " + objInsiderTradingWindow.fromDate + " to " + objInsiderTradingWindow.toDate + ". " + objInsiderTradingWindow.remarks + "<br/><br/>";
                            //body += "Thanks & Regards,<br/>";
                            //body += "ProCS Technology";
                            SqlParameter[] parameters2 = new SqlParameter[9];
                            parameters2[1] = new SqlParameter("@Mode", "GET_LAYOUT_TEMPLATE_AND_SUBJECT_HEADER");
                            parameters2[2] = new SqlParameter("@CompanyId", objInsiderTradingWindow.companyId);
                            parameters2[3] = new SqlParameter("@SetCount", SqlDbType.Int);
                            parameters2[3].Direction = ParameterDirection.Output;
                            parameters2[4] = new SqlParameter("@QuarterEndedOn", objInsiderTradingWindow.quarterEndedOn);
                            parameters2[5] = new SqlParameter("@ToDate", objInsiderTradingWindow.toDate);
                            parameters2[6] = new SqlParameter("@FromDate", objInsiderTradingWindow.fromDate);
                            parameters2[7] = new SqlParameter("@BoardMeetingScheduledOn", objInsiderTradingWindow.boardMeetingScheduledOn);
                            parameters2[8] = new SqlParameter("@WINDOW_CLOSURE_TYPE_ID", objInsiderTradingWindow.windowClosureTypeId);

                            DataSet ds2 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_IT_INSIDER_TRADING_WINDOW_CLOSURE_EMAIL_TEMPLATE", objInsiderTradingWindow.MODULE_DATABASE, parameters2);
                            if (ds2.Tables.Count > 0)
                            {
                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    body += !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["LAYOUT_TEMPLATE"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["LAYOUT_TEMPLATE"]) : String.Empty;
                                    subject = !String.IsNullOrEmpty(Convert.ToString(ds2.Tables[0].Rows[0]["SUBJECT"])) ? Convert.ToString(ds2.Tables[0].Rows[0]["SUBJECT"]) : String.Empty;
                                }
                            }
                            EmailSender.SendMail(
                                userEmail, subject, body, null, "Window Closure Notification", objInsiderTradingWindow.companyId.ToString(),
                                "", objInsiderTradingWindow.windowClosureTypeId.ToString(), Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                            );
                            //EmailHelper.SendEmailForNonCompliant(
                            //    defaultEmail, userEmail, subject, body, smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port
                            //);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        #endregion
        #region "Add Task For Periodic Declaration"
        public PeriodicDeclarationResponse AddTaskForPeriodicDeclaration(PeriodicDeclaration objPeriodicDeclaration)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@MODE", "ADD_TASK_FOR_PERIODIC_DECLARATION");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPeriodicDeclaration.companyId);
                parameters[3] = new SqlParameter("@DECLARATION_DATE", ConvertDate(objPeriodicDeclaration.declarationDate));
                parameters[4] = new SqlParameter("@VALID_TILL_IN_DAYS", objPeriodicDeclaration.validTillInDays);
                parameters[5] = new SqlParameter("@FREQUENCY_IN_MONTHS", objPeriodicDeclaration.frequencyInMonths);
                parameters[6] = new SqlParameter("@CREATED_BY", objPeriodicDeclaration.createdBy);
                PeriodicDeclarationResponse oPeriodicDeclarationRes = new PeriodicDeclarationResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DECLARATION_SETTINGS", objPeriodicDeclaration.MODULE_DATABASE, parameters);

                string enableBulkMailForPeriodicDeclarationTask = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["EnableBulkMailForPeriodicDeclarationTask"]), true);

                if (Convert.ToBoolean(enableBulkMailForPeriodicDeclarationTask))
                {
                    if (SendEmailToAllInsiderForPeriodicDeclaration(objPeriodicDeclaration))
                    {
                        oPeriodicDeclarationRes.StatusFl = true;
                        oPeriodicDeclarationRes.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oPeriodicDeclarationRes.StatusFl = true;
                        oPeriodicDeclarationRes.Msg = "Data has been fetched successfully !";
                    }
                }
                else
                {
                    oPeriodicDeclarationRes.StatusFl = true;
                    oPeriodicDeclarationRes.Msg = "Data has been fetched successfully !";
                }


                return oPeriodicDeclarationRes;
            }
            catch (Exception ex)
            {
                PeriodicDeclarationResponse oPeriodicDeclarationRes = new PeriodicDeclarationResponse();
                oPeriodicDeclarationRes.StatusFl = false;
                oPeriodicDeclarationRes.Msg = "Processing failed, because of system error !";
                return oPeriodicDeclarationRes;
            }
        }
        #endregion
        #region "Send Email To All Insider For Periodic Declaration"
        public Boolean SendEmailToAllInsiderForPeriodicDeclaration(PeriodicDeclaration objPeriodicDeclaration)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@COMPANY_ID", objPeriodicDeclaration.companyId);

            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objPeriodicDeclaration.MODULE_DATABASE, parameters);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                SqlParameter[] parameters1 = new SqlParameter[4];
                parameters1[0] = new SqlParameter("@MODE", "GET_ALL_INSIDERS");
                parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[1].Direction = ParameterDirection.Output;
                parameters1[2] = new SqlParameter("@COMPANY_ID", objPeriodicDeclaration.companyId);
                parameters1[3] = new SqlParameter("@ADMIN_DB", Convert.ToString(HttpContext.Current.Session["AdminDB"]));
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objPeriodicDeclaration.MODULE_DATABASE, parameters1);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string userName = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                            string userEmail = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                            string userId = !String.IsNullOrEmpty(Convert.ToString(dr["LOGIN_ID"])) ? Convert.ToString(dr["LOGIN_ID"]) : String.Empty;
                            //string userPassword = !String.IsNullOrEmpty(Convert.ToString(dr["USER_PWD"])) ? CryptorEngine.Decrypt(Convert.ToString(dr["USER_PWD"]), true) : String.Empty;
                            string subject = "Declaration for Insider Trading Compliance";
                            string body = String.Empty;
                            string tillDate = String.Format("{0:dd/MM/yyyy}", ConvertDate(objPeriodicDeclaration.declarationDate).AddDays(objPeriodicDeclaration.validTillInDays));

                            if (Convert.ToString(HttpContext.Current.Session["CompanyName"]) == "IEX")
                            {
                                body += "Dear Sir/Madam,<br/><br/>";
                                int month = DateTime.Now.Month;
                                int year = DateTime.Now.Year;

                                if (month == 1 || month == 2 || month == 3)
                                {
                                    year = year - 1;
                                }

                                body += "The Periodic Declaration for Insider Trading Compliance is required for the period 1-Apr-" + Convert.ToString(year) + " to 30-Sep-" + Convert.ToString(year) + ".  You are requested to kindly use the below link to submit your declarations for this period on or before - " + tillDate + ".<br/><br/>";

                                body += "Please click on below link to access the portal<br/>";
                                body += "<b><a href='https://pit.iexindia.com' target='_blank'>https://pit.iexindia.com</a></b><br/>";

                                body += "<b>For All IEX-Users,</b> User ID & Password will be your system/laptop password (no required to remember separate login and password details, You need to enter one time your default user id (i.e. employee id for example anujj00088) and enter current system/laptop password.<br/><br/>";
                                body += "<b>For All other Users,</b> <b>User Id:</b> " + userId + "<br/>";
                                //body += "<b>*Password:</b> " + userPassword + "<br/>";
                                //body += "<i>*Default system password, you may please change password after login.</i><br/><br/>";

                                body += "Regards,<br/>";
                                body += "Compliance Department<br/>";
                                body += "Indian Energy Exchange Limited";
                            }
                            else
                            {
                                if (Convert.ToString(HttpContext.Current.Session["CompanyName"]) == "Spark Minda")
                                {
                                    body += "Dear Sir/Madam,<br/><br/>";
                                }
                                else
                                {
                                    body += "Dear " + userName + ",<br/><br/>";
                                }

                                body += "Please Note: The Periodic Declaration for Insider Trading Compliance is required from " + objPeriodicDeclaration.declarationDate + " till " + tillDate + ".  You are requested to kindly submit your declarations for this period on or before - " + tillDate + ".<br/><br/>";
                                string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
                                body += "<p>For submitting the declaration, please logon to the below link</p><p>" + baseUrl + "Login.aspx</p><br/>";

                                if (Convert.ToString(HttpContext.Current.Session["CompanyName"]) == "Spark Minda")
                                {
                                    body += "<b>Best Regards,</b><br/>";
                                    body += "<b>For Minda Corporation Limited</b><br/>";
                                    body += "<b>Secretarial Team</b>";
                                }
                                else
                                {
                                    body += "<b>Best Regards,</b><br/>";
                                    body += "<b>Secretarial Team</b>";
                                }

                            }

                            EmailSender.SendMail(
                                userEmail, subject, body, null, "Periodic Declaration Reminder", objPeriodicDeclaration.companyId.ToString(),
                                "", userEmail, Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                            );
                            //EmailHelper.SendEmailForNonCompliant(
                            //    defaultEmail, userEmail, subject, body, smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port
                            //);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        #endregion
        #region "Get Periodic Declaration Request"
        public PeriodicDeclarationResponse GetPeriodicDeclaration(PeriodicDeclaration objPeriodicDeclaration)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@MODE", "GET_PERIODIC_DECLARATION");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objPeriodicDeclaration.companyId);
                PeriodicDeclarationResponse oPeriodicDeclarationRes = new PeriodicDeclarationResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_DECLARATION_SETTINGS", objPeriodicDeclaration.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        PeriodicDeclaration obj = new PeriodicDeclaration();
                        obj.id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                        obj.declarationDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DECLARATION_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DECLARATION_DATE"]) : String.Empty;
                        obj.validTillInDays = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VALID_TILL_IN_DAYS"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VALID_TILL_IN_DAYS"]) : 0;
                        obj.frequencyInMonths = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FREQUENCY_IN_MONTHS"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["FREQUENCY_IN_MONTHS"]) : 0;
                        oPeriodicDeclarationRes.PeriodicDeclaration = obj;
                        oPeriodicDeclarationRes.StatusFl = true;
                        oPeriodicDeclarationRes.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        oPeriodicDeclarationRes.StatusFl = false;
                        oPeriodicDeclarationRes.Msg = "No data found !";
                    }
                }
                else
                {
                    oPeriodicDeclarationRes.StatusFl = false;
                    oPeriodicDeclarationRes.Msg = "No data found !";
                }

                return oPeriodicDeclarationRes;
            }
            catch (Exception ex)
            {
                PeriodicDeclarationResponse oPeriodicDeclarationRes = new PeriodicDeclarationResponse();
                oPeriodicDeclarationRes.StatusFl = false;
                oPeriodicDeclarationRes.Msg = "Processing failed, because of system error !";
                return oPeriodicDeclarationRes;
            }
        }
        #endregion
        #region "Send Email For Trading Window Closure"
        public InsiderTradingWindowResponse SendEmailForTradingWindowClosure(InsiderTradingWindow objInsiderTradingWindow)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_TRADING_WINDOW_CLOSURE_INFORMATION_BY_ID");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objInsiderTradingWindow.companyId);
                parameters[3] = new SqlParameter("@ID", objInsiderTradingWindow.id);
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRADING_WINDOW", objInsiderTradingWindow.MODULE_DATABASE, parameters);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objInsiderTradingWindow.windowClosureTypeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRADING_WINDOW_CLOSURE_TYPE_ID"]) : 0;
                        objInsiderTradingWindow.fromDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["FROM_DATE"]) : String.Empty;
                        objInsiderTradingWindow.toDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TO_DATE"]) : String.Empty;
                        objInsiderTradingWindow.boardMeetingScheduledOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["BOARD_MEETING_SCHEDULED_ON"]) : String.Empty;
                        objInsiderTradingWindow.quarterEndedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["QUARTER_ENDED_ON"]) : String.Empty;
                        objInsiderTradingWindow.remarks = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]) : String.Empty;
                    }
                }
                if (SendEmailToAllInsiderForWindowClosure(objInsiderTradingWindow))
                {
                    oInsiderTradingWindow.StatusFl = true;
                    oInsiderTradingWindow.Msg = "Email sent successfully !";
                }
                else
                {
                    oInsiderTradingWindow.StatusFl = false;
                    oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                }

                return oInsiderTradingWindow;
            }
            catch (Exception ex)
            {
                InsiderTradingWindowResponse oInsiderTradingWindow = new InsiderTradingWindowResponse();
                oInsiderTradingWindow.StatusFl = false;
                oInsiderTradingWindow.Msg = "Processing failed, because of system error !";
                return oInsiderTradingWindow;
            }
        }
        #endregion
        #region "Welcome Email To Added Insider User"
        public void SendWelcomeEmailToAddedInsiderUser(User objUser)
        {
            string subject = "Introduction of Insider Compliance Management Software";
            string body = String.Empty;
            List<EventBasedForm> lstAllEmailEvents = null;
            string sMainEvnt = "";
            if (objUser.UserType == "AD/SAML")
            {
                sMainEvnt = "Welcome Email AD/SAML";
            }
            else
            {
                sMainEvnt = "Welcome Email Application";
            }
            lstAllEmailEvents = Form.GetAllEmailEvents(
                objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE,
                objUser.LOGIN_ID, objUser.LOGIN_ID, sMainEvnt, ""
            );
            if (lstAllEmailEvents != null)
            {
                foreach (var obj in lstAllEmailEvents)
                {
                    body = obj.formTemplate.Replace("*/**** This is a System Generated Document hence no Signature is Required ****/*", "");
                }
            }
            List<string> lstAttachments = new List<string>();

            lstAttachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/CodeOfConduct/CodeOfConduct.pdf")));
            lstAttachments.Add(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/UserManual/User-Manual.pdf")));
            EmailSender.SendMail(objUser.USER_EMAIL, subject, body, lstAttachments, "Welcome Email", objUser.companyId.ToString());
        }
        #endregion
        #region "Save Uploaded Form"
        public UserResponse SaveUploadedForm(User objuser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@MODE", "SAVE_UPLOADED_FORM_BY_USER");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objuser.companyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objuser.LOGIN_ID);
                parameters[4] = new SqlParameter("@UPLOAD_AVATAR", objuser.uploadAvatar);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objuser.MODULE_DATABASE, parameters);
                var obj = parameters[1].Value;
                UserResponse ouser = new UserResponse();
                if ((Int32)obj == 0)
                {
                    SendUploadedFormMailToApprover(objuser);
                    ouser.StatusFl = true;
                    ouser.Msg = "Data has been saved successfully !";
                    ouser.User = objuser;

                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "User with same email id already exist!";
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Send Uploaded Form Mail to Approver"
        public void SendUploadedFormMailToApprover(User objUser)
        {
            String approver = String.Empty;
            String subject = String.Empty;
            String layoutTemplate = String.Empty;
            String attachment = String.Empty;

            SqlParameter[] parameters1 = new SqlParameter[4];
            parameters1[0] = new SqlParameter("@COMPANY_ID", objUser.companyId);
            parameters1[1] = new SqlParameter("@MODE", "GET_COMPANY_ASSIGNED_USER");
            parameters1[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters1[2].Direction = ParameterDirection.Output;
            parameters1[3] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                approver = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_EMAIL"]) : String.Empty;
            }

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);

            DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objUser.MODULE_DATABASE, parameters);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                attachment = objUser.uploadAvatar;
                subject = "Uploaded Form";
                layoutTemplate += "Dear,<br/><br/>";
                layoutTemplate += "This is to inform that the form is uploaded by the user in the context of the downloaded form.";
                EmailSender.SendMail(
                    approver, subject, layoutTemplate, null, subject, objUser.companyId.ToString(), "",
                    Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToString(HttpContext.Current.Session["EmployeeId"])
                );
                //EmailHelper.SendUploadedFormMailToApprover(
                //    defaultEmail, disclosureEmail, approver, subject, layoutTemplate, smtpHostName, ssl, smtpUserName, password, 
                //    userDefaultCredential, port, attachment
                //);
            }
        }
        #endregion
        #region "Get User Uploaded Forms"
        public UserResponse GetUserUploadedForms(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter("@MODE", "GET_USER_UPLOADED_FORMS");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                UserResponse objUserRes = new UserResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            User obj = new User();
                            obj.USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty;
                            obj.USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty;
                            obj.uploadAvatar = !String.IsNullOrEmpty(Convert.ToString(dr["FILE_NAME"])) ? Convert.ToString(dr["FILE_NAME"]) : String.Empty;
                            obj.formSubmittedOn = !String.IsNullOrEmpty(Convert.ToString(dr["SUBMITTED_ON"])) ? Convert.ToString(dr["SUBMITTED_ON"]) : String.Empty;
                            objUserRes.AddObject(obj);
                        }
                    }
                }
                objUserRes.StatusFl = true;
                objUserRes.Msg = "Data has been fetched successfully !";
                return objUserRes;
            }
            catch (Exception ex)
            {
                UserResponse objUserRes = new UserResponse();
                objUserRes.StatusFl = false;
                objUserRes.Msg = "Processing failed, because of system error !";
                return objUserRes;
            }
        }
        #endregion
        #region "Get Company Forms"
        public UserResponse GetCompanyForms(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter("@MODE", "GET_COMPANY_FORMS");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                UserResponse objUserRes = new UserResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            User obj = new User();
                            obj.formName = !String.IsNullOrEmpty(Convert.ToString(dr["FORM_NM"])) ? Convert.ToString(dr["FORM_NM"]) : String.Empty;
                            obj.ID = Convert.ToInt32(dr["FORM_ID"]);

                            objUserRes.AddObject(obj);
                        }
                    }
                }
                objUserRes.StatusFl = true;
                objUserRes.Msg = "Data has been fetched successfully !";
                return objUserRes;
            }
            catch (Exception ex)
            {
                UserResponse objUserRes = new UserResponse();
                objUserRes.StatusFl = false;
                objUserRes.Msg = "Processing failed, because of system error !";
                return objUserRes;
            }
        }
        #endregion
        #region "Get Company MIS Report"
        public UserResponse GetMISReport(User objUser)
        {
            try
            {
                string xFile = String.Empty;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableDualServer"]))
                {
                    string enableDualServer = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["EnableDualServer"].ToString(), true);
                    if (enableDualServer.ToUpper() == "FALSE")
                    {
                        SqlParameter[] parameters = new SqlParameter[6];

                        parameters[0] = new SqlParameter("@MODE", "GET_COMPANY_MIS_REPORT");
                        parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters[1].Direction = ParameterDirection.Output;
                        parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                        parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);

                        string sourceDir = HttpContext.Current.Server.MapPath("~/InsiderTrading/Declaration_Document/");
                        string backupDir = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/");
                        xFile = "MIS_Report_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                        string TemplateName = "MIS_Report.xlsx";

                        File.Copy(Path.Combine(sourceDir, TemplateName), Path.Combine(backupDir, xFile + ".xlsx"), true);
                        string xFileName = backupDir + xFile + ".xlsx";

                        parameters[4] = new SqlParameter("@FILE_NAME", xFileName);
                        parameters[5] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);

                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                    }
                    else
                    {
                        SqlParameter[] parameters = new SqlParameter[6];

                        parameters[0] = new SqlParameter("@MODE", "GET_COMPANY_MIS_REPORT_FROM_OTHER_SERVER");
                        parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters[1].Direction = ParameterDirection.Output;
                        parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                        parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);

                        xFile = "MIS_Report_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

                        parameters[4] = new SqlParameter("@FILE_NAME", xFile);

                        string exePath = CryptorEngine.Decrypt(ConfigurationManager.AppSettings["MISExePath"].ToString(), true);

                        parameters[5] = new SqlParameter("@ExePath", exePath);

                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                    }
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[5];

                    parameters[0] = new SqlParameter("@MODE", "GET_COMPANY_MIS_REPORT");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);

                    string sourceDir = HttpContext.Current.Server.MapPath("~/InsiderTrading/Declaration_Document/");
                    string backupDir = HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/");
                    xFile = "MIS_Report_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                    string TemplateName = "MIS_Report.xlsx";

                    File.Copy(Path.Combine(sourceDir, TemplateName), Path.Combine(backupDir, xFile + ".xlsx"), true);
                    string xFileName = backupDir + xFile + ".xlsx";

                    parameters[4] = new SqlParameter("@FILE_NAME", xFileName);

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                }


                UserResponse objUserRes = new UserResponse();
                User obj = new User();
                //if (ds.Tables.Count > 0)
                //{
                //    //Create Excel File
                //    //string fname = "MIS_Report_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture) + ".xls";
                //    //string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"), fname);

                //    //ds.Tables[0].TableName = "Personal Details";
                //    //ds.Tables[1].TableName = "Relative Details";
                //    //ds.Tables[2].TableName = "Demat Details";
                //    //ds.Tables[3].TableName = "Holding Details";
                //    //ds.Tables[4].TableName = "Final Declaration Details";
                //    //ExportToExcel.ExportDataSetToExcel(ds, filePath);

                //    obj.misReportPath = xFile + ".xlsx";

                //}
                obj.misReportPath = xFile + ".xlsx";
                objUserRes.User = obj;
                objUserRes.StatusFl = true;
                objUserRes.Msg = "Data has been fetched successfully !";
                return objUserRes;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "UserRepository", "GetMISReport", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                UserResponse objUserRes = new UserResponse();
                objUserRes.StatusFl = false;
                objUserRes.Msg = "Processing failed, because of system error !";
                return objUserRes;
            }
        }
        #endregion
        #region "Set User Role"
        public UserResponse SetUserRole(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@MODE", "SET_USER_ROLE");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                parameters[4] = new SqlParameter("@ROLE", objUser.userRole.ROLE_NM);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                UserResponse objUserRes = new UserResponse();
                objUserRes.StatusFl = true;
                objUserRes.Msg = "Information Updated Successfully !";
                return objUserRes;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message, ex.Source, ex.StackTrace, "UserRepository", "SetUserRole", Convert.ToString(HttpContext.Current.Session["EmployeeId"]), 5, Convert.ToInt32(HttpContext.Current.Session["CompanyId"]));
                UserResponse objUserRes = new UserResponse();
                objUserRes.StatusFl = false;
                objUserRes.Msg = "Processing failed, because of system error !";
                return objUserRes;
            }
        }
        #endregion
        #region "Add Company Name And ISIN"
        public CompanyResponse AddCompanyNameAndISIN(CompanySettings objCompanySettings)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@MODE", "ADD_COMPANY_NAME_AND_ISIN");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCompanySettings.companyId);
                parameters[3] = new SqlParameter("@COMPANY_NM", objCompanySettings.companyName);
                parameters[4] = new SqlParameter("@COMPANY_ISIN", objCompanySettings.companyISIN);
                parameters[5] = new SqlParameter("@TOTAL_PHYSICAL_SHARES", objCompanySettings.totalPhysicalShares);

                CompanyResponse oCompanyRes = new CompanyResponse();
                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_COMPANY_SETTINGS", objCompanySettings.MODULE_DATABASE, parameters);

                if (Convert.ToInt32(parameters[1].Value) == 1)
                {
                    oCompanyRes.StatusFl = true;
                    oCompanyRes.Msg = "Data has been added successfully !";
                }
                else
                {
                    oCompanyRes.StatusFl = true;
                    oCompanyRes.Msg = "Data has been updated successfully !";
                }


                return oCompanyRes;
            }
            catch (Exception ex)
            {
                CompanyResponse oCompanyRes = new CompanyResponse();
                oCompanyRes.StatusFl = false;
                oCompanyRes.Msg = "Processing failed, because of system error !";
                return oCompanyRes;
            }
        }
        #endregion
        #region "Get Company Name And ISIN"
        public CompanyResponse GetCompanyNameAndISIN(CompanySettings objCompanySettings)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "GET_COMPANY_NAME_AND_ISIN");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCompanySettings.companyId);
                parameters[3] = new SqlParameter("@COMPANY_NM", objCompanySettings.companyName);
                parameters[4] = new SqlParameter("@COMPANY_ISIN", objCompanySettings.companyISIN);

                CompanyResponse oCompanyRes = new CompanyResponse();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_COMPANY_SETTINGS", objCompanySettings.MODULE_DATABASE, parameters);

                CompanySettings companySettings = new CompanySettings();

                if (ds.Tables.Count > 0)
                {
                    oCompanyRes.Msg = "Data has been fetched successfully !";
                    oCompanyRes.StatusFl = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            companySettings.companyName = !String.IsNullOrEmpty(Convert.ToString(dr["COMPANY_NAME"])) ? Convert.ToString(dr["COMPANY_NAME"]) : String.Empty;
                            companySettings.companyISIN = !String.IsNullOrEmpty(Convert.ToString(dr["ISIN_NUMBER"])) ? Convert.ToString(dr["ISIN_NUMBER"]) : String.Empty;
                            companySettings.totalPhysicalShares = !String.IsNullOrEmpty(Convert.ToString(dr["TOTAL_PHYSICAL_SHARES"])) ? Convert.ToString(dr["TOTAL_PHYSICAL_SHARES"]) : String.Empty;
                        }
                    }
                }
                else
                {
                    oCompanyRes.Msg = "No data found!";
                    oCompanyRes.StatusFl = true;
                }

                oCompanyRes.CompanySettings = companySettings;

                return oCompanyRes;
            }
            catch (Exception ex)
            {
                CompanyResponse oCompanyRes = new CompanyResponse();
                oCompanyRes.StatusFl = false;
                oCompanyRes.Msg = "Processing failed, because of system error !";
                return oCompanyRes;
            }
        }
        #endregion
        #region "Validate Relative Detail Used In Higher Component"
        public bool ValidateRelativeDetailUsedInHigherComponent(Int32 relativeId, Int32 companyId, string moduleDatabase)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@RELATIVE_ID", relativeId);
                parameter[1] = new SqlParameter("@COMPANY_ID", companyId);
                parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;
                parameter[3] = new SqlParameter("@MODE", "VALIDATE_RELATIVE_DETAIL_USED_IN_HIGHER_COMPONENT");

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", moduleDatabase, parameter);

                if (Convert.ToInt32(parameter[2].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Validate Demat Detail Used In Higher Component"
        public bool ValidateDematDetailUsedInHigherComponent(Int32 dematAccountId, Int32 companyId, string moduleDatabase)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@ACCOUNT_ID", dematAccountId);
                parameter[1] = new SqlParameter("@COMPANY_ID", companyId);
                parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;
                parameter[3] = new SqlParameter("@MODE", "VALIDATE_DEMAT_DETAIL_USED_IN_HIGHER_COMPONENT");

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", moduleDatabase, parameter);

                if (Convert.ToInt32(parameter[2].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Validate Initial Holding Detail Used In Higher Component"
        public bool ValidateInitialHoldingDetailUsedInHigherComponent(Int32 initialHoldingId, Int32 companyId, string moduleDatabase)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@HOLDING_ID", initialHoldingId);
                parameter[1] = new SqlParameter("@COMPANY_ID", companyId);
                parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;
                parameter[3] = new SqlParameter("@MODE", "VALIDATE_INITIAL_HOLDING_DETAIL_USED_IN_HIGHER_COMPONENT");

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", moduleDatabase, parameter);

                if (Convert.ToInt32(parameter[2].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Validate Physical Holding Detail Used In Higher Component"
        public bool ValidatePhysicalHoldingDetailUsedInHigherComponent(Int32 physicalHoldingId, Int32 companyId, string moduleDatabase)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@HOLDING_ID", physicalHoldingId);
                parameter[1] = new SqlParameter("@COMPANY_ID", companyId);
                parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[2].Direction = ParameterDirection.Output;
                parameter[3] = new SqlParameter("@MODE", "VALIDATE_PHYSICAL_HOLDING_DETAIL_USED_IN_HIGHER_COMPONENT");

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", moduleDatabase, parameter);

                if (Convert.ToInt32(parameter[2].Value) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Delete Relative Details"
        public UserResponse DeleteRelativeDetail(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();


                if (objUser.relativeInfo != null)
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@MODE", "DELETE_RELATIVE_DETAILS");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                    parameters[3] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[4] = new SqlParameter("@RELATIVE_ID", objUser.relativeInfo.ID);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters);
                    ouser.StatusFl = true;
                    ouser.Msg = "Record has been deleted successfully !";
                    ouser.User = objUser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Record not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Demat Details"
        public UserResponse DeleteDematDetail(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();


                if (objUser.dematInfo != null)
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@MODE", "DELETE_DEMAT_DETAILS");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                    parameters[3] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[4] = new SqlParameter("@ACCOUNT_ID", objUser.dematInfo.ID);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_DEMAT_MASTER", objUser.MODULE_DATABASE, parameters);
                    ouser.StatusFl = true;
                    ouser.Msg = "Record has been deleted successfully !";
                    ouser.User = objUser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Record not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Initial Holding Details"
        public UserResponse DeleteInitialHoldingDeclarationDetail(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();


                if (objUser.initialHoldingDetailInfo != null)
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@MODE", "DELETE_INITIAL_HOLDING_DETAILS");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                    parameters[3] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[4] = new SqlParameter("@HOLDING_ID", objUser.initialHoldingDetailInfo.ID);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters);
                    ouser.StatusFl = true;
                    ouser.Msg = "Record has been deleted successfully !";
                    ouser.User = objUser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Record not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Delete Physical Holding Details"
        public UserResponse DeletePhysicalHoldingDeclarationDetail(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();


                if (objUser.physicalHoldingDetailInfo != null)
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@MODE", "DELETE_PHYSICAL_HOLDING_DETAILS");
                    parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[1].Direction = ParameterDirection.Output;
                    parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                    parameters[3] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                    parameters[4] = new SqlParameter("@HOLDING_ID", objUser.physicalHoldingDetailInfo.ID);
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters);
                    ouser.StatusFl = true;
                    ouser.Msg = "Record has been deleted successfully !";
                    ouser.User = objUser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Record not Found !";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion
        #region "Convert Html to Pdf"
        private string ConvertHtmlToPdf(string htmlText, string fileNameTemp)
        {
            String downloadFileDir = "/InsiderTrading/emailAttachment/";
            String fileName = String.Empty;

            //Saves the Html  to disk in Pdf format
            fileName = Path.GetFileNameWithoutExtension(fileNameTemp) + ".pdf";

            //Initialize HTML to PDF converter 

            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            //HTML string and base URL 

            string baseUrl = "/assets/";

            //Convert HTML to PDF document 

            PdfDocument document = htmlConverter.Convert(htmlText, HttpContext.Current.Server.MapPath("~" + baseUrl));

            //Save and close the PDF document 

            document.Save(Path.Combine(HttpContext.Current.Server.MapPath("~" + downloadFileDir), fileName));

            document.Close(true);

            return fileName;
        }
        #endregion
        #region "Create Form Logs"
        private void CreateFormLogs(Int32 formId, string dataElementId, string fileName, string createdBy, Int32 companyId, string moduleDb)
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@MODE", "CREATE_FORM_LOGS");
            parameters[1] = new SqlParameter("@FORM_ID", formId);
            parameters[2] = new SqlParameter("@DataElementId", dataElementId);
            parameters[3] = new SqlParameter("@FILE_NAME", fileName);
            parameters[4] = new SqlParameter("@CREATED_BY", createdBy);
            parameters[5] = new SqlParameter("@COMPANY_ID", companyId);
            SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_EVENTS", moduleDb, parameters);
        }
        #endregion
        public UserResponse GetUserApprover(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@Mode", "GET_APPROVER_MULTI_BUSINESS_UNIT");
                parameters[1] = new SqlParameter("@USER_LOGIN", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@ADMIN_DATABASE", objUser.ADMIN_DATABASE);
                parameters[4] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);


                UserResponse oUser = new UserResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_FORMS", objUser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();

                        obj.USER_NM = (!String.IsNullOrEmpty(Convert.ToString(rdr["APPROVER_NAME"]))) ? Convert.ToString(rdr["APPROVER_NAME"]) : String.Empty;
                        obj.USER_EMAIL = (!String.IsNullOrEmpty(Convert.ToString(rdr["APPROVER_EMAIL"]))) ? Convert.ToString(rdr["APPROVER_EMAIL"]) : String.Empty;


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
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse VerifyPan(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@MODE", "Verify_Pan");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@PAN", objUser.panNumber);
                parameters[4] = new SqlParameter("@LOGIN_ID", Convert.ToString(HttpContext.Current.Session["EmployeeId"]));
                parameters[5] = new SqlParameter("@RELATION_ID", objUser.RELATION_ID);
                parameters[6] = new SqlParameter("@RELATIVE_ID", objUser.ID);
                parameters[6] = new SqlParameter("@IS_DESIGNATED_PERSON", objUser.IsDesignatedFl);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                UserResponse ouser = new UserResponse();
                if ((Int32)parameters[1].Value == 0)
                {

                    ouser.StatusFl = true;
                    ouser.Msg = "Pan verified successfully !";
                    ouser.User = objUser;
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Pan already exist ! In case of any discrepancy kindly contact to compliance officer.";
                    ouser.User = objUser;
                }
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse SaveModifyRequest(User objuser)
        {
            UserResponse ouser = new UserResponse();
            try
            {
                string _sql = "SELECT TASK_FOR,TASK_ID FROM  PROCS_INSIDER_USER_TASK WHERE TASK_ID IN (SELECT MAX(TASK_ID) FROM PROCS_INSIDER_USER_TASK where USER_LOGIN='" + objuser.LOGIN_ID + "')";
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                string TaskFor = string.Empty;
                while (rdr.Read())
                {
                    TaskFor = rdr["TASK_FOR"].ToString();
                }

                _sql = "INSERT INTO PROCS_INSIDER_USER_TASK(USER_LOGIN,TASK_FOR,TASK_CREATED_ON,TASK_STATUS," +
                    "TASK_START_DT,TASK_END_DT,TASK_COMPLETED_ON,DATA_ELEMENT_ID,REMARKS) VALUES('" + objuser.LOGIN_ID + "'," +
                    "'" + TaskFor + "',GETDATE(),'Pending',GETDATE(),DATEADD(D,7,GETDATE()),NULL,'" + objuser.LOGIN_ID + "'," +
                    "'" + objuser.remarks + "')";
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);

                _sql = "SELECT A.USER_NM,A.USER_EMAIL FROM " + objuser.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_USER(NOLOCK) B ON A.LOGIN_ID=B.USER_LOGIN " +
                    "WHERE B.IS_APPROVER='Yes'";
                DataSet dsComplianceOfficer = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                DataTable dtComplianceOfficer = dsComplianceOfficer.Tables[0];

                _sql = "SELECT A.USER_NM FROM " + objuser.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_USER(NOLOCK) B ON A.LOGIN_ID=B.USER_LOGIN " +
                    "WHERE B.USER_LOGIN='" + objuser.LOGIN_ID + "'";
                object obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);

                SqlParameter[] parameters1 = new SqlParameter[3];
                parameters1[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[1].Direction = ParameterDirection.Output;
                parameters1[2] = new SqlParameter("@COMPANY_ID", objuser.companyId);

                DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objuser.MODULE_DATABASE, parameters1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                    string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                    string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                    Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                    bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                    string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                    string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                    bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                    string _sMsg = "To,<br />";
                    _sMsg += "The Compliance Officer<br /><br />";
                    _sMsg += (string)obj + " has requested for editing their disclosure in the system, with reason stated below:<br />";
                    _sMsg += objuser.remarks;

                    string _sSubject = "Disclosure edit request by " + (string)obj;

                    EmailSender.SendRequestMail(
                        defaultEmail, Convert.ToString(dtComplianceOfficer.Rows[0]["USER_EMAIL"]), _sSubject, _sMsg,
                        smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port, objuser.LOGIN_ID, objuser.companyId);
                }
                ouser.StatusFl = true;
                ouser.Msg = "Data has been saved successfully !";
                ouser.User = objuser;
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse UpdateModifyRequest(User objuser)
        {
            UserResponse ouser = new UserResponse();
            try
            {
                string _sql = "";
                _sql = "SELECT TASK_FOR FROM PROCS_INSIDER_USER_TASK(NOLOCK) WHERE TASK_ID=" + Convert.ToString(objuser.ID);
                string _sTaskTyp = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);

                _sql = "SELECT DATA_ELEMENT_ID FROM PROCS_INSIDER_USER_TASK(NOLOCK) WHERE TASK_ID=" + Convert.ToString(objuser.ID);
                string _sDataElementId = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                if (objuser.status == "Approved")
                {
                    if (_sTaskTyp == "Annexure III")
                    {
                        _sql = "UPDATE PROCS_INSIDER_USER_TASK SET TASK_STATUS='Open',TASK_END_DT=DATEADD(D,7,GETDATE()) WHERE TASK_ID=" + Convert.ToString(objuser.ID);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);

                        _sql = "UPDATE PROCS_INSIDER_ANNEXURE_III_TASK SET ANNEXURE_STATUS='Open' WHERE TASK_ID=" + _sDataElementId;
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                    }
                    else
                    {
                        _sql = "UPDATE PROCS_INSIDER_USER_TASK SET TASK_STATUS='Open',TASK_END_DT=DATEADD(D,7,GETDATE()) " +
                            "WHERE TASK_ID=" + Convert.ToString(objuser.ID) + ";" +
                            "UPDATE A SET A.IS_FINAL_DECLARED=0 FROM USER_DECLARATION_HDR A " +
                            "INNER JOIN PROCS_INSIDER_USER_TASK(NOLOCK) B ON A.USER_ID=B.USER_LOGIN " +
                            "WHERE B.TASK_ID=" + Convert.ToString(objuser.ID);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                    }
                }
                else
                {
                    if (_sTaskTyp == "Annexure III")
                    {
                        _sql = "UPDATE PROCS_INSIDER_USER_TASK SET TASK_STATUS='Cancelled',TASK_END_DT=DATEADD(D,7,GETDATE()) WHERE TASK_ID=" + Convert.ToString(objuser.ID);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);

                        _sql = "UPDATE PROCS_INSIDER_ANNEXURE_III_TASK SET ANNEXURE_STATUS='Close' WHERE TASK_ID=" + _sDataElementId;
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                    }
                    else
                    {
                        _sql = "UPDATE PROCS_INSIDER_USER_TASK SET TASK_STATUS='Cancelled' WHERE TASK_ID=" + Convert.ToString(objuser.ID);
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                    }
                }


                _sql = "SELECT A.USER_NM,A.USER_EMAIL FROM " + objuser.ADMIN_DATABASE + "..PROCS_USERS(NOLOCK) A " +
                    "INNER JOIN PROCS_INSIDER_USER_TASK(NOLOCK) B ON A.LOGIN_ID=B.USER_LOGIN " +
                    "WHERE B.TASK_ID=" + Convert.ToString(objuser.ID);
                DataSet dsUser = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.Text, _sql, objuser.MODULE_DATABASE, null);
                DataTable dtUser = dsUser.Tables[0];

                SqlParameter[] parameters1 = new SqlParameter[3];
                parameters1[0] = new SqlParameter("@Mode", "GET_Smtp_Config_List");
                parameters1[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[1].Direction = ParameterDirection.Output;
                parameters1[2] = new SqlParameter("@COMPANY_ID", objuser.companyId);

                DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CONFIG_SMTP", objuser.MODULE_DATABASE, parameters1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string defaultEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["DEFAULT_EMAIL"]) : String.Empty;
                    string disclosureEmail = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["CONTINUAL_DISCLOSURE_EMAIL"]) : String.Empty;
                    string smtpHostName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_HOST_NAME"]) : String.Empty;
                    Int32 port = Convert.ToInt32(ds1.Tables[0].Rows[0]["PORT"]);
                    bool ssl = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["SSL"]) == "Yes" ? true : false) : false;
                    string smtpUserName = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]))) ? Convert.ToString(ds1.Tables[0].Rows[0]["SMTP_USER_NAME"]) : String.Empty;
                    string password = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]))) ? CryptorEngine.Decrypt(Convert.ToString(ds1.Tables[0].Rows[0]["PASSWORD"]), true) : String.Empty;
                    bool userDefaultCredential = (!String.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]))) ? (Convert.ToString(ds1.Tables[0].Rows[0]["USER_DEFAULT_CREDENTIAL"]) == "Yes" ? true : false) : false;

                    string _sMsg = "Dear " + Convert.ToString(dtUser.Rows[0]["USER_NM"]) + ",<br /><br />";
                    _sMsg += "Your request to edit disclosure has been " + (objuser.status == "Approved" ? "approved. " : "rejected. ") + "<br />";
                    if (objuser.status == "Approved")
                    {
                        _sMsg += "Please login into the application to modify your disclosure.<br /><br />";
                    }
                    _sMsg += "Regards,<br />Compliance Officer";

                    string _sSubject = "Disclosure request " + (objuser.status == "Approved" ? "approved " : "rejected");

                    EmailSender.SendRequestMail(
                        defaultEmail, Convert.ToString(dtUser.Rows[0]["USER_EMAIL"]), _sSubject, _sMsg,
                        smtpHostName, ssl, smtpUserName, password, userDefaultCredential, port, objuser.LOGIN_ID, objuser.companyId);
                }
                ouser.StatusFl = true;
                ouser.Msg = "Data has been saved successfully !";
                ouser.User = objuser;
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse PreviewDeclarationForm(User objUser)
        {
            try
            {
                string fileName = String.Empty;
                List<EventBasedForm> lstAllFormEvents = null;
                List<string> allAttachments = new List<string>();

                UserResponse ouser = new UserResponse();
                
                string subEvent = string.Empty;
                string role_name = string.Empty;
                String connectionStringIT = CryptorEngine.Decrypt(Convert.ToString(ConfigurationManager.AppSettings["ConnectionStringIT"]), true);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT B.ROLE_NAME FROM PROCS_INSIDER_USER(NOLOCK) A JOIN PROCS_INSIDER_ROLE_MSTR(NOLOCK) B ON A.USER_ROLE=B.ROLE_ID WHERE A.USER_LOGIN='" + objUser.LOGIN_ID + "'", conn))
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                role_name = Convert.ToString(rdr["ROLE_NAME"]);
                            }
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
                if (role_name == "Promoter" || role_name == "Director" || role_name == "KMP" || role_name == "Promoters Group" || role_name == "Body Corporate")
                {
                    subEvent = "Director";
                }
                else if (role_name == "Connected Person")
                {
                    subEvent = "Connected";
                }
                else if (role_name == "Designated Person" || role_name == "Admin")
                {
                    subEvent = "Designated";
                }
                else
                {
                    subEvent = "Admin";
                }

                string sqlTsk = "SELECT TOP 1 TASK_FOR FROM PROCS_INSIDER_USER_TASK(NOLOCK) WHERE USER_LOGIN='" + objUser.LOGIN_ID + "' " +
                    "AND TASK_FOR IN('Initial Disclosure Reminder','Annual Disclosure Reminder') AND TASK_STATUS='Open' ORDER BY TASK_ID DESC";
                string sTskFor = (string)SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.Text, sqlTsk, Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]), null);

                if (sTskFor.ToUpper() == "INITIAL DISCLOSURE REMINDER")
                {
                    //string subEvent = "One Time";
                    lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "User Declaration", subEvent);
                }
                else
                {
                    lstAllFormEvents = Form.GetAllFormEvents(objUser.companyId, objUser.MODULE_DATABASE, objUser.ADMIN_DATABASE, objUser.LOGIN_ID, objUser.LOGIN_ID, "Annual Declaration", subEvent);
                }

                foreach (var obj in lstAllFormEvents)
                {
                    obj.fileName = obj.formName + "_" + DateTime.UtcNow.ToString("yyyy MM dd HH mm ss fff", CultureInfo.InvariantCulture) + ".pdf";
                    if (!String.IsNullOrEmpty(obj.formTemplate) && !String.IsNullOrEmpty(obj.fileName))
                    {
                        string docFileName3 = Form.CreateDocFile(obj.formTemplate, obj.fileName, obj.formOrientation, HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"));
                        if (!String.IsNullOrEmpty(docFileName3))
                        {
                            //String filePath = "/InsiderTrading/emailAttachment/";
                            Form.ConvertDocToPDF(docFileName3, obj.fileName, HttpContext.Current.Server.MapPath("~/InsiderTrading/emailAttachment/"));
                        }
                    }
                    if (!String.IsNullOrEmpty(obj.fileName))
                    {
                        allAttachments.Add("/InsiderTrading/emailAttachment/" + obj.fileName);
                    }
                }
                objUser.lstAttachment = allAttachments;

                ouser.StatusFl = true;
                ouser.Msg = "Form has been downloaded successfully !";
                ouser.User = objUser;

                return ouser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        public UserResponse GetRelativeList(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();
                if (declarationObject > 0)
                {

                    parameters[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID");
                    parameters[2] = new SqlParameter("@D_ID", declarationObject);

                    SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_RELATIVE_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (rdr.HasRows)
                    {
                        List<Relative> lstRelativeDetail = new List<Relative>();
                        while (rdr.Read())
                        {
                            Relative objRelative = new Relative();
                            objRelative.ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_ID"])) ? Convert.ToInt32(rdr["RELATIVE_ID"]) : 0;
                            objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
                            objRelative.relation = new Relation
                            {
                                RELATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_ID"])) ? Convert.ToInt32(rdr["RELATION_ID"]) : 0,
                                RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_NAME"])) ? Convert.ToString(rdr["RELATION_NAME"]) : String.Empty
                            };
                            objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
                            objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                            objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_EMAIL"])) ? Convert.ToString(rdr["RELATIVE_EMAIL"]) : String.Empty;

                            objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
                            objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
                            objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr["MOBILE"])) ? Convert.ToString(rdr["MOBILE"]) : String.Empty;
                            objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"])) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                            objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr["PIN_CODE"])) ? Convert.ToString(rdr["PIN_CODE"]) : String.Empty;
                            objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr["COUNTRY"])) ? Convert.ToString(rdr["COUNTRY"]) : String.Empty;
                            objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"])) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                            objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"])) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
                            objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                            //objRelative.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objRelative.isDeleteRelative = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DELETE_RELATIVE"])) ? (Convert.ToString(rdr["IS_DELETE_RELATIVE"]) == "Yes" ? true : false) : false;
                            objRelative.IsdesignatedPerson = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DP"])) ? Convert.ToString(rdr["IS_DP"]) : String.Empty;
                            lstRelativeDetail.Add(objRelative);

                        }
                        objUser.lstRelative = lstRelativeDetail;
                    }
                    rdr.Close();

                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";

                return objUserResponse;
            }
        }

        #region "Get Debt Security Details"
        public UserResponse GetDebtSecurityDetails(User objUser)
        {
            try
            {
                UserResponse objUserResponse = new UserResponse();
                SqlParameter[] parameters = new SqlParameter[3];
                SqlParameter[] parameters1 = new SqlParameter[3];
                parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[0].Direction = ParameterDirection.Output;
                parameters1[1] = new SqlParameter("@MODE", "GET_DEBT_SECURITY_DETAILS");
                parameters1[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters1);
                if (rdr1.HasRows)
                {
                    List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
                    while (rdr1.Read())
                    {
                        DematAccount objDematAccountDetail = new DematAccount();
                        objDematAccountDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_ID"])) ? Convert.ToInt32(rdr1["ACCOUNT_ID"]) : 0;
                        objDematAccountDetail.RELATIVE_ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_ID"])) ? Convert.ToInt32(rdr1["RELATIVE_ID"]) : 0;
                        objDematAccountDetail.RELATIVE_NM = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : String.Empty;
                        objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
                        lstDematAccountDetail.Add(objDematAccountDetail);
                    }
                    objUser.lstDematAccount = lstDematAccountDetail;
                }
                rdr1.Close();
                objUserResponse.StatusFl = true;
                objUserResponse.Msg = "Data has been fetched succesfully !";
                objUserResponse.User = objUser;
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";
                return objUserResponse;
            }
        }
        #endregion

        #region "Get Transactional Information"
        public UserResponse GetTransactionalInfo(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();
                if (declarationObject > 0)
                {
                    parameters[1] = new SqlParameter("@MODE", "GET_PERSONAL_DETAILS_BY_D_ID");
                    parameters[2] = new SqlParameter("@D_ID", declarationObject);
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        User obj = new User();
                        obj.residentType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"]) : String.Empty;
                        obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PAN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PAN"]) : String.Empty;
                        obj.identificationType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"]) : String.Empty;
                        obj.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"]) : String.Empty;
                        obj.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"])) ? Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"]) : String.Empty;
                        obj.address = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"]) : String.Empty;
                        obj.pinCode = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"]) : String.Empty;
                        obj.country = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"])) ? Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"]) : String.Empty;
                        obj.Ssn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SSN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["SSN"]) : String.Empty;
                        obj.employeeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"]) : String.Empty;
                        obj.joiningDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"]) : String.Empty;
                        obj.becomingInsiderDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"]) : String.Empty;
                        obj.becomingInsiderDate = obj.becomingInsiderDate == "1/1/1900 12:00:00 AM" ? String.Empty : obj.becomingInsiderDate;
                        obj.department = new Department
                        {
                            DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]) : 0
                        };
                        obj.location = new Location
                        {
                            locationId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LOCATION"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]) : 0
                        };
                        obj.designation = new Designation
                        {
                            DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGNATION_ID"]) : 0
                        };
                        obj.category = new Category
                        {
                            ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORY_ID"]) : 0,
                            subCategory = new SubCategory
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"]) : 0
                            }
                        };
                        obj.dinNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"]) : String.Empty;
                        obj.status = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STATUS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) : String.Empty;
                        obj.D_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["D_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["D_ID"]) : 0;
                        obj.companyId = objUser.companyId;
                        obj.MODULE_DATABASE = objUser.MODULE_DATABASE;
                        obj.moduleId = objUser.moduleId;
                        obj.LOGIN_ID = objUser.LOGIN_ID;
                        obj.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"]) : String.Empty;
                        obj.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                        obj.institutionName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"]) : String.Empty;
                        obj.stream = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STREAM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STREAM"]) : String.Empty;
                        obj.employerDetails = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"]) : String.Empty;
                        obj.IsMarried = Convert.ToString(ds.Tables[0].Rows[0]["IS_MARRIED"]);
                        objUser = obj;
                    }
                    parameters[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID");
                    SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (rdr.HasRows)
                    {
                        List<Relative> lstRelativeDetail = new List<Relative>();
                        while (rdr.Read())
                        {
                            Relative objRelative = new Relative();
                            objRelative.ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_ID"])) ? Convert.ToInt32(rdr["RELATIVE_ID"]) : 0;
                            objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
                            objRelative.relation = new Relation
                            {
                                RELATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_ID"])) ? Convert.ToInt32(rdr["RELATION_ID"]) : 0,
                                RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_NAME"])) ? Convert.ToString(rdr["RELATION_NAME"]) : String.Empty
                            };
                            objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
                            objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                            objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_EMAIL"])) ? Convert.ToString(rdr["RELATIVE_EMAIL"]) : String.Empty;
                            objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
                            objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
                            objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr["MOBILE"])) ? Convert.ToString(rdr["MOBILE"]) : String.Empty;
                            objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"])) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                            objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr["PIN_CODE"])) ? Convert.ToString(rdr["PIN_CODE"]) : String.Empty;
                            objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr["COUNTRY"])) ? Convert.ToString(rdr["COUNTRY"]) : String.Empty;
                            objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"])) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                            objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"])) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
                            objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                            objRelative.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objRelative.isDeleteRelative = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DELETE_RELATIVE"])) ? (Convert.ToString(rdr["IS_DELETE_RELATIVE"]) == "Yes" ? true : false) : false;
                            objRelative.IsdesignatedPerson = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DP"])) ? Convert.ToString(rdr["IS_DP"]) : String.Empty;
                            lstRelativeDetail.Add(objRelative);
                        }
                        objUser.lstRelative = lstRelativeDetail;
                    }
                    rdr.Close();
                    SqlParameter[] parameters1 = new SqlParameter[3];
                    parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[0].Direction = ParameterDirection.Output;
                    parameters1[1] = new SqlParameter("@MODE", "GET_DEMAT_ACCOUNT_DETAILS_BY_D_ID");
                    parameters1[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters1);
                    if (rdr1.HasRows)
                    {
                        List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
                        while (rdr1.Read())
                        {
                            DematAccount objDematAccountDetail = new DematAccount();
                            objDematAccountDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_ID"])) ? Convert.ToInt32(rdr1["ACCOUNT_ID"]) : 0;
                            objDematAccountDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_ID"])) ? Convert.ToInt32(rdr1["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : "Self"
                            };
                            objDematAccountDetail.relative.relativeName = objDematAccountDetail.relative.ID == -1 ? "Not Applicable" : objDematAccountDetail.relative.relativeName;
                            objDematAccountDetail.depositoryName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_NAME"]) : String.Empty;
                            objDematAccountDetail.clientId = !String.IsNullOrEmpty(Convert.ToString(rdr1["CLIENT_ID"])) ? Convert.ToString(rdr1["CLIENT_ID"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantId = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"]) : String.Empty;
                            objDematAccountDetail.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr1["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr1["TRADING_MEMBER_ID"]) : String.Empty;
                            objDematAccountDetail.dematType = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEMAT_TYPE"])) ? Convert.ToString(rdr1["DEMAT_TYPE"]) : String.Empty;
                            objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
                            objDematAccountDetail.status = !String.IsNullOrEmpty(Convert.ToString(rdr1["STATUS"])) ? Convert.ToString(rdr1["STATUS"]) : String.Empty;
                            objDematAccountDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr1["CREATED_ON"])) ? Convert.ToString(rdr1["CREATED_ON"]) : String.Empty;
                            objDematAccountDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objDematAccountDetail.isDeleteDemat = !String.IsNullOrEmpty(Convert.ToString(rdr1["IS_DELETE_DEMAT"])) ? (Convert.ToString(rdr1["IS_DELETE_DEMAT"]) == "Yes" ? true : false) : false;
                            lstDematAccountDetail.Add(objDematAccountDetail);
                        }
                        objUser.lstDematAccount = lstDematAccountDetail;
                    }
                    rdr1.Close();
                    SqlParameter[] parameters2 = new SqlParameter[3];
                    parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters2[0].Direction = ParameterDirection.Output;
                    parameters2[1] = new SqlParameter("@MODE", "GET_INITIAL_HOLDING_DETAILS_BY_D_ID");
                    parameters2[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters2);
                    if (rdr2.HasRows)
                    {
                        List<InitialHoldingDetail> lstInitialHoldingDetail = new List<InitialHoldingDetail>();
                        while (rdr2.Read())
                        {
                            InitialHoldingDetail initialHoldingDetail = new InitialHoldingDetail();
                            initialHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["HOLDING_ID"])) ? Convert.ToInt32(rdr2["HOLDING_ID"]) : 0;
                            initialHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr2["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr2["COMPANY_NAME"])) ? Convert.ToString(rdr2["COMPANY_NAME"]) : String.Empty
                            };
                            initialHoldingDetail.restrictedCompany.companyName = initialHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : initialHoldingDetail.restrictedCompany.companyName;
                            initialHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE"])) ? Convert.ToString(rdr2["SECURITY_TYPE"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr2["SECURITY_TYPE_NAME"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = initialHoldingDetail.securityType == "0" ? "Not Applicable" : initialHoldingDetail.securityTypeName;
                            initialHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_ID"])) ? Convert.ToInt32(rdr2["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty
                            };
                            initialHoldingDetail.relative.relativeName = initialHoldingDetail.relative.ID == -1 ? "Not Applicable" : initialHoldingDetail.relative.relativeName;
                            initialHoldingDetail.dematAccount = new DematAccount
                            {
                                //  ID = Convert.ToInt32(rdr2["ACCOUNT_ID"]),
                                accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"]) : String.Empty,
                                tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr2["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr2["TRADING_MEMBER_ID"]) : String.Empty
                            };
                            initialHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr2["CURRENT_HOLDING"])) ? Convert.ToInt32(rdr2["CURRENT_HOLDING"]) : 0;
                            initialHoldingDetail.FY_INITIAL = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_INITIAL_HOLDING"])) ? Convert.ToInt32(rdr2["FY_INITIAL_HOLDING"]) : 0;
                            initialHoldingDetail.FY_LAST = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_LASTHOLDING"])) ? Convert.ToInt32(rdr2["FY_LASTHOLDING"]) : 0;
                            initialHoldingDetail.TOTAL_BUY = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALBUY"])) ? Convert.ToInt32(rdr2["FY_TOTALBUY"]) : 0;
                            initialHoldingDetail.TOTAL_SELL = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALSELL"])) ? Convert.ToInt32(rdr2["FY_TOTALSELL"]) : 0;
                            initialHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
                            initialHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            initialHoldingDetail.isDeleteInitialHolding = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
                            lstInitialHoldingDetail.Add(initialHoldingDetail);
                        }
                        objUser.lstInitialHoldingDetail = lstInitialHoldingDetail;
                    }
                    rdr2.Close();
                    SqlParameter[] parameters3 = new SqlParameter[3];
                    parameters3[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters3[0].Direction = ParameterDirection.Output;
                    parameters3[1] = new SqlParameter("@MODE", "GET_FINAL_DECLARATION_DETAILS_BY_D_ID");
                    parameters3[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr3 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters3);
                    if (rdr3.HasRows)
                    {
                        List<FinalDeclaration> lstFinalDeclaration = new List<FinalDeclaration>();
                        while (rdr3.Read())
                        {
                            FinalDeclaration finalDeclaration = new FinalDeclaration();
                            finalDeclaration.createdOn = !String.IsNullOrEmpty(Convert.ToString(rdr3["CREATED_ON"])) ? Convert.ToString(rdr3["CREATED_ON"]) : String.Empty;
                            finalDeclaration.createdBy = !String.IsNullOrEmpty(Convert.ToString(rdr3["CREATED_BY"])) ? Convert.ToString(rdr3["CREATED_BY"]) : String.Empty;
                            finalDeclaration.fileName = !String.IsNullOrEmpty(Convert.ToString(rdr3["POLICY_DOCUMENT"])) ? Convert.ToString(rdr3["POLICY_DOCUMENT"]) : String.Empty;
                            finalDeclaration.fileFormB = !String.IsNullOrEmpty(Convert.ToString(rdr3["FORMB_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"])) ? Convert.ToString(rdr3["FORMB_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"]) : String.Empty;
                            finalDeclaration.fileFormEOrF = !String.IsNullOrEmpty(Convert.ToString(rdr3["ANNUAL_BIANNUAL_ATTACHMENT"])) ? Convert.ToString(rdr3["ANNUAL_BIANNUAL_ATTACHMENT"]) : String.Empty;
                            finalDeclaration.fileFormK = !String.IsNullOrEmpty(Convert.ToString(rdr3["ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"])) ? Convert.ToString(rdr3["ANNUAL_DISCLOSURE_BY_DESIGNATED_EMPLOYEES"]) : String.Empty;
                            finalDeclaration.version = !String.IsNullOrEmpty(Convert.ToString(rdr3["VERSION_ID"])) ? Convert.ToInt32(rdr3["VERSION_ID"]) : 0;
                            lstFinalDeclaration.Add(finalDeclaration);
                        }
                        objUser.lstFinalDeclaration = lstFinalDeclaration;
                    }
                    rdr3.Close();
                    SqlParameter[] parameters4 = new SqlParameter[3];
                    parameters4[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters4[0].Direction = ParameterDirection.Output;
                    parameters4[1] = new SqlParameter("@MODE", "GET_PHYSICAL_HOLDING_DETAILS_BY_D_ID");
                    parameters4[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr4 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters4);
                    if (rdr4.HasRows)
                    {
                        List<PhysicalHoldingDetail> lstPhysicalHoldingDetail = new List<PhysicalHoldingDetail>();
                        while (rdr4.Read())
                        {
                            PhysicalHoldingDetail physicalHoldingDetail = new PhysicalHoldingDetail();
                            physicalHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["HOLDING_ID"])) ? Convert.ToInt32(rdr4["HOLDING_ID"]) : 0;
                            physicalHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr4["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr4["COMPANY_NAME"])) ? Convert.ToString(rdr4["COMPANY_NAME"]) : String.Empty
                            };
                            physicalHoldingDetail.restrictedCompany.companyName = physicalHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : physicalHoldingDetail.restrictedCompany.companyName;
                            physicalHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_ID"])) ? Convert.ToInt32(rdr4["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_NAME"])) ? Convert.ToString(rdr4["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr4["PAN"])) ? Convert.ToString(rdr4["PAN"]) : String.Empty
                            };
                            physicalHoldingDetail.relative.relativeName = physicalHoldingDetail.relative.ID == -1 ? "Not Applicable" : physicalHoldingDetail.relative.relativeName;
                            physicalHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr4["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr4["NO_OF_SECURITIES"]) : 0;
                            physicalHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr4["CREATED_ON"])) ? Convert.ToString(rdr4["CREATED_ON"]) : String.Empty;
                            physicalHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            physicalHoldingDetail.isDeletePhysicalHolding = !String.IsNullOrEmpty(Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
                            physicalHoldingDetail.dematAccountNo = !String.IsNullOrEmpty(Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"]) : String.Empty;
                            physicalHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE"])) ? Convert.ToString(rdr4["SECURITY_TYPE"]) : String.Empty;
                            //physicalHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr4["SECURITY_TYPE_NAME"]) : String.Empty;
                            lstPhysicalHoldingDetail.Add(physicalHoldingDetail);
                        }
                        objUser.lstPhysicalHoldingDetail = lstPhysicalHoldingDetail;
                    }
                    rdr4.Close();
                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";
                return objUserResponse;
            }
        }
        #endregion

        #region "Add and Update Initial Holding Details"
        public UserResponse AddUpdateInitialHoldingDeclarationDetail(User objUser)
        {
            try
            {
                UserResponse ouser = new UserResponse();
                if (objUser.lstInitialHoldingDetail != null)
                {
                    if (objUser.lstInitialHoldingDetail.Count > 0)
                    {
                        foreach (InitialHoldingDetail objHoldingDetail in objUser.lstInitialHoldingDetail)
                        {
                            SqlParameter[] parameters = new SqlParameter[21];
                            parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_INITIAL_HOLDING_DETAILS");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@RESTRICTED_COMPANY_ID", objHoldingDetail.restrictedCompany.ID);
                            parameters[3] = new SqlParameter("@SECURITY_TYPE", objHoldingDetail.securityType);
                            parameters[4] = new SqlParameter("@RELATIVE_ID", objHoldingDetail.relative.ID);
                            parameters[5] = new SqlParameter("@PAN", objHoldingDetail.relative.panNumber);
                            parameters[6] = new SqlParameter("@ACCOUNT_NO", objHoldingDetail.dematAccount.accountNo);
                            parameters[7] = new SqlParameter("@TRADING_MEMBER_ID", objHoldingDetail.dematAccount.tradingMemberId);
                            parameters[8] = new SqlParameter("@NO_OF_SECURITIES", objHoldingDetail.noOfSecurities);
                            parameters[9] = new SqlParameter("@EMPLOYEE_ID", objUser.LOGIN_ID);
                            parameters[10] = new SqlParameter("@D_ID", objUser.D_ID);
                            parameters[11] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                            parameters[12] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                            parameters[13] = new SqlParameter("@HOLDING_ID", objHoldingDetail.ID);
                            parameters[14] = new SqlParameter("@FY_INITIAL", objHoldingDetail.FY_INITIAL);
                            parameters[15] = new SqlParameter("@FY_LAST", objHoldingDetail.FY_LAST);
                            parameters[16] = new SqlParameter("@TOTAL_BUY", objHoldingDetail.TOTAL_BUY);
                            parameters[17] = new SqlParameter("@TOTAL_SELL", objHoldingDetail.TOTAL_SELL);
                            parameters[18] = new SqlParameter("@FINANCIAL_YEAR", objHoldingDetail.FINANCIAL_YEAR);

                            parameters[19] = new SqlParameter("@TOTAL_BUY_VALUE", objHoldingDetail.TOTAL_BUY_VALUE);
                            parameters[20] = new SqlParameter("@TOTAL_SELL_VALUE", objHoldingDetail.TOTAL_SELL_VALUE);

                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters);
                            if (objHoldingDetail.ID == 0)
                            {
                                objHoldingDetail.ID = (Int32)parameters[1].Value;
                            }
                        }
                        SqlParameter[] parameters2 = new SqlParameter[3];
                        parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters2[0].Direction = ParameterDirection.Output;
                        parameters2[1] = new SqlParameter("@MODE", "GET_INITIAL_HOLDING_DETAILS_BY_D_ID");
                        parameters2[2] = new SqlParameter("@D_ID", objUser.D_ID);
                        SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters2);
                        if (rdr2.HasRows)
                        {
                            List<InitialHoldingDetail> lstInitialHoldingDetail = new List<InitialHoldingDetail>();
                            while (rdr2.Read())
                            {
                                InitialHoldingDetail initialHoldingDetail = new InitialHoldingDetail();
                                initialHoldingDetail.ID = Convert.ToInt32(rdr2["HOLDING_ID"]);
                                initialHoldingDetail.restrictedCompany = new RestrictedCompanies
                                {
                                    ID = Convert.ToInt32(rdr2["RESTRICTED_COMPANY_ID"]),
                                    companyName = !String.IsNullOrEmpty(Convert.ToString(rdr2["COMPANY_NAME"])) ? Convert.ToString(rdr2["COMPANY_NAME"]) : String.Empty
                                };
                                initialHoldingDetail.restrictedCompany.companyName = initialHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : initialHoldingDetail.restrictedCompany.companyName;
                                initialHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE"])) ? Convert.ToString(rdr2["SECURITY_TYPE"]) : String.Empty;
                                initialHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr2["SECURITY_TYPE_NAME"]) : String.Empty;
                                initialHoldingDetail.securityTypeName = initialHoldingDetail.securityType == "0" ? "Not Applicable" : initialHoldingDetail.securityTypeName;
                                initialHoldingDetail.relative = new Relative
                                {
                                    ID = Convert.ToInt32(rdr2["RELATIVE_ID"]),
                                    relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : "Self",
                                    panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty
                                };
                                initialHoldingDetail.relative.relativeName = initialHoldingDetail.relative.ID == -1 ? "Not Applicable" : initialHoldingDetail.relative.relativeName;
                                initialHoldingDetail.dematAccount = new DematAccount
                                {
                                    //  ID = Convert.ToInt32(rdr2["ACCOUNT_ID"]),
                                    accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"]) : String.Empty,
                                    tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr2["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr2["TRADING_MEMBER_ID"]) : String.Empty
                                };
                                initialHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr2["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr2["NO_OF_SECURITIES"]) : 0;
                                initialHoldingDetail.FY_INITIAL = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_INITIAL_HOLDING"])) ? Convert.ToInt32(rdr2["FY_INITIAL_HOLDING"]) : 0;
                                initialHoldingDetail.FY_LAST = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_LASTHOLDING"])) ? Convert.ToInt32(rdr2["FY_LASTHOLDING"]) : 0;
                                initialHoldingDetail.TOTAL_BUY = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALBUY"])) ? Convert.ToInt32(rdr2["FY_TOTALBUY"]) : 0;
                                initialHoldingDetail.TOTAL_SELL = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALSELL"])) ? Convert.ToInt32(rdr2["FY_TOTALSELL"]) : 0;
                                initialHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
                                //   initialHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                                initialHoldingDetail.isDeleteInitialHolding = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
                                lstInitialHoldingDetail.Add(initialHoldingDetail);
                            }
                            objUser.lstInitialHoldingDetail = lstInitialHoldingDetail;
                        }
                        rdr2.Close();


                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been saved successfully !";
                    }
                }
                else
                {
                    ouser.StatusFl = false;
                    ouser.Msg = "Data not Found !";
                }
                if (objUser.lstPhysicalHoldingDetail != null)
                {
                    if (objUser.lstPhysicalHoldingDetail.Count > 0)
                    {
                        foreach (PhysicalHoldingDetail objHoldingDetail in objUser.lstPhysicalHoldingDetail)
                        {
                            SqlParameter[] parameters = new SqlParameter[16];
                            parameters[0] = new SqlParameter("@MODE", "ADD_UPDATE_PHYSICAL_HOLDING_DETAILS");
                            parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters[1].Direction = ParameterDirection.Output;
                            parameters[2] = new SqlParameter("@RESTRICTED_COMPANY_ID", objHoldingDetail.restrictedCompany.ID);
                            parameters[4] = new SqlParameter("@RELATIVE_ID", objHoldingDetail.relative.ID);
                            parameters[5] = new SqlParameter("@PAN", objHoldingDetail.relative.panNumber);
                            parameters[8] = new SqlParameter("@NO_OF_SECURITIES", objHoldingDetail.noOfSecurities);
                            parameters[9] = new SqlParameter("@EMPLOYEE_ID", objUser.LOGIN_ID);
                            parameters[10] = new SqlParameter("@D_ID", objUser.D_ID);
                            parameters[11] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                            parameters[12] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                            parameters[13] = new SqlParameter("@HOLDING_ID", objHoldingDetail.ID);
                            parameters[14] = new SqlParameter("@ACCOUNT_NO", objHoldingDetail.dematAccountNo);
                            parameters[15] = new SqlParameter("@SECURITY_TYPE", objHoldingDetail.securityType);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters);
                            if (objHoldingDetail.ID == 0)
                            {
                                objHoldingDetail.ID = (Int32)parameters[1].Value;
                            }
                        }
                        SqlParameter[] parameters4 = new SqlParameter[3];
                        parameters4[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters4[0].Direction = ParameterDirection.Output;
                        parameters4[1] = new SqlParameter("@MODE", "GET_PHYSICAL_HOLDING_DETAILS_BY_D_ID");
                        parameters4[2] = new SqlParameter("@D_ID", objUser.D_ID);
                        SqlDataReader rdr4 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_HOLDING_MASTER", objUser.MODULE_DATABASE, parameters4);
                        if (rdr4.HasRows)
                        {
                            List<PhysicalHoldingDetail> lstPhysicalHoldingDetail = new List<PhysicalHoldingDetail>();
                            while (rdr4.Read())
                            {
                                PhysicalHoldingDetail physicalHoldingDetail = new PhysicalHoldingDetail();
                                physicalHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["HOLDING_ID"])) ? Convert.ToInt32(rdr4["HOLDING_ID"]) : 0;
                                physicalHoldingDetail.restrictedCompany = new RestrictedCompanies
                                {
                                    ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr4["RESTRICTED_COMPANY_ID"]) : 0,
                                    companyName = !String.IsNullOrEmpty(Convert.ToString(rdr4["COMPANY_NAME"])) ? Convert.ToString(rdr4["COMPANY_NAME"]) : String.Empty
                                };
                                physicalHoldingDetail.restrictedCompany.companyName = physicalHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : physicalHoldingDetail.restrictedCompany.companyName;
                                physicalHoldingDetail.relative = new Relative
                                {
                                    ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_ID"])) ? Convert.ToInt32(rdr4["RELATIVE_ID"]) : 0,
                                    relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_NAME"])) ? Convert.ToString(rdr4["RELATIVE_NAME"]) : "Self",
                                    panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr4["PAN"])) ? Convert.ToString(rdr4["PAN"]) : String.Empty
                                };
                                physicalHoldingDetail.relative.relativeName = physicalHoldingDetail.relative.ID == -1 ? "Not Applicable" : physicalHoldingDetail.relative.relativeName;
                                physicalHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr4["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr4["NO_OF_SECURITIES"]) : 0;
                                physicalHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr4["CREATED_ON"])) ? Convert.ToString(rdr4["CREATED_ON"]) : String.Empty;
                                //   physicalHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                                physicalHoldingDetail.isDeletePhysicalHolding = !String.IsNullOrEmpty(Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
                                physicalHoldingDetail.dematAccountNo = !String.IsNullOrEmpty(Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"]) : String.Empty;
                                physicalHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE"])) ? Convert.ToString(rdr4["SECURITY_TYPE"]) : String.Empty;
                                physicalHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr4["SECURITY_TYPE_NAME"]) : String.Empty;
                                lstPhysicalHoldingDetail.Add(physicalHoldingDetail);
                            }
                            objUser.lstPhysicalHoldingDetail = lstPhysicalHoldingDetail;
                        }
                        rdr4.Close();
                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been fetched succesfully !";
                    }
                }

                if (objUser.lstTransactionHistory != null)
                {
                    if (objUser.lstTransactionHistory.Count > 0)
                    {
                        foreach (TransactionHistory objTransactionDetail in objUser.lstTransactionHistory)
                        {
                            SqlParameter[] parameters3 = new SqlParameter[10];
                            parameters3[0] = new SqlParameter("@MODE", "ADD_UPDATE_TRANSACTION_DETAILS");
                            parameters3[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                            parameters3[1].Direction = ParameterDirection.Output;
                            parameters3[2] = new SqlParameter("@TRANSACTION_ID", objTransactionDetail.transactionId);
                            parameters3[3] = new SqlParameter("@TRANS_BY", objTransactionDetail.transactionBy);
                            parameters3[4] = new SqlParameter("@TRANS_TYPE", objTransactionDetail.transactionSubType);
                            parameters3[5] = new SqlParameter("@TRANS_DATE", ConvertDate(objTransactionDetail.transactionDate));
                            parameters3[6] = new SqlParameter("@QTY", objTransactionDetail.tradeQuantity);
                            parameters3[7] = new SqlParameter("@VALUE", objTransactionDetail.TradeValue);
                            parameters3[8] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                            parameters3[9] = new SqlParameter("@TASK_ID", objUser.TaskId);
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters3);
                        }
                        SqlParameter[] parameters5 = new SqlParameter[3];
                        parameters5[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameters5[0].Direction = ParameterDirection.Output;
                        parameters5[1] = new SqlParameter("@MODE", "GET_TRANSACTION_DETAILS_BY_LOGIN_ID");
                        parameters5[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                        SqlDataReader rdr5 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters5);
                        if (rdr5.HasRows)
                        {
                            List<TransactionHistory> lstTransactionHistory = new List<TransactionHistory>();
                            while (rdr5.Read())
                            {
                                TransactionHistory transactionDetail = new TransactionHistory();
                                transactionDetail.transactionId = !String.IsNullOrEmpty(Convert.ToString(rdr5["TRANSACTION_ID"])) ? Convert.ToInt32(rdr5["TRANSACTION_ID"]) : 0;
                                transactionDetail.transactionBy = !String.IsNullOrEmpty(Convert.ToString(rdr5["TRANS_BY"])) ? Convert.ToString(rdr5["TRANS_BY"]) : String.Empty;
                                transactionDetail.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(rdr5["TRANS_TYPE"])) ? Convert.ToString(rdr5["TRANS_TYPE"]) : String.Empty;
                                transactionDetail.transactionDate = !String.IsNullOrEmpty(rdr5["TRANS_DATE"].ToString()) ? FormatHelper.FormatDate(rdr5["TRANS_DATE"].ToString()) : String.Empty;
                                transactionDetail.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(rdr5["QTY"])) ? Convert.ToInt32(rdr5["QTY"]) : 0;
                                transactionDetail.TradeValue = !String.IsNullOrEmpty(Convert.ToString(rdr5["VALUE"])) ? Convert.ToString(rdr5["VALUE"]) : String.Empty;
                                lstTransactionHistory.Add(transactionDetail);
                            }
                            objUser.lstTransactionHistory = lstTransactionHistory;
                        }
                        rdr5.Close();
                        ouser.StatusFl = true;
                        ouser.Msg = "Data has been fetched succesfully !";
                    }
                }
                ouser.User = objUser;
                return ouser;
            }
            catch (Exception ex)
            {
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion

        #region "BIND DISCLUSER TASK FOR REPORT"
        public UserResponse GetDisclouserTaskList(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Mode", "GET_DISCLOUSER_TASK_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                UserResponse oUser = new UserResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_REPORTS", objUser.MODULE_DATABASE, parameters);
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        User obj = new User();
                        obj.TaskId = Convert.ToInt32(rdr["TASK_ID"]);
                        obj.TaskFor = (!String.IsNullOrEmpty(Convert.ToString(rdr["TITLE"]))) ? Convert.ToString(rdr["TITLE"]) : String.Empty;
                        obj.FinancialYear = (!String.IsNullOrEmpty(Convert.ToString(rdr["COL"]))) ? Convert.ToString(rdr["COL"]) : String.Empty;
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
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                UserResponse oUser = new UserResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }
        #endregion       
        public GenericResponse GetAllUsers(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "GET_USER_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objUser.companyId);
                parameters[3] = new SqlParameter("@MODULE_ID", objUser.moduleId);
                parameters[4] = new SqlParameter("@ADMIN_DB", objUser.ADMIN_DATABASE);
                parameters[5] = new SqlParameter("@BUSINESS_UNIT_ID", objUser.businessUnit.businessUnitId);

                GenericResponse oUser = new GenericResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_PERSONAL_MASTER", objUser.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    List<ExpandoObject> lstUsr = new List<ExpandoObject>();
                    while (rdr.Read())
                    {
                        ExpandoObject o = new ExpandoObject();

                        var expandoDictLogin = o as IDictionary<string, object>;
                        expandoDictLogin.Add("UserLogin", Convert.ToString(rdr["USER_LOGIN"]));

                        var expandoDictNm = o as IDictionary<string, object>;
                        expandoDictNm.Add("UserNm", Convert.ToString(rdr["USER_NM"]));

                        lstUsr.Add(o);
                    }
                    oUser.StatusFl = true;
                    oUser.Msg = "Data has been fetched successfully !";
                    oUser.lst = lstUsr;
                }
                else
                {
                    oUser.StatusFl = false;
                    oUser.Msg = "No data found !";
                }
                rdr.Close();
                return oUser;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(UserRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                GenericResponse oUser = new GenericResponse();
                oUser.StatusFl = false;
                oUser.Msg = "Processing failed, because of system error !";
                return oUser;
            }
        }




        public UserResponse GetPersonDetails(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();

                if (declarationObject > 0)
                {
                    parameters[1] = new SqlParameter("@MODE", "GET_PERSONAL_DETAILS_BY_D_ID");
                    parameters[2] = new SqlParameter("@D_ID", declarationObject);
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        User obj = new User();
                        obj.residentType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["RESIDENT_TYPE"]) : String.Empty;
                        obj.panNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PAN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PAN"]) : String.Empty;
                        obj.identificationType = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE"]) : String.Empty;
                        obj.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"]) : String.Empty;
                        obj.USER_MOBILE = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"])) ? Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_NO"]) : String.Empty;
                        obj.address = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["USER_ADDRESS"]) : String.Empty;
                        obj.pinCode = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["PIN_CODE"]) : String.Empty;
                        obj.country = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"])) ? Convert.ToString(ds.Tables[0].Rows[0]["COUNTRY"]) : String.Empty;
                        obj.Ssn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SSN"])) ? Convert.ToString(ds.Tables[0].Rows[0]["SSN"]) : String.Empty;
                        obj.employeeId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_ID"]) : String.Empty;
                        obj.joiningDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["JOINING_DATE"]) : String.Empty;
                        obj.becomingInsiderDate = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DATE_BECOMING_INSIDER"]) : String.Empty;
                        //obj.becomingInsiderDate = obj.becomingInsiderDate == "1/1/1900 12:00:00 AM" ? String.Empty : obj.becomingInsiderDate;
                        obj.department = new Department
                        {
                            DEPARTMENT_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]) : 0
                        };
                        obj.location = new Location
                        {
                            locationId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["LOCATION"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]) : 0
                        };
                        obj.designation = new Designation
                        {
                            DESIGNATION_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DESIGNATION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["DESIGNATION_ID"]) : 0
                        };
                        obj.category = new Category
                        {
                            ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORY_ID"]) : 0,
                            categoryName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY_NAME"])) ? Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY_NAME"]) : "",
                            subCategory = new SubCategory
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SUBCATEGORY_ID"]) : 0
                            }
                        };
                        obj.dinNumber = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"])) ? Convert.ToString(ds.Tables[0].Rows[0]["DIN_NUMBER"]) : String.Empty;
                        obj.status = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STATUS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) : String.Empty;
                        obj.D_ID = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["D_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["D_ID"]) : 0;
                        obj.companyId = objUser.companyId;
                        obj.MODULE_DATABASE = objUser.MODULE_DATABASE;
                        obj.moduleId = objUser.moduleId;
                        obj.LOGIN_ID = objUser.LOGIN_ID;
                        obj.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"])) ? Convert.ToString(ds.Tables[0].Rows[0]["CREATED_ON"]) : String.Empty;
                        obj.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                        obj.institutionName = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"])) ? Convert.ToString(ds.Tables[0].Rows[0]["INSTITUTE"]) : String.Empty;
                        obj.stream = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["STREAM"])) ? Convert.ToString(ds.Tables[0].Rows[0]["STREAM"]) : String.Empty;
                        obj.employerDetails = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"])) ? Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYER_DETAILS"]) : String.Empty;
                        obj.IsMarried = Convert.ToString(ds.Tables[0].Rows[0]["IS_MARRIED"]);
                        obj.SpouseCnt = Convert.ToInt32(ds.Tables[0].Rows[0]["SPOUSE_CNT"]);
                        objUser = obj;
                    }
                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";
                return objUserResponse;
            }
        }
        public UserResponse GetRelativeDetails(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();

                if (declarationObject > 0)
                {
                    parameters[1] = new SqlParameter("@MODE", "GET_RELATIVE_DETAILS_BY_D_ID");
                    parameters[2] = new SqlParameter("@D_ID", declarationObject);

                    SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                    if (rdr.HasRows)
                    {
                        List<Relative> lstRelativeDetail = new List<Relative>();
                        while (rdr.Read())
                        {
                            Relative objRelative = new Relative();
                            objRelative.ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_ID"])) ? Convert.ToInt32(rdr["RELATIVE_ID"]) : 0;
                            objRelative.relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_NAME"])) ? Convert.ToString(rdr["RELATIVE_NAME"]) : String.Empty;
                            objRelative.relation = new Relation
                            {
                                RELATION_ID = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_ID"])) ? Convert.ToInt32(rdr["RELATION_ID"]) : 0,
                                RELATION_NM = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATION_NAME"])) ? Convert.ToString(rdr["RELATION_NAME"]) : String.Empty
                            };
                            objRelative.relation.RELATION_NM = objRelative.relation.RELATION_ID == 0 ? "Not Applicable" : objRelative.relation.RELATION_NM;
                            objRelative.panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["PAN"])) ? Convert.ToString(rdr["PAN"]) : String.Empty;
                            objRelative.relativeEmail = !String.IsNullOrEmpty(Convert.ToString(rdr["RELATIVE_EMAIL"])) ? Convert.ToString(rdr["RELATIVE_EMAIL"]) : String.Empty;
                            objRelative.identificationType = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_TYPE"])) ? Convert.ToString(rdr["IDENTIFICATION_TYPE"]) : String.Empty;
                            objRelative.identificationNumber = !String.IsNullOrEmpty(Convert.ToString(rdr["IDENTIFICATION_NUMBER"])) ? Convert.ToString(rdr["IDENTIFICATION_NUMBER"]) : String.Empty;
                            objRelative.mobile = !String.IsNullOrEmpty(Convert.ToString(rdr["MOBILE"])) ? Convert.ToString(rdr["MOBILE"]) : String.Empty;
                            objRelative.address = !String.IsNullOrEmpty(Convert.ToString(rdr["ADDRESS"])) ? Convert.ToString(rdr["ADDRESS"]) : String.Empty;
                            objRelative.pinCode = !String.IsNullOrEmpty(Convert.ToString(rdr["PIN_CODE"])) ? Convert.ToString(rdr["PIN_CODE"]) : String.Empty;
                            objRelative.country = !String.IsNullOrEmpty(Convert.ToString(rdr["COUNTRY"])) ? Convert.ToString(rdr["COUNTRY"]) : String.Empty;
                            objRelative.status = !String.IsNullOrEmpty(Convert.ToString(rdr["STATUS"])) ? Convert.ToString(rdr["STATUS"]) : String.Empty;
                            objRelative.remarks = !String.IsNullOrEmpty(Convert.ToString(rdr["REMARKS"])) ? Convert.ToString(rdr["REMARKS"]) : String.Empty;
                            objRelative.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_ON"])) ? Convert.ToString(rdr["CREATED_ON"]) : String.Empty;
                            //objRelative.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objRelative.isDeleteRelative = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DELETE_RELATIVE"])) ? (Convert.ToString(rdr["IS_DELETE_RELATIVE"]) == "Yes" ? true : false) : false;
                            objRelative.IsdesignatedPerson = !String.IsNullOrEmpty(Convert.ToString(rdr["IS_DP"])) ? Convert.ToString(rdr["IS_DP"]) : String.Empty;
                            lstRelativeDetail.Add(objRelative);
                        }
                        objUser.lstRelative = lstRelativeDetail;
                    }
                    rdr.Close();

                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";
                return objUserResponse;
            }
        }
        public UserResponse GetDematDetails(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();

                if (declarationObject > 0)
                {
                    SqlParameter[] parameters1 = new SqlParameter[3];
                    parameters1[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters1[0].Direction = ParameterDirection.Output;
                    parameters1[1] = new SqlParameter("@MODE", "GET_DEMAT_ACCOUNT_DETAILS_BY_D_ID");
                    parameters1[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr1 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters1);
                    if (rdr1.HasRows)
                    {
                        List<DematAccount> lstDematAccountDetail = new List<DematAccount>();
                        while (rdr1.Read())
                        {
                            DematAccount objDematAccountDetail = new DematAccount();
                            objDematAccountDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_ID"])) ? Convert.ToInt32(rdr1["ACCOUNT_ID"]) : 0;
                            objDematAccountDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_ID"])) ? Convert.ToInt32(rdr1["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr1["RELATIVE_NAME"])) ? Convert.ToString(rdr1["RELATIVE_NAME"]) : "Self"
                            };
                            objDematAccountDetail.relative.relativeName = objDematAccountDetail.relative.ID == -1 ? "Not Applicable" : objDematAccountDetail.relative.relativeName;
                            objDematAccountDetail.depositoryName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_NAME"]) : String.Empty;
                            objDematAccountDetail.clientId = !String.IsNullOrEmpty(Convert.ToString(rdr1["CLIENT_ID"])) ? Convert.ToString(rdr1["CLIENT_ID"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantName = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_NAME"]) : String.Empty;
                            objDematAccountDetail.depositoryParticipantId = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"])) ? Convert.ToString(rdr1["DEPOSITORY_PARTICIPANT_ID"]) : String.Empty;
                            objDematAccountDetail.tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr1["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr1["TRADING_MEMBER_ID"]) : String.Empty;
                            objDematAccountDetail.dematType = !String.IsNullOrEmpty(Convert.ToString(rdr1["DEMAT_TYPE"])) ? Convert.ToString(rdr1["DEMAT_TYPE"]) : String.Empty;
                            objDematAccountDetail.accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr1["ACCOUNT_NO"])) ? Convert.ToString(rdr1["ACCOUNT_NO"]) : String.Empty;
                            objDematAccountDetail.status = !String.IsNullOrEmpty(Convert.ToString(rdr1["STATUS"])) ? Convert.ToString(rdr1["STATUS"]) : String.Empty;
                            objDematAccountDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr1["CREATED_ON"])) ? Convert.ToString(rdr1["CREATED_ON"]) : String.Empty;
                            //objDematAccountDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            objDematAccountDetail.isDeleteDemat = !String.IsNullOrEmpty(Convert.ToString(rdr1["IS_DELETE_DEMAT"])) ? (Convert.ToString(rdr1["IS_DELETE_DEMAT"]) == "Yes" ? true : false) : false;
                            lstDematAccountDetail.Add(objDematAccountDetail);
                        }
                        objUser.lstDematAccount = lstDematAccountDetail;
                    }
                    rdr1.Close();

                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";
                return objUserResponse;
            }
        }
        public UserResponse GetHoldingDetails(User objUser)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@MODE", "GET_MAX_D_ID");
                parameters[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);
                Int32 declarationObject = (Int32)parameters[0].Value;
                UserResponse objUserResponse = new UserResponse();

                if (declarationObject > 0)
                {
                    SqlParameter[] parameters2 = new SqlParameter[3];
                    parameters2[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters2[0].Direction = ParameterDirection.Output;
                    parameters2[1] = new SqlParameter("@MODE", "GET_INITIAL_HOLDING_DETAILS_BY_D_ID");
                    parameters2[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr2 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters2);
                    if (rdr2.HasRows)
                    {
                        List<InitialHoldingDetail> lstInitialHoldingDetail = new List<InitialHoldingDetail>();
                        while (rdr2.Read())
                        {
                            InitialHoldingDetail initialHoldingDetail = new InitialHoldingDetail();
                            initialHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["HOLDING_ID"])) ? Convert.ToInt32(rdr2["HOLDING_ID"]) : 0;
                            initialHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr2["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr2["COMPANY_NAME"])) ? Convert.ToString(rdr2["COMPANY_NAME"]) : String.Empty
                            };
                            initialHoldingDetail.restrictedCompany.companyName = initialHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : initialHoldingDetail.restrictedCompany.companyName;
                            initialHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE"])) ? Convert.ToString(rdr2["SECURITY_TYPE"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr2["SECURITY_TYPE_NAME"]) : String.Empty;
                            initialHoldingDetail.securityTypeName = initialHoldingDetail.securityType == "0" ? "Not Applicable" : initialHoldingDetail.securityTypeName;
                            initialHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_ID"])) ? Convert.ToInt32(rdr2["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr2["RELATIVE_NAME"])) ? Convert.ToString(rdr2["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr2["PAN"])) ? Convert.ToString(rdr2["PAN"]) : String.Empty
                            };
                            initialHoldingDetail.relative.relativeName = initialHoldingDetail.relative.ID == -1 ? "Not Applicable" : initialHoldingDetail.relative.relativeName;
                            initialHoldingDetail.dematAccount = new DematAccount
                            {
                                //  ID = Convert.ToInt32(rdr2["ACCOUNT_ID"]),
                                accountNo = !String.IsNullOrEmpty(Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr2["DEMAT_ACCOUNT_NO"]) : String.Empty,
                                tradingMemberId = !String.IsNullOrEmpty(Convert.ToString(rdr2["TRADING_MEMBER_ID"])) ? Convert.ToString(rdr2["TRADING_MEMBER_ID"]) : String.Empty
                            };
                            initialHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr2["CURRENT_HOLDING"])) ? Convert.ToInt32(rdr2["CURRENT_HOLDING"]) : 0;
                            initialHoldingDetail.FY_INITIAL = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_INITIAL_HOLDING"])) ? Convert.ToInt32(rdr2["FY_INITIAL_HOLDING"]) : 0;
                            initialHoldingDetail.FY_LAST = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_LASTHOLDING"])) ? Convert.ToInt32(rdr2["FY_LASTHOLDING"]) : 0;
                            initialHoldingDetail.TOTAL_BUY = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALBUY"])) ? Convert.ToInt32(rdr2["FY_TOTALBUY"]) : 0;
                            initialHoldingDetail.TOTAL_SELL = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALSELL"])) ? Convert.ToInt32(rdr2["FY_TOTALSELL"]) : 0;


                            initialHoldingDetail.TOTAL_BUY_VALUE = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALBUY_VALUE"])) ? Convert.ToDouble(rdr2["FY_TOTALBUY_VALUE"]) : 0;
                            initialHoldingDetail.TOTAL_SELL_VALUE = !String.IsNullOrEmpty(Convert.ToString(rdr2["FY_TOTALSELL_VALUE"])) ? Convert.ToDouble(rdr2["FY_TOTALSELL_VALUE"]) : 0;


                            initialHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr2["CREATED_ON"])) ? Convert.ToString(rdr2["CREATED_ON"]) : String.Empty;
                            //initialHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            initialHoldingDetail.isDeleteInitialHolding = !String.IsNullOrEmpty(Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr2["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
                            lstInitialHoldingDetail.Add(initialHoldingDetail);
                        }
                        objUser.lstInitialHoldingDetail = lstInitialHoldingDetail;
                    }
                    rdr2.Close();

                    SqlParameter[] parameters4 = new SqlParameter[3];
                    parameters4[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters4[0].Direction = ParameterDirection.Output;
                    parameters4[1] = new SqlParameter("@MODE", "GET_PHYSICAL_HOLDING_DETAILS_BY_D_ID");
                    parameters4[2] = new SqlParameter("@D_ID", declarationObject);
                    SqlDataReader rdr4 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters4);
                    if (rdr4.HasRows)
                    {
                        List<PhysicalHoldingDetail> lstPhysicalHoldingDetail = new List<PhysicalHoldingDetail>();
                        while (rdr4.Read())
                        {
                            PhysicalHoldingDetail physicalHoldingDetail = new PhysicalHoldingDetail();
                            physicalHoldingDetail.ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["HOLDING_ID"])) ? Convert.ToInt32(rdr4["HOLDING_ID"]) : 0;
                            physicalHoldingDetail.restrictedCompany = new RestrictedCompanies
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RESTRICTED_COMPANY_ID"])) ? Convert.ToInt32(rdr4["RESTRICTED_COMPANY_ID"]) : 0,
                                companyName = !String.IsNullOrEmpty(Convert.ToString(rdr4["COMPANY_NAME"])) ? Convert.ToString(rdr4["COMPANY_NAME"]) : String.Empty
                            };
                            physicalHoldingDetail.restrictedCompany.companyName = physicalHoldingDetail.restrictedCompany.ID == 0 ? "Not Applicable" : physicalHoldingDetail.restrictedCompany.companyName;
                            physicalHoldingDetail.relative = new Relative
                            {
                                ID = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_ID"])) ? Convert.ToInt32(rdr4["RELATIVE_ID"]) : 0,
                                relativeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["RELATIVE_NAME"])) ? Convert.ToString(rdr4["RELATIVE_NAME"]) : "Self",
                                panNumber = !String.IsNullOrEmpty(Convert.ToString(rdr4["PAN"])) ? Convert.ToString(rdr4["PAN"]) : String.Empty
                            };
                            physicalHoldingDetail.relative.relativeName = physicalHoldingDetail.relative.ID == -1 ? "Not Applicable" : physicalHoldingDetail.relative.relativeName;
                            physicalHoldingDetail.noOfSecurities = !String.IsNullOrEmpty(Convert.ToString(rdr4["NO_OF_SECURITIES"])) ? Convert.ToInt32(rdr4["NO_OF_SECURITIES"]) : 0;
                            physicalHoldingDetail.lastModifiedOn = !String.IsNullOrEmpty(Convert.ToString(rdr4["CREATED_ON"])) ? Convert.ToString(rdr4["CREATED_ON"]) : String.Empty;
                            //physicalHoldingDetail.version = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["VERSION_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["VERSION_ID"]) : 0;
                            physicalHoldingDetail.isDeletePhysicalHolding = !String.IsNullOrEmpty(Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"])) ? (Convert.ToString(rdr4["IS_DELETE_INITIAL_HOLDING"]) == "Yes" ? true : false) : false;
                            physicalHoldingDetail.dematAccountNo = !String.IsNullOrEmpty(Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"])) ? Convert.ToString(rdr4["DEMAT_ACCOUNT_NO"]) : String.Empty;
                            physicalHoldingDetail.securityType = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE"])) ? Convert.ToString(rdr4["SECURITY_TYPE"]) : String.Empty;
                            physicalHoldingDetail.securityTypeName = !String.IsNullOrEmpty(Convert.ToString(rdr4["SECURITY_TYPE_NAME"])) ? Convert.ToString(rdr4["SECURITY_TYPE_NAME"]) : String.Empty;
                            lstPhysicalHoldingDetail.Add(physicalHoldingDetail);
                        }
                        objUser.lstPhysicalHoldingDetail = lstPhysicalHoldingDetail;
                    }
                    rdr4.Close();


                    SqlParameter[] parameters5 = new SqlParameter[4];
                    parameters5[0] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters5[0].Direction = ParameterDirection.Output;
                    parameters5[1] = new SqlParameter("@MODE", "GET_TRANSACTION_HISTORY");
                    parameters5[2] = new SqlParameter("@LOGIN_ID", objUser.LOGIN_ID);
                    parameters5[3] = new SqlParameter("@TASK_ID", objUser.TaskId);
                    SqlDataReader rdr5 = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters5);
                    if (rdr5.HasRows)
                    {
                        List<TransactionHistory> lstTransactionDetail = new List<TransactionHistory>();
                        while (rdr5.Read())
                        {
                            TransactionHistory transactionDetail = new TransactionHistory();
                            transactionDetail.transactionId = !String.IsNullOrEmpty(Convert.ToString(rdr5["TRANSACTION_ID"])) ? Convert.ToInt32(rdr5["TRANSACTION_ID"]) : 0;
                            transactionDetail.transactionBy = !String.IsNullOrEmpty(Convert.ToString(rdr5["TRANS_BY"])) ? Convert.ToString(rdr5["TRANS_BY"]) : String.Empty;
                            transactionDetail.transactionDate = !String.IsNullOrEmpty(rdr5["TRANS_DATE"].ToString()) ? FormatHelper.FormatDate(rdr5["TRANS_DATE"].ToString()) : String.Empty;
                            transactionDetail.transactionSubType = !String.IsNullOrEmpty(Convert.ToString(rdr5["TRANS_TYPE"])) ? Convert.ToString(rdr5["TRANS_TYPE"]) : String.Empty;
                            transactionDetail.TradeValue = !String.IsNullOrEmpty(Convert.ToString(rdr5["VALUE"])) ? Convert.ToString(rdr5["VALUE"]) : String.Empty;
                            transactionDetail.tradeQuantity = !String.IsNullOrEmpty(Convert.ToString(rdr5["QTY"])) ? Convert.ToInt32(rdr5["QTY"]) : 0;
                            lstTransactionDetail.Add(transactionDetail);
                        }
                        objUser.lstTransactionHistory = lstTransactionDetail;
                    }
                    rdr5.Close();
                    objUserResponse.StatusFl = true;
                    objUserResponse.Msg = "Data has been fetched succesfully !";
                    objUserResponse.User = objUser;
                }
                else
                {
                    objUserResponse.StatusFl = false;
                    objUserResponse.Msg = "No data found !";
                }
                return objUserResponse;
            }
            catch (Exception ex)
            {
                UserResponse objUserResponse = new UserResponse();
                objUserResponse.StatusFl = false;
                objUserResponse.Msg = "Processing failed, because of system error !";
                return objUserResponse;
            }
        }
    }
}