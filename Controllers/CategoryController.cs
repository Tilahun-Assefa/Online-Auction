using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Models;
using OnlineAuction.Services.CategoryService;

namespace OnlineAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _categoryService.GetAllCategories());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto newCategory)
        {
            return Ok(await _categoryService.AddCategory(newCategory));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            return Ok(await _categoryService.UpdateCategory(updatedCategory));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }

    }
}