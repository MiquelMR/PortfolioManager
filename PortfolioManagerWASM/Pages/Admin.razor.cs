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
        public Action OnDeletePortfolioDelegate { get; set; }
        public Func<int, Task> OnSelectPortfolioDelegate { get; set; }

        // Properties
        public List<FinancialAsset> FinancialAssets { get; set; } = [];
        public List<User> Users { get; set; }
        public List<Portfolio> PortfoliosBasicInfo { get; set; } = [];
        public Portfolio ActivePortfolio { get; set; } = new();

        // Private fields
        private AdminView _adminView = AdminView.Overview;

        protected override async Task OnInitializedAsync()
        {
            await AdminViewModel.InitAsync();

            FinancialAssets = AdminViewModel.FinancialAssets;
            Users = AdminViewModel.Users;
            ActivePortfolio = AdminViewModel.ActivePortfolio;
            PortfoliosBasicInfo = AdminViewModel.PortfoliosBasicInfo;
            OnUpdateFinancialAssetDelegate = OnUpdateFinancialAsset;
            OnCreateFinancialAssetDelegate = OnCreateFinancialAsset;
            OnDeleteFinancialAssetDelegate = OnDeleteFinancialAsset;
            OnDeleteUserDelegate = OnDeleteUser;
            OnSelectPortfolioDelegate = OnSelectPortfolio;
            OnDeletePortfolioDelegate = OnDeletePortfolio;
        }

        public async Task OnSelectPortfolio(int index)
        {
            await AdminViewModel.OnSelectPortfolioAsync(index);
            ActivePortfolio = AdminViewModel.ActivePortfolio;
            StateHasChanged();
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
        public async void OnDeletePortfolio()
        {
            await AdminViewModel.DeleteActivePortfolioAsync();
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
        Portfolios,
        Users
    }
}