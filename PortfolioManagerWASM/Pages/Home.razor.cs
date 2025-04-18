using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Home
    {
        public User User { get; set; }
        public new List<Asset> Assets { get; set; }
        public string Image { get; set; }
        public List<Portfolio> Portfolios { get; set; }

        [Inject]
        private HomeViewModel HomeViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthState { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState;

            if (authState.User == null)
            {
                var returnUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                NavigationManager.NavigateTo("login", true);
            }

            await HomeViewModel.InitAsync();
            User = HomeViewModel.User;
            Assets = HomeViewModel.Assets;
            Image = HomeViewModel.Image;
            Portfolios = HomeViewModel.Portfolios;
        }

        public void Logout()
        {
            HomeViewModel.Logout();
            NavigationManager.NavigateTo("/login");
        }

    }
}