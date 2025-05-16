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
    public class FinancialAssetService(HttpClient httpClient, IMapper mapper) : IFinancialAssetService
    {
        // Dependencies
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMapper _mapper = mapper;

        public async Task<List<FinancialAsset>> GetFinancialAssetsAsync()
        {
            var response = await _httpClient.GetAsync($"{Initialize.UrlBaseApi}api/assets");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<List<FinancialAssetDto>>>(contentTemp);
            var financialAssetsDto = responseAPI.Data;
            financialAssetsDto.ForEach(financialAssetsDto => { financialAssetsDto.IconFilename = financialAssetsDto.IconFilename ?? "default.svg"; });
            var financialAssets = _mapper.Map<List<FinancialAsset>>(financialAssetsDto);

            return financialAssets;
        }

        public async Task<FinancialAsset> CreateFinancialAssetAsync(FinancialAsset financialAsset)
        {
            var financialAssetDto = _mapper.Map<FinancialAssetDto>(financialAsset);
            var body = JsonConvert.SerializeObject(financialAssetDto);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");
            var response = await _httpClient.PostAsync($"{Initialize.UrlBaseApi}api/assets", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<FinancialAssetDto>>(contentTemp);
            var _financialAsset = _mapper.Map<FinancialAsset>(responseAPI.Data);

            return _financialAsset;
        }

        public async Task<FinancialAsset> UpdateFinancialAssetAsync(FinancialAsset financialAsset)
        {
            financialAsset = (FinancialAsset)TypeHelper.EmptyStringPropertiesToNull(financialAsset);
            var financialAssetDto = _mapper.Map<FinancialAssetDto>(financialAsset);

            var body = JsonConvert.SerializeObject(financialAssetDto);
            var bodyContent = new StringContent(body, Encoding.UTF8, "Application/json");

            var response = await _httpClient.PatchAsync($"{Initialize.UrlBaseApi}api/assets", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<FinancialAsset>>(contentTemp);
            var financialAssetUpdated = responseAPI.Data;

            return _mapper.Map<FinancialAsset>(financialAssetUpdated);
        }

        // TODO
        public async Task<bool> DeleteFinancialAssetAsync(FinancialAsset financialAsset)
        {
            var response = await _httpClient.DeleteAsync($"{Initialize.UrlBaseApi}api/assets/{financialAsset.AssetId}");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var responseAPI = JsonConvert.DeserializeObject<ResponseAPI<FinancialAsset>>(contentTemp);
            if (!response.IsSuccessStatusCode)
                Console.WriteLine(responseAPI.Message);

            return response.IsSuccessStatusCode;
        }
    }
}
