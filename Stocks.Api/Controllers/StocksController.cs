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
        readonly IStockMapper _stockMapper;
        public StocksController(IStockRepository stockRepo, IStockMapper stockMapper)
        {
            _stockRepo = stockRepo;
            _stockMapper = stockMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
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

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateStockDTO dto)
        {
            var stock = await _stockRepo.CreateAsync(_stockMapper.StockFromCreateStockDTO(dto));
            return CreatedAtAction(nameof(Get), new { stock.Id }, stock);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDTO dto)
        {
            var stock = await _stockRepo.UpdateAsync(id, dto);
            if (stock is null)
                return NotFound($"No stock with id {id}");
            return Ok(stock);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock is null)
                return NotFound($"No stock with id {id}");
            return Ok(stock);
        }
    }
}
