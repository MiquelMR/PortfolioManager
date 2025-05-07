using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using PortfolioManagerAPI.Models;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class AssetService(HttpClient httpClient) : IAssetService
    {
        // Services
        private readonly HttpClient _httpClient = httpClient;

        // Constants
        private const string assetIconsDir = "icons/assets";
        private const string defaultIcon = "default.svg";

        public async Task<List<FinancialAsset>> GetAssetsAsync()
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/assets");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<FinancialAsset>>>(contentTemp);
            var assets = responseAPI.Data;
            foreach (var asset in assets)
            {
                var fullPath = Path.Combine(assetIconsDir, $"{asset.IconPath}.svg");
                var iconPath = File.Exists(fullPath)
                    ? fullPath
                    : Path.Combine(assetIconsDir, defaultIcon); ;

                asset.IconPath = iconPath;
            }

            return assets;
        }

        public async Task<FinancialAsset> CreateAssetAsync(FinancialAsset asset)
        {
            var body = JsonConvert.SerializeObject(asset);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/assets", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<FinancialAsset>>(contentTemp);
            var _asset = responseAPI.Data;

            return _asset;
        }

        public async Task<FinancialAsset> UpdateAssetAsync(FinancialAsset asset)
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
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<FinancialAsset>>(contentTemp);
            var _asset = responseAPI.Data;
            return _asset;
        }

        public async Task<bool> DeleteAssetAsync(FinancialAsset asset)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/assets/{asset.AssetId}");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(contentTemp);
            return result;
        }
    }
}
