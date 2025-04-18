using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("assetIcons/{name}", Name = "GetAssetIconByName")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAssetIconByName(string name)
        {
            var image = await _imageService.GetImageAsync(name);
            if (image == null) { return NotFound(); }
            return Ok(image);
        }

        [HttpGet("assetIcons", Name = "GetAssetIcons")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAssetIcons()
        {
            var imageList = await _imageService.GetAllAsync();
            if (imageList == null) { return NotFound(); }
            return Ok(imageList);
        }
    }
}
