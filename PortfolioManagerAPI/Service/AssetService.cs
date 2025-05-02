using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Models.DTOs.UserDto;
using PortfolioManagerAPI.Repository;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class AssetService(IAssetRepository assetRepository, IMapper mapper) : IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;
        private readonly IMapper _mapper = mapper;

        private const string resourcesPath = "Resources/AssetIcons/";

        public async Task<ICollection<AssetDto>> GetAssetsAsync()
        {
            var assets = await _assetRepository.GetAssetsAsync();
            var assetsDto = new List<AssetDto>();

            if (assets == null || assets.Count == 0)
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


            // Save new image
            string fileName = null;
            if (assetDto.Icon != null)
            {
                var imageExtension = TypeConverter.ByteArrayImageToImageFileExtension(assetDto.Icon);
                fileName = Guid.NewGuid().ToString() + imageExtension;
                string fullPath = resourcesPath + fileName;
                File.WriteAllBytes(fullPath, assetDto.Icon);
            }

            var asset = _mapper.Map<Asset>(assetDto);
            asset.IconPath = fileName;
            return _mapper.Map<AssetDto>(await _assetRepository.CreateAssetAsync(asset));
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

            // Save new image
            var imageExtension = TypeConverter.ByteArrayImageToImageFileExtension(assetDto.Icon);
            string fileName = Guid.NewGuid().ToString() + imageExtension;
            string fullPath = resourcesPath + fileName;
            File.WriteAllBytes(fullPath, assetDto.Icon);

            var asset = await _assetRepository.GetByIdAsync(assetDto.AssetId);

            // Delete old image
            if (asset != null)
            {
                var oldImageFullPath = resourcesPath + asset.IconPath;
                File.Delete(oldImageFullPath);
            }

            asset.IconPath = fileName;
            _mapper.Map(assetDto, asset);
            return _mapper.Map<AssetDto>(await _assetRepository.UpdateAsync(asset));
        }

        public async Task<bool> DeleteAssetByIdAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            var success = await _assetRepository.DeleteAssetByIdAsync(assetId);

            // Delete old image
            if (success)
            {
                File.Delete(resourcesPath + asset.IconPath);
            }
            return success;
        }
        public async Task<bool> ExistsByIdAsync(int assetId)
        {
            return await _assetRepository.ExistsByIdAsync(assetId);
        }
    }
}

