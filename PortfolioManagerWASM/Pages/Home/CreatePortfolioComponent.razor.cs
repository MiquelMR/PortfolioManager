using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Pages.Home
{
    public partial class CreatePortfolioComponent
    {
        [Parameter]
        public List<FinancialAsset> FinancialAssets { get; set; } = [];
        [Parameter]
        public EventCallback<Portfolio> OnPortfolioSubmit { get; set; }
        private readonly Portfolio newPortfolio = new() { Name = "My new Portfolio" };

        private void SubmitNewPortfolio()
        {
            OnPortfolioSubmit.InvokeAsync(newPortfolio);
        }

        private void AddFinancialAsset(FinancialAsset financialAsset)
        {
            var portfolioAsset = new PortfolioAsset()
            {
                FinancialAsset = financialAsset,
                AllocationPercentage = 0
            };
            newPortfolio.PortfolioAssets.Add(portfolioAsset);
        }
        private void OnRemoveFinancialAsset(PortfolioAsset portfolioAsset)
        {
            newPortfolio.PortfolioAssets.Remove(portfolioAsset);
        }




    }
}