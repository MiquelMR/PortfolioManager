using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.UserDto;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Login
    {
        [Inject]
        private LoginViewModel LoginViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public LoginView LoginView { get; private set; } = LoginView.LogIn;
        public Func<UserLoginDto, Task> LoginUserAsyncDelegate { get; private set; }
        public Func<UserRegisterDto, Task> RegisterUserAsyncDelegate { get; private set; }
        protected override void OnInitialized()
        {
            LoginViewModel.Init();
            if (LoginViewModel.AlreadyLogged)
            {
                NavigationManager.NavigateTo("/home", true);
            }
            LoginUserAsyncDelegate = async (userLoginDto) =>
            {
                await LoginViewModel.AuthenticateUser(userLoginDto);
                NavigationManager.NavigateTo("/home", true);
            };
            RegisterUserAsyncDelegate = async (userRegisterDto) =>
            {
                var user = await LoginViewModel.RegisterUserAsync(userRegisterDto);
                if (user != null)
                {
                    UserLoginDto userLogin = new()
                    {
                        Email = userRegisterDto.Email,
                        Password = userRegisterDto.Password
                    };
                    await LoginViewModel.AuthenticateUser(userLogin);
                    NavigationManager.NavigateTo("/home", true);
                }
            };
        }

        public void ChangeView()
        {
            LoginView = LoginView == LoginView.LogIn ? LoginView.SignIn : LoginView.LogIn;
        }
    }
    public enum LoginView
    {
        LogIn,
        SignIn
    }
}