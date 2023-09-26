using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class SubCategoryResponse : BaseResponse
    {
        private SubCategory _subCategory;
        private List<SubCategory> lstSubCategory;
        public SubCategory SubCategory
        {
            set
            {
                _subCategory = value;
            }
            get
            {
                return _subCategory;
            }
        }
        public List<SubCategory> SubCategoryList
        {
            set
            {
                lstSubCategory = value;
            }
            get
            {
                return lstSubCategory;
            }
        }
        public void AddObject(SubCategory o)
        {
            if (lstSubCategory == null)
            {
                lstSubCategory = new List<SubCategory>();
            }
            lstSubCategory.Add(o);
        }
    }
}