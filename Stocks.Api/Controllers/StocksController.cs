using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stocks.Api.Controllers
{
    [Route("api/Stocks")]
    [Produces("application/json")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        readonly IStockRepository _stockRepo;

        public StocksController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryObject query)
        {
            var res = await _stockRepo.GetAllAsync(query);
            if (!res.Any())
                return NotFound("No Stock Was Found!");
            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            return stock is null ? NotFound($"No stock with id {id}") : Ok(stock);
        }
    }
}
