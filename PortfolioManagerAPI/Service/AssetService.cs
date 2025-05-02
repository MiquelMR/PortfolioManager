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
            var assets = await _assetRepository.GetAssetsAsync() ?? [];

            var assetsDto = new List<AssetDto>();
            foreach (var asset in assets)
            {
                var assetDto = _mapper.Map<AssetDto>(asset);

                var iconFullpath = Path.Combine(resourcePath, asset.IconFilename);
                assetDto.Icon = ImageHelper.ImagePathToImage(iconFullpath) ?? [];

                assetsDto.Add(assetDto);
            }
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
            var iconFilename = Guid.NewGuid().ToString() + ImageHelper.GetImageExtension(assetDto.Icon);
            string iconFullpath = resourcePath + iconFilename;

            var asset = _mapper.Map<Asset>(assetDto);
            asset.IconFilename = iconFilename;
            var success = await _assetRepository.CreateAssetAsync(asset);

            if (success)
            {
                File.WriteAllBytes(iconFullpath, assetDto.Icon);
            }

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
            var oldImgFullpath = Path.Combine(resourcePath, asset.IconFilename);
            _mapper.Map(assetDto, asset);

            string iconFilename = Guid.NewGuid().ToString() + ImageHelper.GetImageExtension(assetDto.Icon);
            asset.IconFilename = iconFilename;
            var success = await _assetRepository.UpdateAssetAsync(asset);

            if (success && File.Exists(oldImgFullpath))
            {
                var fullPath = Path.Combine(resourcePath + iconFilename);
                ImageHelper.SaveImage(fullPath, assetDto.Icon);
                File.Delete(oldImgFullpath);
            }

            return assetDto;
        }

        public async Task<bool> DeleteAssetByIdAsync(int assetId)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetId);
            var success = await _assetRepository.DeleteAssetByIdAsync(assetId);

            var iconFullPath = resourcePath + asset.IconFilename;
            if (success && File.Exists(iconFullPath))
                File.Delete(iconFullPath);
            return success;
        }

        public async Task<bool> ExistsByIdAsync(int assetId)
        {
            return await _assetRepository.ExistsByIdAsync(assetId);
        }
    }
}

