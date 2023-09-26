using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class CategoryRepository : IRequiresSessionState
    {
        public CategoryResponse GetCategoryList(Category objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_CATEGORY_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);

                CategoryResponse oCategory = new CategoryResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Category obj = new Category();
                        obj.ID = Convert.ToInt32(rdr["CATEGORY_ID"]);
                        obj.categoryName = !String.IsNullOrEmpty(rdr["CATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["CATEGORY_NAME"]) : String.Empty;
                        obj.createdOn = FormatHelper.FormatDate(rdr["CREATED_ON"].ToString());
                        obj.createdBy = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        oCategory.AddObject(obj);
                    }
                    oCategory.StatusFl = true;
                    oCategory.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oCategory.StatusFl = false;
                    oCategory.Msg = "No data found !";
                }
                rdr.Close();
                return oCategory;
            }
            catch (Exception ex)
            {
                CategoryResponse oCategory = new CategoryResponse();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }
        public CategoryResponse SaveCategory(Category objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "INSERT_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@CATEGORY_ID", objCategory.ID);
                parameters[3] = new SqlParameter("@CATEGORY_NAME", objCategory.categoryName);
                parameters[4] = new SqlParameter("@CREATED_BY", objCategory.createdBy);
                parameters[5] = new SqlParameter("@COMPANY_ID", objCategory.companyId);

                CategoryResponse oCategory = new CategoryResponse();
                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CATEGORY", objCategory.MODULE_DATABASE, parameters);
                var count = parameters[1].Value;
                Int32 set_count = Convert.ToInt32(count);
                if (set_count == 0)
                {
                    oCategory.StatusFl = false;
                    oCategory.Msg = "Category already Exists !";
                    return oCategory;
                }
                if (status > 0)
                {

                    oCategory.StatusFl = true;
                    oCategory.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oCategory.StatusFl = false;
                    oCategory.Msg = "No data found !";
                }

                return oCategory;
            }
            catch (Exception ex)
            {
                CategoryResponse oCategory = new CategoryResponse();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }
        public CategoryResponse UpdateCategory(Category objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "UPDATE_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@CATEGORY_ID", objCategory.ID);
                parameters[3] = new SqlParameter("@CATEGORY_NAME", objCategory.categoryName);
                parameters[4] = new SqlParameter("@CREATED_BY", objCategory.createdBy);
                parameters[5] = new SqlParameter("@COMPANY_ID", objCategory.companyId);

                CategoryResponse oCategory = new CategoryResponse();
                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (status > 0)
                {

                    oCategory.StatusFl = true;
                    oCategory.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oCategory.StatusFl = false;
                    oCategory.Msg = "No data found !";
                }

                return oCategory;
            }
            catch (Exception ex)
            {
                CategoryResponse oCategory = new CategoryResponse();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }
        public CategoryResponse editCategory(Category objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_CATEGORY_EDIT");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[3] = new SqlParameter("@CATEGORY_ID", objCategory.ID);

                CategoryResponse oCategory = new CategoryResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Category obj = new Category();
                        obj.ID = Convert.ToInt32(rdr["CATEGORY_ID"]);
                        obj.categoryName = !String.IsNullOrEmpty(rdr["CATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["CATEGORY_NAME"]) : String.Empty;
                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        obj.createdOn = dt.ToString("dd/MM/yyyy");
                        obj.createdBy = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        oCategory.AddObject(obj);
                    }
                    oCategory.StatusFl = true;
                    oCategory.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oCategory.StatusFl = false;
                    oCategory.Msg = "No data found !";
                }
                rdr.Close();
                return oCategory;
            }
            catch (Exception ex)
            {
                CategoryResponse oCategory = new CategoryResponse();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }
        public CategoryResponse DeleteCategory(Category objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "DELETE_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                // parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[2] = new SqlParameter("@CATEGORY_ID", objCategory.ID);

                CategoryResponse oCategory = new CategoryResponse();
                Int32 ST = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                var check = parameters[1].Value;
                int is_check = Convert.ToInt32(check);
                if (is_check == 0)
                {
                    oCategory.StatusFl = true;
                    oCategory.Msg = "Unable to Delete. This Category is Used in Higher Component!!";

                }
                else
                {
                    if (ST > 0)
                    {

                        oCategory.StatusFl = true;
                        oCategory.Msg = "Data has been Deleted successfully !";
                    }
                    else
                    {
                        oCategory.StatusFl = false;
                        oCategory.Msg = "No data found !";
                    }
                }



                return oCategory;
            }
            catch (Exception ex)
            {
                CategoryResponse oCategory = new CategoryResponse();
                oCategory.StatusFl = false;
                oCategory.Msg = "Processing failed, because of system error !";
                return oCategory;
            }
        }
    }
}