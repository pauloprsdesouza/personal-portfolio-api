using System.Threading.Tasks;
using Portfolio.Domain.Users;

namespace Portfolio.Api.Features.Users
{
    public class UserSearch
    {
        public readonly IUserRepository _userRepository;

        public UserSearch(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool UserNotFound { get; private set; }

        public async Task<User> Find(string userEmail)
        {
            var user = await _userRepository.FindById(userId);

            if (user == null)
            {
                UserNotFound = true;
                return null;
            }

            return user;
        }
    }
}
