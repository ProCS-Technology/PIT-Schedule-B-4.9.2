using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class ApproverSetupRepository : IRequiresSessionState
    {

        public ApproverResponseSetUp AddApproverSetUp(ApproverSetUp objApproverSetUp)
        {
            try
            {
                ApproverResponseSetUp oApproverSetUp = new ApproverResponseSetUp();

                SqlParameter[] parameters1 = new SqlParameter[8];

                parameters1[0] = new SqlParameter("@USER_LOGIN", objApproverSetUp.USER_LOGIN);

                parameters1[1] = new SqlParameter("@MIN_LIMIT", objApproverSetUp.MIN_LIMIT);
                parameters1[2] = new SqlParameter("@MAX_LIMIT", objApproverSetUp.MAX_LIMIT);

                parameters1[3] = new SqlParameter("@MODE", "CHECK");
                parameters1[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters1[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[5].Direction = ParameterDirection.Output;
                parameters1[6] = new SqlParameter("@CREATED_BY", objApproverSetUp.CREATED_BY);
                parameters1[7] = new SqlParameter("@COMPANY_ID", objApproverSetUp.COMPANY_ID);


                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters1);
                var count = parameters1[5].Value;
                Int32 set_count = Convert.ToInt32(count);



                if (set_count == 0)
                {
                    SqlParameter[] parameters = new SqlParameter[8];

                    parameters[0] = new SqlParameter("@USER_LOGIN", objApproverSetUp.USER_LOGIN);

                    parameters[1] = new SqlParameter("@MIN_LIMIT", objApproverSetUp.MIN_LIMIT);
                    parameters[2] = new SqlParameter("@MAX_LIMIT", objApproverSetUp.MAX_LIMIT);

                    parameters[3] = new SqlParameter("@MODE", "INSERT_UPDATE");
                    parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                    parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[5].Direction = ParameterDirection.Output;
                    parameters[6] = new SqlParameter("@CREATED_BY", objApproverSetUp.CREATED_BY);
                    parameters[7] = new SqlParameter("@COMPANY_ID", objApproverSetUp.COMPANY_ID);


                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters);
                    oApproverSetUp.StatusFl = true;
                    oApproverSetUp.Msg = "Data has been Saved successfully !";
                    oApproverSetUp.approverSetUp = objApproverSetUp;

                }
                else
                {
                    oApproverSetUp.StatusFl = false;
                    oApproverSetUp.Msg = "Limit already exists !";
                }

                return oApproverSetUp;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp oApproverSetUpResponse = new ApproverResponseSetUp();
                oApproverSetUpResponse.StatusFl = false;
                oApproverSetUpResponse.Msg = "Processing failed, because of system error !";
                return oApproverSetUpResponse;
            }
        }

        public ApproverResponseSetUp UpdateApproverSetUp(ApproverSetUp objApproverSetUp)
        {
            try
            {
                SqlParameter[] parameters1 = new SqlParameter[9];
                parameters1[0] = new SqlParameter("@USER_LOGIN", objApproverSetUp.USER_LOGIN);
                parameters1[1] = new SqlParameter("@MIN_LIMIT", objApproverSetUp.MIN_LIMIT);
                parameters1[2] = new SqlParameter("@MAX_LIMIT", objApproverSetUp.MAX_LIMIT);
                parameters1[3] = new SqlParameter("@MODE", "CHECK");
                parameters1[4] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters1[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters1[5].Direction = ParameterDirection.Output;
                parameters1[6] = new SqlParameter("@MODIFIED_BY", objApproverSetUp.CREATED_BY);
                parameters1[7] = new SqlParameter("@COMPANY_ID", objApproverSetUp.COMPANY_ID);
                parameters1[8] = new SqlParameter("@WF_ID", objApproverSetUp.WF_ID);

                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters1);
                var count = parameters1[5].Value;
                Int32 set_count = Convert.ToInt32(count);

                ApproverResponseSetUp oApproverSetUp = new ApproverResponseSetUp();

                if (set_count == 0)
                {
                    SqlParameter[] parameters = new SqlParameter[9];

                    parameters[0] = new SqlParameter("@USER_LOGIN", objApproverSetUp.USER_LOGIN);

                    parameters[1] = new SqlParameter("@MIN_LIMIT", objApproverSetUp.MIN_LIMIT);
                    parameters[2] = new SqlParameter("@MAX_LIMIT", objApproverSetUp.MAX_LIMIT);

                    parameters[3] = new SqlParameter("@MODE", "INSERT_UPDATE");
                    parameters[4] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                    parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                    parameters[5].Direction = ParameterDirection.Output;
                    parameters[6] = new SqlParameter("@MODIFIED_BY", objApproverSetUp.CREATED_BY);
                    parameters[7] = new SqlParameter("@COMPANY_ID", objApproverSetUp.COMPANY_ID);
                    parameters[8] = new SqlParameter("@WF_ID", objApproverSetUp.WF_ID);

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters);
                    oApproverSetUp.StatusFl = true;
                    oApproverSetUp.Msg = "Data has been Updated successfully !";
                }
                else
                {
                    oApproverSetUp.StatusFl = false;
                    oApproverSetUp.Msg = "Limit already exists ! ";
                }

                return oApproverSetUp;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp oApproverSetUpResponse = new ApproverResponseSetUp();
                oApproverSetUpResponse.StatusFl = false;
                oApproverSetUpResponse.Msg = "Processing failed, because of system error !";
                return oApproverSetUpResponse;
            }
        }

        public ApproverResponseSetUp GetApproverSetUpLIST(ApproverSetUp objApproverSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@COMPANY_ID", objApproverSetUp.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_SP_PROCS_INSIDER_APPROVAL_MSTR_LIST");

                parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[2].Direction = ParameterDirection.Output;

                ApproverResponseSetUp oGrp = new ApproverResponseSetUp();
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ApproverSetUp obj = new ApproverSetUp();
                            obj.WF_ID = !String.IsNullOrEmpty(Convert.ToString(dr["WF_ID"])) ? Convert.ToInt32(dr["WF_ID"]) : 0;
                            obj.USER_LOGIN = !String.IsNullOrEmpty(Convert.ToString(dr["USER_LOGIN"])) ? Convert.ToString(dr["USER_LOGIN"]) : String.Empty;
                            obj.MIN_LIMIT = !String.IsNullOrEmpty(Convert.ToString(dr["MIN_LIMIT"])) ? Convert.ToInt32(dr["MIN_LIMIT"]) : 0;
                            obj.MAX_LIMIT = !String.IsNullOrEmpty(Convert.ToString(dr["MAX_LIMIT"])) ? Convert.ToInt32(dr["MAX_LIMIT"]) : 0;
                            obj.CREATED_BY = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])) ? Convert.ToString(dr["CREATED_BY"]) : String.Empty;
                            obj.COMPANY_ID = !String.IsNullOrEmpty(Convert.ToString(dr["COMPANY_ID"])) ? Convert.ToInt32(dr["COMPANY_ID"]) : 0;

                            oGrp.AddObject(obj);
                        }

                    }
                    oGrp.StatusFl = true;
                    oGrp.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oGrp.StatusFl = false;
                    oGrp.Msg = "No data found !";
                }
                return oGrp;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp oGrp = new ApproverResponseSetUp();
                oGrp.StatusFl = false;
                oGrp.Msg = "Processing failed, because of system error !";
                return oGrp;
            }
        }

        public ApproverResponseSetUp GetApproverSetUpById(ApproverSetUp objApproverSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objApproverSetUp.COMPANY_ID);
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@MODE", "GET_SP_PROCS_INSIDER_APPROVAL_MSTR_BY_ID");
                parameters[3] = new SqlParameter("@WF_ID", objApproverSetUp.WF_ID);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters);
                ApproverResponseSetUp objApproverSetUpResponse = new ApproverResponseSetUp();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];


                    ApproverSetUp obj = new ApproverSetUp();
                    obj.WF_ID = !String.IsNullOrEmpty(Convert.ToString(dr["WF_ID"])) ? Convert.ToInt32(dr["WF_ID"]) : 0;
                    obj.USER_LOGIN = !String.IsNullOrEmpty(Convert.ToString(dr["USER_LOGIN"])) ? Convert.ToString(dr["USER_LOGIN"]) : String.Empty;
                    obj.MIN_LIMIT = !String.IsNullOrEmpty(Convert.ToString(dr["MIN_LIMIT"])) ? Convert.ToInt32(dr["MIN_LIMIT"]) : 0;
                    obj.MAX_LIMIT = !String.IsNullOrEmpty(Convert.ToString(dr["MAX_LIMIT"])) ? Convert.ToInt32(dr["MAX_LIMIT"]) : 0;
                    obj.CREATED_BY = !String.IsNullOrEmpty(Convert.ToString(dr["CREATED_BY"])) ? Convert.ToString(dr["CREATED_BY"]) : String.Empty;
                    obj.COMPANY_ID = !String.IsNullOrEmpty(Convert.ToString(dr["COMPANY_ID"])) ? Convert.ToInt32(dr["COMPANY_ID"]) : 0;

                    objApproverSetUpResponse.StatusFl = true;
                    objApproverSetUpResponse.Msg = "Data has been fetched successfully !";
                    objApproverSetUpResponse.approverSetUp = obj;
                }
                else
                {
                    objApproverSetUpResponse.StatusFl = false;
                    objApproverSetUpResponse.Msg = "No data found !";
                }
                return objApproverSetUpResponse;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp objRelativeResponse = new ApproverResponseSetUp();
                objRelativeResponse.StatusFl = false;
                objRelativeResponse.Msg = "Processing failed, because of system error !";
                return objRelativeResponse;
            }
        }

        public ApproverResponseSetUp DeleteApproverSetUp(ApproverSetUp objApproverSetUp)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "DELETE_SP_PROCS_INSIDER_APPROVAL_MSTR");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;

                parameters[2] = new SqlParameter("@WF_ID", objApproverSetUp.WF_ID);

                ApproverResponseSetUp approverResponseSetUp = new ApproverResponseSetUp();
                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_APPROVAL_MSTR", objApproverSetUp.MODULE_DATABASE, parameters);

                var check = parameters[1].Value;
                int is_check = Convert.ToInt32(check);
                if (is_check > 0)
                {
                    approverResponseSetUp.StatusFl = true;
                    approverResponseSetUp.Msg = "Data has been Deleted successfully !";

                }
                else
                {
                    approverResponseSetUp.StatusFl = false;
                    approverResponseSetUp.Msg = "Record not Found !";
                }



                return approverResponseSetUp;
            }
            catch (Exception ex)
            {
                ApproverResponseSetUp approverResponseSetUp = new ApproverResponseSetUp();
                approverResponseSetUp.StatusFl = false;
                approverResponseSetUp.Msg = "Processing failed, because of system error !";
                return approverResponseSetUp;
            }
        }
    }
}