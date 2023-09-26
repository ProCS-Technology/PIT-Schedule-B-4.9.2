using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class TrainingRepository
    {
        #region "Trainining Task Operation"

        #region "Get All Training Modules"
        public TrainingResponse GetTrainingModules(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@MODE", "GET_ALL_TRAINING_MODULES");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TrainingModule obj = new TrainingModule();
                            obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_ID"])) ? Convert.ToInt32(dr["TRAINING_ID"]) : 0;
                            obj.trainingName = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_NM"])) ? Convert.ToString(dr["TRAINING_NM"]) : String.Empty;
                            obj.trainingStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["START_DT"])) ? FormatHelper.FormatDate(dr["START_DT"].ToString()) : String.Empty;
                            obj.trainingEndDate = !String.IsNullOrEmpty(Convert.ToString(dr["END_DT"])) ? FormatHelper.FormatDate(dr["END_DT"].ToString()) : String.Empty;
                            obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_DOC"])) ? Convert.ToString(dr["TRAINING_DOC"]) : String.Empty;
                            obj.noOfPages = !String.IsNullOrEmpty(Convert.ToString(dr["NO_OF_PAGES"])) ? Convert.ToInt32(dr["NO_OF_PAGES"]) : 0;
                            obj.createdOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                            obj.createdBy = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])) ? Convert.ToString(dr["CREATED_BY"]) : String.Empty;
                            obj.modifiedOn = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_ON"])) ? Convert.ToString(dr["MODIFIED_ON"]) : String.Empty;
                            obj.modifiedBy = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_BY"])) ? Convert.ToString(dr["MODIFIED_BY"]) : String.Empty;
                            obj.trainingModuleUserStatus = new TrainingModuleUserStatus
                            {
                                status = !String.IsNullOrEmpty(Convert.ToString(dr["STATUS"])) ? Convert.ToString(dr["STATUS"]) : String.Empty
                            };

                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion

        #region "Get All Training Modules Report"
        public TrainingResponse GetTrainingModulesReport(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@MODE", "GET_ALL_TRAINING_MODULES_REPORT");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TrainingModule obj = new TrainingModule();
                            obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_ID"])) ? Convert.ToInt32(dr["TRAINING_ID"]) : 0;
                            obj.trainingName = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_NM"])) ? Convert.ToString(dr["TRAINING_NM"]) : String.Empty;
                            obj.trainingStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["START_DT"])) ? Convert.ToString(dr["START_DT"]) : String.Empty;
                            obj.trainingEndDate = !String.IsNullOrEmpty(Convert.ToString(dr["END_DT"])) ? Convert.ToString(dr["END_DT"]) : String.Empty;
                            obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_DOC"])) ? Convert.ToString(dr["TRAINING_DOC"]) : String.Empty;
                            obj.noOfPages = !String.IsNullOrEmpty(Convert.ToString(dr["NO_OF_PAGES"])) ? Convert.ToInt32(dr["NO_OF_PAGES"]) : 0;
                            obj.createdOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                            obj.createdBy = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])) ? Convert.ToString(dr["CREATED_BY"]) : String.Empty;
                            obj.modifiedOn = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_ON"])) ? Convert.ToString(dr["MODIFIED_ON"]) : String.Empty;
                            obj.modifiedBy = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_BY"])) ? Convert.ToString(dr["MODIFIED_BY"]) : String.Empty;
                            obj.trainingModuleUserStatus = new TrainingModuleUserStatus
                            {
                                status = !String.IsNullOrEmpty(Convert.ToString(dr["STATUS"])) ? Convert.ToString(dr["STATUS"]) : String.Empty
                            };

                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion

        #region "Get Training file on load to pdf viewer"
        public TrainingResponse GetTrainingFileOnLoadToPdfViewer(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@MODE", "GET_TRAINING_FILE_ON_LOAD_TO_PDF_VIEWER");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[3] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[4].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TrainingModuleDetail obj = new TrainingModuleDetail();
                        obj.Id = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                        obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRAINING_ID"]) : 0;
                        obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_DOC"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_DOC"]) : String.Empty;
                        obj.sequence = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SEQ"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SEQ"]) : 0;

                        objResponse.TrainingModuleDetail = obj;
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion

        #region "Get Training file on next button to pdf viewer"
        public TrainingResponse GetTrainingFileOnNextButtonToPdfViewer(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@MODE", "GET_TRAINING_FILE_ON_NEXT_BUTTON_TO_PDF_VIEWER");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[3] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[4] = new SqlParameter("@CURRENT_PAGE", objTrainingModule.trainingModuleUserStatus.currentPage);
                parameter[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[5].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TrainingModuleDetail obj = new TrainingModuleDetail();
                        obj.Id = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                        obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRAINING_ID"]) : 0;
                        obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_DOC"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_DOC"]) : String.Empty;
                        obj.sequence = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SEQ"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SEQ"]) : 0;

                        objResponse.TrainingModuleDetail = obj;
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion

        #region "Get Training file on previous button to pdf viewer"
        public TrainingResponse GetTrainingFileOnPreviousButtonToPdfViewer(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@MODE", "GET_TRAINING_FILE_ON_PREVIOUS_BUTTON_TO_PDF_VIEWER");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[3] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[4] = new SqlParameter("@CURRENT_PAGE", objTrainingModule.trainingModuleUserStatus.currentPage);
                parameter[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[5].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TrainingModuleDetail obj = new TrainingModuleDetail();
                        obj.Id = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]) : 0;
                        obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_ID"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["TRAINING_ID"]) : 0;
                        obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_DOC"])) ? Convert.ToString(ds.Tables[0].Rows[0]["TRAINING_DOC"]) : String.Empty;
                        obj.sequence = !String.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SEQ"])) ? Convert.ToInt32(ds.Tables[0].Rows[0]["SEQ"]) : 0;

                        objResponse.TrainingModuleDetail = obj;
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion

        #region "On submission Of Trading File"
        public TrainingResponse OnSubmissionOfTrainingFile(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[6];
                parameter[0] = new SqlParameter("@MODE", "ON_SUBMISSION_OF_TRAINING_FILE");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[3] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[4] = new SqlParameter("@REMARKS", objTrainingModule.trainingModuleUserStatus.remarks);
                parameter[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[5].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (Convert.ToInt32(parameter[5].Value) == 1)
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Data has been submitted successfully !";
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Record not found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion

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

        #region "Report Operation"

        #region "Get Training Report"
        public TrainingResponse GetTrainingReport(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[7];
                parameter[0] = new SqlParameter("@MODE", "GET_TRAINING_REPORT");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@TRAINING_FROM", ConvertDate(objTrainingModule.trainingFrom));
                parameter[4] = new SqlParameter("@TRAINING_TO", ConvertDate(objTrainingModule.trainingTo));
                parameter[5] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[6].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TrainingModule obj = new TrainingModule();
                            obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_ID"])) ? Convert.ToInt32(dr["TRAINING_ID"]) : 0;
                            obj.trainingModuleUserStatus = new TrainingModuleUserStatus
                            {
                                userDetail = new User
                                {
                                    USER_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_NM"])) ? Convert.ToString(dr["USER_NM"]) : String.Empty,
                                    USER_EMAIL = !String.IsNullOrEmpty(Convert.ToString(dr["USER_EMAIL"])) ? Convert.ToString(dr["USER_EMAIL"]) : String.Empty,
                                    userRole = new Role
                                    {
                                        ROLE_NM = !String.IsNullOrEmpty(Convert.ToString(dr["USER_ROLE"])) ? Convert.ToString(dr["USER_ROLE"]) : String.Empty
                                    }
                                },
                                status = !String.IsNullOrEmpty(Convert.ToString(dr["STATUS"])) ? Convert.ToString(dr["STATUS"]) : String.Empty,
                                submittedOn = !String.IsNullOrEmpty(Convert.ToString(dr["SUBMITTED_ON"])) ? Convert.ToString(dr["SUBMITTED_ON"]) : String.Empty,
                                remarks = !String.IsNullOrEmpty(Convert.ToString(dr["REMARKS"])) ? Convert.ToString(dr["REMARKS"]) : String.Empty
                            };

                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }
        }
        #endregion

        #endregion

        #region "Master Crud Operation"

        #region "Get All Training Modules Master"
        public TrainingResponse GetAllTrainingModulesMaster(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[4];
                parameter[0] = new SqlParameter("@MODE", "GET_ALL_TRAINING_MODULES_MASTER");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[3].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TrainingModule obj = new TrainingModule();
                            obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_ID"])) ? Convert.ToInt32(dr["TRAINING_ID"]) : 0;
                            obj.trainingName = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_NM"])) ? Convert.ToString(dr["TRAINING_NM"]) : String.Empty;
                            obj.trainingStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["START_DT"])) ? Convert.ToString(dr["START_DT"]) : String.Empty;
                            obj.trainingEndDate = !String.IsNullOrEmpty(Convert.ToString(dr["END_DT"])) ? Convert.ToString(dr["END_DT"]) : String.Empty;
                            obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_DOC"])) ? Convert.ToString(dr["TRAINING_DOC"]) : String.Empty;
                            obj.noOfPages = !String.IsNullOrEmpty(Convert.ToString(dr["NO_OF_PAGES"])) ? Convert.ToInt32(dr["NO_OF_PAGES"]) : 0;
                            obj.createdOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                            obj.createdBy = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])) ? Convert.ToString(dr["CREATED_BY"]) : String.Empty;
                            obj.modifiedOn = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_ON"])) ? Convert.ToString(dr["MODIFIED_ON"]) : String.Empty;
                            obj.modifiedBy = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_BY"])) ? Convert.ToString(dr["MODIFIED_BY"]) : String.Empty;

                            objResponse.AddObject(obj);
                        }
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion

        #region "Add/Update operation"
        public TrainingResponse SaveTrainingModule(TrainingModule objTrainingModule)
        {
            try
            {
                if (objTrainingModule.lstTrainingAudioVideo != null && objTrainingModule.lstTrainingAudioVideo.Count > 0)
                {
                    objTrainingModule.trainingDocument = String.Empty;
                }
                SqlParameter[] parameter = new SqlParameter[10];
                parameter[0] = new SqlParameter("@MODE", "SAVE_TRAINING_MODULE");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@TRAINING_NAME", objTrainingModule.trainingName);
                parameter[4] = new SqlParameter("@TRAINING_FROM", ConvertDate(objTrainingModule.trainingStartDate));
                parameter[5] = new SqlParameter("@TRAINING_TO", ConvertDate(objTrainingModule.trainingEndDate));
                parameter[6] = new SqlParameter("@TRAINING_DOCUMENT", objTrainingModule.trainingDocument);
                parameter[7] = new SqlParameter("@NO_OF_PAGES", objTrainingModule.noOfPages);
                parameter[8] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[9] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[9].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (objTrainingModule.trainingId == 0)
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Record has been added successfully !";
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Record has been updated successfully !";
                }

                if (objTrainingModule.lstTrainingAudioVideo != null && objTrainingModule.lstTrainingAudioVideo.Count > 0)
                {
                    foreach (TrainingAudioVideo trainingAudioVideo in objTrainingModule.lstTrainingAudioVideo)
                    {
                        SqlParameter[] parameter1 = new SqlParameter[7];
                        parameter1[0] = new SqlParameter("@MODE", "SAVE_TRAINING_MODULE_DETL");
                        parameter1[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                        parameter1[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                        parameter1[3] = new SqlParameter("@TRAINING_ID", Convert.ToInt32(parameter[9].Value));
                        parameter1[4] = new SqlParameter("@TRAINING_DOCUMENT", trainingAudioVideo.fileName);
                        parameter1[5] = new SqlParameter("@SEQ", trainingAudioVideo.sequence);
                        parameter1[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameter1[6].Direction = ParameterDirection.Output;

                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter1);
                    }
                }
                else
                {
                    String userDir = "/InsiderTrading/emailAttachment/";
                    List<string> differentPdfFiles = SplitIntoMultiplePdf(HttpContext.Current.Server.MapPath("~" + userDir), Path.GetFileName(objTrainingModule.trainingDocument), objTrainingModule.noOfPages);
                    int count = 1;
                    foreach (String str in differentPdfFiles)
                    {
                        SqlParameter[] parameter1 = new SqlParameter[7];
                        parameter1[0] = new SqlParameter("@MODE", "SAVE_TRAINING_MODULE_DETL");
                        parameter1[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                        parameter1[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                        parameter1[3] = new SqlParameter("@TRAINING_ID", Convert.ToInt32(parameter[9].Value));
                        parameter1[4] = new SqlParameter("@TRAINING_DOCUMENT", str);
                        parameter1[5] = new SqlParameter("@SEQ", count);
                        parameter1[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                        parameter1[6].Direction = ParameterDirection.Output;

                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter1);
                        count++;
                    }
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, typeof(TrainingRepository).Name, new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name, Convert.ToString(HttpContext.Current.Session["EmployeeId"]), Convert.ToInt32(HttpContext.Current.Session["ModuleId"]));
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }

        #endregion

        #region "Edit operation"
        public TrainingResponse GetAllTrainingModulesById(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@MODE", "GET_TRAINING_MODULE_BY_ID");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[4].Direction = ParameterDirection.Output;

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        TrainingModule obj = new TrainingModule();
                        obj.trainingId = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_ID"])) ? Convert.ToInt32(dr["TRAINING_ID"]) : 0;
                        obj.trainingName = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_NM"])) ? Convert.ToString(dr["TRAINING_NM"]) : String.Empty;
                        obj.trainingStartDate = !String.IsNullOrEmpty(Convert.ToString(dr["START_DT"])) ? Convert.ToString(dr["START_DT"]) : String.Empty;
                        obj.trainingEndDate = !String.IsNullOrEmpty(Convert.ToString(dr["END_DT"])) ? Convert.ToString(dr["END_DT"]) : String.Empty;
                        obj.trainingDocument = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_DOC"])) ? Convert.ToString(dr["TRAINING_DOC"]) : String.Empty;
                        obj.noOfPages = !String.IsNullOrEmpty(Convert.ToString(dr["NO_OF_PAGES"])) ? Convert.ToInt32(dr["NO_OF_PAGES"]) : 0;
                        obj.createdOn = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_ON"])) ? Convert.ToString(dr["CREATED_ON"]) : String.Empty;
                        obj.createdBy = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])) ? Convert.ToString(dr["CREATED_BY"]) : String.Empty;
                        obj.modifiedOn = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_ON"])) ? Convert.ToString(dr["MODIFIED_ON"]) : String.Empty;
                        obj.modifiedBy = !String.IsNullOrEmpty(Convert.ToString(dr["MODIFIED_BY"])) ? Convert.ToString(dr["MODIFIED_BY"]) : String.Empty;
                        obj.lstTrainingAudioVideo = GetAllModuleDetail(objTrainingModule);

                        objResponse.TrainingModule = obj;

                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully !";
                    }
                    else
                    {
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found !";
                    }
                }
                else
                {
                    objResponse.StatusFl = true;
                    objResponse.Msg = "No data found !";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }
        #endregion

        #region "Delete Operation"
        public TrainingResponse DeleteTrainingModule(TrainingModule objTrainingModule)
        {
            try
            {
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@MODE", "DELETE_TRAINING_MODULE_BY_ID");
                parameter[1] = new SqlParameter("@COMPANY_ID", objTrainingModule.companyId);
                parameter[2] = new SqlParameter("@USER_LOGIN", objTrainingModule.userLogin);
                parameter[3] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
                parameter[4] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameter[4].Direction = ParameterDirection.Output;

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = true;
                objResponse.Msg = "Record has been deleted successfully !";
                return objResponse;
            }
            catch (Exception ex)
            {
                TrainingResponse objResponse = new TrainingResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error !";
                return objResponse;
            }

        }

        #endregion

        #region "Split into single pdf file"
        private List<string> SplitIntoMultiplePdf(String filePath, String fileName, int pageCount)
        {
            List<string> splittedPdf = new List<string>();
            try
            {
                for (int i = 0; i <= pageCount - 1; i++)
                {
                    //Load the PDF document
                    PdfLoadedDocument loadedDocument = new PdfLoadedDocument(Path.Combine(filePath, fileName));
                    //Create new PDF document
                    PdfDocument document = new PdfDocument();
                    //Import the particular page from the existing PDF
                    document.ImportPage(loadedDocument, i);

                    String name = Path.GetFileNameWithoutExtension(fileName);
                    String ext = Path.GetExtension(fileName);

                    name = name + "_" + i + "_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture) + ext;

                    splittedPdf.Add(name);

                    //Save the new PDF document
                    document.Save(Path.Combine(filePath, name));
                    //Close the PDF document
                    document.Close(true);
                    loadedDocument.Close(true);

                }

            }
            catch (Exception ex)
            {
                new LogHelper().AddExceptionLogs(ex.Message.ToString(), ex.Source, ex.StackTrace, "FilesToPDFConvertor", "ReadPageCount", "FilesToPDFConvertor Scheduler", 1);
            }
            return splittedPdf;
        }
        #endregion

        #region "Get All Module Detail"
        private List<TrainingAudioVideo> GetAllModuleDetail(TrainingModule objTrainingModule)
        {
            SqlParameter[] parameter = new SqlParameter[4];
            parameter[0] = new SqlParameter("@MODE", "GET_ALL_TRAINING_MODULES_DETL");
            parameter[1] = new SqlParameter("@TRAINING_ID", objTrainingModule.trainingId);
            parameter[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
            parameter[2].Direction = ParameterDirection.Output;

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_TRAINING_MODULE", objTrainingModule.MODULE_DATABASE, parameter);
            TrainingResponse objResponse = new TrainingResponse();
            List<TrainingAudioVideo> trainingAudioVideos = new List<TrainingAudioVideo>();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TrainingAudioVideo obj = new TrainingAudioVideo();
                        obj.fileName = !String.IsNullOrEmpty(Convert.ToString(dr["TRAINING_DOC"])) ? Convert.ToString(dr["TRAINING_DOC"]) : String.Empty;
                        obj.sequence = !String.IsNullOrEmpty(Convert.ToString(dr["SEQ"])) ? Convert.ToInt32(dr["SEQ"]) : 0;

                        trainingAudioVideos.Add(obj);
                    }
                }
            }
            return trainingAudioVideos;
        }
        #endregion

        #endregion
    }
}