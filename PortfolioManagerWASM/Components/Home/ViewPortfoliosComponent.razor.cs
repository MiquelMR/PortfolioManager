using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Pages;

namespace PortfolioManagerWASM.Components.Home
{
    public partial class ViewPortfoliosComponent
    {
        [Parameter]
        public List<Portfolio> UserPortfolios { get; set; }
        [Parameter]
        public Portfolio ActivePortfolio { get; set; }
        [Parameter]
        public Func<int, Task> SelectPortfolioDelegate { get; set; }
        [Parameter] public EventCallback<HomeView> ChangeCurrentHomeViewDelegate { get; set; }
        [Parameter] public EventCallback OnDeleteActivePortfolioDelegate { get; set; }
        private List<ChartDataModel> ChartDataModel { get; set; } = [];

        private async Task OnDeleteActivePortfolio()
        {
            await OnDeleteActivePortfolioDelegate.InvokeAsync();
        }

        private async Task SelectPortfolio(int index)
        {
            await SelectPortfolioDelegate.Invoke(index);
            ChartDataModel = PortfolioAssetsChartData();
        }

        private void ChangeCurrentHomeView(HomeView HomeView)
        {
            ChangeCurrentHomeViewDelegate.InvokeAsync(HomeView);
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