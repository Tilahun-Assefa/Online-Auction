using OnlineAuction.Models;

namespace OnlineAuction.Dtos.ProductCategoryDtos
{
    public class GetProductCategoryDto : IMapFrom<ProductCategory>
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}