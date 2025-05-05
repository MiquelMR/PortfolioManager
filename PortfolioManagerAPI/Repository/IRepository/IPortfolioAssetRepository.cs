using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioAssetRepository
    {
        Task<List<PortfolioAsset>> GetPortfolioAssetsByPortfolioIdAsync(int portfolioId);
    }
}
