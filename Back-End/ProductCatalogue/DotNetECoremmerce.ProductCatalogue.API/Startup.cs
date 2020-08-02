using System;
using DotNetECoremmerce.ProductCatalogue.API.Configuration;
using Interview.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using DotNetECoremmerce.ProductCatalogue.API.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DotNetECoremmerce.ProductCatalogue.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSettings = new AppSettings();

            Configuration.GetSection("DotNetECoremmerce:ProductCatalogue:ConnectionStrings").Bind(appSettings.ConnectionStrings);
            Configuration.GetSection("DotNetECoremmerce:ProductCatalogue:ApiSettings").Bind(appSettings.ApiSettings);

            services.AddSingleton(appSettings);

            services.AddScoped<IConfigurationService, ConfigurationService>();


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "http://localhost:3000/*")
                        .WithHeaders(HeaderNames.Authorization, HeaderNames.ContentType)
                        .AllowAnyMethod();

                    });
            });

            services.AddControllers();

            var connectionString = Configuration["DotNetECoremmerce:ProductCatalogue:ConnectionStrings:ProductCatalogueContext"];
            services.AddDbContext<ProductCatalogueContext>(options => options.UseSqlServer(connectionString));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Product Catalogue API",
                    Description = "API for the DotNet eCoremmerce product catalogue",
                    TermsOfService = new Uri("https://example.com/terms"),
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });


            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["DotNetECoremmerce:ProductCatalogue:Auth0Settings:Authority"];
                options.Audience = Configuration["DotNetECoremmerce:ProductCatalogue:Auth0Settings:Audience"];
            });

            // 2. Add Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CanEditProducts",
                    policyBuilder => policyBuilder.AddRequirements(
                        new ProductsAdminRequirement()
                    )
                );
            });

            services.AddSingleton<IAuthorizationHandler, ProductsAdminHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                /*  HTTP Strict Transport Security (HSTS) 
                    An opt-in security enhancement.
                    If a website specifies it and a browser supports it, then it forces all
                    communication over HTTPS and prevents the visitor from using untrusted or
                    invalid certificates.
                */
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalogue API V1");
            });

            app.UseRouting();

            app.UseCors();

            // redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable authentication middleware
            app.UseAuthentication();

            // Enable authorization middleware
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
