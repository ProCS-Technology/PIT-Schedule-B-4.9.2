using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/SubCategory")]
    public class SubCategoryController : ApiController
    {
        [Route("GetSubCategoryList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Sub-Category APIs" })]
        public SubCategoryResponse GetSubCategoryList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                Category category = new JavaScriptSerializer().Deserialize<Category>(input);
                category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);

                if (!category.ValidateInput())
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SubCategoryRequest categoryList = new SubCategoryRequest(category);
                SubCategoryResponse gResCategoryList = categoryList.GetSubCategoryList();
                return gResCategoryList;
            }
            catch (Exception ex)
            {
                SubCategoryResponse objResponse = new SubCategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("SaveSubCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Sub-Category APIs" })]
        public SubCategoryResponse SaveSubCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SubCategory subCategory = new JavaScriptSerializer().Deserialize<SubCategory>(input);
                subCategory.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                subCategory.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                subCategory.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!subCategory.ValidateInput())
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SubCategoryRequest categoryList = new SubCategoryRequest(subCategory);
                SubCategoryResponse gResCategoryList = categoryList.SaveSubCategory();
                return gResCategoryList;
            }
            catch (Exception ex)
            {
                SubCategoryResponse objResponse = new SubCategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("ListMasterSubCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Sub-Category APIs" })]
        public SubCategoryResponse ListMasterSubCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SubCategory subCategory = new JavaScriptSerializer().Deserialize<SubCategory>(input);
                subCategory.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                subCategory.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                subCategory.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!subCategory.ValidateInput())
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SubCategoryRequest subcategoryList = new SubCategoryRequest(subCategory);
                SubCategoryResponse subCategoryList = subcategoryList.ListSubCategoryMaster();
                return subCategoryList;
            }
            catch (Exception ex)
            {
                SubCategoryResponse objResponse = new SubCategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("EditSubCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Sub-Category APIs" })]
        public SubCategoryResponse EditSubCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SubCategory subCategory = new JavaScriptSerializer().Deserialize<SubCategory>(input);
                subCategory.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                subCategory.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                subCategory.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!subCategory.ValidateInput())
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SubCategoryRequest subcategoryList = new SubCategoryRequest(subCategory);
                SubCategoryResponse subCategoryList = subcategoryList.EditSubCategoryMaster();
                return subCategoryList;
            }
            catch (Exception ex)
            {
                SubCategoryResponse objResponse = new SubCategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }

        [Route("DeleteSubCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Sub-Category APIs" })]
        public SubCategoryResponse DeleteSubCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "SessionExpired";
                    return objResponse;
                }
                string input;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
                {
                    input = sr.ReadToEnd();
                }
                SubCategory subCategory = new JavaScriptSerializer().Deserialize<SubCategory>(input);
                subCategory.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                subCategory.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                subCategory.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                if (!subCategory.ValidateInput())
                {
                    SubCategoryResponse objResponse = new SubCategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = "Invalid Input Format";
                    return objResponse;
                }
                SubCategoryRequest subcategoryList = new SubCategoryRequest(subCategory);
                SubCategoryResponse subCategoryList = subcategoryList.deleteSubCategoryMaster();
                return subCategoryList;
            }
            catch (Exception ex)
            {
                SubCategoryResponse objResponse = new SubCategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}
