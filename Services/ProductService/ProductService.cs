using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using OnlineAuction.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using OnlineAuction.Dtos.Review;

namespace OnlineAuction.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public List<ProductCategory> pc;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<GetProductDto>> PostProduct(AddProductDto newProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new();
            pc = new();

            try
            {
                Product searchproduct = await _context.Products.FirstOrDefaultAsync(p => p.Title == newProduct.Title);

                if (searchproduct == null)
                {
                    Product prd = new()
                    {
                        Title = newProduct.Title,
                        Price = newProduct.Price,
                        Description = newProduct.Description,
                        Rating = newProduct.Rating
                    };

                    foreach (string cat in newProduct.Categories)
                    {
                        ProductCategory productcategory = new()
                        {
                            Product = prd,
                            Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == cat)
                        };
                        pc.Add(productcategory);
                    }

                    prd.ProductCategories = pc;

                    _context.Products.Add(prd);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = new GetProductDto()
                    {
                        Id = prd.Id,
                        Description = prd.Description,
                        Price = prd.Price,
                        Rating = prd.Rating,
                        Title = prd.Title
                    };
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Added succesfully";
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
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new()
            {
                Data = await _context.Products.Select(p => new GetProductDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Rating = p.Rating,
                    Description = p.Description
                }).ToListAsync(),
                Success = true,
                Message = "Ok"
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<ProductDto>> GetProductById(int id)
        {
            ServiceResponse<ProductDto> serviceResponse = new();

            List<GetReviewDto> rev = new();
            List<string> cat = new();
            Product product = await _context.Products.Include(p => p.Reviews)
                        .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                        .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                serviceResponse.Success = false;
                return serviceResponse;
            }

            ProductDto productDto = new()
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Rating = product.Rating,
                Description = product.Description
            };

            foreach (Review r in product.Reviews)
            {
                rev.Add(new GetReviewDto()
                {
                    Rating = r.Rating,
                    Comment = r.Comment,
                    TimeStamp = r.TimeStamp,
                    User = r.User
                });
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

        public async Task<bool> UpdateProduct(UpdateProductDto updatedProduct)
        {
            try
            {
                if (ProductExists(updatedProduct.Id))
                {
                    var existingProduct = await _context.Products
                        .Include(p => p.ProductCategories)
                        .SingleAsync(p => p.Id == updatedProduct.Id);
                    existingProduct.Title = updatedProduct.Title;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.Rating = updatedProduct.Rating;
                    existingProduct.Description = updatedProduct.Description;

                    foreach (ProductCategory linkToRemove in existingProduct.ProductCategories)
                    {
                        _context.Remove(linkToRemove);
                    }

                    foreach (string cat in updatedProduct.Categories)
                    {
                        var exisistingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == cat);

                        existingProduct.ProductCategories.Add(new ProductCategory
                        {
                            Product = existingProduct,
                            Category = exisistingCategory
                        });
                    }

                    //_context.Entry(existingProduct).State = EntityState.Modified;
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
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}