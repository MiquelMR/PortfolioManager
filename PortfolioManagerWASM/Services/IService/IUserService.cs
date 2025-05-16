using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.UserDto;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IUserService
    {
        User ActiveUser { get; }
        public Task InitializeAsync();
        public Task<User> GetUserByEmailAsync(string Email);
        public Task<List<User>> GetUsersAsync();
        public Task<AuthResponse> LoginUserAsync(UserLoginDto userLoginDto);
        public Task<User> RegisterUserAsync(UserRegisterDto registerUserDto);
        public Task<User> UpdateUserAsync(UserUpdateDto userUpdateDto);
        public Task<bool> DeleteUserAsync(User user);
        public Task Logout();
    }
}
