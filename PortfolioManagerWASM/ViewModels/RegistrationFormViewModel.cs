using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class RegisterFormViewModel(IAuthService authService)
    {
        public UserRegisterDto UserRegisterDto { get; set; } = new UserRegisterDto();
        public RegisterResponse RegisterResponse { get; set; } = new RegisterResponse();
        public string Message { get; set; } = string.Empty;
        private readonly IAuthService _authService = authService;

        public async Task RegisterUserAsync(UserRegisterDto userRegisterDTO)
        {
            var result = await _authService.RegisterUser(userRegisterDTO);
            RegisterResponse.Success = result.IsSuccess;
            if (result.IsSuccess)
            {
                Message = $"{userRegisterDTO.Name} registered succesfully";
            }
            else
            {
                RegisterResponse.Errors.Append("Error during registering");
                Message = $"Error during registration";
            }
        }
    }
}