using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController(IPortfolioService portfolioService) : ControllerBase
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        [HttpGet("byPortfolioId/{portfolioId}", Name = "GetPortfolioById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPortfolioById(int portfolioId)
        {
            var portfolioDto = await _portfolioService.GetPortfolioById(portfolioId);
            if (portfolioDto == null) { return NotFound(); }
            return Ok(portfolioDto);
        }

        [HttpGet("basicPortfolioInfoByUserEmail/{userEmail}", Name = "GetPortfoliosBasicInfoByUserEmail")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPortfoliosBasicInfoByUserEmail(string userEmail)
        {
            var portfolioDtoList = await _portfolioService.GetPortfoliosBasicInfoByUserEmailAsync(userEmail);
            if (portfolioDtoList == null) { return NotFound(); }
            return Ok(portfolioDtoList);
        }
    }
}
