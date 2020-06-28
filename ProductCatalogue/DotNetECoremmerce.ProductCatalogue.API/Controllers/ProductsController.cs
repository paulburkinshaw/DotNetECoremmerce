using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetECoremmerce.ProductCatalogue.API.Model;
using Interview.API.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotNetECoremmerce.ProductCatalogue.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCatalogueContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, ProductCatalogueContext context)
        {
            _logger = logger;
            _context = context;
        }

        [EnableCors()]
        [HttpGet]
        public async Task<Product[]> Get()
        {

            return await _context.Products.ToArrayAsync();

        }
    }
}
