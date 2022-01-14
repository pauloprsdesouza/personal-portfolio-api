using System;
using Amazon.DynamoDBv2.DocumentModel;
using NUlid;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Categories
{
    public class CategoryQuery
    {
        public QueryOperationConfig ToDynamoDBQuery()
        {
            var primaryKey = new CategoryKey(String.Empty);

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
