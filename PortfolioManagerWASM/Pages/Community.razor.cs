using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Community
    {
        // Services
        [Inject] private CommunityViewModel CommunityViewModel { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; }

        // Properties
        public User ActiveUser { get; set; } = new();
        public List<Portfolio> PublicPortfoliosBasicInfo { get; set; } = [];
        public Portfolio ActivePortfolio { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await CommunityViewModel.InitAsync();
            if (AuthState == null)
                CommunityViewModel.ToLogin();

            ActiveUser = CommunityViewModel.ActiveUser;

            PublicPortfoliosBasicInfo = CommunityViewModel.PublicPortfolios;
            ActivePortfolio = CommunityViewModel.ActivePortfolio;
        }

        // Private fields
        private List<ChartDataModel> ChartDataModel { get; set; } = [];

        // Events
        private async Task OnSelectPortfolio(int index)
        {
            await CommunityViewModel.OnSelectPortfolioAsync(index);
            ActivePortfolio = CommunityViewModel.ActivePortfolio;
            ChartDataModel = PortfolioAssetsChartData();
            StateHasChanged();
        }

        public async void OnAddPortfoliot()
        {
            await CommunityViewModel.AddPortfolio(ActivePortfolio);
        }

        private List<ChartDataModel> PortfolioAssetsChartData()
        {
            List<ChartDataModel> chartDataModel = [];
            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                var portfolioAssetData = new ChartDataModel
                {
                    AssetName = portfolioAsset.FinancialAsset.Name,
                    Allocation = portfolioAsset.AllocationPercentage
                };
                chartDataModel.Add(portfolioAssetData);
            }
            return chartDataModel;
        }
    }
    public class ChartDataModel
    {
        public string AssetName { get; set; }
        public int Allocation { get; set; }
    }
}