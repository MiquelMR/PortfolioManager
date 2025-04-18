namespace PortfolioManagerWASM.Services.IService
{
    public interface IAppService
    {
        IUserService UserService { get; }
        IAssetService AssetService { get; }
        IAuthService AuthService { get; }
        IImageService ImageService { get; }
        IPortfolioService PortfolioService { get; }
    }
}
