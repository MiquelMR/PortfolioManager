using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreateAssetAsync(Asset asset)
        {
            _db.Assets.Add(asset);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAssetAsync(Asset asset)
        {
            _db.Assets.Update(asset);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAssetAsync(Asset asset)
        {
            _db.Assets.Remove(asset);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsByIdAsync(int assetId)
        {
            bool result = await _db.Assets.AnyAsync(asset => asset.AssetId == assetId);
            return result;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            bool result = await _db.Assets.AnyAsync(asset => asset.Name == name);
            return result;
        }

        public async Task<Asset> GetAssetByIdAsync(int assetId)
        {
            return await _db.Assets.FirstOrDefaultAsync(asset => asset.AssetId == assetId);
        }
        public async Task<Asset> GetAssetByNameAsync(string name)
        {
            return await _db.Assets.FirstOrDefaultAsync(asset => asset.Name == name);
        }

        public async Task<ICollection<Asset>> GetAssetsAsync()
        {
            return await _db.Assets.OrderBy(asset => asset.AssetId).ToListAsync();
        }
    }
}
