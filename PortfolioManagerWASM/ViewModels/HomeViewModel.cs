using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel(IPortfolioService PortfolioService, IUserService UserService, IAuthService AuthService)
    {
        private readonly IPortfolioService _portfolioService = PortfolioService;
        private readonly IUserService _userService = UserService;
        private readonly IAuthService _authService = AuthService;

        public User ActiveUser { get; set; }
        public List<Portfolio> PortfoliosBasicInfo { get; set; }
        public Portfolio ActivePortfolio { get; set; }

        public async Task InitAsync()
        {
            ActiveUser = _userService.ActiveUser;
            PortfoliosBasicInfo = (await _portfolioService.GetPortfoliosBasicInfoByUserAsync(ActiveUser.Email)).ToList();
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(PortfoliosBasicInfo[0].PortfolioId);
        }

        public void Logout()
        {
            _authService.Logout();
        }

        public string GetBase64String(byte[] icon)
        {
            return $"data:image/svg+xml;base64,{Convert.ToBase64String(icon)}";
        }

        public async Task SelectPortfolio(int index)
        {
            ActivePortfolio = await _portfolioService.GetPortfolioByIdAsync(PortfoliosBasicInfo[index].PortfolioId);
        }
    }
}
