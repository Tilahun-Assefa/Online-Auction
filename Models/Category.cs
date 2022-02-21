using System.Collections.Generic;

namespace OnlineAuction.Models
{
    public class Category
    {
        public int Id{set; get;}
        public string Name{set; get;}    
        public List<ProductCategory> ProductCategories{set; get;}    
    }
}