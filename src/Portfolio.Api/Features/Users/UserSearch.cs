using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Portfolio.Api.Infrastructure.Database.DataModel.Users;

namespace Portfolio.Api.Features.Users
{
    public class UserSearch
    {
        public readonly IDynamoDBContext _dbContext;

        public UserSearch(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool UserNotFound { get; private set; }

        public async Task<User> Find(string email)
        {
            var userKey = new UserKey(email);

            var user =  await _dbContext.LoadAsync<User>(userKey.PK, userKey.SK);

            UserNotFound = user == null;

            return user;
        }
    }
}
