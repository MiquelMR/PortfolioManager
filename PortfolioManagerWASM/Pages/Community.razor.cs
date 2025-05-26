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
        private List<FinancialAssetsChartDataModel> AssetCompositionChartData { get; set; } = [];
        private List<StrenghtsAndWeaknessChartDataModelPolar> StrenghtsAndWeaknessChartData { get; set; } = [];

        // Events
        private async Task OnSelectPortfolio(int index)
        {
            await CommunityViewModel.OnSelectPortfolioAsync(index);
            ActivePortfolio = CommunityViewModel.ActivePortfolio;
            AssetCompositionChartData = PortfolioAssetsChartData();
            StrenghtsAndWeaknessChartData = StrenghtsAndWeakness();
            StateHasChanged();
        }

        public async void OnAddPortfoliot()
        {
            await CommunityViewModel.AddPortfolio(ActivePortfolio);
        }

        private List<FinancialAssetsChartDataModel> PortfolioAssetsChartData()
        {
            List<FinancialAssetsChartDataModel> chartDataModel = [];
            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                var portfolioAssetData = new FinancialAssetsChartDataModel
                {
                    AssetName = portfolioAsset.FinancialAsset.Name,
                    Allocation = portfolioAsset.AllocationPercentage
                };
                chartDataModel.Add(portfolioAssetData);
            }
            return chartDataModel;
        }
        private List<StrenghtsAndWeaknessChartDataModelPolar> StrenghtsAndWeakness()
        {
            // Establish the characteristics of the portfolio
            float income = 0;
            float inflationHedge = 0;
            float volatility = 0;
            float defensive = 0;
            float expansion = 0;
            float growth = 0;
            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                // Provides income
                income +=
                    portfolioAsset.FinancialAsset.Income * (portfolioAsset.AllocationPercentage / 100f) * 20f;

                // Inflation hedge
                inflationHedge +=
                    portfolioAsset.FinancialAsset.InflationHedge * (portfolioAsset.AllocationPercentage / 100f) * 20f;

                // Volatility
                volatility +=
                    portfolioAsset.FinancialAsset.Volatility * (portfolioAsset.AllocationPercentage / 100f) * 20f;

                // Defensive
                defensive +=
                    portfolioAsset.FinancialAsset.Defensive * (portfolioAsset.AllocationPercentage / 100f) * 20f;

                // Expansión
                expansion +=
                    portfolioAsset.FinancialAsset.FavorsExpansion * (portfolioAsset.AllocationPercentage / 100f) * 20f;

                // Returns by Growth
                growth +=
                    portfolioAsset.FinancialAsset.Growth * (portfolioAsset.AllocationPercentage / 100f) * 20f;
            }
            List<StrenghtsAndWeaknessChartDataModelPolar> data =
                [
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Growth", Intensity= growth},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Expansion" , Intensity= expansion},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Inflation", Intensity= inflationHedge},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Income", Intensity= income},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Defensive", Intensity= defensive},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Volatiliy", Intensity= volatility},
                 ];
            return data;
        }

        private static string GetTraitStyle(int trait)
        {
            string style = "";
            style = trait switch
            {
                1 => "font-weight:Bold; color: #3c6cbc",
                2 => "color: #3c7cdc",
                3 => "color: #e4c45c",
                4 => "font-weight:Bold; color: #e4c45c",
                5 => "font-weight:Bold; color: #bc9404",
                _ => "",
            };
            return style;
        }
    }

    public class FinancialAssetsChartDataModel
    {
        public string AssetName { get; set; }
        public int Allocation { get; set; }
    }
    public class StrenghtsAndWeaknessChartDataModelPolar
    {
        public string Environment { get; set; }
        public float Intensity { get; set; }
    };
}