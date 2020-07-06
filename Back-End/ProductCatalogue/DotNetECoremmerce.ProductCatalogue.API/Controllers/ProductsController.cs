using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetECoremmerce.ProductCatalogue.API.Configuration;
using DotNetECoremmerce.ProductCatalogue.API.Model;
using Interview.API.Data;
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
        public async Task<Product[]> Get()
        {
            return await _context.Products.ToArrayAsync();

        }
    }
}
