using AutoMapper;
using Newtonsoft.Json;
using PortfolioManagerAPI.Models;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class PortfolioService(HttpClient httpClient, IMapper mapper) : IPortfolioService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMapper _mapper = mapper;

        public async Task<List<Portfolio>> GetPortfoliosBasicInfoByUserAsync(string userEmail)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/basicPortfolioInfoByUserEmail/{userEmail}");
            var content = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<PortfolioDto>>>(content);
            var portfoliosDto = responseAPI.Data;
            var portfolios = _mapper.Map<List<Portfolio>>(portfoliosDto);

            return portfolios;
        }

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/byPortfolioId/{portfolioId}");
            var content = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<PortfolioDto>>(content);
            var portfolioDto = responseAPI.Data;
            var portfolio = _mapper.Map<Portfolio>(portfolioDto);

            return portfolio;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio newPortfolio)
        {
            var newPortfolioDto = _mapper.Map<PortfolioDto>(newPortfolio);
            newPortfolioDto.PortfolioAssetsDto = _mapper.Map<List<PortfolioAssetDto>>(newPortfolio.PortfolioAssets);
            var body = JsonConvert.SerializeObject(newPortfolioDto);
            var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/portfolios", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<PortfolioDto>>(contentTemp);
            var portfolioDtoTemp = responseAPI.Data;

            var portfolio = _mapper.Map<Portfolio>(portfolioDtoTemp);
            return portfolio;
        }
    }
}