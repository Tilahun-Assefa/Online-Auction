using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            ServiceResponse<List<GetCategoryDto>> serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                List<Category> categories = await _context.Categories.ToListAsync();
                serviceResponse.Data = categories.Select(c => new GetCategoryDto()
                {
                    Name = c.Name
                }).ToList();
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
                serviceResponse.Data = new GetCategoryDto()
                {
                    Name = category.Name
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> AddCategory(Category newCategory)
        {
            ServiceResponse<GetCategoryDto> serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                Category searchCat = await _context.Categories.FirstOrDefaultAsync(c => c.Name == newCategory.Name);
                if (searchCat == null)
                {
                    await _context.Categories.AddAsync(newCategory);
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
        
        public async Task<bool> UpdateCategory(Category updatedCategory)
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
