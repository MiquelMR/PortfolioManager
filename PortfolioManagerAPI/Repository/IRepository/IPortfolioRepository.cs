using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolioByIdAsync(int portfolioId);
        Task<List<Portfolio>> GetPortfoliosAsync();
        Task<List<Portfolio>> GetPortfoliosByUserEmailAsync(string userEmail);
        Task<Portfolio> CreatePortfolioAsync(Portfolio newPortfolio);
        Task<bool> DeletePortfolioByIdAsync(int portfolioId);
        Task<bool> ExistsByIdAsync(int portfolioId);
    }
}
