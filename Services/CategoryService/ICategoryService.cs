using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Models;

namespace OnlineAuction.Services.CategoryService
{
    public interface ICategoryService
    {
         Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
         Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id);
         Task<ServiceResponse<GetCategoryDto>> AddCategory(Category newCategory);
         Task<bool> UpdateCategory(Category updatedCategory);
         Task<bool> DeleteCategory(int id); 
    }
}