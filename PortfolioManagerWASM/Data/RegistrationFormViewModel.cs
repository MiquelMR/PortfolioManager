using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Data
{
    public class RegisterFormViewModel
    {
        public UserRegisterDTO UserRegisterDTO { get; set; }
        public string Message { get; set; } = string.Empty;
        private readonly IAppService _appService;

        public RegisterFormViewModel(IAppService appService)
        {
            UserRegisterDTO = new UserRegisterDTO();
            _appService = appService;
        }

        public async Task RegisterUserAsync(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var registeredUser = await _appService.UserService.RegisterUser(userRegisterDTO);
                Message = $"User {registeredUser.Name} registered successfully!";
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }
        }
    }
}