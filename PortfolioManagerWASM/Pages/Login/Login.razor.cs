using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages.Login
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
                await LoginViewModel.LoginUserAsyncDelegate.Invoke(userLoginDto);
                NavigationManager.NavigateTo("/home", true);
            };
            RegisterUserAsyncDelegate = async (userRegisterDto) =>
            {
                await LoginViewModel.RegisterUserAsyncDelegate.Invoke(userRegisterDto);
                UserLoginDto userLogin = new();
                userLogin.Email = userRegisterDto.Email;
                userLogin.Password = userRegisterDto.Password;
                await LoginViewModel.LoginUserAsyncDelegate.Invoke(userLogin);
                NavigationManager.NavigateTo("/home", true);
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