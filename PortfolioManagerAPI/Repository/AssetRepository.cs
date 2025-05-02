using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Repository
{
    public class AssetRepository(ApplicationDbContext db) : IAssetRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Asset> GetByIdAsync(int assetId)
        {
            try
            {
                return await _db.Assets.FirstOrDefaultAsync(asset => asset.AssetId == assetId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<Asset>> GetAssetsAsync()
        {
            try
            {
                return await _db.Assets.OrderBy(asset => asset.AssetId).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            try
            {
                var _asset = await _db.Assets.AddAsync(asset);
                await _db.SaveChangesAsync();
                return _asset.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Asset> UpdateAsync(Asset asset)
        {
            try
            {
                var _asset = _db.Assets.Update(asset);
                await _db.SaveChangesAsync();
                return _asset.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAssetByIdAsync(int assetId)
        {
            try
            {
                var asset = await _db.Assets.FirstOrDefaultAsync(asset => asset.AssetId == assetId);
                if (asset == null) return false;

                _db.Assets.Remove(asset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExistsByIdAsync(int assetId)
        {
            try
            {
                return await _db.Assets.AnyAsync(asset => asset.AssetId == assetId);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
