using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IPortfolioService
    {
        public Task<ICollection<Portfolio>> GetPortfoliosBasicInfoByUserAsync(string userEmail);
        public Task<Portfolio> GetPortfolioByIdAsync(int id);
    }
}
