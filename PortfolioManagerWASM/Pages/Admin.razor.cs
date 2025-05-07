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

        // Properties
        public List<FinancialAsset> FinancialAssets { get; set; } = [];

        // Private fields
        private AdminView _adminView = AdminView.Overview;

        protected override async Task OnInitializedAsync()
        {
            await AdminViewModel.InitAsync();

            FinancialAssets = AdminViewModel.Assets;
        }

        private void OnTabClick(AdminView adminView)
        {
            _adminView = adminView;
        }
    }

    public enum AdminView
    {
        Overview,
        FinancialAssets,
        Users
    }
}