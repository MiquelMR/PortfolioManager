using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IAssetService
    {
        Task<ICollection<AssetDto>> GetAssetsAsync();
    }
}
