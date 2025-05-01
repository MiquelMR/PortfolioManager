using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Repository
{
    public class AssetRepository(ApplicationDbContext db) : IAssetRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<bool> CreateAsync(Asset asset)
        {
            try
            {
                _db.Assets.Add(asset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Asset asset)
        {
            try
            {
                _db.Assets.Update(asset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            try
            {
                var asset = await _db.Assets.FirstOrDefaultAsync(asset => asset.Name == name);
                if (asset == null) return false;

                _db.Assets.Remove(asset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
        public async Task<Asset> GetByNameAsync(string name)
        {
            try
            {
                return await _db.Assets.FirstOrDefaultAsync(asset => asset.Name == name);
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

        public async Task<bool> ExistsByNameAsync(string name)
        {
            try
            {
                return await _db.Assets.AnyAsync(asset => asset.Name == name);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
