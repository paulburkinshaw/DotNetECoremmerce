using System.Collections.Generic;
using System.Linq;
using DotNetECoremmerce.ProductCatalogue.API.Model;
using Interview.API.Data;

namespace DotNetECoremmerce.ProductCatalogue.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProductCatalogueContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new List<Product>
            {
                new Product { Name = "Wrench", Category = "Hand Tools", Price = 6.00m },
                new Product { Name = "Claw Hammer", Category = "Hand Tools", Price = 4.00m },
                new Product { Name = "Drill", Category = "Power Tools", Price = 40.00m },
                new Product { Name = "Jigsaw", Category = "Power Tools", Price = 60.00m },
                new Product { Name = "Padlock", Category = "Ironmongery", Price = 2.50m },
                new Product { Name = "Silicone", Category = "Sealants", Price = 15.00m }
            };

            context.Products.AddRange(products);
            context.SaveChanges();


        }
    }
}
