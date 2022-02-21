using System;
using System.Collections.Generic;

namespace OnlineAuction.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public List<ProductCategory> ProductCategories { set; get; }
        public List<Review> Reviews { get; set; }
    }
}