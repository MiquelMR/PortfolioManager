using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.UserDto;

namespace PortfolioManagerWASM.Components.Login
{
    public partial class LogInFormComponent
    {
        [Parameter]
        public Func<UserLoginDto, Task> LoginUserAsyncDelegate { get; set; }

        private UserLoginDto loginUser = new();
        public NavigationManager NavigationManager { get; set; }
        private async Task AuthenticateUser()
        {
            if (LoginUserAsyncDelegate != null)
            {
                await LoginUserAsyncDelegate.Invoke(loginUser);
            }
        }
    }
}