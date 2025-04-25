using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly HttpClient _httpClient;

        public PortfolioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ICollection<Portfolio>> GetPortfoliosBasicInfoByUserAsync(string userEmail)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/basicPortfolioInfoByUserEmail/{userEmail}");
            var content = await response.Content.ReadAsStringAsync();
            var portfolios = JsonConvert.DeserializeObject<ICollection<Portfolio>>(content);
            return portfolios;
        }

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/byPortfolioId/{portfolioId}");
            var content = await response.Content.ReadAsStringAsync();
            var portfolios = JsonConvert.DeserializeObject<Portfolio>(content);
            return portfolios;
        }
    }
}