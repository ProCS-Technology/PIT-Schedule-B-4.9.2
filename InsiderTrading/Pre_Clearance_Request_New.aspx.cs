using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using ProcsDLL.Models.InsiderTrading.Service.Request;
using System.Configuration;
using ProcsDLL.Models.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using ProcsDLL.Models.InsiderTrading.Model;

namespace ProcsDLL.InsiderTrading
{
    public partial class Pre_Clearance_Request_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = ConfigurationManager.AppSettings["UndertakingBeforeSubmitRequest"];
            if (!String.IsNullOrEmpty(s))
            {
                string undertakingBeforeSubmitRequest = Convert.ToString(CryptorEngine.Decrypt(ConfigurationManager.AppSettings["UndertakingBeforeSubmitRequest"], true));
                if (Convert.ToBoolean(undertakingBeforeSubmitRequest))
                {
                    enableUndertakingBeforeRequest.Value = "true";
                }
                else
                {
                    enableUndertakingBeforeRequest.Value = "false";
                }
            }
            else
            {
                enableUndertakingBeforeRequest.Value = "false";
            }

            if (!Page.IsPostBack)
            {
                fnGetAllCategory();
            }
        }

        private void fnGetAllCategory()
        {
            ProcsDLL.Models.InsiderTrading.Model.Category category = new ProcsDLL.Models.InsiderTrading.Model.Category();
            category.companyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            category.MODULE_DATABASE = Convert.ToString(HttpContext.Current.Session["ModuleDatabase"]);
            CategoryRequest categoryList = new CategoryRequest(category);
            CategoryResponse gResCategoryList = categoryList.GetCategoryList();

            if (gResCategoryList.StatusFl)
            {
                if (gResCategoryList.CategoryList.Count > 0)
                {
                    ddlCategory.Items.Add(new ListItem("", ""));
                    foreach (ProcsDLL.Models.InsiderTrading.Model.Category deptX in gResCategoryList.CategoryList)
                    {
                        ddlCategory.Items.Add(new ListItem(deptX.categoryName, deptX.ID.ToString()));
                    }
                }
            }
        }
    }
}