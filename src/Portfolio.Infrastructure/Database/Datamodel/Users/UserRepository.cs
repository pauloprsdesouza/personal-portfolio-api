using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Users;

namespace Portfolio.Infrastructure.Database.Datamodel.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(ApiDbContext dbContext) {
            _dbContext = dbContext;
            _users = dbContext.Set<User>();
        }

        public async Task<User> FindByEmail(string userEmail)
        {
            return await _users.Where(p => p.Email == userEmail).SingleOrDefaultAsync();
        }

        public async Task<User> FindById(int id)
        {
            return await _users.Where(p => p.Id == id).SingleOrDefaultAsync();
        }
    }
}
