using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int UserId);
        public Task<User> Login(UserLoginDTO userLoginDto);
        public Task<User> RegisterUser(UserRegisterDTO user);
        public Task<User> UpdateUser(int UserId, User user);
        public Task<bool> DeleteUser(string Email);
    }
}
