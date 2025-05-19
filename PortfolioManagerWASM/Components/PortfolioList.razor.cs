using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Components.Home;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components
{
    public partial class PortfolioList
    {
        //Properties
        [Parameter] public List<Portfolio> PortfoliosBasicInfo { get; set; }
        [Parameter] public Portfolio PortfolioExpanded { get; set; } = new();
        [Parameter] public Func<int, Task> OnExpandPortfolioInformationDelegate { get; set; }

        // Private fields
        private List<ChartDataModel> ChartDataModel { get; set; } = [];

        private void OnSelectPortfolio(int portfolioId)
        {
            OnExpandPortfolioInformationDelegate.Invoke(portfolioId);
            StateHasChanged();
        }

        private List<ChartDataModel> PortfolioAssetsChartData()
        {
            List<ChartDataModel> chartDataModel = [];
            foreach (PortfolioAsset portfolioAsset in PortfolioExpanded.PortfolioAssets)
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