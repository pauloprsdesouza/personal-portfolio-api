using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Api.Configuration;
using Portfolio.Api.Filters;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Portfolio.Api.Authorization;
using Portfolio.Infrastructure.Database.Datamodel;
using Microsoft.EntityFrameworkCore;
using Portfolio.Infrastructure.Dependencies;

namespace Portfolio.Api
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
            services.AddDefaultAWSOptions(_configuration.GetAWSOptions());

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql("server=localhost;Port=5440;database=postgres;user id=postgres;password=mysecretpassword", pgsql =>
                {
                    pgsql.MigrationsHistoryTable(tableName: "__migration_history", schema: ApiDbContext.Schema);
                });
            });

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

               // options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(RequestValidationFilter));
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.Default());

            services.AddSwaggerDocumentation();

            services.AddAWSService<IAmazonDynamoDB>();

            services.AddScoped<IDynamoDBContext, DynamoDBContext>();

            services.AddDefaultCorsPolicy();
           // services.AddJwtAuthentication(_configuration.GetSection("JWT"));
            services.RepositoriesConfigure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerDocumentation();
            app.UseRedoclyDocumentation();

            app.UseRouting();

            app.UseCors();
           // app.UseAuthentication();
           // app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
