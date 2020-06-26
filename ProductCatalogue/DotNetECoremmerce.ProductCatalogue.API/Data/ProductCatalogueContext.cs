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
            /* modelBuilder.Entity<Product>()
                .Property(p => p.Inserted)
                .ValueGeneratedOnAdd(); */
        }


    }

    /* public class ProductCatalogueContextDesignFactory : IDesignTimeDbContextFactory<ProductCatalogueContext>
    {
        public ProductCatalogueContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductCatalogueContext>()
                .UseSqlServer("Server=localhost;Database=ProductCatalogueDb;User Id=sa;Password=BigPassw0rd");

            return new ProductCatalogueContext(optionsBuilder.Options);
        }
    } */
}
