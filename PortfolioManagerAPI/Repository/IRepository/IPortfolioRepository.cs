using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioRepository
    {
        Task<bool> CreateAsync(Portfolio portfolio);
        Task<bool> UpdateAsync(Portfolio portfolio);
        Task<bool> DeleteByNameAsync(string name);
        Task<Portfolio> GetByNameAsync(string name);
        Task<ICollection<Portfolio>> GetAllByUserAsync(string userEmail);
        Task<ICollection<Portfolio>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
    }
}
