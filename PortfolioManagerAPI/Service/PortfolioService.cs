using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
namespace PortfolioManagerAPI.Service
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IPortfolioAssetRepository _portfolioAssetRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetService> _logger;

        public PortfolioService(IPortfolioRepository portfolioRepository, IUserRepository userRepository, IPortfolioAssetRepository portfolioAssetsRepository, IMapper mapper, ILogger<AssetService> logger, IAssetRepository assetRepository)
        {
            _portfolioRepository = portfolioRepository;
            _userRepository = userRepository;
            _portfolioAssetRepository = portfolioAssetsRepository;
            _mapper = mapper;
            _logger = logger;
            _assetRepository = assetRepository;
        }

        public async Task<PortfolioDto> GetPortfolioById(int portfolioId)
        {
            try
            {
                // Get and Map portfolio
                var portfolio = await _portfolioRepository.GetPortfolioByIdAsync(portfolioId);
                if (portfolio == null)
                {
                    return new PortfolioDto();
                }

                var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);

                // Get and Map PortfolioAssets for this portfolio
                var portfolioAssets = await _portfolioAssetRepository.GetPortfolioAssetsByPortfolioIdAsync(portfolioDto.PortfolioId);
                var portfolioAssetsDto = new List<PortfolioAssetDto>();

                portfolioAssetsDto = portfolioAssets.Select(portfolioAsset =>
                {
                    var portfolioAssetDto = _mapper.Map<PortfolioAssetDto>(portfolioAsset);
                    portfolioAssetDto.Asset = _mapper.Map<AssetDto>(portfolioAsset.Asset);
                    portfolioAssetDto.Asset.Icon = portfolioAsset.Asset.IconPath != null
                        ? TypeConverter.assetIconPathToIcon(portfolioAsset.Asset.IconPath)
                        : null;
                    return portfolioAssetDto;
                }).ToList();

                portfolioDto.PortfolioAssets = portfolioAssetsDto;

                // Map Icon: From path to byte[]
                portfolioDto.Icon = portfolio.IconPath != null ? TypeConverter.portfolioIconPathToIcon(portfolio.IconPath) : [];

                return portfolioDto;
            }
            catch (Exception)
            {
                throw new Exception($"An error occurred while retrieving portfolios");
            }
        }

        public async Task<ICollection<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return null;
            }

            try
            {
                var portfolios = await _portfolioRepository.GetPortfoliosByUserEmailAsync(userEmail);
                if (portfolios == null || portfolios.Count == 0)
                {
                    return [];
                }

                var portfoliosDto = portfolios.Select(portfolio =>
                {
                    var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
                    portfolioDto.Icon = portfolio.IconPath != null
                        ? TypeConverter.portfolioIconPathToIcon(portfolio.IconPath)
                        : Array.Empty<byte>();

                    return portfolioDto;
                }).ToList();

                return portfoliosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking portfolio");
                throw new Exception($"An error occurred while checking existence of the portfolio with email: {userEmail}.", ex);
            }
        }
    }
}
