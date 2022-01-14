using System;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.Converters;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Papers
{
    [DynamoDBTable(AppDynamoDBTable.Name)]
    public class Paper
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
        public string SubmissionDeadline { get; set; }

        [DynamoDBProperty]
        public string Type { get; set; }

        [DynamoDBProperty]
        public string Place { get; set; }

        [DynamoDBProperty]
        public string Qualis { get; set; }

        [DynamoDBProperty]
        public string WebsiteUrl { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
