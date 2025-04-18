using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IPortfolioService
    {
        public Task<ICollection<Portfolio>> GetAllByUserAsync(string userEmail);
        public Task<Portfolio> CreateAsync(Portfolio portfolio);
        public Task<Portfolio> UpdateAsync(Portfolio portfolio);
        public Task<bool> DeleteAsync(Portfolio portfolio);
    }
}
