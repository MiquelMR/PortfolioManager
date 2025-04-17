using Blazored.LocalStorage;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PortfolioManagerWASM.ViewModels
{
    public class HomeViewModel
    {
        private readonly IAppService _AppService;
        private readonly ILocalStorageService _localStorage;

        public User User { get; set; }
        public List<Asset> Assets { get; set; }
        public string Image { get; set; }

        public HomeViewModel(IAppService AppService, ILocalStorageService localStorage)
        {
            _AppService = AppService;
            _localStorage = localStorage;
        }

        public async Task InitAsync()
        {
            User = await GetUserAsync();
            Assets = (List<Asset>)await _AppService.AssetService.GetAssets();
            Image = await _AppService.ImageService.GetImageByName("gold.jpg");
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
    }
}
