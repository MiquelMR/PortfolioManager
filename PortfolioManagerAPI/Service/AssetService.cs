using AutoMapper;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class AssetService(IAssetRepository assetRepository, IMapper mapper) : IAssetService
    {
        private readonly IAssetRepository _assetRepository = assetRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<FinancialAssetDto>> GetAssetsAsync()
        {
            var assets = await _assetRepository.GetAssetsAsync();
            if (assets == null) { return null; }

            var assetsDto = _mapper.Map<List<FinancialAssetDto>>(assets);
            return assetsDto;
        }

        public async Task<FinancialAssetDto> CreateAssetAsync(FinancialAssetDto assetDto)
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
            var asset = _mapper.Map<FinancialAsset>(assetDto);

            var success = await _assetRepository.CreateAssetAsync(asset);
            if (!success) { return null; }

            return assetDto;
        }

        public async Task<FinancialAssetDto> UpdateAssetAsync(FinancialAssetDto assetDto)
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

