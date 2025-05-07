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
        public List<Asset> FinancialAssets { get; set; } = new();
        public Portfolio ActivePortfolio { get; set; }
        private HomeView CurrentHomeView { get; set; } = HomeView.ViewPortfolio;
        public Func<int, Task> SelectPortfolioDelegate { get; set; }
        public Action<Portfolio> OnPortfolioSubmitDelegate { get; set; }

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
            FinancialAssets = HomeViewModel.FinancialAssets;
            ActivePortfolio = HomeViewModel.ActivePortfolio;
            SelectPortfolioDelegate = SelectPortfolio;
            OnPortfolioSubmitDelegate = OnPortfolioSubmit;
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

        public void OnPortfolioSubmit(Portfolio portfolio)
        {
            HomeViewModel.RegisterPortfolio(portfolio);
        }
    }

    public enum HomeView
    {
        CreatePortfolio,
        ViewPortfolio
    }

}