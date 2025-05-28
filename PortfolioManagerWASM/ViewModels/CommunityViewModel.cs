using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Collections.Generic;

namespace PortfolioManagerWASM.ViewModels
{
    public class CommunityViewModel(IPortfolioService PortfolioService, IUserService UserService, IFinancialAssetService AssetService)
    {
        // Dependencies
        private readonly IPortfolioService _portfolioService = PortfolioService;
        private readonly IUserService _userService = UserService;
        public NavigationManager NavigationManager { get; set; }

        // Properties
        public User ActiveUser { get; set; } = new();
        public Portfolio ActivePortfolio { get; set; } = new();
        public List<Portfolio> PublicPortfolios { get; set; } = [];

        public async Task InitAsync()
        {
            if (ActiveUser == null)
                ToLogin();

            await _userService.InitializeAsync();
            ActiveUser = _userService.ActiveUser;
            PublicPortfolios = FilterPortfoliosByNotOwned(await _portfolioService.GetPublicPortfolios());
            ActivePortfolio = PublicPortfolios.Count > 0 ? await _portfolioService.GetPortfolioByIdAsync(PublicPortfolios[0].PortfolioId) : null;
        }

        // Events
        public async Task OnSelectPortfolioAsync(int index)
        {
            var portfolioId = PublicPortfolios[index].PortfolioId;
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(portfolioId);
        }

        public void CleanData()
        {
            ActiveUser = new User();
            ActivePortfolio = new Portfolio();
            PublicPortfolios.Clear();
        }

        public async Task<Portfolio> AddPortfolio(Portfolio portfolio)
        {
            portfolio.PortfolioId = 0;
            portfolio.Accessibility = Accessibility.Private;
            portfolio.Owner = ActiveUser.Name;
            var portfolioAdded = await _portfolioService.CreatePortfolioAsync(portfolio);
            return portfolioAdded;
        }

        public void ToLogin()
        {
            NavigationManager.NavigateTo("login", true);
        }

        private List<Portfolio> FilterPortfoliosByNotOwned(List<Portfolio> portfolios)
        {
            List<Portfolio> filteredPortfolios = [.. portfolios];
            foreach (var portfolio in portfolios)
                filteredPortfolios.RemoveAll(p => p.Owner == ActiveUser.Name);

            return filteredPortfolios;
        }
    }
}
