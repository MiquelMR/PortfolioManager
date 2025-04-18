namespace PortfolioManagerWASM.Services.IService
{
    public interface IImageService
    {
        public Task<string> GetImageByName(string name);
    }
}
