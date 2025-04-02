using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(int UserId);
        bool ExistsById(int UserId);

        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
