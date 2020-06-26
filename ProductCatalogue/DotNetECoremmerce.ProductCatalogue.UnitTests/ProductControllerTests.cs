using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using DotNetECoremmerce.ProductCatalogue.API.Controllers;
using System.Collections;
using System.Linq;
using Interview.API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DotNetECoremmerce.ProductCatalogue.API.Model;
/* using Microsoft.EntityFrameworkCore.InMemory; */

namespace DotNetECoremmerce.ProductCatalogue.UnitTests
{
    public class ProductControllerTest
    {
        private readonly DbContextOptions<ProductCatalogueContext> _dbOptions;

        public ProductControllerTest()
        {
            _dbOptions = new DbContextOptionsBuilder<ProductCatalogueContext>()
            .UseInMemoryDatabase(databaseName: "ProductCatalogueDb_InMemory").Options;

            using (var dbContext = new ProductCatalogueContext(_dbOptions))
            {
                dbContext.Products.AddRange(GetFakeProducts());
                dbContext.SaveChanges();
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Get_products_Should_return_correct_products()
        {

            var inMemoryproductCatalogueContext = new ProductCatalogueContext(_dbOptions);
            var loggerMock = new Mock<ILogger<ProductsController>>();

            var productsController = new ProductsController(loggerMock.Object, inMemoryproductCatalogueContext);

            int expectedCount = 6;
            var result = await productsController.Get();
            var resultCount = result.Count();

            Assert.AreEqual(expectedCount, resultCount);

            await Task.CompletedTask;

        }

        private List<Product> GetFakeProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Wrench", Category = "Hand Tools", Price = 6.00m },
                new Product { Name = "Claw Hammer", Category = "Hand Tools", Price = 4.00m },
                new Product { Name = "Drill", Category = "Power Tools", Price = 40.00m },
                new Product { Name = "Jigsaw", Category = "Power Tools", Price = 60.00m },
                new Product { Name = "Padlock", Category = "Ironmongery", Price = 2.50m },
                new Product { Name = "Silicone", Category = "Sealants", Price = 15.00m }
            };

        }
    }
}