using Microsoft.AspNetCore.Components;
using PortfolioManagerWASM.Models;
using PortfolioManagerWASM.Models.DTOs;
using PortfolioManagerWASM.Services.IService;

namespace PortfolioManagerWASM.ViewModels
{
    public class AdminViewModel(IAssetService assetService)
    {
        private readonly IAssetService _assetService = assetService;

        public List<Asset> Assets { get; set; }

        public async Task InitAsync()
        {
            Assets = (await _assetService.GetAssetsAsync()).ToList();
        }
    }
}
