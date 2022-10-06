using Microsoft.EntityFrameworkCore;
using OnlineAuction.Models;

namespace OnlineAuction.Data
{
    public class DataContext : DbContext
    {
        #region Constructor
        public DataContext() : base()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Map Entity names to DB table names
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });
        }
        #endregion Methods

        #region Properties
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        #endregion Properties
    }
}