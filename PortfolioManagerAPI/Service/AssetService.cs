using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class AssetService(IAssetRepository assetRepository, IMapper mapper) : IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ICollection<AssetDto>> GetAssetsAsync()
        {
            var assets = await _assetRepository.GetAssetsAsync();
            var assetsDto = new List<AssetDto>();

            if(assets == null || assets.Count == 0)
            {
                return [];
            }

            foreach (var asset in assets)
            {
                var assetDto = _mapper.Map<AssetDto>(asset);
                assetDto.Icon = asset.IconPath != null ? TypeConverter.AssetIconPathToIcon(asset.IconPath) : [];
            
                assetsDto.Add(assetDto);
            }
            return assetsDto;
        }
    }
}

