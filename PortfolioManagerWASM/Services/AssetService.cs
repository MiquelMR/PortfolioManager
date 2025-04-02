using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using System.Text;

namespace PortfolioManagerWASM.Services
{
    public class AssetService : IAssetService
    {
        private readonly HttpClient _httpClient;

        public AssetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Asset> CreateAsset(Asset asset)
        {
            var content = JsonConvert.SerializeObject(asset);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/assets", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Asset>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> DeleteAsset(int AssetId)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/assets/{AssetId}");
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

        public async Task<Asset> GetAsset(int AssetId)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/assets/{AssetId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var asset = JsonConvert.DeserializeObject<Asset>(content);
                return asset;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/assets");
            var content = await response.Content.ReadAsStringAsync();
            var assets = JsonConvert.DeserializeObject<IEnumerable<Asset>>(content);
            return assets;
        }

        public async Task<Asset> UpdateAsset(int AssetId, Asset asset)
        {
            var body = JsonConvert.SerializeObject(asset);
            var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/assets/{AssetId}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Asset>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
