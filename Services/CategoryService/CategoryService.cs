using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Models;
using System;

namespace OnlineAuction.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CategoryService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            ServiceResponse<List<GetCategoryDto>> serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            Category category = _mapper.Map<Category>(newCategory);
            try
            {
                Category searchCat = await _context.Categories.FirstOrDefaultAsync(c => c.Name == newCategory.Name);
                if (searchCat == null)
                {
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Category with the same name Already Registered.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(int id)
        {
            ServiceResponse<List<GetCategoryDto>> serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Products
                    .Select(c => _mapper.Map<GetCategoryDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            ServiceResponse<List<GetCategoryDto>> serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                serviceResponse.Data = (categories.Select(c => _mapper.Map<GetCategoryDto>(c))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            ServiceResponse<GetCategoryDto> serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            ServiceResponse<GetCategoryDto> serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == updatedCategory.Id);
                if (category != null)
                {
                    category.Name = updatedCategory.Name;
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
