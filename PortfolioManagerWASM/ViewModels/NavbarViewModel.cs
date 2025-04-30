using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class NavbarViewModel(IUserService UserService, ICleaningService cleaningService)
    {
        private readonly IUserService _userService = UserService;
        private readonly ICleaningService _cleaningService = cleaningService;

        public User ActiveUser { get; set; }

        public void Init()
        {
            ActiveUser = _userService.ActiveUser;
        }

        public async Task Logout()
        {
            ActiveUser = null;
            _cleaningService.CleanAllState();
            await _userService.Logout();
        }
    }
}
