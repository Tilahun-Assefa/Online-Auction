using System.Threading.Tasks;
using OnlineAuction.Models;
using OnlineAuction.Dtos.Product;
using System.Collections.Generic;

namespace OnlineAuction.Services.ProductService
{
    public interface IProductService
    {
         Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
         Task<ServiceResponse<GetProductDto>> GetProductById(int id);
         Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
         Task<ServiceResponse<List<GetProductDto>>> UpdateProduct(UpdateProductDto updatedProduct);
         Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);        
    
    }
}