using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Pages.Admin
{
    public partial class Admin
    {
        [Inject]
        private AdminViewModel AdminViewModel { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Func<Asset, Task> UpdateAssetAsyncDelegate { get; set; }
        public Func<Asset, Task> CreateAssetAsyncDelegate { get; set; }
        public List<Asset> AllAssets { get; set; } = [];
        public Asset ActiveAsset { get; set; } = new();
        public Asset AssetCreate { get; set; } = new();
        private AdminView AdminView { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AdminViewModel.InitAsync();
            UpdateAssetAsyncDelegate = async (updateAsset) =>
            {
                ActiveAsset = await AdminViewModel.UpdatePublicProfileAsync(updateAsset);
                NavigationManager.NavigateTo("/admin", true);
            };
            CreateAssetAsyncDelegate = async (createAsset) =>
            {
                await AdminViewModel.CreateAssetAsync(createAsset);
                NavigationManager.NavigateTo("/admin", true);
            };
            AllAssets = AdminViewModel.Assets;
            ActiveAsset = AllAssets[0];
        }

        private async Task<bool> DeleteAssetAsync()
        {
            var success = await AdminViewModel.DeleteAssetAsync(ActiveAsset);
            if (success) NavigationManager.NavigateTo("/admin", true);
            return success;
        }

        private void SelectAsset(int index)
        {
            ActiveAsset = AllAssets[index];
        }

        private void ChangeView()
        {
            AdminView = AdminView == AdminView.View ? AdminView.Edit : AdminView.View;
        }

        private void ToCreateView()
        {
            AdminView = AdminView.Create;
        }
    }

    public enum AdminView
    {
        View,
        Edit,
        Create
    }
}