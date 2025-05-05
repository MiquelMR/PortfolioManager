using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IPortfolioService
    {
        Task<PortfolioDto> GetPortfolioById(int portfolioId);
        Task<List<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail);
        Task<bool> ExistsByIdAsync(int portfolioId);
    }
}
