using AutoMapper;
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
            try
            {
                var assets = await _assetRepository.GetAllAsync();
                return _mapper.Map<List<AssetDto>>(assets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving assets");
                throw new Exception("An error occurred while retrieving all assets.", ex);
            }
        }

        public async Task<AssetDto> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            try
            {
                var asset = await _assetRepository.GetByNameAsync(name);
                return _mapper.Map<AssetDto>(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset");
                throw new Exception($"An error occurred while retrieving the asset with name: {name}.", ex);
            }
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

