using AutoMapper;
using PortfolioManagerAPI.Helpers;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
using XAct;
namespace PortfolioManagerAPI.Service
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IPortfolioAssetsRepository _portfolioAssetsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetService> _logger;

        public PortfolioService(IPortfolioRepository portfolioRepository, IUserRepository userRepository, IPortfolioAssetsRepository portfolioAssetsRepository, IMapper mapper, ILogger<AssetService> logger, IAssetRepository assetRepository)
        {
            _portfolioRepository = portfolioRepository;
            _userRepository = userRepository;
            _portfolioAssetsRepository = portfolioAssetsRepository;
            _mapper = mapper;
            _logger = logger;
            _assetRepository = assetRepository;
        }

        public async Task<bool> CreateAsync(PortfolioDto portfolioDto, string userEmail)
        {
            if (portfolioDto == null || string.IsNullOrWhiteSpace(portfolioDto.Name))
            {
                return false;
            }

            try
            {
                var user = await _userRepository.GetByEmailAsync(userEmail);
                var portfolio = _mapper.Map<Portfolio>(portfolioDto);
                portfolio.UserId = user.UserId;
                return await _portfolioRepository.CreateAsync(portfolio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating portfolio");
                throw new Exception("An error occurred while creating the portfolio.", ex);
            }
        }
        public async Task<bool> UpdateAsync(PortfolioDto portfolioDto)
        {
            if (portfolioDto == null || string.IsNullOrWhiteSpace(portfolioDto.Name))
            {
                return false;
            }

            try
            {
                var portfolio = _mapper.Map<Portfolio>(portfolioDto);
                return await _portfolioRepository.UpdateAsync(portfolio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating portfolio");
                throw new Exception("An error occurred while updating the portfolio.", ex);
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
                return await _portfolioRepository.DeleteByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting portfolio");
                throw new Exception($"An error occurred while deleting the portfolio with name: {name}.", ex);
            }
        }
        public async Task<PortfolioDto> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            try
            {
                var portfolio = await _portfolioRepository.GetByNameAsync(name);
                var filePath = portfolio.IconPath;
                var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
                portfolioDto.Icon = TypeConverter.pathToIcon(filePath);
                return portfolioDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving portfolio");
                throw new Exception($"An error occurred while retrieving the portfolio with name: {name}.", ex);
            }
        }

        public async Task<ICollection<PortfolioDto>> GetAllAsync()
        {
            try
            {
                var portfolios = await _portfolioRepository.GetAllAsync();
                if (portfolios == null || portfolios.Count == 0)
                {
                    return new List<PortfolioDto>();
                }
                var portfoliosDto = new List<PortfolioDto>();
                foreach (var portfolio in portfolios)
                {
                    var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
                    portfolioDto.Icon = portfolio.IconPath != null ? TypeConverter.pathToIcon(portfolio.IconPath) : Array.Empty<byte>();

                    portfoliosDto.Add(portfolioDto);
                }
                return portfoliosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking portfolio");
                throw new Exception($"An error occurred while retrieving portfolios", ex);
            }
        }

        public async Task<ICollection<PortfolioDto>> GetAllByUserAsync(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return null;
            }

            try
            {
                var portfolios = await _portfolioRepository.GetAllByUserAsync(userEmail);
                if (portfolios == null || portfolios.Count == 0)
                {
                    return [];
                }
                var portfoliosDto = new List<PortfolioDto>();
                foreach (var portfolio in portfolios)
                {
                    var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
                    portfolioDto.Icon = portfolio.IconPath != null ? TypeConverter.pathToIcon(portfolio.IconPath) : Array.Empty<byte>();

                    portfoliosDto.Add(portfolioDto);
                }
                return portfoliosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking portfolio");
                throw new Exception($"An error occurred while checking existence of the portfolio with email: {userEmail}.", ex);
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
                return await _portfolioRepository.ExistsByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking portfolio");
                throw new Exception($"An error occurred while checking existence of the portfolio with name: {name}.", ex);
            }
        }
    }
}
