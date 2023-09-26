using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class SubCategoryRequest
    {
        private Category _category;
        private SubCategory _subCategory;

        public SubCategoryRequest()
        {

        }

        public SubCategoryRequest(Category category)
        {
            _category = new Category();
            _category = category;
        }

        public SubCategoryRequest(SubCategory subCategory)
        {
            _subCategory = new SubCategory();
            _subCategory = subCategory;
        }

        public SubCategoryResponse GetSubCategoryList()
        {
            SubCategoryRepository oRepository = new SubCategoryRepository();
            return oRepository.GetSubCategoryList(_category);
        }

        public SubCategoryResponse SaveSubCategory()
        {
            SubCategoryRepository oRepository = new SubCategoryRepository();
            if (_subCategory.ID == 0)
            {
                return oRepository.SaveSubCategory(_subCategory);
            }
            else
            {

                return oRepository.UpdateSubCategory(_subCategory);
            }
        }

        public SubCategoryResponse ListSubCategoryMaster()
        {
            SubCategoryRepository oRepository = new SubCategoryRepository();
            return oRepository.GetSubCategoryMaster(_subCategory);
        }

        public SubCategoryResponse EditSubCategoryMaster()
        {
            SubCategoryRepository oRepository = new SubCategoryRepository();
            return oRepository.EditSubCategoryMaster(_subCategory);
        }

        public SubCategoryResponse deleteSubCategoryMaster()
        {
            SubCategoryRepository oRepository = new SubCategoryRepository();
            return oRepository.DeleteSubCategoryMaster(_subCategory);
        }
    }
}