using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Models.DTOs.UserDto;
using PortfolioManagerAPI.Repository;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
using XAct;

namespace PortfolioManagerAPI.Service
{
    public class AssetService(IAssetRepository assetRepository, IMapper mapper, IConfiguration config) : IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;
        private readonly IMapper _mapper = mapper;

        private readonly string resourcePath = config.GetValue<string>("ResourcesPaths:AssetIcons");

        public async Task<List<AssetDto>> GetAssetsAsync()
        {
            var assets = await _assetRepository.GetAssetsAsync();
            if (assets == null) { return null; }

            var assetsDto = _mapper.Map<List<AssetDto>>(assets);
            return assetsDto;
        }

        public async Task<AssetDto> CreateAssetAsync(AssetDto assetDto)
        {
            assetDto.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(p =>
                {
                    var value = (string)p.GetValue(assetDto);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        p.SetValue(assetDto, null);
                    }
                });
            var asset = _mapper.Map<Asset>(assetDto);

            var success = await _assetRepository.CreateAssetAsync(asset);
            if (!success) { return null; }

            return assetDto;
        }

        public async Task<AssetDto> UpdateAssetAsync(AssetDto assetDto)
        {
            assetDto.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(p =>
                {
                    var value = (string)p.GetValue(assetDto);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        p.SetValue(assetDto, null);
                    }
                });

            var asset = await _assetRepository.GetAssetByIdAsync(assetDto.AssetId);
            if (asset == null) { return null; }

            _mapper.Map(assetDto, asset);
            var success = await _assetRepository.UpdateAssetAsync(asset);
            if (!success) { return null; }

            return assetDto;
        }

        public async Task<bool> DeleteAssetByIdAsync(int assetId)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetId);
            if (asset == null)
                return false;
            var success = await _assetRepository.DeleteAssetByIdAsync(assetId);
            if (!success)
                return false;

            return success;
        }

        public async Task<bool> ExistsByIdAsync(int assetId)
        {
            return await _assetRepository.ExistsByIdAsync(assetId);
        }
    }
}

