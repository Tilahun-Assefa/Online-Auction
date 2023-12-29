using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.ProductCategoryDtos;
using OnlineAuction.Models;

namespace OnlineAuction.Services.ProductCategoryService
{
    public interface IProductCategoryService
    {
         Task<ServiceResponse<List<GetProductCategoryDto>>> GetAllProductCategories();
    }
}