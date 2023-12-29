using OnlineAuction.Models;

namespace OnlineAuction.Dtos.CategoryDtos
{
    public class UpdateCategoryDto : IMapFrom<Category>
    {
        public int Id{set; get;}
        public string Name{set; get;}
    }
}