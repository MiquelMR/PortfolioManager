using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Service.IService;

namespace PortfolioManagerAPI.Controllers
{
    // [Authorize]
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PortfolioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioDto portfolioDto, string userEmail)
        {
            if (portfolioDto == null)
            {
                return BadRequest("Portfolio data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Portfolio data is invalid");
            }

            if (await _portfolioService.ExistsByNameAsync(portfolioDto.Name))
            {
                ModelState.AddModelError("", "Portfolio already exists");
                return StatusCode(409, ModelState);
            }

            await _portfolioService.CreateAsync(portfolioDto, userEmail);
            return CreatedAtRoute("GetPortfolioByName", new { portfolioDto.Name }, portfolioDto);
        }

        [HttpPatch]
        [ProducesResponseType(201, Type = typeof(PortfolioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePortfolio(int PortfolioId, [FromBody] PortfolioDto portfolioDto)
        {
            if (portfolioDto == null)
            {
                return BadRequest("Portfolio data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Portfolio data is invalid");
            }

            if (!await _portfolioService.UpdateAsync(portfolioDto))
            {
                ModelState.AddModelError("", "Portfolio not found");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{name}", Name = "DeletePortfolio")]
        [ProducesResponseType(201, Type = typeof(PortfolioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePortfolio(string name)
        {
            if (!await _portfolioService.ExistsByNameAsync(name))
            {
                ModelState.AddModelError("", "Error with the model");
                return StatusCode(404, ModelState);
            }

            if (!await _portfolioService.DeleteAsync(name))
            {
                return NotFound("Portfolio not found");
            }

            return NoContent();
        }

        [HttpGet("byName{name}", Name = "GetPortfolioByName")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPortfolioByName(string name)
        {
            var portfolioDto = await _portfolioService.GetByNameAsync(name);
            if (portfolioDto == null) { return NotFound(); }
            return Ok(portfolioDto);
        }

        [HttpGet("byUserEmail/{userEmail}", Name = "GetPortfoliosByUserEmail")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUser(string userEmail)
        {
            var portfolioDtoList = await _portfolioService.GetAllByUserAsync(userEmail);
            if (portfolioDtoList == null) { return NotFound(); }
            return Ok(portfolioDtoList);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var portfolioDtoList = await _portfolioService.GetAllAsync();
            if (portfolioDtoList == null) { return NotFound(); }
            return Ok(portfolioDtoList);
        }
    }
}
