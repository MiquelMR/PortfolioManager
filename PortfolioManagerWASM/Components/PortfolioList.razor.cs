using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Components
{
    public partial class PortfolioList
    {
        [Parameter] public List<Portfolio> PortfoliosBasicInfo { get; set; }

        private void OnSelectPortfolio(int index)
        {

        }
    }
}