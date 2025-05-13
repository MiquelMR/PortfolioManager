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
    public class FinancialAssetController(IFinancialAssetService financialAssetService) : ControllerBase
    {
        private readonly IFinancialAssetService _financialAssetService = financialAssetService;

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assetDtoList = await _financialAssetService.GetFinancialAssetsAsync();
            if (assetDtoList == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<List<FinancialAssetDto>>(200, "Success", assetDtoList));
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] FinancialAssetDto financialAssetDto)
        {
            if (financialAssetDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: assetDto is null", null));

            var financialAssetCreated = await _financialAssetService.CreateFinancialAssetAsync(financialAssetDto);
            if (financialAssetCreated == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset creation failed", null));

            return Ok(new ResponseAPI<FinancialAssetDto>(200, "Asset created successfully", financialAssetCreated));
        }

        // [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateAsset([FromBody] FinancialAssetDto financialAssetDto)
        {
            if (financialAssetDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: assetDto is null", null));
            var exists = await _financialAssetService.ExistsByIdAsync(financialAssetDto.AssetId);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Asset not found", null));

            var financialAssetUpdated = await _financialAssetService.UpdateFinancialAssetAsync(financialAssetDto);
            if (financialAssetUpdated == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset update failed", null));

            return Ok(new ResponseAPI<FinancialAssetDto>(200, "Asset updated successfully", financialAssetUpdated));
        }

        // [Authorize]
        [HttpDelete("{financialAssetId}")]
        public async Task<IActionResult> DeleteAssetById(int financialAssetId)
        {
            if (financialAssetId == 0)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: assetId is 0", null));
            var exists = await _financialAssetService.ExistsByIdAsync(financialAssetId);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Asset not found", null));

            var success = await _financialAssetService.DeleteAssetByIdAsync(financialAssetId);
            if (!success)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset deletion failed", null));

            return Ok(new ResponseAPI<FinancialAssetDto>(200, "Asset deleted successfully", null));
        }
    }

}
