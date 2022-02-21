using System.Collections.Generic;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Review;

namespace OnlineAuction.Dtos.Product
{
    public class GetProductDto
    {
        public int Id { set; get; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public List<GetCategoryDto> Categories { set; get; }
        public List<GetReviewDto> Reviews { get; set; }
    }
}