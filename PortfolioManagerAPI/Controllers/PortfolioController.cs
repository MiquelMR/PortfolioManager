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

        [HttpGet("byPortfolioId/{portfolioId}")]
        public async Task<IActionResult> GetPortfolioById(int portfolioId)
        {
            if (portfolioId == 0) { BadRequest(); }
            var portfolioDto = await _portfolioService.GetPortfolioById(portfolioId) ?? new();
            return Ok(portfolioDto);
        }

        [HttpGet("basicPortfolioInfoByUserEmail/{userEmail}")]
        public async Task<IActionResult> GetPortfoliosBasicInfoByUserEmail(string userEmail)
        {
            if (userEmail == null) { BadRequest(); }
            var portfolioDtoList = await _portfolioService.GetPortfoliosBasicInfoByUserEmailAsync(userEmail) ?? [];
            return Ok(portfolioDtoList);
        }
    }
}
