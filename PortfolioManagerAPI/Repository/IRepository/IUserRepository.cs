using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
