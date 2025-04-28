using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class RegisterForm
    {
        [Inject]
        private RegisterFormViewModel RegisterFormViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public UserRegisterDto RegisterFormDto { get; set; }
        public string Message { get; set; }
        protected override void OnInitialized()
        {
            RegisterFormDto = RegisterFormViewModel.UserRegisterDto;
            Message = RegisterFormViewModel.Message;
        }

        private async Task RegisterUser()
        {
            await RegisterFormViewModel.RegisterUserAsync(RegisterFormViewModel.UserRegisterDto);
            if (RegisterFormViewModel.RegisterResponse.Success)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
