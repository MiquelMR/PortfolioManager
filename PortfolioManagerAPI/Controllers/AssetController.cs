using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
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
            var assetDtoList = await _assetService.GetAssetsAsync();
            if (assetDtoList == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<List<AssetDto>>(200, "Success", assetDtoList));
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: assetDto is null", null));

            var createdAsset = await _assetService.CreateAssetAsync(assetDto);
            if (createdAsset == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset creation failed", null));

            return Ok(new ResponseAPI<AssetDto>(200, "Asset created successfully", createdAsset));
        }

        // [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: assetDto is null", null));
            var exists = await _assetService.ExistsByIdAsync(assetDto.AssetId);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Asset not found", null));

            var updatedAsset = await _assetService.UpdateAssetAsync(assetDto);
            if (updatedAsset == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset update failed", null));

            return Ok(new ResponseAPI<AssetDto>(200, "Asset updated successfully", updatedAsset));
        }

        // [Authorize]
        [HttpDelete("{assetId}")]
        public async Task<IActionResult> DeleteAssetById(int assetId)
        {
            if (assetId == 0)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: assetId is 0", null));
            var exists = await _assetService.ExistsByIdAsync(assetId);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Asset not found", null));

            var success = await _assetService.DeleteAssetByIdAsync(assetId);
            if (!success)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset deletion failed", null));

            return Ok(new ResponseAPI<AssetDto>(200, "Asset deleted successfully", null));
        }
    }

}
