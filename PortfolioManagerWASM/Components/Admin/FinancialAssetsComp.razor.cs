using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components.Admin
{
    public partial class FinancialAssetsComp
    {
        // Properties
        [Parameter] public List<FinancialAsset> FinancialAssets { get; set; } = [];
    }
}