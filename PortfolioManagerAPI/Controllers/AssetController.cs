using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using System.Security.AccessControl;
using XAct;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/assets")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetController(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssets()
        {
            var assetList = await _assetRepository.GetAssetsAsync();
            var assetDtoList = _mapper.Map<List<AssetDto>>(assetList);
            return Ok(assetDtoList);
        }

        [HttpGet("/assetById/{AssetId:int}", Name = "GetAssetById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAssetById(int assetId)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(assetId);
            if (asset == null) { return NotFound(); }
            var assetDto = _mapper.Map<AssetDto>(asset);
            return Ok(assetDto);
        }

        [HttpGet("/assetByName/{Name}", Name = "GetAssetByName")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAssetByName(string name)
        {
            var asset = await _assetRepository.GetAssetByNameAsync(name);
            if (asset == null) { return NotFound(); }
            var assetDto = _mapper.Map<AssetDto>(asset);
            return Ok(assetDto);
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

            var asset = _mapper.Map<Asset>(assetDto);

            if (await _assetRepository.ExistsByIdAsync(asset.AssetId))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(409, ModelState);
            }
            await _assetRepository.CreateAssetAsync(asset);
            return CreatedAtRoute("GetAsset", new { asset.AssetId }, asset);
        }

        [HttpPatch("{Name}", Name = "UpdatePatchAsset")]
        [ProducesResponseType(201, Type = typeof(AssetDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAsset(int AssetId, [FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
            {
                return BadRequest("User data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("User data is invalid");
            }

            var asset = _mapper.Map<Asset>(assetDto);

            if (!_assetRepository.UpdateAssetAsync(asset))
            {
                ModelState.AddModelError("", "Asset not found");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{AssetId:int}", Name = "DeleteAsset")]
        [ProducesResponseType(201, Type = typeof(AssetDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAsset(int AssetId)
        {
            if (!_assetRepository.ExistsByIdAsync(AssetId))
            {
                ModelState.AddModelError("", "Asset not found");
                return StatusCode(404, ModelState);
            }

            var user = _assetRepository.GetAssetByIdAsync(AssetId);
            _assetRepository.DeleteAsset(user);
            return NoContent();
        }
    }
}
