using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetECoremmerce.ProductCatalogue.API.Data;
using Interview.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetECoremmerce.ProductCatalogue.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ProductCatalogueContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                    throw;
                }


            }

            host.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();

                        config.AddAzureAppConfiguration(options =>
                                           options
                                               .Connect(settings["ConnectionStrings:AppConfig"])
                                               // Load configuration values with no label
                                               .Select(KeyFilter.Any, LabelFilter.Null)
                                               // Override with any configuration values specific to current hosting env
                                               .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                                       );
                    });
                    webBuilder.UseStartup<Startup>();

                });
    }
}
