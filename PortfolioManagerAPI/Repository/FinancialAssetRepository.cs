using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Repository
{
    public class FinancialAssetRepository(ApplicationDbContext db) : IFinancialAssetRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<FinancialAsset> GetFinancialAssetByIdAsync(int assetId)
        {
            try
            {
                return await _db.Assets.FirstOrDefaultAsync(asset => asset.AssetId == assetId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<FinancialAsset>> GetFinancialAssetsAsync()
        {
            try
            {
                return await _db.Assets.OrderBy(asset => asset.AssetId).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<FinancialAsset> CreateAssetAsync(FinancialAsset newFinancialAsset)
        {
            try
            {
                var financialAssetCreated = await _db.Assets.AddAsync(newFinancialAsset);
                var success = await _db.SaveChangesAsync() > 0;
                if (success)
                    return financialAssetCreated.Entity;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<FinancialAsset> UpdateAssetAsync(FinancialAsset updateFinancialAsset)
        {
            try
            {
                var financialAssetUpdated = _db.Assets.Update(updateFinancialAsset);
                var success = await _db.SaveChangesAsync() > 0;
                if (success)
                    return financialAssetUpdated.Entity;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAssetByIdAsync(int assetId)
        {
            try
            {
                var asset = await _db.Assets.FirstOrDefaultAsync(asset => asset.AssetId == assetId);
                if (asset == null)
                    return false;

                _db.Assets.Remove(asset);
                var success = await _db.SaveChangesAsync() > 0;
                return success;
            }
            catch
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
            catch
            {
                return false;
            }
        }
        public async Task<int> GetFinancialAssetIdByNameAsync(string name)
        {
            try
            {
                var financialAsset = await _db.Assets.FirstOrDefaultAsync(financialAsset => financialAsset.Name == name);
                return financialAsset.AssetId;
            }
            catch
            {
                return 0;
            }
        }
    }
}
