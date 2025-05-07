using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Pages.Home
{
    public partial class CreatePortfolioComponent
    {
        [Parameter]
        public List<Asset> FinancialAssets { get; set; } = [];
        [Parameter]
        public EventCallback<Portfolio> OnPortfolioSubmit { get; set; }
        private readonly Portfolio newPortfolio = new();

        private void SubmitNewPortfolio()
        {
            OnPortfolioSubmit.InvokeAsync(newPortfolio);
        }

        private void AddFinancialAsset(Asset financialAsset)
        {
            var portfolioAsset = new PortfolioAsset()
            {
                Asset = financialAsset,
                AllocationPercentage = 10
            };
            newPortfolio.PortfolioAssets.Add(portfolioAsset);
        }


    }
}