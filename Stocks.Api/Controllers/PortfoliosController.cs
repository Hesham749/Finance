using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocks.Api.Extensions;

namespace Stocks.Api.Controllers
{
    [Route("api/portfolios")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PortfoliosController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;


        public PortfoliosController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var Id = User.GetUserId();
            var res = await _portfolioRepo.GetUserPortfolio(Id);
            if (!res.Any())
                return NotFound($"You don't have any stocks");
            return Ok(res);
        }

        [HttpPost("{symbol}")]
        [Authorize]
        public async Task<ActionResult> Create(string symbol)
        {
            var portfolioStockDTO = await _stockRepo.GetStockIdBySymbol(symbol);
            if (portfolioStockDTO is null) return BadRequest($"Stock not found!");
            var userId = User.GetUserId();
            var res = await _portfolioRepo.CreateAsync(userId, portfolioStockDTO.StockId);
            if (res is null) return BadRequest($"You already assigned to this symbol");
            return Ok();
        }
    }
}
