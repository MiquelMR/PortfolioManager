using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Data;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Pages
{
    public partial class LoginForm
    {
        [Inject]
        private LoginFormViewModel LoginFormViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public UserLoginDto UserLoginDto { get; set; }
        public string Message { get; set; }
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