using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class CategoryRequest
    {
        private Category _category;

        public CategoryRequest()
        {

        }

        public CategoryRequest(Category category)
        {
            _category = new Category();
            _category = category;
        }

        public CategoryResponse GetCategoryList()
        {
            CategoryRepository oRepository = new CategoryRepository();
            return oRepository.GetCategoryList(_category);
        }

        public CategoryResponse SaveCategory()
        {
            CategoryRepository oRepository = new CategoryRepository();

            if (_category.ID == 0)
            {
                return oRepository.SaveCategory(_category);
            }
            else
            {

                return oRepository.UpdateCategory(_category);
            }



        }
        public CategoryResponse editCategory()
        {
            CategoryRepository oRepository = new CategoryRepository();
            return oRepository.editCategory(_category);
        }

        public CategoryResponse deleteCategory()
        {
            CategoryRepository oRepository = new CategoryRepository();
            return oRepository.DeleteCategory(_category);
        }
    }
}