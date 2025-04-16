using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IAssetRepository
    {
        Task<ICollection<Asset>> GetAssetsAsync();
        Task<Asset> GetAssetByIdAsync(int assetId);
        Task<Asset> GetAssetByNameAsync(string name);
        Task<bool> ExistsByIdAsync(int assetId);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> CreateAssetAsync(Asset asset);
        Task<bool> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetByNameAsync(string name);
    }
}
