using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IFinancialAssetRepository
    {
        Task<FinancialAsset> GetFinancialAssetByIdAsync(int assetId);
        Task<List<FinancialAsset>> GetFinancialAssetsAsync();
        Task<FinancialAsset> CreateAssetAsync(FinancialAsset asset);
        Task<FinancialAsset> UpdateAssetAsync(FinancialAsset asset);
        Task<bool> DeleteAssetByIdAsync(int assetId);
        Task<bool> ExistsByIdAsync(int assetId);
        Task<int> GetFinancialAssetIdByNameAsync(string name);
    }
}
