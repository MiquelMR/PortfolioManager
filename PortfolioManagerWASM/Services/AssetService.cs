using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class AssetService(HttpClient httpClient) : IAssetService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<Asset>> GetAssetsAsync()
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/assets");
            var content = await response.Content.ReadAsStringAsync();
            var assets = JsonConvert.DeserializeObject<List<Asset>>(content);
            return assets;
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            var body = JsonConvert.SerializeObject(asset);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/assets", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Asset>(contentTemp);
            return result;
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            asset.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .ToList()
                .ForEach(p =>
                {
                    var value = (string)p.GetValue(asset);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        p.SetValue(asset, null);
                    }
                });
            var body = JsonConvert.SerializeObject(asset);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");

            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/assets", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Asset>(contentTemp);
            return result;
        }

        public async Task<bool> DeleteAssetAsync(Asset asset)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/assets/{asset.AssetId}");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(contentTemp);
            return result;
        }
    }
}
