using PortfolioManagerWASM.Services.IService;
using PortfolioManagerWASM.ViewModels;

namespace PortfolioManagerWASM.Services
{
    public class CleaningService(HomeViewModel homeViewModel, IUserService userService) : ICleaningService
    {
        private readonly HomeViewModel _homeViewmodel = homeViewModel;
        private readonly IUserService _userService = userService;

        public void CleanAllState()
        {
            _homeViewmodel.CleanData();
            _userService.RefreshActiveUserAsync();
        }
    }
}
