using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel(IPortfolioService PortfolioService, IUserService UserService, IFinancialAssetService AssetService)
    {
        // Dependencies
        private readonly IPortfolioService _portfolioService = PortfolioService;
        private readonly IUserService _userService = UserService;
        private readonly IFinancialAssetService _assetService = AssetService;
        public NavigationManager NavigationManager { get; set; }


        // Properties
        public User ActiveUser { get; set; } = new();
        public Portfolio ActivePortfolio { get; set; } = new();
        public List<Portfolio> UserPortfoliosBasicInfo { get; set; } = [];
        public List<FinancialAsset> FinancialAssets { get; set; } = [];

        public async Task InitAsync()
        {
            if (ActiveUser == null)
                ToLogin();

            await _userService.InitializeAsync();
            ActiveUser = _userService.ActiveUser;
            UserPortfoliosBasicInfo = (await _portfolioService.GetPortfoliosBasicInfoByUserAsync(ActiveUser.Email));
            ActivePortfolio = UserPortfoliosBasicInfo.Count > 0 ? await _portfolioService.GetPortfolioByIdAsync(UserPortfoliosBasicInfo[0].PortfolioId) : null;
            FinancialAssets = await _assetService.GetFinancialAssetsAsync();
        }

        // Events
        public async Task OnSelectPortfolioAsync(int index)
        {
            var portfolioId = UserPortfoliosBasicInfo[index].PortfolioId;
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(portfolioId);
        }

        public async Task<Portfolio> RegisterPortfolioAsync(Portfolio newPortfolio)
        {
            newPortfolio.Owner = ActiveUser.Name;
            newPortfolio.Author = ActiveUser.Name;
            var portfolioCreated = await _portfolioService.CreatePortfolioAsync(newPortfolio);
            UserPortfoliosBasicInfo = new List<Portfolio>(UserPortfoliosBasicInfo)
            {
                portfolioCreated
            };
            return portfolioCreated;
        }

        public async Task DeleteActivePortfolioAsync()
        {
            await _portfolioService.DeletePortfolioAsync(ActivePortfolio.PortfolioId);
        }

        public void CleanData()
        {
            ActiveUser = new User();
            ActivePortfolio = new Portfolio();
            UserPortfoliosBasicInfo.Clear();
        }

        public void ToLogin()
        {
            NavigationManager.NavigateTo("login", true);
        }
    }
}
