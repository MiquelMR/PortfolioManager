using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Models.DTOs.UserDto;
using PortfolioManagerAPI.Service.IService;
using System.Net;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/assets")]
    [ApiController]
    public class AssetController(IAssetService assetService) : ControllerBase
    {
        private readonly IAssetService _assetService = assetService;

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assetDtoList = await _assetService.GetAssetsAsync() ?? [];
            return Ok(assetDtoList);
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null) { BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(); }
            var _assetDto = await _assetService.CreateAssetAsync(assetDto) ?? new();
            return Ok(_assetDto);
        }

        // [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null) { BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(); }
            var _assetDto = await _assetService.UpdateAssetAsync(assetDto) ?? new();
            return Ok(_assetDto);
        }

        // [Authorize]
        [HttpDelete("{assetId}")]
        public async Task<IActionResult> DeleteAssetById(int assetId)
        {
            if (assetId == 0) { BadRequest(); }
            var success = await _assetService.DeleteAssetByIdAsync(assetId);
            return Ok(success);
        }
    }

}
