using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Home
    {
        public User User { get; set; } = new User();
        public List<Asset> Assets { get; set; } = new List<Asset>();
        public string Image { get; set; } = string.Empty;
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

        [Inject]
        private HomeViewModel HomeViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthState { get; set; }

        private int indexOfActivePortfolio = 0;
        private Portfolio activePortfolio = new Portfolio();
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
            activePortfolio = Portfolios[indexOfActivePortfolio];
        }

        public void Logout()
        {
            HomeViewModel.Logout();
            NavigationManager.NavigateTo("/login");
        }

    }
}