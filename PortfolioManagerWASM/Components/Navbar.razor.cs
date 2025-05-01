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

        public User ActiveUser { get; set; } = new User();

        protected override async Task OnInitializedAsync()
        {
            await NavbarViewModel.InitAsync();
            ActiveUser = NavbarViewModel.ActiveUser;
        }

        public async Task Logout()
        {
            await NavbarViewModel.Logout();
            NavigationManager.NavigateTo("/");
        }

        public void ToActiveUserProfile()
        {
            NavigationManager.NavigateTo("/userProfile");
        }

        public void ToAdmin()
        {
            NavigationManager.NavigateTo("/admin");
        }
    }
}