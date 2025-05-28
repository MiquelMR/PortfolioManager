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
        private Portfolio newPortfolio = new()
        {
            IconPath = AppConfig.GetResourcePath("PortfolioIcons") + "/default.svg",
            Accessibility = Accessibility.Private
        };
        private bool isPublic;

        // Events
        private void OnSubmitNewPortfolio()
        {
            newPortfolio.Accessibility = isPublic ? Accessibility.Public : Accessibility.Private;
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
                    "default.svg",
                    "balance-scale-left.svg",
                    "briefcase.svg",
                    "budget.svg",
                    "business-value.svg",
                    "chart-histogram.svg",
                    "chart-pie-alt.svg",
                    "chart-pyramid.svg",
                    "cheap-dollar.svg",
                    "deposit-alt.svg",
                    "dollar.svg",
                    "euro.svg",
                    "plant-seed-invest.svg",
                    "revenue.svg",
                    "revenue-euro.svg",
                    "shopping-basket.svg",
                    "star-of-david.svg",
                    "stats.svg",
                    "tax-alt.svg",
                    "usd-circle.svg",
                    "house-chimney.svg",
                    "wallet.svg"
            }
            .Select(item => Path.Combine(AppConfig.GetResourcePath("PortfolioIcons"), item))
            .ToList();
        }

        private void OnSelectIcon(string iconPath)
        {
            newPortfolio.IconPath = iconPath;
        }

        private void OnAccessibilityCheck()
        {

        }
    }
}