namespace PortfolioManagerAPI.Service.IService
{
    public interface IImageService
    {
        Task<byte[]> GetImageAsync(string imageName);
        Task<ICollection<byte[]>> GetAllAsync();
    }
}
