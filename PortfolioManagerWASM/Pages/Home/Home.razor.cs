using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;
using System;

namespace PortfolioManagerWASM.Pages.Home
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
        public Portfolio ActivePortfolio { get; set; }
        private HomeView CurrentHomeView { get; set; } = HomeView.ViewPortfolio;
        public Func<int, Task> SelectPortfolioDelegate { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthState;
            if (authState.User == null)
                NavigationManager.NavigateTo("login", true);

            await HomeViewModel.InitAsync();
            ActiveUser = HomeViewModel.ActiveUser;
            if (ActiveUser == null)
                NavigationManager.NavigateTo("login", true);

            UserPortfolios = HomeViewModel.PortfoliosBasicInfo;
            ActivePortfolio = HomeViewModel.ActivePortfolio;
            SelectPortfolioDelegate = SelectPortfolio;
        }

        private async Task SelectPortfolio(int index)
        {
            await HomeViewModel.SelectPortfolioAsync(index);
            ActivePortfolio = HomeViewModel.ActivePortfolio;
            StateHasChanged();
        }

        private void ChangeCurrentHomeView(HomeView homeView)
        {
            CurrentHomeView = homeView;
        }
    }

    public enum HomeView
    {
        CreatePortfolio,
        ViewPortfolio
    }

}