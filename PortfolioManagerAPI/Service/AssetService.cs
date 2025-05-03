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

            var assetsDto = new List<AssetDto>();
            foreach (var asset in assets)
            {
                var assetDto = _mapper.Map<AssetDto>(asset);
                if (asset.IconFilename != null)
                {
                    var iconFullpath = Path.Combine(resourcePath, asset.IconFilename);
                    try
                    {
                        assetDto.Icon = ImageHelper.ImagePathToImage(iconFullpath);
                    }
                    catch (Exception) { assetDto.Icon = null; }
                }
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
            var asset = _mapper.Map<Asset>(assetDto);

            string iconFilename = null;
            string iconFullpath = null;
            if (assetDto.Icon != null)
            {
                try
                {
                    iconFilename = Guid.NewGuid().ToString() + ImageHelper.GetImageExtension(assetDto.Icon);
                    iconFullpath = Path.Combine(resourcePath, iconFilename);
                }
                catch
                {
                    iconFilename = null;
                }
            }
            asset.IconFilename = iconFilename;

            var success = await _assetRepository.CreateAssetAsync(asset);
            if (!success) { return null; }

            if (assetDto.Icon != null)
            {
                ImageHelper.SaveImage(iconFullpath, assetDto.Icon);
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
            if (asset == null) { return null; }
            string oldIconFilename = asset.IconFilename;

            _mapper.Map(assetDto, asset);

            string iconFilename = null;
            bool validIcon = false;
            if (assetDto.Icon != null)
            {
                try
                {
                    iconFilename = Guid.NewGuid().ToString() + ImageHelper.GetImageExtension(assetDto.Icon);
                    validIcon = true;
                }
                catch
                {
                    iconFilename = oldIconFilename;
                }
                asset.IconFilename = iconFilename;

                var success = await _assetRepository.UpdateAssetAsync(asset);
                if (!success) { return null; }

                if (validIcon)
                {
                    var iconFullpath = Path.Combine(resourcePath, iconFilename); ;
                    if (ImageHelper.SaveImage(iconFullpath, assetDto.Icon))
                    {
                        var oldAvatarFullpath = Path.Combine(resourcePath, oldIconFilename ?? "");
                        if (File.Exists(oldAvatarFullpath)) { File.Delete(oldAvatarFullpath); }
                    }
                }
            }

            return assetDto;
        }

        public async Task<bool> DeleteAssetByIdAsync(int assetId)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetId);
            if (asset == null) { return false; }
            var success = await _assetRepository.DeleteAssetByIdAsync(assetId);
            if (!success) { return false; }

            var iconFullPath = resourcePath + asset.IconFilename;
            if (File.Exists(iconFullPath)) { File.Delete(iconFullPath); }
            return success;
        }

        public async Task<bool> ExistsByIdAsync(int assetId)
        {
            return await _assetRepository.ExistsByIdAsync(assetId);
        }
    }
}

