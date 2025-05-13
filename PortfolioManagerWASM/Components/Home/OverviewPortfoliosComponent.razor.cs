using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Pages;

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
        [Parameter] public List<Portfolio> UserPortfolios { get; set; }

        // Private fields
        private List<ChartDataModel> ChartDataModel { get; set; } = [];

        // Events
        private async Task OnDeleteActivePortfolio()
        {
            await OnDeleteActivePortfolioDelegate.InvokeAsync();
            await OnSelectPortfolioActivePortfolio(0);
        }

        private async Task OnSelectPortfolioActivePortfolio(int index)
        {
            await OnSelectPortfolioDelegate.Invoke(index);
            ChartDataModel = PortfolioAssetsChartData();
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
        public float Allocation { get; set; }
    }
}