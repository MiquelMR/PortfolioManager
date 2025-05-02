using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IAssetRepository
    {
        Task<Asset> GetByIdAsync(int assetId);
        Task<ICollection<Asset>> GetAssetsAsync();
        Task<Asset> CreateAssetAsync(Asset asset);
        Task<Asset> UpdateAsync(Asset asset);
        Task<bool> DeleteAssetByIdAsync(int assetId);
        Task<bool> ExistsByIdAsync(int assetId);
    }
}
