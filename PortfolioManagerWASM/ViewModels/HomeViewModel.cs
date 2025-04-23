using Blazored.LocalStorage;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel
    {
        private readonly IAppService _AppService;
        private readonly ILocalStorageService _localStorage;

        public User User { get; set; }
        public List<Asset> Assets { get; set; }
        public List<Portfolio> Portfolios { get; set; }
        public Portfolio ActivePortfolio { get; set; }


        public HomeViewModel(IAppService AppService, ILocalStorageService localStorage)
        {
            _AppService = AppService;
            _localStorage = localStorage;
        }

        public async Task InitAsync()
        {
            User = await GetUserAsync();
            Assets = (List<Asset>)await _AppService.AssetService.GetAssets();
            Portfolios = (await _AppService.PortfolioService.GetAllByUserAsync(User.Email)).ToList();
            ActivePortfolio = Portfolios[0];
        }

        public void Logout()
        {
            _AppService.AuthService.Logout();
        }

        private async Task<User> GetUserAsync()
        {
            var ActiveUserEmail = await _localStorage.GetItemAsync<string>(Initialize.User_Local_Data);
            return await _AppService.UserService.GetUserByEmail(ActiveUserEmail);
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
