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
        private readonly IAppService _appService;

        public LoginFormViewModel(IAppService appService)
        {
            UserLoginDto = new UserLoginDto();
            _appService = appService;
            AuthResponse = new AuthResponse();
        }

        public async Task AuthenticateUser(UserLoginDto userLoginDTO)
        {
            var result = await _appService.AuthService.Login(userLoginDTO);
            AuthResponse.IsSuccess = result.IsSuccess;
        }
    }
}