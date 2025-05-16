using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Components.Home;
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
        public Action<FinancialAsset> OnCreateFinancialAssetDelegate { get; set; }
        public Action<FinancialAsset> OnDeleteFinancialAssetDelegate { get; set; }
        public Action<User> OnDeleteUserDelegate { get; set; }

        // Properties
        public List<FinancialAsset> FinancialAssets { get; set; } = [];
        public List<User> Users { get; set; }

        // Private fields
        private AdminView _adminView = AdminView.Overview;

        protected override async Task OnInitializedAsync()
        {
            await AdminViewModel.InitAsync();

            FinancialAssets = AdminViewModel.FinancialAssets;
            Users = AdminViewModel.Users;
            OnUpdateFinancialAssetDelegate = OnUpdateFinancialAsset;
            OnCreateFinancialAssetDelegate = OnCreateFinancialAsset;
            OnDeleteFinancialAssetDelegate = OnDeleteFinancialAsset;
            OnDeleteUserDelegate = OnDeleteUser;
        }

        public async void OnCreateFinancialAsset(FinancialAsset financialAsset)
        {
            await AdminViewModel.CreateFinancialAssetAsync(financialAsset);
        }

        public async void OnUpdateFinancialAsset(FinancialAsset financialAsset)
        {
            await AdminViewModel.UpdateFinancialAssetAsync(financialAsset);
        }

        public async void OnDeleteFinancialAsset(FinancialAsset financialAsset)
        {
            await AdminViewModel.DeleteFinancialAssetAsync(financialAsset);
        }
        public async void OnDeleteUser(User user)
        {
            await AdminViewModel.DeleteUserAsync(user);
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