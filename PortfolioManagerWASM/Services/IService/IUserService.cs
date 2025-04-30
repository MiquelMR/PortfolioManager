using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IUserService
    {
        User ActiveUser { get; }
        public Task<User> GetUserByEmail(string Email);
        public Task<AuthResponse> LoginUser(UserLoginDto userLoginDto);
        public Task Logout();
        public Task<bool> RegisterUser(UserRegisterDto registerUserDto);
        public Task<User> UpdateUser(UserUpdateDto userUpdateDto);
        public Task<bool> DeleteUserAsync();
        public Task RefreshActiveUserAsync();
    }
}
