using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Portfolio.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        const string Title = "Portfolio Api";
        const string Description = "Portfolio's API manager Paulo Roberto de Souza";
        const string Version = "v1";

        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlComments = Path.Combine(AppContext.BaseDirectory, "Portfolio.Api.xml");

                options.SwaggerDoc(Version, new OpenApiInfo
                {
                    Title = Title,
                    Description = Description,
                    Version = Version
                });

                options.AddSecurityDefinition("api-key", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "x-api-key",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "api-key",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });

                options.IncludeXmlComments(xmlComments);
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = Title;
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint($"swagger/{Version}/swagger.json", Description);
            });
        }

        public static void UseRedoclyDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "docs/swagger/{documentname}/swagger.json";
            });

            app.UseReDoc(options =>
            {
                options.DocumentTitle = Title;
                options.RoutePrefix = "docs";
                options.SpecUrl($"swagger/{Version}/swagger.json");
            });
        }
    }
}
