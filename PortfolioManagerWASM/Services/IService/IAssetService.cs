using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IAssetService
    {
        public Task<List<Asset>> GetAssetsAsync();
        public Task<Asset> CreateAssetAsync(Asset asset);
        public Task<Asset> UpdateAssetAsync(Asset asset);
        public Task<bool> DeleteAssetAsync(Asset asset);
    }
}
