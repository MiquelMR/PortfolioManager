using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserByEmailAsync(string email);
        Task<int> GetUserIdByNameAsync(string name);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
