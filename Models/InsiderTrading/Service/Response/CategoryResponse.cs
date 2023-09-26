using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class CategoryResponse : BaseResponse
    {
        private Category _category;
        private List<Category> lstCategory;
        public Category Category
        {
            set
            {
                _category = value;
            }
            get
            {
                return _category;
            }
        }
        public List<Category> CategoryList
        {
            set
            {
                lstCategory = value;
            }
            get
            {
                return lstCategory;
            }
        }
        public void AddObject(Category o)
        {
            if (lstCategory == null)
            {
                lstCategory = new List<Category>();
            }
            lstCategory.Add(o);
        }
    }
}