using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Models;

namespace OnlineAuction.Services.CategoryService
{
    public interface ICategoryService
    {
         Task<ServiceResponse<List<CategoryDto>>> GetAllCategories();
         Task<ServiceResponse<CategoryDto>> GetCategoryById(int id);
         Task<ServiceResponse<CategoryDto>> AddCategory(CategoryDto newCategory);
         Task<bool> UpdateCategory(UpdateCategoryDto updatedCategory);
         Task<bool> DeleteCategory(int id); 
    }
}