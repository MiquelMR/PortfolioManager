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
            Assets = (await _assetService.GetAssetsAsync()).ToList();
        }

        public async Task<FinancialAsset> CreateAssetAsync(FinancialAsset asset)
        {
            return await _assetService.CreateAssetAsync(asset);
        }

        public async Task<FinancialAsset> UpdatePublicProfileAsync(FinancialAsset updateAsset)
        {
            return await _assetService.UpdateAssetAsync(updateAsset);
        }

        public async Task<bool> DeleteAssetAsync(FinancialAsset asset)
        {
            return await _assetService.DeleteAssetAsync(asset);
        }
    }
}
