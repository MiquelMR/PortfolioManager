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
                    .Include(pa => pa.Asset)
                    .Where(pa => pa.PortfolioId == portfolioId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}