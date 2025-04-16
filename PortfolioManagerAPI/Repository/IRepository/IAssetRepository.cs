using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IAssetRepository
    {
        Task<bool> CreateAsync(Asset asset);
        Task<bool> UpdateAsync(Asset asset);
        Task<bool> DeleteByNameAsync(string name);
        Task<Asset> GetByNameAsync(string name);
        Task<ICollection<Asset>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
    }
}
