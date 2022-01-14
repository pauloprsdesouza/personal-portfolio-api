using System;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.Converters;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Publications
{
    [DynamoDBTable(AppDynamoDBTable.Name)]
    public class Publication
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
        public string Abstract { get; set; }

        [DynamoDBProperty]
        public string Publisher { get; set; }

        [DynamoDBProperty]
        public string Year { get; set; }

        [DynamoDBProperty]
        public string Volume { get; set; }

        [DynamoDBProperty]
        public string Page { get; set; }

        [DynamoDBProperty]
        public string Type { get; set; }

        [DynamoDBProperty]
        public string Qualis { get; set; }

        [DynamoDBProperty]
        public string UrlPublication { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

    }
}
