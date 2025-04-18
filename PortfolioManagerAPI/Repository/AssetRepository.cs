using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AssetRepository> _logger;

        public AssetRepository(ApplicationDbContext db, ILogger<AssetRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Asset asset)
        {
            try
            {
                _db.Assets.Add(asset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating asset");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating asset");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting asset");
                return false;
            }
        }

        public async Task<Asset> GetByIdAsync(int assetId)
        {
            try
            {
                return await _db.Assets.FirstOrDefaultAsync(asset => asset.AssetId == assetId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset");
                return null;
            }
        }
        public async Task<Asset> GetByNameAsync(string name)
        {
            try
            {
                return await _db.Assets.FirstOrDefaultAsync(asset => asset.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset");
                return null;
            }
        }

        public async Task<ICollection<Asset>> GetAllAsync()
        {
            try
            {
                return await _db.Assets.OrderBy(asset => asset.AssetId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting assets");
                return [];
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            try
            {
                return await _db.Assets.AnyAsync(asset => asset.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking asset");
                return false;
            }
        }
    }
}
