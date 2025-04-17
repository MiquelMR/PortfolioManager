using Microsoft.EntityFrameworkCore;
using PortfolioManagerAPI.Data;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;
using System.Xml.Linq;

namespace PortfolioManagerAPI.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PortfolioRepository> _logger;

        public PortfolioRepository(ApplicationDbContext db, ILogger<PortfolioRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(Portfolio portfolio)
        {
            _db.Portfolios.Add(portfolio);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Portfolio portfolio)
        {
            try
            {
                _db.Portfolios.Update(portfolio);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating portfolio");
                return false;
            }
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            try
            {
                var portfolio = await _db.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.Name == name);
                if (portfolio == null) return false;

                _db.Portfolios.Remove(portfolio);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting portfolio");
                return false;
            }
        }
        public async Task<Portfolio> GetByNameAsync(string name)
        {
            try
            {
                return await _db.Portfolios.FirstOrDefaultAsync(portfolio => portfolio.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving portfolio");
                return null;
            }
        }

        public async Task<ICollection<Portfolio>> GetAllByUserAsync(string userEmail)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
            try
            {
                return await _db.Portfolios
                    .OrderBy(portfolio => portfolio.PortfolioId)
                    .Where(portfolio => portfolio.UserId == user.UserId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting portfolios");
                return [];
            }
        }
        public async Task<ICollection<Portfolio>> GetAllAsync()
        {
            try
            {
                return await _db.Portfolios.OrderBy(portfolio => portfolio.PortfolioId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting portfolios");
                return [];
            }
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            try
            {
                return await _db.Portfolios.AnyAsync(portfolio => portfolio.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking portfolio");
                return false;
            }
        }
    }
}
