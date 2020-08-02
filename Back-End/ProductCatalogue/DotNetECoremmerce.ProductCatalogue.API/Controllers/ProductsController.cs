using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetECoremmerce.ProductCatalogue.API.Configuration;
using DotNetECoremmerce.ProductCatalogue.API.Model;
using Interview.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetECoremmerce.ProductCatalogue.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCatalogueContext _context;
        private readonly ILogger<ProductsController> _logger;
        private readonly IConfigurationService _configurationService;

        public ProductsController(ILogger<ProductsController> logger, ProductCatalogueContext context, IConfigurationService configurationService)
        {
            _logger = logger;
            _context = context;
            _configurationService = configurationService;
        }

        [EnableCors()]
        [HttpGet]
        public async Task<Product[]> GetAllProducts()
        {
            return await _context.Products.ToArrayAsync();
        }

        [EnableCors()]
        [HttpGet("{id}")]
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        [EnableCors()]
        [HttpPut("{id}")]
        [Authorize("CanEditProducts")]
        public async Task<Product> UpdateProduct(int id, Product product)
        {
            try
            {
                // Below updates every property regardless of whether the values have changed
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            return product;
        }


        [EnableCors()]
        [HttpPost]
        [Authorize("CanEditProducts")]
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            return product;
        }




















        /// <summary>
		/// Creates a DGML class diagram of most of the entities in the project wher you go to localhost/dgml
		/// See https://github.com/ErikEJ/SqlCeToolbox/wiki/EF-Core-Power-Tools
		/// </summary>
		/// <returns>a DGML class diagram</returns>
		[HttpGet]
        [Route("dgml")]
        public IActionResult GetDgml()
        {
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\ProductCatalogueEntities.dgml", _context.AsDgml(), System.Text.Encoding.UTF8);

            var file = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "\\ProductCatalogueEntities.dgml");
            var response = File(file, "application/octet-stream", "ProductCatalogueEntities.dgml");
            return response;
        }
    }
}
