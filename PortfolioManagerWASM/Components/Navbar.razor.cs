using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Components
{
    public partial class Navbar
    {
        [Inject]
        private NavbarViewModel NavbarViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public User ActiveUser { get; set; } = null;

        protected override void OnInitialized()
        {
            NavbarViewModel.Init();
            ActiveUser = NavbarViewModel.ActiveUser;
        }

        public void Logout()
        {
            NavbarViewModel.Logout();
            NavigationManager.NavigateTo("/login");
        }

        public void ToActiveUserProfile()
        {
            NavigationManager.NavigateTo("/userProfile");
        }
    }
}