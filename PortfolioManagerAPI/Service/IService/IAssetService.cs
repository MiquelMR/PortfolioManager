using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IAssetService
    {
        Task<List<AssetDto>> GetAssetsAsync();
        Task<AssetDto> CreateAssetAsync(AssetDto assetDto);
        Task<AssetDto> UpdateAssetAsync(AssetDto assetDto);
        Task<bool> DeleteAssetByIdAsync(int assetId);
        Task<bool> ExistsByIdAsync(int assetId);
    }
}
