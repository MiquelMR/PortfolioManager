using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.Admin
{
    public partial class FinancialAssetsComp
    {
        // Properties
        [Parameter] public List<FinancialAsset> FinancialAssets { get; set; } = [];

        // Delegates
        [Parameter] public Action<FinancialAsset> OnUpdateFinancialAssetDelegate { get; set; }

        private void OnUpdateSubmit(FinancialAsset financialAsset)
        {
            OnUpdateFinancialAssetDelegate.Invoke(financialAsset);
        }

        private void OnSelectIcon(string iconPath, FinancialAsset financialAsset)
        {
            var index = FinancialAssets.IndexOf(financialAsset);
            FinancialAssets[index].IconPath = iconPath;
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