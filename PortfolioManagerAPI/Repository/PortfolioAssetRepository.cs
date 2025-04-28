using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;
using System.Threading;
using System.Xml.Linq;

namespace PortfolioManagerAPI.Repository
{
    public class PortfolioAssetRepository : IPortfolioAssetRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PortfolioAssetRepository> _logger;

        public PortfolioAssetRepository(ApplicationDbContext db, ILogger<PortfolioAssetRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ICollection<PortfolioAsset>> GetPortfolioAssetsByPortfolioIdAsync(int portfolioId)
        {
            try
            {
                return await _db.PortfolioAssets
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