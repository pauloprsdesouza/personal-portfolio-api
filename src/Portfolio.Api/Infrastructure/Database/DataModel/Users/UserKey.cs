using NUlid;

namespace Portfolio.Api.Infrastructure.Database.DataModel.Users
{
    public class UserKey
    {
         public UserKey(string userEmail)
        {
            PK = $"User";
            SK = $"Email#{userEmail}";
        }

        public string PK { get; }

        public string SK { get; }

        public void AssignTo(User user)
        {
            user.PK = PK;
            user.SK = SK;
        }
    }
}
