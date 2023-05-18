using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using OnlineAuction.Services.ProductService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineAuction.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<ServiceResponse<GetProductDto>>
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public float Rating { get; set; }                    
        public string[] Categories { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<GetProductDto>>
        {
            private readonly IProductService _productService;

            public CreateProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ServiceResponse<GetProductDto>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                Product product = new()
                {
                    Name = command.Title,
                    Price = command.Price,
                    Description = command.Description,
                    ImgPath = command.ImgPath,
                    ProductCategories = new List<ProductCategory>()
                };

                foreach (string cat in command.Categories)
                {
                    product.ProductCategories.Add(new ProductCategory
                    {
                        Product = product,
                        Category = new() { Name = cat }
                    });
                }

                return await _productService.CreateProduct(product);
            }
        }
    }
}
