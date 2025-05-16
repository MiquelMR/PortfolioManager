using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class AdminViewModel(IFinancialAssetService assetService, IUserService userService, IPortfolioService portfolioService)
    {
        // Dependencies
        private readonly IFinancialAssetService _assetService = assetService;
        private readonly IUserService _userService = userService;
        private readonly IPortfolioService _portfolioService = portfolioService;

        // Properties
        public List<FinancialAsset> FinancialAssets { get; set; }
        public List<Portfolio> PortfoliosBasicInfo { get; set; }
        public List<User> Users { get; set; }

        public async Task InitAsync()
        {
            FinancialAssets = (await _assetService.GetFinancialAssetsAsync()).ToList();
            PortfoliosBasicInfo = (await _portfolioService.GetPortfoliosBasicInfoAsync()).ToList();
            Users = (await _userService.GetUsersAsync());
            // Mira a ver si el vm coge las cabeceras
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
