using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.UserDto;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IUserService
    {
        User ActiveUser { get; }
        public Task<User> GetUserByEmail(string Email);
        public Task<AuthResponse> LoginUser(UserLoginDto userLoginDto);
        public Task Logout();
        public Task<User> RegisterUser(UserRegisterDto registerUserDto);
        public Task<User> UpdatePublicProfile(UserUpdateDto userUpdateDto);
        public Task<bool> DeleteUserAsync();
        public Task InitializeAsync();
    }
}
