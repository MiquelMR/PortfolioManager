using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IAuthService
    {
        public Task<AuthResponse> RegisterUser(UserRegisterDto userFromRegisterForm);
        public Task<AuthResponse> Login(UserLoginDto userFromLoginForm);
        public Task Logout();
    }
}
