using PortfolioManagerWASM.Models;

namespace PortfolioManagerWASM.Services.IService
{
    public interface IAssetService
    {
        public Task<IEnumerable<Asset>> GetAssetsAsync();
    }
}
