using PortfolioManagerAPI.Models.DTOs;

namespace PortfolioManagerAPI.Service.IService
{
    public interface IPortfolioService
    {
        Task<PortfolioDto> GetPortfolioById(int portfolioId);
        Task<List<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail);
        Task<PortfolioDto> CreatePortfolioAsync(PortfolioDto portfolioDto);
        Task<bool> DeletePortfolioByIdAsync(int portfolioId);
        Task<bool> ExistsByIdAsync(int portfolioId);
    }
}
