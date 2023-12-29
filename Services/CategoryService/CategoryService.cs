using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.CategoryDtos;
using OnlineAuction.Models;
using System;
using AutoMapper;

namespace OnlineAuction.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategories()
        {
            ServiceResponse<List<CategoryDto>> serviceResponse = new ServiceResponse<List<CategoryDto>>();
            try
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                serviceResponse.Data = categories.Select(c => _mapper.Map<CategoryDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategoryById(int id)
        {
            ServiceResponse<CategoryDto> serviceResponse = new ServiceResponse<CategoryDto>();
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                serviceResponse.Data = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<CategoryDto>> AddCategory(CategoryDto newCategory)
        {
            ServiceResponse<CategoryDto> serviceResponse = new ServiceResponse<CategoryDto>();
            try
            {
                Category searchCat = await _context.Categories.FirstOrDefaultAsync(c => c.Name == newCategory.Name);
                if (searchCat == null)
                {
                    await _context.Categories.AddAsync(_mapper.Map<Category>(newCategory) );
                    await _context.SaveChangesAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Category with the same name is Already Registered.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        
        public async Task<bool> UpdateCategory(UpdateCategoryDto updatedCategory)
        {
            _context.Entry(updatedCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return false;

            }
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
