using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class SubCategoryRepository : IRequiresSessionState
    {
        public SubCategoryResponse GetSubCategoryList(Category objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "GET_SUB_CATEGORY_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[3] = new SqlParameter("@CATEGORY_ID", objCategory.ID);

                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SUB_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        SubCategory obj = new SubCategory();
                        obj.ID = Convert.ToInt32(rdr["SUBCATEGORY_ID"]);
                        obj.subCategoryName = !String.IsNullOrEmpty(rdr["SUBCATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["SUBCATEGORY_NAME"]) : String.Empty;
                        oSubCategory.AddObject(obj);
                    }
                    oSubCategory.StatusFl = true;
                    oSubCategory.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "No data found !";
                }
                rdr.Close();
                return oSubCategory;
            }
            catch (Exception ex)
            {
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                oSubCategory.StatusFl = false;
                oSubCategory.Msg = "Processing failed, because of system error !";
                return oSubCategory;
            }
        }
        public SubCategoryResponse SaveSubCategory(SubCategory objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@Mode", "INSERT_SUB_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[3] = new SqlParameter("@CATEGORY_ID", objCategory.category.ID);
                parameters[4] = new SqlParameter("@SUBCATEGORY_ID", objCategory.ID);
                parameters[5] = new SqlParameter("@SUBCATEGORY_NAME", objCategory.subCategoryName);
                parameters[6] = new SqlParameter("@CREATED_BY", objCategory.createdBy);
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                int st = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SUB_CATEGORY", objCategory.MODULE_DATABASE, parameters);
                var counbt_s = parameters[1].Value;
                Int32 count_status = Convert.ToInt32(counbt_s);
                if (count_status == 0)
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "Sub Category already Exist !";
                    return oSubCategory;
                }


                if (st > 0 && count_status > 0)
                {

                    oSubCategory.StatusFl = true;
                    oSubCategory.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "No data found !";
                }

                return oSubCategory;
            }
            catch (Exception ex)
            {
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                oSubCategory.StatusFl = false;
                oSubCategory.Msg = "Processing failed, because of system error !";
                return oSubCategory;
            }
        }
        public SubCategoryResponse UpdateSubCategory(SubCategory objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@Mode", "UPDATE_SUB_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[3] = new SqlParameter("@CATEGORY_ID", objCategory.category.ID);
                parameters[4] = new SqlParameter("@SUBCATEGORY_ID", objCategory.ID);
                parameters[5] = new SqlParameter("@SUBCATEGORY_NAME", objCategory.subCategoryName);
                parameters[6] = new SqlParameter("@CREATED_BY", objCategory.subCategoryName);
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                int st = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SUB_CATEGORY", objCategory.MODULE_DATABASE, parameters);
                var counbt_s = parameters[1].Value;
                Int32 count_status = Convert.ToInt32(counbt_s);


                if (st > 0)
                {

                    oSubCategory.StatusFl = true;
                    oSubCategory.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "No data found !";
                }

                return oSubCategory;
            }
            catch (Exception ex)
            {
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                oSubCategory.StatusFl = false;
                oSubCategory.Msg = "Processing failed, because of system error !";
                return oSubCategory;
            }
        }
        public SubCategoryResponse GetSubCategoryMaster(SubCategory objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "LIST_SUB_CATEGORY_MASTER");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);


                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SUB_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        SubCategory obj = new SubCategory();
                        obj.ID = Convert.ToInt32(rdr["SUBCATEGORY_ID"]);
                        obj.subCategoryName = !String.IsNullOrEmpty(rdr["SUBCATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["SUBCATEGORY_NAME"]) : String.Empty;
                        obj.category = new Category
                        {
                            ID = Convert.ToInt32(rdr["CATEGORY_ID"]),
                            categoryName = !String.IsNullOrEmpty(rdr["CATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["CATEGORY_NAME"]) : String.Empty



                        };

                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        string dd = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dd;
                        obj.createdBy = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;


                        oSubCategory.AddObject(obj);
                    }
                    oSubCategory.StatusFl = true;
                    oSubCategory.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "No data found !";
                }
                rdr.Close();
                return oSubCategory;
            }
            catch (Exception ex)
            {
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                oSubCategory.StatusFl = false;
                oSubCategory.Msg = "Processing failed, because of system error !";
                return oSubCategory;
            }
        }
        public SubCategoryResponse EditSubCategoryMaster(SubCategory objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "EDIT_SUB_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[3] = new SqlParameter("@SUBCATEGORY_ID", objCategory.ID);


                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SUB_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        SubCategory obj = new SubCategory();
                        obj.ID = Convert.ToInt32(rdr["SUBCATEGORY_ID"]);
                        obj.subCategoryName = !String.IsNullOrEmpty(rdr["SUBCATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["SUBCATEGORY_NAME"]) : String.Empty;
                        obj.category = new Category
                        {
                            ID = Convert.ToInt32(rdr["CATEGORY_ID"]),
                            categoryName = !String.IsNullOrEmpty(rdr["CATEGORY_NAME"].ToString()) ? Convert.ToString(rdr["CATEGORY_NAME"]) : String.Empty



                        };

                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        string dd = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dd;
                        obj.createdBy = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;


                        oSubCategory.AddObject(obj);
                    }
                    oSubCategory.StatusFl = true;
                    oSubCategory.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "No data found !";
                }
                rdr.Close();
                return oSubCategory;
            }
            catch (Exception ex)
            {
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                oSubCategory.StatusFl = false;
                oSubCategory.Msg = "Processing failed, because of system error !";
                return oSubCategory;
            }
        }
        public SubCategoryResponse DeleteSubCategoryMaster(SubCategory objCategory)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "DELETE_SUB_CATEGORY");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objCategory.companyId);
                parameters[3] = new SqlParameter("@SUBCATEGORY_ID", objCategory.ID);


                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                Int32 rdr = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_SUB_CATEGORY", objCategory.MODULE_DATABASE, parameters);

                if (rdr > 0)
                {
                    oSubCategory.StatusFl = true;
                    oSubCategory.Msg = "Data has been Deleted successfully !";
                }
                else
                {
                    oSubCategory.StatusFl = false;
                    oSubCategory.Msg = "No data Deleted !";
                }

                return oSubCategory;
            }
            catch (Exception ex)
            {
                SubCategoryResponse oSubCategory = new SubCategoryResponse();
                oSubCategory.StatusFl = false;
                oSubCategory.Msg = "Processing failed, because of system error !";
                return oSubCategory;
            }
        }
    }
}