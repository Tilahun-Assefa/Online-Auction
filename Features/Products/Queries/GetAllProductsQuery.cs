using MediatR;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Models;
using OnlineAuction.Services.ProductService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineAuction.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<ServiceResponse<IEnumerable<GetProductDto>>>
    {
        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllProductsQuery, ServiceResponse<IEnumerable<GetProductDto>>>
        {
            private readonly IProductService _productService;

            public GetAllPlayersQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ServiceResponse<IEnumerable<GetProductDto>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                return await _productService.GetProductsList();
            }
        }
    }
}
