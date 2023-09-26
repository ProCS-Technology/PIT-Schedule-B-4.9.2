using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;


namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class UserNewRepository : IRequiresSessionState
    {
        public UserNewResponse SaveUserNew(UserNewHeader objUserNew)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[13];
                parameters[0] = new SqlParameter("@UserNew_ID ", objUserNew.id);
                //parameters[1] = new SqlParameter("@UserNew_Name", objUserNew.UserNewName);
                parameters[7] = new SqlParameter("@FILENAME_UserNew", objUserNew.fileNameUserNew);
                // parameters[1] = new SqlParameter("@AS_OF_DATE", ConvertDate(objUserNew.asOfDate));
                parameters[2] = new SqlParameter("@COMPANY_ID", objUserNew.companyId);
                //parameters[2] = new SqlParameter("@", objUserNew.companyId);
                // parameters[3] = new SqlParameter("@RESTRICTED_COMPANY_ID", objUserNew.restrictedCompany.ID);
                parameters[3] = new SqlParameter("@FILENAME", objUserNew.fileName);
                parameters[4] = new SqlParameter("@CREATED_BY", objUserNew.createdBy);
                parameters[5] = new SqlParameter("@MODE", "INSERT_IT_UserNew");
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;
               
               
          

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UserNew", objUserNew.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                UserNewResponse oUserNew = new UserNewResponse();
                
                oUserNew.StatusFl = true;
                oUserNew.Msg = "Data has been Save successfully !";
                oUserNew.UserNewHeader = objUserNew;
                //else
                //{
                //    oUserNew.StatusFl = false;
                //    oUserNew.Msg = "Processing failed, because of system error !";
                //}
                return oUserNew;
            }
            catch (Exception ex)
            {
                UserNewResponse oUserNew = new UserNewResponse();
                oUserNew.StatusFl = false;
                oUserNew.Msg = "Processing failed, because of system error !";
                return oUserNew;
            }
        }
        public UserNewResponse GetUserNewList(UserNewHeader objUserNew)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objUserNew.companyId);
                //parameters[0] = new SqlParameter("@UserNew_ID", objUserNew.UserNewHdrId);
                parameters[1] = new SqlParameter("@Mode", "GET_UserNew_List");
                parameters[2] = new SqlParameter("@CREATED_BY", objUserNew.createdBy);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                //parameters[3].Direction = ParameterDirection.Output;

                UserNewResponse oUserNew = new UserNewResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UserNew", objUserNew.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        UserNewHeader obj = new UserNewHeader();

                        obj.id = Convert.ToInt32(rdr["UserNew_ID"]);
                        obj.asOfDate = !String.IsNullOrEmpty(Convert.ToString(rdr["UPLOADED_ON"])) ? Convert.ToString(rdr["UPLOADED_ON"]) : String.Empty;
                        obj.fileName = !String.IsNullOrEmpty(Convert.ToString(rdr["FILENAME"])) ? Convert.ToString(rdr["FILENAME"]) : String.Empty;
                        obj.createdBy = !String.IsNullOrEmpty(Convert.ToString(rdr["CREATED_BY"])) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        
                        //obj.type = !String.IsNullOrEmpty(Convert.ToString(rdr["TYPE"])) ? Convert.ToString(rdr["TYPE"]) : String.Empty;

                        oUserNew.AddObject(obj);
                    }
                    oUserNew.StatusFl = true;
                    oUserNew.Msg = "Document Data has been fetched successfully !";
                }
                else
                {
                    oUserNew.StatusFl = false;
                    oUserNew.Msg = "No data found !";
                }
                rdr.Close();
                return oUserNew;
            }
            catch (Exception ex)
            {
                UserNewResponse oUserNew = new UserNewResponse();
                oUserNew.StatusFl = false;
                oUserNew.Msg = "Processing failed, because of system error !";
                return oUserNew;
            }
        }
        //public UserNewMappingResponse GetUserNewFieldMapping(UserNewHeader objUserNew)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = new SqlParameter[3];
        //        parameters[0] = new SqlParameter("@COMPANY_ID", objUserNew.companyId);
        //        parameters[1] = new SqlParameter("@Mode", "GET_IT_UserNew_MAPPING");
        //        parameters[2] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
        //        parameters[2].Direction = ParameterDirection.Output;

        //        UserNewMappingResponse oUserNew = new UserNewMappingResponse();
        //        SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_UserNew", objUserNew.MODULE_DATABASE, parameters);

        //        if (rdr.HasRows)
        //        {
        //           while (rdr.Read())
        //            {
        //                UserNewMapping obj = new UserNewMapping();
        //                obj.ExcelField = !String.IsNullOrEmpty(Convert.ToString(rdr["EXCEL_FIELD_NAME"])) ? Convert.ToString(rdr["EXCEL_FIELD_NAME"]) : String.Empty;
        //                obj.FieldType = !String.IsNullOrEmpty(Convert.ToString(rdr["EXCEL_FIELD_TYPE"])) ? Convert.ToString(rdr["EXCEL_FIELD_TYPE"]) : String.Empty;
        //                obj.TemplateType = !String.IsNullOrEmpty(Convert.ToString(rdr["TEMPLATE_TYPE"])) ? Convert.ToString(rdr["TEMPLATE_TYPE"]) : String.Empty;
        //                oUserNew.AddObject(obj);
        //            }
        //            oUserNew.StatusFl = true;
        //            oUserNew.Msg = "Success";
        //        }
        //        else
        //        {
        //            oUserNew.StatusFl = false;
        //            oUserNew.Msg = "No data found !";
        //        }
        //        rdr.Close();
        //        return oUserNew;
        //    }
        //    catch (Exception ex)
        //    {
        //        UserNewMappingResponse oUserNew = new UserNewMappingResponse();
        //        oUserNew.StatusFl = false;
        //        oUserNew.Msg = "Processing failed, because of system error !";
        //        return oUserNew;
        //    }
        //}
      


    }
}


