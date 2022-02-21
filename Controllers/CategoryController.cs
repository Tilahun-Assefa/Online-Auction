using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Dtos.Category;
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

        [HttpGet("GetCategories")]
        public async Task<IActionResult> Get() =>
            Ok(await _categoryService.GetAllCategories());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddCategoryDto newCategory)
        {
            return Ok(await _categoryService.AddCategory(newCategory));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateCategoryDto updatedCategory)
        {
            return Ok(await _categoryService.UpdateCategory(updatedCategory));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }

    }
}