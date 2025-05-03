using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;
using System.Xml.Linq;

namespace PortfolioManagerAPI.Repository
{
    public class PortfolioRepository(ApplicationDbContext db) : IPortfolioRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            try
            {
                return await _db.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.PortfolioId == portfolioId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Portfolio>> GetPortfoliosByUserEmailAsync(string userEmail)
        {
            var userId = await _db.Users
                .Where(user => user.Email == userEmail)
                .Select(user => user.UserId)
                .FirstOrDefaultAsync();

            if (userId == 0)
            {
                return null;
            }

            try
            {
                return await _db.Portfolios
                    .Where(portfolio => portfolio.UserId == userId)
                    .OrderBy(portfolio => portfolio.PortfolioId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
