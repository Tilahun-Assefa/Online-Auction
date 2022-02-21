using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Dtos.ProductCategory;
using OnlineAuction.Models;

namespace OnlineAuction.Services.ProductCategoryService
{
    public interface IProductCategoryService
    {
         Task<ServiceResponse<GetProductDto>> AddProductCategory(AddProductCategoryDto newProductCategory);
         Task<ServiceResponse<List<GetProductCategoryDto>>> GetAllProductCategories();
    }
}