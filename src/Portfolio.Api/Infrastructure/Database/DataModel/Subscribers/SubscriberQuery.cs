using Amazon.DynamoDBv2.DocumentModel;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Subscribers
{
    public class SubscriberQuery
    {
        public string SubscriberEmail { get; set; }

        public QueryOperationConfig ToDynamoDBQuery()
        {
            var primaryKey = new SubscriberKey(SubscriberEmail);

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
