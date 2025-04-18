using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;
using System.Threading;
using System.Xml.Linq;

namespace PortfolioManagerAPI.Repository
{
    public class PortfolioAssetRepository : IPortfolioAssetsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PortfolioAssetRepository> _logger;

        public PortfolioAssetRepository(ApplicationDbContext db, ILogger<PortfolioAssetRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(PortfolioAsset portfolioAsset)
        {
            _db.PortfolioAssets.Add(portfolioAsset);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(PortfolioAsset portfolioAsset)
        {
            try
            {
                _db.PortfolioAssets.Update(portfolioAsset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating portfolioAsset");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(PortfolioAsset portfolioAsset)
        {
            try
            {
                if (portfolioAsset == null) return false;

                _db.PortfolioAssets.Remove(portfolioAsset);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting portfolioAsset");
                return false;
            }
        }

        public async Task<ICollection<PortfolioAsset>> GetAllByPortfolioAsync(int portfolioId)
        {
            try
            {
                return await _db.PortfolioAssets
                    .Include(pa => pa.Portfolio)
                    .Include(pa => pa.Asset)
                    .Where(pa => pa.PortfolioId == portfolioId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting portfolioAssets");
                return [];
            }
        }
    }
}