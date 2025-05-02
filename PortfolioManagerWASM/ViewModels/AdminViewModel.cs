using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class AdminViewModel(IAssetService assetService)
    {
        private readonly IAssetService _assetService = assetService;

        public List<Asset> Assets { get; set; }

        public async Task InitAsync()
        {
            Assets = (await _assetService.GetAssetsAsync()).ToList();
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            return await _assetService.CreateAssetAsync(asset);
        }

        public async Task<Asset> UpdatePublicProfileAsync(Asset updateAsset)
        {
            return await _assetService.UpdateAssetAsync(updateAsset);
        }

        public async Task<bool> DeleteAssetAsync(Asset asset)
        {
            return await _assetService.DeleteAssetAsync(asset);
        }
    }
}
