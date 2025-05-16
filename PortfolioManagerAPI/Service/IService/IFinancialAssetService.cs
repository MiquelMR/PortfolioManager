using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IFinancialAssetService
    {
        Task<List<FinancialAssetDto>> GetFinancialAssetsAsync();
        Task<FinancialAssetDto> CreateFinancialAssetAsync(FinancialAssetDto newFinancialAssetDto);
        Task<FinancialAssetDto> UpdateFinancialAssetAsync(FinancialAssetDto updateFinancialAssetDto);
        Task<bool> DeleteAssetByIdAsync(int assetId);
        Task<bool> ExistsByIdAsync(int assetId);
    }
}
