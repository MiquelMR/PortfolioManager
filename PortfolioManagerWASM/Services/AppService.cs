using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Services
{
    public class AppService : IAppService
    {
        public IUserService UserService { get; }
        public IAssetService AssetService { get; }
        public IAuthService AuthService { get; }
        public IImageService ImageService { get; }
        public IPortfolioService PortfolioService { get; set; }

        public AppService(IUserService userService, IAssetService assetService, IAuthService authService, IImageService imageService, IPortfolioService portfolioService)
        {
            UserService = userService;
            AssetService = assetService;
            AuthService = authService;
            ImageService = imageService;
            PortfolioService = portfolioService;
        }
    }
}
