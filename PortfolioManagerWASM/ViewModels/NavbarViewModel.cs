using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class NavbarViewModel(IUserService UserService)
    {
        private readonly IUserService _userService = UserService;

        public User ActiveUser { get; set; }

        public async Task InitAsync()
        {
            await _userService.InitializeAsync();
            ActiveUser = _userService.ActiveUser;
        }

        public async Task Logout()
        {
            ActiveUser = null;
            await _userService.Logout();
        }
    }
}
