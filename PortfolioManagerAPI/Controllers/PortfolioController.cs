using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController(IPortfolioService portfolioService) : ControllerBase
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        [HttpGet("byPortfolioId/{portfolioId}")]
        public async Task<IActionResult> GetPortfolioById(int portfolioId)
        {
            if (portfolioId == 0)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: portfolioId is 0", null));
            var exists = await _portfolioService.ExistsByIdAsync(portfolioId);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Asset not found", null));

            var portfolioDto = await _portfolioService.GetPortfolioById(portfolioId);
            if (portfolioDto == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<PortfolioDto>(200, "Success", portfolioDto));
        }

        [HttpGet("publicPortfolios")]
        public async Task<IActionResult> GetPublicPortfolios()
        {
            var publicPortfoliosDto = await _portfolioService.GetPortfoliosBasicInfoByAccessibility(Accessibility.Public);
            if (publicPortfoliosDto == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<List<PortfolioDto>>(200, "Success", publicPortfoliosDto));
        }

        [HttpGet("basicPortfolioInfo")]
        public async Task<IActionResult> GetPortfoliosBasicInfo()
        {
            var portfolioDtoList = await _portfolioService.GetPortfoliosBasicInfoAsync();
            if (portfolioDtoList == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<List<PortfolioDto>>(200, "Success", portfolioDtoList));
        }

        [HttpGet("basicPortfolioInfoByUserEmail/{userEmail}")]
        public async Task<IActionResult> GetPortfoliosBasicInfoByUserEmail(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: user email is null", null));

            var portfolioDtoList = await _portfolioService.GetPortfoliosBasicInfoByUserEmailAsync(userEmail);
            if (portfolioDtoList == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<List<PortfolioDto>>(200, "Success", portfolioDtoList));
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioDto portfolioDto)
        {
            if (portfolioDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: portfolioDto is null", null));

            var createdPortfolio = await _portfolioService.CreatePortfolioAsync(portfolioDto);
            if (createdPortfolio == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Asset creation failed", null));

            return Ok(new ResponseAPI<PortfolioDto>(200, "Portfolio created successfully", createdPortfolio));
        }

        // [Authorize]
        [HttpDelete("{portfolioId}")]
        public async Task<IActionResult> DeletePortfolioById(int portfolioId)
        {
            if (portfolioId == 0)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: portfolioId is 0", null));
            var exists = await _portfolioService.ExistsByIdAsync(portfolioId);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Portfolio not found", null));

            var success = await _portfolioService.DeletePortfolioByIdAsync(portfolioId);
            if (!success)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: Portfolio deletion failed", null));

            return Ok(new ResponseAPI<PortfolioDto>(200, "Asset deleted successfully", null));
        }
    }
}
