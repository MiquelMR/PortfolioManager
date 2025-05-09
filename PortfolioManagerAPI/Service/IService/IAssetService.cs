using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IAssetService
    {
        Task<List<FinancialAssetDto>> GetAssetsAsync();
        Task<FinancialAssetDto> CreateAssetAsync(FinancialAssetDto assetDto);
        Task<FinancialAssetDto> UpdateAssetAsync(FinancialAssetDto assetDto);
        Task<bool> DeleteAssetByIdAsync(int assetId);
        Task<bool> ExistsByIdAsync(int assetId);
    }
}
