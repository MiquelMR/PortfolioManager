using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/assets")]
    [ApiController]
    public class AssetController(IAssetService assetService) : ControllerBase
    {
        private readonly IAssetService _assetService = assetService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssets()
        {
            var assetDtoList = await _assetService.GetAssetsAsync();
            if (assetDtoList == null) { return NotFound(); }
            return Ok(assetDtoList);
        }
    }
}
