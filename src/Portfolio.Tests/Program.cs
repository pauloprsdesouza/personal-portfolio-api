using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Portfolio.Tests
{
    public class Program
    {
        private readonly IConfiguration _configuration;

        public Program()
        {
            BuildPath = Path.Combine("bin", "Debug", "netcoreapp3.1");
            ProjectPath = AppContext.BaseDirectory.Replace(BuildPath, string.Empty);
            ContentPath = ProjectPath.Replace("Tests", "Api");

            _configuration = new ConfigurationBuilder()
                .SetBasePath(ProjectPath)
                .Build();
        }

        public string BuildPath { get; }
        public string ProjectPath { get; }
        public string ContentPath { get; }

        public IWebHostBuilder CreateWebHostBuilder() => new WebHostBuilder()
            .UseStartup<Startup>()
            .UseEnvironment("Testing")
            .ConfigureAppConfiguration((builder, config) =>
            {
                builder.HostingEnvironment.ContentRootPath = ProjectPath;
                builder.HostingEnvironment.WebRootPath = ContentPath;
                builder.HostingEnvironment.ContentRootFileProvider = new PhysicalFileProvider(ProjectPath);
                builder.HostingEnvironment.WebRootFileProvider = new PhysicalFileProvider(ContentPath);

                config.AddConfiguration(_configuration);
            });
    }
}
