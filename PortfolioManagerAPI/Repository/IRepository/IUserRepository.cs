using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserByIdAsync(int userId);
        Task<bool> DeleteUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsByIdAsync(int userId);
        Task<bool> UserExistsByEmailAsync(string email);
    }
}
