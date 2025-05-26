using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using PortfolioManagerWASM.Models;
using System.Threading;

namespace PortfolioManagerWASM.Components.Home
{
    public partial class OverviewPortfoliosComponent
    {
        // Delegates
        [Parameter] public EventCallback OnDeleteActivePortfolioDelegate { get; set; }
        [Parameter] public EventCallback OnEditActivePortfolioDelegate { get; set; }
        [Parameter] public Func<int, Task> OnSelectPortfolioDelegate { get; set; }

        // Properties
        [Parameter] public Portfolio ActivePortfolio { get; set; }
        [Parameter] public List<Portfolio> UserPortfoliosBasicInfo { get; set; }

        // Private fields
        private List<FinancialAssetsChartDataModel> AssetCompositionChartData { get; set; } = [];
        private List<StrenghtsAndWeaknessChartDataModelPolar> PortfolioStrenghAndWeaknessChartData { get; set; } = new();

        // Events
        private async Task OnDeleteActivePortfolio()
        {
            await OnDeleteActivePortfolioDelegate.InvokeAsync();
            var portfolioDeleted = UserPortfoliosBasicInfo.FirstOrDefault(portfolio => portfolio.PortfolioId == ActivePortfolio.PortfolioId);
            UserPortfoliosBasicInfo.Remove(portfolioDeleted);
            if (UserPortfoliosBasicInfo.Count > 0) await OnSelectPortfolio(0);
        }

        private async Task OnSelectPortfolio(int index)
        {
            await OnSelectPortfolioDelegate.Invoke(index);
            AssetCompositionChartData = PortfolioAssetsChartData();
            PortfolioStrenghAndWeaknessChartData = StrenghtsAndWeakness();
            StateHasChanged();
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
            switch (trait)
            {
                case 1:
                    style = "font-weight:Bold; color: #3c6cbc";
                    break;
                case 2:
                    style = "color: #3c7cdc";
                    break;
                case 3:
                    style = "color: #e4c45c";
                    break;
                case 4:
                    style = "font-weight:Bold; color: #e4c45c";
                    break;
                case 5:
                    style = "font-weight:Bold; color: #bc9404";
                    break;
                default:
                    style = "";
                    break;
            }
            return style;
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
        };
    }
}
