using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using AutoMapper;
using OnlineAuction.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineAuction.Dtos.Category;

namespace OnlineAuction.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public List<ProductCategory> pc = new List<ProductCategory>();
        public ProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetProductDto>>> PostProduct(GetProductDto newProduct)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            Product product = _mapper.Map<Product>(newProduct);
            try
            {
                Product searchproduct = await _context.Products.FirstOrDefaultAsync(p => p.Title == newProduct.Title);
                if (searchproduct == null)
                {
                    await _context.Products.AddAsync(product);
                    foreach (GetCategoryDto nc in newProduct.Categories)
                    {
                        Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == nc.Name);
                        if (category == null)
                        {
                            serviceResponse.Success = false;
                            serviceResponse.Message = "Category not found.";
                            return serviceResponse;
                        }
                        ProductCategory productcategory = new ProductCategory
                        {
                            Product = product,
                            Category = category
                        };
                        pc.Add(productcategory);
                    }
                    product.ProductCategories = pc;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Products.Select(c => _mapper.Map<GetProductDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product with the same Title Already Registered.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                Product product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Products.Select(c => _mapper.Map<GetProductDto>(c))).ToList();
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

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            List<Product> dbProducts = await _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).ToListAsync();
            serviceResponse.Data = (dbProducts.Select(c => _mapper.Map<GetProductDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            Product product = await _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<GetProductDto>(product);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(int id, UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            _context.Entry(updatedProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProductExists(id))
                {
                    return serviceResponse;
                }
                else
                {
                    throw;
                }                
            }
            return serviceResponse;
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }       
    }
}