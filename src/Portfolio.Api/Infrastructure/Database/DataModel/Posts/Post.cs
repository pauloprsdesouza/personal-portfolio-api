using System;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.Converters;
using NUlid;
using Portfolio.Api.Models.Users;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Posts
{
    [DynamoDBTable(AppDynamoDBTable.Name)]
    public class Post
    {
        [DynamoDBHashKey]
        public string PK { get; set; }

        [DynamoDBRangeKey]
        public string SK { get; set; }

        [DynamoDBProperty(typeof(UlidConverter))]
        public Ulid Id { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }

        [DynamoDBProperty]
        public string Subtitle { get; set; }

        [DynamoDBProperty]
        public string Status { get; set; }

        [DynamoDBProperty]
        public string FrontImageUrl { get; set; }

        [DynamoDBProperty]
        public string CategoryId { get; set; }

        [DynamoDBProperty]
        public string Content { get; set; }

        [DynamoDBProperty]
        public string PostedBy { get; set; }

        [DynamoDBProperty]
        public string ReadingTime { get; set; }

        [DynamoDBProperty]
        public string Views { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
