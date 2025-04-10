using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Data
{
    public class RegisterFormViewModel
    {
        public UserRegisterDto UserRegisterDto { get; set; }
        public RegisterResponse RegisterResponse { get; set; }
        public string Message { get; set; } = string.Empty;
        private readonly IAppService _appService;
        public RegisterFormViewModel(IAppService appService)
        {
            UserRegisterDto = new UserRegisterDto();
            RegisterResponse = new RegisterResponse();
            _appService = appService;

        }

        public async Task RegisterUserAsync(UserRegisterDto userRegisterDTO)
        {
            var result = await _appService.AuthService.RegisterUser(userRegisterDTO);
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