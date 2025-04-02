using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IAssetRepository
    {
        ICollection<Asset> GetAsset();
        Asset GetAssetById(int AssetId);
        bool ExistsById(int AssetId);

        bool CreateAsset(Asset asset);
        bool UpdateAsset(Asset asset);
        bool DeleteAsset(Asset asset);
        bool Save();
    }
}
