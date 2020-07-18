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

namespace DotNetECoremmerce.ProductCatalogue.API.Configuration
{

    public interface IConfigurationService
    {
        public AppSettings GetAppSettings();
    }

    public class ConfigurationService: IConfigurationService
    {

        private readonly ILogger<ConfigurationService> _logger;

        AppSettings AppSettings { get; }

        public ConfigurationService(ILogger<ConfigurationService> logger, AppSettings appSettings)
        {
            _logger = logger;
            AppSettings = appSettings;
        }

        public AppSettings GetAppSettings()
        {
            return AppSettings;
        }
    }
}
