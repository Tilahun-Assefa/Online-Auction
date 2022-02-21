using Microsoft.EntityFrameworkCore;
using OnlineAuction.Models;

namespace OnlineAuction.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) {           
            
         }
         protected override void OnModelCreating (ModelBuilder modelBuilder){
             modelBuilder.Entity<ProductCategory>().HasKey(pc => new{pc.ProductId, pc.CategoryId});
         }
        public DbSet<Product> Products {get; set;}
        public DbSet<Review> Reviews {get; set;}
        public DbSet<Category> Categories {get; set;}
        public DbSet<ProductCategory> ProductCategories {get; set;}
    }
}