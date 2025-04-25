using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Data
{
    public class LoginFormViewModel
    {
        public UserLoginDto UserLoginDto { get; set; }
        public AuthResponse AuthResponse { get; set; }
        public string Message { get; set; } = string.Empty;
        private readonly IAuthService _authService;

        public LoginFormViewModel(IAuthService authService)
        {
            UserLoginDto = new UserLoginDto();
            AuthResponse = new AuthResponse();
            _authService = authService;
        }

        public async Task AuthenticateUser(UserLoginDto userLoginDTO)
        {
            var result = await _authService.Login(userLoginDTO);
            AuthResponse.IsSuccess = result.IsSuccess;
        }
    }
}