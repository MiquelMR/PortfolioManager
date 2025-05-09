using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IAssetRepository
    {
        Task<FinancialAsset> GetAssetByIdAsync(int assetId);
        Task<List<FinancialAsset>> GetAssetsAsync();
        Task<bool> CreateAssetAsync(FinancialAsset asset);
        Task<bool> UpdateAssetAsync(FinancialAsset asset);
        Task<bool> DeleteAssetByIdAsync(int assetId);
        Task<bool> ExistsByIdAsync(int assetId);
    }
}
