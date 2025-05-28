using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.Admin
{
    public partial class FinancialAssetsComp
    {
        // Services
        [Inject] HttpClient HttpClient { get; set; }
        // Properties
        [Parameter] public List<FinancialAsset> FinancialAssets { get; set; } = [];

        // Delegates
        [Parameter] public Action<FinancialAsset> OnUpdateFinancialAssetDelegate { get; set; }
        [Parameter] public Action<FinancialAsset> OnCreateFinancialAssetDelegate { get; set; }
        [Parameter] public Action<FinancialAsset> OnDeleteFinancialAssetDelegate { get; set; }

        // Private fields
        private readonly FinancialAsset newFinancialAsset = new() { IconPath = AppConfig.GetResourcePath("AssetIcons") + "/default.svg" };

        private void OnSubmitFinancialAsset(FinancialAsset newFinancialAsset)
        {
            OnCreateFinancialAssetDelegate.Invoke(newFinancialAsset);
            FinancialAssets.Add(newFinancialAsset);
        }

        private void OnUpdateFinancialAsset(FinancialAsset financialAsset)
        {
            OnUpdateFinancialAssetDelegate.Invoke(financialAsset);
        }
        private void OnDeleteFinancialAsset(FinancialAsset financialAsset)
        {
            OnDeleteFinancialAssetDelegate.Invoke(financialAsset);
            FinancialAssets.Remove(financialAsset);
        }

        private void OnSelectIcon(string iconPath, FinancialAsset financialAsset)
        {
            var index = FinancialAssets.IndexOf(financialAsset);
            if (index > -1)
                FinancialAssets[index].IconPath = iconPath;
            else
                newFinancialAsset.IconPath = iconPath;
        }

        // TODO : Pendiente de centralizar 
        private List<string> GetIconPaths()
        {
            var dir = HttpClient.GetStringAsync("icons/assets");
            return new List<string>()
            {
                "default.svg",
                "bank.svg",
                "bonds.svg",
                "cash.svg",
                "cloud-1.svg",
                "cloud-2.svg",
                "consumer.svg",
                "corporativebonds.svg",
                "dividend.svg",
                "domestic.svg",
                "domestic-bonds.svg",
                "energy.svg",
                "gold.svg",
                "growth.svg",
                "health.svg",
                "highyield.svg",
                "long.svg",
                "mortgage.svg",
                "private.svg",
                "real-estate-1.svg",
                "real-estate-2.svg",
                "short.svg",
                "small.svg",
                "tech.svg",
                "wheat.svg",
                "gem.svg",
                "crane.svg",
                "world.svg"
            }
            .Select(item => Path.Combine(AppConfig.GetResourcePath("AssetIcons"), item))
            .ToList();
        }
    }
}