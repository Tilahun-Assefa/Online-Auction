using System.Threading.Tasks;
using OnlineAuction.Models;
using OnlineAuction.Dtos.Product;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OnlineAuction.Services.ProductService
{
    public interface IProductService
    {
         Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
         Task<ServiceResponse<ProductDto>> GetProductById(int id);
         Task<ServiceResponse<GetProductDto>> PostProduct(AddProductDto newProduct);
         Task<bool> UpdateProduct(UpdateProductDto updatedProduct);
         Task<bool> DeleteProduct(int id);        
    
    }
}