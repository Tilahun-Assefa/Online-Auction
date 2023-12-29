using OnlineAuction.Models;

namespace OnlineAuction.Dtos.CategoryDtos
{
    public class CategoryDto : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}