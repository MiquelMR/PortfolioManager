using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class ImageService : IImageService
    {
        private readonly string _baseFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/AssetIcons");
        private readonly ILogger<ImageService> _logger;

        public ImageService(ILogger<ImageService> logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> GetImageAsync(string imageName)
        {
            var filePath = Path.Combine(_baseFolderPath, imageName);
            _logger.LogDebug($"File: {filePath}");
            if (!File.Exists(filePath))
            {
                return null;
            }

            await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            var buffer = new byte[fileStream.Length];
            await fileStream.ReadExactlyAsync(buffer, 0, (int)fileStream.Length);

            return buffer;
        }
        public async Task<ICollection<byte[]>> GetAllAsync()
        {
            var filePath = Path.Combine(_baseFolderPath);
            var files = new List<byte[]>();
            _logger.LogDebug($"File: {filePath}");
            if (!File.Exists(filePath))
            {
                return null;
            }

            foreach (var file in filePath)
            {
                await using var fileStream = new FileStream(filePath + file, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
                var buffer = new byte[fileStream.Length];
                await fileStream.ReadExactlyAsync(buffer, 0, (int)fileStream.Length);
                files.Add(buffer);
            }
            return files;
        }
    }
}
