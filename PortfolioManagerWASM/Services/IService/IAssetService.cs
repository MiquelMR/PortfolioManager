using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IAssetService
    {
        public Task<IEnumerable<Asset>> GetAssets();
        public Task<Asset> GetAsset(int AssetId);
        public Task<Asset> CreateAsset(Asset asset);
        public Task<Asset> UpdateAsset(int AssetId, Asset asset);
        public Task<bool> DeleteAsset(int AssetId);
    }
}
