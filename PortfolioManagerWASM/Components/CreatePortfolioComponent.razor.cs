using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Pages;
using Syncfusion.Blazor.Data;

namespace PortfolioManagerWASM.Components
{
    public partial class CreatePortfolioComponent
    {
        // Services
        [Inject] HttpClient HttpClient { get; set; }

        // Properties
        [Parameter] public List<FinancialAsset> FinancialAssets { get; set; }
        private List<FinancialAsset> FilteredFinancialAssets
        {
            get
            {
                var financialAssetsFilteredByUniqueness = new List<FinancialAsset>(FinancialAssets);
                newPortfolio.PortfolioAssets.ForEach(asset => financialAssetsFilteredByUniqueness.Remove(asset.FinancialAsset));
                return financialAssetsFilteredByUniqueness;
            }
        }

        // Delegates
        [Parameter] public EventCallback<Portfolio> OnPortfolioSubmitDelegate { get; set; }
        [Parameter] public EventCallback<HomeView> OnClickBackButtonDelegate { get; set; }

        // Private fields
        private Portfolio newPortfolio = new() { Name = "My new Portfolio", IconPath = AppConfig.GetResourcePath("PortfolioIcons") + "/default.svg" };

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

        private List<string> GetIconPaths()
        {
            var dir = HttpClient.GetStringAsync("icons/portfolios");
            return new List<string>()
            {
                "default.svg"
            }
            .Select(item => Path.Combine(AppConfig.GetResourcePath("PortfolioIcons"), item))
            .ToList();
        }

        private void OnSelectIcon(string iconPath)
        {
            newPortfolio.IconPath = iconPath;
        }
    }
}