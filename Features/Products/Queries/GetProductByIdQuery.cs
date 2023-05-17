using MediatR;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using OnlineAuction.Services.ProductService;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineAuction.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ServiceResponse<ProductDto>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<ProductDto>>
        {
            private readonly IProductService _productService;

            public GetProductByIdQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ServiceResponse<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                return await _productService.GetProductById(query.Id);
            }
        }
    }
}
