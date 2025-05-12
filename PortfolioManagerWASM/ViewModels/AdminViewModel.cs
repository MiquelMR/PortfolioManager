using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class AdminViewModel(IFinancialAssetService assetService, IUserService userService)
    {
        private readonly IFinancialAssetService _assetService = assetService;
        private readonly IUserService _userService = userService;

        public List<FinancialAsset> FinancialAssets { get; set; }
        public List<User> Users { get; set; }

        public async Task InitAsync()
        {
            FinancialAssets = (await _assetService.GetFinancialAssetsAsync()).ToList();
            Users = (await _userService.GetUsersAsync());
        }

        public async Task<FinancialAsset> CreateFinancialAssetAsync(FinancialAsset financialAsset)
        {
            return await _assetService.CreateFinancialAssetAsync(financialAsset);
        }

        public async Task<FinancialAsset> UpdateFinancialAssetAsync(FinancialAsset financialAsset)
        {
            return await _assetService.UpdateFinancialAssetAsync(financialAsset);
        }

        public async Task<bool> DeleteFinancialAssetAsync(FinancialAsset financialAsset)
        {
            return await _assetService.DeleteFinancialAssetAsync(financialAsset);
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            return await _userService.DeleteUserAsync(user);
        }
    }
}
