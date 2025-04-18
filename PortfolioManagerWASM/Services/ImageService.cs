using Newtonsoft.Json;
using PortfolioManagerWASM.Helpers;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Services.IService;
using static System.Net.WebRequestMethods;

namespace PortfolioManagerWASM.Services
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;

        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetImageByName(string name)
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/images/assetIcons/{name}");
            if (response.IsSuccessStatusCode)
            {
                var image = await response.Content.ReadAsByteArrayAsync();
                return Convert.ToBase64String(image);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
