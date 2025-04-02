using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _db;

        public AssetRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateAsset(Asset asset)
        {
            _db.Assets.Add(asset);
            return Save();
        }

        public bool DeleteAsset(Asset asset)
        {
            _db.Assets.Remove(asset);
            return Save();
        }

        public bool ExistsById(int AssetId)
        {
            bool result = _db.Assets.Any(asset => asset.AssetId == AssetId);
            return result;
        }

        public Asset GetAssetById(int AssetId)
        {
            return _db.Assets.FirstOrDefault(asset => asset.AssetId == AssetId);
        }

        public ICollection<Asset> GetAsset()
        {
            return _db.Assets.OrderBy(asset => asset.AssetId).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool UpdateAsset(Asset asset)
        {
            _db.Assets.Update(asset);
            return Save();
        }
    }
}
