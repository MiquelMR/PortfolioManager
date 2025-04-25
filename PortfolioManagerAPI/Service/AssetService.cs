using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetService> _logger;

        public AssetService(IAssetRepository assetRepository, IMapper mapper, ILogger<AssetService> logger)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(AssetDto assetDto)
        {
            if (assetDto == null || string.IsNullOrWhiteSpace(assetDto.Name))
            {
                return false;
            }

            try
            {
                var asset = _mapper.Map<Asset>(assetDto);
                return await _assetRepository.CreateAsync(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating asset");
                throw new Exception("An error occurred while creating the asset.", ex);
            }
        }

        public async Task<bool> UpdateAsync(AssetDto assetDto)
        {
            if (assetDto == null || string.IsNullOrWhiteSpace(assetDto.Name))
            {
                return false;
            }

            try
            {
                var asset = _mapper.Map<Asset>(assetDto);
                return await _assetRepository.UpdateAsync(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating asset");
                throw new Exception("An error occurred while updating the asset.", ex);
            }
        }

        public async Task<bool> DeleteAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            try
            {
                return await _assetRepository.DeleteByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting asset");
                throw new Exception($"An error occurred while deleting the asset with name: {name}.", ex);
            }
        }

        public async Task<ICollection<AssetDto>> GetAllAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var assetsDto = new List<AssetDto>();

            if(assets == null || assets.Count == 0)
            {
                return [];
            }

            foreach (var asset in assets)
            {
                var assetDto = _mapper.Map<AssetDto>(asset);
                assetDto.Icon = asset.IconPath != null ? TypeConverter.assetIconPathToIcon(asset.IconPath) : [];
            
                assetsDto.Add(assetDto);
            }
            return assetsDto;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            try
            {
                return await _assetRepository.ExistsByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking asset");
                throw new Exception($"An error occurred while checking existence of the asset with name: {name}.", ex);
            }
        }
    }
}

