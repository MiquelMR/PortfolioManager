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

        private async Task OnAddPortfolio()
        {
            await CommunityViewModel.AddPortfolio(ActivePortfolio);
        }

        private List<FinancialAssetsChartDataModel> PortfolioAssetsChartData()
        {
            List<FinancialAssetsChartDataModel> chartDataModel = [];
            List<string> colors =
                ["#3498db",
                "#393E46",
                "#F2C078",
                "#f5f5f5",
                "#8be04e",
                "#0d88e6",
                "#7c1158",
                "#00b7c7",
                "#5ad45a"];
            int counter = 0;

            foreach (PortfolioAsset portfolioAsset in ActivePortfolio.PortfolioAssets)
            {
                if (counter > colors.Count - 1)
                    counter = 0;

                var portfolioAssetData = new FinancialAssetsChartDataModel
                {
                    AssetName = portfolioAsset.FinancialAsset.Name,
                    Allocation = portfolioAsset.AllocationPercentage,
                    Color = colors[counter]
                };
                chartDataModel.Add(portfolioAssetData);
                counter++;
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
                    (portfolioAsset.FinancialAsset.Income - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Inflation hedge
                inflationHedge +=
                    (portfolioAsset.FinancialAsset.InflationHedge - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Volatility
                volatility +=
                    (portfolioAsset.FinancialAsset.Volatility - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Defensive
                defensive +=
                    (portfolioAsset.FinancialAsset.Defensive - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Expansión
                expansion +=
                    (portfolioAsset.FinancialAsset.FavorsExpansion - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;

                // Returns by Growth
                growth +=
                    (portfolioAsset.FinancialAsset.Growth - 1) * (portfolioAsset.AllocationPercentage / 100f) * 25;
            }
            List<StrenghtsAndWeaknessChartDataModelPolar> data =
                [
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Growth", Intensity= growth, Style=GetTraitStyle(growth), Description = GetDescription("Growth")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Expansion" , Intensity= expansion, Style=GetTraitStyle(expansion), Description = GetDescription("Expansion")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Inflation", Intensity= inflationHedge, Style=GetTraitStyle(inflationHedge), Description = GetDescription("Inflation")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Income", Intensity = income, Style = GetTraitStyle(income), Description = GetDescription("Income")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Defensive", Intensity = defensive ,Style = GetTraitStyle(defensive), Description = GetDescription("Defensive")},
                    new StrenghtsAndWeaknessChartDataModelPolar() { Environment = "Volatiliy", Intensity = volatility, Style = GetTraitStyle(volatility), Description = GetDescription("Volatiliy")},
                 ];
            return data;
        }

        private static string GetTraitStyle(float intensity)
        {
            string style;
            if (intensity < 30)
                style = "color:white; font-weight:bold; background-color:var(--blue);";
            else if (intensity < 45)
                style = "color:white; background-color:var(--softerBlue);";
            else if (intensity < 60)
                style = "color:white; background-color:var(--gray);";
            else if (intensity < 70)
                style = "color:white; background-color:var(--softerGold);";
            else
                style = "color:white; font-weight:bold; background-color:var(--gold);";
            return style;
        }

        private static string GetDescription(string trait)
        {
            string description;
            switch (trait)
            {
                case "Growth":
                    description = "Potential for capital appreciation through reinvestment of earnings";
                    break;
                case "Expansion":
                    description = "Tendency to perform in line with periods of economic growth and expansion";
                    break;
                case "Inflation":
                    description = "Ability to protect purchasing power by appreciating during inflationary periods";
                    break;
                case "Income":
                    description = "Capability to provide returns in the form of dividends or coupon payments";
                    break;
                case "Defensive":
                    description = "Ability to maintain value or stability during economic downturns";
                    break;
                case "Volatiliy":
                    description = "Susceptibility to frequent or significant price fluctuations";
                    break;
                default:
                    Console.WriteLine("Error providing description");
                    description = "Internal Error";
                    break;
            }
            return description;
        }

        public class FinancialAssetsChartDataModel
        {
            public string AssetName { get; set; }
            public int Allocation { get; set; }
            public string Color { get; set; }
        };

        public class StrenghtsAndWeaknessChartDataModelPolar
        {
            public string Environment { get; set; }
            public float Intensity { get; set; }
            public string Style { get; set; }
            public string Description { get; set; }
        };
    }
}