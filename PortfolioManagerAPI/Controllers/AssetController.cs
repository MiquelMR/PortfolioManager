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
        protected ResponseAPI _responseApi = new();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssets()
        {
            var assetDtoList = await _assetService.GetAssetsAsync();
            if (assetDtoList == null) { return NotFound(); }
            return Ok(assetDtoList);
        }

        // [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto assetDto)
        {
            var assetTemp = await _assetService.CreateAssetAsync(assetDto);
            if (assetTemp == null)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Error during updadte");
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseApi);
            }

            return Ok(assetTemp);
        }

        // [Authorize]
        [HttpPatch("updateAsset")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsset([FromBody] AssetDto assetDto)
        {
            var assetTemp = await _assetService.UpdateAssetAsync(assetDto);
            if (assetTemp == null)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Error during updadte");
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseApi);
            }

            return Ok(assetTemp);
        }

        // [Authorize]
        [HttpDelete("{assetId}")]
        public async Task<IActionResult> DeleteAssetById(int assetId)
        {
            if (!await _assetService.ExistsByIdAsync(assetId))
            {
                _responseApi.StatusCode = HttpStatusCode.NotFound;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Asset not found with the given email");
                return BadRequest(_responseApi);
            }

            var success = await _assetService.DeleteAssetByIdAsync(assetId);
            if (!success)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Server error");
                return BadRequest(_responseApi);
            }

            return Ok(success);
        }
    }

}
