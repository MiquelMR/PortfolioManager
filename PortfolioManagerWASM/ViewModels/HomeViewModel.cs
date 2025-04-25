using Blazored.LocalStorage;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;
using System.Net.Http;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public User User { get; set; }
        public List<Portfolio> PortfoliosBasicInfo { get; set; }
        public Portfolio ActivePortfolio { get; set; }


        public HomeViewModel(IPortfolioService PortfolioService, IUserService UserService, IAuthService AuthService)
        {
            _portfolioService = PortfolioService;
            _userService = UserService;
            _authService = AuthService;
        }

        public async Task InitAsync()
        {
            User = _userService.ActiveUser;
            PortfoliosBasicInfo = (await _portfolioService.GetPortfoliosBasicInfoByUserAsync(User.Email)).ToList();
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
