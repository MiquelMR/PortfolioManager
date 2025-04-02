using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int UserId);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(int UserId, User user);
        public Task<bool> DeleteUser(int UserId);
    }
}
