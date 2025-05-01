using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IAssetRepository
    {
        Task<Asset> GetByNameAsync(string name);
        Task<Asset> GetByIdAsync(int assetId);
        Task<ICollection<Asset>> GetAssetsAsync();
        Task<bool> CreateAsync(Asset asset);
        Task<bool> UpdateAsync(Asset asset);
        Task<bool> DeleteByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name);
    }
}
