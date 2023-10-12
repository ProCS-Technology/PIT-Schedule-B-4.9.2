using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ReportsRequest
    {
        private DeclarationReport _DeclarationReport;
        private TradingReport _TradingReport;
        private BenposHeader _BenposHeader;
        private UPSICommunication _UPSICommunication;
        private LogsReport _LogsReport;
        private User _ObjReport;
        public ReportsRequest()
        {

        }
        public ReportsRequest(User ObjReport)
        {
            _ObjReport = new User();
            _ObjReport = ObjReport;
        }
        public ReportsRequest(DeclarationReport declarationReport)
        {
            _DeclarationReport = new DeclarationReport();
            _DeclarationReport = declarationReport;
        }
        public ReportsRequest(TradingReport tradingReport)
        {
            _TradingReport = new TradingReport();
            _TradingReport = tradingReport;
        }
        public ReportsRequest(BenposHeader benposHeader)
        {
            _BenposHeader = new BenposHeader();
            _BenposHeader = benposHeader;
        }
        public ReportsRequest(UPSICommunication uPSICommunication)
        {
            _UPSICommunication = new UPSICommunication();
            _UPSICommunication = uPSICommunication;
        }
        #region "Get Declaration Report"
        public ReportsResponse GetDeclarationReports()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetDeclarationReports(_DeclarationReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        public ReportsResponse GetNonComplianceReport()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetNonComplianceReport(_TradingReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #region "Get Users Made Declaration Report"
        public ReportsResponse GetUserMadeDeclarationReports()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetUsersMadeDeclarationReports(_DeclarationReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get Users Not Made Declaration Report"
        public ReportsResponse GetUserNotMadeDeclarationReports()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetUsersNotMadeDeclarationReports(_DeclarationReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get Declaration Report Between Dates"
        public ReportsResponse GetDeclarationReportsBetweenDates()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetDeclarationReportsBetweenDates(_DeclarationReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get Trading Report Between Dates"
        public ReportsResponse GetTradingReportBetweenDates()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetTradingReportBetweenDates(_TradingReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Close Non Compliance Task"
        public ReportsResponse CloseNonComplianceTask()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.CloseNonComplianceTask(_TradingReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get Benpos Uploaded List"
        public ReportsResponse GetBenposUploadedList()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetBenposUploadedList(_BenposHeader);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get Benpos Report"
        public ReportsResponse GetBenposReport()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetBenposReport(_BenposHeader);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
        #region "Get UPSI Report Between Dates"
        public ReportsResponse GetUPSIReportBetweenDates()
        {
            ReportsRepository reportsRepository = new ReportsRepository();
            return reportsRepository.GetUPSIReportBetweenDates(_UPSICommunication);
        }
        #endregion
        #region "Get UPSI Template Report Between Dates"
        public ReportsResponse GetUPSITemplateReportBetweenDates()
        {
            ReportsRepository reportsRepository = new ReportsRepository();
            return reportsRepository.GetUPSITemplateReportBetweenDates(_UPSICommunication);
        }
        #endregion
        public ReportsResponse GetConnectedPersonTradingReport()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetConnectedPersonTradingReport(_TradingReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        public ReportsResponse GetBrokerNoteDetailsBetweenDates()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetBrokerNoteDetailsBetweenDates(_TradingReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #region "Get Esop Report Between Dates"
        public ReportsResponse GetEsopReportBetweenDates()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetEsopReportBetweenDates(_TradingReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
        #endregion
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
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_USER_MASTER", objUser.MODULE_DATABASE, parameters);

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

        public ReportsRequest(LogsReport LogsReport)
        {
            _LogsReport = new LogsReport();
            _LogsReport = LogsReport;
        }
        public ReportsResponse GetFormLogsReportBetweenDates()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetFormLogsReportBetweenDates(_LogsReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }

        public ReportsResponse GetTaskDisclouserReport()
        {
            try
            {
                ReportsRepository oRepository = new ReportsRepository();
                return oRepository.GetTaskDisclouserReport(_ObjReport);
            }
            catch (Exception ex)
            {
                ReportsResponse objReportsResponse = new ReportsResponse();
                objReportsResponse.StatusFl = false;
                objReportsResponse.Msg = "Processing failed due to system error !";
                return objReportsResponse;
            }
        }
    }
}