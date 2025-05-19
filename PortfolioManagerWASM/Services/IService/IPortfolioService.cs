using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IPortfolioService
    {
        public Task<List<Portfolio>> GetPortfoliosBasicInfoAsync();
        public Task<List<Portfolio>> GetPublicPortfolios();
        public Task<List<Portfolio>> GetPortfoliosBasicInfoByUserAsync(string userEmail);
        public Task<Portfolio> GetPortfolioByIdAsync(int id);
        public Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
        public Task<bool> DeletePortfolioAsync(int portfolioId);
    }
}
