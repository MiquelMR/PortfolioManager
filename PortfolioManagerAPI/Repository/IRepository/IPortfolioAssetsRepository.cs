using PortfolioManagerAPI.Models;

namespace PortfolioManagerAPI.Repository.IRepository
{
    public interface IPortfolioAssetsRepository
    {
        Task<bool> CreateAsync(PortfolioAsset portfolioAssets);
        Task<bool> UpdateAsync(PortfolioAsset portfolioAssets);
        Task<bool> DeleteAsync(PortfolioAsset portfolioAssets);
        Task<ICollection<PortfolioAsset>> GetAllByPortfolioAsync(int portfolioId);
    }
}
