using AutoMapper;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly string secretKey;

        public AssetService(IAssetRepository assetRepository, IMapper mapper, IConfiguration config)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            secretKey = config.GetValue<string>("ApiSettings:Secret");
        }



        public Task<bool> CreateAssetAsync(Asset asset)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAssetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<AssetDto> GetAssetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<AssetDto>> GetAssetsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAssetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAssetAsync(Asset asset)
        {
            throw new NotImplementedException();
        }
    }
}
