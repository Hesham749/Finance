using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocks.Api.Extensions;

namespace Stocks.Api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;


        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var UserId = User.GetUserId();
            var res = await _portfolioRepo.GetUserPortfolio(UserId);
            if (!res.Any())
                return NotFound($"You don't have any stocks");
            return Ok(res);
        }

        [HttpPost("{symbol}")]
        public async Task<ActionResult> Create([FromRoute] string symbol)
        {
            var portfolioStockDTO = await _stockRepo.GetStockIdBySymbol(symbol);
            if (portfolioStockDTO is null) return BadRequest($"Stock not found!");
            var userId = User.GetUserId();
            var portfolio = new Portfolio
            {
                StockId = portfolioStockDTO.StockId,
                AppUserId = userId
            };
            if (await _portfolioRepo.PortfolioExist(portfolio))
                return BadRequest($"you already have this stock");
            var res = await _portfolioRepo.CreateAsync(portfolio);
            if (res is null) return StatusCode(500, $"profile not Created");
            return Created();
        }

        [HttpDelete(("{symbol}"))]
        public async Task<ActionResult> Delete(string symbol)
        {
            var portfolioStockDTO = await _stockRepo.GetStockIdBySymbol(symbol);
            if (portfolioStockDTO is null) return BadRequest($"Stock not found!");
            var userId = User.GetUserId();
            var portfolio = new Portfolio
            {
                StockId = portfolioStockDTO.StockId,
                AppUserId = userId
            };
            if (!await _portfolioRepo.PortfolioExist(portfolio))
                return BadRequest($"You don't have that stock");
            var res = await _portfolioRepo.DeleteAsync(portfolio);
            return NoContent();
        }
    }
}
