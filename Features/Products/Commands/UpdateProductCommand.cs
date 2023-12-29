using MediatR;
using OnlineAuction.Dtos.ProductDtos;
using OnlineAuction.Services.ProductService;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineAuction.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public float Rating { get; set; }
        public string[] Categories { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
        {
            private readonly IProductService _productService;

            public UpdateProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var serviceResponse = await _productService.GetProductById(command.Id);
                var product = serviceResponse.Data;
                if (product == null)
                    return default;
                UpdateProductDto dto = new()
                {
                    Id = command.Id,
                    Name = command.Title,
                    Price = command.Price,
                    Description = command.Description,
                    ImgPath = command.ImgPath,
                    Categories = command.Categories
                };

                return await _productService.UpdateProduct(dto);
            }
        }
    }
}
