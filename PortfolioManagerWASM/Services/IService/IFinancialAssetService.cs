using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IFinancialAssetService
    {
        public Task<List<FinancialAsset>> GetFinancialAssetsAsync();
        public Task<FinancialAsset> CreateFinancialAssetAsync(FinancialAsset financialAsset);
        public Task<FinancialAsset> UpdateFinancialAssetAsync(FinancialAsset financialAsset);
        public Task<bool> DeleteFinancialAssetAsync(FinancialAsset financialAsset);
    }
}
