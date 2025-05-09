using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class AdminViewModel(IAssetService assetService)
    {
        private readonly IAssetService _assetService = assetService;

        public List<FinancialAsset> Assets { get; set; }

        public async Task InitAsync()
        {
            Assets = (await _assetService.GetFinancialAssetsAsync()).ToList();
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
    }
}
