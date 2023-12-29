using MediatR;
using OnlineAuction.Dtos.ProductDtos;
using OnlineAuction.Models;
using OnlineAuction.Services.ProductService;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineAuction.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ServiceResponse<GetProductDto>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<GetProductDto>>
        {
            private readonly IProductService _productService;

            public GetProductByIdQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ServiceResponse<GetProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                return await _productService.GetProductById(query.Id);
            }
        }
    }
}
