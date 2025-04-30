using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.DTOs;

namespace PortfolioManagerWASM.Pages.Login
{
    public partial class SignInFormComponent
    {
        [Parameter]
        public Func<UserRegisterDto, Task> RegisterNewUserAsyncDelegate { get; set; }

        private UserRegisterDto newUser = new();

        public async Task RegisterNewUserAsync()
        {
            if (RegisterNewUserAsyncDelegate != null)
            {
                await RegisterNewUserAsyncDelegate.Invoke(newUser);
            }
        }
    }
}