using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
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
        private readonly DataContext _context;
        public List<ProductCategory> pc = new List<ProductCategory>();
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<GetProductDto>> PostProduct(AddProductDto newProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();

            try
            {
                Product searchproduct = await _context.Products.FirstOrDefaultAsync(p => p.Title == newProduct.Title);

                if (searchproduct == null)
                {
                    Product prd = new Product()
                    {
                        Title = newProduct.Title,
                        Price = newProduct.Price,
                        Description = newProduct.Description
                    };

                    foreach (string cat in newProduct.Categories)
                    {
                        ProductCategory productcategory = new ProductCategory
                        {
                            Product = prd,
                            Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == cat)
                        };
                        pc.Add(productcategory);
                    }

                    prd.ProductCategories = pc;

                    _context.Products.Add(prd);
                    await _context.SaveChangesAsync();
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
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            //List<Product> dbProducts = await _context.Products
            //.Include(p => p.Reviews)
            //.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).ToListAsync();

            serviceResponse.Data = await _context.Products.Select(p => new GetProductDto()
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Rating = p.Rating,
                Description = p.Description
            }).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            List<string> rev = new List<string>();
            List<string> cat = new List<string>();

            Product product = await _context.Products.Include(p => p.Reviews)
            .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

            GetProductDto getProduct = new GetProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Rating = product.Rating,
                Description = product.Description
            };

            foreach (Review r in product.Reviews)
            {
                rev.Add(r.Comment);
            }
            foreach (ProductCategory pc in product.ProductCategories)
            {
                cat.Add(pc.Category.Name);
            }

            getProduct.Reviews = rev;
            getProduct.Categories = cat;
            serviceResponse.Data = getProduct;
            return serviceResponse;
        }

        public async Task<bool> UpdateProduct(UpdateProductDto updatedProduct)
        {
            _context.Entry(updatedProduct).State = EntityState.Modified;

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
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}