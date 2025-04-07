using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Data;

namespace PortfolioManagerWASM.Pages
{
    public partial class RegistrationForm
    {
        [Inject]
        public RegisterFormViewModel RegisterFormViewModel { get; private set; }
        protected override void OnInitialized()
        {

        }

        private async Task RegisterUser()
        {
            await RegisterFormViewModel.RegisterUserAsync(RegisterFormViewModel.UserRegisterDTO);
        }
    }
}
