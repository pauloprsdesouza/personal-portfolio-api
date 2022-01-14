using Amazon.DynamoDBv2.DocumentModel;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Users
{
    public class UserQuery
    {
        public string UserEmail { get; set; }

        public QueryOperationConfig ToDynamoDBQuery()
        {
            var primaryKey = new UserKey(UserEmail);

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
