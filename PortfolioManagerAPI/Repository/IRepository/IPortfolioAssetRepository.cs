using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioAssetRepository
    {
        Task<ICollection<PortfolioAsset>> GetPortfolioAssetsByPortfolioIdAsync(int portfolioId);
    }
}
