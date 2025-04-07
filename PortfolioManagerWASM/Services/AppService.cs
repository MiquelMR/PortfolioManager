using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Services
{
    public class AppService : IAppService
    {
        public IUserService UserService { get; }
        public IAssetService AssetService { get; }

        public AppService(IUserService userService, IAssetService assetService)
        {
            UserService = userService;
            AssetService = assetService;
        }
    }
}
