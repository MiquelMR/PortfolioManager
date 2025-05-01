using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Admin
    {
        [Inject]
        private AdminViewModel AdminViewModel { get; set; }
        public List<Asset> AllAssets { get; set; } = [];
        public Asset FocusAsset { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await AdminViewModel.InitAsync();
            AllAssets = AdminViewModel.Assets;
            FocusAsset = AllAssets[0];
        }

        private void SelectAsset(int index)
        {
            FocusAsset = AllAssets[index];
        }
    }
}