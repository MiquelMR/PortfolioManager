using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Pages.Admin
{
    public partial class AdminViewComponent
    {
        [Parameter]
        public Asset ActiveAsset { get; set; }
    }
}