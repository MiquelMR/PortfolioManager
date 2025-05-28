using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
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
            catch
            {
                return null;
            }
        }

        public async Task<List<Portfolio>> GetPortfoliosAsync()
        {
            try
            {
                return await _db.Portfolios
                    .OrderBy(portfolio => portfolio.PortfolioId)
                    .ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Portfolio>> GetPortfoliosByAccessibility(Accessibility accessibility)
        {
            try
            {
                return await _db.Portfolios
                    .Where(portfolio => portfolio.Accessibility == accessibility)
                    .OrderBy(portfolio => portfolio.PortfolioId)
                    .ToListAsync();
            }
            catch
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
            catch
            {
                return null;
            }
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio newPortfolio)
        {
            try
            {
                var portfolioCreated = await _db.Portfolios.AddAsync(newPortfolio);
                var success = await _db.SaveChangesAsync() > 0;
                if (success) return
                        portfolioCreated.Entity;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeletePortfolioByIdAsync(int portfolioId)
        {
            try
            {
                var portfolio = await _db.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.PortfolioId == portfolioId);
                if (portfolio == null)
                    return false;
                _db.Portfolios.Remove(portfolio);
                var success = await _db.SaveChangesAsync() > 0;
                return success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExistsByIdAsync(int portfolioId)
        {
            try
            {
                return await _db.Portfolios.AnyAsync(p => p.PortfolioId == portfolioId);
            }
            catch
            {
                return false;
            }

        }
    }
}
