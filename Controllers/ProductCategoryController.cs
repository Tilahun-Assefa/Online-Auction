using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Services.ProductCategoryService;

namespace OnlineAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController: ControllerBase
    {
        private readonly IProductCategoryService _productcategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productcategoryService = productCategoryService;
        }

        [HttpGet("GetProductCategories")]
        public async Task<IActionResult> Get()
        {
            // throw new System.Exception();            
            return Ok(await _productcategoryService.GetAllProductCategories());
        }       
       
    }
}