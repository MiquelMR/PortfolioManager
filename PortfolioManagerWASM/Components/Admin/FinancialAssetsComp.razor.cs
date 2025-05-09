using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.Admin
{
    public partial class FinancialAssetsComp
    {
        // Properties
        [Parameter] public List<FinancialAsset> FinancialAssets { get; set; } = [];

        // Delegates
        [Parameter] public Action<FinancialAsset> OnUpdateFinancialAssetDelegate { get; set; }

        // Private fields
        private readonly FinancialAsset newFinancialAsset = new() { IconPath = AppConfig.GetDefaultIcon("AssetIcons") };

        private void OnCreateSubmit(FinancialAsset financialAsset)
        {
            //OnCreateFinancialAssetDelegate.Invoke(financialAsset);
        }

        private void OnUpdateSubmit(FinancialAsset financialAsset)
        {
            OnUpdateFinancialAssetDelegate.Invoke(financialAsset);
        }

        private void OnSelectIcon(string iconPath, FinancialAsset financialAsset)
        {
            var index = FinancialAssets.IndexOf(financialAsset);
            if (index > -1)
                FinancialAssets[index].IconPath = iconPath;
            else
                newFinancialAsset.IconPath = iconPath;
        }

        // Borrar esta putisima mierda
        private List<string> GetIconPaths()
        {
            return new List<string>
            {
                "icons/assets/default.svg",
                "icons/assets/gold.svg"
            };
        }
    }
}