using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Home
    {
        [Inject]
        private HomeViewModel HomeViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [CascadingParameter]

        private Task<AuthenticationState> AuthState { get; set; }
        public User ActiveUser { get; set; } = new();
        public List<Portfolio> UserPortfolios { get; set; } = [];
        private Portfolio ActivePortfolio = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState;

            if (authState.User == null)
            {
                NavigationManager.NavigateTo("login", true);
            }

            await HomeViewModel.InitAsync();
            ActiveUser = HomeViewModel.ActiveUser;
            UserPortfolios = HomeViewModel.PortfoliosBasicInfo;
            ActivePortfolio = HomeViewModel.ActivePortfolio;
        }

        public async Task SelectPortfolio(int index)
        {
            await HomeViewModel.SelectPortfolio(index);
            ActivePortfolio = HomeViewModel.ActivePortfolio;
        }
    }
}