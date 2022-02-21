using System.Collections.Generic;
using OnlineAuction.Dtos.Category;

namespace OnlineAuction.Dtos.Product
{
    public class AddProductDto
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public string[] Categories{get; set;}
    }
}