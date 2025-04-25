using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Components
{
    public partial class Navbar
    {
        private readonly IAppService _appService;
        public User ActiveUser { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string MyProperty { get; set; } = "mimi";

        public Navbar(IAppService AppService)
        {
            _appService = AppService;
        }

        protected override async Task OnInitializedAsync()
        {
            ActiveUser = _appService.UserService.ActiveUser;

            if (ActiveUser == null)
            {
                Console.WriteLine("ActiveUser is not initialized.");
            }
        }

        public void Logout()
        {
            _appService.AuthService.Logout();
            NavigationManager.NavigateTo("/login");
        }

    }
}