using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IAssetService
    {
        Task<bool> CreateAsync(AssetDto asset);
        Task<bool> UpdateAsync(AssetDto asset);
        Task<bool> DeleteAsync(string name);
        Task<ICollection<AssetDto>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
    }
}
