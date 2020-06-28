using DotNetECoremmerce.ProductCatalogue.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Interview.API.Data
{
    public class ProductCatalogueContext : DbContext
    {

        public ProductCatalogueContext(DbContextOptions<ProductCatalogueContext> options) : base(options)
        {

            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }


    }

}
