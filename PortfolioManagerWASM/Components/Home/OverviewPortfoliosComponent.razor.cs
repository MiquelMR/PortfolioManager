using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

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
        private List<ChartDataModel> ChartDataModel { get; set; } = [];

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
            ChartDataModel = PortfolioAssetsChartData();
            StateHasChanged();
        }

        private List<ChartDataModel> PortfolioAssetsChartData()
        {
            List<ChartDataModel> chartDataModel = [];
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

                var portfolioAssetData = new ChartDataModel
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
    }

    public class ChartDataModel
    {
        public string AssetName { get; set; }
        public int Allocation { get; set; }
        public string Color { get; set; }
    };
}
