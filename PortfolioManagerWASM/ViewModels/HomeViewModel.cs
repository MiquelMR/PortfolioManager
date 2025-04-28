using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel(IPortfolioService PortfolioService, IUserService UserService)
    {
        private readonly IPortfolioService _portfolioService = PortfolioService;
        private readonly IUserService _userService = UserService;

        public User ActiveUser { get; set; } = new User();
        public List<Portfolio> PortfoliosBasicInfo { get; set; } = [];
        public Portfolio ActivePortfolio { get; set; } = new Portfolio();

        public async Task InitAsync()
        {
            ActiveUser = _userService.ActiveUser;
            PortfoliosBasicInfo = (await _portfolioService.GetPortfoliosBasicInfoByUserAsync(ActiveUser.Email)).ToList();
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(PortfoliosBasicInfo[0].PortfolioId);
        }

        public async Task SelectPortfolio(int index)
        {
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(PortfoliosBasicInfo[index].PortfolioId);
        }

        public void CleanData()
        {
            ActiveUser = new User();
            ActivePortfolio = new Portfolio();
            PortfoliosBasicInfo.Clear();
        }
    }
}
