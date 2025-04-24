using Blazored.LocalStorage;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Net.Http;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel
    {
        private readonly IAppService _AppService;

        public User User { get; set; }
        public List<Asset> Assets { get; set; }
        public List<Portfolio> Portfolios { get; set; }
        public Portfolio ActivePortfolio { get; set; }


        public HomeViewModel(IAppService AppService)
        {
            _AppService = AppService;
        }

        public async Task InitAsync()
        {
            User = _AppService.UserService.ActiveUser;
            Assets = (List<Asset>)await _AppService.AssetService.GetAssets();
            Portfolios = (await _AppService.PortfolioService.GetAllByUserAsync(User.Email)).ToList();
            ActivePortfolio = Portfolios[0];
        }

        public void Logout()
        {
            _AppService.AuthService.Logout();
        }

        public string GetBase64String(byte[] icon)
        {
            return $"data:image/svg+xml;base64,{Convert.ToBase64String(icon)}";
        }

        public void SelectPortfolio(int index)
        {
            ActivePortfolio = Portfolios[index];
        }
    }
}
