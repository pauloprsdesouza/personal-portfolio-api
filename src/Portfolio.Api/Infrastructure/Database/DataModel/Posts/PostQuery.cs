using Amazon.DynamoDBv2.DocumentModel;
using NUlid;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Posts
{
    public class PostQuery
    {
        public Ulid BeforePost { get; set; }

        public int Length { get; set; }

        public string CategoryId { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public QueryOperationConfig ToDynamoDBQuery()
        {
            var primaryKey = new PostKey(BeforePost);

            var filter = new QueryFilter();
            filter.AddCondition(AppDynamoDBTable.PK, QueryOperator.Equal, primaryKey.PK);
            filter.AddCondition(AppDynamoDBTable.SK, QueryOperator.LessThan, primaryKey.SK);

            if (CategoryId != null)
            {
                filter.AddCondition("CategoryId", QueryOperator.Equal, CategoryId);
            }

            if (Status != null)
            {
                filter.AddCondition("Status", QueryOperator.Equal, Status);
            }

            if (Title != null)
            {
                filter.AddCondition("Title", QueryOperator.Equal, Title);
            }

            return new QueryOperationConfig
            {
                Filter = filter,
                Limit = Length,
                BackwardSearch = true
            };
        }
    }
}
