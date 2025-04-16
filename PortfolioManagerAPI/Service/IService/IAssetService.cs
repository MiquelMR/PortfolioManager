using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IAssetService
    {
        Task<ICollection<AssetDto>> GetAssetsAsync();
        Task<AssetDto> GetAssetByNameAsync(string name);
        Task<bool> CreateAssetAsync(Asset asset);
        Task<bool> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetAsync(string name);
        Task<bool> ExistsByNameAsync(string name);
    }
}
