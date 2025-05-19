using AutoMapper;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
namespace PortfolioManagerAPI.Service
{
    public class PortfolioService(IPortfolioRepository portfolioRepository, IFinancialAssetRepository assetRepository, IPortfolioAssetRepository portfolioAssetsRepository, IMapper mapper, IUserRepository userRepository) : IPortfolioService
    {
        // Repositories
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IPortfolioAssetRepository _portfolioAssetRepository = portfolioAssetsRepository;
        private readonly IFinancialAssetRepository _assetRepository = assetRepository;
        private readonly IUserRepository _userRepository = userRepository;

        private readonly IMapper _mapper = mapper;

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
                portfolioAssetDto.FinancialAssetDto = _mapper.Map<FinancialAssetDto>(portfolioAsset.FinancialAsset);
                return portfolioAssetDto;
            }).ToList();
            portfolioDto.PortfolioAssetsDto = portfolioAssetsDto;

            return portfolioDto;
        }

        public async Task<List<PortfolioDto>> GetPortfoliosBasicInfoAsync()
        {
            var portfolios = await _portfolioRepository.GetPortfoliosAsync() ?? [];
            if (portfolios == null)  
                return null; 

            var portfoliosDto = _mapper.Map<List<PortfolioDto>>(portfolios);
            return portfoliosDto;
        }

        public async Task<List<PortfolioDto>> GetPortfoliosBasicInfoByUserEmailAsync(string userEmail)
        {

            var portfolios = await _portfolioRepository.GetPortfoliosByUserEmailAsync(userEmail) ?? [];
            if (portfolios == null) { return null; }

            var portfoliosDto = _mapper.Map<List<PortfolioDto>>(portfolios);
            return portfoliosDto;
        }

        public async Task<PortfolioDto> CreatePortfolioAsync(PortfolioDto newPortfolioDto)
        {
            var newPortfolio = _mapper.Map<Portfolio>(newPortfolioDto);
            var userId = await _userRepository.GetUserIdByNameAsync(newPortfolio.Author);
            if (userId == 0)
                return null;
            newPortfolio.UserId = userId;

            var portfolioCreated = await _portfolioRepository.CreatePortfolioAsync(newPortfolio);
            if (portfolioCreated == null)
                return null;

            foreach (var portfolioAssetDto in newPortfolioDto.PortfolioAssetsDto)
            {
                PortfolioAsset portfolioAsset = new()
                {
                    AllocationPercentage = portfolioAssetDto.AllocationPercentage,
                    PortfolioId = newPortfolio.PortfolioId,
                    AssetId = await _assetRepository.GetFinancialAssetIdByNameAsync(portfolioAssetDto.FinancialAssetDto.Name)
                };
                await _portfolioAssetRepository.CreatePortfolioAssetAsync(portfolioAsset);
            }
            var test = _mapper.Map<PortfolioDto>(portfolioCreated);
            return test;
        }
        public async Task<bool> DeletePortfolioByIdAsync(int portfolioId)
        {
            var asset = await _portfolioRepository.GetPortfolioByIdAsync(portfolioId);
            if (asset == null)
                return false;
            var success = await _portfolioRepository.DeletePortfolioByIdAsync(portfolioId);
            if (!success)
                return false;

            return success;
        }

        public async Task<bool> ExistsByIdAsync(int portfolioId)
        {
            return await _portfolioRepository.ExistsByIdAsync(portfolioId);
        }
    }
}

