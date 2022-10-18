using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Services.ProductService;

namespace OnlineAuction.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService){
            _productService  = productService;
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            // throw new System.Exception();            
            return Ok(await _productService.GetAllProducts());
        }        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto newProduct)
        {
            return Ok(await _productService.AddProduct(newProduct));
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updatedProduct)
        {
            return Ok(await _productService.UpdateProduct(updatedProduct));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

    }
}