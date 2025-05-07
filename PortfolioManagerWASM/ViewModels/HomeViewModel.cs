using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel(IPortfolioService PortfolioService, IUserService UserService, IAssetService AssetService)
    {
        private readonly IPortfolioService _portfolioService = PortfolioService;
        private readonly IUserService _userService = UserService;
        private readonly IAssetService _assetService = AssetService;

        public User ActiveUser { get; set; } = new();
        public List<Portfolio> PortfoliosBasicInfo { get; set; } = [];
        public Portfolio ActivePortfolio { get; set; } = new();
        public List<FinancialAsset> FinancialAssets { get; set; } = new();

        public async Task InitAsync()
        {
            await _userService.InitializeAsync();
            ActiveUser = _userService.ActiveUser;
            PortfoliosBasicInfo = (await _portfolioService.GetPortfoliosBasicInfoByUserAsync(ActiveUser.Email));
            ActivePortfolio = PortfoliosBasicInfo.Count > 0 ? await _portfolioService.GetPortfolioByIdAsync(PortfoliosBasicInfo[0].PortfolioId) : new();
            FinancialAssets = await _assetService.GetAssetsAsync();
        }

        public async Task SelectPortfolioAsync(int index)
        {
            var portfolioId = PortfoliosBasicInfo[index].PortfolioId;
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(portfolioId);
        }

        public Portfolio RegisterPortfolio(Portfolio portfolio)
        {
            // var registeredPortfolio = _portfolioService.RegisterPortfolio(portfolio);
            return null;
        }

        public void CleanData()
        {
            ActiveUser = new User();
            ActivePortfolio = new Portfolio();
            PortfoliosBasicInfo.Clear();
        }
    }
}
