namespace PortfolioManagerWASM.Services.IService
{
    public interface IAppService
    {
        IUserService UserService { get; }
        IAssetService AssetService { get; }
    }
}
