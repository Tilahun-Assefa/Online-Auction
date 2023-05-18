using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Dtos.ProductCategory;
using OnlineAuction.Models;

namespace OnlineAuction.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductCategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        

        public async Task<ServiceResponse<List<GetProductCategoryDto>>> GetAllProductCategories()
        {
            ServiceResponse<List<GetProductCategoryDto>> serviceResponse = new ServiceResponse<List<GetProductCategoryDto>>();
            List<ProductCategory> dbProductCategories = await _context.ProductCategories.Include(pc=> pc.Category).Include(pc=> pc.Product).ToListAsync();

            serviceResponse.Data = dbProductCategories.Select(pc => _mapper.Map<GetProductCategoryDto>(pc)).ToList();
            return serviceResponse;
        }
    }
}