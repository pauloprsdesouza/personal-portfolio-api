using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Portfolio.Api.Models.Contacts;
using Portfolio.Tests.Fakes;
using Xunit;

namespace Portfolio.Tests.Functional.Contacts
{
    public class SendMessageTest
    {
        private readonly FakeApiServer _server;
        private readonly FakeApiClient _client;

        public SendMessageTest()
        {
            _server = new FakeApiServer();
            _client = new FakeApiClient(_server);
        }

        [Fact]
        public async Task ShouldSendAMessage()
        {
            ContactRequest contactRequest = new ContactRequest();
            contactRequest.From = "contact@contact.com.br";
            contactRequest.Subject = "Subject Test";
            contactRequest.Content = "Content";

            var response = await _client.PostJsonAsync("/contacts", contactRequest);

            var responseJson = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ShouldValidateAMessage()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < 151; i++)
            {
                sb.Append("*");
            }

            ContactRequest contactRequest = new ContactRequest();
            contactRequest.From = sb.ToString();
            contactRequest.Subject = sb.ToString();
            contactRequest.Content = sb.ToString();

            var response = await _client.PostJsonAsync("/contacts", contactRequest);

            var responseJson = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
