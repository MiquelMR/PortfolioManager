using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.Admin
{
    public partial class OverviewPortfoliosComponent
    {
        // Delegates
        [Parameter] public EventCallback OnDeleteActivePortfolioDelegate { get; set; }
        [Parameter] public EventCallback OnEditActivePortfolioDelegate { get; set; }
        [Parameter] public Func<int, Task> OnSelectPortfolioDelegate { get; set; }

        // Properties
        [Parameter] public Portfolio ActivePortfolio { get; set; }
        [Parameter] public List<Portfolio> PortfoliosBasicInfo { get; set; }

        // Private fields
        private List<ChartDataModel> ChartDataModel { get; set; } = [];

        // Events
        private async Task OnDeleteActivePortfolio()
        {
            await OnDeleteActivePortfolioDelegate.InvokeAsync();
            var portfolioDeleted = PortfoliosBasicInfo.FirstOrDefault(portfolio => portfolio.PortfolioId == ActivePortfolio.PortfolioId);
            PortfoliosBasicInfo.Remove(portfolioDeleted);
            if (PortfoliosBasicInfo.Count > 0) await OnSelectPortfolio(0);
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