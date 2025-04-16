using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;

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
        public IActionResult GetAssets()
        {
            var assetList = _assetRepository.GetAsset();
            var assetDtoList = new List<AssetDto>();
            foreach (var asset in assetList)
            {
                assetDtoList.Add(_mapper.Map<AssetDto>(asset));
            }
            return Ok(assetDtoList);
        }

        [HttpGet("{AssetId:int}", Name = "GetAsset")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAsset(int AssetId)
        {
            var asset = _assetRepository.GetAssetById(AssetId);
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
        public IActionResult CreateAsset([FromBody] AssetDto assetDto)
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

            if (_assetRepository.ExistsById(asset.AssetId))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(409, ModelState);
            }
            _assetRepository.CreateAsset(asset);
            return CreatedAtRoute("GetAsset", new { AssetId = asset.AssetId }, asset);
        }

        [HttpPatch("{AssetId:int}", Name = "UpdatePatchAsset")]
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

            if (!_assetRepository.UpdateAsset(asset))
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
            if (!_assetRepository.ExistsById(AssetId))
            {
                ModelState.AddModelError("", "Asset not found");
                return StatusCode(404, ModelState);
            }

            var user = _assetRepository.GetAssetById(AssetId);
            _assetRepository.DeleteAsset(user);
            return NoContent();
        }
    }
}
