using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IAssetService
    {
        public Task<List<FinancialAsset>> GetFinancialAssetsAsync();
        public Task<FinancialAsset> CreateFinancialAssetAsync(FinancialAsset asset);
        public Task<FinancialAsset> UpdateFinancialAssetAsync(FinancialAsset asset);
        public Task<bool> DeleteFinancialAssetAsync(FinancialAsset asset);
    }
}
