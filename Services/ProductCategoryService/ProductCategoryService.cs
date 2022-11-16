using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Dtos.ProductCategory;
using OnlineAuction.Models;

namespace OnlineAuction.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly DataContext _context;
        public ProductCategoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<GetProductDto>> AddProductCategory(AddProductCategoryDto newProductCategory)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();
            try
            {
                Product product = await _context.Products
                    .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                    .Include(p => p.Reviews)
                    .FirstOrDefaultAsync(p => p.Id == newProductCategory.ProductId);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == newProductCategory.CategoryId);
                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Category not found.";
                    return response;
                }
                ProductCategory productcategory = new ProductCategory
                {
                    Product = product,
                    Category = category
                };
                await _context.ProductCategories.AddAsync(productcategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetProductCategoryDto>>> GetAllProductCategories()
        {
            ServiceResponse<List<GetProductCategoryDto>> serviceResponse = new ServiceResponse<List<GetProductCategoryDto>>();
            List<ProductCategory> dbProductCategories = await _context.ProductCategories
            .Include(pc => pc.Category).ToListAsync();

            serviceResponse.Data = dbProductCategories.Select(pc => new GetProductCategoryDto()
            {
                ProductId = pc.Product.Id,
                CategoryId = pc.Category.Id
            }).ToList();
            return serviceResponse;
        }
    }
}