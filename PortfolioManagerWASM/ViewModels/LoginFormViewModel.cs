using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.ViewModels
{
    public class LoginFormViewModel(IAuthService authService, IUserService userService)
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userService = userService;

        public UserLoginDto UserLoginDto { get; set; } = new UserLoginDto();
        public AuthResponse AuthResponse { get; set; } = new AuthResponse();
        public string Message { get; set; } = string.Empty;

        public async Task AuthenticateUser(UserLoginDto userLoginDTO)
        {
            var result = await _authService.Login(userLoginDTO);
            await _userService.RefreshActiveUserAsync();
            AuthResponse.IsSuccess = result.IsSuccess;
        }
    }
}