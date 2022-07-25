namespace Portfolio.Domain.Users
{
    public interface IUserRepository
    {
         Task<User> FindById(int id);
         Task<User> FindByEmail(string userEmail);
    }
}
