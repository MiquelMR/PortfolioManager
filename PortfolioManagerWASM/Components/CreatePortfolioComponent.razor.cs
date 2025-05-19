using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Pages;

namespace PortfolioManagerWASM.Components
{
    public partial class CreatePortfolioComponent
    {
        // Properties
        [Parameter] public List<FinancialAsset> FinancialAssets { get; set; } = [];

        // Delegates
        [Parameter] public EventCallback<Portfolio> OnPortfolioSubmitDelegate { get; set; }
        [Parameter] public EventCallback<HomeView> OnClickBackButtonDelegate { get; set; }

        // Private fields
        private readonly Portfolio newPortfolio = new() { Name = "My new Portfolio" };

        // Events
        private void OnSubmitNewPortfolio()
        {
            OnPortfolioSubmitDelegate.InvokeAsync(newPortfolio);
        }

        private void OnAddFinancialAsset(FinancialAsset financialAsset)
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

        private void OnClickBackButton(HomeView HomeView)
        {
            OnClickBackButtonDelegate.InvokeAsync(HomeView);
        }
    }
}