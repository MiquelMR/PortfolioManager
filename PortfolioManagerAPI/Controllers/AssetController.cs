using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/assets")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AssetDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
            {
                return BadRequest("Asset data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Asset data is invalid");
            }

            if (await _assetService.ExistsByNameAsync(assetDto.Name))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(409, ModelState);
            }

            await _assetService.CreateAsync(assetDto);
            return CreatedAtRoute("GetAssetByName", new { assetDto.Name }, assetDto);
        }

        [HttpPatch]
        [ProducesResponseType(201, Type = typeof(AssetDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
            {
                return BadRequest("User data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("User data is invalid");
            }

            if (!await _assetService.UpdateAsync(assetDto))
            {
                ModelState.AddModelError("", "Asset not found");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{name}", Name = "DeleteAsset")]
        [ProducesResponseType(201, Type = typeof(AssetDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsset(string name)
        {
            if (!await _assetService.ExistsByNameAsync(name))
            {
                ModelState.AddModelError("", "Error with the model");
                return StatusCode(404, ModelState);
            }

            if (!await _assetService.DeleteAsync(name))
            {
                return NotFound("Asset not found");
            }

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssets()
        {
            var assetDtoList = await _assetService.GetAllAsync();
            if (assetDtoList == null) { return NotFound(); }
            return Ok(assetDtoList);
        }
    }
}
