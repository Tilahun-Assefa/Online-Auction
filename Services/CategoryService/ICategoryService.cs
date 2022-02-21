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
         Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newReview);
         Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedReview);
         Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id); 
    }
}