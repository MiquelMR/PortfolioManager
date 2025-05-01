using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolioByIdAsync(int portfolioId);
        Task <ICollection<Portfolio>> GetPortfoliosByUserEmailAsync(string userEmail);
    }
}
