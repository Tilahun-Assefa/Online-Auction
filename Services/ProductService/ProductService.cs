using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using OnlineAuction.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using OnlineAuction.Dtos.Review;
using AutoMapper;
using OnlineAuction.Dtos.ProductCategory;

namespace OnlineAuction.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //return all products from database
        public async Task<ServiceResponse<IEnumerable<GetProductDto>>> GetProductsList()
        {
            ServiceResponse<IEnumerable<GetProductDto>> serviceResponse = new()
            {
                Data = await _context.Products.Select(p => _mapper.Map<GetProductDto>(p)).ToListAsync(),
                Success = true,
                Message = "Ok"
            };
            return serviceResponse;
        }

        //return single product from database 
        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductDto> serviceResponse = new();
            List<ReviewDto> rev = new();
            List<string> cat = new();
            Product product = await _context.Products.Include(p => p.Reviews)
                        .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                        .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found";
                return serviceResponse;
            }

            GetProductDto productDto = _mapper.Map<GetProductDto>(product);

            foreach (Review r in product.Reviews)
            {
                rev.Add(_mapper.Map<ReviewDto>(r));
            }
            foreach (ProductCategory pc in product.ProductCategories)
            {
                cat.Add(pc.Category.Name);
            }

            productDto.Reviews = rev;
            productDto.Categories = cat;

            serviceResponse.Data = productDto;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> CreateProduct(Product newProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new();

            try
            {
                Product searchproduct = await _context.Products.FirstOrDefaultAsync(p => p.Name == newProduct.Name);

                if (searchproduct == null)
                {     
                    _context.Products.Add(newProduct);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetProductDto>(newProduct);
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Added succesfully";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product with the same title already registered.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }        
        
        public async Task<bool> UpdateProduct(UpdateProductDto updatedProduct)
        {
            try
            {
                if (ProductExists(updatedProduct.Id))
                {
                    var existingProduct = await _context.Products
                        .Include(p => p.ProductCategories)
                        .SingleAsync(p => p.Id == updatedProduct.Id);
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.Description = updatedProduct.Description;
                    existingProduct.ImgPath= updatedProduct.ImgPath;

                    foreach (ProductCategory linkToRemove in existingProduct.ProductCategories)
                    {
                        _context.Remove(linkToRemove);
                    }

                    foreach (string cat in updatedProduct.Categories)
                    {
                        existingProduct.ProductCategories.Add(new ProductCategory
                        {
                            Product = existingProduct,
                            Category = new() { Name= cat }
                        });
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}