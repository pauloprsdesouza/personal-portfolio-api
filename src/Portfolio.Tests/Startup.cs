using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Configuration;
using Portfolio.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using Portfolio.Infrastructure.Database.Datamodel;
using System;
using Microsoft.EntityFrameworkCore;
using Bogus;
using Portfolio.Infrastructure.Dependencies;

namespace Portfolio.Tests
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options =>
           {
               options.UseInMemoryDatabase(Guid.NewGuid().ToString());
           });

            services.Configure<Faker>(options => options.Locale = "pt_BR");

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(RequestValidationFilter));
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.Default())
            .AddApplicationPart(typeof(Portfolio.Api.Startup).Assembly);

            services.AddScoped(_ => Substitute.For<IDynamoDBContext>());
            services.RepositoriesConfigure();

            services.AddSingleton<ApiDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
