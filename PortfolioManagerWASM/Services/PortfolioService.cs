using Newtonsoft.Json;
using PortfolioManagerAPI.Models;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Services
{
    public class PortfolioService(HttpClient httpClient) : IPortfolioService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<Portfolio>> GetPortfoliosBasicInfoByUserAsync(string userEmail)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/basicPortfolioInfoByUserEmail/{userEmail}");
            var content = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<Portfolio>>>(content);
            var portfolios = responseAPI.Data;

            return portfolios;
        }

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/byPortfolioId/{portfolioId}");
            var content = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<Portfolio>>(content);
            var portfolio = responseAPI.Data;

            return portfolio;
        }
    }
}