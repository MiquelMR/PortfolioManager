using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;
using System.Threading;
using System.Xml.Linq;

namespace PortfolioManagerAPI.Repository
{
    public class PortfolioAssetRepository(ApplicationDbContext db) : IPortfolioAssetRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<List<PortfolioAsset>> GetPortfolioAssetsByPortfolioIdAsync(int portfolioId)
        {
            try
            {
                return await _db.PortfolioAssets
                    .Include(pa => pa.FinancialAsset)
                    .Where(pa => pa.PortfolioId == portfolioId)
                    .ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<PortfolioAsset> CreatePortfolioAssetAsync(PortfolioAsset newPortfolioAsset)
        {
            try
            {
                var portfolioAssetCreated = await _db.PortfolioAssets.AddAsync(newPortfolioAsset);
                var success = await _db.SaveChangesAsync() > 0;
                if (success)
                    return portfolioAssetCreated.Entity;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
    }
}