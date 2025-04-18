using AutoMapper;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository;
using PortfolioManagerAPI.Repository.IRepository;
using PortfolioManagerAPI.Service.IService;
using System.Xml.Linq;

namespace PortfolioManagerAPI.Service
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPortfolioAssetsRepository _portfolioAssetsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetService> _logger;

        public PortfolioService(IPortfolioRepository portfolioRepository, IUserRepository userRepository, IPortfolioAssetsRepository portfolioAssetsRepository, IMapper mapper, ILogger<AssetService> logger)
        {
            _portfolioRepository = portfolioRepository;
            _userRepository = userRepository;
            _portfolioAssetsRepository = portfolioAssetsRepository;
            _mapper = mapper;
            _logger = logger;
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
                var portfolioDto = await _portfolioRepository.GetByNameAsync(name);
                return _mapper.Map<PortfolioDto>(portfolioDto);
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
                return _mapper.Map<List<PortfolioDto>>(portfolios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving portfolio");
                throw new Exception("An error occurred while retrieving all portfolio.", ex);
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
                var portfoliosDto = new List<PortfolioDto>();
                var portfolios = await _portfolioRepository.GetAllByUserAsync(userEmail);
                foreach (var portfolio in portfolios)
                {
                    var portfolioAssets = await _portfolioAssetsRepository.GetAllByPortfolioAsync(portfolio.PortfolioId);
                    var portfolioDto = _mapper.Map<PortfolioDto>(portfolio);
                    portfolioDto.PortfolioAssets = portfolioAssets;

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
