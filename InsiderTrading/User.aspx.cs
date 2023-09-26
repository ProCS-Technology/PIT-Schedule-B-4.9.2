using ProcsDLL.Models.InsiderTrading.Service.Request;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Web;
using System.Web.UI.WebControls;
namespace ProcsDLL.InsiderTrading
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["BusinessUnitId"])))
            {
                txtBusinessUnitId.Value = Convert.ToString(Session["BusinessUnitId"]);
                   // Convert.ToString(HttpContext.Current.Session["BusinessUnitId"]);
            }
            if (!this.IsPostBack)
            {
                fnGetAllCategory();
            }
        }
        private void fnGetAllCategory()
        {
            try
            {
                ProcsDLL.Models.InsiderTrading.Model.Role rol = new ProcsDLL.Models.InsiderTrading.Model.Role();
                rol.CREATED_BY = Convert.ToString(HttpContext.Current.Session["EmployeeId"]);
                rol.COMPANY_ID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                rol.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);


                ProcsDLL.Models.InsiderTrading.Model.Category category = new ProcsDLL.Models.InsiderTrading.Model.Category();
                category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
                category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
                CategoryRequest categoryList = new CategoryRequest(category);
                CategoryResponse gResCategoryList = categoryList.GetCategoryList();

                if (gResCategoryList.StatusFl)
                {
                    if (gResCategoryList.CategoryList.Count > 0)
                    {

                        foreach (ProcsDLL.Models.InsiderTrading.Model.Category deptX in gResCategoryList.CategoryList)
                        {
                            ddlCategory.Items.Add(new ListItem(deptX.categoryName, deptX.ID.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}