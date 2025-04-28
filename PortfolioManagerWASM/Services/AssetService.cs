using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.Services
{
    public class AssetService(HttpClient httpClient) : IAssetService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IEnumerable<Asset>> GetAssetsAsync()
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/assets");
            var content = await response.Content.ReadAsStringAsync();
            var assets = JsonConvert.DeserializeObject<IEnumerable<Asset>>(content);
            return assets;
        }
    }
}
