using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class LoginForm
    {
        [Inject]
        private LoginFormViewModel LoginFormViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public UserLoginDto UserLoginDto { get; set; } = new UserLoginDto();
        public string Message { get; set; } = string.Empty;
        protected override void OnInitialized()
        {
            UserLoginDto = LoginFormViewModel.UserLoginDto;
            Message = LoginFormViewModel.Message;
        }

        private async Task AuthenticateUser()
        {
            await LoginFormViewModel.AuthenticateUser(LoginFormViewModel.UserLoginDto);
            if (LoginFormViewModel.AuthResponse.IsSuccess)
            {
                NavigationManager.NavigateTo("/home");
            }
        }
    }
}