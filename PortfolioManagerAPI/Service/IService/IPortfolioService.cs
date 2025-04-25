using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IPortfolioService
    {
        Task<ICollection<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail);
        Task<PortfolioDto> GetPortfolioById(int portfolioId);
    }
}
