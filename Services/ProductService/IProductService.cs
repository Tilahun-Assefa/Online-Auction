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
         Task<ServiceResponse<GetProductDto>> GetProductById(int id);
         Task<ServiceResponse<List<GetProductDto>>> PostProduct(GetProductDto newProduct);
         Task<ServiceResponse<GetProductDto>> UpdateProduct(int id, UpdateProductDto updatedProduct);
         Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);        
    
    }
}