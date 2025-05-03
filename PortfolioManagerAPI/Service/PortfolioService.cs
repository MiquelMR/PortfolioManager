using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
namespace PortfolioManagerAPI.Service
{
    public class PortfolioService(IPortfolioRepository portfolioRepository, IPortfolioAssetRepository portfolioAssetsRepository, IMapper mapper, IConfiguration config) : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IPortfolioAssetRepository _portfolioAssetRepository = portfolioAssetsRepository;
        private readonly IMapper _mapper = mapper;

        private readonly string portfolioResourcePath = config.GetValue<string>("ResourcesPaths:PortfolioIcons");
        private readonly string assetsResourcePath = config.GetValue<string>("ResourcesPaths:AssetIcons");

        public async Task<PortfolioDto> GetPortfolioById(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioByIdAsync(portfolioId);
            if (portfolio == null) { return null; }
            var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);

            var portfolioAssets = await _portfolioAssetRepository.GetPortfolioAssetsByPortfolioIdAsync(portfolioDto.PortfolioId) ?? [];
            if (portfolioAssets == null) { return null; }

            var portfolioAssetsDto = new List<PortfolioAssetDto>();
            portfolioAssetsDto = portfolioAssets.Select(portfolioAsset =>
            {
                var portfolioAssetDto = _mapper.Map<PortfolioAssetDto>(portfolioAsset);
                portfolioAssetDto.Asset = _mapper.Map<AssetDto>(portfolioAsset.Asset);
                if (portfolioAsset.Asset.IconFilename != null)
                {
                    try
                    {
                        var assetIconFullPath = Path.Combine(assetsResourcePath, portfolioAsset.Asset.IconFilename);
                        portfolioAssetDto.Asset.Icon = ImageHelper.ImagePathToImage(assetIconFullPath);
                    }
                    catch
                    {
                        portfolioAssetDto.Asset.Icon = null;
                    }
                }
                return portfolioAssetDto;
            }).ToList();
            portfolioDto.PortfolioAssets = portfolioAssetsDto;

            if (portfolio.IconPath != null)
            {
                try
                {
                    var portfolioIconFullpath = Path.Combine(portfolioResourcePath, portfolio.IconPath);
                    portfolioDto.Icon = ImageHelper.ImagePathToImage(portfolioIconFullpath);
                }
                catch
                {
                    portfolioDto.Icon = null;
                }
            }
            return portfolioDto;
        }

        public async Task<List<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail)
        {

            var portfolios = await _portfolioRepository.GetPortfoliosByUserEmailAsync(userEmail) ?? [];
            if (portfolios == null) { return null; }

            var portfoliosDto = portfolios.Select(portfolio =>
            {
                var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
                if (portfolio.IconPath != null)
                {
                    try
                    {
                        var portfolioIconFullpath = Path.Combine(portfolioResourcePath, portfolio.IconPath);
                        portfolioDto.Icon = ImageHelper.ImagePathToImage(portfolioIconFullpath);
                    }
                    catch
                    {
                        portfolioDto.Icon = null;
                    }
                }
                return portfolioDto;
            }).ToList();

            return portfoliosDto;
        }
    }
}

