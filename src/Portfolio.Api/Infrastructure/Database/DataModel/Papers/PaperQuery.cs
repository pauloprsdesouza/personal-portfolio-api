using System;
using Amazon.DynamoDBv2.DocumentModel;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Papers
{
    public class PaperQuery
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string SubmissionDeadline { get; set; }

        public string Qualis { get; set; }

        public string Type { get; set; }

        public QueryOperationConfig ToDynamoDBQuery()
        {
            var primaryKey = new PaperKey(Id);

            var filter = new QueryFilter();
            filter.AddCondition(AppDynamoDBTable.PK, QueryOperator.Equal, primaryKey.PK);

            if (!string.IsNullOrEmpty(Title))
            {
                filter.AddCondition("Title", QueryOperator.BeginsWith, Title);
            }

            if (!string.IsNullOrEmpty(SubmissionDeadline))
            {
                filter.AddCondition("SubmissionDeadline", QueryOperator.GreaterThanOrEqual, SubmissionDeadline);
            }

            if (!string.IsNullOrEmpty(Qualis))
            {
                filter.AddCondition("Qualis", QueryOperator.Equal, Qualis);
            }

            if (!string.IsNullOrEmpty(Type))
            {
                filter.AddCondition("Type", QueryOperator.Equal, Type);
            }

            return new QueryOperationConfig
            {
                Filter = filter,
                BackwardSearch = true
            };
        }
    }
}
