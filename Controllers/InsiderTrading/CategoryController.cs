using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Configuration;
namespace ProcsDLL.Controllers.InsiderTrading
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        string sXSSErrMsg = Convert.ToString(ConfigurationManager.AppSettings["XSSErrMsg"]);
        [Route("GetCategoryList")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Category APIs" })]
        public CategoryResponse GetCategoryList()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CategoryResponse objResponse = new CategoryResponse();
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
                    CategoryResponse objResponse = new CategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }

                CategoryRequest categoryList = new CategoryRequest(category);
                CategoryResponse gResCategoryList = categoryList.GetCategoryList();
                return gResCategoryList;
            }
            catch (Exception ex)
            {
                CategoryResponse objResponse = new CategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("SaveCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Category APIs" })]
        public CategoryResponse SaveCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CategoryResponse objResponse = new CategoryResponse();
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
                category.createdBy = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);

                if (!category.ValidateInput())
                {
                    CategoryResponse objResponse = new CategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                CategoryRequest categoryList = new CategoryRequest(category);
                CategoryResponse gResCategoryList = categoryList.SaveCategory();
                return gResCategoryList;
            }
            catch (Exception ex)
            {
                CategoryResponse objResponse = new CategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("EditCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Category APIs" })]
        public CategoryResponse EditCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CategoryResponse objResponse = new CategoryResponse();
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
                    CategoryResponse objResponse = new CategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                CategoryRequest categoryList = new CategoryRequest(category);
                CategoryResponse gResCategoryList = categoryList.editCategory();
                return gResCategoryList;
            }
            catch (Exception ex)
            {
                CategoryResponse objResponse = new CategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
        [Route("DeleteCategory")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Category APIs" })]
        public CategoryResponse DeleteCategory()
        {
            try
            {
                if (HttpContext.Current.Session.Count == 0)
                {
                    CategoryResponse objResponse = new CategoryResponse();
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
                    CategoryResponse objResponse = new CategoryResponse();
                    objResponse.StatusFl = false;
                    objResponse.Msg = sXSSErrMsg;//"Invalid Input Format";
                    return objResponse;
                }
                CategoryRequest categoryList = new CategoryRequest(category);
                CategoryResponse gResCategoryList = categoryList.deleteCategory();
                return gResCategoryList;
            }
            catch (Exception ex)
            {
                CategoryResponse objResponse = new CategoryResponse();
                objResponse.StatusFl = false;
                objResponse.Msg = ex.Message;
                return objResponse;
            }
        }
    }
}