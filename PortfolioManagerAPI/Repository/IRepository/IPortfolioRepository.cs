using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolioByIdAsync(int portfolioId);
        Task<List<Portfolio>> GetPortfoliosByUserEmailAsync(string userEmail);
        Task<bool> CreatePortfolioAsync(Portfolio newPortfolio);
        Task<bool> ExistsByIdAsync(int portfolioId);
    }
}
