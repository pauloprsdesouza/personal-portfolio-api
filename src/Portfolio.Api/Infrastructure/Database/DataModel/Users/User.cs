using System;
using Amazon.DynamoDBv2.DataModel;
using NUlid;
using Portfolio.Api.Infrastructure.Database.Converters;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Users
{
    [DynamoDBTable(AppDynamoDBTable.Name)]
    public class User
    {
        [DynamoDBHashKey]
        public string PK { get; set; }

        [DynamoDBRangeKey]
        public string SK { get; set; }

        [DynamoDBProperty(typeof(UlidConverter))]
        public Ulid Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string Email { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }

        [DynamoDBProperty]
        public string ProfileImageUrl { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        [DynamoDBProperty(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
