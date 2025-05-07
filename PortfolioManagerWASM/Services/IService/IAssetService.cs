using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IAssetService
    {
        public Task<List<FinancialAsset>> GetAssetsAsync();
        public Task<FinancialAsset> CreateAssetAsync(FinancialAsset asset);
        public Task<FinancialAsset> UpdateAssetAsync(FinancialAsset asset);
        public Task<bool> DeleteAssetAsync(FinancialAsset asset);
    }
}
