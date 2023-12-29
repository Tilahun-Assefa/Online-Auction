using System.Threading.Tasks;
using OnlineAuction.Models;
using OnlineAuction.Dtos.ProductDtos;
using System.Collections.Generic;

namespace OnlineAuction.Services.ProductService
{
    public interface IProductService
    {
         Task<ServiceResponse<IEnumerable<GetProductDto>>> GetProductsList();
         Task<ServiceResponse<GetProductDto>> GetProductById(int id);
         Task<ServiceResponse<GetProductDto>> CreateProduct(Product newProduct);
         Task<bool> UpdateProduct(UpdateProductDto updatedProduct);
         Task<bool> DeleteProduct(int id);        
    
    }
}