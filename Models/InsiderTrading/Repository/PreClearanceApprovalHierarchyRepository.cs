using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class PreClearanceApprovalHierarchyRepository
    {
        #region "Save Officer Hierarchy Order"
        public PreClearanceApprovalHierarchyResponse SaveOfficerHierarchyOrder(PreClearanceApprovalHierarchy objRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@MODE", "SET_OFFICER_HIERARCHY_ORDER");
                parameters[1] = new SqlParameter("@OFFICER_USER_LOGIN", objRequest.officerUserLogin);
                parameters[2] = new SqlParameter("@ORDER_SEQUENCE", objRequest.orderSequence);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4] = new SqlParameter("@COMPANY_ID", objRequest.companyId);
                parameters[5] = new SqlParameter("@CREATED_BY", objRequest.userLogin);

                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRECLEARANCE_APPROVAL_HIERARCHY", objRequest.MODULE_DATABASE, parameters);

                if (Convert.ToInt32(parameters[3].Value) == 1)
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Data has been added successfully!";
                    return objResponse;
                }
                else if (Convert.ToInt32(parameters[3].Value) == 0)
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Data has been updated successfully!";
                    return objResponse;
                }
                else if (Convert.ToInt32(parameters[3].Value) == 2)
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Order has been already assigned!";
                    return objResponse;
                }
                else
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Processing failed, because of system error!";
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error!";
                return objResponse;
            }
        }
        #endregion

        #region "Get All Officer Hirarchy Order"
        public PreClearanceApprovalHierarchyResponse GetAllOfficerHierarchyOrder(PreClearanceApprovalHierarchy objRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@MODE", "GET_ALL_OFFICER_HIERARCHY_ORDER");
                parameters[1] = new SqlParameter("@COMPANY_ID", objRequest.companyId);
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@CREATED_BY", objRequest.userLogin);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRECLEARANCE_APPROVAL_HIERARCHY", objRequest.MODULE_DATABASE, parameters);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<PreClearanceApprovalHierarchy> lstPrecLearanceApprovalHierarchy = new List<PreClearanceApprovalHierarchy>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PreClearanceApprovalHierarchy obj = new PreClearanceApprovalHierarchy();
                            obj.ID = !String.IsNullOrEmpty(Convert.ToString(dr["ID"])) ? Convert.ToInt32(dr["ID"]) : 0;
                            obj.officerName = !String.IsNullOrEmpty(Convert.ToString(dr["OFFICER_NAME"])) ? Convert.ToString(dr["OFFICER_NAME"]) : String.Empty;
                            obj.officerEmail = !String.IsNullOrEmpty(Convert.ToString(dr["OFFICER_EMAIL"])) ? Convert.ToString(dr["OFFICER_EMAIL"]) : String.Empty;
                            obj.officerUserLogin = !String.IsNullOrEmpty(Convert.ToString(dr["OFFICER_LOGIN"])) ? Convert.ToString(dr["OFFICER_LOGIN"]) : String.Empty;
                            obj.orderSequence = !String.IsNullOrEmpty(Convert.ToString(dr["ORDER_SEQUENCE"])) ? Convert.ToInt32(dr["ORDER_SEQUENCE"]) : 0;
                            lstPrecLearanceApprovalHierarchy.Add(obj);
                        }
                        PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                        objResponse.PreClearanceApprovalHierarchyList = lstPrecLearanceApprovalHierarchy;
                        objResponse.StatusFl = true;
                        objResponse.Msg = "Data has been fetched successfully!";
                        return objResponse;
                    }
                    else
                    {
                        PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                        objResponse.StatusFl = true;
                        objResponse.Msg = "No data found!";
                        return objResponse;
                    }

                }
                else
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Processing failed, because of system error!";
                    return objResponse;
                }

            }
            catch (Exception ex)
            {
                PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error!";
                return objResponse;
            }
        }
        #endregion

        #region "Delete Officer Hierarchy Order"
        public PreClearanceApprovalHierarchyResponse DeleteOfficerHierarchyOrder(PreClearanceApprovalHierarchy objRequest)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@MODE", "REMOVE_OFFICER_HIERARCHY_ORDER");
                parameters[1] = new SqlParameter("@ID", objRequest.ID);
                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3] = new SqlParameter("@COMPANY_ID", objRequest.companyId);
                parameters[4] = new SqlParameter("@CREATED_BY", objRequest.userLogin);

                SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_PRECLEARANCE_APPROVAL_HIERARCHY", objRequest.MODULE_DATABASE, parameters);

                if (Convert.ToInt32(parameters[2].Value) == 1)
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = true;
                    objResponse.Msg = "Data has been deleted successfully!";
                    return objResponse;
                }
                else
                {
                    PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Processing failed, because of system error!";
                    return objResponse;
                }

            }
            catch (Exception ex)
            {
                PreClearanceApprovalHierarchyResponse objResponse = new PreClearanceApprovalHierarchyResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = "Processing failed, because of system error!";
                return objResponse;
            }
        }
        #endregion
    }
}