using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IUserService
    {
        public Task<User> GetUserById(int UserId);
        public Task<User> GetUserByEmail(string Email);
        public Task<User> UpdateUser(int UserId, User user);
        public Task<bool> DeleteUser(string Email);
        User ActiveUser { get; }
    }
}
