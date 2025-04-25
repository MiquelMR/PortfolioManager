using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Components
{
    public partial class Navbar
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public User ActiveUser { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string MyProperty { get; set; } = "mimi";

        public Navbar(IUserService UserService, IAuthService authService)
        {
            _userService = UserService;
            _authService = authService;
        }

        protected override void OnInitialized()
        {
            ActiveUser = _userService.ActiveUser;

            if (ActiveUser == null)
            {
                Console.WriteLine("ActiveUser is not initialized.");
            }
        }

        public void Logout()
        {
            _authService.Logout();
            NavigationManager.NavigateTo("/login");
        }

        public void ToActiveUserProfile()
        {
            NavigationManager.NavigateTo("/userProfile");
        }

    }
}