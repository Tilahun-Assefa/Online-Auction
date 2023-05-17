using MediatR;
using OnlineAuction.Services.ProductService;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineAuction.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class DeletePlayerCommandHandler : IRequestHandler<DeleteProductCommand, bool>
        {
            private readonly IProductService _productService;

            public DeletePlayerCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {

                return await _productService.DeleteProduct(command.Id);
            }
        }
    }
}
