using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Services.ProductService;

namespace OnlineAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetProducts()
        {
            // throw new System.Exception();            
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(GetProductDto newProduct)
        {           
            try
            {
                await _productService.PostProduct(newProduct);
                return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex )
            {
                throw;
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest();
            }
            try
            {
                await _productService.UpdateProduct(id, updatedProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

    }
}