using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Service
{
    public class FinancialAssetService(IFinancialAssetRepository assetRepository, IMapper mapper) : IFinancialAssetService
    {
        private readonly IFinancialAssetRepository _assetRepository = assetRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<FinancialAssetDto>> GetFinancialAssetsAsync()
        {
            var financialAssets = await _assetRepository.GetFinancialAssetsAsync();
            if (financialAssets == null)
                return null;

            var assetsDto = _mapper.Map<List<FinancialAssetDto>>(financialAssets);
            return assetsDto;
        }

        public async Task<FinancialAssetDto> CreateFinancialAssetAsync(FinancialAssetDto newFinancialAssetDto)
        {
            //assetDto.GetType().GetProperties()
            //    .Where(p => p.PropertyType == typeof(string))
            //    .ToList()
            //    .ForEach(p =>
            //    {
            //        var value = (string)p.GetValue(assetDto);
            //        if (string.IsNullOrWhiteSpace(value))
            //        {
            //            p.SetValue(assetDto, null);
            //        }
            //    });

            // Mira a ver si esto funciona,  
            newFinancialAssetDto = (FinancialAssetDto)TypeHelper.EmptyStringPropertiesToNull(newFinancialAssetDto);
            var financialAsset = _mapper.Map<FinancialAsset>(newFinancialAssetDto);

            var financialAssetCreated = await _assetRepository.CreateAssetAsync(financialAsset);
            return _mapper.Map<FinancialAssetDto>(financialAssetCreated);
        }

        public async Task<FinancialAssetDto> UpdateFinancialAssetAsync(FinancialAssetDto financialAssetUpdateDto)
        {
            financialAssetUpdateDto.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(p =>
                {
                    var value = (string)p.GetValue(financialAssetUpdateDto);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        p.SetValue(financialAssetUpdateDto, null);
                    }
                });

            var financialAsset = await _assetRepository.GetFinancialAssetByIdAsync(financialAssetUpdateDto.AssetId);
            if (financialAsset == null)
                return null;

            _mapper.Map(financialAssetUpdateDto, financialAsset);
            var financialAssetUpdated = await _assetRepository.UpdateAssetAsync(financialAsset);
            if (financialAsset == null)
                return null;

            return _mapper.Map<FinancialAssetDto>(financialAssetUpdated);
        }

        public async Task<bool> DeleteAssetByIdAsync(int financialAssetId)
        {
            var financialAsset = await _assetRepository.GetFinancialAssetByIdAsync(financialAssetId);
            if (financialAsset == null)
                return false;

            var success = await _assetRepository.DeleteAssetByIdAsync(financialAssetId);
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

