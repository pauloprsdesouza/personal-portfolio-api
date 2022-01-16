using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Portfolio.Tests.Fakes
{
    public sealed class FakeApiServer : TestServer
    {
        public FakeApiServer() : base(new Program().CreateWebHostBuilder()) { }

        public IWebHostEnvironment Environment => Host.Services.GetService<IWebHostEnvironment>();
        public IDynamoDBContext AmazonDynamoDB => Host.Services.GetService<IDynamoDBContext>();
    }
}
