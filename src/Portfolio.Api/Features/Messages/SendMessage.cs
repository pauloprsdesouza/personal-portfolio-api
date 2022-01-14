using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Portfolio.Api.Features.Messages
{
    public class SendMessage
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }

        public HttpStatusCode MessageSent { get; set; }

        public async Task Send()
        {
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = Source,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { "paulo.prsdesouza@gmail.com" }
                    },
                    Message = new Message
                    {
                        Subject = new Content(Subject),
                        Body = new Body
                        {
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = Content
                            }
                        }
                    }
                };

                try
                {
                    var response = await client.SendEmailAsync(sendRequest);

                    MessageSent = response.HttpStatusCode;
                }
                catch (Exception ex)
                {
                    var message = ex.Message;

                    MessageSent = HttpStatusCode.NotFound;
                }
            }
        }
    }
}
