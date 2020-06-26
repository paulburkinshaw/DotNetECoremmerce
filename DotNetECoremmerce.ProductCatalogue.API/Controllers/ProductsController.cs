using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetECoremmerce.ProductCatalogue.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetECoremmerce.ProductCatalogue.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Product1", "Product2", "Product3", "Product4"
        };

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                Name = Products[rng.Next(Products.Length)]
            })
            .ToArray();
        }
    }
}
