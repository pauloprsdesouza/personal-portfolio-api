using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Infrastructure.Database.Datamodel;

namespace Portfolio.Tests.Fakes
{
    public sealed class FakeApiServer : TestServer
    {
        public FakeApiServer() : base(new Program().CreateWebHostBuilder()) { }

        public IWebHostEnvironment Environment => Host.Services.GetService<IWebHostEnvironment>();
        public ApiDbContext DataBase => Host.Services.GetService<ApiDbContext>();
    }
}
