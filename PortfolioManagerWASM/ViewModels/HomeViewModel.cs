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
        public string Image { get; set; }
        public List<Portfolio> Portfolios { get; set; }


        public HomeViewModel(IAppService AppService, ILocalStorageService localStorage)
        {
            _AppService = AppService;
            _localStorage = localStorage;
        }

        public async Task InitAsync()
        {
            User = await GetUserAsync();
            Assets = (List<Asset>)await _AppService.AssetService.GetAssets();
            Image = ConvertToBase64(await _AppService.ImageService.GetImageByName("gold.jpg"));
            Portfolios = (await _AppService.PortfolioService.GetAllByUserAsync(User.Email)).ToList();
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

        public string ConvertToBase64(string image)
        {
            string base64String = image;
            return $"data:image/jpeg;base64,{image}";
        }
    }
}
