using Amazon.DynamoDBv2.DocumentModel;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Publications
{
    public class PublicationQuery
    {
        public string Id { get; set; }

        public QueryOperationConfig ToDynamoDBQuery()
        {
            var primaryKey = new PublicationKey(Id);

            var filter = new QueryFilter();
            filter.AddCondition(AppDynamoDBTable.PK, QueryOperator.Equal, primaryKey.PK);

            return new QueryOperationConfig
            {
                Filter = filter,
                BackwardSearch = true
            };
        }
    }
}
