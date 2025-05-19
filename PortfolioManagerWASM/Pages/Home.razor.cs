using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Home
    {
        // Dependencies
        [Inject] private HomeViewModel HomeViewModel { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; }

        // Delegates 
        public Func<int, Task> OnSelectPortfolioDelegate { get; set; }
        public Action<Portfolio> OnPortfolioSubmitDelegate { get; set; }
        public Action OnDeleteActivePortfolioDelegate { get; set; }
        public Action<HomeView> OnClickBackButtonDelegate { get; set; }

        // Properties
        public User ActiveUser { get; set; } = new();
        public List<Portfolio> UserPortfoliosBasicInfo { get; set; } = [];
        public List<FinancialAsset> FinancialAssets { get; set; } = [];
        public Portfolio ActivePortfolio { get; set; }

        // Private fields
        private HomeView CurrentHomeView { get; set; } = HomeView.Overview;

        protected override async Task OnInitializedAsync()
        {
            await HomeViewModel.InitAsync();
            if (AuthState == null)
                HomeViewModel.ToLogin();

            ActiveUser = HomeViewModel.ActiveUser;

            UserPortfoliosBasicInfo = HomeViewModel.UserPortfoliosBasicInfo;
            FinancialAssets = HomeViewModel.FinancialAssets;
            ActivePortfolio = HomeViewModel.ActivePortfolio;
            OnSelectPortfolioDelegate = OnSelectPortfolio;
            OnPortfolioSubmitDelegate = OnPortfolioSubmit;
            OnDeleteActivePortfolioDelegate = OnDeleteActivePortfolio;
            OnClickBackButtonDelegate = OnChangeCurrentHomeView;
        }

        // Events
        private async Task OnSelectPortfolio(int index)
        {
            await HomeViewModel.OnSelectPortfolioAsync(index);
            ActivePortfolio = HomeViewModel.ActivePortfolio;
            StateHasChanged();
        }

        public async void OnPortfolioSubmit(Portfolio newPortfolio)
        {
            var portfolioCreated = await HomeViewModel.RegisterPortfolioAsync(newPortfolio);
            UserPortfoliosBasicInfo.Add(portfolioCreated);
            ActivePortfolio = UserPortfoliosBasicInfo.Last();
            OnChangeCurrentHomeView(HomeView.Overview);
            StateHasChanged();
        }

        public async void OnDeleteActivePortfolio()
        {
            await HomeViewModel.DeleteActivePortfolioAsync();
        }

        private void OnChangeCurrentHomeView(HomeView homeView)
        {
            CurrentHomeView = homeView;
        }
    }

    public enum HomeView
    {
        CreatePortfolio,
        Overview
    }

}