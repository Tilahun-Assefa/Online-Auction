using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using OnlineAuction.Services.ProductService;

namespace OnlineAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            // return list of products            
            return Ok(await _productService.GetAllProducts());
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var serviceResponse = await _productService.GetProductById(id);

            if (serviceResponse.Success == false)
            {
                return NotFound();
            }
            return Ok(serviceResponse);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest();
            }

            var check = await _productService.UpdateProduct(updatedProduct);

            if (!check)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> PostProduct(AddProductDto newProduct)
        {
            try
            {
                var serviceResponse = await _productService.PostProduct(newProduct);
                return CreatedAtAction("GetProduct", new { id = serviceResponse.Data.Id }, serviceResponse.Data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var check = await _productService.DeleteProduct(id);
            if (!check)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}