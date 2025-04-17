using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IPortfolioService
    {
        Task<bool> CreateAsync(PortfolioDto portfolio, string userEmail);
        Task<bool> UpdateAsync(PortfolioDto portfolio);
        Task<bool> DeleteAsync(string name);
        Task<PortfolioDto> GetByNameAsync(string name);
        Task<ICollection<PortfolioDto>> GetAllByUserAsync(string userEmail);
        Task<ICollection<PortfolioDto>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
    }
}
