using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly HttpClient _httpClient;

        public PortfolioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            var content = JsonConvert.SerializeObject(portfolio);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/portfolios", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Portfolio>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
        public async Task<Portfolio> UpdateAsync(Portfolio portfolio)
        {
            var body = JsonConvert.SerializeObject(portfolio);
            var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/portfolios", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Portfolio>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> DeleteAsync(Portfolio portfolio)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/portfolios");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<ICollection<Portfolio>> GetAllByUserAsync(string userEmail)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/portfolios/byUserEmail/{userEmail}");
            var content = await response.Content.ReadAsStringAsync();
            var portfolios = JsonConvert.DeserializeObject<ICollection<Portfolio>>(content);
            return portfolios;
        }
    }
}