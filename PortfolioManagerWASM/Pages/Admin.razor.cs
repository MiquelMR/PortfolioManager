using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages
{
    public partial class Admin
    {
        // Dependencies
        [Inject] private AdminViewModel AdminViewModel { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        // Delegates
        public Action<FinancialAsset> OnUpdateFinancialAssetDelegate { get; set; }

        // Properties
        public List<FinancialAsset> FinancialAssets { get; set; } = [];

        // Private fields
        private AdminView _adminView = AdminView.Overview;

        protected override async Task OnInitializedAsync()
        {
            await AdminViewModel.InitAsync();

            FinancialAssets = AdminViewModel.Assets;
            OnUpdateFinancialAssetDelegate = OnUpdateFinancialAsset;
        }

        private void OnTabClick(AdminView adminView)
        {
            _adminView = adminView;
        }
        public async void OnUpdateFinancialAsset(FinancialAsset financialAsset)
        {
            await AdminViewModel.UpdateFinancialAssetAsync(financialAsset);
        }
    }

    public enum AdminView
    {
        Overview,
        FinancialAssets,
        Users
    }
}