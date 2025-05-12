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

        private void OnSubmitFinancialAsset(FinancialAsset financialAsset)
        {
            OnCreateFinancialAssetDelegate.Invoke(financialAsset);
        }

        private void OnUpdateFinancialAsset(FinancialAsset financialAsset)
        {
            OnUpdateFinancialAssetDelegate.Invoke(financialAsset);
        }
        private void OnDeleteFinancialAsset(FinancialAsset financialAsset)
        {
            OnDeleteFinancialAssetDelegate.Invoke(financialAsset);
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
        private static List<string> GetIconPaths()
        {
            return new List<string>()
            {
                "default.svg",
                "gold.svg"
            }
            .Select(item => Path.Combine(AppConfig.GetResourcePath("AssetIcons"), item))
            .ToList();
        }
    }
}