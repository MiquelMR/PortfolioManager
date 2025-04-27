using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class NavbarViewModel(IUserService UserService, IAuthService AuthService)
    {
        private readonly IUserService _userService = UserService;
        private readonly IAuthService _authService = AuthService;

        public User ActiveUser { get; set; }

        public void Init()
        {
            ActiveUser = _userService.ActiveUser;
        }

        public void Logout()
        {
            _authService.Logout();
        }
    }
}
