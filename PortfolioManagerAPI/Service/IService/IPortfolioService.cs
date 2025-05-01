using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IPortfolioService
    {
        Task<PortfolioDto> GetPortfolioById(int portfolioId);
        Task<ICollection<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail);
    }
}
